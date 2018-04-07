<%@ Page Title="E-File Cleaned Student Data" Language="VB" AutoEventWireup="false" CodeFile="efilecleanstudentdata.aspx.vb" Inherits="admin_efilecleanstudentdata" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cleaned Student Data</title>
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveClientUrl("~/")%>common/style2005.css">
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/util/hoverobject.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/fncVerifySave.js"></script>
    <script language = "javascript" src ="<%= Page.ResolveClientUrl("~/")%>common/SCSGlobal.js"></script>   

</head>
<body text="#000000" style="margin:0px; padding:0px; background-color:#ffffff">

    <form id="frm" runat="server">
    <h3>Cleaned Student Data</h3>
<table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 
    <div style="margin:5px; vertical-align:middle">

    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>'));"><img id="Img2" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
</div>

            <asp:GridView ID="GridViewList" runat="server" 
        AllowPaging="true" AllowSorting="true" 
             AutoGenerateColumns="true"
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
            
            
        </Columns>
        </asp:GridView>

        <p></p>
            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />
            </td>
        </tr>
    </table>

    </form>
</body>
</html>

