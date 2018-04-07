Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data
Imports System.Globalization

Partial Class SchoolEdit
    Inherits BasePagePublic

    Public Property Frame_N_() As String
        Get
            If ViewState("Frame_N_") = Nothing Then
                ViewState("Frame_N_") = ""
            End If
            Return ViewState("Frame_N_")
        End Get
        Set(ByVal value As String)
            ViewState("Frame_N_") = value
        End Set
    End Property

    Public Property Grade() As Integer
        Get
            If ViewState("Grade") = Nothing Then
                ViewState("Grade") = 0
            End If
            Return ViewState("Grade")
        End Get
        Set(ByVal value As Integer)
            ViewState("Grade") = value
        End Set
    End Property
    Public ReadOnly Property HasFrame_N_() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Frame_N_())
        End Get
    End Property

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

    Public ReadOnly Property tblGeneralCSSDisplay() As String
        Get
            If String.IsNullOrEmpty(Request.QueryString("print")) Then
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.Form("tblGeneralState"))), "block", Server.HtmlEncode(Request.Form("tblGeneralState"))))
            Else
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.QueryString("gen"))), "none", Server.HtmlEncode(Request.QueryString("gen"))))
            End If
        End Get
    End Property

    Public ReadOnly Property tblGeneralLinkCSS() As String
        Get
            Return IIf(Me.tblGeneralCSSDisplay().Equals("block"), "menuOn", "menu")
        End Get
    End Property

    Public ReadOnly Property tblPreAssessmentCSSDisplay() As String
        Get
            If String.IsNullOrEmpty(Request.QueryString("print")) Then
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.Form("tblPreAssessmentState"))), "none", Server.HtmlEncode(Request.Form("tblPreAssessmentState"))))
                'Return "none"
            Else
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.QueryString("pre"))), "none", Server.HtmlEncode(Request.QueryString("pre"))))
            End If
        End Get
    End Property

    Public ReadOnly Property tblPreAssessmentLinkCSS() As String
        Get
            Return IIf(Me.tblPreAssessmentCSSDisplay().Equals("block"), "menuOn", "menu")
        End Get
    End Property

    Public ReadOnly Property tblAssessmentCSSDisplay() As String
        Get
            If String.IsNullOrEmpty(Request.QueryString("print")) Then
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.Form("tblAssessmentState"))), "none", Server.HtmlEncode(Request.Form("tblAssessmentState"))))
                'Return "none"
            Else
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.QueryString("smt"))), "none", Server.HtmlEncode(Request.QueryString("smt"))))
            End If
        End Get
    End Property

    Public ReadOnly Property tblAssessmentLinkCSS() As String
        Get
            Return IIf(Me.tblAssessmentCSSDisplay().Equals("block"), "menuOn", "menu")
        End Get
    End Property

    Public ReadOnly Property tblPostAssessmentCSSDisplay() As String
        Get
            If String.IsNullOrEmpty(Request.QueryString("print")) Then
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.Form("tblPostAssessmentState"))), "none", Server.HtmlEncode(Request.Form("tblPostAssessmentState"))))
                'Return "none"
            Else
                Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Server.HtmlEncode(Request.QueryString("pst"))), "none", Server.HtmlEncode(Request.QueryString("pst"))))
            End If
        End Get
    End Property

    Public ReadOnly Property tblPostAssessmentLinkCSS() As String
        Get
            Return IIf(Me.tblPostAssessmentCSSDisplay().Equals("block"), "menuOn", "menu")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
            'If Not String.IsNullOrEmpty(Request.QueryString("print")) Then
            '    Dim gen As String = Server.HtmlEncode(IIf(IIf(String.IsNullOrEmpty(Request.QueryString("gen")), "none", Request.QueryString("gen")) = "block", "true", "false"))
            '    Dim cnt As String = Server.HtmlEncode(IIf(IIf(String.IsNullOrEmpty(Request.QueryString("cnt")), "none", Request.QueryString("cnt")) = "block", "true", "false"))
            '    Dim pre As String = Server.HtmlEncode(IIf(IIf(String.IsNullOrEmpty(Request.QueryString("pre")), "none", Request.QueryString("pre")) = "block", "true", "false"))
            '    Dim smt As String = Server.HtmlEncode(IIf(IIf(String.IsNullOrEmpty(Request.QueryString("smt")), "none", Request.QueryString("smt")) = "block", "true", "false"))
            '    Dim pst As String = Server.HtmlEncode(IIf(IIf(String.IsNullOrEmpty(Request.QueryString("pst")), "none", Request.QueryString("pst")) = "block", "true", "false"))
            '    Response.Write(Request.QueryString)
            '    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "showsections", "<script language = ""javascript"">showSection(" & gen & "," & cnt & "," & pre & "," & smt & "," & pst & ");</script>")

            'End If
        End If
    End Sub

    Private Sub BindData()
        If Not HasGradeId() Then
            Return
        End If

        Me.Frame_N_ = TimssBll.GetFrameN(Me.GradelId)

        If Not HasFrame_N_() Then
            Return
        End If

        If TimssBll.IsNAEPStateCoordinator Then
            ButtonErroc.Visible = False
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        Dim personel As DataTable = TimssBll.GetSchoolPersonnelDataTable(Me.Frame_N_)

        Dim dr As DataRow = TimssBll.GetSchoolDetailsDataRow(Me.GradelId)

        'Dim grade As Integer = dr("SMPGRD")
        Me.Grade = dr("SMPGRD")
        Dim leaid As String = dr("leaid")

        Dim REPSBGRP As String = dr("REPSBGRP")
        ViewState("REPSBGRP") = REPSBGRP.ToString()

        Dim principalID As Integer = IIf(dr("principalID") Is DBNull.Value, 0, dr("principalID"))
        Dim coordinatorid As Integer = IIf(dr("coordinatorid") Is DBNull.Value, 0, dr("coordinatorid"))

        'HrefPrincipal.HRef = "javascript:AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=principalID', 'mywin', 500, 250,1)}"
        HrefPrincipal.HRef = "javascript:if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=principalID', 'mywin', 600, 250,1)}"
        'HrefPrincipal.Attributes.Add("onclick", "AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=principalID', 'mywin', 500, 250,1)}")
        HrefPrincipal.InnerText = "Add"

        'HrefCoordinator.HRef = "javascript:AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=coordinatorid', 'mywin', 500, 250,1)}"
        HrefCoordinator.HRef = "javascript:if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=coordinatorid', 'mywin', 600, 250,1)}"
        'HrefCoordinator.Attributes.Add("onclick", "AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=coordinatorid', 'mywin', 500, 250,1)}")
        HrefCoordinator.InnerText = "Add"

        If personel.Rows.Count < 0 Then
            db_principalID.Visible = False
            db_coordinatorid.Visible = False
        Else
            db_principalID.Visible = True
            db_coordinatorid.Visible = True

            db_principalID.DataSource = personel
            db_principalID.DataBind()

            db_coordinatorid.DataSource = personel
            db_coordinatorid.DataBind()

            'HrefPrincipal.HRef = "javascript:AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=principalID&pid=' + getFrm().elements['" & db_principalID.UniqueID & "'].options[getFrm().elements['" & db_principalID.UniqueID & "'].selectedIndex].value, 'mywin', 500, 250,1)}"
            HrefPrincipal.HRef = "javascript:if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=principalID&pid=' + getFrm().elements['" & db_principalID.UniqueID & "'].options[getFrm().elements['" & db_principalID.UniqueID & "'].selectedIndex].value, 'mywin', 600, 250,1)}"
            'HrefPrincipal.Attributes.Add("onclick", "AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=principalID&pid=' + getFrm().elements['" & db_principalID.UniqueID & "'].options[getFrm().elements['" & db_principalID.UniqueID & "'].selectedIndex].value, 'mywin', 500, 250,1)}")

            'HrefCoordinator.HRef = "javascript:AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=coordinatorid&pid=' + getFrm().elements['" & db_coordinatorid.UniqueID & "'].options[getFrm().elements['" & db_coordinatorid.UniqueID & "'].selectedIndex].value, 'mywin', 500, 250,1)}"
            HrefCoordinator.HRef = "javascript:if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=coordinatorid&pid=' + getFrm().elements['" & db_coordinatorid.UniqueID & "'].options[getFrm().elements['" & db_coordinatorid.UniqueID & "'].selectedIndex].value, 'mywin', 600, 250,1)}"
            'HrefCoordinator.Attributes.Add("onclick", "AutoPostbackControlId = '" & Me.ButtonSave.UniqueID & "';if(Personnelautosave()){popUp('SchoolPersonnelEdit.aspx?s=" & Me.Frame_N_() & "&fldname=coordinatorid&pid=' + getFrm().elements['" & db_coordinatorid.UniqueID & "'].options[getFrm().elements['" & db_coordinatorid.UniqueID & "'].selectedIndex].value, 'mywin', 500, 250,1)}")

            TIMSSBLL.SetDropDownListSelectedValue(db_principalID, principalID)

            'Try
            '    db_principalID.SelectedValue = principalID
            'Catch ex As Exception

            'End Try

            If principalID <> 0 Then
                HrefPrincipal.InnerText = "Edit"
            End If

            TimssBll.SetDropDownListSelectedValue(db_coordinatorid, coordinatorid)
            'Try
            '    db_coordinatorid.SelectedValue = coordinatorid
            'Catch ex As Exception

            'End Try

            If coordinatorid <> 0 Then
                HrefCoordinator.InnerText = "Edit"
            End If
        End If


        'If Request.Url.ToString().Contains("mytimsstst.wesdemo") Then
        'HyperLinkMyTIMSS.Visible = True
        'End If

        HyperLinkMyTIMSS.NavigateUrl = "~/ProvideSchoolInformation.aspx?s=" & Me.Frame_N_

        If dr("fldProjectDesc").ToString().Contains("TIMSS") Then
            HyperLinkMyTIMSS.Text = "MyTIMSS"
        ElseIf dr("fldProjectDesc").ToString().Contains("ICILS") Then
            HyperLinkMyTIMSS.Text = "MyICILS"
        Else
            HyperLinkMyTIMSS.Text = "MySchool"
        End If


        DistrictUpdatePageHyperLink.NavigateUrl = "DistrictEdit.aspx?d=" & leaid
        DistrictHyperLink.Text = IIf(dr("d_name") Is DBNull.Value, "", dr("d_name"))
        DistrictHyperLink.NavigateUrl = "DistrictEdit.aspx?d=" & leaid

        Region.Text = IIf(dr("testregion") Is DBNull.Value, "", dr("testregion"))
        Territory.Text = IIf(dr("fldTerritoryCode") Is DBNull.Value, "", dr("fldTerritoryCode"))
        Area.Text = IIf(dr("fldAreaCode") Is DBNull.Value, "", dr("fldAreaCode"))
        SchoolName.Text = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))
        SampledGrade.Text = grade
        TIMSSID.Text = Me.GradelId
        ProjectName.Text = IIf(dr("fldProjectDesc") Is DBNull.Value, "", dr("fldProjectDesc"))
        ProjectName2.Text = ProjectName.Text

        If ProjectName.Text.Contains("TIMSS") Then
            lblG8Math.Text = "Number of 8<sup>th</sup>-grade math classes: "
        Else
            lblG8Math.Text = "Number of 8<sup>th</sup>-grade teachers: "
        End If

        lblOrigOrSub.Text = dr("ORIGSUB")

        '*********************************************************************************************************************
        'General Tab
        '*********************************************************************************************************************

        db_s_name.Text = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))
        db_s_addr1.Text = IIf(dr("S_Addr1") Is DBNull.Value, "", dr("S_Addr1"))
        db_s_addr2.Text = IIf(dr("S_Addr2") Is DBNull.Value, "", dr("S_Addr2"))
        db_s_city.Text = IIf(dr("S_City") Is DBNull.Value, "", dr("S_City"))
        db_s_state.Text = IIf(dr("S_State") Is DBNull.Value, "", dr("S_State"))
        db_s_zip.Text = IIf(dr("S_Zip") Is DBNull.Value, "", dr("S_Zip"))
        db_s_county.Text = IIf(dr("S_County") Is DBNull.Value, "", dr("S_County"))
        db_MailAddr1.Text = IIf(dr("MailAddr1") Is DBNull.Value, "", dr("MailAddr1"))
        db_MailAddr2.Text = IIf(dr("MailAddr2") Is DBNull.Value, "", dr("MailAddr2"))
        db_MailCity.Text = IIf(dr("MailCity") Is DBNull.Value, "", dr("MailCity"))
        db_MailState.Text = IIf(dr("MailState") Is DBNull.Value, "", dr("MailState"))
        db_MailZip.Text = IIf(dr("MailZip") Is DBNull.Value, "", dr("MailZip"))
        db_s_phone.Text = IIf(dr("S_Phone") Is DBNull.Value, "", dr("S_Phone"))
        SchoolType.Text = IIf(dr("TypeName") Is DBNull.Value, "", dr("TypeName"))
        db_SEASCH.Text = IIf(dr("SEASCH") Is DBNull.Value, "", dr("SEASCH"))

        ' If Not Request.Url.ToString().ToLower().Contains("www.mytimss.com") Then
        rowRegID.Visible = True
        Me.LabelRegistrationID.Text = IIf(dr("MyNAEPREGID") Is DBNull.Value, "", dr("MyNAEPREGID"))
        ' Else
        'rowRegID.Visible = False
        ' End If

        'new fields 12/11/17
        If TIMSSBLL.MyRoles().Contains("Home Office") = True Then
            If dr("IEA_ID") IsNot DBNull.Value Then
                rowIEAID.Visible = True
                LabelIEAID.Text = dr("IEA_ID")
            Else
                rowIEAID.Visible = False
            End If

            rowSpecialCase.Visible = True
            TIMSSBLL.SetDropDownListSelectedValue(dbgrade_SPECIAL_CASE, IIf(dr("SPECIAL_CASE") Is DBNull.Value, "N", dr("SPECIAL_CASE")))
            dbgrade_SPECIAL_CASE.DataBind()

        Else
            rowIEAID.Visible = False
            rowSpecialCase.Visible = False
        End If

        WeatherHref.HRef = "javascript:openAWindow('http://www.weather.com/weather/today/" & db_s_zip.Text & "', 'mywin', 800, 800,1)"
        TimeHref.HRef = "javascript:openAWindow('https://www.google.com/search?q=time+" & db_s_zip.Text & "', 'mywin', 800, 800,1)"
        DirectionsHref.HRef = "javascript:openAWindow('http://maps.google.com/?saddr=Current+location&daddr=" & db_s_addr1.Text & "&nbsp;&nbsp;" & db_s_city.Text & "&nbsp;" & db_s_state.Text & "&nbsp;" & db_s_zip.Text & "', 'mywin', 800, 800,0)"

        ButtonErroc.Attributes.Add("onclick", "openAWindow('EROCSchool.aspx?id=" & Me.GradelId & "&frame_n_=" & Me.Frame_N_ & "', 'mywin', 800, 800,1)")

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "initweekofday", "<script language = ""javascript"">weekofday(getFrm().elements['" & dbgrade_ScheDate.UniqueID & "'])</script>")

        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "initChgTIMSSDISPComments", "<script language = ""javascript"">toggle_ChgTIMSSDISPComments(getFrm().elements['" & dbgrade_DISP.UniqueID & "'])</script>")

        dbgrade_sch_partltrsentdt.Items.Clear()
        dbgrade_sch_partltrsentdt.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2017, 5, 1), New Date(2018, 4, 30))
        dbgrade_sch_partltrsentdt.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_sch_partltrsentdt, IIf(dr("sch_partltrsentdt") Is DBNull.Value, "", dr("sch_partltrsentdt")))
        'dbgrade_sch_partltrsentdt.SelectedValue = IIf(dr("sch_partltrsentdt") Is DBNull.Value, "", dr("sch_partltrsentdt"))

        dbgrade_SchAsmtLtrSentDT.Items.Clear()
        dbgrade_SchAsmtLtrSentDT.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2017, 6, 1), New Date(2018, 4, 30))
        dbgrade_SchAsmtLtrSentDT.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_SchAsmtLtrSentDT, IIf(dr("SchAsmtLtrSentDT") Is DBNull.Value, "", dr("SchAsmtLtrSentDT")))
        'dbgrade_SchAsmtLtrSentDT.SelectedValue = IIf(dr("SchAsmtLtrSentDT") Is DBNull.Value, "", dr("SchAsmtLtrSentDT"))

        dbgrade_AugSchLtrSentDT.Items.Clear()
        dbgrade_AugSchLtrSentDT.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2017, 8, 1), New Date(2018, 4, 30))
        dbgrade_AugSchLtrSentDT.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_AugSchLtrSentDT, IIf(dr("AugSchLtrSentDT") Is DBNull.Value, "", dr("AugSchLtrSentDT")))
        'dbgrade_AugSchLtrSentDT.SelectedValue = IIf(dr("AugSchLtrSentDT") Is DBNull.Value, "", dr("AugSchLtrSentDT"))

        Enrollment.Text = IIf(dr("ESTGRE") Is DBNull.Value, "", dr("ESTGRE"))

        Dim disp As String = IIf(dr("Disp") Is DBNull.Value, "", dr("Disp"))

        dbgrade_DISP.Items.Clear()
        dbgrade_DISP.DataSource = TimssBll.GetDispCodesNameValuePairArrayList()
        dbgrade_DISP.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_DISP, disp)
        'dbgrade_DISP.SelectedValue = disp

        'Keeps Original Disp value if changed
        hiddenOriginalDISPcode.Text = dbgrade_DISP.SelectedValue
        hiddenOriginalDISPtext.Text = dbgrade_DISP.SelectedItem.Text

        dbgrade_ChgTIMSSDISPComments.Text = IIf(dr("ChgTIMSSDISPComments") Is DBNull.Value, "", dr("ChgTIMSSDISPComments"))

        Dim ScheDate As String = IIf(dr("ScheDate") Is DBNull.Value, "", dr("ScheDate"))
        Dim ScheTime As String = IIf(dr("ScheTime") Is DBNull.Value, "", dr("ScheTime"))

        dbgrade_ScheDate.Items.Clear()
        dbgrade_ScheDate.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 3, 1), New Date(2018, 6, 8))
        dbgrade_ScheDate.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_ScheDate, ScheDate)
        'dbgrade_ScheDate.SelectedValue = ScheDate

        dbgrade_ScheTime.Text = ScheTime
        dbgrade_ArrivalTime.Text = IIf(dr("ArrivalTime") Is DBNull.Value, "", dr("ArrivalTime"))
        db_s_comment.Text = IIf(dr("s_comment") Is DBNull.Value, "", dr("s_comment"))


        Dim ScheDate2 As String = IIf(dr("ScheDate2") Is DBNull.Value, "", dr("ScheDate2"))
        Dim ScheTime2 As String = IIf(dr("ScheTime2") Is DBNull.Value, "", dr("ScheTime2"))

        'dbgrade_ScheDate2.Items.Clear()
        'dbgrade_ScheDate2.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 3, 1), New Date(2018, 6, 8))
        'dbgrade_ScheDate2.DataBind()
        ' TimssBll.SetDropDownListSelectedValue(dbgrade_ScheDate2, ScheDate2)

        ' dbgrade_ScheTime2.Text = ScheTime2
        ' dbgrade_ArrivalTime2.Text = IIf(dr("ArrivalTime2") Is DBNull.Value, "", dr("ArrivalTime2"))

        ' PanelScheDateTime2.Visible = dr("isETIMSS")
        ' If dr("isETIMSS") Then
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "initweekofday2", "<script language = ""javascript"">weekofday2(getFrm().elements['" & dbgrade_ScheDate2.UniqueID & "'])</script>")
        ' End If


        If Not (disp = "03" Or disp = "05" Or disp = "30" Or disp = "32" Or disp = "33" Or disp = "34" Or disp = "40" Or disp = "41") Then
            divChgTIMSSDISPComments.Attributes.Add("style", "display:none;")
            'ValidatorEnable(document.getElementById("<%=RequiredFieldValidatordbgrade_ChgTIMSSDISPComments.ClientID%>"), false);
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "disableRequiredFieldValidatordbgrade_ChgTIMSSDISPComments", "<script language = ""javascript"">ValidatorEnable(document.getElementById(""" & RequiredFieldValidatordbgrade_ChgTIMSSDISPComments.ClientID & """), false);</script>")

        End If

        If Me.Grade = 4 Then
            PanelG4.Visible = True
            PanelG8.Visible = False
            PanelG12.Visible = False
            dbG4_DateSchoolReturnsFromWinterBreak.Items.Clear()
            dbG4_DateSchoolReturnsFromWinterBreak.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 1, 1), New Date(2018, 3, 1))
            dbG4_DateSchoolReturnsFromWinterBreak.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG4_DateSchoolReturnsFromWinterBreak, IIf(dr("DateSchoolReturnsFromWinterBreak") Is DBNull.Value, "", dr("DateSchoolReturnsFromWinterBreak")))

            dbG4_DateSchoolReturnsFromSpringBreak.Items.Clear()
            dbG4_DateSchoolReturnsFromSpringBreak.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 3, 1), New Date(2018, 5, 30))
            dbG4_DateSchoolReturnsFromSpringBreak.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG4_DateSchoolReturnsFromSpringBreak, IIf(dr("DateSchoolReturnsFromSpringBreak") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak")))

            dbG4_LastDayofSchool.Items.Clear()
            dbG4_LastDayofSchool.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 4, 1), New Date(2018, 7, 31))
            dbG4_LastDayofSchool.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG4_LastDayofSchool, IIf(dr("LastDayofSchool") Is DBNull.Value, "", dr("LastDayofSchool")))

            dbG4_EnrollmentAtGrade.Text = IIf(dr("EnrollmentAtGrade") Is DBNull.Value, "", dr("EnrollmentAtGrade"))
            dbG4_NumberOfClasses.Text = IIf(dr("NumberOfClasses") Is DBNull.Value, "", dr("NumberOfClasses"))
        End If

        If Me.Grade = 8 Then
            PanelG4.Visible = False
            PanelG8.Visible = True
            PanelG12.Visible = False
            dbG8_DateSchoolReturnsFromWinterBreak.Items.Clear()
            dbG8_DateSchoolReturnsFromWinterBreak.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 1, 1), New Date(2018, 3, 1))
            dbG8_DateSchoolReturnsFromWinterBreak.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG8_DateSchoolReturnsFromWinterBreak, IIf(dr("DateSchoolReturnsFromWinterBreak") Is DBNull.Value, "", dr("DateSchoolReturnsFromWinterBreak")))

            dbG8_DateSchoolReturnsFromSpringBreak.Items.Clear()
            dbG8_DateSchoolReturnsFromSpringBreak.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 3, 1), New Date(2018, 5, 30))
            dbG8_DateSchoolReturnsFromSpringBreak.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG8_DateSchoolReturnsFromSpringBreak, IIf(dr("DateSchoolReturnsFromSpringBreak") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak")))

            dbG8_LastDayofSchool.Items.Clear()
            dbG8_LastDayofSchool.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 4, 1), New Date(2018, 7, 31))
            dbG8_LastDayofSchool.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG8_LastDayofSchool, IIf(dr("LastDayofSchool") Is DBNull.Value, "", dr("LastDayofSchool")))

            dbG8_EnrollmentAtGrade.Text = IIf(dr("EnrollmentAtGrade") Is DBNull.Value, "", dr("EnrollmentAtGrade"))
            dbG8_NumberOfMathClasses.Text = IIf(dr("NumberOfMathClasses") Is DBNull.Value, "", dr("NumberOfMathClasses"))
        End If

        If Me.Grade = 12 Then
            PanelG4.Visible = False
            PanelG8.Visible = False
            PanelG12.Visible = True

            dbG12_DateSchoolReturnsFromWinterBreak.Items.Clear()
            dbG12_DateSchoolReturnsFromWinterBreak.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2018, 1, 1), New Date(2018, 3, 1))
            dbG12_DateSchoolReturnsFromWinterBreak.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG12_DateSchoolReturnsFromWinterBreak, IIf(dr("DateSchoolReturnsFromWinterBreak") Is DBNull.Value, "", dr("DateSchoolReturnsFromWinterBreak")))

            dbG12_DateSchoolReturnsFromSpringBreak.Items.Clear()
            dbG12_DateSchoolReturnsFromSpringBreak.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2017, 1, 1), New Date(2018, 6, 30))
            dbG12_DateSchoolReturnsFromSpringBreak.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG12_DateSchoolReturnsFromSpringBreak, IIf(dr("DateSchoolReturnsFromSpringBreak") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak")))

            dbG12_LastDayofSchool.Items.Clear()
            dbG12_LastDayofSchool.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2017, 1, 1), New Date(2018, 6, 30))
            dbG12_LastDayofSchool.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG12_LastDayofSchool, IIf(dr("LastDayofSchool") Is DBNull.Value, "", dr("LastDayofSchool")))

            dbG12_EnrollmentAtGrade.Text = IIf(dr("EnrollmentAtGrade") Is DBNull.Value, "", dr("EnrollmentAtGrade"))

            dbG12_AdvancedEligibility.Items.Clear()
            dbG12_AdvancedEligibility.DataSource = TimssBll.GetAdvancedEligibilityNameValuePairList()
            dbG12_AdvancedEligibility.DataBind()
            TimssBll.SetDropDownListSelectedValue(dbG12_AdvancedEligibility, IIf(dr("AdvancedEligibility") Is DBNull.Value, "", dr("AdvancedEligibility")))

            dbG12_AdvancedEligibilityComments.Text = IIf(dr("AdvancedEligibilityComments") Is DBNull.Value, "", dr("AdvancedEligibilityComments"))
            dbG12_AdvancedMathComments.Text = IIf(dr("AdvancedMathComments") Is DBNull.Value, "", dr("AdvancedMathComments"))
            dbG12_AdvancedPhysicsComments.Text = IIf(dr("AdvancedPhysicsComments") Is DBNull.Value, "", dr("AdvancedPhysicsComments"))

        End If

        dbG4_EnrollmentAtGrade.Visible = PanelG4.Visible
        dbG8_EnrollmentAtGrade.Visible = PanelG8.Visible
        dbG12_EnrollmentAtGrade.Visible = PanelG12.Visible

        '*********************************************************************************************************************
        'Pre-Assessment Tab
        '*********************************************************************************************************************

        If Me.Grade = 12 Then
            LabelSampleListAvail.Text = "Student Listing Form Available:"
            LabelClassListAvailableDate.Text = "Student Listing Form Available Date:"
            'LabelNumberofClasses.Text = "Number of Students Sampled:"
            tdStudentsSampled.Visible = True
            tdCLFStatusCode.Visible = False
            tdCLFCompletedDate.Visible = False
            tdNumberofClasses.Visible = False
            tdStudentSampled.Visible = False
        Else
            LabelSampleListAvail.Text = "Class List Available:"
            LabelClassListAvailableDate.Text = "Class List Available Date:"
            'LabelNumberofClasses.Text = "Number of Classes Sampled:"
            tdStudentsSampled.Visible = False
            tdCLFStatusCode.Visible = True
            tdCLFCompletedDate.Visible = True
            tdNumberofClasses.Visible = True
            tdStudentSampled.Visible = True
        End If

        'SampleListAvail.Text = "YES"
        'ClassListAvailableDate.Text = "2/23/2015"

        'CLFStatusCode.Text = "Completed"
        'CLFCompletedDate.Text = "2/27/2015"
        'NumberofClasses.Text = "2"
        'StudentSampled.Text = "&nbsp;"
        'StudentsSampledAdvancedScience.Text = ""
        'StudentsSampledAdvancedMathematics.Text = ""
        'STLFStatusCode.Text = "Completed"
        'DateOfSTLFMailing.Text = "2/27/2015"
        'STLFCompletedDate.Text = "2/27/2015"
        'TTFStatusCode.Text = "Completed"
        'STFStatusCode.Text = "Completed"
        'STFSentToTADate.Text = "3/13/15"

        '*********************************************************************************************************************
        'Assessment Tab
        '*********************************************************************************************************************

        dbgrade_ParentConsentType.Items.Clear()
        dbgrade_ParentConsentType.DataSource = TimssBll.GetParentLetterTypeNameValuePairArrayList()
        dbgrade_ParentConsentType.DataBind()

        dbgrade_ParentConsentLanguage.Items.Clear()
        dbgrade_ParentConsentLanguage.DataSource = TimssBll.GetParentLetterLngNameValuePairArrayList()
        dbgrade_ParentConsentLanguage.DataBind()

        dbgrade_PreAssessmentCallCompleted.Items.Clear()
        dbgrade_PreAssessmentCallCompleted.DataSource = TimssBll.GetPreassessmentNameValuePairArrayList()
        dbgrade_PreAssessmentCallCompleted.DataBind()

        Dim theDate As Date = Nothing
        If Not String.IsNullOrEmpty(ScheDate) And DateTime.TryParse(ScheDate, theDate) Then
            AssessmentDate.Text = theDate.ToString("MM/dd/yyyy")
        Else
            AssessmentDate.Text = ""
        End If

        AssessmentTime.Text = ScheTime.ToUpper()

        PanelLabelAssessmentDateTime2.Visible = dr("iseTIMSS")

        theDate = Nothing
        'If Not String.IsNullOrEmpty(ScheDate2) And DateTime.TryParse(ScheDate2, theDate) Then
        '    AssessmentDate2.Text = theDate.ToString("MM/dd/yyyy")
        'Else
        '    AssessmentDate2.Text = ""
        'End If
        'AssessmentTime2.Text = ScheTime2.ToUpper()

        dbgrade_AssessmentLocation.Text = IIf(dr("AssessmentLocation") Is DBNull.Value, "", dr("AssessmentLocation"))
        dbgrade_AssessmentDayLogisticsInformation.Text = IIf(dr("AssessmentDayLogisticsInformation") Is DBNull.Value, "", dr("AssessmentDayLogisticsInformation"))
        TimssBll.SetDropDownListSelectedValue(dbgrade_ParentConsentType, IIf(dr("ParentConsentType") Is DBNull.Value, "", dr("ParentConsentType")))
        'ParentConsentType.SelectedValue = "IMPLICIT"
        TimssBll.SetDropDownListSelectedValue(dbgrade_ParentConsentLanguage, IIf(dr("ParentConsentLanguage") Is DBNull.Value, "", dr("ParentConsentLanguage")))
        'ParentConsentLanguage.SelectedValue = "ENGLISH"
        TimssBll.SetDropDownListSelectedValue(dbgrade_PreAssessmentCallCompleted, IIf(dr("PreAssessmentCallCompleted") Is DBNull.Value, "", dr("PreAssessmentCallCompleted")))
        'PreAssessmentCallCompleted.SelectedValue = "Y"
        dbgrade_SchoolIncentiveCheckSentTxt.Text = IIf(dr("SchoolIncentiveCheckSentTxt") Is DBNull.Value, "", dr("SchoolIncentiveCheckSentTxt"))
        dbgrade_SCIncentiveCheckSentTxt.Text = IIf(dr("SCIncentiveCheckSentTxt") Is DBNull.Value, "", dr("SCIncentiveCheckSentTxt"))

        '*********************************************************************************************************************
        'Post-Assessment 
        '*********************************************************************************************************************

        dbgrade_AssessmentCompleted.Items.Clear()
        dbgrade_AssessmentCompleted.DataSource = TimssBll.GetAssessmentNameValuePairArrayList()
        dbgrade_AssessmentCompleted.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_AssessmentCompleted, IIf(dr("AssessmentCompleted") Is DBNull.Value, "", dr("AssessmentCompleted")))
        'Assessment.SelectedValue = "YES"
        'Assessment.SelectedValue = "MAKE-UP REQUIRED"

        dbgrade_AssessmentMaterialsMailedToPearson.Items.Clear()
        dbgrade_AssessmentMaterialsMailedToPearson.DataSource = TimssBll.GetMarkNCSNameValuePairArrayList()
        dbgrade_AssessmentMaterialsMailedToPearson.DataBind()
        TimssBll.SetDropDownListSelectedValue(dbgrade_AssessmentMaterialsMailedToPearson, IIf(dr("AssessmentMaterialsMailedToPearson") Is DBNull.Value, "", dr("AssessmentMaterialsMailedToPearson")))
        'MarkNCS.SelectedValue = "Y"

        dbgrade_DateOfMakeUp.Text = IIf(dr("DateOfMakeUp") Is DBNull.Value, "", dr("DateOfMakeUp"))

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "toggleMakeUp", "<script language = ""javascript"">MakeUpCHK();</script>")

        SchoolIncentiveCheckSent.Text = IIf(dr("SchoolIncentiveCheckSentDT") Is DBNull.Value, "", dr("SchoolIncentiveCheckSentDT"))
        SCIncentiveCheckSent.Text = IIf(dr("SCIncentiveCheckSentDT") Is DBNull.Value, "", dr("SCIncentiveCheckSentDT"))
        dbgrade_FedExNumber1.Text = IIf(dr("FedExNumber1") Is DBNull.Value, "", dr("FedExNumber1"))
        dbgrade_FedExNumber2.Text = IIf(dr("FedExNumber2") Is DBNull.Value, "", dr("FedExNumber2"))
        dbgrade_UPSNumber1.Text = IIf(dr("UPSNumber1") Is DBNull.Value, "", dr("UPSNumber1"))
        dbgrade_UPSNumber2.Text = IIf(dr("UPSNumber2") Is DBNull.Value, "", dr("UPSNumber2"))

    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click, ButtonSave2.Click

        'Save changes
        TimssBll.SaveSchoolEditChanges(Me.Form.Controls, Me.Frame_N_, Me.GradelId, Me.Grade)

        'IF Disposition is saved to anything other than Pending, Initial Contact Pending, or Cooperating,
        'OR If a school changes from "Cooperating" to any other status,
        'AND Disposition code has changed,
        'THEN send email to timss@westat.com or ICILS@westat.com
        If ((hiddenOriginalDISPcode.Text.ToString() <> dbgrade_DISP.SelectedValue.ToString()) And (dbgrade_DISP.SelectedValue.ToString() <> "00" And dbgrade_DISP.SelectedValue <> "01" And dbgrade_DISP.SelectedValue <> "02")) Or
                ((hiddenOriginalDISPcode.Text.ToString() = "01") And (hiddenOriginalDISPcode.Text.ToString() <> dbgrade_DISP.SelectedValue.ToString())) Then
            If Request.Url.ToString().ToLower().Contains("mytimss.com") Or Request.Url.ToString().ToLower().Contains("myicils.com") Then
                Dim body As String = "A school has changed its disposition:<ul><li>Site: Production</li>" &
                           "<li>Study: " & ProjectName.Text.ToString() & "</li>" &
                           "<li>School ID: " & TIMSSID.Text.ToString() & "</li>" & "<li>REPSBGRP: " & ViewState("REPSBGRP").ToString() & "</li>" &
                           "<li>Grade: " & SampledGrade.Text & "</li>" & "<li>Previous Status: <b>" & hiddenOriginalDISPcode.Text.ToString() & ", " & hiddenOriginalDISPtext.Text.ToString() & "</b></li>" &
                           "<li>New Status: <b>" & dbgrade_DISP.SelectedValue.ToString() & ", " & dbgrade_DISP.SelectedItem.Text.ToString() & "</b></li></ul>"

                If ProjectName.Text.Contains("TIMSS") = True Then
                    TimssBll.SendEmail("TIMSS@westat.com", "TIMSS@westat.com;lauraegan@westat.com;ewelinajansson@westat.com", "", "", "MyTIMSS school status changed", body, 1, "TIMSS@westat.com")
                ElseIf ProjectName.Text.Contains("ICILS") = True Then
                    TimssBll.SendEmail("ICILS@westat.com", "ICILS@westat.com;lauraegan@westat.com;ewelinajansson@westat.com", "", "", "MyICILS school status changed", body, 1, "ICILS@westat.com")
                End If
            End If
        End If

        Me.BindData()
    End Sub

End Class
