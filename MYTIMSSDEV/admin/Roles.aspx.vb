
Partial Class admin_Roles
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.GridViewList.DataSource = Roles.GetAllRoles()
            Me.GridViewList.DataBind()
            'Dim u As MembershipUser = Membership.GetUser
            'Response.Write(u.ProviderUserKey.ToString() & " - " & User.Identity.Name)
        End If
    End Sub
End Class
