<%@ Page Title="Questionnaire Status Report" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="ReportQuestionnaireStatus.aspx.vb" Inherits="TIMSSSCS2015_ReportQuestionnaireStatus" %>

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
          
            <a href="ReportSchoolCompletion.aspx" class="menu" title="School Completion">School Completion</a>
                  
                  |
            <a href="ReportAssessmentDay.aspx" class="menu" title="Assessment Day">Assessment Day</a>
                  |
            <a href="ReportQuestionnaireStatus.aspx" class="menuOn" title="Questionnaire Status Report">Questionnaire Status Report</a>      
                  |
            <a href="ReportSchoolDetail.aspx" class="menu" title="School Detail">School Detail</a>
                  
                  </TD></TR></TBODY></TABLE>
                  </TD></TR></TBODY></TABLE>
      </TD>
    <TD height=29 width=4></TD></TR>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
            	               <div style="margin:5px; vertical-align:middle">
                   <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                    <div style="text-align:left">
                    &nbsp;
                    &nbsp;
                    <b>State: </b>
                    <asp:DropDownList ID="DropDownListState" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    <b>Region: </b>
                    <asp:DropDownList ID="DropDownListRegion" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
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

<h4>School Administrator and Teacher Questionnaire Status Report</h4>
<table border="0" cellpadding="3" cellspacing="5">
<tr>
    <td valign="top">
    <asp:ListBox ID="ListBoxSchoolList" runat="server" DataValueField="Value" DataTextField="Name" Rows="30" AutoPostBack="true"></asp:ListBox>
    </td>
    <td valign="top">
            <asp:Repeater ID="RepeaterSchoolList" runat="server">
                <HeaderTemplate>
			        <table border="1" class="sortable" cellpadding="3" cellspacing="5">            
				      <tr>
				      <td><b>Date Generated:</b></td>
				      <td><%#DateTime.Now%></td>
					  </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td><b><%#Eval("Name")%>:</b></td>
				      <td><%#Eval("Value")%></td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
    </td>
</tr>
</table>
                        

                        
            </td>
        </tr>
    </table>
</asp:Content>

