<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    

    
    <asp:ChangePassword ID="cpdChangePswd" runat="server">
        <ChangePasswordTemplate>
            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="height: 172px">
                        <table border="0" cellpadding="0">
                            <tr>
                                <td align="left" colspan="2" class="webFormHeaderSmall">
                                    Change Your Password</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2"><br />
                                    <asp:Label ID="ChangePasswordMsg" runat="server" Text="Your password has expired.<br />There are N times left for you to change the password.<br />
                                    Otherwise your account will be locked." Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label><br /><br />
                                </td>
                            </tr>                    
                            <tr>
                               <td colspan="2">
                                Create your password using the following criteria:
                                    <ul>
                                        <li>Must have 8-14 characters</li>
                                        <li>Must have <b>all of the following</b>:
                                            <ul>
                                                <li>Uppercase(letter)</li>
                                                <li>Lowercase(letter)</li>
                                                <li>Numerical(digit)</li>
                                                <li>Special character (*!,~%@#$?^+=&)</li>
                                            </ul>
                                        </li>
                                    </ul>
                                    <span style="color:Red">Sample password:</span> KnightsR#1
                                    <br />&nbsp;
                                </td>
                            </tr>        
                            <tr>
                                <td align="right" style="width: 170px" valign="top">
                                    <asp:Label ID="lblCurrentPswd" runat="server" AssociatedControlID="CurrentPassword">Password:</asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valCurrentPswd" runat="server" ControlToValidate="CurrentPassword"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="valgrpChangePassword" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 24px; width: 170px;" valign="top">
                                    <asp:Label ID="lblNewPswd" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label></td>
                                <td style="height: 24px" align="left" valign="top">
                                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valNewPswd" runat="server" ControlToValidate="NewPassword" ToolTip="New Password is required."
                                        ValidationGroup="valgrpChangePassword" SetFocusOnError="True">*Please enter a new Password</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 170px" valign="top">
                                    <asp:Label ID="lblConfirmNewPswd" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valConfirmNewPswd" runat="server" ControlToValidate="ConfirmNewPassword"
                                        ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required."
                                        ValidationGroup="valgrpChangePassword" SetFocusOnError="True">*You must retype your password</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="valPasswords" runat="server" ControlToCompare="NewPassword"
                                        ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                                        ValidationGroup="valgrpChangePassword" SetFocusOnError="True"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: red; height: 13px;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnChangePswd" runat="server" CommandName="ChangePassword"
                                        Text="Change Password" ValidationGroup="valgrpChangePassword" CssClass="button3" Width="123px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
    </div>
    </form>
</body>
</html>
