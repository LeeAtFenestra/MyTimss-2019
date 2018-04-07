<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:Panel ID="PanelMyTIMSSRegistrationID" runat="server" Visible="true">
     <p>
                First time visiting the 2018 <asp:Label ID="lblSite1" runat="server" /> site?
                <br />
                Please register by entering your assigned <asp:Label ID="lblSite2" runat="server" /> Registration ID.
                </p>

                <p>
                <asp:TextBox ID="TextBoxMyTIMSSRegistrationID" runat="server"></asp:TextBox>
                </p>
        
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxMyTIMSSRegistrationID" runat="server" ErrorMessage="*" ControlToValidate="TextBoxMyTIMSSRegistrationID"></asp:RequiredFieldValidator>
                <p>
                    <asp:Label ID="LabelFailureText" runat="server" EnableViewState="False" Visible="false" ForeColor="Red" />
                </p>
                
               <asp:Button ID="ButtonValidateID" runat="server" Text="Continue" />
    </asp:Panel>
    <asp:Panel ID="PanelRegistrationInformation" runat="server" Visible="false">

                <table border="0" cellpadding="3" cellspacing="4" width="100%">
                <tr>
                    <td width="50%" align="right"><b>District:</b></td>
                    <td><asp:Label ID="LabelDistrict" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"><b>School Name:</b></td>
                    <td><asp:Label ID="LabelSchoolName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"><b><asp:Label ID="lblLabelRegID" runat="server" /> Registration ID:</b></td>
                    <td><asp:Label ID="LabelMyTIMSSRegistrationID" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2"><hr /></td>
                </tr>
                <tr>
                    <td align="right"><b>First Name:</b></td>
                    <td><asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxFirstName" runat="server" ErrorMessage="*" ControlToValidate="TextBoxFirstName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right"><b>Last Name:</b></td>
                    <td><asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxLastName" runat="server" ErrorMessage="*" ControlToValidate="TextBoxLastName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right"><b>Telephone:</b></td>
                    <td><asp:TextBox ID="TextBoxTelephone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxTelephone" runat="server" ErrorMessage="*" ControlToValidate="TextBoxTelephone"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right"><b>Extension:</b></td>
                    <td><asp:TextBox ID="TextBoxExtension" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" valign="top"><b>Email:</b></td>
                    <td><asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxEmail" runat="server" ErrorMessage="*" ControlToValidate="TextBoxEmail"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidatorEmail" runat="server" 
                        ControlToValidate="TextBoxConfirmEmail"
                        CssClass="ValidationError"
                        ControlToCompare="TextBoxEmail"
                        ErrorMessage="Email and Confirm Email must be the same" 
                        ToolTip="Email and Confirm Email must be the same"
                         Display="Dynamic" /></td>
                
                </tr>
                <tr>
                    <td align="right" valign="top"><b>Confirm Email Address:</b></td>
                    <td><asp:TextBox ID="TextBoxConfirmEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxConfirmEmail" runat="server" ErrorMessage="*" ControlToValidate="TextBoxConfirmEmail"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Are you the school coordinator for <asp:Label ID="LabelSchoolName2" runat="server"></asp:Label>?</td>
                    <td><asp:CheckBox ID="CheckBoxSchoolCoordinator" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2"><hr /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Create your own password using the following criteria:
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top"><b>Password:</b></td>
                    <td><asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxPassword" runat="server" ErrorMessage="*" ControlToValidate="TextBoxPassword"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidatorPassword" runat="server" 
                        ControlToValidate="TextBoxConfirmPassword"
                        CssClass="ValidationError"
                        ControlToCompare="TextBoxPassword"
                        ErrorMessage="Password and Confirm Password must be the same" 
                        ToolTip="Password and Confirm Password must be the same" 
                         Display="Dynamic"/></td>

                </tr>
                <tr>
                    <td align="right" valign="top"><b>Confirm Password:</b></td>
                    <td><asp:TextBox ID="TextBoxConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxConfirmPassword" runat="server" ErrorMessage="*" ControlToValidate="TextBoxConfirmPassword"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><asp:RegularExpressionValidator ID="revPassword" runat="server" 
                                                ErrorMessage="Password must be between 8 and 14 characters and contain at least two of the following:<br /> uppercase letter, lowercase letter, numeral, and one special character." 
                                                ValidationExpression="(?=^.{8,14}$)((?=.*\d)(?=.*[a-z])(?=.*[A-Z])*|(?=.*\d)*(?=.*[a-z])(?=.*[A-Z])|(?=.*\d)(?=.*[a-z])*(?=.*[A-Z]))^.*$" 
                                                ControlToValidate="TextBoxPassword" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
            </table>
                 
                                 
            <p style="text-align:center">
               <asp:Button ID="ButtonRegister" runat="server" Text="Register" />
            </p>

        <asp:Label ID="labelSchoolState" runat="server" Visible="false" />

    </asp:Panel>

    <asp:Panel ID="PanelRegistrationComplete" runat="server" Visible="false">
    <h3>You are successfully registered!</h3>
                <br />
                Your username is: <asp:Label ID="LabelUsername" runat="server"></asp:Label>
                <br />
                Please remember your username and password!
                <hr />
                A confirmation email that includes your username has been sent.

                <p>
                <asp:Button ID="ButtonContinueToMyTIMSS" 
                                                    runat="server" 
                                                    Text="" />
                </p>
    </asp:Panel>
    
</asp:Content>

