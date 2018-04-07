<%@ Page Title="Prepare For Assessment" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PrepareForAssessment.aspx.vb" Inherits="PrepareForAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
                <h3>Prepare For Assessment</h3>
                <!--
                <p>Information coming this winter.</p>
                -->
    <asp:Label ID="lblNotAvailable"  Visible="false"  runat="server" Text="The page will go live about a week after you have uploaded your lists."></asp:Label>



<asp:Panel ID="panelgrade4eTIMSS" runat="server" Width="96%">
<h4>Grade 4</h4>

<p>Thank you for providing information about all the fourth-grade classes in your school! After submitting both class and student lists, you will receive an email telling you that your sample of classes has been selected. Return to this webpage when you receive the email from TIMSS@westat.com: “The list of students selected to participate in TIMSS is available in the Student Teacher Linkage Form (STLF),” available below.</p>

<p>
    You will see more than one STLF link if more than one class was selected for TIMSS at your school. Each STLF has the list of students in the selected class and their teachers.
    <br /><br />
    <b>Student Tracking Linkage Form (download both if more than one link is shown)</b>
    <br />
    <asp:Image ID="ImageSTLF1Grade4eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTLF1Grade4eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId4%>'  ToolTip="Click here to download">Click here to download the STLF 1</asp:LinkButton>
     <br />
    <asp:Image ID="ImageSTLF2Grade4eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTLF2Grade4eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId4%>'  ToolTip="Click here to download">Click here to download the STLF 2</asp:LinkButton>
</p>


<ul><li><b>Student-Teacher Linkage Form (STLF)</b>&nbsp;–&nbsp;The forms list the selected students and their teachers. Please make sure students and teachers are linked correctly on the form. Instructions on how to access and review the forms were emailed to you and are in the <b>Documents</b> section on this website.</li></ul>
<p>Please note the following:</p>
<p>If the students and teacher linkages appear fine in the STLF, no need to contact the TIMSS help desk. Three business days later, please return to this page. Two new links will be shown here to download STF1 and STF2 (Student Tracking Forms). This is the form you can review to see if any student names, information, new enrollees, no longer enrolled students, should be updated. Your Test Administrator will contact you to discuss this. Do not contact the TIMSS help desk unless there are major updates needed on this form. You can discuss the minor updates with your Test Administrator when they call you in the pre-assessment call.</p>

<p>----------------------------------------------------------------</p>

<p>
    <b>Student Tracking Form (download both if more than one link is shown)</b>
    <br />
    <asp:Image ID="ImageSTF1Grade4eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTF1Grade4eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId4%>'  ToolTip="Click here to download">Click here to download the STF 1</asp:LinkButton>
     <br />
    <asp:Image ID="ImageSTF2Grade4eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTF2Grade4eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId4%>'  ToolTip="Click here to download">Click here to download the STF 2</asp:LinkButton>
</p>

<ul>
    <li><b>Student Tracking Form (STF)</b>&nbsp;–&nbsp;The forms list the students who have been selected to participate in the assessment at your school. Instructions on how to access and review the forms are in the <b>Documents</b> section on this website.</li>
    <li><b>Sample Parent Notification Letter</b> and <b>Facts for Parents about TIMSS (Parent FAQ)</b>&nbsp;–&nbsp;You will receive a packet of TIMSS materials including a sample parent/guardian notification letter and a TIMSS Parent FAQ (not for Indiana schools). </li> 
        <p>(<font color="red">If you are a school in Indiana, please contact TIMSS@westat.com;</font> do not use the instructions below).</p>
        <p> For non-Indiana schools, a sample parent/guardian notification letter is in the <b>Documents</b> section for you to customize and print on your school letterhead. Please distribute the parent notification letter and the Facts for Parents about TIMSS (from our mailing to you) to the parents of the selected students prior to the assessment. A Spanish version of the letter is also available in the <b>Documents</b> page. You can also share the Parent FAQ link available here: 
        <a href="https://nces.ed.gov/timss/parents/" target="_blank">https://nces.ed.gov/timss/parents/</a>.  </p>
        <p>As your assessment date approaches, your Test Administrator will be contacting you to finalize the assessment day activities.</p>
    </ul>

   <%-- <asp:Panel ID="PanelUpdateSTLFFileGrade4" runat="server" Visible="false" style="border-style:dashed; margin:5px; padding:5px; border-color:#008CB3;">
    <h3>Replace TIMSS ID&nbsp;<asp:Label ID="TIMSSIDGrade4" runat="server" /> STLF file</h3>
    
