Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Configuration.Provider
Imports System.Web.Profile
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Collections.Specialized
Imports System.Web.Hosting
Imports System.Security

Public Class TIMSSSqlStoredProcedureProfileProvider
    Inherits ProfileProvider
    ' Methods
    Private Shared Function CreateOutputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal size As Integer) As SqlParameter
        Dim param As New SqlParameter(paramName, dbType)
        param.Direction = ParameterDirection.Output
        param.Size = size
        Return param
    End Function

    Private Function CreateSprocSqlCommand(ByVal sproc As String, ByVal conn As SqlConnection, ByVal username As String, ByVal isAnonymous As Boolean) As SqlCommand
        Dim cmd As New SqlCommand(sproc, conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = Me.CommandTimeout
        cmd.Parameters.AddWithValue("@ApplicationName", Me.ApplicationName)
        cmd.Parameters.AddWithValue("@Username", username)
        cmd.Parameters.AddWithValue("@IsUserAnonymous", isAnonymous)
        Return cmd
    End Function

    Public Overrides Function DeleteInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Public Overrides Function DeleteProfiles(ByVal profiles As ProfileInfoCollection) As Integer
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Public Overrides Function DeleteProfiles(ByVal usernames As String()) As Integer
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Public Overrides Function FindInactiveProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Public Overrides Function FindProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Public Overrides Function GetAllInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Public Overrides Function GetAllProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As ProfileInfoCollection
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Friend Shared Function GetConnectionString(ByVal specifiedConnectionString As String) As String
        If Not String.IsNullOrEmpty(specifiedConnectionString) Then
            Dim connObj As ConnectionStringSettings = ConfigurationManager.ConnectionStrings.Item(specifiedConnectionString)
            If (Not connObj Is Nothing) Then
                Return connObj.ConnectionString
            End If
        End If
        Return Nothing
    End Function

    Friend Shared Function GetDefaultAppName() As String
        Try
            Dim appName As String = HostingEnvironment.ApplicationVirtualPath
            If String.IsNullOrEmpty(appName) Then
                appName = Process.GetCurrentProcess.MainModule.ModuleName
                Dim indexOfDot As Integer = appName.IndexOf("."c)
                If (indexOfDot <> -1) Then
                    appName = appName.Remove(indexOfDot)
                End If
            End If
            If String.IsNullOrEmpty(appName) Then
                Return "/"
            End If
            Return appName
        Catch exception1 As SecurityException
            Return "/"
        End Try
    End Function

    Public Overrides Function GetNumberOfInactiveProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal userInactiveSinceDate As DateTime) As Integer
        Throw New NotSupportedException("This method is not supported for this provider.")
    End Function

    Private Sub GetProfileDataFromSproc(ByVal properties As SettingsPropertyCollection, ByVal svc As SettingsPropertyValueCollection, ByVal username As String, ByVal conn As SqlConnection, ByVal userIsAuthenticated As Boolean)
        Using cmd As SqlCommand = Me.CreateSprocSqlCommand(Me._readSproc, conn, username, userIsAuthenticated)
            cmd.Parameters.RemoveAt("@IsUserAnonymous")
            Dim columnData As New List(Of ProfileColumnData)(properties.Count)
            Dim prop As SettingsProperty
            For Each prop In properties
                Dim value As New SettingsPropertyValue(prop)
                svc.Add(value)
                Dim persistenceData As String = TryCast(prop.Attributes.Item("CustomProviderData"), String)
                If Not String.IsNullOrEmpty(persistenceData) Then
                    Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                    If (chunk.Length = 3) Then
                        Dim varname As String = chunk(0)
                        Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)
                        Dim size As Integer = 0
                        If Not Integer.TryParse(chunk(2), size) Then
                            Throw New ArgumentException(("Unable to parse as integer: " & chunk(2)))
                        End If
                        columnData.Add(New ProfileColumnData(varname, value, Nothing, datatype))
                        cmd.Parameters.Add(TIMSSSqlStoredProcedureProfileProvider.CreateOutputParam(varname, datatype, size))
                    End If
                End If
            Next
            cmd.ExecuteNonQuery()
            Dim i As Integer
            For i = 0 To columnData.Count - 1
                Dim colData As ProfileColumnData = columnData.Item(i)
                Dim val As Object = cmd.Parameters.Item(colData.VariableName).Value
                Dim propValue As SettingsPropertyValue = colData.PropertyValue
                If Not (TypeOf val Is DBNull OrElse (val Is Nothing)) Then
                    propValue.PropertyValue = val
                    propValue.IsDirty = False
                    propValue.Deserialized = True
                End If
            Next i
        End Using
    End Sub

    Public Overrides Function GetPropertyValues(ByVal context As SettingsContext, ByVal collection As SettingsPropertyCollection) As SettingsPropertyValueCollection
        Dim svc As New SettingsPropertyValueCollection
        If (((Not collection Is Nothing) AndAlso (collection.Count >= 1)) AndAlso (Not context Is Nothing)) Then
            Dim username As String = CStr(context.Item("UserName"))
            Dim userIsAuthenticated As Boolean = CBool(context.Item("IsAuthenticated"))
            If String.IsNullOrEmpty(username) Then
                Return svc
            End If
            Dim conn As SqlConnection = Nothing
            Try
                conn = New SqlConnection(Me._sqlConnectionString)
                conn.Open()
                Me.GetProfileDataFromSproc(collection, svc, username, conn, userIsAuthenticated)
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
            name = "StoredProcedureDBProfileProvider"
        End If
        If String.IsNullOrEmpty(config.Item("description")) Then
            config.Remove("description")
            config.Add("description", "StoredProcedureDBProfileProvider")
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
        Me._setSproc = config.Item("setProcedure")
        If String.IsNullOrEmpty(Me._setSproc) Then
            Throw New ProviderException("setProcedure not specified")
        End If
        Me._readSproc = config.Item("readProcedure")
        If String.IsNullOrEmpty(Me._readSproc) Then
            Throw New ProviderException("readProcedure not specified")
        End If
        Dim timeout As String = config.Item("commandTimeout")
        If Not (Not String.IsNullOrEmpty(timeout) AndAlso Integer.TryParse(timeout, Me._commandTimeout)) Then
            Me._commandTimeout = 30
        End If
        config.Remove("commandTimeout")
        config.Remove("connectionStringName")
        config.Remove("applicationName")
        config.Remove("readProcedure")
        config.Remove("setProcedure")
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
        Dim pp As SettingsPropertyValue
        If (((Not username Is Nothing) AndAlso (username.Length >= 1)) AndAlso (collection.Count >= 1)) Then
            Dim conn As SqlConnection = Nothing
            Dim cmd As SqlCommand = Nothing
            Try
                Dim anyItemsToSave As Boolean = False
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
                        If (Not userIsAuthenticated AndAlso Not CBool(pp.Property.Attributes.Item("AllowAnonymous"))) Then
                            Continue For
                        End If
                        Dim persistenceData As String = TryCast(pp.Property.Attributes.Item("CustomProviderData"), String)
                        If Not String.IsNullOrEmpty(persistenceData) Then
                            Dim chunk As String() = persistenceData.Split(New Char() {";"c})
                            If (chunk.Length = 3) Then
                                Dim varname As String = chunk(0)
                                Dim datatype As SqlDbType = DirectCast([Enum].Parse(GetType(SqlDbType), chunk(1), True), SqlDbType)
                                Dim value As Object = Nothing
                                If Not (pp.IsDirty OrElse Not pp.UsingDefaultValue) Then
                                    value = DBNull.Value
                                ElseIf (pp.Deserialized AndAlso (pp.PropertyValue Is Nothing)) Then
                                    value = DBNull.Value
                                Else
                                    value = pp.PropertyValue
                                End If
                                columnData.Add(New ProfileColumnData(varname, pp, value, datatype))
                            End If
                        End If
                    Next
                    cmd = Me.CreateSprocSqlCommand(Me._setSproc, conn, username, userIsAuthenticated)
                    Dim data As ProfileColumnData
                    For Each data In columnData
                        cmd.Parameters.AddWithValue(data.VariableName, data.Value)
                        cmd.Parameters.Item(data.VariableName).SqlDbType = data.DataType
                    Next
                    cmd.ExecuteNonQuery()
                End If
            Finally
                If (Not cmd Is Nothing) Then
                    cmd.Dispose()
                End If
                If (Not conn Is Nothing) Then
                    conn.Close()
                End If
            End Try
        End If
    End Sub


    ' Properties
    Public Overrides Property ApplicationName As String
        Get
            Return Me._appName
        End Get
        Set(ByVal value As String)
            If (value Is Nothing) Then
                Throw New ArgumentNullException("ApplicationName")
            End If
            If (value.Length > &H100) Then
                Throw New ProviderException("Application name too long")
            End If
            Me._appName = value
        End Set
    End Property

    Private ReadOnly Property CommandTimeout As Integer
        Get
            Return Me._commandTimeout
        End Get
    End Property


    ' Fields
    Private _appName As String
    Private _commandTimeout As Integer
    Private _readSproc As String
    Private _setSproc As String
    Private _sqlConnectionString As String

    ' Nested Types
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure ProfileColumnData
        Public VariableName As String
        Public PropertyValue As SettingsPropertyValue
        Public Value As Object
        Public DataType As SqlDbType
        Public Sub New(ByVal var As String, ByVal pv As SettingsPropertyValue, ByVal val As Object, ByVal type As SqlDbType)
            Me.VariableName = var
            Me.PropertyValue = pv
            Me.Value = val
            Me.DataType = type
        End Sub
    End Structure
End Class