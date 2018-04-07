<%@ Page Title="Accounts" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="Accounts.aspx.vb" Inherits="admin_Accounts" %>

<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF"> 
    <div style="margin:5px; vertical-align:middle">
            <a href="accountedit.aspx" title="Create account"><img id="Img1" src="~/common/images/buttons/pencilplus.gif" runat="server" border="0" alt="Create account" style="margin-right:10px" />Create Account</a>
    <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?sortfld=<%=me.GridViewSortColumn() %>&sortdir=<%=me.GridViewSortDirection()%>&print=1&searchSTR=' + getFrm().<%=ucFilterAndPageControl1.searchSTRUniqueID %>.value + '&searchFLD=' + getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].options[getFrm().elements['<%=ucFilterAndPageControl1.searchFLDUniqueID %>'].selectedIndex].value + '&pg=<%=ucFilterAndPageControl1.SelectedPage() %>&ps=<%=ucFilterAndPageControl1.PageSizeSelected() %>'));"><img src="~/common/images/pdficon_small.png" runat="server" border="0" alt="Save as pdf" style="margin-right:10px" />Save as PDF</a>
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
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User Name"  SortExpression="UserName">
                <ItemTemplate>
                    <a href="accountedit.aspx?u=<%# Eval("UserName")%>"><%# Eval("UserName")%></a></b>
                    <asp:ImageButton ID="ImageButtonLockedOut" runat="server"  ImageUrl="~/common/images/buttons/keys.gif" ToolTip='<%# Eval("LastLockoutDate")%>' Visible='<%# Eval("IsLockedOut")%>' BorderWidth="0" CommandName="unlock" CommandArgument='<%# Eval("UserName")%>' OnClientClick="return confirm('Are you sure you want to unlock this account?');"  />
                    
                    <img src="~/common/images/warning.jpg" runat="server" id="imgdisabled" alt="Disabled account" visible='<%# not Eval("IsApproved")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField HeaderText="Firstname" 
                  DataField="FirstName" SortExpression="FirstName">
                  </asp:BoundField> 
            
            <asp:BoundField HeaderText="Lastname" 
                  DataField="Lastname" SortExpression="Lastname">
                  </asp:BoundField> 
                  

            <asp:BoundField HeaderText="Email" 
                  DataField="Email" SortExpression="Email">
                  </asp:BoundField>       
            <asp:BoundField HeaderText="Wins ID" 
                  DataField="WINSID" SortExpression="WINSID">
                  </asp:BoundField> 
                         
                     
            <asp:BoundField HeaderText="REPSBGRP" 
                  DataField="REPSBGRP" SortExpression="REPSBGRP">
                  </asp:BoundField> 
                     
            <asp:BoundField HeaderText="RegistrationId" 
                  DataField="RegistrationId" SortExpression="RegistrationId">
                  </asp:BoundField> 
                     
            <asp:BoundField HeaderText="Frame_N_" 
                  DataField="Frame_N_" SortExpression="Frame_N_">
                  </asp:BoundField> 
                  
            <asp:TemplateField HeaderText="Telephone" SortExpression="Telephone">
                <ItemTemplate>
                
                <%# Eval("Telephone")%>
                <%# Eval("TelephoneExtension")%>

                </ItemTemplate>
            </asp:TemplateField>
            

            <asp:TemplateField HeaderText="Assigned Roles">
                <ItemTemplate>
                

                    <asp:Repeater ID="RepeaterRoles" runat="server"
             DataSource='<%# Roles.GetRolesForUser(Eval("UserName"))%>'>
                        <ItemTemplate>                    
				              <%#Container.DataItem%><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:TemplateField>
            

            <asp:BoundField HeaderText="Last Login" 
                  DataField="LastLoginDate" SortExpression="LastLoginDate" DataFormatString="{0:d}">
                  </asp:BoundField> 

                  
            <asp:TemplateField HeaderText="Days till Password Expires" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Me.CalcDaysTillPasswordExpires(Eval("CreateDate"))%>
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

