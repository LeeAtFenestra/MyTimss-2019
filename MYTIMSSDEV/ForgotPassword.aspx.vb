
Partial Class ForgotPassword
    Inherits BasePagePublic


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Request.QueryString("page")) Then
            Response.Redirect("default.aspx")
        End If

        If Request.QueryString("page").Equals("password") Then
            PanelForgotPassword.Visible = True
        End If

        If Request.Url.ToString().ToLower().Contains("mytimss.com") Or Request.Url.ToString().ToLower().Contains("mytimssdev.westat.com") Or Request.Url.ToString().ToLower().Contains("mytimssdemo.wesdemo.com") Or Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") Then
            divTIMSS.Visible = True
        ElseIf Request.Url.ToString().ToLower().Contains("myicils.com") Then
            divICILS.Visible = True
        End If

    End Sub

    Protected Sub ButtonSendPassword_Click(sender As Object, e As System.EventArgs) Handles ButtonSendPassword.Click
        Dim u As MembershipUser = FindAccount(Me.TextBoxForgotPassword.Text)
        If u Is Nothing Then
            ErrorMsg.Visible = True
        Else
            Dim newpassword As String = Nothing

            Try
                newpassword = u.ResetPassword()
            Catch ex As MembershipPasswordException
                ErrorMsg.Visible = True
                ErrorMsg.Text = "Invalid password answer. Please re-enter and try again."
                Return
            Catch ex As Exception
                ErrorMsg.Visible = True
                ErrorMsg.Text = ex.Message
                Return
            End Try

            If newpassword Is Nothing Then
                ErrorMsg.Visible = True
                ErrorMsg.Text = "Password reset failed. Please re-enter your values and try again."
                Return
            End If


            Dim FABMembershipProvider As Westat.FAB.MembershipProvider.CustomMembershipProvider
            FABMembershipProvider = CType(Membership.Provider, Westat.FAB.MembershipProvider.CustomMembershipProvider)


            If Request.Url.ToString().Contains("www.mytimss.com") Then
                Dim emailbody = "Dear User, " & vbCrLf & vbCrLf &
                "Your MyTIMSS password has been reset to:" & vbCrLf & vbCrLf &
                "[Password]" & vbCrLf & vbCrLf &
                "Please log in to https://www.mytimss.com to change your password. Once you change your password, it will expire in " & FABMembershipProvider.DaysPasswordIsValid() & " days." & vbCrLf & vbCrLf &
                "If you have any questions, you can contact the help desk at TIMSS@westat.com" & vbCrLf & vbCrLf &
                "This e-mail is automatically generated." & vbCrLf & vbCrLf &
                "Thanks, " & vbCrLf &
                "TIMSS Help Desk"

                emailbody = emailbody.Replace("[Username]", u.UserName).Replace("[Password]", newpassword)

                TimssBll.SendEmail("TIMSS@westat.com", u.Email(), "", "", "MyTIMSS Password Reset", emailbody, False, "")

            ElseIf Request.Url.ToString().Contains("www.myicils.com") Then
                Dim emailbody = "Dear User, " & vbCrLf & vbCrLf &
                "Your MyICILS password has been reset to:" & vbCrLf & vbCrLf &
                "[Password]" & vbCrLf & vbCrLf &
                "Please log in to https://www.myicils.com to change your password. Once you change your password, it will expire in " & FABMembershipProvider.DaysPasswordIsValid() & " days." & vbCrLf & vbCrLf &
                "If you have any questions, you can contact the help desk at ICILS@westat.com" & vbCrLf & vbCrLf &
                "This e-mail is automatically generated." & vbCrLf & vbCrLf &
                "Thanks, " & vbCrLf &
                "ICILS Help Desk"

                emailbody = emailbody.Replace("[Username]", u.UserName).Replace("[Password]", newpassword)

                TimssBll.SendEmail("ICILS@westat.com", u.Email(), "", "", "MyICILS Password Reset", emailbody, False, "")
            End If



            PanelDone.Visible = True
            PanelForgotPassword.Visible = False



        End If
    End Sub

    Protected Sub ButtonSendUsername_Click(sender As Object, e As System.EventArgs) Handles ButtonSendUsername.Click
        Dim u As MembershipUser = FindAccount(Me.TextBoxForgotUsername.Text)
        If u Is Nothing Then
            ErrorMsg.Visible = True
        Else
        End If
    End Sub

    Private Function FindAccount(email As String) As MembershipUser
        Dim result As MembershipUser = Nothing
        Dim username As String = Membership.GetUserNameByEmail(email)
        If Not String.IsNullOrEmpty(username) Then
            result = Membership.GetUser(username)
        End If
        Return result
    End Function
End Class
