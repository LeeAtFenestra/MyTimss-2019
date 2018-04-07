<%@ Page  Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="cache-control" content="max-age=0" />
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="expires" content="0" />
<meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />
<meta http-equiv="pragma" content="no-cache" />
                                    <script type="text/javascript" language="javascript">
                                        function setUsernameFocus() {
                                            var frm = getFrm();
                                            if (frm) {
                                                var uid = getFrm().elements["ctl00$cphBody$LoginView1$lgnFABLogin$UserName"];
                                                if (uid) {
                                                    uid.focus();
                                                }
                                            }
                                        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">


                                       <asp:LoginView ID="LoginView1" runat="server" EnableViewState="false">
                            <AnonymousTemplate>
                            <div>
<p>&nbsp;</p>

                                <%--<asp:Panel ID="PanelSiteDown" runat="server" Visible="false">
                                    <center><h1>MyTIMSS is down for maintenance</h1></center>
                                </asp:Panel>--%>
                <asp:Login ID="lgnFABLogin" runat="server" DestinationPageUrl="~/Default.aspx" OnLoggedIn="lgnFABLogin_LoggedIn" OnLoggingIn="lgnFABLogin_LoggingIn" OnLoginError="lgnFABLogin_LoginError">
                    <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                    <TextBoxStyle Font-Size="0.8em" />
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                    <LayoutTemplate>
                    
                                    <h1>Login</h1>
                                    <p>
                                        <asp:Label ID="lblTitle" runat="server" />
                                    </p>
                                   <%-- <p>&nbsp;</p>--%>
                                    <hr />
                                    <h2>Existing User Login</h2>

                                    <table border="0" cellpadding="6" cellspacing="0"
                                            width="100%">
                                        <tr>
                                            <td valign="top"> <asp:Label ID="lblUserName" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="UserName" CssClass="textbox" maxlength="50" runat="server" style="width: 170px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="valgrpFABLogin">*</asp:RequiredFieldValidator>

                                    <br />
                                    <br />
                                                <asp:Label ID="lblPassword" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                <br />
                                                <asp:TextBox  CssClass ="textbox"  MaxLength="50" ID="Password" runat="server" style="width: 170px" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="valgrpFABLogin">*</asp:RequiredFieldValidator>
                                 <%--   <br />
                                    <br />--%>
                                                <%--<asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>--%>
                                    <br />
                                    <br />
                                    <a href="ForgotPassword.aspx?page=password">Forgot Password</a> 
                                    |
                                    <a href="Troubleshoot.aspx">Having trouble logging in?</a>
  
                                    <br />
                                    <br />
                                  <%--  <br />
                                    <br />--%>
                                                <asp:Button CssClass="button3" ID="LoginButton" 
                                                    runat="server" 
                                                    CommandName="Login" Text="Log In" 
                                                    ValidationGroup="valgrpFABLogin" />&nbsp;&nbsp;<font color="red"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></font> </td>
                                            <td valign="top">
                                            <h3>
                                            First time visiting the <asp:Label ID="lblSite" runat="server" /> 2018 site? <br />Schools can register now!
                                            <br /><br />
                                    <a href="Register.aspx">Please register</a> 
                                            </h3>
                                            </td>
                                        </tr> 

                                     <tr><td colspan="2">
                                          <br /><br /><br />
                                         <asp:Label ID="lblFooterText" runat="server" Text="" />
                                      </td></tr>
  
                                    </table>

                    </LayoutTemplate>
                </asp:Login>
                <p>&nbsp;</p>
                <p>&nbsp;</p>

                                <asp:Panel ID="PanelAutosignIn" runat="server" Visible="false">
                                    <script type="text/javascript" language="javascript">
                                        function autologin2(code) {
                                            autologin2(code, code);
                                        }

                                        function autologin(u, p) {
                                            var frm = getFrm();
                                            var uid = getFrm().elements["ctl00$cphBody$LoginView1$lgnFABLogin$UserName"];
                                            var pwd = getFrm().elements["ctl00$cphBody$LoginView1$lgnFABLogin$Password"];
                                            var btn = getFrm().elements["ctl00$cphBody$LoginView1$lgnFABLogin$LoginButton"];

                                            uid.value = u;
                                            pwd.value = p;
                                            btn.click();
                                        }		
</script>
                                                Login with these test accounts:
                <br />
                <br />
                
                <a href="javascript:autologin('TIMS0001', 'Timss$2019');">Home Office</a>
                <br />
                
               <!--   <a href="javascript:autologin('TIMS0003', 'B=)6rZ&}8=!k');">NAEP State Coordinator</a>
                <br />
              
                <a href="javascript:autologin('TIMS0004', 'next#pwD!2016');">Field Manager</a>
                <br />
              
                <a href="javascript:autologin('TIMS0002', 'B=)6rZ&}8=!k');">Test Administrator</a>
                <br />  -->
                 
               
                <!--
                <a href="javascript:autologin('EfileUser', 'Sje@vxc12re2lY');">EfileUser</a>
                <br />
                         
                <a href="javascript:autologin('Admin', 'B=)6rZ&}8=!k');">Admin</a>      
                <br />-->
                 <!--
                <a href="javascript:autologin('FabTestUser', 'Sje@vxc12re2lY');">FabTestUser</a>
                <br /> 
              
                
                <a href="javascript:autologin('gordona8', 'B=)6rZ&}8=!k');">eTIMSS: Apollo ES Grade 4</a>
                <br /> 
                
                <a href="javascript:autologin('gordona2', 'B=)6rZ&}8=!k');">eTIMSS: Battle Ground Middle School Grade 8</a>
                <br /> 

                <a href="javascript:autologin('gordona4', 'B=)6rZ&}8=!k');">ICILS: Alva MS Grade 8</a>
                <br />   --> 
                
<!--                <a href="javascript:autologin('gordona', 'next#pwD!2017');">MyTIMSS User - 12th Grade</a>
                <br /> 

                <a href="javascript:autologin('tuda', 'B=)6rZ&}8=!k');">Tuda Coordinator User</a>
                <br /> -->
                
                                </asp:Panel>

                
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                </div>
                            </AnonymousTemplate>
                            <LoggedInTemplate>     
                                <asp:Panel ID="PanelTIMSS" runat="server" Visible="False">
                                    <h3>Welcome to the TIMSS 2018 field test!</h3>
                                    <p>&nbsp;</p>
                                    <p>Your school is participating in the TIMSS 2018 field test for the Trends in Mathematics and Science Study (TIMSS) to be conducted in 2018 at grades 4 and 8. Students in schools selected for grade 4 or grade 8 will take a digitally-based assessment of mathematics and science.</p>
                                    <p>This website will help school coordinators prepare for the upcoming TIMSS field test. Each assessment will be administered by trained TIMSS representatives. Thank you for helping your school participate in this important assessment.</p>
                                    <p>The "What You Need to Do" menu on the left will guide you through these activities. You will need to visit this website several times throughout the next few months, so remember your MyTIMSS username and password. You will be asked to update your password every 120 days. Use the left-hand menu to complete the tasks listed below.</p>
                                    <ul>
                                        <li>Provide School Information—Fall, 2017</li>
                                        <li>Submit Class List—January, 2018</li>
                                        <li>Submit Student List—January, 2018</li>
                                        <li>Prepare for Assessment—January-February, 2018</li>
                                    </ul>
                                </asp:Panel>
                                <asp:Panel ID="PanelICILS" runat="server" Visible="False">
                                    <h3>Welcome to ICILS 2018!</h3>
                                    <p>&nbsp;</p>
                                    <p>Your school is participating in the main study for the Information and Computer Literacy Study (ICILS) to be conducted in 2018 at grade 8. Students in schools selected for ICILS will take a digitally-based assessment of information and computer literacy. Teachers selected for ICILS will take an online questionnaire, as will the school principal and the ICT coordinator or person in charge of computer technology and literacy in your school. This website will help school coordinators prepare for the upcoming ICILS main study. Each assessment will be administered by trained ICILS representatives. Thank you for helping your school participate in this important assessment.</p>
                                    <p>The “What You Need to Do” menu on the left will guide you through these activities. You will need to visit this website several times throughout the next few months, so remember your MyICILS username and password. You will be asked to update your password every 120 days. Use the left-hand menu to complete the tasks listed below.</p>
                                    <ul>
                                        <li>Provide School Information—Fall, 2017</li>
                                        <li>Submit Student List—January, 2018</li>
                                        <li>Submit Teacher List—January, 2018</li>
                                        <li>Prepare for Assessment—January-February, 2018</li>
                                    </ul>
                                </asp:Panel>                               
                            </LoggedInTemplate>
                        </asp:LoginView>

</asp:Content>

