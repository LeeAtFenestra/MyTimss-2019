<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="ManageDocuments.aspx.vb" Inherits="admin_ManageDocuments" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="tophead" Runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 
   


            <%--'AllowMultiple="True"--%>
                <table width="75%">
                    <tr>
                        <td>
                              Document 1: <asp:FileUpload ID="FileUpload_File1" runat="server" />
                              <br />
                              <br />
                              Document 2: <asp:FileUpload ID="FileUpload_File2" runat="server"/>
                              <br />
                              <br />
                               Document 3: <asp:FileUpload ID="FileUpload_File3" runat="server"  />
                              <br />
                              <br />
                               Document 4: <asp:FileUpload ID="FileUpload_File4" runat="server"  />
                              <br />
                              <br />
                               Document 5: <asp:FileUpload ID="FileUpload_File5" runat="server"  />
                        </td>
                        <td>Upload up to 5 documents at once.<br /> <br />   
                            The file path must match the following format with the type of document (STF, STLF, STF2, STLF2, TTF) first, an underscore second, and the 4-digit IEA ID at the end.
                            <br /><br /> See examples below:<br />
                            STF_1234.xlsx&nbsp;&nbsp;&nbsp;(or .xls)<br />
                            STLF_2341.zip<br />
                            STF2_1432.xlsx<br />
                            STLF2_3412.zip<br />
                            TTF_4321.zip
                        </td>
                    </tr>
                </table>
                

                        <p class="submitButton">
                            <asp:Button ID="ButtonUpload" runat="server" Text="Update" 
                                 ValidationGroup="upload"/>

                                 <asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                 </p>
                 <div style="margin:5px; vertical-align:middle">
    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?sortfld=<%=Me.GridViewSortColumn() %>&sortdir=<%=Me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>'));"><img id="Img2" src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
