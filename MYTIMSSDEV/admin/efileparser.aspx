<%@ Page Title="E-File Parser" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="efileparser.aspx.vb" Inherits="admin_efileparser" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script>

    function toggleSelected() {
        //alert('asdfsdaf');
        var inputs = document.getElementsByTagName("input");
        var suffix = '$CheckBoxDoWork';
        //var arr = new Array();
        var arr = [];  // empty array
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].name.indexOf(suffix, this.length - suffix.length) !== -1) {
                inputs[i].checked = !inputs[i].checked;
            }
        }
    }

  </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
<table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 
    <div style="margin:5px; vertical-align:middle">

    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?dps=<%=Me.DropDownListDPStatus.SelectedValue %>&g=<%=Me.DropDownListGrade.SelectedValue %>&sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>'));"><img id="Img2" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
    -
                        Grade:
                    <asp:DropDownList ID="DropDownListGrade" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                     
                    </asp:DropDownList>
                    -
                        DP Status:
                    <asp:DropDownList ID="DropDownListDPStatus" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="">All</asp:ListItem>
                        <asp:ListItem Value="Ready For Parsing" Selected="True">Ready For Parsing</asp:ListItem>
                        <asp:ListItem Value="Ready for OFF-LINE">Ready for OFF-LINE</asp:ListItem>
                    </asp:DropDownList>
                    -
                    <a href="javascript:toggleSelected();">Select All:</a>
                    
            <asp:Button ID="ButtonDoWork" runat="server" Text="Do work..." />

                    
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
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxDoWork" runat="server" />           
                                        <asp:HiddenField ID="fileid" runat="server" Value='<%#Eval("fileid")%>' />      
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="javascript:openAWindow('efilecleanstudentdata.aspx?fileid=<%# Eval("FileId")%>', 'mywin', 1000, 400,1)" title="View Parsed Data">View</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UploadDT"  SortExpression="UploadDT">
                <ItemTemplate>
                    <a href='<%# Page.ResolveClientUrl("~/") %>SubmitStudentList.aspx?s=<%# Eval("frame_n_")%>&f=<%# Eval("FileId")%>' target="_blank" title="Launch E-File Process.."><%# Eval("UploadDT")%></a></b>
                </ItemTemplate>
            </asp:TemplateField>                                    
            <asp:BoundField HeaderText="District" 
                  DataField="D_Name" SortExpression="D_Name">
                  </asp:BoundField>  
                   
            <asp:BoundField HeaderText="School" 
                  DataField="S_Name" SortExpression="S_Name">
                  </asp:BoundField>  

            <asp:BoundField HeaderText="SmpGrd" 
                  DataField="SmpGrd" SortExpression="SmpGrd">
                  </asp:BoundField>   
            
            <asp:BoundField HeaderText="Frame_N_" 
                  DataField="Frame_N_" SortExpression="Frame_N_">
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

            <asp:BoundField HeaderText="TotalRows" 
                  DataField="TotalRows" SortExpression="TotalRows">
                  </asp:BoundField>   
                  
            <asp:TemplateField HeaderText="Filename"  SortExpression="UserFilePath">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonViewEfile" runat="server" CommandName="DownloadEfile" CommandArgument='<%# Eval("FileId")%>' ToolTip='<%# Eval("UserFilePath")%>'><%# Eval("UserFilePath")%></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
                               
            <asp:BoundField HeaderText="Uploaded By" 
                  DataField="Fullname" SortExpression="Fullname">
                  </asp:BoundField>  
                                    
            <asp:TemplateField HeaderText="E-File Status"  SortExpression="EfileStatusCode">
                <ItemTemplate>                
                    <a href="javascript:openAWindow('EfileStatusHistory.aspx?fileid=<%# Eval("fileid")%>&type=E-File', 'mywin', 1000, 400,1)" title="View Status History"><img src="<%# Page.ResolveClientUrl("~/") %>common/images/buttons/note.gif" alt="View Status History" border="0" /></a></b>
                    <img src="<%# Page.ResolveClientUrl("~/") %>common/images/tooltip/Info.png" width="15" title='Modified By <%# Eval("EfileStatusFullname")%> on <%# Eval("EfileStatusEditDT")%>' alt='Modified By <%# Eval("EfileStatusFullname")%> on <%# Eval("EfileStatusEditDT")%>' />
                    <%# Eval("EfileStatusCode")%>
                </ItemTemplate>
            </asp:TemplateField> 
                                    
            <asp:TemplateField HeaderText="DP Status"  SortExpression="DPStatusCode">
                <ItemTemplate>
                    <a href="javascript:openAWindow('EfileStatusHistory.aspx?fileid=<%# Eval("fileid")%>&type=DP', 'mywin', 1000, 400,1)" title="View Status History"><img src="<%# Page.ResolveClientUrl("~/") %>common/images/buttons/note.gif" alt="View Status History" border="0" /></a></b>
                    <img src="<%# Page.ResolveClientUrl("~/") %>common/images/tooltip/Info.png" width="15" title='Modified By <%# Eval("DPStatusFullname")%> on <%# Eval("DPStatusEditDT")%>' alt='Modified By <%# Eval("DPStatusFullname")%> on <%# Eval("DPStatusEditDT")%>' />
                    <%# Eval("DPStatusCode")%>
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

