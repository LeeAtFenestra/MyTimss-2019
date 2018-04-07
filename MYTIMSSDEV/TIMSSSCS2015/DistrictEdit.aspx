<%@ Page Title="District Information Screen" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="DistrictEdit.aspx.vb" Inherits="DistrictEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript">
    //<-- hide this script from non-javascript-enabled browsers
    var imgEdit = new hoverbutton('edit', "<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif", "<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif", "<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif", true);
    //-->		
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphTopNavigationLevel2" Runat="Server">
    <TR>
    <TD height=29 width=4></TD>
    <TD class=hotcell>
      <table border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
          <td><b><font size="2">&nbsp;District Edit&nbsp;</font></b></td>
          
	          <td width="70%"><asp:Button ID="ButtonSave" runat="server" Text="Save" class="hotcell2" onclientclick="ClearEdited();" /><input type="reset"  value="Reset" name="B1" onclick="ClearEdited();" class="hotcell2">
              <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&print=1'));"><img src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>
              </td>
	       
          <td align="right"><img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          
              

        </tr>
      </table>        
      </TD>
    <TD height=29 width=4></TD>
  </TR>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" Runat="Server">
					        <asp:ValidationSummary ID="MyValidationSummary" runat="server"
                       ShowMessageBox="true"
                       ShowSummary="false" />
<asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
<input type="hidden" name="edited" value = "0" />

