Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Configuration.Provider
Imports System.Web.Profile
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Collections.Specialized

Public Class TIMSSSqlTableProfileProvider
    Inherits ProfileProvider

    ' Methods
    Private Shared Function CreateInputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal objValue As Object) As SqlParameter
        Dim param As New SqlParameter(paramName, dbType)
        If (objValue Is Nothing) Then
            objValue = String.Empty
        End If
        param.Value = objValue
        Return param
    End Function

    Private Shared Function CreateOutputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal size As Integer) As SqlParameter
        Dim param As New SqlParameter(paramName, dbType)
        param.Direction = ParameterDirection.Output
        param.Size = size
        Return param
    End Function

    Public Overrides Function DeleteInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
        Try
            Dim conn As SqlConnection = Nothing
            Dim cmd As SqlCommand = Nothing
            Try
                conn = New SqlConnection(Me._sqlConnectionString)
                conn.Open()
                cmd = New SqlCommand(Me.GenerateQuery(True, authenticationOption), conn)
                cmd.CommandTimeout = Me.CommandTimeout
                cmd.Parameters.Add(TIMSSSqlTableProfileProvider.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime))
                Return cmd.ExecuteNonQuery
            Finally
                If (Not cmd Is Nothing) Then
                    cmd.Dispose()
                End If
                If (Not conn Is Nothing) Then
                    conn.Close()
                    conn = Nothing
                End If
            End Try
        Catch exc As Exception
            Throw exc
        End Try
    End Function

    Public Overrides Function DeleteProfiles(ByVal usernames As String()) As Integer
        If ((usernames Is Nothing) OrElse (usernames.Length < 1)) Then
            Return 0
        End If
        Dim numProfilesDeleted As Integer = 0
        Dim beginTranCalled As Boolean = False
        Try
            Dim conn As SqlConnection = Nothing
            Try
                Dim cmd As SqlCommand
                conn = New SqlConnection(Me._sqlConnectionString)
                conn.Open()
                Dim numUsersRemaing As Integer = usernames.Length
                Do While (numUsersRemaing > 0)
                    cmd = New SqlCommand(String.Empty, conn)
                    cmd.Parameters.AddWithValue("@UserName0", usernames((usernames.Length - numUsersRemaing)))
                    Dim allUsers As New StringBuilder("@UserName0")
                    numUsersRemaing -= 1
                    Dim userIndex As Integer = 1
                    Dim iter As Integer
                    For iter = (usernames.Length - numUsersRemaing) To usernames.Length - 1
                        If (((allUsers.Length + usernames(iter).Length) + 3) >= &HFA0) Then
                            Exit For
                        End If
                        Dim userNameParam As String = ("@UserName" & userIndex)
                        allUsers.Append(",")
                        allUsers.Append(userNameParam)
                        cmd.Parameters.AddWithValue(userNameParam, usernames(iter))
                        numUsersRemaing -= 1
                        userIndex += 1
                    Next iter
                    If Not (beginTranCalled OrElse (numUsersRemaing <= 0)) Then
                        Dim beginCmd As New SqlCommand("BEGIN TRANSACTION", conn)
                        beginCmd.ExecuteNonQuery()
                        beginTranCalled = True
                    End If
                    cmd.CommandText = String.Concat(New Object() {"DELETE FROM [", Me._table, "] WHERE UserId IN ( SELECT u.UserId FROM vw_aspnet_Users u WHERE u.ApplicationId = '", Me.AppId, "' AND u.UserName IN (", allUsers.ToString, "))"})
                    cmd.CommandTimeout = Me.CommandTimeout
                    numProfilesDeleted = (numProfilesDeleted + cmd.ExecuteNonQuery)
                Loop
                If beginTranCalled Then
                    cmd = New SqlCommand("COMMIT TRANSACTION", conn)
                    cmd.ExecuteNonQuery()
                    beginTranCalled = False
                End If
            Catch exc As Exception
                If beginTranCalled Then
                    Dim cmd As New SqlCommand("ROLLBACK TRANSACTION", conn)
                    cmd.ExecuteNonQuery()
                    beginTranCalled = False
                End If
                Throw
            Finally
                If (Not conn Is Nothing) Then
                    conn.Close()
                    conn = Nothing
                End If
            End Try
        Catch exc As Exception
            Throw exc
        End Try
        Return numProfilesDeleted
    End Function

    Public Overrides Function DeleteProfiles(ByVal profiles As ProfileInfoCollection) As Integer
        If (profiles Is Nothing) Then
            Throw New ArgumentNullException("profiles")
        End If
        If (profiles.Count < 1) Then
            Throw New ArgumentException("Profiles collection is empty")
        End If
        Dim usernames As String() = New String(profiles.Count - 1) {}
        Dim iter As Integer = 0
        Dim profile As ProfileInfo
        For Each profile In profiles
            usernames(iter) = profile.UserName
            iter = iter + 1
        Next
        Return Me.DeleteProfiles(usernames)
    End Function

    Private Shared Sub EnsureValidTableOrColumnName(ByVal name As String)
        Dim i As Integer
        For i = 0 To name.Length - 1
            If Not (Char.IsLetterOrDigit(name.Chars(i)) OrElse (TIMSSSqlTableProfileProvider.s_legalChars.IndexOf(name.Chars(i)) <> -1)) Then
                Throw New ProviderException(("Table and column names cannot contain: " & name.Chars(i)))
            End If
        Next i
    End Sub

    Public Overrides Function FindInactiveProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Dim insertQuery As StringBuilder = Me.GenerateTempInsertQueryForGetProfiles(authenticationOption)
        insertQuery.Append(" AND u.UserName LIKE LOWER(@UserName) AND u.LastActivityDate <= @InactiveSinceDate")
        Dim args As SqlParameter() = New SqlParameter() {TIMSSSqlTableProfileProvider.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime), TIMSSSqlTableProfileProvider.CreateInputParam("@UserName", SqlDbType.NVarChar, usernameToMatch)}
        Return Me.GetProfilesForQuery(args, pageIndex, pageSize, insertQuery, totalRecords)
    End Function

    Public Overrides Function FindProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Dim insertQuery As StringBuilder = Me.GenerateTempInsertQueryForGetProfiles(authenticationOption)
        insertQuery.Append(" AND u.UserName LIKE LOWER(@UserName)")
        Dim args As SqlParameter() = New SqlParameter() {TIMSSSqlTableProfileProvider.CreateInputParam("@UserName", SqlDbType.NVarChar, usernameToMatch)}
        Return Me.GetProfilesForQuery(args, pageIndex, pageSize, insertQuery, totalRecords)
    End Function

    Private Function GenerateQuery(ByVal delete As Boolean, ByVal authenticationOption As ProfileAuthenticationOption) As String
        Dim cmdStr As New StringBuilder(200)
        If delete Then
            cmdStr.Append("DELETE FROM [")
        Else
            cmdStr.Append("SELECT COUNT(*) FROM [")
        End If
        cmdStr.Append(Me._table)
        cmdStr.Append("] WHERE UserId IN (SELECT u.UserId FROM vw_aspnet_Users u WHERE u.ApplicationId = '").Append(Me.AppId)
        cmdStr.Append("' AND (u.LastActivityDate <= @InactiveSinceDate)")
        Select Case authenticationOption
            Case ProfileAuthenticationOption.Anonymous
                cmdStr.Append(" AND u.IsAnonymous = 1")
                Exit Select
            Case ProfileAuthenticationOption.Authenticated
                cmdStr.Append(" AND u.IsAnonymous = 0")
                Exit Select
        End Select
        cmdStr.Append(")")
        Return cmdStr.ToString
    End Function

    Private Function GenerateTempInsertQueryForGetProfiles(ByVal authenticationOption As ProfileAuthenticationOption) As StringBuilder
        Dim cmdStr As New StringBuilder(200)
        cmdStr.Append("INSERT INTO #PageIndexForProfileUsers (UserId) ")
        cmdStr.Append("SELECT u.UserId FROM vw_aspnet_Users u, [").Append(Me._table)
        cmdStr.Append("] p WHERE ApplicationId = '").Append(Me.AppId)
        cmdStr.Append("' AND u.UserId = p.UserId")
        Select Case authenticationOption
            Case ProfileAuthenticationOption.Anonymous
                cmdStr.Append(" AND u.IsAnonymous = 1")
                Return cmdStr
            Case ProfileAuthenticationOption.Authenticated
                cmdStr.Append(" AND u.IsAnonymous = 0")
                Return cmdStr
            Case ProfileAuthenticationOption.All
                Return cmdStr
        End Select
        Return cmdStr
    End Function

    Public Overrides Function GetAllInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Dim insertQuery As StringBuilder = Me.GenerateTempInsertQueryForGetProfiles(authenticationOption)
        insertQuery.Append(" AND u.LastActivityDate <= @InactiveSinceDate")
        Dim args As SqlParameter() = New SqlParameter() {TIMSSSqlTableProfileProvider.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime)}
        Return Me.GetProfilesForQuery(args, pageIndex, pageSize, insertQuery, totalRecords)
    End Function

    Public Overrides Function GetAllProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Dim insertQuery As StringBuilder = Me.GenerateTempInsertQueryForGetProfiles(authenticationOption)
        Return Me.GetProfilesForQuery(Nothing, pageIndex, pageSize, insertQuery, totalRecords)
    End Function

    Public Overrides Function GetNumberOfInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            conn = New SqlConnection(Me._sqlConnectionString)
            conn.Open()
            cmd = New SqlCommand(Me.GenerateQuery(False, authenticationOption), conn)
            cmd.CommandTimeout = Me.CommandTimeout
            cmd.Parameters.Add(TIMSSSqlTableProfileProvider.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime))
            Dim o As Object = cmd.ExecuteScalar
            If Not ((Not o Is Nothing) AndAlso TypeOf o Is Integer) Then
                Return 0
            End If
            Return CInt(o)
        Finally
            If (Not cmd Is Nothing) Then
                cmd.Dispose()
            End If
            If (Not conn Is Nothing) Then
                conn.Close()
                conn = Nothing
            End If
        End Try
    End Function

    Private Sub GetProfileDataFromTable(ByVal properties As SettingsPropertyCollection, ByVal svc As SettingsPropertyValueCollection, ByVal username As String, ByVal conn As SqlConnection)
        Dim columnData As New List(Of ProfileColumnData)(properties.Count)
        Dim commandText As New StringBuilder("SELECT u.UserID")
        Dim cmd As New SqlCommand(String.Empty, conn)
        Dim columnCount As Integer = 0
        Dim prop As SettingsProperty
        For Each prop In properties
            Dim value As New SettingsPropertyValue(prop)
            svc.Add(value)
            Dim persistenceData As String = TryCast(prop.Attributes.Item("CustomProviderData"), String)
            If Not String.IsNullOrEmpty(persistenceData) Then
                Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                If (chunk.Length = 2) Then
                    Dim columnName As String = chunk(0)
                    Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)
                    columnData.Add(New ProfileColumnData(columnName, value, Nothing, datatype))
                    commandText.Append(", ")
                    commandText.Append(("t." & columnName))
                    columnCount += 1
                End If
            End If
        Next
        commandText.Append((" FROM [" & Me._table & "] t, vw_aspnet_Users u WHERE u.ApplicationId = '")).Append(Me.AppId)
        commandText.Append("' AND u.UserName = LOWER(@Username) AND t.UserID = u.UserID")
        cmd.CommandText = commandText.ToString
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@Username", username)
        Dim reader As SqlDataReader = Nothing
        Try
            reader = cmd.ExecuteReader
            If reader.Read Then
                Dim userId As Guid = reader.GetGuid(0)
                Dim i As Integer
                For i = 0 To columnData.Count - 1
                    Dim val As Object = reader.GetValue((i + 1))
                    Dim colData As ProfileColumnData = columnData.Item(i)
                    Dim propValue As SettingsPropertyValue = colData.PropertyValue
                    If Not (TypeOf val Is DBNull OrElse (val Is Nothing)) Then
                        propValue.PropertyValue = val
                        propValue.IsDirty = False
                        propValue.Deserialized = True
                    End If
                Next i
                If (Not reader Is Nothing) Then
                    reader.Close()
                    reader = Nothing
                End If
                TIMSSSqlTableProfileProvider.UpdateLastActivityDate(conn, userId)
            End If
        Finally
            If (Not reader Is Nothing) Then
                reader.Close()
            End If
        End Try
    End Sub

    Private Function GetProfilesForQuery(ByVal insertArgs As SqlParameter(), ByVal pageIndex As Integer, ByVal pageSize As Integer, ByVal insertQuery As StringBuilder, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        If (pageIndex < 0) Then
            Throw New ArgumentException("pageIndex")
        End If
        If (pageSize < 1) Then
            Throw New ArgumentException("pageSize")
        End If
        Dim lowerBound As Long = (pageIndex * pageSize)
        Dim upperBound As Long = ((lowerBound + pageSize) - 1)
        If (upperBound > &H7FFFFFFF) Then
            Throw New ArgumentException("pageIndex and pageSize")
        End If
        Dim conn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            conn = New SqlConnection(Me._sqlConnectionString)
            conn.Open()
            Dim cmdStr As New StringBuilder(200)
            cmd = New SqlCommand("CREATE TABLE #PageIndexForProfileUsers(IndexId int IDENTITY (0, 1) NOT NULL, UserId uniqueidentifier)", conn)
            cmd.CommandTimeout = Me.CommandTimeout
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            insertQuery.Append(" ORDER BY UserName")
            cmd = New SqlCommand(insertQuery.ToString, conn)
            cmd.CommandTimeout = Me.CommandTimeout
            If (Not insertArgs Is Nothing) Then
                Dim arg As SqlParameter
                For Each arg In insertArgs
                    cmd.Parameters.Add(arg)
                Next
            End If
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmdStr = New StringBuilder(200)
            cmdStr.Append("SELECT u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate FROM vw_aspnet_Users u, [").Append(Me._table)
            cmdStr.Append("] p, #PageIndexForProfileUsers i WHERE u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= ")
            cmdStr.Append(lowerBound).Append(" AND i.IndexId <= ").Append(upperBound)
            cmd = New SqlCommand(cmdStr.ToString, conn)
            cmd.CommandTimeout = Me.CommandTimeout
            reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
            Dim profiles As New ProfileInfoCollection
            Do While reader.Read
                Dim dtLastUpdated As DateTime = DateTime.UtcNow
                Dim username As String = reader.GetString(0)
                Dim isAnon As Boolean = reader.GetBoolean(1)
                Dim dtLastActivity As DateTime = DateTime.SpecifyKind(reader.GetDateTime(2), DateTimeKind.Utc)
                dtLastUpdated = DateTime.SpecifyKind(reader.GetDateTime(3), DateTimeKind.Utc)
                profiles.Add(New ProfileInfo(username, isAnon, dtLastActivity, dtLastUpdated, 0))
            Loop
            totalRecords = profiles.Count
            If (Not reader Is Nothing) Then
                reader.Close()
                reader = Nothing
            End If
            cmd.Dispose()
            cmd = New SqlCommand("DROP TABLE #PageIndexForProfileUsers", conn)
            cmd.ExecuteNonQuery()
            Return profiles
        Finally
            If (Not reader Is Nothing) Then
                reader.Close()
            End If
            If (Not cmd Is Nothing) Then
                cmd.Dispose()
            End If
            If (Not conn Is Nothing) Then
                conn.Close()
                conn = Nothing
            End If
        End Try
    End Function

    Public Overrides Function GetPropertyValues(ByVal context As SettingsContext, ByVal collection As SettingsPropertyCollection) As SettingsPropertyValueCollection
        Dim svc As New SettingsPropertyValueCollection
        If (((Not collection Is Nothing) AndAlso (collection.Count >= 1)) AndAlso (Not context Is Nothing)) Then
            Dim username As String = CStr(context.Item("UserName"))
            If String.IsNullOrEmpty(username) Then
                Return svc
            End If
            Dim conn As SqlConnection = Nothing
            Try
                conn = New SqlConnection(Me._sqlConnectionString)
                conn.Open()
                Me.GetProfileDataFromTable(collection, svc, username, conn)
            Finally
                If (Not conn Is Nothing) Then
                    conn.Close()
                End If
            End Try
        End If
        Return svc
    End Function

    Public Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
        If (config Is Nothing) Then
            Throw New ArgumentNullException("config")
        End If
        If String.IsNullOrEmpty(name) Then
            name = "TIMSSSqlTableProfileProvider"
        End If
        If String.IsNullOrEmpty(config.Item("description")) Then
            config.Remove("description")
            config.Add("description", "TIMSSSqlTableProfileProvider")
        End If
        MyBase.Initialize(name, config)
        Dim temp As String = config.Item("connectionStringName")
        If String.IsNullOrEmpty(temp) Then
            Throw New ProviderException("connectionStringName not specified")
        End If
        Me._sqlConnectionString = TIMSSSqlStoredProcedureProfileProvider.GetConnectionString(temp)
        If String.IsNullOrEmpty(Me._sqlConnectionString) Then
            Throw New ProviderException("connectionStringName not specified")
        End If
        Me._appName = config.Item("applicationName")
        If String.IsNullOrEmpty(Me._appName) Then
            Me._appName = TIMSSSqlStoredProcedureProfileProvider.GetDefaultAppName
        End If
        If (Me._appName.Length > &H100) Then
            Throw New ProviderException("Application name too long")
        End If
        Me._table = config.Item("table")
        If String.IsNullOrEmpty(Me._table) Then
            Throw New ProviderException("No table specified")
        End If
        TIMSSSqlTableProfileProvider.EnsureValidTableOrColumnName(Me._table)
        Dim timeout As String = config.Item("commandTimeout")
        If Not (Not String.IsNullOrEmpty(timeout) AndAlso Integer.TryParse(timeout, Me._commandTimeout)) Then
            Me._commandTimeout = 30
        End If
        config.Remove("commandTimeout")
        config.Remove("connectionStringName")
        config.Remove("applicationName")
        config.Remove("table")
        If (config.Count > 0) Then
            Dim attribUnrecognized As String = config.GetKey(0)
            If Not String.IsNullOrEmpty(attribUnrecognized) Then
                Throw New ProviderException(("Unrecognized config attribute:" & attribUnrecognized))
            End If
        End If
    End Sub

    Public Overrides Sub SetPropertyValues(ByVal context As SettingsContext, ByVal collection As SettingsPropertyValueCollection)
        Dim username As String = CStr(context.Item("UserName"))
        Dim userIsAuthenticated As Boolean = CBool(context.Item("IsAuthenticated"))
        If (((Not username Is Nothing) AndAlso (username.Length >= 1)) AndAlso (collection.Count >= 1)) Then
            Dim conn As SqlConnection = Nothing
            Dim reader As SqlDataReader = Nothing
            Dim cmd As SqlCommand = Nothing
            Try
                Dim anyItemsToSave As Boolean = False
                Dim pp As SettingsPropertyValue
                For Each pp In collection
                    If pp.IsDirty Then
                        If (Not userIsAuthenticated AndAlso Not CBool(pp.Property.Attributes.Item("AllowAnonymous"))) Then
                            Continue For
                        End If
                        anyItemsToSave = True
                        Exit For
                    End If
                Next
                If anyItemsToSave Then
                    conn = New SqlConnection(Me._sqlConnectionString)
                    conn.Open()
                    Dim columnData As New List(Of ProfileColumnData)(collection.Count)
                    For Each pp In collection
                        If ((Not userIsAuthenticated AndAlso Not CBool(pp.Property.Attributes.Item("AllowAnonymous"))) OrElse Not pp.IsDirty) Then
                            Continue For
                        End If
                        Dim persistenceData As String = TryCast(pp.Property.Attributes.Item("CustomProviderData"), String)
                        If Not String.IsNullOrEmpty(persistenceData) Then
                            Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                            If (chunk.Length = 2) Then
                                Dim columnName As String = chunk(0)
                                Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)
                                Dim value As Object = Nothing
                                If (pp.Deserialized AndAlso (pp.PropertyValue Is Nothing)) Then
                                    value = DBNull.Value
                                Else
                                    value = pp.PropertyValue
                                End If
                                columnData.Add(New ProfileColumnData(columnName, pp, value, datatype))
                            End If
                        End If
                    Next
                    Dim userId As Guid = Guid.Empty
                    cmd = New SqlCommand(("SELECT u.UserId FROM vw_aspnet_Users u WHERE u.ApplicationId = '" & Me.AppId.ToString & "' AND u.UserName = LOWER(@Username)"), conn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@Username", username)
                    Try
                        reader = cmd.ExecuteReader
                        If reader.Read Then
                            userId = reader.GetGuid(0)
                        Else
                            reader.Close()
                            cmd.Dispose()
                            reader = Nothing
                            cmd = New SqlCommand("dbo.aspnet_Users_CreateUser", conn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@ApplicationId", Me.AppId)
                            cmd.Parameters.AddWithValue("@UserName", username)
                            cmd.Parameters.AddWithValue("@IsUserAnonymous", Not userIsAuthenticated)
                            cmd.Parameters.AddWithValue("@LastActivityDate", DateTime.UtcNow)
                            cmd.Parameters.Add(TIMSSSqlTableProfileProvider.CreateOutputParam("@UserId", SqlDbType.UniqueIdentifier, &H10))
                            cmd.ExecuteNonQuery()
                            userId = DirectCast(cmd.Parameters.Item("@userid").Value, Guid)
                        End If
                    Finally
                        If (Not reader Is Nothing) Then
                            reader.Close()
                            reader = Nothing
                        End If
                        cmd.Dispose()
                    End Try
                    cmd = New SqlCommand(String.Empty, conn)
                    Dim sqlCommand As StringBuilder = New StringBuilder("IF EXISTS (SELECT 1 FROM [").Append(Me._table)
                    sqlCommand.Append("] WHERE UserId = @UserId) ")
                    cmd.Parameters.AddWithValue("@UserId", userId)
                    Dim columnStr As New StringBuilder
                    Dim valueStr As New StringBuilder
                    Dim setStr As New StringBuilder
                    Dim count As Integer = 0
                    Dim data As ProfileColumnData
                    For Each data In columnData
                        columnStr.Append(", ")
                        valueStr.Append(", ")
                        columnStr.Append(data.ColumnName)
                        Dim valueParam As String = ("@Value" & count)
                        valueStr.Append(valueParam)
                        cmd.Parameters.AddWithValue(valueParam, data.Value)
                        If (data.DataType <> SqlDbType.Timestamp) Then
                            If (count > 0) Then
                                setStr.Append(",")
                            End If
                            setStr.Append(data.ColumnName)
                            setStr.Append("=")
                            setStr.Append(valueParam)
                        End If
                        count += 1
                    Next
                    columnStr.Append(",LastUpdatedDate ")
                    valueStr.Append(",@LastUpdatedDate")
                    setStr.Append(",LastUpdatedDate=@LastUpdatedDate")
                    cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow)
                    sqlCommand.Append("BEGIN UPDATE [").Append(Me._table).Append("] SET ").Append(setStr.ToString)
                    sqlCommand.Append(" WHERE UserId = '").Append(userId).Append("'")
                    sqlCommand.Append("END ELSE BEGIN INSERT [").Append(Me._table).Append("] (UserId").Append(columnStr.ToString)
                    sqlCommand.Append(") VALUES ('").Append(userId).Append("'").Append(valueStr.ToString).Append(") END")
                    cmd.CommandText = sqlCommand.ToString
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                    If (Not reader Is Nothing) Then
                        reader.Close()
                        reader = Nothing
                    End If
                    TIMSSSqlTableProfileProvider.UpdateLastActivityDate(conn, userId)
                End If
            Finally
                If (Not reader Is Nothing) Then
                    reader.Close()
                End If
                If (Not cmd Is Nothing) Then
                    cmd.Dispose()
                End If
                If (Not conn Is Nothing) Then
                    conn.Close()
                End If
            End Try
        End If
    End Sub

    Private Shared Sub UpdateLastActivityDate(ByVal conn As SqlConnection, ByVal userId As Guid)
        Dim cmd As New SqlCommand(("UPDATE aspnet_Users SET LastActivityDate = @LastUpdatedDate WHERE UserId = '" & userId.ToString & "'"), conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow)
        Try
            cmd.ExecuteNonQuery()
        Finally
            cmd.Dispose()
        End Try
    End Sub


    ' Properties
    Private ReadOnly Property AppId As Guid
        Get
            If Not Me._appIdSet Then
                Dim conn As SqlConnection = Nothing
                Try
                    conn = New SqlConnection(Me._sqlConnectionString)
                    conn.Open()
                    Dim cmd As New SqlCommand("dbo.aspnet_Applications_CreateApplication", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@applicationname", Me.ApplicationName)
                    cmd.Parameters.Add(TIMSSSqlTableProfileProvider.CreateOutputParam("@ApplicationId", SqlDbType.UniqueIdentifier, 0))
                    cmd.ExecuteNonQuery()
                    Me._appId = DirectCast(cmd.Parameters.Item("@ApplicationId").Value, Guid)
                    Me._appIdSet = True
                Finally
                    If (Not conn Is Nothing) Then
                        conn.Close()
                    End If
                End Try
            End If
            Return Me._appId
        End Get
    End Property

    Public Overrides Property ApplicationName As String
        Get
            Return Me._appName
        End Get
        Set(ByVal value As String)
            If (value Is Nothing) Then
                Throw New ArgumentNullException("ApplicationName")
            End If
            If (value.Length > 256) Then
                Throw New ProviderException("Application name too long")
            End If
            Me._appName = value
            Me._appIdSet = False
        End Set
    End Property

    Private ReadOnly Property CommandTimeout As Integer
        Get
            Return Me._commandTimeout
        End Get
    End Property


    ' Fields
    Private _appId As Guid
    Private _appIdSet As Boolean
    Private _appName As String
    Private _commandTimeout As Integer
    Private _sqlConnectionString As String
    Private _table As String
    Private Shared s_legalChars As String = "_@#$"

    ' Nested Types
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure ProfileColumnData
        Public ColumnName As String
        Public PropertyValue As SettingsPropertyValue
        Public Value As Object
        Public DataType As SqlDbType
        Public Sub New(ByVal col As String, ByVal pv As SettingsPropertyValue, ByVal val As Object, ByVal type As SqlDbType)
            TIMSSSqlTableProfileProvider.EnsureValidTableOrColumnName(col)
            Me.ColumnName = col
            Me.PropertyValue = pv
            Me.Value = val
            Me.DataType = type
        End Sub
    End Structure
End Class
