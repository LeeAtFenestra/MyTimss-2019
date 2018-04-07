Imports System.Data
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL

Partial Class SchoolPersonnelEdit
    Inherits BasePagePublic



    Public ReadOnly Property PersonelId() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("pid"))
        End Get
    End Property

    Public ReadOnly Property HasPersonelId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(PersonelId())
        End Get
    End Property

    Public ReadOnly Property targetFldName() As String
        Get
            Return Server.HtmlEncode(IIf(Request.QueryString("fldname") Is Nothing, "", Request.QueryString("fldname")))
        End Get
    End Property

    Public ReadOnly Property IsPrincipal() As Boolean
        Get
            Return targetFldName().ToLower().Equals("principalid")
        End Get
    End Property

    Public ReadOnly Property IsCoordinator() As Boolean
        Get
            Return targetFldName().ToLower().Equals("coordinatorid")
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()

        Dim dr As DataRow = Nothing

        If Me.HasPersonelId Then
            dr = TimssBll.GetSchoolPersonnelDataRow(Me.PersonelId)
        End If


        If TimssBll.IsNAEPStateCoordinator Then
            ButtonConfirmation.Visible = False
            ButtonEditEmailTemplate.Visible = False
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        'prefixlist.Items.Clear()
        'prefixlist.Items.Add(New ListItem("None/Other", "none"))
        'prefixlist.Items.Add(New ListItem("Mr.", "Mr."))
        'prefixlist.Items.Add(New ListItem("Mrs.", "Mrs."))
        'prefixlist.Items.Add(New ListItem("Ms.", "Ms."))
        'prefixlist.Items.Add(New ListItem("Dr.", "Dr."))
        'prefixlist.Items.Add(New ListItem("Fr.", "Fr."))
        'prefixlist.Items.Add(New ListItem("Sr.", "Sr."))


        If IsPrincipal() Then
            ButtonEditEmailTemplate.Attributes.Add("onclick", "document.location='EditEmailTemplate.aspx?t=PrincipalEmail&referer=" & Server.UrlEncode(Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING")) & "'")
            ButtonConfirmation.Attributes.Add("onclick", "launchSendEmail('PrincipalEmail', '" & Me.PersonelId & "', " & db_email.ClientID & ".value,'" & Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING") & "', '', '','&fname=' + encodeURIComponent(" & db_fname.ClientID & ".value) + '&lname=' + encodeURIComponent(" & db_lname.ClientID & ".value)   );")
        ElseIf IsCoordinator() Then
            ButtonEditEmailTemplate.Attributes.Add("onclick", "document.location='EditEmailTemplate.aspx?t=SchoolContactEmail&referer=" & Server.UrlEncode(Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING")) & "'")
            ButtonConfirmation.Attributes.Add("onclick", "launchSendEmail('SchoolContactEmail', '" & Me.PersonelId & "', " & db_email.ClientID & ".value,'" & Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING") & "', '', '','&fname=' + encodeURIComponent(" & db_fname.ClientID & ".value) + '&lname=' + encodeURIComponent(" & db_lname.ClientID & ".value)   );")
        End If

        fldname.Text = targetFldName()
        db_frame_n_.Text = Server.HtmlEncode(Request.QueryString("s"))

        If dr Is Nothing Then
            db_prefix.Text = ""
            db_fname.Text = ""
            db_lname.Text = ""
            db_suffix.Text = ""

            If IsPrincipal() Then
                db_title.Text = "Principal"
            ElseIf IsCoordinator() Then
                db_title.Text = "Coordinator"
            End If

            db_phone.Text = ""
            db_phoneext.Text = ""
            db_fax.Text = ""
            db_email.Text = ""
        Else
            db_prefix.Text = IIf(dr("prefix") Is DBNull.Value, "", dr("prefix"))
            db_fname.Text = IIf(dr("fname") Is DBNull.Value, "", dr("fname"))
            db_lname.Text = IIf(dr("lname") Is DBNull.Value, "", dr("lname"))

            db_suffix.Text = IIf(dr("suffix") Is DBNull.Value, "", dr("suffix"))
            db_title.Text = IIf(dr("title") Is DBNull.Value, "", dr("title"))

            db_phone.Text = IIf(dr("phone") Is DBNull.Value, "", dr("phone"))
            db_phoneext.Text = IIf(dr("phoneext") Is DBNull.Value, "", dr("phoneext"))
            db_fax.Text = IIf(dr("fax") Is DBNull.Value, "", dr("fax"))
            db_email.Text = IIf(dr("email") Is DBNull.Value, "", dr("email"))

            If Not dr("ConfirmationEmailSent") Is DBNull.Value Then
                Me.ButtonConfirmation.Attributes.Add("disabled", "disabled")
                Me.ButtonConfirmation.Attributes.Add("title", "Email sent: " & dr("ConfirmationEmailSent"))
            End If

        End If

    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click

        TimssBll.SaveSchoolPersonnelEditChanges(Me.Form.Controls, Me.PersonelId, Me.fldname.Text, Me.db_frame_n_.Text, Me.db_email.Text)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.opener.location.href=window.opener.location.href;window.close();</script>")
    End Sub
End Class
