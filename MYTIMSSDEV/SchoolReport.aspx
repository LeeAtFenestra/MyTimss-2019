<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchoolReport.aspx.vb" Inherits="SchoolReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Report Dissemination</title>
        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div id="divBanner" runat="server" Visible="True">
              <table style="width:105%; background-color:#123F75; margin:-10px; padding:-10px;"><tr><td><asp:Image ID="imageTIMSSLogo" runat="server" ImageUrl="~/Common/images/timssusalogo.PNG" /></td></tr></table> 
        </div>

        <div id="divText" runat="server" visible ="False">
             <h1><label><b>TIMSS <asp:Label ID="labelAdvanced" runat="server" Text="Advanced&nbsp;" />2015 School Report Dissemination</b></label></h1>
        </div>

        <div id="divSchoolReportDissimination" runat="server" visible="true" style="padding-left:50px;padding-right:50px">

              <div id="divStep1EnterConfCode" runat="Server" visible="False">
                <p><h3>Please enter the confirmation code from your email here:</h3>&nbsp;<asp:TextBox ID="textboxConfID" runat="server" MaxLength="12" Width="110"  /></p>
                  <div id="divIncorrectConfirmation" runat="server" visible="false">
                      <label style="color:red">*Confirmation code is not valid</label>
                  </div>
                <p><asp:Button ID="buttonExit" runat="server" Text="Exit" />&nbsp;<asp:Button ID="buttonNext" runat="server" Text="Next" /></p>
              </div>

              <div id="divStep2AreYouPrincipal" runat="Server" visible="False">
                <p>Are you the current <b>principal</b> of <b><asp:Label ID="labelSchoolName" runat="server" /></b> located at <b><asp:Label ID="labelSchoolAddress" runat="server" /></b>?</p>
                <p><asp:Button ID="buttonBack3" runat="server" Text="Go back to previous screen" />&nbsp;<asp:Button ID="buttonStep2No" runat="server" Text="No, I am not" OnClientClick="return confirm('If you are not the principal of this school, please confirm below:\n\nPress OK to confirm you are not the principal.\nPress Cancel to go back.')" />&nbsp;<asp:Button ID="buttonStep2Yes" runat="server" Text="Yes, I am" /></p>
              </div>

              <div id="divStep3ThankYou" runat="Server" visible="False">
                  <p>Thank you for specifying this information. An email summary has been sent to our TIMSS Help Desk letting them know you are not the current principal.</p>
                  <p>We may or may not reach out to you during this time to get this information corrected.</p>
                  <p>If you have any questions or comments, please email <b>TIMSS@westat.com</b> or call <b>1-855-445-5604</b> Monday through Friday between 9 a.m. and 5:00 p.m. ET</p>
                  <p>You may now close this Web page or go back to the Login page&nbsp;<asp:LinkButton ID="linkbuttonGoBack" runat="server" Text="here" PostBackUrl="~/Default.aspx" />.</p>
              </div>

            <div id="divStep4ViewContactInformation" runat="Server" visible="False">
                <p>Is the below principal contact information up-to-date for <b><asp:Label ID="labelSchoolName2" runat="server" /></b>?</p>
                <p><label>First, last name:&nbsp;</label><b><asp:Label ID="labelPrincipalFirstName" runat="server" />&nbsp;<asp:Label ID="labelPrincipalLastName" runat="server" /></b></p>
                <p><label>Email address:&nbsp;</label><b><asp:Label ID="labelPrincipalEmail" runat="server" /></b></p>
                <p><asp:Button ID="buttonBack2" runat="server" Text="Back" />&nbsp;<asp:Button ID="buttonNotCorrect" runat="server" Text="No, I need to update" />&nbsp;<asp:Button ID="buttonYesCorrect" runat="server" Text="Yes, this is correct" /></p>
            </div>

            <div id="divStep5UpdateContactInformation" runat="server" visible="false">
                <p>Please enter the up-to-date principal contact information below for <b><asp:Label ID="labelSchoolName3" runat="server" /></b>:</p>
                <p><label>First name:&nbsp;</label><asp:TextBox ID="textboxFirstName" runat="server" /></p>
                <p><label>Last name:&nbsp;</label><asp:TextBox ID="textboxLastName" runat="server" /></p>
                <p><label>Email address:&nbsp;</label><asp:TextBox ID="textboxEmail" runat="server" /></p>
                <p><asp:Button ID="buttonCancel" runat="server" Text="Reset to default and go back" />&nbsp;<asp:Button ID="buttonUpdate" runat="server" Text="Update" />&nbsp;<asp:Label ID="labelEmailRequired" runat="server" Text="*All fields are required" visible="false" ForeColor="Red"/></p>
            </div>

            <div id="divStep6DoYouWantAReport" runat="server" visible="false">
              
                    <div id="literalReportYes" runat="server" Visible="False"><p><label>You have indicated that you <b>would like</b> a TIMSS <asp:Label ID="labelAdvanced2" runat="server" Text="Advanced&nbsp;" />2015 school report. Only the current principal at <b><asp:Label ID="labelSchoolName4" runat="server" /></b> may receive this report. Please click the <b>Finish</b> button to <b>receive</b> a TIMSS <asp:Label ID="labelAdvanced6" runat="server" Text="Advanced&nbsp;" />2015 school report.</label></p></div>
                    <div id="literalReportNo" runat="server" Visible="False"><p><label>You have <b>not yet</b> indicated that you would like a TIMSS <asp:Label ID="labelAdvanced3" runat="server" Text="Advanced&nbsp;" />2015 school report. Only the current principal at <b><asp:Label ID="labelSchoolName5" runat="server" /></b> may receive this report.  Please click the <b>Finish</b> button to <b>not receive</b> a TIMSS <asp:Label ID="labelAdvanced7" runat="server" Text="Advanced&nbsp;" />2015 school report.</label></p></div>
                    <div id="literalReportYes2" runat="server" Visible="False"><p><label>If you do not want a TIMSS <asp:Label ID="labelAdvanced4" runat="server" Text="Advanced&nbsp;" />2015 school report, please click <asp:LinkButton ID="buttonSwitchRespondent" runat="server" Text="here"  />.</label></p></div>
                    <div id="literalReportNo2" runat="server" Visible="False"><p><label>If you would like to receive a TIMSS <asp:Label ID="labelAdvanced5" runat="server" Text="Advanced&nbsp;" />2015 school report, please click here <asp:LinkButton ID="buttonSwitchRespondent2" runat="server" Text="here"  />.</label></p></div>

                <p><asp:Button ID="buttonBack" runat="server" Text="Back" />&nbsp;<asp:Button ID="buttonNext2" runat="server" Text="Finish" OnClientClick="return confirm('This will complete your School Report Dissemination process. If you are finished, please confirm below:\n\nPress OK to confirm\nPress Cancel to go back.')" /></p>
            </div>

            <div id="divStep7ThankYou" runat="server" visible="False">
                <p><label>Thank you for taking the time to answer these questions.</label></p>
                <p><label>We have sent a follow-up email to the email address:</label>&nbsp;<b><asp:Label ID="labelEmailSummary" runat="server" /></b></p>
                <p>You may now exit this webpage or go back to the Login page&nbsp;<asp:LinkButton ID="linkbuttonGoBack2" runat="server" Text="here" PostBackUrl="~/Default.aspx" />.</p>
            </div>

            <div id="divFooter" runat="server" visible="True">
                <p><label style="font-size:small;"><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></label></p>
            </div>
             
        </div>
    </form>
</body>
</html>
