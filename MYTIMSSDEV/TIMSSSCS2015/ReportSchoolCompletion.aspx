<%@ Page Title="School Completion Report" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="ReportSchoolCompletion.aspx.vb" Inherits="TIMSSSCS2015_ReportSchoolCompletion" %>


<%@ Register src="../usercontrols/ucFilterAndPageControl.ascx" tagname="ucFilterAndPageControl" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopNavigationLevel2" Runat="Server">

  <TR>
    <TD height=29 width=4></TD>
    <TD>
      <TABLE cellSpacing=0 cellPadding=0 width="100%" bgColor=#2a3985 
        border=0><TBODY>
        <TR>
          <TD colSpan=2>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TBODY>
              <TR>
                <TD height=28 noWrap><FONT size=2>&nbsp;<FONT color=#ffffff> 
                <FONT color=#ffffff><b>Reports:</b></FONT>
            <a href="ReportSchoolRecruitment.aspx" class="menu" title="School Recruitment">School Recruitment</a>
                  |
          
            <a href="ReportSchoolCompletion.aspx" class="menuOn" title="School Completion">School Completion</a>
                  
                  |
            <a href="ReportAssessmentDay.aspx" class="menu" title="Assessment Day">Assessment Day</a>
                  |
            <a href="ReportQuestionnaireStatus.aspx" class="menu" title="Questionnaire Status Report">Questionnaire Status Report</a>                 
                  |
            <a href="ReportSchoolDetail.aspx" class="menu" title="School Detail">School Detail</a>
                  </TD></TR></TBODY></TABLE>
                  </TD></TR></TBODY></TABLE>
      </TD>
    <TD height=29 width=4></TD></TR>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
	               <div style="margin:5px; vertical-align:middle">
                   <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                    <div style="text-align:left">
                    &nbsp;
                    &nbsp;
                         <b><font color="#000000">School Type:</font></b>
                    <asp:DropDownList ID="DropDownListSchoolType" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                            &nbsp;
                    &nbsp;
                       <b><font color="#000000">  Territory:</b>
                    <asp:DropDownList ID="DropDownListTerritory" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    <b>State: </b>
                    <asp:DropDownList ID="DropDownListState" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    <b>Region: </b>
                    <asp:DropDownList ID="DropDownListRegion" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;       
                    &nbsp;   
                    &nbsp;       
                    &nbsp;            
                    <b><font color="#000000">Export:</font></b>
                    <asp:DropDownList ID="DropDownListExport" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                         
<asp:ImageButton ID="ImageButtonXportToExcel" runat="server" ImageUrl="~/common/images/Excel.jpg" Width="20" Height="20" ToolTip="Excel Export" />
                    &nbsp;       
                    &nbsp;   
            <input type="button"  value="Print Page" onclick="window.print();" class="hotcell2" />
                    </div>

                        </td>
                        <td align="right">
                        
            </td>
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
                <ItemTemplate><b><%# Container.DataItemIndex + 1 %>.</b></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Code" 
                  DataField="DISP" SortExpression="DISP">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Code Description" 
                  DataField="DispName" SortExpression="DispName">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Total" 
                  DataField="Total">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Original Total" 
                  DataField="OriginalTotal">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Substitute Total" 
                  DataField="SubTotal">
                  </asp:BoundField>
        </Columns>
        </asp:GridView>



</td></tr><tr><td bgcolor = "#FFFFFF">

            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />
</td></tr></table>

</asp:Content>


