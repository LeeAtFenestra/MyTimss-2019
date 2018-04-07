Imports Westat.TIMSS.BLL

Partial Class TIMSSSCS2015_SendEmail
    Inherits BasePagePublic

    Public ReadOnly Property EmailTemplateName() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("t"))
        End Get
    End Property

    Public ReadOnly Property HasEmailTemplateName() As Boolean
        Get
            Return Not String.IsNullOrEmpty(EmailTemplateName())
        End Get
    End Property

    Public ReadOnly Property Referer() As String
        Get
            Return Request.QueryString("referer")
        End Get
    End Property

    Public ReadOnly Property Firstname() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("fname"))
        End Get
    End Property

    Public ReadOnly Property Lastname() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("lname"))
        End Get
    End Property

    Public ReadOnly Property EmailTo() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("to"))
        End Get
    End Property

    Public ReadOnly Property EmailCC() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("cc"))
        End Get
    End Property

    Public ReadOnly Property EmailBCC() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("bcc"))
        End Get
    End Property

    Public ReadOnly Property PrimaryKey() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("pk"))
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub


    Private Sub BindData()

        Dim dr As DataRow = Nothing

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        If Me.HasEmailTemplateName Then
            dr = TimssBll.GetEmailTemplateDataRow(Me.EmailTemplateName)
        End If

        Dim EmailFrom As String = IIf(dr("EmailFrom") Is DBNull.Value, "", dr("EmailFrom"))
        Dim TempEmailCC As String = IIf(dr("EmailCC") Is DBNull.Value, "", dr("EmailCC"))
        Dim TempEmailBCC As String = IIf(dr("EmailBCC") Is DBNull.Value, "", dr("EmailBCC"))
        Dim EmailSubject As String = IIf(dr("EmailSubject") Is DBNull.Value, "", dr("EmailSubject"))
        Dim EmailBody As String = IIf(dr("EmailBody") Is DBNull.Value, "", dr("EmailBody"))

        EmailBody = EmailBody.Replace("[fname]", Me.Firstname).Replace("[lname]", Me.Lastname)

        If Not String.IsNullOrEmpty(Me.EmailBCC) Then
            TempEmailBCC = Me.EmailBCC & ";" & TempEmailBCC
        End If

        If Not String.IsNullOrEmpty(Me.EmailCC) Then
            TempEmailCC = Me.EmailCC & ";" & TempEmailCC
        End If

        TempEmailCC = Membership.GetUser(User.Identity.Name).Email & IIf(String.IsNullOrEmpty(Me.EmailCC), "", ";") & TempEmailCC

        If User.Identity.IsAuthenticated Then

        End If
        LabelEmailTo.Text = Me.EmailTo
        LabelEmailTemplateName.Text = Me.EmailTemplateName
        LabelEmailFrom.Text = EmailFrom
        LabelEmailCC.Text = TempEmailCC
        LabelEmailBCC.Text = TempEmailBCC
        LabelEmailSubject.Text = EmailSubject
        LabelEmailBody.Text = EmailBody
        'ButtonClose.Attributes.Add("onclick", "document.location='" & Me.Referer & "'")
        ButtonClose.Attributes.Add("onclick", "window.close();")

    End Sub

    Protected Sub ButtonSend_Click(sender As Object, e As System.EventArgs) Handles ButtonSend.Click

        Dim EmailFrom As String = LabelEmailFrom.Text
        Dim EmailTo As String = LabelEmailTo.Text
        Dim EmailCC As String = LabelEmailCC.Text
        Dim EmailBCC As String = LabelEmailBCC.Text
        Dim emailsubject As String = LabelEmailSubject.Text
        Dim EmailBody As String = LabelEmailBody.Text

        'EmailTo = "adriangordon@westat.com"
        'EmailCC = "adriangordon@westat.com"
        'EmailBCC = "adriangordon@westat.com"
        'EmailReplyTo = "adriangordon@westat.com"

        If TimssBll.IsDevelopmentWebsite() Or TimssBll.IsTestingWebsite() Or TimssBll.IsTrainingWebsite() Then
            emailsubject = emailsubject & " (" & Request.ServerVariables("HTTP_HOST") & ")"
        End If

        Dim EmailReplyTo As String = IIf(String.IsNullOrEmpty(EmailCC), "", EmailCC & ";") & EmailFrom
        EmailReplyTo = EmailReplyTo.Replace(";;", ",").Replace(";", ",")

        TimssBll.SendEmail(EmailFrom, EmailTo, EmailCC, EmailBCC, emailsubject, EmailBody, True, EmailReplyTo)

        TimssBll.UpdateConfirmationEmailSent(Me.PrimaryKey)

        'Response.Redirect(Me.Referer)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.close();</script>")


    End Sub

End Class
