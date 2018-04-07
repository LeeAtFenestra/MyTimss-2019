<%@ Page Title="Provide School Information" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ProvideSchoolInformation.aspx.vb" Inherits="ProvideSchoolInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-ui.css" />

  <script src="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-1.10.2.js"></script>

  <script src="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-ui.js"></script>
    <script>

        //<-- hide this script from non-javascript-enabled browsers
        var imgEdit = new hoverbutton('edit', '<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', true);
        //-->	

        $(function () {

            $("#<%=dbG4_DateSchoolStartsSpringBreak.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });
            $("#<%=dbG4_DateSchoolReturnsFromSpringBreak.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });
            $("#<%=dbG4_LastDayofSchool.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });

            $("#<%=dbG8_DateSchoolStartsSpringBreak.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });
            $("#<%=dbG8_DateSchoolReturnsFromSpringBreak.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });
            $("#<%=dbG8_LastDayofSchool.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });

            $("#<%=dbG12_DateSchoolStartsSpringBreak.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });
            $("#<%=dbG12_DateSchoolReturnsFromSpringBreak.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });
            $("#<%=dbG12_LastDayofSchool.ClientID%>").datepicker({ minDate: new Date(2017, 1 - 1, 1), maxDate: new Date(2018, 6 - 1, 30) });



      });
  </script>
    <style type="text/css">
        .auto-style1 {
            height: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <input type="hidden"  name="edited" value = "0" size="5">
<input type="hidden"  name="reader" value = 'False' size="8">
<h1>Provide School Information</h1>
<table border="0" cellpadding="2" cellspacing="4" width="100%" style="background-color:#FFC000;">
    <tr>
        <td width="50%"><b>School Name: </b><asp:Label ID="LabelSchoolName1" runat="server"></asp:Label></td>
       <%-- <td><b><%=TimssBll.ProjectName()%> Registration ID: </b><asp:Label ID="LabelTIMSSRegistrationID" runat="server"></asp:Label></td>--%>
        <td><b>District: </b><asp:Label ID="LabelDistrict" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td><b>State: </b><asp:Label ID="LabelState" runat="server"></asp:Label></td>
        <td><b>Submitted by: </b><asp:Label ID="LabelSubmittedBy" runat="server"></asp:Label></td>
    </tr>
    <%--<tr>
        <td>&nbsp;</td>
        <td><b>Submitted by: </b><asp:Label ID="LabelSubmittedBy" runat="server"></asp:Label></td>
    </tr>--%>
</table>
<br />
Please review and complete the form below to ensure that <%=TimssBll.ProjectName()%> has the most up-to-date information about your school.  Some fields are filled in already; please edit these if they are incorrect.
<div style="text-align:right">
    <img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15"><b><font size="2">&nbsp;&nbsp;
</div>
            <asp:Label ID="LabelMissingRequiredFields" runat="server" ViewStateMode="Disabled" Visible="false" ForeColor="Red">
                <div style="text-align:center"><table style="border:solid;border-color:red;border-width:2px;width:100%;padding:2em"><tr><td><font color="red">*This section is incomplete, please review the <asp:Label ID="numCount" runat="server" /> missing field<asp:Label ID="lblFields" runat="server" Text="s" /> below.  (&#10038)</font></td></tr></table></div>
            </asp:Label><asp:Label ID="LabelSaveComplete" runat="server" ViewStateMode="Disabled" Visible="false" ForeColor="Blue">
                <div style="text-align:center"><p>Thank you for completing the Provide School Information form!  Your information should now show in the Current column.</p></div>
            </asp:Label><table border="0" cellpadding="2" cellspacing="4" width="100%">
    <tr style="background-color:#cccccc; text-align:center">
        <td colspan="2"><b>School Contact Information</b></td></tr><tr>
        <td style="text-align:left; width: 600px; color:blue" valign="top">Current</td><td style="text-align: left; color:blue" valign="top">New</td></tr><tr>
        <td valign="top"><b>School Name:&nbsp;</b><asp:Label ID="req1" runat="server" ForeColor="Red" Visible="False" Font-Size="medium">&#10038;</asp:Label><asp:Label ID="LabelSchoolName2" runat="server" /></td><td valign="top"><asp:TextBox ID="db_s_name" runat="server" Columns="60" MaxLength="60" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>School Address 1:&nbsp;<asp:Label ID="req2" runat="server" ForeColor="Red" Visible="False" font-size="medium">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolAddress1" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="db_s_addr1" runat="server" Columns="60" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>School Address 2:&nbsp;</b><asp:Label ID="LabelSchoolAddress2" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="db_s_addr2" runat="server" Columns="60" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>City:&nbsp;<asp:Label ID="req3" runat="server" ForeColor="Red" Visible="False" Font-Size="medium">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolCity" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="db_s_city" runat="server" Columns="60" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>State:&nbsp;<asp:Label ID="req4" runat="server" ForeColor="Red" Visible="False" Font-Size="medium">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolState" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="db_s_state" runat="server" Columns="3" MaxLength="2" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>ZIP Code:&nbsp;<asp:Label ID="req5" runat="server"  ForeColor="Red" Visible="False" Font-Size="medium"> &#10038;</asp:Label></b><asp:Label ID="LabelSchoolZipCode" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="db_s_zip" runat="server" Columns="60" onchange="Edited();"></asp:TextBox></td></tr>
                
                <asp:Panel ID="PanelICTCoordinatorName" runat="server">
        <tr>
          <td valign="top">ICT Coordinator Name (Staff responsible for Information and Communication Technology (ICT). Enter principal info if no ICT coordinator at your school):&nbsp;<asp:Label ID="req6" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelICTCoordinatorName" runat="server" />

          </td>
            <td valign="top"><asp:TextBox ID="db_ICTCoordinatorName" runat="server" Columns="60" maxlength="60" onchange="Edited();"></asp:TextBox>

            </td>

        </tr>

                      <tr>
          <td valign="top">ICT Coordinator Email:&nbsp;<asp:Label ID="req20" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelICTCoordinatorEmail" runat="server" />

          </td>
            <td valign="top"><asp:TextBox ID="db_ICTCoordinatorEmail" runat="server" Columns="60" maxlength="50" onchange="Edited();"></asp:TextBox>

            </td>

        </tr>

                </asp:Panel>
                <tr>
        <td valign="top" colspan="2">&nbsp; <asp:HiddenField ID="dbprincipal_frame_n_" runat="server" />
        <asp:HiddenField ID="dbprincipal_title" runat="server" />
        <asp:HiddenField ID="dbprincipal_fax" runat="server" /></td>
    </tr>
    <tr>
        <td valign="top"><b>Principal Prefix:&nbsp;</b><asp:Label ID="LabelSchoolPrincipalPrefix" runat="server"></asp:Label>&nbsp;<asp:Label ID="LabelPrincipalId" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbprincipal_prefix" runat="server" Columns="5" MaxLength="8" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td><b>Principal First Name:&nbsp;<asp:Label ID="req7" runat="server" ForeColor="Red" Visible="False" Font-Size="medium">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolPrincipalFname" runat="server"></asp:Label></td><td><asp:TextBox ID="dbprincipal_fname" runat="server" Columns="27" MaxLength="30" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td><b>Principal Last Name:&nbsp;<asp:Label ID="req8" runat="server" ForeColor="Red" Visible="False" Font-Size="medium">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolPrincipalLname" runat="server"></asp:Label></td><td><asp:TextBox ID="dbprincipal_lname" runat="server" Columns="27" MaxLength="30" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td><b>Principal Suffix:&nbsp;</b><asp:Label ID="LabelSchoolPrincipalSuffix" runat="server"></asp:Label></td><td><asp:TextBox ID="dbprincipal_suffix" runat="server" size="5" maxlength="8" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>Principal Telephone Number:&nbsp;<asp:Label ID="req9" runat="server" ForeColor="Red" Visible="False" Font-Size="Medium">&#10038;</asp:Label></b><asp:Label ID="LabelPrincipalTelephoneNumber" runat="server"></asp:Label>&nbsp;<b>Ext:</b>&nbsp;<asp:Label ID="LabelPrincipalTelephoneNumberExt" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbprincipal_phone" runat="server" size="27" maxlength="27" onchange="Edited();"></asp:TextBox><font face="Verdana" size="2">&nbsp;Ext:&nbsp;</font><asp:TextBox ID="dbprincipal_phoneext" runat="server" size="5" maxlength="5" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>Principal Email:&nbsp;<asp:Label ID="req10" runat="server" ForeColor="Red" Visible="False" Font-Size="Medium">&#10038;</asp:Label></b><asp:Label ID="LabelPrincipalEmail" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbprincipal_email" runat="server" size="35" maxlength="100" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top" colspan="2">&nbsp;<asp:HiddenField ID="dbcoordinator_frame_n_" runat="server" /></td>
    </tr>
    <tr>
        <td valign="top"><b>School Coordinator Prefix:&nbsp;</b><asp:Label ID="LabelSchoolCoordinatorPrefix" runat="server"></asp:Label>&nbsp;<asp:Label ID="LabelSchoolCoordinatorID" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbcoordinator_prefix" runat="server" Columns="5" MaxLength="8" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td><b>School Coordinator First Name:&nbsp;<asp:Label ID="req11" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolCoordinatorFname" runat="server"></asp:Label></td><td><asp:TextBox ID="dbcoordinator_fname" runat="server" Columns="27" MaxLength="30" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td><b>School Coordinator Last Name:&nbsp;<asp:Label ID="req12" runat="server" font-size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label></b><asp:Label ID="LabelSchoolCoordinatorLname" runat="server"></asp:Label></td><td><asp:TextBox ID="dbcoordinator_lname" runat="server" Columns="27" MaxLength="30" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td><b>School Coordinator Suffix:&nbsp;</b><asp:Label ID="LabelSchoolCoordinatorSuffix" runat="server"></asp:Label></td><td><asp:TextBox ID="dbcoordinator_suffix" runat="server" size="5" maxlength="8" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>School Coordinator Title:&nbsp;</b><asp:Label ID="LabelSchoolCoordinatorTitle" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbcoordinator_title" runat="server" size="27" maxlength="50" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>School Coordinator Telephone:&nbsp;<asp:Label ID="req13" runat="server" ForeColor="Red" Visible="False" Font-Size="Medium">&#10038;</asp:Label></b><asp:Label ID="LabelCoordinatorTelephoneNumber" runat="server"></asp:Label>&nbsp;<b>Ext:</b>&nbsp;<asp:Label ID="LabelCoordinatorTelephoneNumberExt" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbcoordinator_phone" runat="server" size="27" maxlength="27" onchange="Edited();"></asp:TextBox><font face="Verdana" size="2">&nbsp;Ext:&nbsp;</font><asp:TextBox ID="dbcoordinator_phoneext" runat="server" size="5" maxlength="5" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>School Coordinator Fax:&nbsp;</b><asp:Label ID="LabelCoordinatorFaxNumber" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbcoordinator_fax" runat="server" size="27" maxlength="27" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><b>School Coordinator Email:&nbsp;<asp:Label ID="req14" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label></b><asp:Label ID="LabelCoordinatorEmail" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbcoordinator_email" runat="server" size="35" maxlength="100" onchange="Edited();"></asp:TextBox></td></tr><tr style="background-color:#cccccc; text-align:center">
        <td colspan="2"><b>School Characteristics</b></td></tr><asp:Panel ID="PanelG4" runat="server">
    <tr style="background-color:#cccccc; text-align:center" ID="trG4Header" runat="server">
        <td colspan="2"><b>Grade 4</b></td></tr><asp:PlaceHolder ID="placeholdergread4springbreak" runat="server">
    <tr>
        <td valign="top">Date school starts spring break:&nbsp;<asp:Label ID="LabelDateSchoolStartsSpringBreak4" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG4_DateSchoolStartsSpringBreak" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Date school returns from spring break:&nbsp;<asp:Label ID="LabelDateSchoolReturnsFromSpringBreak4" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG4_DateSchoolReturnsFromSpringBreak" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr></asp:PlaceHolder><tr>
        <td valign="top">Last day of school for the current school year:&nbsp;<asp:Label ID="LabelLastDayofSchool4" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG4_LastDayofSchool" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Enrollment at grade 4 for the entire school:&nbsp;<asp:Label ID="req15" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelEnrollmentAtGrade4" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG4_EnrollmentAtGrade" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Number of classes with at least one 4<sup>th</sup> grade student:&nbsp;<asp:Label ID="req16" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelNumberOfClasses4" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG4_NumberOfClasses" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td></tr></asp:Panel><asp:Panel ID="PanelG8" runat="server">
    <tr style="background-color:#cccccc; text-align:center" ID="trG8Header" runat="server">
        <td colspan="2"><b>Grade 8</b></td></tr><asp:PlaceHolder ID="placeholdergrade8springbreak" runat="server">
    <tr>
        <td valign="top">Date school starts spring break:&nbsp;<asp:Label ID="LabelDateSchoolStartsSpringBreak8" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG8_DateSchoolStartsSpringBreak" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Date school returns from spring break:&nbsp;<asp:Label ID="LabelDateSchoolReturnsFromSpringBreak8" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG8_DateSchoolReturnsFromSpringBreak" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr></asp:PlaceHolder><tr>
        <td valign="top">Last day of school for the current school year:&nbsp;<asp:Label ID="LabelLastDayofSchool8" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG8_LastDayofSchool" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Enrollment at grade 8 for the entire school:&nbsp;<asp:Label ID="req17" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelEnrollmentAtGrade8" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG8_EnrollmentAtGrade" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top"><asp:Label ID="LabelNumberOfMathClasses8Static" runat="server">Number of 8<sup>th</sup>-grade math classes</asp:Label>:&nbsp;<asp:Label ID="req18" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelNumberOfMathClasses8" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG8_NumberOfMathClasses" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td></tr></asp:Panel><asp:Panel ID="PanelG12" runat="server">
    <tr style="background-color:#cccccc; text-align:center" ID="trG12Header" runat="server">
        <td colspan="2"><b>Grade 12</b></td></tr><asp:PlaceHolder ID="placeholdergrade12springbreak" runat="server">
    <tr>
        <td valign="top">Date school starts spring break:&nbsp;<asp:Label ID="LabelDateSchoolStartsSpringBreak12" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG12_DateSchoolStartsSpringBreak" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Date school returns from spring break:&nbsp;<asp:Label ID="LabelDateSchoolReturnsFromSpringBreak12" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG12_DateSchoolReturnsFromSpringBreak" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr></asp:PlaceHolder><tr>
        <td valign="top">Last day of school for the current school year for 12<sup>th</sup> graders:&nbsp;<asp:Label ID="LabelLastDayofSchool12" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG12_LastDayofSchool" runat="server" size="16" maxlength="20" onchange="Edited();"></asp:TextBox></td></tr><tr>
        <td valign="top">Enrollment at grade 12 for entire school:&nbsp;<asp:Label ID="req19" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False">&#10038;</asp:Label><asp:Label ID="LabelEnrollmentAtGrade12" runat="server"></asp:Label></td><td valign="top"><asp:TextBox ID="dbG12_EnrollmentAtGrade" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td></tr></asp:Panel><tr>
        <td valign="top" colspan="2" style="text-align:center">
            <br />
            <asp:Button ID="ButtonSave" runat="server" Text="Save" onclientclick="ClearEdited();" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type="reset"  value="Reset" name="B1" onclick="ClearEdited();" /> </td></tr></table><br />
<br />
</asp:Content>

