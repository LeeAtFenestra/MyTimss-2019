<%@ Page Title="Reports" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="TIMSSSCS2015_Reports" %>

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
                <%--  |
            <a href="ReportQuestionnaireStatus.aspx" class="menu" title="Assessment Day">Questionnaire Status Report</a>      --%>
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
            <ul>
            <h3>Reports</h3>
                <li><a href="ReportSchoolRecruitment.aspx">School Recruitment</a></li>
         <!--       <li><a href="ReportDistrictRecruitment.aspx">District Recruitment</a></li>  -->
                <li><a href="ReportSchoolCompletion.aspx">School Completion</a></li>
                <li><a href="ReportAssessmentDay.aspx">Assessment Day</a></li>
           <%--     <li><a href="ReportQuestionnaireStatus.aspx" title="Questionnaire Status Report">Questionnaire Status Report</a></li>--%>
                <li><a href="ReportSchoolDetail.aspx" title="School Detail">School Detail</a></li>
            </ul>
                        
            </td>
        </tr>
    </table>
</asp:Content>

