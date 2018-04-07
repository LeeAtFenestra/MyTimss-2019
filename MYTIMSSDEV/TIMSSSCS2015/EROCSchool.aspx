<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="EROCSchool.aspx.vb" Inherits="EROCSchool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>School EROC</title>
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveClientUrl("~/")%>common/style2005.css">
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/util/hoverobject.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/fncVerifySave.js"></script>
    <script language = "javascript" src="<%= Page.ResolveClientUrl("~/")%>common/SCSGlobal.js"></script>

    

  <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-ui.css" />

  <script src="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-1.10.2.js"></script>

  <script src="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-ui.js"></script>
  
  <script>

      $(function () {

          //$("#<%=db_DateContacted.ClientID%>").datepicker({ minDate: new Date(2014, 7 - 1, 4), maxDate: new Date(2014, 7 - 1, 25) });
          $("#<%=db_DateContacted.ClientID%>").datepicker();


      });

  </script>
</head>
<body text="#000000" style="margin:0px; padding:0px; background-color:#ffffff">
<script language="javascript">
    //<-- hide this script from non-javascript-enabled browsers
    var imgEdit = new hoverbutton('edit', "<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif", "<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif", "<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif", true);
    //-->		
</script>
    <form id="frm" runat="server">
<input type="hidden" name="edited" value="0">
	<input type="hidden"  name="reader" value = "False" size="8">

    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
                
<center><FONT size =5 color=darkorchid face=cursive><U>eTIMSS/ICILS - School EROC </U></FONT> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;        <img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15"></center>
<font class="dataheaderlarge">
	<b><u>School: <asp:Label ID="SchoolName" runat="server"></asp:Label></u></b><br />
	<b><u>School ID: <asp:Label ID="SchoolID" runat="server"></asp:Label></u></b><br />
	<b><u>Study: <asp:Label ID="Study" runat="server"></asp:Label></u></b><br />
</font>
<asp:HiddenField ID="db_frame_n_" runat="server" />
<asp:HiddenField ID="db_id" runat="server" />
<asp:HiddenField ID="db_fldProjectID" runat="server" />

<table border="0" class='tbform'>
	<tr>
		<td><b>Person Contact Title:</b></td>
		<td><asp:TextBox ID="db_PersonContactTitle" runat="server" size="20" maxlength="50" Text="" onchange="Edited();"></asp:TextBox></td>
	</tr>
	<tr>
		<td><b>Person Contacted:</b></td>
		<td><asp:TextBox ID="db_PersonContacted" runat="server" size="20" maxlength="50" Text="" onchange="Edited();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatordb_PersonContacted" runat="server" 
                                ErrorMessage="Please enter contact person's name." ControlToValidate="db_PersonContacted" ForeColor="Red" Font-Bold="true" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                
					        <asp:ValidationSummary ID="MyValidationSummary" runat="server"
                       ShowMessageBox="true"
                       ShowSummary="false" /></td>
	</tr>
	<tr>
		<td><b>Date Contacted:</b></td>
		<td><asp:TextBox ID="db_DateContacted" runat="server" size="16" maxlength="20" Text="" onchange="datecheck(this, 'Date Contacted');Edited();"></asp:TextBox> (mm/dd/yyyy)
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatordb_DateContacted" runat="server" 
                                ErrorMessage="Please enter contact date." ControlToValidate="db_DateContacted" ForeColor="Red" Font-Bold="true" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
	</tr>
	<tr>
		<td><b>Contact Mode:</b></td>
		<td><asp:DropDownList ID="db_ContactMode" runat="server" onchange = "Edited();" DataTextField="Name" DataValueField="Value">
            <asp:ListItem value="Telephone">Telephone</asp:ListItem>
            <asp:ListItem value="Email">Email</asp:ListItem>
            <asp:ListItem value="Mail">Mail</asp:ListItem>            
        </asp:DropDownList></td>
	</tr>
	<tr>
		<td><b>Outcome of the Call:</b></td>
		<td><asp:TextBox ID="db_OutcomeOfTheCall" runat="server" size="55" maxlength="80" Text="" onkeyup="Edited();SetLimitToTextArea(this,80, 'db_OutcomeOfTheCallSpan')" onKeyPress="return maxLength(this,'80');" onpaste="return maxLengthPaste(this,'80');"></asp:TextBox> (Maximum 80 Characters) <span id='db_OutcomeOfTheCallSpan'></span>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatordb_OutcomeOfTheCall" runat="server" 
                                ErrorMessage="Please enter outcome of call." ControlToValidate="db_OutcomeOfTheCall" ForeColor="Red" Font-Bold="true" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
	</tr>


	<tr>
		<td><b>Disposition Code:</b></td>
		<td>
            <asp:DropDownList ID="db_Disp" runat="server" onchange="Edited();" DataTextField="Name" DataValueField="Value" Enabled="false">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td valign="top"><b>Additional Notes:</b></td>
        <td>
            <asp:TextBox ID="db_AdditionalNotes" runat="server" TextMode="MultiLine" Rows="2" Columns="60" maxlength="1000" Text="" onkeyup="Edited();SetLimitToTextArea(this,1000, 'db_AdditionalNotesspan')" onKeyPress="return maxLength(this,'1000');" onpaste="return maxLengthPaste(this,'1000');"></asp:TextBox>
            <span id='db_AdditionalNotesspan'></span>
         </td>
	</tr>
</table>
<center>
                <asp:Button ID="ButtonSave" runat="server" Text="Submit" onclientclick="ClearEdited();" />
	<input type="Reset"  value="Reset" onclick="ClearEdited();" /> 
	<input type="button"  value="Exit" onclick="window.close();" /> 
	<input type="button" value="Print Page" class="dataheader" onclick="window.print();" />
    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>    
</center>
<font class="dataheaderRed"><b>Record of Calls:</b></font>
<table border="1" width="100%">
	<tr class="dataheader">
        <th>Date</th>
        <th>Person Contacted</th>
        <th>Contact Mode</th>
        <th>Outcome</th>
        <th>Disposition Code</th>
        <th>Notes</th>
        <th>Updated by</th>
    </tr>
                        <asp:Repeater ID="RepeaterEROCList" runat="server">
                <HeaderTemplate>


                </HeaderTemplate>
                <ItemTemplate>                    
<tr>	
        <td><%#DataBinder.Eval(Container.DataItem, "DateContacted","{0:MM/dd/yyyy}")%>&nbsp;</td>
        <td><%#Container.DataItem("PersonContactTitle")%> <%#Container.DataItem("PersonContacted")%>&nbsp;</td>
        <td><%#Container.DataItem("ContactMode")%>&nbsp;</td>
        <td><%#Container.DataItem("OutcomeOfTheCall")%>&nbsp;</td>
        <td><%#Container.DataItem("DispName")%>&nbsp;</td>
        <td><%#Container.DataItem("AdditionalNotes")%>&nbsp;</th>
        <td><%#Container.DataItem("Updatedby")%>&nbsp;</td>
</tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
</table>

            </td>
        </tr>
    </table>


    </form>
</body>
</html>


