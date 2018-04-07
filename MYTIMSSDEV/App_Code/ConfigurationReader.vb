Namespace Westat.FAB.Configuration
    ''' <summary>
    ''' This class is used a helper class to read configuration settings from the Web.config.
    ''' </summary>
    Public Class ConfigurationReader
        Public ReadOnly Property DefaultRole() As String
            Get
                Return readMySettings("fabFrame", "roleInit", "defaultRole")
            End Get
        End Property
        Public ReadOnly Property AppTitle() As String
            Get
                Return readMySettings("fabFrame", "appInit", "appTitle")
            End Get
        End Property
        Public ReadOnly Property AuditLogging() As String
            Get
                Return readMySettings("fabFrame", "appInit", "AuditLogging")
            End Get
        End Property
        Public ReadOnly Property AppFooter() As String
            Get
                Return readMySettings("fabFrame", "appInit", "appFooter")
            End Get
        End Property
        Public ReadOnly Property FromEmail() As String
            Get
                Return readMySettings("fabFrame", "appInit", "FromEmail")
            End Get
        End Property
        Public ReadOnly Property AcctDisabledMsg() As String
            Get
                Return readMySettings("fabFrame", "appInit", "AcctDisabledMsg")
            End Get
        End Property
        Public ReadOnly Property AcctLockOutMsg() As String
            Get
                Return readMySettings("fabFrame", "appInit", "AcctLockOutMsg")
            End Get
        End Property
        Public ReadOnly Property AdminContactEmail() As String
            Get
                Return readMySettings("fabFrame", "appInit", "AdminContactEmail")
            End Get
        End Property
        Public ReadOnly Property AdminContactPhone() As String
            Get
                Return readMySettings("fabFrame", "appInit", "AdminContactPhone")
            End Get
        End Property
        Public ReadOnly Property Version() As String
            Get
                Return readMySettings("fabFrame", "appInit", "Version")
            End Get
        End Property
        Public ReadOnly Property DotNetVersion() As String
            Get
                Return readMySettings("fabFrame", "appInit", "DotNetVersion")
            End Get
        End Property
        Public ReadOnly Property ReleaseDate() As String
            Get
                Return readMySettings("fabFrame", "appInit", "ReleaseDate")
            End Get
        End Property
        Public ReadOnly Property AllowUserPasswordReset() As Boolean
            Get
                Return readMySettings("fabFrame", "appInit", "AllowUserPasswordReset")
            End Get
        End Property
        Private Function readMySettings(ByVal sectionGroup As String, ByVal section As String, ByVal setting As String) As String
            Dim nvcConfigSection As NameValueCollection
            Dim strSection As String = sectionGroup + "/" + section

            nvcConfigSection = CType(ConfigurationManager.GetSection(strSection), NameValueCollection)
            If nvcConfigSection Is Nothing Then
                Return Nothing
            Else
                Return CType(nvcConfigSection.Item(setting), String)
            End If
        End Function
    End Class
End Namespace


