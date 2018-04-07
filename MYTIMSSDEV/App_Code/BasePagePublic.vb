Imports Westat.TIMSS.BLL

''' <summary>
''' Intended to be a base class that all pages will inherit from and therefore be given a base level of functionality.
''' Extends the System.Web.UI.Page class.
''' </summary>

Public Class BasePagePublic
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Overrides PreInit to define a Master page file
    ''' </summary>

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        'Dim UseDefaultMasterPage As Boolean

        'UseDefaultMasterPage = ConfigurationManager.AppSettings("UseDefaultMasterPage")
        'If UseDefaultMasterPage = True Then
        '    Theme = ConfigurationManager.AppSettings("DefaultTheme")
        '    If Page.User.Identity.Name = "" Then
        '        MasterPageFile = ConfigurationManager.AppSettings("DefaultPublicMasterPage")
        '    Else
        '        If String.Compare(Page.AppRelativeVirtualPath.ToLower(), "~/public/fabregister.aspx") = 0 Then
        '            MasterPageFile = ConfigurationManager.AppSettings("DefaultPublicMasterPage")
        '        Else
        '            MasterPageFile = ConfigurationManager.AppSettings("DefaultMasterPage")
        '        End If
        '    End If
        'Else
        '    If Page.User.Identity.Name = "" Then
        '        MasterPageFile = "~/Public/MasterPagePublic.master"
        '    Else
        '        If String.Compare(Master.AppRelativeVirtualPath.ToLower(), "~/public/masterpagetopmenu.master") <> 0 Then
        '            If String.Compare(Page.AppRelativeVirtualPath.ToLower(), "~/public/fabregister.aspx") = 0 Then
        '                MasterPageFile = "~/Public/MasterPagePublic.master"
        '            Else
        '                MasterPageFile = "~/Public/MasterPage.master"
        '            End If
        '        End If
        '    End If
        'End If
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)

        If (User.Identity.IsAuthenticated) Then
            Page.ViewStateUserKey = User.Identity.Name
        End If

    End Sub

    Private mTimssBll As TIMSSBLL
    Public ReadOnly Property TimssBll() As TIMSSBLL
        Get
            If mTimssBll Is Nothing Then
                mTimssBll = New TIMSSBLL()
            End If
            Return mTimssBll
        End Get
    End Property


    Private mProf As ProfileCommon
    Public ReadOnly Property MyProfile() As ProfileCommon
        Get
            If mProf Is Nothing Then
                mProf = ProfileCommon.GetUserProfile()
            End If
            Return mProf
        End Get
    End Property

End Class


