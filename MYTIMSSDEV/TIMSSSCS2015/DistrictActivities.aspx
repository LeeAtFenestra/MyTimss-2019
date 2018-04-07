<%@ Page Title="District Activities" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="DistrictActivities.aspx.vb" Inherits="DistrictActivities" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
    <div style="margin:5px; vertical-align:middle">
    
                       <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td><a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>&al=<%=Me.CurrentAlphaBarLetter() %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a></td>
                        <td align="right">
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
            </asp:Repeater></td>
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
                <ItemTemplate>
                    <b><%# Container.DataItemIndex + 1 %>.</b>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="District Name" SortExpression="D_Name">
                <ItemTemplate>
                <a title = 'View' href = "DistrictEdit.aspx?d=<%#Container.DataItem("leaid")%>"><%#Container.DataItem("D_Name")%></a>&nbsp;</font>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Superintendent Name" 
                  DataField="D_Super" SortExpression="D_Super">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Test Director" 
                  DataField="td_Name" SortExpression="td_Name">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Other Contact" 
                  DataField="D_Contact" SortExpression="D_Contact">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District City" 
                  DataField="D_City" SortExpression="D_City">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District State" 
                  DataField="D_State" SortExpression="D_State">
                  </asp:BoundField>

        </Columns>
        </asp:GridView>



</td></tr><tr><td bgcolor = "#FFFFFF">
            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />

</td></tr></table>
</asp:Content>

