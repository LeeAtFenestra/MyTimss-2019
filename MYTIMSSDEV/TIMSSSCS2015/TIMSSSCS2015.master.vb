Imports Westat.TIMSS.BLL

Partial Class TIMSSSCS2015_TIMSSSCS2015
    Inherits System.Web.UI.MasterPage


    Private mTimssBll As TIMSSBLL
    Public ReadOnly Property TimssBll() As TIMSSBLL
        Get
            If mTimssBll Is Nothing Then
                mTimssBll = New TIMSSBLL()
            End If
            Return mTimssBll
        End Get
    End Property

    Public Function HighlightMenuItemCSS(targetList As String) As String
        Dim result As String = "menu"
        Dim targets As String() = targetList.Split("|")
        For Each target As String In targets
            If Request.ServerVariables("SCRIPT_NAME").ToLower().EndsWith(target.ToLower()) Then
                result = "menuOn"
                Exit For
            End If
        Next
        Return result
    End Function

    'Private prof As ProfileCommon = ProfileCommon.GetUserProfile()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Request.Url.ToString().ToLower().Contains("mytimss.com") Or Request.Url.ToString().ToLower().Contains("mytimssdev.westat.com") Or Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") Or Request.Url.ToString().ToLower().Contains("mytimssdemo.wesdemo.com") Then
                footerTIMSS.Visible = True
                IMG2.Visible = True
                IMG9.Visible = False
                rowColor1.Style.Add("background-color", "#FFC000")
                rowColor2.Style.Add("background-color", "#FFC000")
            ElseIf Request.Url.ToString().ToLower().Contains("myicils.com") Then
                footerICILS.Visible = True
                IMG2.Visible = False
                IMG9.Visible = True
                rowColor1.Style.Add("background-color", "#E66057")
                rowColor2.Style.Add("background-color", "#E66057")
            End If

            'Me.spanselect.Visible = TIMSSBLL.CanFilterByArea()

            'Me.DataBind()
            'Try
            '    Me.DataBind()
            'Catch ex As Exception

            'End Try
        End If
    End Sub

    Public Function Fullname() As String
        Dim prof As ProfileCommon = ProfileCommon.GetUserProfile()
        Return prof.FirstName & " " & prof.LastName
    End Function

    Public Function MyRoles() As String
        Dim result As String = ""
        Dim u As MembershipUser = Membership.GetUser
        Dim rolelist As String() = Roles.GetRolesForUser(u.UserName)
        For Each role As String In rolelist
            result = result & IIf(String.IsNullOrEmpty(result), "", "<br />") & role
        Next
        Return "<span>" & result & "</span>"
    End Function

    Sub HeadLoginStatus_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs)
        TimssBll.CleanUpSessionVariables()
    End Sub
End Class

