<%@ Page Title="Forgot Password" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
                          <table  width = "100%" cellpadding="8" cellspacing="4">
        <tr>
            <td bgcolor="#FFFFFF">
                            <h1>
                            Forgot Password
                            </h1>
                <asp:Panel ID="PanelForgotUsername" runat="server" Visible="false">
                Please provide your email address. Your username will be sent to you in a confirmation email. 
                If you cannot remember your password, use the <a href="ForgotPassword.aspx?page=password">Forgot Password</a> link on the the login page.
                    <br />
                    <h4>Email Address</h4>
                    <asp:TextBox ID="TextBoxForgotUsername" runat="server" Columns="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxForgotUsername" runat="server" ControlToValidate="TextBoxForgotUsername" CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required.">*</asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Button ID="ButtonSendUsername" runat="server" Text="Send Username" />
                </asp:Panel>
                <asp:Panel ID="PanelForgotPassword" runat="server" Visible="false">
                Please provide your email address. A temporary password will be sent to you in a confirmation email.
               
                    <div id="divTIMSS" runat="server" visible="false">
                        If you cannot remember your username, please contact <a href="mailto:TIMSS@westat.com">TIMSS@westat.com</a>.
                    </div>
                     
                    <div id="divICILS" runat="server" visible="false">
                        If you cannot remember your username, please contact <a href="mailto:ICILS@westat.com">ICILS@westat.com</a>.
                    </div>

                    <br />
                    <h4>Email Address</h4>
                    <asp:TextBox ID="TextBoxForgotPassword" runat="server" Columns="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxForgotPassword" runat="server" ControlToValidate="TextBoxForgotPassword" CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required.">*</asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Button ID="ButtonSendPassword" runat="server" Text="Send Password" />
                </asp:Panel>
                <asp:Panel ID="PanelDone" runat="server" Visible="false">
                    Your password has been sent to the Email address provided.
                    <br />
                    <br />
                    <a href="Default.aspx">Back to MyTIMSS</a>
                </asp:Panel>
                <br />
                <br />
                                    <asp:Label id="ErrorMsg" runat="server" ForeColor="maroon" Text="This email address does not exist in our system." Visible="false" ViewStateMode="Disabled" /><br />
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
            </td>
        </tr>
    </table>     
</asp:Content>

