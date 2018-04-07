<%@ Page Title="Edit Profile" Language="VB" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="Profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Profile</title>
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveClientUrl("~/")%>common/style2005.css">
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/util/hoverobject.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/fncVerifySave.js"></script>

</head>
<body text="#000000" style="margin:0px; padding:0px; background-color:#ffffff">
<script language="javascript">
    //<-- hide this script from non-javascript-enabled browsers
    var imgEdit = new hoverbutton('edit', "<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif", "<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif", "<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif", true);
    //-->		
</script>
    <form id="frm" runat="server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
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
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                        </fieldset>
                        
                        <p class="submitButton">
                            <asp:Button ID="ButtonSave" runat="server" Text="Save" 
                                 ValidationGroup="RegisterUserValidationGroup"/>
                                 <input type="button" value="Cancel" onclick="window.close();" />
                                 </p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>


