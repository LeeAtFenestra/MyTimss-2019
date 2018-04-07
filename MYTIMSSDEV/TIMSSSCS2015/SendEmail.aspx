<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SendEmail.aspx.vb" Inherits="TIMSSSCS2015_SendEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Email Template</title>
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveClientUrl("~/")%>common/style2005.css">
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/util/hoverobject.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/fncVerifySave.js"></script>

</head>
<body text="#000000" style="margin:0px; padding:0px; background-color:#ffffff">

    <form id="frm" runat="server">
<input type="hidden" name="edited" value="0">
	<input type="hidden"  name="reader" value = "False" size="8">
 <table border="0" width="100%">
	<tr>
		<td nowrap class="hotcell" colspan="2">
        <strong>Email Preview</strong>
        </td>
	</tr>
	<tr>
		<td>
        <strong>Email Template:</strong>
        </td>
        <td>
        <asp:Label ID="LabelEmailTemplateName" runat="server" Text=""></asp:Label>
        </td>
	</tr>
	<tr>
		<td>
        <strong>From:</strong>
        </td>
        <td>
        <asp:Label ID="LabelEmailFrom" runat="server" Text=""></asp:Label>
        </td>
	</tr>
    
	<tr>
		<td>
        <strong>To:</strong>
        </td>
        <td>
        <asp:Label ID="LabelEmailTo" runat="server" Text=""></asp:Label>
        </td>
	</tr>
	<tr>
		<td>
        <strong>CC:</strong>
        </td>
        <td>
        <asp:Label ID="LabelEmailCC" runat="server" Text=""></asp:Label>
        </td>
	</tr>
	<tr>
		<td>
        <strong>BCC:</strong>
        </td>
        <td>
        <asp:Label ID="LabelEmailBCC" runat="server" Text=""></asp:Label>
        </td>
	</tr>
	<tr>
		<td>
        <strong>Subject:</strong>
       </td>
        <td>
        <asp:Label ID="LabelEmailSubject" runat="server" Text=""></asp:Label>
        </td>
	</tr>
    
	<tr><td colspan="2"><HR></td></tr>
    
	<tr>
		<td colspan="2">
        <asp:Label ID="LabelEmailBody" runat="server" Text=""></asp:Label>
        </td>
	</tr>
	<tr><td colspan="2"><HR></td></tr>
    </table>
    <center>
			 	<asp:Button ID="ButtonSend" runat="server" Text="Send" />
	    <INPUT type="button"  id="ButtonClose" runat="server" value="Exit">
    </center>



    </form>
</body>
</html>


