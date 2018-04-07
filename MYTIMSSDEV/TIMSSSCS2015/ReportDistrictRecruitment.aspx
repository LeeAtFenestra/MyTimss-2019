<%@ Page Title="District Recruitment" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="ReportDistrictRecruitment.aspx.vb" Inherits="TIMSSSCS2015_ReportDistrictRecruitment" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopNavigationLevel2" Runat="Server">

  <TR>
    <TD height=29 width=4></TD>
    <TD>
      <TABLE cellSpacing=0 cellPadding=0 width="100%" bgColor=#2a3985 
        border=0><TBODY>
        <TR>
          <TD colSpan=2>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TBODY>
              <TR>
                <TD height=28 noWrap><FONT size=2>&nbsp;<FONT color=#ffffff> 
                <FONT color=#ffffff><b>Reports:</b></FONT>
            <a href="ReportSchoolRecruitment.aspx" class="menu" title="School Recruitment">School Recruitment</a>
                  |
            <a href="ReportDistrictRecruitment.aspx" class="menuOn" title="District Recruitment">District Recruitment</a>
                  |
            <a href="ReportSchoolCompletion.aspx" class="menu" title="School Completion">School Completion</a>
                  
                  |
            <a href="ReportAssessmentDay.aspx" class="menu" title="Assessment Day">Assessment Day</a>
                  |
            <a href="ReportQuestionnaireStatus.aspx" class="menu" title="Questionnaire Status Report">Questionnaire Status Report</a>      
                  |
            <a href="ReportSchoolDetail.aspx" class="menu" title="School Detail">School Detail</a>
                  
                  </TD></TR></TBODY></TABLE>
                  </TD></TR></TBODY></TABLE>
      </TD>
    <TD height=29 width=4></TD></TR>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
<table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
    <div style="margin:5px; vertical-align:middle">
    
                       <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        
                                                <td>
                    <div style="text-align:left">
                    &nbsp;
                    &nbsp;
                        <b><font color="#000000">School Type:</font></b>
                    <asp:DropDownList ID="DropDownListSchoolType" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;
                    &nbsp;
                        <b><font color="#000000">Filter:</font></b>
                    <asp:DropDownList ID="DropDownListFilter" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;       
                    &nbsp;       
                    <b><font color="#000000">Export:</font></b>
                    <asp:DropDownList ID="DropDownListExport" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                         
<asp:ImageButton ID="ImageButtonXportToExcel" runat="server" ImageUrl="~/common/images/Excel.jpg" Width="20" Height="20" ToolTip="Excel Export" />
                    </div>
<!--
&nbsp;
                        <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?showall=1&st=<%=Me.DropDownListSchoolType.SelectedValue %>&f=<%=Me.DropDownListFilter.SelectedValue %>&sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>
                        -->
                        </td>
                        <td align="right">
                        <!--
            <asp:Repeater ID="RepeaterFirstLetter" runat="server">
                <HeaderTemplate>
			        <table border="1">
				        <tr>
                </HeaderTemplate>
                <ItemTemplate>       
                    <td nowrap width="15" align="center" class='<%# Me.AphaBarCSS(Eval("Value")) %>' style="padding-top:7px;padding-bottom:7px;"><asp:LinkButton ID="LinkButtonLetter" runat="server" CommandArgument='<%#Eval("Value")%>' CommandName="filterbyfirstletter" CssClass='<%# Me.AphaBarLinkCSS(Eval("Value")) %>' ToolTip='<%# Me.AphaBarLinkTooltip(Eval("Value")) %>'><%#Eval("Name")%></asp:LinkButton></td>
                </ItemTemplate>
                <FooterTemplate>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            -->
            </td>
                    </tr>
                   </table>