<asp:FileUpload ID="FileUploadGrade4" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="valFileUpload" runat="server" ValidationGroup="upload4"
            ControlToValidate="FileUploadGrade4" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="regexpvalFileExtension" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx, .zip files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX|zip|ZIP)$"
                            ControlToValidate="FileUploadGrade4" Display="Dynamic" ValidationGroup="upload4"></asp:RegularExpressionValidator>
        <br /><br />
         &nbsp;<asp:Button ID="ButtonUploadGrade4" runat="server" Text="Upload" ValidationGroup="upload4" />
         <asp:Button ID="ButtonRemoveGrade4" runat="server" Text="Remove" OnClientClick="return confirm('Are you sure?');" />
          <asp:Label ID="LabelFileUploadGrade4" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
</asp:Panel>--%>
</asp:Panel>






<asp:Panel ID="panelgrade8eTIMSS" runat="server" Width="96%">
<h4>Grade 8</h4>

<p>Thank you for providing information about all the eighth-grade classes in your school! After submitting both class and student lists, you will receive an email telling you that your sample of classes has been selected. Return to this webpage when you receive the email from TIMSS@westat.com: “The list of students selected to participate in TIMSS is available in the Student Teacher Linkage Form (STLF),” available below.</p>

<p>
    You will see more than one STLF link if more than one class was selected for TIMSS at your school. Each STLF has the list of students in the selected class and their teachers.
    <br /><br />
    <b>Student Tracking Linkage Form (download both if more than one link is shown)</b>
    <br />
    <asp:Image ID="ImageSTLF1Grade8eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTLF1Grade8eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId8%>'  ToolTip="Click here to download">Click here to download the STLF 1</asp:LinkButton>
     <br />
    <asp:Image ID="ImageSTLF2Grade8eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTLF2Grade8eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId8%>'  ToolTip="Click here to download">Click here to download the STLF 2</asp:LinkButton>
</p>


<b><ul><li>Student-Teacher Linkage Form (STLF)</b>&nbsp;–&nbsp;The forms list the selected students and their teachers. Please make sure students and teachers are linked correctly on the form. Instructions on how to access and review the forms were emailed to you and are in the <b>Documents</b> section on this website.</li></ul>
<p>Please note the following:</p>
<p>If the students and teacher linkages appear fine in the STLF, no need to contact the TIMSS help desk. Three business days later, please return to this page. Two new links will be shown here to download STF1 and STF2 (Student Tracking Forms). This is the form you can review to see if any student names, information, new enrollees, no longer enrolled students, should be updated. Your Test Administrator will contact you to discuss this. Do not contact the TIMSS help desk unless there are major updates needed on this form. You can discuss the minor updates with your Test Administrator when they call you in the pre-assessment call.</p>

<p>----------------------------------------------------------------</p>

<p>
    <b>Student Tracking Form (download both if more than one link is shown)</b>
    <br />
    <asp:Image ID="ImageSTF1Grade8eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTF1Grade8eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId8%>'  ToolTip="Click here to download">Click here to download the STF 1</asp:LinkButton>
     <br />
    <asp:Image ID="ImageSTF2Grade8eTIMSS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTF2Grade8eTIMSS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId8%>'  ToolTip="Click here to download">Click here to download the STF 2</asp:LinkButton>
</p>

<ul>
    <li><b>Student Tracking Form (STF)</b>&nbsp;–&nbsp;The forms list the students who have been selected to participate in the assessment at your school. Instructions on how to access and review the forms are in the <b>Documents</b> section on this website.</li>
    <li><b>Sample Parent Notification Letter</b> and <b>Facts for Parents about TIMSS (Parent FAQ)</b>&nbsp;–&nbsp;You will receive a packet of TIMSS materials including a sample parent/guardian notification letter and a TIMSS Parent FAQ (not for Indiana schools).</li> 

    <p>(<font color="red">If you are a school in Indiana, please contact TIMSS@westat.com;</font> do not use the instructions below).</p>
    <p> For non-Indiana schools, a sample parent/guardian notification letter is in the <b>Documents</b> section for you to customize and print on your school letterhead. Please distribute the parent notification letter and the Facts for Parents about TIMSS (from our mailing to you) to the parents of the selected students prior to the assessment. A Spanish version of the letter is also available in the <b>Documents</b> page. You can also share the Parent FAQ link available here: 
    <a href="https://nces.ed.gov/timss/parents/" target="_blank">https://nces.ed.gov/timss/parents/</a>.  </p>
    <p>As your assessment date approaches, your Test Administrator will be contacting you to finalize the assessment day activities.</p>
   </ul>

