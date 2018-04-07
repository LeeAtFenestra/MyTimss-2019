<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchoolPersonnelEdit.aspx.vb" Inherits="SchoolPersonnelEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>School Personnel</title>
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveClientUrl("~/")%>common/style2005.css">
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/util/hoverobject.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/fncVerifySave.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/SCSGlobal.js"></script>   

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
<table BORDER="0" CELLSPACING="1" width="100%">

    <tr>
      <td colspan="2" class="hotcell"><font face="Verdana" size="2"><b>School Personnel</b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;<img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15"></td>
    </tr>
    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Prefix:</font></td>
      <td>
                <asp:TextBox ID="fldname" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="db_frame_n_" runat="server" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="prefixlist" runat="server" onchange = "Edited();" Visible="false">
                        </asp:DropDownList>
                        
                <asp:TextBox ID="db_prefix" runat="server" size="5" maxlength="8" Text="prefix" onchange="Edited();"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">First Name:</font></td>
      <td>
                <asp:TextBox ID="db_fname" runat="server" size="27" maxlength="30" Text="fname" onchange="Edited();"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Last Name:</font></td>
      <td>
                <asp:TextBox ID="db_lname" runat="server" size="27" maxlength="30" Text="lname" onchange="Edited();"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatordb_lname" runat="server" 
                                ErrorMessage="Last Name is required." ControlToValidate="db_lname" ForeColor="Red" Font-Bold="true" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                
					        <asp:ValidationSummary ID="MyValidationSummary" runat="server"
                       ShowMessageBox="true"
                       ShowSummary="false" />
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Suffix:</font></td>
      <td>
                <asp:TextBox ID="db_suffix" runat="server" size="5" maxlength="8" Text="suffix" onchange="Edited();"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Title:</font></td>
      <td>
                <asp:TextBox ID="db_title" runat="server" size="27" maxlength="50" Text="title" onchange="Edited();"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Phone:&nbsp;</font></td>
      <td>
                <asp:TextBox ID="db_phone" runat="server" size="27" maxlength="27" Text="phone" onchange="Edited();"></asp:TextBox>
      <font face="Verdana" size="2">Ext:&nbsp;</font><asp:TextBox ID="db_phoneext" runat="server" size="5" maxlength="5" Text="phoneext" onchange="Edited();"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Fax:&nbsp;</font></td>
      <td>
                <asp:TextBox ID="db_fax" runat="server" size="27" maxlength="27" Text="fax" onchange="Edited();"></asp:TextBox>
      </td>
    </tr>

    <tr>
      <td ALIGN="right" nowrap><font face="Verdana" size="2">Email:&nbsp;</font></td>
      <td>
                <asp:TextBox ID="db_email" runat="server" size="35" maxlength="100" Text="email" onchange="Edited();"></asp:TextBox>
          <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="db_email" Display="Dynamic" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                <input type="button"  value="Confirmation" runat="server" id="ButtonConfirmation" />
                <input type="button"  value="Edit Template" runat="server" id="ButtonEditEmailTemplate" />
      </td>
    </tr>
    <tr>
      <td ALIGN="right" nowrap><b><font FACE="Verdana" SIZE="2">&nbsp;</font></b></td>
      <td>&nbsp;
      
			 	<asp:Button ID="ButtonSave" runat="server" Text="Save" CLASS="Hotcell2" onclientclick="ClearEdited();" />
   			   	<input TYPE="button" VALUE="Cancel" CLASS="Hotcell2" NAME="cancel" onclick="window.close();">

			
      </td>
    </tr>
  </table>


    </form>
</body>
</html>