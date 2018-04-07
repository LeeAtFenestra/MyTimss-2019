<%@ Page Title="Edit Account" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="AccountEdit.aspx.vb" Inherits="admin_AccountEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function ShowConfirm(obj, copy) {
            var copyObj = document.getElementById(copy);
            //alert(copy + ' = ' + copyObj);
            if (confirm("Copy name and email address?") == true) {
                copyObj.value = 1;
                //__doPostBack(obj.id, ''); 
            }
            else {
                copyObj.value = 0;
                //return false;
            }
            __doPostBack(obj.id, '');
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
                <table border="0" cellpadding="3" cellspacing="5">
                    <tr>
                        <td>
                        
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server">User Name:</asp:Label>
                                <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TextBoxUserNameRequired" runat="server" ControlToValidate="TextBoxUserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>

                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <asp:Panel ID="panelpassword" runat="server">
                            
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                            </asp:Panel>

                            <p>
                                <asp:Label ID="LabelDescription" runat="server" AssociatedControlID="TextBoxDescription">Description:</asp:Label>
                                <asp:TextBox ID="TextBoxDescription" runat="server" CssClass="textEntry"></asp:TextBox>
                            </p>          
                            <p>
                                <asp:Label ID="LabelIsApproved" runat="server" AssociatedControlID="CheckBoxIsApproved">Is Active:</asp:Label>
                                <asp:CheckBox ID="CheckBoxIsApproved" runat="server" Checked="true" />
                            </p>         
                            <asp:Panel ID="panelResetPassword" runat="server">
                                <div style="text-align:right">
                                    <asp:LinkButton ID="LinkButtonLoginAs" runat="server" OnClick="LinkButtonLoginAs_OnClick" OnClientClick="return confirm('Are you sure you want to login as this account?');">Login As</asp:LinkButton>
                                    <br />
                                    <asp:Label id="Msg" runat="server" ForeColor="maroon" /><br />
                                    <asp:Button id="ResetPasswordButton" Text="Reset Password" OnClick="ResetPassword_OnClick" runat="server" OnClientClick="return confirm('Are you sure you want to reset the password for this account?');" />
                                </div>
                            </asp:Panel>     
                        </fieldset>
                        </td>
                        <td rowspan="2" valign="top">
                        <fieldset class="register">
                            <legend>Roles</legend>
                            <p>
                                <asp:CheckBoxList ID="CheckBoxListRoles" runat="server">
                                </asp:CheckBoxList>

                            </p>
                        </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <fieldset class="register">
                            <legend>Profile Information</legend>
                            <p>
                                <asp:Label ID="LabelFirstname" runat="server" AssociatedControlID="TextBoxFirstname">Firstname:</asp:Label>
                                <asp:TextBox ID="TextBoxFirstname" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxFirstname" runat="server" ControlToValidate="TextBoxFirstname" 
                                     CssClass="failureNotification" ErrorMessage="Firstname is required." ToolTip="Firstname is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="LabelLastname" runat="server" AssociatedControlID="TextBoxLastname">Lastname:</asp:Label>
                                <asp:TextBox ID="TextBoxLastname" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxLastname" runat="server" ControlToValidate="TextBoxLastname" 
                                     CssClass="failureNotification" ErrorMessage="Lastname is required." ToolTip="Lastname is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="LabelWINSID" runat="server" AssociatedControlID="DropDownListWINSID">WINS ID:</asp:Label>
                                <asp:DropDownList ID="DropDownListWINSID" runat="server" DataTextField="Name" DataValueField="Value" AutoPostBack="true">
                                </asp:DropDownList> <i>(needed for Field Manager, Test Administrator and Test Administrator Troubleshooter roles)</i>
                                <asp:Label ID="LabelWinsIdError" runat="server" Visible="false" ForeColor="Red"><br /><strong>This WINSID has already been linked to an account</strong></asp:Label>
                                <asp:HyperLink ID="HyperLinkViewWinsId" runat="server" Visible="false" Target="_blank">View</asp:HyperLink>
                                <asp:HiddenField ID="HiddenFieldCaopyNameAndEmail" runat="server" />

                            </p>
                            <p>
                                <asp:Label ID="LabelREPSBGRP" runat="server" AssociatedControlID="DropDownListREPSBGRP">REPSBGRP:</asp:Label>
                                <asp:DropDownList ID="DropDownListREPSBGRP" runat="server" DataTextField="Name" DataValueField="Value">
                                </asp:DropDownList> <i>(needed for the NAEP State Coordinator role)</i>
                            </p>
                            <p>
                                <asp:Label ID="LabelTelephone" runat="server" AssociatedControlID="TextBoxTelephone">Telephone:</asp:Label>
                                <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="textEntry"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="LabelExtension" runat="server" AssociatedControlID="TextBoxExtension">Extension:</asp:Label>
                                <asp:TextBox ID="TextBoxExtension" runat="server" CssClass="textEntry"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="LabelMyTIMSSRegistrationID" runat="server" AssociatedControlID="DropDownListSchoolList">MyTIMSS Registration ID:</asp:Label>
                                <asp:DropDownList ID="DropDownListSchoolList" runat="server" DataTextField="Name" DataValueField="Value" AutoPostBack="true"></asp:DropDownList>
                            </p>
                            <p>
                                <asp:Label ID="LabelFrame_N_" runat="server" AssociatedControlID="TextBoxFrame_N_">Frame_N_:</asp:Label>
                                <asp:TextBox ID="TextBoxFrame_N_" runat="server" CssClass="textEntry"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="LabelTUA_LEA" runat="server" AssociatedControlID="DropDownListTUA_LEA">TUDA LEAID:</asp:Label>
                                <asp:DropDownList ID="DropDownListTUA_LEA" runat="server" DataTextField="Name" DataValueField="Value"></asp:DropDownList>
                            </p>
                        </fieldset>
                        </td>
                    </tr>
                </table>
                        <br />
                        
                        <p class="submitButton">
                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" 
                                 ValidationGroup="RegisterUserValidationGroup"/>

                            <asp:Button ID="ButtonAddNew" runat="server" Text="Create" 
                                 ValidationGroup="RegisterUserValidationGroup"/>

                                 <input type="button" value="Cancel" onclick="document.location='accounts.aspx'" />
                                 <asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                 </p>
            </td>
        </tr>
    </table>
</asp:Content>

