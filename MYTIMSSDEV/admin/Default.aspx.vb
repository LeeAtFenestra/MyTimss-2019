
Partial Class admin_Default
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Redirect("Accounts.aspx")
    End Sub
End Class
