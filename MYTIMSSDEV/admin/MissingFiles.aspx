<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="MissingFiles.aspx.vb" Inherits="admin_MissingFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 

    <div style="margin:5px; vertical-align:middle">
    
    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
</div>
        <b>
        <asp:Label ID="LabelMissingEfiles" runat="server" Text=""></asp:Label>
        Missing E-File Documents</b>
        <asp:Repeater ID="RepeaterEfileList" runat="server">
                <HeaderTemplate>
			        <table border="1" class="sortable" id="school_list_tb" cellpadding="3" cellspacing="5">
				        <tr>
				        <td align="center"><B>Server File Path</B></td>
				        <td align="center"><B>Upload DT</B></td>
				        </tr>
                </HeaderTemplate>
                        <ItemTemplate>                    
				      <tr>               
				      <td><%#Container.DataItem("ServerFilePath")%></td>
				      <td><%#Container.DataItem("UploadDT")%></td>               
				      </tr>
                        </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
                    </asp:Repeater>
                    <b>
        <asp:Label ID="LabelMissingSTLF" runat="server" Text=""></asp:Label>
                    Missing STLF Documents</b>
        <asp:Repeater ID="RepeaterSTLFList" runat="server">
                <HeaderTemplate>
			        <table border="1" class="sortable" id="school_list_tb" cellpadding="3" cellspacing="5">
				        <tr>
				        <td align="center"><B>STLF Document</B></td>
				        </tr>
                </HeaderTemplate>
                        <ItemTemplate>                    
				      <tr>               
				      <td><%#Container.DataItem("STLFDocument")%></td>          
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

