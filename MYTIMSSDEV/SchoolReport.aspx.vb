
Partial Class SchoolReport
    Inherits BasePagePublic

    Private Sub buttonExit_Click(sender As Object, e As EventArgs) Handles buttonExit.Click
        Session.Abandon()
        FormsAuthentication.SignOut()
        Response.Redirect("~/")
    End Sub

    Private Sub buttonNext_Click(sender As Object, e As EventArgs) Handles buttonNext.Click

        Dim school As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())

        If Not school Is Nothing Then
            labelSchoolName.Text = school("SCHOOLNAME")
            labelSchoolName2.Text = school("SCHOOLNAME")
            labelSchoolName3.Text = school("SCHOOLNAME")
            labelSchoolName4.Text = school("SCHOOLNAME")
            labelSchoolName5.Text = school("SCHOOLNAME")
            labelPrincipalFirstName.Text = school("PRINCIPALFIRSTNAME")
            labelPrincipalLastName.Text = school("PRINCIPALLASTNAME")
            labelPrincipalEmail.Text = school("PRINCIPALEMAIL")
            labelSchoolAddress.Text = school("SCHOOLADDRESS1") & " " & school("SCHOOLADDRESS2")
            labelAdvanced.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)
            labelAdvanced2.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)
            labelAdvanced3.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)
            labelAdvanced4.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)
            labelAdvanced5.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)
            labelAdvanced6.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)
            labelAdvanced7.Visible = IIf(school("REPORTLINK").ToString().Contains("\Adv Reports\"), True, False)

            divText.Visible = True
            divStep1EnterConfCode.Visible = False
            divStep2AreYouPrincipal.Visible = True
            divStep3ThankYou.Visible = False
            divStep4ViewContactInformation.Visible = False
            divStep5UpdateContactInformation.Visible = False
            divStep6DoYouWantAReport.Visible = False
            divStep7ThankYou.Visible = False

            divIncorrectConfirmation.Visible = False
        Else
            divStep1EnterConfCode.Visible = True
            divIncorrectConfirmation.Visible = True
        End If

    End Sub

    Private Sub buttonStep2No_Click(sender As Object, e As EventArgs) Handles buttonStep2No.Click
        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = True
        divStep4ViewContactInformation.Visible = False
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False

        'Kills Session
        Session.Abandon()
        FormsAuthentication.SignOut()

        If Request.Url.ToString().Contains("mytimss.com") Then
            TimssBll.SendEmail_SCR_Summary(labelSchoolName.Text.ToString(), labelPrincipalFirstName.Text.ToString() & " " & labelPrincipalLastName.Text.ToString(), labelPrincipalEmail.Text.ToString(), "TIMSSreport@westat.com", "TIMSSreport@westat.com", "", "", "MyTIMSS Principal Information Incorrect", True, "")
        End If

    End Sub

    Private Sub buttonStep2Yes_Click(sender As Object, e As EventArgs) Handles buttonStep2Yes.Click
        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = True
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False
    End Sub

    Private Sub buttonBack2_Click(sender As Object, e As EventArgs) Handles buttonBack2.Click
        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = True
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = False
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False
    End Sub

    Private Sub buttonBack3_Click(sender As Object, e As EventArgs) Handles buttonBack3.Click
        divText.Visible = False
        divStep1EnterConfCode.Visible = True
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = False
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False
    End Sub

    Private Sub buttonNotCorrect_Click(sender As Object, e As EventArgs) Handles buttonNotCorrect.Click

        Dim school As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())

        textboxFirstName.Text = school("PRINCIPALFIRSTNAME")
        textboxLastName.Text = school("PRINCIPALLASTNAME")
        textboxEmail.Text = school("PRINCIPALEMAIL")

        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = False
        divStep5UpdateContactInformation.Visible = True
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False
    End Sub

    Private Sub buttonYesCorrect_Click(sender As Object, e As EventArgs) Handles buttonYesCorrect.Click
        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = False
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = True
        divStep7ThankYou.Visible = False

        Dim school As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())

        'based off Responded flag
        If school("RESPONDED") = "Y" Then
            literalReportYes.Visible = True
            literalReportYes2.Visible = True
            literalReportNo.Visible = False
            literalReportNo2.Visible = False
        ElseIf school("RESPONDED") = "N" Then
            literalReportYes.Visible = False
            literalReportYes2.Visible = False
            literalReportNo.Visible = True
            literalReportNo2.Visible = True
        End If

    End Sub

    Private Sub buttonCancel_Click(sender As Object, e As EventArgs) Handles buttonCancel.Click

        'Sets fields to blank
        textboxFirstName.Text = ""
        textboxLastName.Text = ""
        textboxEmail.Text = ""

        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = True
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False

    End Sub


    Private Sub buttonUpdate_Click(sender As Object, e As EventArgs) Handles buttonUpdate.Click

        If Trim(textboxFirstName.Text.ToString()) = "" Or Trim(textboxLastName.Text.ToString()) = "" Or Trim(textboxEmail.Text.ToString()) = "" Then
            labelEmailRequired.Visible = True
        Else
            labelEmailRequired.Visible = False

            Dim school As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())

            'Update principal information
            TimssBll.SCR_SavePrincipalInformation(school("TIMSSID"), textboxFirstName.Text.ToString(), textboxLastName.Text.ToString(), textboxEmail.Text.ToString())

            Dim school2 As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())
            labelPrincipalFirstName.Text = school2("PRINCIPALFIRSTNAME")
            labelPrincipalLastName.Text = school2("PRINCIPALLASTNAME")
            labelPrincipalEmail.Text = school2("PRINCIPALEMAIL")

            divStep1EnterConfCode.Visible = False
            divStep2AreYouPrincipal.Visible = False
            divStep3ThankYou.Visible = False
            divStep4ViewContactInformation.Visible = True
            divStep5UpdateContactInformation.Visible = False
            divStep6DoYouWantAReport.Visible = False
            divStep7ThankYou.Visible = False

            'based off Responded flag
            If school2("RESPONDED") = "Y" Then
                literalReportYes.Visible = True
                literalReportYes2.Visible = True
                literalReportNo.Visible = False
                literalReportNo2.Visible = False
            ElseIf school2("RESPONDED") = "N" Then
                literalReportYes.Visible = False
                literalReportYes2.Visible = False
                literalReportNo.Visible = True
                literalReportNo2.Visible = True
            End If
        End If

    End Sub

    Private Sub buttonSwitchRespondent_Click(sender As Object, e As EventArgs) Handles buttonSwitchRespondent.Click, buttonSwitchRespondent2.Click

        Dim responded As String

        If literalReportNo.Visible Then
            responded = "Y"
            literalReportYes.Visible = True
            literalReportNo.Visible = False
            literalReportYes2.Visible = True
            literalReportNo2.Visible = False
        Else
            responded = "N"
            literalReportYes.Visible = False
            literalReportNo.Visible = True
            literalReportYes2.Visible = False
            literalReportNo2.Visible = True
        End If

        Dim school As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())

        TimssBll.SCR_UpdateRespondedFlag(school("TIMSSID"), responded)

    End Sub

    Private Sub buttonBack_Click(sender As Object, e As EventArgs) Handles buttonBack.Click
        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = True
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = False
    End Sub

    Private Sub buttonNext2_Click(sender As Object, e As EventArgs) Handles buttonNext2.Click

        Dim school As DataRow = TimssBll.getSchoolRecordByConfID(textboxConfID.Text.ToString())
        labelEmailSummary.Text = school("PRINCIPALEMAIL")

        divStep1EnterConfCode.Visible = False
        divStep2AreYouPrincipal.Visible = False
        divStep3ThankYou.Visible = False
        divStep4ViewContactInformation.Visible = False
        divStep5UpdateContactInformation.Visible = False
        divStep6DoYouWantAReport.Visible = False
        divStep7ThankYou.Visible = True


        If Request.Url.ToString().ToLower().Contains("www.mytimss.com") Then
            Dim attachmentfilepath As String
            attachmentfilepath = Server.MapPath("\DOCUMENTS" & school("REPORTLINK"))

            If school("REPORTLINK").ToString().Contains("\G4 Reports\") And school("RESPONDED") = "Y" Then
                TimssBll.SendEmail_SCR_Yand4(school("SCHOOLNAME"), school("TIMSSID"), attachmentfilepath, school("PRINCIPALLASTNAME"), "TIMSSreport@westat.com", school("PRINCIPALEMAIL"), "", "", "TIMSS 2015 School Report Now Available", True, "")
            ElseIf school("REPORTLINK").ToString().Contains("\G4 Reports\") And school("RESPONDED") = "N" Then
                TimssBll.SendEmail_SCR_Nand4(school("SCHOOLNAME"), school("TIMSSID"), school("PRINCIPALLASTNAME"), "TIMSSreport@westat.com", school("PRINCIPALEMAIL"), "", "", "TIMSS 2015 School Report Confirmation", True, "")
            ElseIf school("REPORTLINK").ToString().Contains("\G8 Reports\") And school("RESPONDED") = "Y" Then
                TimssBll.SendEmail_SCR_Yand8(school("SCHOOLNAME"), school("TIMSSID"), attachmentfilepath, school("PRINCIPALLASTNAME"), "TIMSSreport@westat.com", school("PRINCIPALEMAIL"), "", "", "TIMSS 2015 School Report Now Available", True, "")
            ElseIf school("REPORTLINK").ToString().Contains("\G8 Reports\") And school("RESPONDED") = "N" Then
                TimssBll.SendEmail_SCR_Nand8(school("SCHOOLNAME"), school("TIMSSID"), school("PRINCIPALLASTNAME"), "TIMSSreport@westat.com", school("PRINCIPALEMAIL"), "", "", "TIMSS 2015 School Report Confirmation", True, "")
            ElseIf school("REPORTLINK").ToString().Contains("\Adv Reports\") And school("RESPONDED") = "Y" Then
                TimssBll.SendEmail_SCR_YandAdv(school("SCHOOLNAME"), school("TIMSSID"), attachmentfilepath, school("PRINCIPALLASTNAME"), "TIMSSreport@westat.com", school("PRINCIPALEMAIL"), "", "", "TIMSS Advanced 2015 School Report Now Available", True, "")
            ElseIf school("REPORTLINK").ToString().Contains("\Adv Reports\") And school("RESPONDED") = "N" Then
                TimssBll.SendEmail_SCR_NandAdv(school("SCHOOLNAME"), school("TIMSSID"), school("PRINCIPALLASTNAME"), "TIMSSreport@westat.com", school("PRINCIPALEMAIL"), "", "", "TIMSS Advanced 2015 School Report Confirmation", True, "")
            End If
        End If


        Session.Abandon()
        FormsAuthentication.SignOut()
    End Sub

    Private Sub SchoolReport_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            divText.Visible = False
            divSchoolReportDissimination.Visible = True
            divStep1EnterConfCode.Visible = True
            divStep2AreYouPrincipal.Visible = False
            divStep3ThankYou.Visible = False
            divStep4ViewContactInformation.Visible = False
            divStep5UpdateContactInformation.Visible = False
            divStep6DoYouWantAReport.Visible = False
            divStep7ThankYou.Visible = False
        End If


    End Sub
End Class
