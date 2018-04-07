
Partial Class admin_RoleEdit
    Inherits BasePagePublic
    Public ReadOnly Property Rolename() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("r"))
        End Get
    End Property

    Public ReadOnly Property HasRolename() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Rolename())
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not HasRolename() Then
                ButtonUpdate.Visible = False
                ButtonAddNew.Visible = True
                Me.TextBoxRoleName.Enabled = True
                Return
            Else
                Me.TextBoxRoleName.Enabled = False
                ButtonUpdate.Visible = True
                ButtonAddNew.Visible = False
            End If

        End If
    End Sub

    Protected Sub ButtonAddNew_Click(sender As Object, e As System.EventArgs) Handles ButtonAddNew.Click
        Dim r As String = Me.TextBoxRoleName.Text
        Roles.CreateRole(r)
        Response.Redirect("roles.aspx")
    End Sub

End Class
