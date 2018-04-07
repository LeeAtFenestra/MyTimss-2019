<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditEmailTemplate.aspx.vb" Inherits="TIMSSSCS2015_EditEmailTemplate" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Email Template</title>
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveClientUrl("~/")%>common/style2005.css">
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/util/hoverobject.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/fncVerifySave.js"></script>

</head>
<body text="#000000" style="margin:0px; padding:0px; background-color:#ffffff">
<script language="javascript">
    //<-- hide this script from non-javascript-enabled browsers
    var imgEdit = new hoverbutton('edit', '<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', true);
    //-->		
</script>
    <form id="frm" runat="server">
<input type="hidden" name="edited" value="0">
	<input type="hidden"  name="reader" value = "False" size="8">
 <table border="0" width="100%">
	<tr>
		<td nowrap class="hotcell">
        <strong>Email Template: <asp:Label ID="LabelEmailTemplateName" runat="server" Text=""></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;        <img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15"></strong>
        </td>
	</tr>
	<tr>
		<td>
        <strong>From:</strong>
        <br />
                <asp:TextBox ID="db_EmailFrom" runat="server" Columns="80" Text="" onchange="Edited();"></asp:TextBox></td>
	</tr>
	<tr>
		<td>
        <br />
        <strong>CC:</strong>
        <br />
        <asp:TextBox ID="db_Emailcc" runat="server" Columns="80" Text="" onchange="Edited();"></asp:TextBox></td>
	</tr>
	<tr>
		<td>
        <br />
        <strong>BCC:</strong>
        <br />
        <asp:TextBox ID="db_Emailbcc" runat="server" Columns="80" Text="" onchange="Edited();"></asp:TextBox></td>
	</tr>
	<tr>
		<td>
        <br />
        <strong>Subject:</strong>
        <br />
        <asp:TextBox ID="db_EmailSubject" runat="server" Columns="80" Text="" onchange="Edited();"></asp:TextBox></td>
	</tr>
	<tr>
		<td>
        <br />
        <strong>Body:</strong>
        <br />
        <asp:TextBox ID="db_EmailBody" runat="server" Columns="80"  Rows="15" TextMode="MultiLine" Text="" onchange="Edited();"></asp:TextBox></td>
	</tr>
	<tr><td colspan="2"><HR></td></tr>
    </table>
    <center>
			 	<asp:Button ID="ButtonSave" runat="server" Text="Save" onclientclick="ClearEdited();" />
	    <INPUT type="reset" value="Reset"  name="Reset" onclick = "ClearEdited();">
	    <INPUT type="button"  id="ButtonClose" runat="server" value="Exit">
    </center>



    </form>
</body>
</html>


