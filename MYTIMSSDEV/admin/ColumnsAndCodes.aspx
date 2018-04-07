<%@ Page Title="E-File Column and Code List" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="ColumnsAndCodes.aspx.vb" Inherits="admin_ColumnsAndCodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">

    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 
            
    <div style="margin:5px; vertical-align:middle">

    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>'));"><img id="Img2" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
</div>

            <asp:Repeater ID="RepeaterList" runat="server">
                <HeaderTemplate>
                <b>E-File Column and Code List</b>
                
                        		        <table border="1" cellpadding="4" cellspacing="0" style="width:600px">
                                        <!--
				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>&nbsp;</B></Font></td>
				        <td id="Td1" align="center" runat="server"><Font size=2><B>Code Label</B></Font></td>
				        <td id="Td13" align="center" runat="server"><Font size=2><B>Code Value</B></Font></td>                        

				        </tr>
                        -->
                </HeaderTemplate>
                <ItemTemplate>                      
				      <tr bgcolor="#cccccc" id="Tr1" runat="server" visible='<%# HandleVerifyColumnHeaderVisibility(Container.DataItem("NaepLabel")) %>'>
				      <td><Font size=2><B><u><%#Container.DataItem("NaepLabel")%></u> Code Labels</B></Font></td>
				        <td nowrap><Font size=2><B>Code Values</B></Font></td>     
					  </tr>             
				      <tr>
				      <td>&nbsp;&nbsp;&nbsp;<%#Container.DataItem("CodeLabel")%></td>
				      <td><%#Container.DataItem("CodeValue")%></td>
					  </tr>
                </ItemTemplate>
                
                <FooterTemplate>           
        </table>
                </FooterTemplate>
            </asp:Repeater> 
            </td>
        </tr>
    </table>
</asp:Content>

