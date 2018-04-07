
Partial Class Profile
    Inherits BasePagePublic
    Private prof As ProfileCommon = ProfileCommon.GetUserProfile()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.TextBoxFirstname.Text = prof.FirstName
            Me.TextBoxLastname.Text = prof.LastName
            Dim u As MembershipUser = Membership.GetUser(User.Identity.Name)
            Me.Email.Text = u.Email
        End If
    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click

        Dim u As MembershipUser = Membership.GetUser(User.Identity.Name)
        u.Email = Me.Email.Text
        Membership.UpdateUser(u)
        prof.FirstName = Me.TextBoxFirstname.Text
        prof.LastName = Me.TextBoxLastname.Text
        prof.Save()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.opener.location.href=window.opener.location.href;window.close();</script>")
    End Sub

End Class
