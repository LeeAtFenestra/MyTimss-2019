<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectSchool.aspx.vb" Inherits="SelectSchool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change School</title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>
        Select a school: 
        <asp:Button ID="ButtonSelectSchool" runat="server" Text="View School" />
   </h3>    
            <asp:ListBox ID="SelectedSchool" runat="server" DataTextField="s_name" DataValueField="frame_n_" Rows="25"></asp:ListBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectedSchool" runat="server" ErrorMessage="*" ControlToValidate="SelectedSchool"></asp:RequiredFieldValidator>
    
    </form>
</body>
</html>
