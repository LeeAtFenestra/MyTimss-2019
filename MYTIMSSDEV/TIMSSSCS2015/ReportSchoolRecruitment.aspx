<%@ Page Title="School Recruitment" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="ReportSchoolRecruitment.aspx.vb" Inherits="TIMSSSCS2015_ReportSchoolRecruitment" %>

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
            <a href="ReportSchoolRecruitment.aspx" class="menuOn" title="School Recruitment">School Recruitment</a>
                  |
          
            <a href="ReportSchoolCompletion.aspx" class="menu" title="School Completion">School Completion</a>
                  
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
                        <b><font color="#000000">Filter:</font></b>
                    <asp:DropDownList ID="DropDownListFilter" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;       
                    &nbsp;            
                    <b><font color="#000000">Export:</font></b>
                    <asp:DropDownList ID="DropDownListExport" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                         
<asp:ImageButton ID="ImageButtonXportToExcel" runat="server" ImageUrl="~/common/images/Excel.jpg" Width="20" Height="20" ToolTip="Excel Export" />
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
            <asp:BoundField HeaderText="School Name" 
                  DataField="S_Name" SortExpression="S_Name">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="Study Name" SortExpression="Study_Name">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "Study_Name")%> </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Address1" 
                  DataField="S_Addr1" SortExpression="S_Addr1">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Address2" 
                  DataField="S_Addr2" SortExpression="S_Addr2">
                  </asp:BoundField>
            <asp:BoundField HeaderText="City" 
                  DataField="S_City" SortExpression="S_City">
                  </asp:BoundField>
            <asp:BoundField HeaderText="State" ItemStyle-HorizontalAlign="center" 
                  DataField="S_State" SortExpression="S_State">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Zip" 
                  DataField="S_Zip" SortExpression="S_Zip">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Phone" 
                  DataField="S_Phone" SortExpression="S_Phone">
                  </asp:BoundField>
            <asp:BoundField HeaderText="County" 
                  DataField="S_County" SortExpression="S_County">
                  </asp:BoundField>     
             <asp:BoundField HeaderText="Registration ID" 
                  DataField="MyNAEPREGID" SortExpression="MyNAEPREGID">
            </asp:BoundField>            
            <asp:BoundField HeaderText="PSI Status" ItemStyle-HorizontalAlign="center" 
                  DataField="PSIComplete" SortExpression="PSIComplete">
                  </asp:BoundField>
            <asp:BoundField HeaderText="School Cooperation status" 
                  DataField="DispName" SortExpression="DispName">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="Assessment date" SortExpression="ScheDate">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ScheDate", "{0:M/d/yyyy}")%> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Assessment time" SortExpression="ScheTime">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ScheTime")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Assessment Arrival Time" SortExpression="ArrivalTime">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ArrivalTime")%></ItemTemplate>
            </asp:TemplateField>
        
            <%--removed 9/12/17--%>
       <%--     <asp:TemplateField HeaderText="Assessment date 2" SortExpression="ScheDate2">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ScheDate2", "{0:M/d/yyyy}")%> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Assessment time 2" SortExpression="ScheTime2">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ScheTime2")%></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Assessment Arrival Time 2" SortExpression="ArrivalTime2">
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem, "ArrivalTime2")%></ItemTemplate>
            </asp:TemplateField>--%>
        
            <asp:BoundField HeaderText="School enrollment" 
                  DataField="ESTGRE" SortExpression="ESTGRE">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Grade" 
                  DataField="SMPGRD" SortExpression="SMPGRD">
                  </asp:BoundField>
            <asp:BoundField HeaderText="State School ID" ItemStyle-HorizontalAlign="center" 
                  DataField="SEASCH" SortExpression="SEASCH">
                  </asp:BoundField>
            <asp:BoundField HeaderText="School ID" ItemStyle-HorizontalAlign="center" 
                  DataField="id" SortExpression="id">
                  </asp:BoundField>
            <asp:BoundField HeaderText="IEA_ID" ItemStyle-HorizontalAlign="center" 
                  DataField="IEA_ID" SortExpression="IEA_ID">
                  </asp:BoundField>
             <asp:BoundField HeaderText="Special Case?" 
                  DataField="SPECIAL_CASE" SortExpression="SPECIAL_CASE">
                  </asp:BoundField> 
            <asp:BoundField HeaderText="Parent Consent Type" 
                  DataField="ParentConsentType" SortExpression="ParentConsentType">
                  </asp:BoundField>


            <asp:BoundField HeaderText="Returns from winter break" 
                  DataField="DateSchoolReturnsFromWinterBreak" SortExpression="DateSchoolReturnsFromWinterBreak" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Returns from spring break" 
                  DataField="DateSchoolReturnsFromSpringBreak" SortExpression="DateSchoolReturnsFromSpringBreak" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Last day of school for the year" 
                  DataField="LastDayofSchool" SortExpression="LastDayofSchool" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Enrollment at grade" 
                  DataField="EnrollmentAtGrade" SortExpression="EnrollmentAtGrade">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Number of Math classes" 
                  DataField="NumberOfMathClasses" SortExpression="NumberOfMathClasses">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Number of classes" 
                  DataField="NumberOfClasses" SortExpression="NumberOfClasses">
                  </asp:BoundField>
                  
            <asp:BoundField HeaderText="Territory" 
                  DataField="fldTerritoryCode" SortExpression="fldTerritoryCode">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Original or Sub" 
                  DataField="ORIGSUB" SortExpression="ORIGSUB">
                  </asp:BoundField>
                  
            <asp:BoundField HeaderText="School Type" 
                  DataField="SchoolType">
                  </asp:BoundField>
            <asp:BoundField HeaderText="REPSBGRP" 
                  DataField="REPSBGRP" SortExpression="REPSBGRP">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Region" 
                  DataField="testregion" SortExpression="testregion">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Principal Prefix" 
                  DataField="pprefix" SortExpression="pprefix">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Principal Fname" 
                  DataField="pfname" SortExpression="pfname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Principal Lname" 
                  DataField="plname" SortExpression="plname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Principal Suffix" 
                  DataField="sp_suffix" SortExpression="sp_suffix">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="Principal Phone" SortExpression="sp_phone">
                <ItemTemplate><%#Container.DataItem("sp_phone")%> <%#Container.DataItem("sp_Phoneext")%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Principal Email" 
                  DataField="sp_email" SortExpression="sp_email">
                  </asp:BoundField>
                  
            <asp:BoundField HeaderText="School Coordinator Prefix" 
                  DataField="sc_prefix" SortExpression="sc_prefix">
                  </asp:BoundField>
            <asp:BoundField HeaderText="School Coordinator Fname" 
                  DataField="sc_fname" SortExpression="sc_fname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="School Coordinator Lname" 
                  DataField="sc_lname" SortExpression="sc_lname">
                  </asp:BoundField>
            <asp:BoundField HeaderText="School Coordinator Suffix" 
                  DataField="sc_suffix" SortExpression="sc_suffix">
                  </asp:BoundField>
            <asp:TemplateField HeaderText="School Coordinator Phone" SortExpression="sc_phone">
                <ItemTemplate><%#Container.DataItem("sc_phone")%> <%#Container.DataItem("sc_Phoneext")%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="School Coordinator Email" 
                  DataField="sc_email" SortExpression="sc_email">
                  </asp:BoundField>

             
            <asp:BoundField HeaderText="District Name" 
                  DataField="d_name" SortExpression="d_name">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District ID" 
                  DataField="LEAID" SortExpression="LEAID">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District Addr1" 
                  DataField="d_addr1" SortExpression="d_addr1">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District Addr2" 
                  DataField="d_addr2" SortExpression="d_addr2">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District City" 
                  DataField="d_city" SortExpression="d_city">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District State" 
                  DataField="d_state" SortExpression="d_state">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District ZIP" 
                  DataField="d_zip" SortExpression="d_zip">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District Phone" 
                  DataField="d_phone" SortExpression="d_phone">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District Fax" 
                  DataField="d_fax" SortExpression="d_fax">
                  </asp:BoundField>
             <asp:BoundField HeaderText="District Contact" 
                  DataField="d_contact" SortExpression="d_contact">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Contact Phone" 
                  DataField="d_contphone" SortExpression="d_contphone">
                  </asp:BoundField>
            <asp:BoundField HeaderText="District Contact Email" 
                  DataField="d_contemail" SortExpression="d_contemail">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Date Initial Sch Sel Ltr Sent" 
                  DataField="sch_partltrsentdt" SortExpression="sch_partltrsentdt" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Date Sch Asmt Date Ltr Sent" 
                  DataField="SchAsmtLtrSentDT" SortExpression="SchAsmtLtrSentDT" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField>
            <asp:BoundField HeaderText="Date Sch Coord Resp Mailing Sent" 
                  DataField="AugSchLtrSentDT" SortExpression="AugSchLtrSentDT" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField> 
            <asp:BoundField HeaderText="Date Dist Ltr Sent" 
                  DataField="d_partltrsentdt" SortExpression="d_partltrsentdt" DataFormatString="{0:M/d/yyyy}">
                  </asp:BoundField> 



        </Columns>
        </asp:GridView>



</td></tr><tr><td bgcolor = "#FFFFFF">

            <uc1:ucFilterAndPageControl ID="ucFilterAndPageControl1" runat="server" />
</td></tr></table>

</asp:Content>