<input type="hidden"  name="reader" value = 'False' size="8" />

    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">      
               <table border="1" cellspacing="0" cellpadding="0" height="336" width="865">
	<tr>
		<td valign="Top" width="411" height="308">
			<table width="560">

				<tr>
					
					<td colspan="2" height="20" width="403"><font size="2">
					      District included in New Schools from Small Districts
						</font>
					</td>
					
				</tr>

				<tr>
					<td height="25" width="195"><font size="2">Name:</font></td>
					<td height="25" width="355"><asp:TextBox ID="db_d_name" runat="server" size="35" MaxLength="60" onchange="Edited();"></asp:TextBox></td>
				</tr>
				<tr>
					<td height="25" width="195"><font size="2">District ID:</font></td>
					<td height="25" width="355"><asp:Label ID="TIMSSID" runat="server"></asp:Label></td>
				</tr>
                
				<tr>
					<td height="25" width="195"><font size="2">Address:</font></td>
					<td height="25" width="355"><asp:TextBox ID="db_d_addr1" runat="server" size="35" MaxLength="30" onchange="Edited();"></asp:TextBox></td>
				</tr>
				<tr>
					<td height="25" width="195"><font size="2">Address (cont'd):</font></td>
					<td height="25" width="355"><asp:TextBox ID="db_d_addr2" runat="server" size="35" MaxLength="30" onchange="Edited();"></asp:TextBox></td>
				</tr>
				<tr>
					<td valign="top" height="25" width="195"><font size="2">City:, State: Zip:</font></td>
					<td valign="top" height="25" width="355"><asp:TextBox ID="db_d_city" runat="server" size="21" MaxLength="30" onchange="Edited();"></asp:TextBox>, 
                        <asp:TextBox ID="db_d_state" runat="server" size="2" MaxLength="2" onchange="Edited();"></asp:TextBox>
                        <asp:TextBox ID="db_d_zip" runat="server" size="10" MaxLength="10" onchange="Edited();"></asp:TextBox>
				</tr>
				<tr>
					<td height="25" width="195"><font size="2">Phone:</font></td>
					<td height="25" width="355"><asp:TextBox ID="db_d_phone" runat="server" size="14" MaxLength="27" onchange="Edited();"></asp:TextBox>
					 <font size="2">(XXX) XXX-XXXX</font>
                            <asp:RegularExpressionValidator ID="RegularExpressiondb_d_phone" 
                                                  runat="server" 
                                                  ControlToValidate="db_d_phone"
                                                  Display="None" 
                                                  ErrorMessage="Invalid phone number" 
                                                  SetFocusOnError="True" 
                                                  ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator></td>
				</tr>
				<tr>
					<td height="25" width="195"><font size="2">Fax:</font></td>
					<td height="25" width="355"><asp:TextBox ID="db_d_fax" runat="server" size="14" MaxLength="27" onchange="Edited();"></asp:TextBox>
					 <font size="2">(XXX) XXX-XXXX</font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatordb_d_fax" 
                                                  runat="server" 
                                                  ControlToValidate="db_d_fax"
                                                  Display="None" 
                                                  ErrorMessage="Invalid fax number" 
                                                  SetFocusOnError="True" 
                                                  ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator></td>
				</tr>
				<tr>
					<td height="20" width="195"><font size="2">Superintendent:</font></td>
					<td height="20" width="355">
					 
                        <asp:DropDownList ID="db_d_super" runat="server" onchange = "Edited();" DataTextField="fullname" DataValueField="pid">
                        </asp:DropDownList>
							<a href="#" runat="server" id="HrefSuperintendent">Add</a>
					
					</td>
				</tr>
				<tr>
					<td height="20" width="195"><font size="2">Test Director:</font></td>
					<td height="20" width="355">
					
                        <asp:DropDownList ID="db_d_tDirector" runat="server" onchange = "Edited();" DataTextField="fullname" DataValueField="pid">
                        </asp:DropDownList>
							<a href="#" runat="server" id="HrefTestDirector">Add</a>
					
					</td>
				</tr>
				<tr>
					<td height="20" width="195"><font size="2">District Other Contact:</font></td>
					<td height="20" width="355" nowrap>
					    
                        <asp:DropDownList ID="db_d_contact" runat="server" onchange = "Edited();" DataTextField="fullname" DataValueField="pid">
                        </asp:DropDownList>
							<a href="#" runat="server" id="HrefDistrictContact">Add</a>
									
				
				<tr>
					<td height="25" width="195"><font size="2">Date District Letter Sent:</font></td>
					<td height="25" width="355">

                        <asp:DropDownList ID="db_d_partltrsentdt" runat="server" onchange = "Edited();" DataTextField="Name" DataValueField="Value">
                        </asp:DropDownList>


					 </td>
				</tr>
                
				
	
			</table>
		</td>			
	
		<!-- BEGINNING OF SECOND COLUMN MATERIAL -->	
		<td valign="Top" width="514" height="308">
			<table width="400">
				<tr>
				
					<td colspan="2" width="400">
					<div id="db.d_comment.txt"><font size="2">Comments (Up to 1024 characters):<span id='d_commentsapn'></span></font>
					</div>
					</td>
				</tr>
				<tr>
					<td colspan="2" width="400">
    					<asp:TextBox ID="db_d_comment" runat="server" size="14" MaxLength="27"  rows="5"   Columns="36" onkeyup="Edited();SetLimitToTextArea(this,1024, 'd_commentsapn')" TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>

			</table>		
            <asp:Repeater ID="RepeaterSchoolList" runat="server">
                <HeaderTemplate>
			        <table border="1" class="sortable" id="school_list_tb">
				        <tr>
				        <td width=280 align="center"><Font size=2><B>School Name</B></Font></td>
				        <td width=100 align="center"><Font size=2><B>School ID</B></Font></td>
				        <td width=134 align="center"><Font size=2><B>Project</B></Font></td>
				        <td width=134 align="center"><Font size=2><B>Disposition Code</B></Font></td>
				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td nowrap align="center"><Font size=2>
					  <a href="SchoolEdit.aspx?id=<%#Container.DataItem("id")%>" ><%#Container.DataItem("s_name")%></a>
				       </Font></td>
				      <td width=100 align="Center"><Font size=2><%#Container.DataItem("id")%></Font></td>
				      <td width=134 align="Center" nowrap><Font size=2><%#Container.DataItem("project")%></Font></td>
					  <td width=134 align="Center"><Font size=2><%#Container.DataItem("dispname")%></Font></td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
                
		</td>	
	</tr>
</table>
            </td>
        </tr>
    </table>
<br />
      <table border="0" width="100%" cellspacing="4" cellpadding="0" class="hotcell" height="24" >
        <tr>
	          <td>&nbsp;<asp:Button ID="ButtonSave2" runat="server" Text="Save" class="hotcell2" onclientclick="ClearEdited();" /><input type="reset"  value="Reset" name="B1" onclick="ClearEdited();" class="hotcell2">
              <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&print=1'));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>
              </td>
        </tr>
      </table>  
</asp:Content>

