Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL

Partial Class Register
    Inherits BasePagePublic

    Public Property FRAME_N_() As String
        Get
            If ViewState("FRAME_N_") = Nothing Then
                ViewState("FRAME_N_") = ""
            End If
            Return ViewState("FRAME_N_")
        End Get
        Set(ByVal value As String)
            ViewState("FRAME_N_") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Request.Url.ToString().ToLower().Contains("mytimss.com") Or Request.Url.ToString().ToLower().Contains("mytimssdev.westat.com") Or Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") Or Request.Url.ToString().ToLower().Contains("mytimssdemo.wesdemo.com") Then
                lblSite1.Text = "MyTIMSS"
                lblSite2.Text = "MyTIMSS"
                LabelFailureText.Text = "*Invalid MyTIMSS Registration ID!"
                ButtonContinueToMyTIMSS.Text = "Continue To MyTIMSS"
                lblLabelRegID.Text = "MyTIMSS"
            ElseIf Request.Url.ToString().ToLower().Contains("myicils.com") Then
                lblSite1.Text = "MyICILS"
                lblSite2.Text = "MyICILS"
                LabelFailureText.Text = "*Invalid MyICILS Registration ID!"
                ButtonContinueToMyTIMSS.Text = "Continue To MyICILS"
                lblLabelRegID.Text = "MyICILS"
            End If

            'Dim FABMembershipProvider As Westat.FAB.MembershipProvider.CustomMembershipProvider
            'FABMembershipProvider = CType(Membership.Provider, Westat.FAB.MembershipProvider.CustomMembershipProvider)
            'Response.Write("FABMembershipProvider.DaysPasswordIsValid() = " & FABMembershipProvider.DaysPasswordIsValid())
            'Me.DropDownListSchoolList.DataSource = TimssBll.GetRegistrationIDNameValuePairList()
            'Me.DropDownListSchoolList.DataBind()
            'LabelCount.Text = Me.DropDownListSchoolList.Items.Count & " items in list..."
        End If
    End Sub

    'Private Sub Wizard1_NextButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles WizardRegistration.NextButtonClick
    '    If WizardRegistration.ActiveStepIndex = 0 Then
    '        Dim dr As DataRow = TimssBll.GetMyTIMSSRegistrationDataRow(Me.TextBoxMyTIMSSRegistrationID.Text)

    '        If dr Is Nothing Then
    '            LabelFailureText.Visible = True
    '            e.Cancel = True
    '        Else
    '            Me.LabelSchoolName.Text = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))
    '            Me.LabelSchoolName2.Text = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))
    '            Me.LabelDistrict.Text = IIf(dr("d_name") Is DBNull.Value, "", dr("d_name"))
    '            Me.LabelMyTIMSSRegistrationID.Text = IIf(dr("MyNAEPREGID") Is DBNull.Value, "", dr("MyNAEPREGID"))
    '        End If

    '    End If
    'End Sub


    Protected Sub ButtonValidateID_Click(sender As Object, e As System.EventArgs) Handles ButtonValidateID.Click
        Dim dr As DataRow = TimssBll.GetMyTIMSSRegistrationDataRow(Me.TextBoxMyTIMSSRegistrationID.Text)

        If dr Is Nothing Then
            LabelFailureText.Visible = True
            PanelMyTIMSSRegistrationID.Visible = True
            PanelRegistrationInformation.Visible = False
        ElseIf dr("study_name") = "ICILS" And (Request.Url.ToString().ToLower().Contains("www.mytimss.com") Or Request.Url.ToString().ToLower().Contains("mytimssdev.westat.com") Or Request.Url.ToString().ToLower().Contains("mytimssdemo.wesdemo.com") Or Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com")) Then
            'ICILS cannot register using TIMSS
            LabelFailureText.Visible = True
            LabelFailureText.Text = "*Please register using www.myicils.com"
            PanelMyTIMSSRegistrationID.Visible = True
            PanelRegistrationInformation.Visible = False
        ElseIf dr("study_name") = "TIMSS" And Request.Url.ToString().ToLower().Contains("www.myicils.com") Then
            'TIMSS cannot register using ICILS
            LabelFailureText.Visible = True
            LabelFailureText.Text = "*Please register using www.mytimss.com"
            PanelMyTIMSSRegistrationID.Visible = True
            PanelRegistrationInformation.Visible = False
        Else
            Me.labelSchoolState.Text = IIf(dr("s_state") Is DBNull.Value, "", dr("s_state"))
            Me.LabelSchoolName.Text = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))
            Me.LabelSchoolName2.Text = Me.LabelSchoolName.Text
            Me.LabelDistrict.Text = IIf(dr("d_name") Is DBNull.Value, "", dr("d_name"))
            Me.LabelMyTIMSSRegistrationID.Text = IIf(dr("MyNAEPREGID") Is DBNull.Value, "", dr("MyNAEPREGID"))
            Me.FRAME_N_ = IIf(dr("FRAME_N_") Is DBNull.Value, "", dr("FRAME_N_"))
            PanelMyTIMSSRegistrationID.Visible = False
            PanelRegistrationInformation.Visible = True
        End If
    End Sub

    Protected Sub ButtonRegister_Click(sender As Object, e As System.EventArgs) Handles ButtonRegister.Click

        Try

            Dim lastname As String = Me.TextBoxLastName.Text.Trim()
            Dim firstname As String = Me.TextBoxFirstName.Text.Trim()
            Dim firstletter As String = firstname.Replace(" ", "").Substring(0, 1).ToLower()

            Dim uid As String = (lastname.Replace(" ", "") & firstletter).ToLower()
            Dim cnt As Integer = 1

            While Membership.FindUsersByName(uid).Count > 0
                cnt = cnt + 1
                uid = (lastname.Replace(" ", "") & firstletter & cnt).ToLower()
            End While

            Dim u As MembershipUser = Membership.CreateUser(uid, Me.TextBoxPassword.Text, Me.TextBoxEmail.Text)
            u.IsApproved = True
            Membership.UpdateUser(u)


            Roles.AddUserToRole(u.UserName, "MyTIMSSUser")
            Dim prof As ProfileCommon = ProfileCommon.GetUserProfile(u.UserName)
            prof.FirstName = firstname
            prof.LastName = lastname
            prof.Telephone = Me.TextBoxTelephone.Text.Trim()
            prof.TelephoneExtension = Me.TextBoxExtension.Text.Trim()
            prof.RegistrationId = Me.TextBoxMyTIMSSRegistrationID.Text.Trim()
            prof.Frame_N_ = Me.FRAME_N_

            prof.Save()


            Dim NameValuePairList As New List(Of NameValuePair)
            NameValuePairList.Add(New NameValuePair("PREFIX", ""))
            NameValuePairList.Add(New NameValuePair("FNAME", firstname))
            NameValuePairList.Add(New NameValuePair("FRAME_N_", Me.FRAME_N_))
            NameValuePairList.Add(New NameValuePair("LNAME", lastname))
            If CheckBoxSchoolCoordinator.Checked Then
                NameValuePairList.Add(New NameValuePair("TITLE", ""))
            Else
                NameValuePairList.Add(New NameValuePair("TITLE", ""))
            End If
            NameValuePairList.Add(New NameValuePair("EMAIL", Me.TextBoxEmail.Text))
            NameValuePairList.Add(New NameValuePair("FAX", ""))
            NameValuePairList.Add(New NameValuePair("PHONE", Me.TextBoxTelephone.Text))
            NameValuePairList.Add(New NameValuePair("PHONEEXT", Me.TextBoxExtension.Text))
            NameValuePairList.Add(New NameValuePair("SUFFIX", ""))

            TimssBll.SaveSchoolPersonnelEditChangesDuringRegistration(NameValuePairList, CheckBoxSchoolCoordinator.Checked, Me.FRAME_N_, Me.TextBoxEmail.Text)

            LabelUsername.Text = u.UserName
            PanelRegistrationInformation.Visible = False
            PanelRegistrationComplete.Visible = True
            ButtonContinueToMyTIMSS.CommandArgument = u.UserName

            'FormsAuthentication.SetAuthCookie(u.UserName, True)

            Dim FABMembershipProvider As Westat.FAB.MembershipProvider.CustomMembershipProvider
            FABMembershipProvider = CType(Membership.Provider, Westat.FAB.MembershipProvider.CustomMembershipProvider)

            'If Request.Url.ToString().Contains("www.mytimss.com") or Request.Url.ToString().Contains("www.myicils.com") Then 'Production only
            Dim ProjectName As String = TimssBll.getProjectNameFromMyNAEPREGID(Me.TextBoxMyTIMSSRegistrationID.Text.Trim())
            Dim attachmentfilepath As String
            Dim numofdays = FABMembershipProvider.DaysPasswordIsValid

            If ProjectName.ToString().Contains("ICILS") Then

                TimssBll.SendEmail_ICILSRegistrationLoginInfo(numofdays, u.UserName, "ICILS@westat.com", u.Email(), "ICILS@westat.com", "", "MyICILS Registration Information", True, "")

                If labelSchoolState.Text = "IN" And CheckBoxSchoolCoordinator.Checked = True Then
                    'No attachment for Indiana, and minor text difference
                    TimssBll.SendEmail_ICILSRegistrationIN(Me.LabelSchoolName.Text, firstname & " " & lastname, "ICILS@westat.com", u.Email(), "ICILS@westat.com", "", "Thank you for registering!", True, "")
                Else
                    If CheckBoxSchoolCoordinator.Checked = True Then
                        attachmentfilepath = Server.MapPath("\DOCUMENTS\ICILS_2018_MS_Summary_of_School_Activities.pdf")
                        TimssBll.SendEmail_ICILSRegistration(attachmentfilepath, Me.LabelSchoolName.Text, firstname & " " & lastname, "ICILS@westat.com", u.Email(), "ICILS@westat.com", "", "Thank you for registering!", True, "")
                    End If
                End If


            ElseIf ProjectName.ToString().Contains("TIMSS") Then
                    TimssBll.SendEmail_TIMSSRegistrationLoginInfo(numofdays, u.UserName, "TIMSS@westat.com", u.Email(), "TIMSS@westat.com", "", "MyTIMSS Registration Information", True, "")

                If CheckBoxSchoolCoordinator.Checked = True Then
                    Dim SMPGRD As String = TimssBll.getSMPGRDFromMyNAEPREGID(Me.TextBoxMyTIMSSRegistrationID.Text.Trim())
                    attachmentfilepath = Server.MapPath("\DOCUMENTS\TIMSS_2018_FT_Summary_of_School_Activities.pdf")

                    Select Case SMPGRD.ToString()
                        Case "4"
                            If labelSchoolState.Text = "IN" Then
                                TimssBll.SendEmail_TIMMSRegistrationG4IN(Me.LabelSchoolName.Text, firstname & " " & lastname, "TIMSS@westat.com", u.Email(), "TIMSS@westat.com", "", "Thank you for registering!", True, "")
                            Else
                                TimssBll.SendEmail_TIMMSRegistrationG4(attachmentfilepath, Me.LabelSchoolName.Text, firstname & " " & lastname, "TIMSS@westat.com", u.Email(), "TIMSS@westat.com", "", "Thank you for registering!", True, "")
                            End If

                        Case "8"
                            If labelSchoolState.Text = "IN" Then
                                TimssBll.SendEmail_TIMMSRegistrationG8IN(Me.LabelSchoolName.Text, firstname & " " & lastname, "TIMSS@westat.com", u.Email(), "TIMSS@westat.com", "", "Thank you for registering!", True, "")
                            Else
                                TimssBll.SendEmail_TIMMSRegistrationG8(attachmentfilepath, Me.LabelSchoolName.Text, firstname & " " & lastname, "TIMSS@westat.com", u.Email(), "TIMSS@westat.com", "", "Thank you for registering!", True, "")
                            End If
                    End Select

                End If
            End If
            'End if

            PanelRegistrationInformation.Visible = False
            PanelRegistrationComplete.Visible = True

        Catch ex As Exception
            LabelError.Text = ex.Message
        End Try


    End Sub

    Protected Sub ButtonContinueToMyTIMSS_Click(sender As Object, e As CommandEventArgs) Handles ButtonContinueToMyTIMSS.Command

        FormsAuthentication.SetAuthCookie(e.CommandArgument, False)
        Response.Redirect("~/")
    End Sub

    'Sub SelectedSchool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListSchoolList.SelectedIndexChanged
    '    Dim ddl As DropDownList = sender
    '    Me.TextBoxMyTIMSSRegistrationID.Text = ddl.SelectedValue
    'End Sub
End Class
