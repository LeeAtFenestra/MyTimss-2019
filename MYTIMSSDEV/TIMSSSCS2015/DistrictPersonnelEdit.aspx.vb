
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class DistrictPersonnelEdit
    Inherits BasePagePublic

    Public ReadOnly Property DistrictId() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("d"))
        End Get
    End Property

    Public ReadOnly Property HasDistrictId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(DistrictId())
        End Get
    End Property


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

    Public ReadOnly Property IsSuperintendent() As Boolean
        Get
            Return targetFldName().ToLower().Equals("d_super")
        End Get
    End Property

    Public ReadOnly Property IsTestDirector() As Boolean
        Get
            Return targetFldName().ToLower().Equals("d_tdirector")
        End Get
    End Property

    Public ReadOnly Property IsDistrictContact() As Boolean
        Get
            Return targetFldName().ToLower().Equals("d_contact")
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
            dr = TimssBll.GetDistrictPersonnelDataRow(Me.PersonelId)
        End If

        If TimssBll.IsNAEPStateCoordinator Then
            ButtonConfirmation.Visible = False
            ButtonEditEmailTemplate.Visible = False
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        Dim districtdr As DataRow = TimssBll.GetDistrictDetailsDataRow(Me.DistrictId)

        Dim d_addr_1 As String = ""
        Dim d_addr_2 As String = ""
        Dim d_city As String = ""
        Dim d_state As String = ""
        Dim d_zip As String = ""

        If Not districtdr Is Nothing Then
            d_addr_1 = IIf(districtdr("d_addr1") Is DBNull.Value, "", districtdr("d_addr1"))
            d_addr_2 = IIf(districtdr("d_addr2") Is DBNull.Value, "", districtdr("d_addr2"))
            d_city = IIf(districtdr("d_city") Is DBNull.Value, "", districtdr("d_city"))
            d_state = IIf(districtdr("d_state") Is DBNull.Value, "", districtdr("d_state"))
            d_zip = IIf(districtdr("d_zip") Is DBNull.Value, "", districtdr("d_zip"))
        End If

        fldname.Text = targetFldName()
        db_leaid.Text = DistrictId()
        'launchSendEmail('SchoolEmail', '<%=strSUID%>', document.form1.Email.value, '<%=Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING") %>', '', '', '&sname=' + encodeURIComponent(document.form1.SchoolName.value));
        If IsSuperintendent() Then
            ButtonEditEmailTemplate.Attributes.Add("onclick", "document.location='EditEmailTemplate.aspx?t=DistrictSuperintendentEmail&referer=" & Server.UrlEncode(Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING")) & "'")
            ButtonConfirmation.Attributes.Add("onclick", "launchSendEmail('DistrictSuperintendentEmail', '" & Me.PersonelId & "', " & db_email.ClientID & ".value,'" & Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING") & "', '', '','&fname=' + encodeURIComponent(" & db_fname.ClientID & ".value) + '&lname=' + encodeURIComponent(" & db_lname.ClientID & ".value)   );")
        ElseIf IsTestDirector() Then
            ButtonEditEmailTemplate.Attributes.Add("onclick", "document.location='EditEmailTemplate.aspx?t=DistrictTestDirectorEmail&referer=" & Server.UrlEncode(Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING")) & "'")
            ButtonConfirmation.Attributes.Add("onclick", "launchSendEmail('DistrictTestDirectorEmail', '" & Me.PersonelId & "', " & db_email.ClientID & ".value,'" & Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING") & "', '', '','&fname=' + encodeURIComponent(" & db_fname.ClientID & ".value) + '&lname=' + encodeURIComponent(" & db_lname.ClientID & ".value)   );")
        ElseIf IsDistrictContact() Then
            ButtonEditEmailTemplate.Attributes.Add("onclick", "document.location='EditEmailTemplate.aspx?t=DistrictOtherContactEmail&referer=" & Server.UrlEncode(Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING")) & "'")
            ButtonConfirmation.Attributes.Add("onclick", "launchSendEmail('DistrictOtherContactEmail', '" & Me.PersonelId & "', " & db_email.ClientID & ".value,'" & Request.ServerVariables("PATH_INFO") & "?" & Request.ServerVariables("QUERY_STRING") & "', '', '','&fname=' + encodeURIComponent(" & db_fname.ClientID & ".value) + '&lname=' + encodeURIComponent(" & db_lname.ClientID & ".value)   );")
        End If

        If dr Is Nothing Then
            db_prefix.Text = ""
            db_fname.Text = ""
            db_lname.Text = ""
            db_suffix.Text = ""
            If IsSuperintendent() Then
                db_title.Text = "Superintendent"
            ElseIf IsTestDirector() Then
                db_title.Text = "Test Director"
            ElseIf IsDistrictContact() Then
                db_title.Text = "Other Contact"
            End If

            db_addr_1.Text = ""
            db_addr_2.Text = ""
            db_city.Text = ""
            db_state.Text = ""
            db_zip.Text = ""
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
            db_addr_1.Text = IIf(dr("addr_1") Is DBNull.Value, "", dr("addr_1"))
            db_addr_2.Text = IIf(dr("addr_2") Is DBNull.Value, "", dr("addr_2"))
            db_city.Text = IIf(dr("city") Is DBNull.Value, "", dr("city"))
            db_state.Text = IIf(dr("state") Is DBNull.Value, "", dr("state"))
            db_zip.Text = IIf(dr("zip") Is DBNull.Value, "", dr("zip"))
            db_phone.Text = IIf(dr("phone") Is DBNull.Value, "", dr("phone"))
            db_phoneext.Text = IIf(dr("phoneext") Is DBNull.Value, "", dr("phoneext"))
            db_fax.Text = IIf(dr("fax") Is DBNull.Value, "", dr("fax"))
            db_email.Text = IIf(dr("email") Is DBNull.Value, "", dr("email"))

            If Not dr("ConfirmationEmailSent") Is DBNull.Value Then
                Me.ButtonConfirmation.Attributes.Add("disabled", "disabled")
                Me.ButtonConfirmation.Attributes.Add("title", "Email sent: " & dr("ConfirmationEmailSent"))
            End If
        End If

        Dim copyinfojavascript As String = "<script language = ""javascript"">" & vbCrLf & _
        "   function CopyInfo() {" & vbCrLf & _
        "    getFrm().elements['" & db_addr_1.UniqueID & "'].value = """ & d_addr_1 & """;" & vbCrLf & _
        "    getFrm().elements['" & db_addr_2.UniqueID & "'].value = """ & d_addr_2 & """;" & vbCrLf & _
        "    getFrm().elements['" & db_city.UniqueID & "'].value = """ & d_city & """;" & vbCrLf & _
        "    getFrm().elements['" & db_state.UniqueID & "'].value = """ & d_state & """;" & vbCrLf & _
        "    getFrm().elements['" & db_zip.UniqueID & "'].value = """ & d_zip & """;" & vbCrLf & _
        "   }" & vbCrLf & _
        "</script>" & vbCrLf
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "CopyInfo", copyinfojavascript)

    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click
        TimssBll.SaveDistrictPersonnelEditChanges(Me.Form.Controls, Me.PersonelId, Me.fldname.Text, Me.db_leaid.Text, Me.db_email.Text)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.opener.location.href=window.opener.location.href;window.close();</script>")
    End Sub

End Class