</div>
        
            <asp:GridView ID="GridViewDistrictList" runat="server" 
        AllowPaging="true" AllowSorting="true" 
        AutoGenerateColumns="false" 
        CssClass="celldata" bordercolor="#EBEBEB" 
        Width="100%" rules="rows" PagerSettings-Mode="NextPrevious"  PagerSettings-NextPageText=" Next Page >>" PagerSettings-PreviousPageText="<< Previous Page ">
        <HeaderStyle CssClass="fldheader"></HeaderStyle>
        
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                <ItemTemplate><b><%# Container.DataItemIndex + 1 %>.</b></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="District Name" 
                  DataField="D_Name" SortExpression="D_Name">
                  </asp:BoundField>
            <asp:BoundField HeaderText="LEAID" 
                  DataField="LEAID" SortExpression="LEAID">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Address1" 
                  DataField="D_Addr1" SortExpression="D_Addr1">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Address2" 
                  DataField="D_Addr2" SortExpression="D_Addr2">
                  </asp:BoundField>
            <asp:BoundField HeaderText="City" 
                  DataField="D_City" SortExpression="D_City">
                  </asp:BoundField>
            <asp:BoundField HeaderText="State" 
                  DataField="D_State" SortExpression="D_State">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Zip" 
                  DataField="D_Zip" SortExpression="D_Zip">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Phone" 
                  DataField="D_Phone" SortExpression="D_Phone">
                  </asp:BoundField>

            <asp:BoundField HeaderText="District Supt Prefix" 
                  DataField="D_SuperPrefix" SortExpression="D_SuperPrefix">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt Fname" 
                  DataField="D_SuperFname" SortExpression="D_SuperFname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt Lname" 
                  DataField="D_SuperLname" SortExpression="D_SuperLname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt Suffix" 
                  DataField="D_SuperSuffix" SortExpression="D_SuperSuffix">
                  </asp:BoundField>                  
            <asp:BoundField HeaderText="District Supt Address 1" 
                  DataField="D_SuperAddr1" SortExpression="D_SuperAddr1">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt Address 2" 
                  DataField="D_SuperAddr2" SortExpression="D_SuperAddr2">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt City" 
                  DataField="D_SuperCity" SortExpression="D_SuperCity">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt State" 
                  DataField="D_SuperState" SortExpression="D_SuperState">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Supt Zip" 
                  DataField="D_SuperZip" SortExpression="D_SuperZip">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="District Supt Phone" SortExpression="D_SuperPhone">
                <ItemTemplate><%#Container.DataItem("D_SuperPhone")%> <%#Container.DataItem("D_SuperPhoneext")%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="District Supt Email" 
                  DataField="D_SuperEmail" SortExpression="D_SuperEmail">
                  </asp:BoundField>
                  
            <asp:BoundField HeaderText="District Test Director Prefix" 
                  DataField="D_TdPrefix" SortExpression="D_TdPrefix">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director Fname" 
                  DataField="D_TdFname" SortExpression="D_TdFname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director Lname" 
                  DataField="D_TdLname" SortExpression="D_TdLname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director Suffix" 
                  DataField="D_TdSuffix" SortExpression="D_TdSuffix">
                  </asp:BoundField>                  
            <asp:BoundField HeaderText="District Test Director Address 1" 
                  DataField="D_TdAddr1" SortExpression="D_TdAddr1">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director Address 2" 
                  DataField="D_TdAddr2" SortExpression="D_TdAddr2">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director City" 
                  DataField="D_TdCity" SortExpression="D_TdCity">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director State" 
                  DataField="D_TdState" SortExpression="D_TdState">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Test Director Zip" 
                  DataField="D_TdZip" SortExpression="D_TdZip">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="District Test Director Phone" SortExpression="D_TdPhone">
                <ItemTemplate><%#Container.DataItem("D_TdPhone")%> <%#Container.DataItem("D_TdPhoneext")%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="District Test Director Email" 
                  DataField="D_TdEmail" SortExpression="D_TdEmail">
                  </asp:BoundField>
        </Columns>
        </asp:GridView>



</td></tr><tr><td bgcolor = "#FFFFFF">
            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />

</td></tr></table>
</asp:Content>

