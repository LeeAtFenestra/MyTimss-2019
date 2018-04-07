
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class EROCSchool
    Inherits BasePagePublic



    Public ReadOnly Property GradelId() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("id"))
        End Get
    End Property

    Public ReadOnly Property HasGradeId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradelId())
        End Get
    End Property


    Public ReadOnly Property Frame_N_() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("Frame_N_"))
        End Get
    End Property

    Public ReadOnly Property HasFrame_N_() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Frame_N_())
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()

        If Not HasGradeId() Then
            Return
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        Dim dr As DataRow = TimssBll.GetSchoolDetailsDataRow(Me.GradelId)

        db_PersonContactTitle.Text = ""
        db_PersonContacted.Text = ""
        db_DateContacted.Text = ""
        db_OutcomeOfTheCall.Text = ""
        db_AdditionalNotes.Text = ""

        Me.db_frame_n_.Value = Me.Frame_N_
        Me.db_id.Value = Me.GradelId
        Dim disp As String = IIf(dr("Disp") Is DBNull.Value, "", dr("Disp"))
        SchoolName.Text = IIf(dr("s_name") Is DBNull.Value, 0, dr("s_name"))
        SchoolID.Text = Me.GradelId
        Study.Text = IIf(dr("fldProjectDesc") Is DBNull.Value, "", dr("fldProjectDesc"))
        Me.db_fldProjectID.Value = IIf(dr("fldProjectID") Is DBNull.Value, 0, dr("fldProjectID"))

        db_Disp.Items.Clear()
        db_Disp.DataSource = TimssBll.GetDispCodesNameValuePairArrayList(True)
        db_Disp.DataBind()
        TimssBll.SetDropDownListSelectedValue(db_Disp, disp)

        'Try
        '    db_Disp.SelectedValue = disp
        'Catch ex As Exception

        'End Try

        Dim args As New GetSchoolEROCArrayListArgs()
        RepeaterEROCList.DataSource = TimssBll.GetSchoolsEROCDataTable(Me.GradelId)
        RepeaterEROCList.DataBind()

    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click

        TimssBll.SaveSchoolErocChanges(Me.Form.Controls, Me.GradelId, Nothing)
        'Me.BindData()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.opener.location.href=window.opener.location.href;window.close();</script>")

    End Sub

    'Protected Sub ButtonPrint_Click(sender As Object, e As System.EventArgs) Handles ButtonPrint.Click
    '    Dim pdfDocumentBytes As Byte() = New Winnovative.WnvHtmlConvert.PdfConverter().GetPdfBytesFromUrl("www.gogle.com")

    '    Response.AddHeader("Content-Disposition", [String].Format("inline; filename=GettingStarted.pdf; size={0}", pdfDocumentBytes.Length.ToString()))
    '    Response.BinaryWrite(pdfDocumentBytes)
    '    ' Note: it is important to end the response, otherwise the ASP.NET
    '    ' web page will render its content to PDF document stream
    '    Response.End()


    'End Sub

End Class
