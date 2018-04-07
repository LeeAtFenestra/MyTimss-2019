Imports Westat.TIMSS.HL

Partial Class ProvideSchoolInformation
    Inherits BasePagePublic


    Public Property GradeId4() As String
        Get
            If ViewState("GradeId4") = Nothing Then
                ViewState("GradeId4") = ""
            End If
            Return ViewState("GradeId4")
        End Get
        Set(ByVal value As String)
            ViewState("GradeId4") = value
        End Set
    End Property
    Public ReadOnly Property HasGradeId4() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId4())
        End Get
    End Property


    Public Property GradeId8() As String
        Get
            If ViewState("GradeId8") = Nothing Then
                ViewState("GradeId8") = ""
            End If
            Return ViewState("GradeId8")
        End Get
        Set(ByVal value As String)
            ViewState("GradeId8") = value
        End Set
    End Property
    Public ReadOnly Property HasGradeId8() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId8())
        End Get
    End Property

    Public Property GradeId12() As String
        Get
            If ViewState("GradeId12") = Nothing Then
                ViewState("GradeId12") = ""
            End If
            Return ViewState("GradeId12")
        End Get
        Set(ByVal value As String)
            ViewState("GradeId12") = value
        End Set
    End Property
    Public ReadOnly Property HasGradeId12() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId12())
        End Get
    End Property

    'Public ReadOnly Property HasFrame_N_() As Boolean
    '    Get
    '        If String.IsNullOrEmpty(TimssBll.Frame_N_()) Then

    '        End If
    '        Return Not String.IsNullOrEmpty(TimssBll.Frame_N_())
    '    End Get
    'End Property

    Public Property PrincipalId() As String
        Get
            If ViewState("PrincipalId") = Nothing Then
                ViewState("PrincipalId") = ""
            End If
            Return ViewState("PrincipalId")
        End Get
        Set(ByVal value As String)
            ViewState("PrincipalId") = value
        End Set
    End Property
    Public ReadOnly Property HasPrincipalId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(PrincipalId())
        End Get
    End Property

    Public Property CoordinatorId() As String
        Get
            If ViewState("CoordinatorId") = Nothing Then
                ViewState("CoordinatorId") = ""
            End If
            Return ViewState("CoordinatorId")
        End Get
        Set(ByVal value As String)
            ViewState("CoordinatorId") = value
        End Set
    End Property
    Public ReadOnly Property HasCoordinatorId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(CoordinatorId())
        End Get
    End Property

    'Public ReadOnly Property Frame_N_() As String
    '    Get
    '        If String.IsNullOrEmpty(Session("Frame_N_")) Then
    '            Session("Frame_N_") = "084627"
    '        End If
    '        Return Server.HtmlEncode(Session("Frame_N_"))
    '    End Get
    'End Property



    'Public ReadOnly Property HasGradeId() As Boolean
    '    Get
    '        Return Not String.IsNullOrEmpty(GradelId())
    '    End Get
    'End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

            'Dim dt As DataTable = TimssBll.GetSchoolListDataTable(args)
            'Me.SelectedSchool.DataSource = TimssBll.GetSchoolListDataTable(args)

            'If Westat.TIMSS.BLL.TIMSSBLL.CanChangeSchools() Then
            '    If Not String.IsNullOrEmpty(Request.QueryString("s")) Then
            '        TimssBll.Frame_N_ = Server.HtmlEncode(Request.QueryString("s"))
            '    End If
            '    Me.SelectedSchool.Visible = True
            '    Me.SelectedSchool.DataSource = TimssBll.GetDistinctSchoolListDataTable()
            '    Me.SelectedSchool.DataBind()
            '    'Response.Write(Me.SelectedSchool.Items.Count & " Schools...")
            '    If Not TimssBll.HasFrame_N_ Then
            '        TimssBll.Frame_N_ = Me.SelectedSchool.SelectedValue
            '    Else
            '        Me.SelectedSchool.SelectedValue = TimssBll.Frame_N_
            '    End If
            'Else
            '    Me.SelectedSchool.Visible = False
            'End If

            BindData()

            CheckRequiredFields()
        End If
    End Sub

    Private Sub BindData()

        If Not TimssBll.HasFrame_N_() Then
            Return
        End If

        Dim personel As DataTable = TimssBll.GetSchoolPersonnelDataTable(TimssBll.Frame_N_)

        Dim dr As DataRow = TimssBll.GetPSISchoolDetailsDataRow(TimssBll.Frame_N_)
        Dim gradecount As Integer = 0

        Me.GradeId4 = IIf(dr("id4") Is DBNull.Value, "", dr("id4"))
        Me.GradeId8 = IIf(dr("id8") Is DBNull.Value, "", dr("id8"))
        Me.GradeId12 = IIf(dr("id12") Is DBNull.Value, "", dr("id12"))
        'MyNAEPREGID
        ' Me.LabelTIMSSRegistrationID.Text = IIf(dr("MyNAEPREGID") Is DBNull.Value, "", dr("MyNAEPREGID"))
        Me.LabelSubmittedBy.Text = IIf(dr("PSISubmittedBy") Is DBNull.Value, "", dr("PSISubmittedBy"))

        'Dim grade As Integer = 8 'dr("SMPGRD")
        Dim leaid As String = dr("leaid")

        Me.PanelG4.Visible = Me.HasGradeId4
        Me.PanelG8.Visible = Me.HasGradeId8
        Me.PanelG12.Visible = Me.HasGradeId12

        Me.PanelICTCoordinatorName.Visible = Me.TimssBll.isICILS()

        If Me.HasGradeId4 Then
            gradecount = gradecount + 1
            dbG4_DateSchoolStartsSpringBreak.Text = IIf(dr("DateSchoolStartsSpringBreak_4") Is DBNull.Value, "", dr("DateSchoolStartsSpringBreak_4"))
            'dbG4_DateSchoolReturnsFromWinterBreak.Text = IIf(dr("DateSchoolReturnsFromWinterBreak_4") Is DBNull.Value, "", dr("DateSchoolReturnsFromWinterBreak_4"))
            dbG4_DateSchoolReturnsFromSpringBreak.Text = IIf(dr("DateSchoolReturnsFromSpringBreak_4") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak_4"))
            dbG4_LastDayofSchool.Text = IIf(dr("LastDayofSchool_4") Is DBNull.Value, "", dr("LastDayofSchool_4"))
            dbG4_EnrollmentAtGrade.Text = IIf(dr("EnrollmentAtGrade_4") Is DBNull.Value, "", dr("EnrollmentAtGrade_4"))
            dbG4_NumberOfClasses.Text = IIf(dr("NumberOfClasses_4") Is DBNull.Value, "", dr("NumberOfClasses_4"))

            LabelDateSchoolStartsSpringBreak4.Text = IIf(dr("DateSchoolStartsSpringBreak_4") Is DBNull.Value, "", dr("DateSchoolStartsSpringBreak_4"))
            LabelDateSchoolReturnsFromSpringBreak4.Text = IIf(dr("DateSchoolReturnsFromSpringBreak_4") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak_4"))
            LabelLastDayofSchool4.Text = IIf(dr("LastDayofSchool_4") Is DBNull.Value, "", dr("LastDayofSchool_4"))
            LabelEnrollmentAtGrade4.Text = IIf(dr("EnrollmentAtGrade_4") Is DBNull.Value, "", dr("EnrollmentAtGrade_4"))
            LabelNumberOfClasses4.Text = IIf(dr("NumberOfClasses_4") Is DBNull.Value, "", dr("NumberOfClasses_4"))

            'edit by Wei IC-10 4/20/2017 hide spring break fields for ICILS schools
            'If Me.TimssBll.isICILS() Then
            '    placeholdergread4springbreak.Visible = False
            'Else
            '    placeholdergread4springbreak.Visible = True
            'End If



        End If

        If Me.HasGradeId8 Then

            LabelNumberOfMathClasses8Static.Text = IIf(TimssBll.isICILS(), "Number of teachers who teach at least one 8<sup>th</sup>-grade class or 8<sup>th</sup>-grade student in an 8<sup>th</sup>-grade class<br>(Do not include teachers who teach 8<sup>th</sup>-graders in a class for 7<sup>th</sup> or 9<sup>th</sup>-grade.<br>Do not include non-staff teachers who teach (non-compulsory) subjects that are not part of the curriculum. If electives (such as Band, Visual Arts, or Gym) are a part of the 8<sup>th</sup>-grade curriculum, please include teachers who teach these classes too.)", "Number of 8<sup>th</sup>-grade math classes")

            gradecount = gradecount + 1
            dbG8_DateSchoolStartsSpringBreak.Text = IIf(dr("DateSchoolStartsSpringBreak_8") Is DBNull.Value, "", dr("DateSchoolStartsSpringBreak_8"))
            'dbG8_DateSchoolReturnsFromWinterBreak.Text = IIf(dr("DateSchoolReturnsFromWinterBreak_8") Is DBNull.Value, "", dr("DateSchoolReturnsFromWinterBreak_8"))
            dbG8_DateSchoolReturnsFromSpringBreak.Text = IIf(dr("DateSchoolReturnsFromSpringBreak_8") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak_8"))
            dbG8_LastDayofSchool.Text = IIf(dr("LastDayofSchool_8") Is DBNull.Value, "", dr("LastDayofSchool_8"))
            dbG8_EnrollmentAtGrade.Text = IIf(dr("EnrollmentAtGrade_8") Is DBNull.Value, "", dr("EnrollmentAtGrade_8"))
            dbG8_NumberOfMathClasses.Text = IIf(dr("NumberOfMathClasses_8") Is DBNull.Value, "", dr("NumberOfMathClasses_8"))

            LabelDateSchoolStartsSpringBreak8.Text = IIf(dr("DateSchoolStartsSpringBreak_8") Is DBNull.Value, "", dr("DateSchoolStartsSpringBreak_8"))
            LabelDateSchoolReturnsFromSpringBreak8.Text = IIf(dr("DateSchoolReturnsFromSpringBreak_8") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak_8"))
            LabelLastDayofSchool8.Text = IIf(dr("LastDayofSchool_8") Is DBNull.Value, "", dr("LastDayofSchool_8"))
            LabelEnrollmentAtGrade8.Text = IIf(dr("EnrollmentAtGrade_8") Is DBNull.Value, "", dr("EnrollmentAtGrade_8"))
            LabelNumberOfMathClasses8.Text = IIf(dr("NumberOfMathClasses_8") Is DBNull.Value, "", dr("NumberOfMathClasses_8"))

            'edit by Wei IC-10 4/20/2017 hide spring break fields for ICILS schools
            'If Me.TimssBll.isICILS() Then
            '    placeholdergrade8springbreak.Visible = False
            'Else
            '    placeholdergrade8springbreak.Visible = True
            'End If

        End If

        If Me.HasGradeId12 Then
            gradecount = gradecount + 1
            dbG12_DateSchoolStartsSpringBreak.Text = IIf(dr("DateSchoolStartsSpringBreak_12") Is DBNull.Value, "", dr("DateSchoolStartsSpringBreak_12"))
            'dbG12_DateSchoolReturnsFromWinterBreak.Text = IIf(dr("DateSchoolReturnsFromWinterBreak_12") Is DBNull.Value, "", dr("DateSchoolReturnsFromWinterBreak_12"))
            dbG12_DateSchoolReturnsFromSpringBreak.Text = IIf(dr("DateSchoolReturnsFromSpringBreak_12") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak_12"))
            dbG12_LastDayofSchool.Text = IIf(dr("LastDayofSchool_12") Is DBNull.Value, "", dr("LastDayofSchool_12"))
            dbG12_EnrollmentAtGrade.Text = IIf(dr("EnrollmentAtGrade_12") Is DBNull.Value, "", dr("EnrollmentAtGrade_12"))

            LabelDateSchoolStartsSpringBreak12.Text = IIf(dr("DateSchoolStartsSpringBreak_12") Is DBNull.Value, "", dr("DateSchoolStartsSpringBreak_12"))
            LabelDateSchoolReturnsFromSpringBreak12.Text = IIf(dr("DateSchoolReturnsFromSpringBreak_12") Is DBNull.Value, "", dr("DateSchoolReturnsFromSpringBreak_12"))
            LabelLastDayofSchool12.Text = IIf(dr("LastDayofSchool_12") Is DBNull.Value, "", dr("LastDayofSchool_12"))
            LabelEnrollmentAtGrade12.Text = IIf(dr("EnrollmentAtGrade_12") Is DBNull.Value, "", dr("EnrollmentAtGrade_12"))

            'edit by Wei IC-10 4/20/2017 hide spring break fields for ICILS schools
            'If Me.TimssBll.isICILS() Then
            '    placeholdergrade12springbreak.Visible = False
            'Else
            '    placeholdergrade12springbreak.Visible = True
            'End If

        End If

        trG4Header.Visible = gradecount > 1
        trG8Header.Visible = gradecount > 1
        trG12Header.Visible = gradecount > 1

        'Me.LabelEnrollmentAtGrade.Text = grade


        'If grade = 4 Then
        '    trNumberOfClasses.Visible = True
        '    LabelNumberOfClasses.Text = "Number of classes"
        'ElseIf grade = 8 Then
        '    trNumberOfClasses.Visible = True
        '    LabelNumberOfClasses.Text = "Number of math classes"
        'ElseIf grade = 12 Then
        '    trNumberOfClasses.Visible = False
        'End If

        Me.PrincipalId = IIf(dr("principalID") Is DBNull.Value, "", dr("principalID"))
        Me.CoordinatorId = IIf(dr("coordinatorid") Is DBNull.Value, "", dr("coordinatorid"))

        'Me.LabelPrincipalId.Text = " - " & Me.PrincipalId
        'Me.LabelSchoolCoordinatorID.Text = " - " & Me.CoordinatorId

        Me.LabelSchoolName1.Text = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))
        Me.LabelSchoolName2.Text = Me.LabelSchoolName1.Text
        Me.db_s_name.Text = Me.LabelSchoolName1.Text

        db_s_addr1.Text = IIf(dr("S_Addr1") Is DBNull.Value, "", dr("S_Addr1"))
        Me.LabelSchoolAddress1.Text = db_s_addr1.Text

        db_s_addr2.Text = IIf(dr("S_Addr2") Is DBNull.Value, "", dr("S_Addr2"))
        Me.LabelSchoolAddress2.Text = db_s_addr2.Text

        db_s_city.Text = IIf(dr("S_City") Is DBNull.Value, "", dr("S_City"))
        Me.LabelSchoolCity.Text = db_s_city.Text

        db_s_state.Text = IIf(dr("S_State") Is DBNull.Value, "", dr("S_State"))
        Me.LabelSchoolState.Text = db_s_state.Text
        Me.LabelState.Text = db_s_state.Text

        db_s_zip.Text = IIf(dr("S_Zip") Is DBNull.Value, "", dr("S_Zip"))
        Me.LabelSchoolZipCode.Text = db_s_zip.Text

        db_ICTCoordinatorName.Text = IIf(dr("ICTCoordinatorName") Is DBNull.Value, "", dr("ICTCoordinatorName"))
        Me.LabelICTCoordinatorName.Text = db_ICTCoordinatorName.Text

        db_ICTCoordinatorEmail.Text = IIf(dr("ICTCoordinatorEmail") Is DBNull.Value, "", dr("ICTCoordinatorEmail"))
        Me.LabelICTCoordinatorEmail.Text = db_ICTCoordinatorEmail.Text


        Me.LabelDistrict.Text = IIf(dr("d_name") Is DBNull.Value, "", dr("d_name"))

        Dim principaldr As DataRow = Nothing
        Dim coordinatordr As DataRow = Nothing

        For Each perdr As DataRow In personel.Rows
            If IIf(perdr("pid") Is DBNull.Value, -100, perdr("pid")).ToString() = Me.PrincipalId Then
                principaldr = perdr
            End If

            If IIf(perdr("pid") Is DBNull.Value, -100, perdr("pid")).ToString() = Me.CoordinatorId Then
                coordinatordr = perdr
            End If
        Next

        dbprincipal_frame_n_.Value = TimssBll.Frame_N_
        dbcoordinator_frame_n_.Value = TimssBll.Frame_N_

        If Not principaldr Is Nothing Then

            LabelSchoolPrincipalPrefix.Text = IIf(principaldr("prefix") Is DBNull.Value, "", principaldr("prefix"))
            dbprincipal_prefix.Text = LabelSchoolPrincipalPrefix.Text

            LabelSchoolPrincipalFname.Text = IIf(principaldr("fname") Is DBNull.Value, "", principaldr("fname"))
            dbprincipal_fname.Text = LabelSchoolPrincipalFname.Text

            LabelSchoolPrincipalLname.Text = IIf(principaldr("lname") Is DBNull.Value, "", principaldr("lname"))
            dbprincipal_lname.Text = LabelSchoolPrincipalLname.Text

            LabelSchoolPrincipalSuffix.Text = IIf(principaldr("suffix") Is DBNull.Value, "", principaldr("suffix"))
            dbprincipal_suffix.Text = LabelSchoolPrincipalSuffix.Text

            LabelPrincipalTelephoneNumber.Text = IIf(principaldr("phone") Is DBNull.Value, "", principaldr("phone"))
            dbprincipal_phone.Text = LabelPrincipalTelephoneNumber.Text

            LabelPrincipalTelephoneNumberExt.Text = IIf(principaldr("phoneext") Is DBNull.Value, "", principaldr("phoneext"))
            dbprincipal_phoneext.Text = LabelPrincipalTelephoneNumberExt.Text

            LabelPrincipalEmail.Text = IIf(principaldr("email") Is DBNull.Value, "", principaldr("email"))
            dbprincipal_email.Text = LabelPrincipalEmail.Text

            dbprincipal_title.Value = IIf(principaldr("title") Is DBNull.Value, "", principaldr("title"))
            dbprincipal_fax.Value = IIf(principaldr("fax") Is DBNull.Value, "", principaldr("fax"))

        Else
            LabelSchoolPrincipalPrefix.Text = ""
            LabelSchoolPrincipalFname.Text = ""
            LabelSchoolPrincipalLname.Text = ""
            LabelSchoolPrincipalSuffix.Text = ""
            LabelPrincipalTelephoneNumber.Text = ""
            LabelPrincipalTelephoneNumberExt.Text = ""
            LabelPrincipalEmail.Text = ""
            dbprincipal_prefix.Text = ""
            dbprincipal_fname.Text = ""
            dbprincipal_lname.Text = ""
            dbprincipal_suffix.Text = ""
            dbprincipal_phone.Text = ""
            dbprincipal_phoneext.Text = ""
            dbprincipal_email.Text = ""
            dbprincipal_title.Value = "Principal"
            dbprincipal_fax.Value = ""
        End If

        If Not coordinatordr Is Nothing Then

            LabelSchoolCoordinatorPrefix.Text = IIf(coordinatordr("prefix") Is DBNull.Value, "", coordinatordr("prefix"))
            dbcoordinator_prefix.Text = LabelSchoolCoordinatorPrefix.Text

            LabelSchoolCoordinatorFname.Text = IIf(coordinatordr("fname") Is DBNull.Value, "", coordinatordr("fname"))
            dbcoordinator_fname.Text = LabelSchoolCoordinatorFname.Text

            LabelSchoolCoordinatorLname.Text = IIf(coordinatordr("lname") Is DBNull.Value, "", coordinatordr("lname"))
            dbcoordinator_lname.Text = LabelSchoolCoordinatorLname.Text

            LabelSchoolCoordinatorSuffix.Text = IIf(coordinatordr("suffix") Is DBNull.Value, "", coordinatordr("suffix"))
            dbcoordinator_suffix.Text = LabelSchoolCoordinatorSuffix.Text

            LabelCoordinatorTelephoneNumber.Text = IIf(coordinatordr("phone") Is DBNull.Value, "", coordinatordr("phone"))
            dbcoordinator_phone.Text = LabelCoordinatorTelephoneNumber.Text

            LabelCoordinatorTelephoneNumberExt.Text = IIf(coordinatordr("phoneext") Is DBNull.Value, "", coordinatordr("phoneext"))
            dbcoordinator_phoneext.Text = LabelCoordinatorTelephoneNumberExt.Text

            LabelCoordinatorEmail.Text = IIf(coordinatordr("email") Is DBNull.Value, "", coordinatordr("email"))
            dbcoordinator_email.Text = LabelCoordinatorEmail.Text

            LabelSchoolCoordinatorTitle.Text = IIf(coordinatordr("title") Is DBNull.Value, "", coordinatordr("title"))
            dbcoordinator_title.Text = LabelSchoolCoordinatorTitle.Text

            LabelCoordinatorFaxNumber.Text = IIf(coordinatordr("fax") Is DBNull.Value, "", coordinatordr("fax"))
            dbcoordinator_fax.Text = LabelCoordinatorFaxNumber.Text

        Else
            LabelSchoolCoordinatorPrefix.Text = ""
            LabelSchoolCoordinatorFname.Text = ""
            LabelSchoolCoordinatorLname.Text = ""
            LabelSchoolCoordinatorSuffix.Text = ""
            LabelCoordinatorTelephoneNumber.Text = ""
            LabelCoordinatorTelephoneNumberExt.Text = ""
            LabelCoordinatorEmail.Text = ""
            LabelCoordinatorFaxNumber.Text = ""
            dbcoordinator_prefix.Text = ""
            dbcoordinator_fname.Text = ""
            dbcoordinator_lname.Text = ""
            dbcoordinator_suffix.Text = ""
            dbcoordinator_phone.Text = ""
            dbcoordinator_phoneext.Text = ""
            dbcoordinator_email.Text = ""
            dbcoordinator_fax.Text = ""
            dbcoordinator_title.Text = ""
        End If
    End Sub

    Sub SelectedSchool_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddl As DropDownList = sender
        TimssBll.Frame_N_ = ddl.SelectedValue
        Me.BindData()
    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click

        ResetTextBoxToOriginalValueIfEmpty(Me.db_s_name, Me.LabelSchoolName1)
        ResetTextBoxToOriginalValueIfEmpty(Me.db_s_addr1, Me.LabelSchoolAddress1)
        ResetTextBoxToOriginalValueIfEmpty(Me.db_s_addr2, Me.LabelSchoolAddress2)
        ResetTextBoxToOriginalValueIfEmpty(Me.db_s_city, Me.LabelSchoolCity)
        ResetTextBoxToOriginalValueIfEmpty(Me.db_s_state, Me.LabelSchoolState)
        ResetTextBoxToOriginalValueIfEmpty(Me.db_s_zip, Me.LabelSchoolZipCode)

        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_prefix, Me.LabelSchoolPrincipalPrefix)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_fname, Me.LabelSchoolPrincipalFname)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_lname, Me.LabelSchoolPrincipalLname)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_suffix, Me.LabelSchoolPrincipalSuffix)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_phone, Me.LabelPrincipalTelephoneNumber)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_phoneext, Me.LabelPrincipalTelephoneNumberExt)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbprincipal_email, Me.LabelPrincipalEmail)

        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_prefix, Me.LabelSchoolCoordinatorPrefix)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_fname, Me.LabelSchoolCoordinatorFname)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_lname, Me.LabelSchoolCoordinatorLname)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_suffix, Me.LabelSchoolCoordinatorSuffix)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_phone, Me.LabelCoordinatorTelephoneNumber)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_phoneext, Me.LabelCoordinatorTelephoneNumberExt)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_email, Me.LabelCoordinatorEmail)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_title, Me.LabelSchoolCoordinatorTitle)
        ResetTextBoxToOriginalValueIfEmpty(Me.dbcoordinator_fax, Me.LabelCoordinatorFaxNumber)

        Dim updateprincipal As Boolean = Not String.IsNullOrEmpty(Me.dbprincipal_prefix.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbprincipal_fname.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbprincipal_lname.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbprincipal_suffix.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbprincipal_phone.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbprincipal_phoneext.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbprincipal_email.Text)

        Dim updatecoordinator As Boolean = Not String.IsNullOrEmpty(Me.dbcoordinator_prefix.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_fname.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_lname.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_suffix.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_phone.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_phoneext.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_email.Text) _
                                         Or Not String.IsNullOrEmpty(Me.dbcoordinator_fax.Text)
        'Or Not String.IsNullOrEmpty(Me.dbcoordinator_title.Text) _

        'Response.Write("updateprincipal= " & updateprincipal & " - updatecoordinator = " & updatecoordinator)

        TimssBll.SaveProvideSchoolInformationEditChanges(Me.Form.Controls, TimssBll.Frame_N_, Me.PrincipalId, Me.dbprincipal_email.Text, Me.CoordinatorId, Me.dbcoordinator_email.Text, Me.GradeId4, Me.GradeId8, Me.GradeId12, updateprincipal, updatecoordinator)

        Me.BindData()

        CheckRequiredFields()
    End Sub

    Sub CheckRequiredFields()
        Dim count As Int16 = 0

        If Trim(db_s_name.Text) = "" Then
            req1.Visible = True
            count = count + 1
        Else
            req1.Visible = False
        End If

        If Trim(db_s_addr1.Text) = "" Then
            req2.Visible = True
            count = count + 1
        Else
            req2.Visible = False
        End If

        If Trim(db_s_city.Text) = "" Then
            req3.Visible = True
            count = count + 1
        Else
            req3.Visible = False
        End If

        If Trim(db_s_state.Text) = "" Then
            req4.Visible = True
            count = count + 1
        Else
            req4.Visible = False
        End If

        If Trim(db_s_zip.Text) = "" Then
            req5.Visible = True
            count = count + 1
        Else
            req5.Visible = False
        End If

        If Me.TimssBll.isICILS() Then
            If Trim(db_ICTCoordinatorName.Text) = "" Then
                req6.Visible = True
                count = count + 1
            Else
                req6.Visible = False
            End If

            If Trim(db_ICTCoordinatorEmail.Text) = "" Then
                req20.Visible = True
                count = count + 1
            Else
                req20.Visible = False
            End If
        End If


        If Trim(dbprincipal_fname.Text) = "" Then
            req7.Visible = True
            count = count + 1
        Else
            req7.Visible = False
        End If

        If Trim(dbprincipal_lname.Text) = "" Then
            req8.Visible = True
            count = count + 1
        Else
            req8.Visible = False
        End If

        If Trim(dbprincipal_phone.Text) = "" Then
            req9.Visible = True
            count = count + 1
        Else
            req9.Visible = False
        End If

        If Trim(dbprincipal_email.Text) = "" Then
            req10.Visible = True
            count = count + 1
        Else
            req10.Visible = False
        End If

        If Trim(dbcoordinator_fname.Text) = "" Then
            req11.Visible = True
            count = count + 1
        Else
            req11.Visible = False
        End If

        If Trim(dbcoordinator_lname.Text) = "" Then
            req12.Visible = True
            count = count + 1
        Else
            req12.Visible = False
        End If

        If Trim(dbcoordinator_phone.Text) = "" Then
            req13.Visible = True
            count = count + 1
        Else
            req13.Visible = False
        End If

        If Trim(dbcoordinator_email.Text) = "" Then
            req14.Visible = True
            count = count + 1
        Else
            req14.Visible = False
        End If

        If Me.HasGradeId4 Then
            If Trim(dbG4_EnrollmentAtGrade.Text) = "" Then
                req15.Visible = True
                count = count + 1
            Else
                req15.Visible = False
            End If

            If Trim(dbG4_NumberOfClasses.Text) = "" Then
                req16.Visible = True
                count = count + 1
            Else
                req16.Visible = False
            End If
        End If

        If Me.HasGradeId8 Then
            If Trim(dbG8_EnrollmentAtGrade.Text) = "" Then
                req17.Visible = True
                count = count + 1
            Else
                req17.Visible = False
            End If

            If Trim(dbG8_NumberOfMathClasses.Text) = "" Then
                req18.Visible = True
                count = count + 1
            Else
                req18.Visible = False
            End If
        End If

        If Me.HasGradeId12 Then
            If Trim(dbG12_EnrollmentAtGrade.Text) = "" Then
                req19.Visible = True
                count = count + 1
            Else
                req19.Visible = False
            End If
        End If

        If count = 0 Then
            LabelSaveComplete.Visible = True
            LabelMissingRequiredFields.Visible = False
        Else
            LabelSaveComplete.Visible = False
            LabelMissingRequiredFields.Visible = True
            numCount.Text = count.ToString()

            If count = 1 Then
                lblFields.Visible = False
            Else
                lblFields.Visible = True
            End If
        End If
    End Sub

    Private Sub ResetTextBoxToOriginalValueIfEmpty(tb As TextBox, lbl As Label)
        If String.IsNullOrEmpty(tb.Text) And String.IsNullOrEmpty(lbl.Text) = False Then
            tb.Text = lbl.Text
        End If
    End Sub

End Class
