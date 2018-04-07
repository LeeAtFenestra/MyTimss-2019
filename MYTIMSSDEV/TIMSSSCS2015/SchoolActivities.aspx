<%@ Page Title="School Activities" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="SchoolActivities.aspx.vb" Inherits="SchoolActivities" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
	               <div style="margin:5px; vertical-align:middle">
                   <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td><a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&t=<%=Me.DropDownListTerritory.SelectedValue %>&s=<%=Me.DropDownListREPS.SelectedValue %>&st=<%=Me.DropDownListSchoolType.SelectedValue %>>&r=<%=Me.DropDownListRegion.SelectedValue %>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>&al=<%=Me.CurrentAlphaBarLetter() %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>
                        
                        Territory:
                    <asp:DropDownList ID="DropDownListTerritory" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                        REPSBGRP:
                    <asp:DropDownList ID="DropDownListREPS" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                        School Type:
                    <asp:DropDownList ID="DropDownListSchoolType" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    Region:
                    <asp:DropDownList ID="DropDownListRegion" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                        </td>
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
        <asp:GridView ID="GridViewSchoolList" runat="server" 
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
            <asp:TemplateField HeaderText="School Name" SortExpression="S_Name">
                <ItemTemplate>
                <a title = 'View' href = "SchoolEdit.aspx?id=<%#Container.DataItem("id")%>"><%#Container.DataItem("S_Name")%></a>&nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="School ID" 
                  DataField="id" SortExpression="id">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Project Name" 
                  DataField="project" SortExpression="project">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Status" 
                  DataField="DispName" SortExpression="DispName">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Region" 
                  DataField="testregion" SortExpression="testregion">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Area" 
                  DataField="fldAreaCode" SortExpression="fldAreaCode">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="Assessment date/time" SortExpression="ScheDate">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ScheDate","{0:M/d/yyyy}")%> <%#Container.DataItem("ScheTime")%></ItemTemplate>
            </asp:TemplateField>
      <%--      <asp:TemplateField HeaderText="Assessment 2 date/time" SortExpression="ScheDate2">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ScheDate2", "{0:M/d/yyyy}")%> <%#Container.DataItem("ScheTime2")%></ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField HeaderText="Territory" 
                  DataField="fldTerritoryCode" SortExpression="fldTerritoryCode">
                  </asp:BoundField>
            <asp:BoundField HeaderText="State" 
                  DataField="S_State" SortExpression="S_State">
                  </asp:BoundField>
            <asp:BoundField HeaderText="School Type" 
                  DataField="SchoolType">
                  </asp:BoundField>
            <asp:BoundField HeaderText="REPSBGRP" 
                  DataField="REPSBGRP" SortExpression="REPSBGRP">
                  </asp:BoundField>
            <asp:BoundField HeaderText="County" 
                  DataField="S_County" SortExpression="S_County">
                  </asp:BoundField>

            <asp:BoundField HeaderText="Registration ID" 
                  DataField="MyNAEPREGID" SortExpression="MyNAEPREGID">
            </asp:BoundField>

            <asp:BoundField HeaderText="District Name" 
                  DataField="D_Name" SortExpression="D_Name">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="STLF Uploaded" SortExpression="STLFUploaded">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonViewFileSTLF" runat="server" CommandName="Downloadfile" CommandArgument='<%#Container.DataItem("id")%>'  ToolTip="Click here to download" Enabled='<%#HandleSTLFDocumentAccess(Container.DataItem("STLFUploaded"))%>'><%#Container.DataItem("STLFUploadedText")%></asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>



</td></tr><tr><td bgcolor = "#FFFFFF">

            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />
</td></tr></table>

</asp:Content>

