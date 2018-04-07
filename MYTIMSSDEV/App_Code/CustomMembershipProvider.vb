Imports Microsoft.VisualBasic
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports System.Net
Imports System.Xml
Imports System.Web.Management
Imports System.Security.Cryptography

Namespace Westat.FAB.MembershipProvider
    Public Class CustomMembershipProvider
        Inherits SqlMembershipProvider
        Private autoUnlockTimeout As Integer = 60
        Private strConnectionString As String
        Private _daysPasswordIsValid As Integer
        Private _bNextTimeChangePW As Boolean = False
        Private _bChangePWAfterCreateUser As Boolean = False
        Private _passwordBlackoutInterval As Integer = 0
        Private _retryAttemptsAfterPWExpires As Integer = 1
        Private _bAutoActivateUserAfterReset As Boolean = True

        'Public Enum MethodID
        '    ValidateUser = 0
        '    ChangePassword = 1
        'End Enum
        Public ReadOnly Property UnlockTimeout() As Integer
            Get
                Return autoUnlockTimeout
            End Get
        End Property
        'Default to 60 minutes
        Public Overrides Sub Initialize(ByVal name As String, ByVal config As System.Collections.Specialized.NameValueCollection)
            Dim strlockTimeOut As String = config("autoUnlockTimeout")
            If Not [String].IsNullOrEmpty(strlockTimeOut) Then
                autoUnlockTimeout = Int32.Parse(strlockTimeOut)
            End If
            _daysPasswordIsValid = config("daysPasswordIsValid")
            _passwordBlackoutInterval = config("passwordBlackoutInterval")
            _retryAttemptsAfterPWExpires = config("numberOfRetriesAfterPasswordExpires")
            If (config("changePasswordAfterReset") IsNot Nothing AndAlso config("changePasswordAfterReset").ToLower = "true") Then
                _bNextTimeChangePW = True
            End If
            If (config("changePasswordAfterCreateUser") IsNot Nothing AndAlso config("changePasswordAfterCreateUser").ToLower = "true") Then
                _bChangePWAfterCreateUser = True
            End If

            If (config("AutoActivateUserAfterReset") IsNot Nothing AndAlso config("AutoActivateUserAfterReset").ToLower = "true") Then
                _bAutoActivateUserAfterReset = True
            End If

            strConnectionString = ConfigurationManager.ConnectionStrings(config("connectionStringName")).ConnectionString

            config.Remove("autoUnlockTimeout")
            config.Remove("daysPasswordIsValid")
            config.Remove("passwordBlackoutInterval")
            config.Remove("changePasswordAfterReset")
            config.Remove("changePasswordAfterCreateUser")
            config.Remove("numberOfRetriesAfterPasswordExpires")
            config.Remove("AutoActivateUserAfterReset")

            MyBase.Initialize(name, config)
        End Sub 'Initialize
        Public ReadOnly Property DaysPasswordIsValid() As Integer
            Get
                Return _daysPasswordIsValid
            End Get
        End Property
        Public ReadOnly Property NextTimeChangePW() As Boolean
            Get
                Return _bNextTimeChangePW
            End Get
        End Property
        Public ReadOnly Property ChangePasswordAfterCreateUser() As Boolean
            Get
                Return _bChangePWAfterCreateUser
            End Get
        End Property

        Public ReadOnly Property PasswordBlackoutInterval() As Integer
            Get
                Return _passwordBlackoutInterval
            End Get
        End Property

        Public ReadOnly Property RetryAttemptsAfterPWExpires() As Integer
            Get
                Return _retryAttemptsAfterPWExpires
            End Get
        End Property
        Public ReadOnly Property AutoActivateUserAfterReset() As Integer
            Get
                Return _bAutoActivateUserAfterReset
            End Get
        End Property
        Private Function IsUserLocked(ByVal username As String) As Boolean
            Dim mu As MembershipUser = Me.GetUser(username, False)
            If autoUnlockTimeout > 0 AndAlso Not (mu Is Nothing) AndAlso mu.IsLockedOut AndAlso mu.LastLockoutDate.AddMinutes(autoUnlockTimeout) < DateTime.Now Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function AutoUnlockUser(ByVal username As String) As Boolean
            Dim mu As MembershipUser = Me.GetUser(username, False)
            Return mu.UnlockUser()
        End Function 'AutoUnlockUser
        Private Function IsUserDisabled(ByVal username As String) As Boolean
            Dim mu As MembershipUser = Me.GetUser(username, False)
            If (AutoActivateUserAfterReset() = True) AndAlso Not (mu Is Nothing) AndAlso mu.IsApproved = False Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub ActivateUser(ByVal username As String)
            Dim mu As MembershipUser = Me.GetUser(username, False)
            mu.IsApproved = True
            Me.UpdateUser(mu)
        End Sub 'ActivateUser

        Private Sub DeactivateUser(ByVal username As String)
            Dim mu As MembershipUser = Me.GetUser(username, False)
            mu.IsApproved = False
            Me.UpdateUser(mu)
        End Sub 'DeactivateUser
        Public Overrides Function ChangePassword(ByVal username As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean

            Dim bSuccess As Boolean

            If PasswordUsedBefore(username, newPassword) Then
                Return False
            End If

            bSuccess = MyBase.ChangePassword(username, oldPassword, newPassword)

            If bSuccess = False Then
                Return bSuccess
            End If

            'Only insert the password row if the password was changed
            Try
                InsertHistoryRow(username, newPassword, False, RetryAttemptsAfterPWExpires())
                'log to HM
                Dim psdChgEvent As New Westat.FAB.Security.HealthMonitoring.FAB_PasswordChangedEvent(username, Me)
                Westat.FAB.Security.HealthMonitoring.FAB_PasswordChangedEvent.Raise(psdChgEvent)
                Return bSuccess
            Catch ex As Exception
                'Attempt to cleamup after a failure to log the new password
                MyBase.ChangePassword(username, newPassword, oldPassword)
                Return False
            End Try
        End Function

        Public Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
            If IsUserLocked(username) Then
                Dim successfulUnlock As Boolean = AutoUnlockUser(username)

                If successfulUnlock = False Then
                    Dim lgnEvent As New Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationFailureAuditEvent("Failed", Me, username)
                    Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationFailureAuditEvent.Raise(lgnEvent)
                    Return successfulUnlock
                End If
            End If

            Dim bSuccess As Boolean
            bSuccess = MyBase.ValidateUser(username, password)
            If bSuccess Then
                Dim lgnEvent As New Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationSuccessAuditEvent("Succeeded", Me, username)
                Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationSuccessAuditEvent.Raise(lgnEvent)
            Else
                Dim lgnEvent As New Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationFailureAuditEvent("Failed", Me, username)
                Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationFailureAuditEvent.Raise(lgnEvent)
            End If
            Return bSuccess
        End Function
        Public Overrides Function ResetPassword(ByVal username As String, ByVal passwordAnswer As String) As String
            'A MembershipPasswordException could be due to a lockout
            Try
                Dim strNewPassword As String
                Dim evtReset As Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationSuccessAuditEvent

                If IsUserLocked(username) Then
                    Dim successfulUnlock As Boolean = AutoUnlockUser(username)

                    If successfulUnlock = False Then
                        Dim evtLockout As New Westat.FAB.Security.HealthMonitoring.FAB_AccountPasswordResetFailure("Password Reset Failed- Account Locked", Me, username, New System.Configuration.Provider.ProviderException("Cannot reset password, account locked out."))
                        Throw New System.Configuration.Provider.ProviderException("Cannot reset password, account locked out.")
                    End If
                End If


                strNewPassword = MyBase.ResetPassword(username, passwordAnswer)
                InsertHistoryRow(username, strNewPassword, NextTimeChangePW(), RetryAttemptsAfterPWExpires())
                If IsUserDisabled(username) Then
                    ActivateUser(username)
                End If

                evtReset = New Westat.FAB.Security.HealthMonitoring.FAB_WebAuthenticationSuccessAuditEvent("Password Reset", Me, username)
                Westat.FAB.Security.HealthMonitoring.FAB_AccountPasswordResetSuccess.Raise(evtReset)

                Return strNewPassword

            Catch ex As MembershipPasswordException
                'an exception will occur if the user is locked out
                'test if user locked out
                Dim evtLockout As New Westat.FAB.Security.HealthMonitoring.FAB_AccountPasswordResetFailure("Password Reset Failed", Me, username, ex)
                Westat.FAB.Security.HealthMonitoring.FAB_AccountPasswordResetFailure.Raise(evtLockout)
                Return Nothing
            End Try
        End Function 'ResetPassword

        Private Function GetRandomSaltValue() As Byte()
            Dim rcsp As New RNGCryptoServiceProvider()
            Dim bSalt As Byte() = New Byte(15) {}
            rcsp.GetBytes(bSalt)

            Return bSalt
        End Function
        Private Sub InsertHistoryRow(ByVal username As String, ByVal password As String, ByVal changePassword As Boolean, ByVal RetryAttempts As Integer)
            Try
                Using conn As New SqlConnection(strConnectionString)
                    'Setup the command
                    Dim command As String = "dbo.FAB_InsertPasswordHistoryRow"
                    Dim cmd As New SqlCommand(command, conn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    'Setup the parameters
                    Dim arrParams As SqlParameter() = New SqlParameter(7) {}
                    arrParams(0) = New SqlParameter("pUserName", SqlDbType.NVarChar, 256)
                    arrParams(1) = New SqlParameter("pApplicationName", SqlDbType.NVarChar, 256)
                    arrParams(2) = New SqlParameter("pPassword", SqlDbType.NVarChar, 128)
                    arrParams(3) = New SqlParameter("pPasswordSalt", SqlDbType.NVarChar, 128)
                    arrParams(4) = New SqlParameter("pPWBlackoutInterval ", SqlDbType.Int)
                    arrParams(5) = New SqlParameter("pChangePWNextTime", SqlDbType.Bit)
                    arrParams(6) = New SqlParameter("pRetryAttemptsAfterPWExp", SqlDbType.Int)
                    arrParams(7) = New SqlParameter("returnValue", SqlDbType.Int)

                    'Hash the password again for storage in the history table
                    Dim passwordSalt As Byte() = Me.GetRandomSaltValue()
                    Dim bytePassword As Byte() = Encoding.Unicode.GetBytes(password)
                    Dim inputBuffer As Byte() = New Byte(bytePassword.Length + 15) {}

                    Buffer.BlockCopy(bytePassword, 0, inputBuffer, 0, bytePassword.Length)
                    Buffer.BlockCopy(passwordSalt, 0, inputBuffer, bytePassword.Length, 16)

                    Dim ha As HashAlgorithm = HashAlgorithm.Create(Membership.HashAlgorithmType)
                    Dim bhashedPassword As Byte() = ha.ComputeHash(inputBuffer)
                    Dim hashedPassword As String = Convert.ToBase64String(bhashedPassword)
                    Dim stringizedPasswordSalt As String = Convert.ToBase64String(passwordSalt)

                    'Put the results into the command object
                    arrParams(0).Value = username
                    arrParams(1).Value = Me.ApplicationName
                    arrParams(2).Value = hashedPassword
                    arrParams(3).Value = stringizedPasswordSalt
                    arrParams(4).Value = PasswordBlackoutInterval()
                    arrParams(5).Value = changePassword
                    arrParams(6).Value = RetryAttempts
                    arrParams(7).Direction = ParameterDirection.ReturnValue

                    cmd.Parameters.AddRange(arrParams)

                    'Insert the row into the password history table
                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Dim procResult As Integer = CInt(arrParams(7).Value)
                    conn.Close()
                    If procResult <> 0 Then
                        Throw New Exception("An error occurred while inserting the password history row.")
                    End If
                End Using
            Catch ex As Exception
                Throw New Exception("An error occurred while inserting the password history row.")
            End Try

        End Sub

        Public Function UpdateCurrentPasswordStatus(ByVal username As String, ByVal password As String, ByVal changePassword As String, ByVal RetryAttempts As Integer) As Integer
            Try

                Using conn As New SqlConnection(strConnectionString)
                    'Setup the command
                    Dim command As String = "dbo.FAB_UpdateCurrentPasswordStatus"
                    Dim cmd As New SqlCommand(command, conn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    'Setup the parameters
                    Dim arrParams As SqlParameter() = New SqlParameter(5) {}
                    arrParams(0) = New SqlParameter("pUserName", SqlDbType.NVarChar, 256)
                    arrParams(1) = New SqlParameter("pApplicationName", SqlDbType.NVarChar, 256)
                    arrParams(2) = New SqlParameter("pPassword", SqlDbType.NVarChar, 128)
                    arrParams(3) = New SqlParameter("pChangePWNextTime", SqlDbType.Bit)
                    arrParams(4) = New SqlParameter("pRetryAttemptsAfterPWExp", SqlDbType.Int)
                    arrParams(5) = New SqlParameter("returnValue", SqlDbType.Int)


                    'Put the results into the command object
                    arrParams(0).Value = username
                    arrParams(1).Value = Me.ApplicationName
                    arrParams(2).Value = password
                    arrParams(3).Value = changePassword
                    arrParams(4).Value = RetryAttempts
                    arrParams(5).Direction = ParameterDirection.ReturnValue

                    cmd.Parameters.AddRange(arrParams)

                    'Insert the row into the password history table
                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Dim procResult As Integer = CInt(arrParams(5).Value)
                    conn.Close()
                    If procResult <> 0 Then
                        Throw New Exception("An error occurred while updating the password history row.")
                    End If
                End Using
            Catch ex As Exception
                Throw New Exception("An error occurred while updating the password history row.")
            End Try
        End Function
        Public Function ShouldChangePassword(ByVal username As String, ByVal password As String, ByRef ChangePassword As String, ByRef hashedPassword As String, ByRef RetryAttempts As Integer) As Boolean
            Dim bShouldChangePassword As Boolean = False
            Try

                Using conn As New SqlConnection(strConnectionString)
                    'Setup the command
                    Dim command As String = "dbo.FAB_GetCurrPasswordStatusForUser"
                    Dim cmd As New SqlCommand(command, conn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    'Setup the parameters
                    Dim arrParams As SqlParameter() = New SqlParameter(1) {}
                    arrParams(0) = New SqlParameter("pUserName", SqlDbType.NVarChar, 256)
                    arrParams(1) = New SqlParameter("pApplicationName", SqlDbType.NVarChar, 256)

                    arrParams(0).Value = username
                    arrParams(1).Value = Me.ApplicationName

                    cmd.Parameters.AddRange(arrParams)

                    'Fetch the password history from the database
                    Dim dsOldPasswords As New DataSet()
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dr As DataRow
                    da.Fill(dsOldPasswords)

                    Dim ha As HashAlgorithm = HashAlgorithm.Create(Membership.HashAlgorithmType)
                    If (dsOldPasswords.Tables(0).Rows.Count = 0) Then
                        Return bShouldChangePassword
                    End If
                    dr = dsOldPasswords.Tables(0).Rows(0)
                    'Dim bChangePassword As Boolean = DirectCast(dr(3), Boolean)
                    ChangePassword = DirectCast(dr(3), Boolean)
                    RetryAttempts = DirectCast(dr(4), Integer)
                    Dim oldEncodedPassword As String = DirectCast(dr(0), String)
                    Dim oldEncodedSalt As String = DirectCast(dr(1), String)
                    Dim oldSalt As Byte() = Convert.FromBase64String(oldEncodedSalt)
                    Dim bytePassword As Byte() = Encoding.Unicode.GetBytes(password)
                    Dim inputBuffer As Byte() = New Byte(bytePassword.Length + 15) {}
                    Dim dtPasswordExpire As Date
                    Dim dtPasswordCreated As Date
                    dtPasswordCreated = DirectCast(dr(2), Date)
                    Buffer.BlockCopy(bytePassword, 0, inputBuffer, 0, bytePassword.Length)
                    Buffer.BlockCopy(oldSalt, 0, inputBuffer, bytePassword.Length, 16)

                    Dim bhashedPassword As Byte() = ha.ComputeHash(inputBuffer)
                    'Dim hashedPassword As String = Convert.ToBase64String(bhashedPassword)
                    hashedPassword = Convert.ToBase64String(bhashedPassword)

                    If hashedPassword = oldEncodedPassword Then
                        If ChangePassword Then
                            bShouldChangePassword = ChangePassword
                        Else
                            dtPasswordExpire = DateAdd(DateInterval.DayOfYear, CDbl(DaysPasswordIsValid()), dtPasswordCreated)
                            If dtPasswordExpire <= Now Then
                                bShouldChangePassword = True
                            End If

                        End If
                    End If
                End Using
                'No matching passwords were found if you make it this far
                Return bShouldChangePassword
            Catch ex As Exception
                Throw New Exception("An error occurred while checking the password history row.")
            End Try
        End Function

        Public Function PasswordUsedBefore(ByVal username As String, ByVal password As String) As Boolean
            Using conn As New SqlConnection(strConnectionString)
                'Setup the command
                Dim command As String = "dbo.FAB_GetPasswordHistory"
                Dim cmd As New SqlCommand(command, conn)
                cmd.CommandType = System.Data.CommandType.StoredProcedure

                'Setup the parameters
                Dim arrParams As SqlParameter() = New SqlParameter(1) {}
                arrParams(0) = New SqlParameter("pUserName", SqlDbType.NVarChar, 256)
                arrParams(1) = New SqlParameter("pApplicationName", SqlDbType.NVarChar, 256)

                arrParams(0).Value = username
                arrParams(1).Value = Me.ApplicationName

                cmd.Parameters.AddRange(arrParams)

                'Fetch the password history from the database
                Dim dsOldPasswords As New DataSet()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dsOldPasswords)

                Dim ha As HashAlgorithm = HashAlgorithm.Create(Membership.HashAlgorithmType)
                For Each dr As DataRow In dsOldPasswords.Tables(0).Rows
                    Dim oldEncodedPassword As String = DirectCast(dr(0), String)
                    Dim oldEncodedSalt As String = DirectCast(dr(1), String)
                    Dim oldSalt As Byte() = Convert.FromBase64String(oldEncodedSalt)

                    Dim bytePassword As Byte() = Encoding.Unicode.GetBytes(password)
                    Dim inputBuffer As Byte() = New Byte(bytePassword.Length + 15) {}

                    Buffer.BlockCopy(bytePassword, 0, inputBuffer, 0, bytePassword.Length)
                    Buffer.BlockCopy(oldSalt, 0, inputBuffer, bytePassword.Length, 16)

                    Dim bhashedPassword As Byte() = ha.ComputeHash(inputBuffer)
                    Dim hashedPassword As String = Convert.ToBase64String(bhashedPassword)

                    If hashedPassword = oldEncodedPassword Then
                        Return True
                    End If
                Next
            End Using

            'No matching passwords were found if you make it this far
            Return False
        End Function

        Public Function DeleteUserPasswordHistory(ByVal username As String) As Boolean
            Try

                Using conn As New SqlConnection(strConnectionString)
                    'Setup the command
                    Dim command As String = "dbo.FAB_DeleteUserPasswordHistory"
                    Dim cmd As New SqlCommand(command, conn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure

                    'Setup the parameters
                    Dim arrParams As SqlParameter() = New SqlParameter(2) {}
                    arrParams(0) = New SqlParameter("pUserName", SqlDbType.NVarChar, 256)
                    arrParams(1) = New SqlParameter("pApplicationName", SqlDbType.NVarChar, 256)
                    arrParams(2) = New SqlParameter("returnValue", SqlDbType.Int)

                    arrParams(0).Value = username
                    arrParams(1).Value = Me.ApplicationName
                    arrParams(2).Direction = ParameterDirection.ReturnValue

                    cmd.Parameters.AddRange(arrParams)
                    'Delete the user from the password history table
                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Dim procResult As Integer = CInt(arrParams(2).Value)
                    conn.Close()
                    If procResult <> 0 Then
                        Throw New Exception("An error occurred while deleting the user password history.")
                    End If
                End Using
            Catch ex As Exception
                Throw New Exception("An error occurred while deleting the user password history.")
            End Try
        End Function
        Public Overloads Overrides Function CreateUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, _
         ByVal providerUserKey As Object, ByRef status As MembershipCreateStatus) As MembershipUser
            Dim mu As MembershipUser
            mu = MyBase.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, _
             providerUserKey, status)
            If status <> MembershipCreateStatus.Success Then
                Return mu
            End If

            'Only insert the password row if the user was created
            Try
                InsertHistoryRow(username, password, ChangePasswordAfterCreateUser(), RetryAttemptsAfterPWExpires())
                Return mu
            Catch ex As Exception
                'Attempt to cleanup after a creation failure
                MyBase.DeleteUser(username, True)
                status = MembershipCreateStatus.ProviderError
                Return Nothing
            End Try
        End Function
        Public Overrides Function DeleteUser(ByVal username As String, ByVal deleteAllRelatedData As Boolean) As Boolean

            'Only insert the password row if the user was created
            Try
                If (deleteAllRelatedData = True) Then
                    DeleteUserPasswordHistory(username)
                End If
                Return MyBase.DeleteUser(username, deleteAllRelatedData)
            Catch ex As Exception
                'Attempt to cleanup after a creation failure
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace
