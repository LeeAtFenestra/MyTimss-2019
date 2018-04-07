<%@ Page Title="Submitted Class Lists" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="ClassList.aspx.vb" Inherits="admin_ClassList" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
<table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 
    <div style="margin:5px; vertical-align:middle">

    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?&g=<%=Me.DropDownListGrade.SelectedValue %>&sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>'));"><img id="Img2" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>

                        Grade:
                    <asp:DropDownList ID="DropDownListGrade" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    
</div>

            <asp:GridView ID="GridViewList" runat="server" 
        AllowPaging="true" AllowSorting="true" 
             AutoGenerateColumns="false"
        CssClass="celldata" bordercolor="#EBEBEB" 
        Width="100%" rules="rows" PagerSettings-Mode="NextPrevious"  PagerSettings-NextPageText=" Next Page >>" PagerSettings-PreviousPageText="<< Previous Page ">
        <HeaderStyle CssClass="fldheader"></HeaderStyle>
        <RowStyle VerticalAlign="Top" />
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <b><%# Container.DataItemIndex + 1 %>.</b>
                </ItemTemplate>
            </asp:TemplateField>
                                                
            <asp:BoundField HeaderText="School" 
                  DataField="S_Name" SortExpression="S_Name">
                  </asp:BoundField>  

            <asp:BoundField HeaderText="District" 
                  DataField="D_Name" SortExpression="D_Name">
                  </asp:BoundField>  
                   

            <asp:BoundField HeaderText="SmpGrd" 
                  DataField="SmpGrd" SortExpression="SmpGrd">
                  </asp:BoundField>   
                  
            <asp:BoundField HeaderText="LEAID" 
                  DataField="LEAID" SortExpression="LEAID">
                  </asp:BoundField> 

            <asp:BoundField HeaderText="ID" 
                  DataField="ID" SortExpression="ID">
                  </asp:BoundField> 
                  
                  
            <asp:TemplateField HeaderText="Project">
                <ItemTemplate><%#TimssBll.ProjectName(Container.DataItem("ID"))%></ItemTemplate>
            </asp:TemplateField>
                   
            <asp:TemplateField HeaderText="Class List Submitted"  SortExpression="ClassListSubmited">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButtonClassListLocked" runat="server"  ImageUrl="~/common/images/buttons/keys.gif" BorderWidth="0" CommandName="unlockclasslistedit" CommandArgument='<%# Eval("ID")%>' OnClientClick="return confirm('Are you sure you want to unlock class list edited?\n\nThis will disable efiling until the class list is resubmitted!');" visible='<%# iif(Eval("ClassListSubmited") is dbnull.value, false, true)%>'  />
                    <%# Eval("ClassListSubmited")%>
                </ItemTemplate>
            </asp:TemplateField>             

            <asp:TemplateField HeaderText="Class List Submitted By">
                <ItemTemplate>
                    <%# Eval("ClassListSubmitedByFirstName") & " " & Eval("ClassListSubmitedByLastName")%>
                </ItemTemplate>
            </asp:TemplateField> 
        </Columns>
        </asp:GridView>

        <p></p>
            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>