<%--    <asp:Panel ID="PanelUpdateSTLFFileGrade8eTIMSS" runat="server" Visible="false" style="border-style:dashed; margin:5px; padding:5px; border-color:#008CB3;">
    <h3>Replace TIMSS ID&nbsp;<asp:Label ID="TIMSSIDGrade8eTIMSS" runat="server" /> STLF file</h3>
    
<asp:FileUpload ID="FileUploadGrade8eTIMSS" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="upload8etimssstlf"
            ControlToValidate="FileUploadGrade8eTIMSS" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx, .zip files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX|zip|ZIP)$"
                            ControlToValidate="FileUploadGrade8eTIMSS" Display="Dynamic" ValidationGroup="upload8etimssstlf"></asp:RegularExpressionValidator>
        <br /><br />
         &nbsp;<asp:Button ID="ButtonUploadGrade8eTIMSS" runat="server" Text="Upload" ValidationGroup="upload8etimssstlf" />
         <asp:Button ID="ButtonRemoveGrade8eTIMSS" runat="server" Text="Remove" OnClientClick="return confirm('Are you sure?');" />
          <asp:Label ID="LabelFileUploadGrade8eTIMSS" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
</asp:Panel>--%>
</asp:Panel>






<asp:Panel ID="panelgrade8ICILS" runat="server" Width="96%">
<h4>Grade 8</h4>

<p>Thank you for providing information about all the eighth-grade students and teachers at your school! After submitting both student and teacher lists, you will receive an email telling you that your sample of students and teachers has been selected. Return to this webpage when you receive the email from ICILS@westat.com: “The list of students and teachers selected to participate in ICILS is available in the Student Teacher Form (STF) and Teacher Tracking Form (TTF),” which are available below.</p>
   
<p><asp:Image ID="ImageSTFGrade8ICILS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileSTFGrade8ICILS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId8%>'  ToolTip="Click here to download">Click here to download the STF</asp:LinkButton></p>


    <%--<asp:Panel ID="PanelUpdateSTLFFileGrade8" runat="server" Visible="false" style="border-style:dashed; margin:5px; padding:5px; border-color:#008CB3;">
    <h3>Replace TIMSS ID&nbsp;<asp:Label ID="TIMSSIDSTLFGrade8" runat="server" /> STLF file</h3>

<asp:FileUpload ID="FileUploadSTLFGrade8" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1STLF" runat="server" ValidationGroup="upload8stlf"
            ControlToValidate="FileUploadSTLFGrade8" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="RegularExpressionValidator1STLF" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx, .zip files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX|zip|ZIP)$"
                            ControlToValidate="FileUploadSTLFGrade8" Display="Dynamic" ValidationGroup="upload8stlf"></asp:RegularExpressionValidator>
        <br /><br />
         &nbsp;<asp:Button ID="ButtonUploadSTLFGrade8" runat="server" Text="Upload" ValidationGroup="upload8stlf" />
         <asp:Button ID="ButtonRemoveSTLFGrade8" runat="server" Text="Remove" OnClientClick="return confirm('Are you sure?');" />
          <asp:Label ID="LabelFileUploadSTLFGrade8" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
</asp:Panel>--%>

<p><asp:Image ID="ImageTTFGrade8ICILS" runat="server" />&nbsp;<asp:LinkButton ID="LinkButtonViewFileTTFGrade8ICILS" runat="server" CommandName="Downloadfile" CommandArgument='<%# TimssBll.GradeId8%>'  ToolTip="Click here to download">Click here to download the TTF</asp:LinkButton></p>

    <%--<asp:Panel ID="PanelUpdateTTFFileGrade8" runat="server" Visible="false" style="border-style:dashed; margin:5px; padding:5px; border-color:#008CB3;">
    <h3>Replace TIMSS ID&nbsp;<asp:Label ID="TIMSSIDTTFGrade8" runat="server" /> TTF file</h3>

