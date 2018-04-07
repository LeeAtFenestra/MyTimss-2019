Imports Westat.TIMSS.BLL

Partial Class _Default
    Inherits BasePagePublic
    Private mMaintenanceWindow As Boolean = False

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim currentabsolute As String = System.Web.HttpContext.Current.Request.Url.AbsoluteUri

        Dim lgnFABLogin As Login = LoginView1.FindControl("lgnFABLogin")
            If lgnFABLogin IsNot Nothing Then
                Dim TitleMessage As Label = lgnFABLogin.FindControl("lblTitle")
                Dim FooterText As Label = lgnFABLogin.FindControl("lblFooterText")
                Dim lblSiteText As Label = lgnFABLogin.FindControl("lblSite")
            If currentabsolute.Contains("mytimss.com") Or currentabsolute.Contains("mytimssdev.westat.com") Or currentabsolute.Contains("mytimsstst.wesdemo.com") Or currentabsolute.Contains("mytimssdemo.wesdemo.com") Then
                TitleMessage.Text = "MyTIMSS is a restricted-use website that contains information on the Trends in International Mathematics and Science Study (TIMSS)."
                FooterText.Text = "The National Center for Education Statistics (NCES), within the U.S. Department of Education, conducts TIMSS in the United States as authorized by the Education Sciences Reform Act of 2002 (20 U.S.C. §9543). All of the information you provide may be used only for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).<br><br>According to the Paperwork Reduction Act of 1995, no persons are required to respond to a collection of information unless such collection displays a valid OMB control number. The valid OMB control number for this voluntary information collection is 1850-0695. The time required to complete this information collection is estimated to average 240 minutes per school coordinator, including the time to review instructions, search existing data resources, gather the data needed, and complete and review the information collection. If you have any comments or concerns regarding the accuracy of the time estimate(s), suggestions for improving the form, or questions about the status of your individual submission of this form, write directly to Trends in International Mathematics and Science Study (TIMSS), National Center for Education Statistics, Potomac Center Plaza (PCP), 550 12th St., SW, 4th floor, Washington, DC 20202.<br><br>OMB No. 1850-0695, Approval Expires 01/31/2021.<br><br><b>Notice: You are accessing a U.S. Government information system.</b><br><br>This warning banner provides privacy and security notices consistent with applicable federal laws, directives, and other federal guidance for accessing this Government system, which includes all devices/storage media attached to this system. This system is provided for Government-authorized use only. Unauthorized or improper use of this system is prohibited and may result in disciplinary action and/or civil and criminal penalties."
                lblSiteText.Text = "MyTIMSS"
                Page.Title = "MyTIMSS"
            ElseIf currentabsolute.Contains("myicils.com") Then
                Page.Title = "MyICILS"
                    lblSiteText.Text = "MyICILS"
                    TitleMessage.Text = "MyICILS is a restricted-use website that contains information on the International Computer and Information Literacy Study (ICILS)."
                FooterText.Text = "The National Center for Education Statistics (NCES), within the U.S. Department of Education, conducts ICILS in the United States as authorized by the Education Sciences Reform Act of 2002 (20 U.S.C. §9543). All of the information you provide may be used only for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).<br><br>According to the Paperwork Reduction Act of 1995, no persons are required to respond to a collection of information unless such collection displays a valid OMB control number. The valid OMB control number for this voluntary information collection is 1850-0929. The time required to complete this information collection is estimated to average 240 minutes per school coordinator, including the time to review instructions, search existing data resources, gather the data needed, and complete and review the information collection. If you have any comments or concerns regarding the accuracy of the time estimate(s), suggestions for improving the form, or questions about the status of your individual submission of this form, write directly to: International Computer and Information Literacy Study (ICILS), National Center for Education Statistics, Potomac Center Plaza (PCP), 550 12th St., SW, 4th floor, Washington, DC 20202.<br><br>OMB No. 1850-0929, Approval Expires 12/31/2020.<br><br><b>Notice: You are accessing a U.S. Government information system.</b><br><br>This warning banner provides privacy and security notices consistent with applicable federal laws, directives, and other federal guidance for accessing this Government system, which includes all devices/storage media attached to this system. This system is provided for Government-authorized use only. Unauthorized or improper use of this system is prohibited and may result in disciplinary action and/or civil and criminal penalties."
            End If

            End If

        If mMaintenanceWindow Then
            Dim showbackdoor As Boolean = IIf(String.IsNullOrEmpty(Request.QueryString("backdoor")), False, True)
            'Dim PanelSiteDown As Panel = LoginView1.FindControl("PanelSiteDown")
            'If PanelSiteDown IsNot Nothing Then
            '    PanelSiteDown.Visible = Not showbackdoor
            'End If

            If lgnFABLogin IsNot Nothing Then
                lgnFABLogin.Visible = showbackdoor
            End If

            Dim pnl As Panel = LoginView1.FindControl("PanelAutosignIn")
            If pnl IsNot Nothing Then
                If TIMSSBLL.IsDevelopmentWebsite Then
                    pnl.Visible = showbackdoor
                    ' Else
                    '    pnl.Visible = False
                End If

                If TIMSSBLL.IsTrainingWebsite Then
                    pnl.Visible = showbackdoor
                    ' Else
                    '    pnl.Visible = False
                End If
            End If
        Else
            Dim pnl As Panel = LoginView1.FindControl("PanelAutosignIn")
            If pnl IsNot Nothing Then

                Dim flg As Boolean = IIf(TIMSSBLL.IsDevelopmentWebsite, True, IIf(TIMSSBLL.IsTrainingWebsite, True, False))

                pnl.Visible = flg
            End If
        End If

        If User.Identity.IsAuthenticated Then
            If TIMSSBLL.IsMyTIMSSUser Then
                If Not TimssBll.HasSchoolName() Then
                    TimssBll.InitSchoolSessionInfo()
                End If
            End If
            Dim PanelTIMSS As Panel = LoginView1.FindControl("PanelTIMSS")
            Dim PanelICILS As Panel = LoginView1.FindControl("PanelICILS")
            PanelTIMSS.Visible = False
            PanelICILS.Visible = False
            If TimssBll.HasSchoolName() Then
                PanelTIMSS.Visible = TimssBll.iseTIMSS()
                PanelICILS.Visible = Not PanelTIMSS.Visible
            End If
        Else
            TIMSSBLL.CleanUpSessionVariables()
            If Not IsPostBack Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "setusernamefocus", "<script language=javascript>setUsernameFocus();</script>")

            End If
        End If

    End Sub


    Protected Sub lgnFABLogin_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs)
        'when the user logs in for the 1st time we need to make sure that they have a profile so they can edit it
        'the only reason they wouldn't have a profile was if they were created using the Web Site Administration Tool


        TIMSSBLL.CleanUpSessionVariables()

        Dim lgnFABLogin As Login = sender
        Dim profiles As New ProfileInfoCollection
        profiles = ProfileManager.FindProfilesByUserName(ProfileAuthenticationOption.All, lgnFABLogin.UserName)
        'If profiles.Count = 0 Then
        '    Westat.FAB.Profile.CustomProfile.SaveDetail(lgnFABLogin.UserName, Nothing, "Not Set", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '    Westat.FAB.Profile.CustomProfile.SaveDetail(lgnFABLogin.UserName, Nothing, "", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'End If

        Dim intRetryAttampts As Integer
        Dim bChangePassword As Boolean
        Dim hashedPassword As String = Nothing
        Dim bShouldChangePassword As Boolean
        Dim FABMembershipProvider As Westat.FAB.MembershipProvider.CustomMembershipProvider
        FABMembershipProvider = CType(Membership.Provider, Westat.FAB.MembershipProvider.CustomMembershipProvider)
        Session("ChangePassword") = "no"
        Session("RetryAttampts") = 0
        bShouldChangePassword = FABMembershipProvider.ShouldChangePassword(lgnFABLogin.UserName, lgnFABLogin.Password, bChangePassword, hashedPassword, intRetryAttampts)
        If (bShouldChangePassword = True And intRetryAttampts > 0) Then
            Session("ChangePassword") = "yes"
            Session("RetryAttampts") = intRetryAttampts
            intRetryAttampts = intRetryAttampts - 1
            FABMembershipProvider.UpdateCurrentPasswordStatus(lgnFABLogin.UserName, hashedPassword, bChangePassword, intRetryAttampts)
            Response.Redirect("~/ChangePassword.aspx")
        Else
            'Response.Redirect("~/TIMSSSCS2015/Default.aspx")

            If lgnFABLogin.UserName.ToLower() = "reports2015" Then
                Response.Redirect("~/SchoolReport.aspx")
            End If

            'Update so PDFs will open on login
            'Still authenticates users' ReturnURL so they don't access SCS
            If Request.QueryString("ReturnURL") IsNot Nothing Then
                If Request.QueryString("ReturnURL").ToString.Contains("/TIMSSSCS2015/") Then
                    If TIMSSBLL.UsernameCanAccessSCS(lgnFABLogin.UserName) Then
                        Response.Redirect(Me.Request.QueryString("ReturnURL"))
                    End If
                Else
                    Response.Redirect(Me.Request.QueryString("ReturnURL"))
                End If
            End If

            If TIMSSBLL.UsernameCanAccessSCS(lgnFABLogin.UserName) Then
                Response.Redirect("~/TIMSSSCS2015/Default.aspx")
            Else
                Response.Redirect("~/Default.aspx")
            End If

        End If

    End Sub

    Protected Sub lgnFABLogin_LoggingIn(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs)
        Dim lgnFABLogin As Login = sender
        Dim intRetryAttampts As Integer
        Dim bChangePassword As Boolean
        Dim hashedPassword As String = Nothing
        Dim bShouldChangePassword As Boolean
        Dim FABMembershipProvider As Westat.FAB.MembershipProvider.CustomMembershipProvider
        FABMembershipProvider = CType(Membership.Provider, Westat.FAB.MembershipProvider.CustomMembershipProvider)
        bShouldChangePassword = FABMembershipProvider.ShouldChangePassword(lgnFABLogin.UserName, lgnFABLogin.Password, bChangePassword, hashedPassword, intRetryAttampts)
        If (bShouldChangePassword = True) Then
            If (intRetryAttampts <= 0) Then
                Dim mu As MembershipUser = FABMembershipProvider.GetUser(lgnFABLogin.UserName, False)
                If (mu.IsApproved = True) Then
                    mu.IsApproved = False
                    FABMembershipProvider.UpdateUser(mu)
                End If
                Dim cr As New Westat.FAB.Configuration.ConfigurationReader
                lgnFABLogin.FailureText = "*" & cr.AcctDisabledMsg
            End If
        End If
    End Sub

    Protected Sub lgnFABLogin_LoginError(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lgnFABLogin As Login = sender
        Dim user As MembershipUser = Membership.GetUser(lgnFABLogin.UserName)
        If user IsNot Nothing Then
            If user.IsLockedOut Then
                'log to HM
                Dim actLogOutEvent As New Westat.FAB.Security.HealthMonitoring.FAB_AccountLockedEvent(user.UserName.ToString, String.Format("Account lock out for user {0}", user.UserName), Me, user.UserName.ToString)
                Westat.FAB.Security.HealthMonitoring.FAB_AccountLockedEvent.Raise(actLogOutEvent)
                Dim cr As New Westat.FAB.Configuration.ConfigurationReader
                lgnFABLogin.FailureText = "*" & cr.AcctLockOutMsg
            End If
        End If
    End Sub



End Class
