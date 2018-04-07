
Partial Class ChangePassword
    Inherits BasePagePublic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.cpdChangePswd.Focus()
        End If
        Dim strRetryAttampts As String = Session("RetryAttampts")
        If (Session("ChangePassword") = "yes") Then
            Dim tbChangePWMsg As Label
            tbChangePWMsg = Me.cpdChangePswd.ChangePasswordTemplateContainer.FindControl("ChangePasswordMsg")
            If (tbChangePWMsg IsNot Nothing AndAlso strRetryAttampts > 0) Then
                Dim sbMsg As New StringBuilder

                If (strRetryAttampts = "1") Then
                    With sbMsg
                        .Append("Your password has expired.<br />You must change the password this time, Otherwise your account will be disabled.")
                    End With
                Else
                    strRetryAttampts = strRetryAttampts + " times"
                    With sbMsg
                        .Append("Your password has expired.<br />There are " & strRetryAttampts)
                        .Append(" left for you to change the password.<br />" & vbCrLf)
                        .Append("Otherwise your account will be disabled.")
                    End With

                End If
                tbChangePWMsg.Text = sbMsg.ToString
                tbChangePWMsg.Visible = True
            ElseIf (tbChangePWMsg IsNot Nothing) Then
                tbChangePWMsg.Visible = False
            End If
        End If
    End Sub

    Protected Sub cpdChangePswd_ChangePasswordError(ByVal sender As Object, ByVal e As System.EventArgs) Handles cpdChangePswd.ChangePasswordError
        Try
            Dim username, newPassword As String
            Dim FABMembershipProvider As Westat.FAB.MembershipProvider.CustomMembershipProvider

            FABMembershipProvider = CType(Membership.Provider, Westat.FAB.MembershipProvider.CustomMembershipProvider)
            username = Me.cpdChangePswd.UserName
            newPassword = Me.cpdChangePswd.NewPassword
            If FABMembershipProvider.PasswordUsedBefore(username, newPassword) Then
                Me.cpdChangePswd.ChangePasswordFailureText = "The new password you entered is the same password you used before, please try another password."
            Else
                Me.cpdChangePswd.ChangePasswordFailureText = "Password incorrect or New Password invalid. New Password length minimum: {0}. Non-alphanumeric characters required: {1}."
            End If
        Catch ex As Exception
            Me.cpdChangePswd.ChangePasswordFailureText = ex.Message
        End Try


    End Sub

    Protected Sub cpdChangePswd_ChangedPassword(ByVal sender As Object, ByVal e As System.EventArgs) Handles cpdChangePswd.ChangedPassword
        Response.Redirect("~/")
    End Sub
End Class
