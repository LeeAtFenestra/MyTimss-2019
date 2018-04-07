<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="RoleEdit.aspx.vb" Inherits="admin_RoleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
                <table border="0" cellpadding="3" cellspacing="5">
                    <tr>
                        <td>
                        
                        <fieldset class="register">
                            <legend>Role Information</legend>
                            <p>
                                <asp:Label ID="RoleNameLabel" runat="server">Role Name:</asp:Label>
                                <asp:TextBox ID="TextBoxRoleName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TextBoxRoleNameRequired" runat="server" ControlToValidate="TextBoxRoleName" 
                                     CssClass="failureNotification" ErrorMessage="Role Name is required." ToolTip="Role Name is required." 
                                     ValidationGroup="RegisterRoleValidationGroup">*</asp:RequiredFieldValidator>
                            </p>                  
                        </fieldset>
                        </td>
                    </tr>
                </table>
                        <br />
                        
                        <p class="submitButton">
                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" 
                                 ValidationGroup="RegisterRoleValidationGroup"/>

                            <asp:Button ID="ButtonAddNew" runat="server" Text="Create" 
                                 ValidationGroup="RegisterRoleValidationGroup"/>

                                 <input type="button" value="Cancel" onclick="document.location='roles.aspx'" />
                                 </p>
            </td>
        </tr>
    </table>
</asp:Content>

