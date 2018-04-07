<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="Roles.aspx.vb" Inherits="admin_Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
    <div style="margin:5px; vertical-align:middle">
            <a href="roleedit.aspx" title="Create role"><img id="Img2" src="~/common/images/buttons/pencilplus.gif" runat="server" border="0" alt="Create role" style="margin-right:10px" />Create Role</a>
    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/") %>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
</div>

            <asp:GridView ID="GridViewList" runat="server" 
        CssClass="celldata" bordercolor="#EBEBEB" 
         AutoGenerateColumns="false"
        Width="100%" rules="rows" PagerSettings-Mode="NextPrevious"  PagerSettings-NextPageText=" Next Page >>" PagerSettings-PreviousPageText="<< Previous Page ">
        <HeaderStyle CssClass="fldheader"></HeaderStyle>
        
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <b><%# Container.DataItemIndex + 1 %>.</b>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField HeaderText="Role" 
                  DataField="!" SortExpression="!" >
                  </asp:BoundField>

            <asp:TemplateField HeaderText="Total Users Assigned">
                <ItemTemplate>                
                   <%# Roles.GetUsersInRole(Container.DataItem).Count %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