<asp:FileUpload ID="FileUploadTTFGrade8" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1TTF" runat="server" ValidationGroup="upload8ttf"
            ControlToValidate="FileUploadTTFGrade8" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="RegularExpressionValidator1TTF" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx, .zip files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX|zip|ZIP)$"
                            ControlToValidate="FileUploadTTFGrade8" Display="Dynamic" ValidationGroup="upload8ttf"></asp:RegularExpressionValidator>
        <br /><br />
         &nbsp;<asp:Button ID="ButtonUploadTTFGrade8" runat="server" Text="Upload" ValidationGroup="upload8ttf" />
         <asp:Button ID="ButtonRemoveTTFGrade8" runat="server" Text="Remove" OnClientClick="return confirm('Are you sure?');" />
          <asp:Label ID="LabelFileUploadTTFGrade8" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
</asp:Panel>--%>

Please note the following:
<ul>
    <li><b>Student Tracking Form (STF)</b>&nbsp;–&nbsp;The forms list the students who have been selected to participate in the assessment at your school. Instructions on how to access and review the forms were emailed to you and are in the <b>Documents</b> section on this website.</li>
    <li><b>Teacher Tracking Form (TTF)</b>&nbsp;–&nbsp;The forms list the teachers who have been selected to participate in the online teacher questionnaire at your school. Instructions on how to access and review the forms were emailed to you and are in the <b>Documents</b> section on this website.</li>
    <li><b>Sample Parent Notification Letter</b> and <b>Facts for Parents about ICILS (Parent FAQ)</b>&nbsp;–&nbsp;A sample parent/guardian notification letter is in the <b>Documents</b> section for you to customize and print on your school letterhead.  Please distribute the parent notification letter and the <i>Facts for Parents about ICILS 2018 (Parent FAQ)</i> to the parents of the selected students prior to the assessment. A Spanish version of the letter is also available.</li>
</ul>
<p>If you are not a school in Indiana, please distribute the Parent FAQ electronically or share the Parent FAQ link available here: <a href="https://nces.ed.gov/surveys/icils/parents/" target="_blank">https://nces.ed.gov/surveys/icils/parents/</a>. If you would like paper copies of the Parent FAQ, please request them from <a href="mailto:ICILS@westat.com">ICILS@westat.com</a>. For Indiana Schools, please contact <a href="mailto:ICILS@westat.com">ICILS@westat.com</a>. Do not use the link above. </p>
<p>As your assessment date approaches, your Test Administrator will be contacting you to finalize the assessment day activities.</p>
</asp:Panel>




                
 
 <%--<asp:Panel ID="panelgrade12" runat="server">
<h4>&nbsp;&nbsp;Grade 12</h4>
    <asp:Image ID="ImageSTLFGrade12" runat="server" />
<asp:HyperLink ID="HyperLinkDownloadSTLFGrade12" runat="server" ToolTip="Click here to download">Click here to download the STLF.</asp:HyperLink>
<br />

   <asp:Panel ID="PanelUpdateSTLFFileGrade12" runat="server" Visible="false" style="border-style:dashed; margin:5px; padding:5px; border-color:#008CB3;">
    <h3>Replace TIMSS ID&nbsp;<asp:Label ID="TIMSSIDGrade12" runat="server" /> STLF file</h3>

<asp:FileUpload ID="FileUploadGrade12" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="upload12"
            ControlToValidate="FileUploadGrade12" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="RegularExpressionValidator2" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx, .zip files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX|zip|ZIP)$"
                            ControlToValidate="FileUploadGrade12" Display="Dynamic" ValidationGroup="upload12"></asp:RegularExpressionValidator>
        <br /><br />
         &nbsp;<asp:Button ID="ButtonUploadGrade12" runat="server" Text="Upload" ValidationGroup="upload12" />
         <asp:Button ID="ButtonRemoveGrade12" runat="server" Text="Remove" OnClientClick="return confirm('Are you sure?');" />
          <asp:Label ID="LabelFileUploadGrade12" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
</asp:Panel>
</asp:Panel>--%>


</asp:Content>