</div>
            <asp:GridView ID="GridViewAccountList" runat="server" 
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
                                <asp:HiddenField ID="Frame_N_" runat="server" Value='<%#Container.DataItem("Frame_N_")%>' />
                                <asp:HiddenField ID="id" runat="server" Value='<%#Container.DataItem("id")%>' />
                                <asp:HiddenField ID="iea_id" runat="server" Value='<%#Container.DataItem("iea_id")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField HeaderText="School" 
                  DataField="s_name" SortExpression="s_name">
                  </asp:BoundField> 
            
            <asp:BoundField HeaderText="ID" 
                  DataField="id" SortExpression="id">
                  </asp:BoundField> 

            <asp:BoundField HeaderText="IEA ID" 
                  DataField="iea_id" SortExpression="iea_id">
                  </asp:BoundField> 
                  
            <asp:BoundField HeaderText="Grade" 
                  DataField="SmpGrd" SortExpression="SmpGrd">
                  </asp:BoundField> 
                                    
            <asp:TemplateField HeaderText="Project">
                <ItemTemplate>
                    <%#TimssBll.ProjectName(Eval("id")) %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="STF File">
                <ItemTemplate>
                    
                                <asp:LinkButton ID="LinkButtonViewFileSTF" runat="server" CommandName="DownloadfileSTF" CommandArgument='<%# Eval("id")%>' ToolTip='<%# Eval("STFUserFilePath")%>' Visible='<%#TimssBll.HasFilename(Eval("STFUserFilePath")) %>'><asp:Image ID="ImageSTF" runat="server" ImageUrl='<%#TimssBll.GetIconForFile(Eval("STFUserFilePath")) %>' Visible='<%#TimssBll.HasIconForFile(Eval("STFUserFilePath")) %>' /></asp:LinkButton>

                            &nbsp;
                    <asp:CheckBox ID="CheckBoxRemoveSTF" runat="server" Text="Remove" ForeColor="Red" Visible='<%#TimssBll.HasFilename(Eval("STFUserFilePath")) %>' />
                </ItemTemplate>
            </asp:TemplateField>

               <asp:TemplateField HeaderText="STF2 File">
                <ItemTemplate>
                    
                                <asp:LinkButton ID="LinkButtonViewFileSTF2" runat="server" CommandName="DownloadfileSTF2" CommandArgument='<%# Eval("id")%>' ToolTip='<%# Eval("STF2UserFilePath")%>' Visible='<%#TimssBll.HasFilename(Eval("STF2UserFilePath")) %>'><asp:Image ID="ImageSTF2" runat="server" ImageUrl='<%#TimssBll.GetIconForFile(Eval("STF2UserFilePath")) %>' Visible='<%#TimssBll.HasIconForFile(Eval("STF2UserFilePath")) %>' /></asp:LinkButton>

                            &nbsp;
                    <asp:CheckBox ID="CheckBoxRemoveSTF2" runat="server" Text="Remove" ForeColor="Red" Visible='<%#TimssBll.HasFilename(Eval("STF2UserFilePath")) %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="STLF File">
                <ItemTemplate>
                    
                                <asp:LinkButton ID="LinkButtonViewFileSTLF" runat="server" CommandName="DownloadfileSTLF" CommandArgument='<%# Eval("id")%>' ToolTip='<%# Eval("STLFUserFilePath")%>' Visible='<%#TimssBll.HasFilename(Eval("STLFUserFilePath")) %>'><asp:Image ID="ImageSTLF" runat="server" ImageUrl='<%#TimssBll.GetIconForFile(Eval("STLFUserFilePath")) %>' Visible='<%#TimssBll.HasIconForFile(Eval("STLFUserFilePath")) %>' /></asp:LinkButton>

                            &nbsp;
                    <asp:CheckBox ID="CheckBoxRemoveSTLF" runat="server" Text="Remove" ForeColor="Red" Visible='<%#TimssBll.HasFilename(Eval("STLFUserFilePath")) %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="STLF2 File">
                <ItemTemplate>
                    
                                <asp:LinkButton ID="LinkButtonViewFileSTLF2" runat="server" CommandName="DownloadfileSTLF2" CommandArgument='<%# Eval("id")%>' ToolTip='<%# Eval("STLF2UserFilePath")%>' Visible='<%#TimssBll.HasFilename(Eval("STLF2UserFilePath")) %>'><asp:Image ID="ImageSTLF2" runat="server" ImageUrl='<%#TimssBll.GetIconForFile(Eval("STLF2UserFilePath")) %>' Visible='<%#TimssBll.HasIconForFile(Eval("STLF2UserFilePath")) %>' /></asp:LinkButton>

                            &nbsp;
                    <asp:CheckBox ID="CheckBoxRemoveSTLF2" runat="server" Text="Remove" ForeColor="Red" Visible='<%#TimssBll.HasFilename(Eval("STLF2UserFilePath")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
                                    
            <asp:TemplateField HeaderText="TTF File">
                <ItemTemplate>
                    
                                <asp:LinkButton ID="LinkButtonViewFileTTF" runat="server" CommandName="DownloadfileTTF" CommandArgument='<%# Eval("id")%>' ToolTip='<%# Eval("TTFUserFilePath")%>' Visible='<%#TimssBll.HasFilename(Eval("TTFUserFilePath")) And TimssBll.isICILS(Eval("id")) %>'><asp:Image ID="ImageTTF" runat="server" ImageUrl='<%#TimssBll.GetIconForFile(Eval("TTFUserFilePath")) %>' Visible='<%#TimssBll.HasIconForFile(Eval("TTFUserFilePath")) %>' /></asp:LinkButton>

                            &nbsp;
                    <asp:CheckBox ID="CheckBoxRemoveTTF" runat="server" Text="Remove" ForeColor="Red" Visible='<%#TimssBll.HasFilename(Eval("TTFUserFilePath")) And TimssBll.isICILS(Eval("id")) %>' />
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


