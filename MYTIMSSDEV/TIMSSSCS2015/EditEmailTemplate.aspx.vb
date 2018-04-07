
Partial Class TIMSSSCS2015_EditEmailTemplate
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
        Dim EmailCC As String = IIf(dr("EmailCC") Is DBNull.Value, "", dr("EmailCC"))
        Dim EmailBCC As String = IIf(dr("EmailBCC") Is DBNull.Value, "", dr("EmailBCC"))
        Dim EmailSubject As String = IIf(dr("EmailSubject") Is DBNull.Value, "", dr("EmailSubject"))
        Dim EmailBody As String = IIf(dr("EmailBody") Is DBNull.Value, "", dr("EmailBody"))

        LabelEmailTemplateName.Text = Me.EmailTemplateName
        db_EmailFrom.Text = EmailFrom
        db_Emailcc.Text = EmailCC
        db_Emailbcc.Text = EmailBCC
        db_EmailSubject.Text = EmailSubject
        db_EmailBody.Text = EmailBody
        'ButtonClose.Attributes.Add("onclick", "document.location='" & Me.Referer & "'")
        ButtonClose.Attributes.Add("onclick", "window.close();")

    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click
        TimssBll.SaveEmailTemplateEditChanges(Me.Form.Controls, Me.EmailTemplateName)
        'Response.Redirect(Me.Referer)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.close();</script>")

    End Sub

End Class
