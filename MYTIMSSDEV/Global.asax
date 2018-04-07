<%@ Application Language="VB" %>
<%@ Import Namespace="System.Net.Mail" %>
<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Protected Sub Application_Error(sender As [Object], e As EventArgs)
        Dim ex As Exception = Server.GetLastError()

        'Comment out below line to stop Exception emails
        'EmailException(ex)


        Response.Redirect("~/error.aspx")
    End Sub 'Application_Error


    Private Sub EmailException(ex As Exception)
        Dim mail As New MailMessage()

        'mail.To.Add(ConfigurationManager.AppSettings("NotifyEmailList"))

        Dim lst As String() = ConfigurationManager.AppSettings("NotifyEmailList").ToString().Split(";")
        For Each e As String In lst
            If Not String.IsNullOrEmpty(e) Then
                mail.To.Add(e)
            End If
        Next

        mail.From = New MailAddress(ConfigurationManager.AppSettings("NotificationFromEmail"))

        mail.Subject = "An exception occurred."
        Dim body As String = ""


        If User IsNot Nothing Then
            If User.Identity IsNot Nothing Then

                body += "IsAuthenticated: " & User.Identity.IsAuthenticated & vbCrLf
                If User.Identity.IsAuthenticated Then
                    body += "Username: " & User.Identity.Name & vbCrLf

                    If Membership.GetUser(User.Identity.Name) IsNot Nothing Then
                        Dim email As String = Membership.GetUser(User.Identity.Name).Email
                        body += "Email: " & email & vbCrLf
                    End If

                End If

                body += "AuthenticationType: " & User.Identity.AuthenticationType & vbCrLf & vbCrLf
            End If
        End If

        Dim Prof As ProfileCommon = ProfileCommon.GetUserProfile()

        If Prof IsNot Nothing Then
            body += "FirstName: " & Prof.FirstName & vbCrLf
            body += "MiddleName: " & Prof.MiddleName & vbCrLf
            body += "LastName: " & Prof.LastName & vbCrLf
            body += "LastPageSize: " & Prof.LastPageSize & vbCrLf
            body += "LastRegion: " & Prof.LastRegion & vbCrLf
            body += "REPSBGRP: " & Prof.REPSBGRP & vbCrLf
            body += "WINSID: " & Prof.WINSID & vbCrLf
            body += "RegistrationId: " & Prof.RegistrationId & vbCrLf
            body += "Frame_N_: " & Prof.Frame_N_ & vbCrLf
            body += vbCrLf & vbCrLf
        End If

        If Request IsNot Nothing Then
            mail.Subject += " (" & Request.ServerVariables("http_host") & ")"
            body += "Domain: " & Request.ServerVariables("http_host") & vbCrLf & vbCrLf
            body += "Path: " & Request.Url.ToString() & vbCrLf & vbCrLf
            body += "User Agent: " & Request.UserAgent.ToString() & vbCrLf & vbCrLf
        End If
        body += "Exception Details:" & vbCrLf & ex.ToString() & vbCrLf & vbCrLf


        If HttpContext.Current.Session IsNot Nothing Then
            body += "Session:" & vbCrLf & vbCrLf
            For Each key As String In HttpContext.Current.Session.Keys
                body += key & " (" & Len(Session(key)) & ") = " & Session(key) & vbCrLf
            Next
            body += vbCrLf
        End If

        If Request IsNot Nothing Then
            body += "Request.Form:" & vbCrLf & vbCrLf
            For Each key As String In Request.Form.AllKeys
                body += key & " (" & Len(Request.Form(key)) & ") = " & Request.Form(key) & vbCrLf
            Next
            body += vbCrLf



            body += "Request.QueryString:" & vbCrLf & vbCrLf
            For Each key As String In Request.QueryString.AllKeys
                body += key & " (" & Len(Request.QueryString(key)) & ") = " & Request.QueryString(key) & vbCrLf
            Next
            body += vbCrLf

            body += "Request.ServerVariables:" & vbCrLf & vbCrLf
            For Each key As String In Request.ServerVariables.AllKeys
                body += key & " = " & Request.ServerVariables(key) & vbCrLf
            Next
            body += vbCrLf

        End If

        mail.Body = body

        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'EmailException

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        ' The session variables need to be initialized here (or in Page_load event), 

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

    Sub Profile_MigrateAnonymous(ByVal sender As Object, ByVal e As ProfileMigrateEventArgs)
        'Dim anonProfile As ProfileCommon = Profile.GetProfile(e.AnonymousID)
        'Try
        '    If Not anonProfile.FirstName.ToString = "System.Object" _
        '    and Not anonProfile.FirstName.ToString = "" Then
        '        Profile.FirstName = anonProfile.FirstName
        '        Profile.LastName = anonProfile.LastName
        '        Profile.Address1 = anonProfile.Address1
        '        Profile.Address2 = anonProfile.Address2
        '        Profile.City = anonProfile.City
        '        Profile.State = anonProfile.State
        '        Profile.Zip = anonProfile.Zip
        '        Profile.Tel = anonProfile.Tel
        '        Profile.Save()
        '        AnonymousIdentificationModule.ClearAnonymousIdentifier()
        '        ProfileManager.DeleteProfile(e.AnonymousID)
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub



</script>