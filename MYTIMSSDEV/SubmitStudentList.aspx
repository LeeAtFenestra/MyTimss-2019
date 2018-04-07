<%@ Page Title="Submit Student List" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SubmitStudentList.aspx.vb" Inherits="SubmitStudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>

        //<-- hide this script from non-javascript-enabled browsers
        var imgEdit = new hoverbutton('edit', '<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', true);
        //-->	
        function BatchDateFormatUpdate() {
            var selects = document.getElementsByTagName("select");
            var suffix = '$db_NaepCodeId';
            var batch = document.getElementById("ctl00_cphBody_DropDownListDateFormat");
            //alert('selects =' + selects.length);
            for (var i = 0; i < selects.length; i++) {
                if (selects[i].name.indexOf(suffix, this.length - suffix.length) !== -1) {
                    //alert('selects[' + i + '] =' + selects[i].name);
                    selects[i].selectedIndex = batch.selectedIndex;
                }
            }
        }

        function ValidateColumnMapping() {
            //alert('asdfsdaf');
            var selects = document.getElementsByTagName("select");
            var suffix = '$db_NaepLabelId';
            //var arr = new Array();
            var arr = [];  // empty array
            for (var i = 0; i < selects.length; i++) {
                if (selects[i].name.indexOf(suffix, this.length - suffix.length) !== -1) {
                    var column = selects[i].options[selects[i].selectedIndex].text;
                    if (alreadyExists(arr, column) == false) {
                        //alert('selects[' + i + '] =' + column);
                        arr.push(column);
                    }
                    else {
                        if (column != 'N/A') {
                            selects[i].focus();
                            alert(column + ' cannot be selected more than once');
                            return false;
                        }
                    }
                }
            }
            //alert(arr.length);
            ClearEdited();
            return true;
        }

        function alreadyExists(arr, value) {
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] == value) {
                    return true;
                }
            }
            return false;
        }
  </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <input type="hidden"  name="edited" value = "0" size="5">
<input type="hidden"  name="reader" value = 'False' size="8">
                
                <h3>Submit Student List</h3>
     <asp:Label ID="lblNotAvailable"  Visible="false"  runat="server" Text="The page will go live in mid-January, 2018, after we have emailed you instructions for submitting lists."></asp:Label>        
    <asp:Panel ID="PanelGrade4ClassListNeeded" runat="server" Visible="false">
    
    <b>&nbsp;Grade 4</b>
    <br />
    <br />
    You need to submit your class list before you can submit your student list.
    </asp:Panel>

    <asp:Panel ID="PanelGrade4" runat="server" Visible="false">
    
    <b>&nbsp;Grade 4 File</b>
        <br />
    <br />
    <p>Along with the list of classes, TIMSS needs a complete and current list of <b>all 4<sup>th</sup> grade students</b> in the selected grade 4 classes in order to draw a random sample of classes (and therefore students) to participate in the assessment. Your student data electronic file (E-File) must be submitted as a Microsoft Excel file.</p>
    <p>You may use the TIMSS E-File Excel Template or you may provide an Excel file with the same information. Include the following information for each student:</p>
    <p><b><U>The first 8 fields below are REQUIRED in your student list file (especially exact Class Names that you entered in the previous Submit Class Lists page).</b></U></p>
    <ul>
        <li>Class Name: The fourth-grade class the student is enrolled in. Please use the same unique class names you used in the Submit Class List screen.</li>
        <li>Mathematics Teacher Name: Mathematics Teacher of the above named class.</li>
        <li>Mathematics Teacher Email of Class</li>
        <li>Student Name: The preferred format is First Name, Middle Name (or Initial), and Last Name in separate columns. However, TIMSS will accept student names in one column.</li>
        <li>Sex: Codes for Male/Female.</li>
        <li>Date of Birth: The preferred format is Month of Birth and Year of Birth in separate columns. However, TIMSS will accept Date of Birth in one column.</li>
        <li>Student with a Disability Status (Use 1=Yes, student has disability and/or IEP; 2=No, student does not have disability or IEP)</li>
        <li>English Language Learner (Use 1=Yes, student is ELL; 2=No, student is formerly ELL; 3=No, student is not ELL)</li>
        <li>Science Teacher Name: Science teacher of selected class (if different from mathematics Teacher)</li>
        <li>Science Teacher Email: Science teacher email of selected class (if different from mathematics teacher)</li>
    </ul>
    It is preferred that you include column headers as the first row in your E-File (see templates for examples in <a href="documents.aspx">Documents</a>).  However, E-Files without column headers will be accepted. If you cannot submit your student data with this information in an Excel file, please call or email the TIMSS Help Desk at <a href="mailto:TIMSS@westat.com">TIMSS@westat.com</a> or 1-855-445-5604. 
<br />
<br />
        <p>(1. All students in selected classes should be entered on the same excel worksheet in the same excel workbook file.<br/>
 2. All students on the excel file should be in the selected grade only).</p>
                        		        <table border="1" cellpadding="4" cellspacing="0">
        
            <asp:Repeater ID="RepeaterGrade4Uploads" runat="server">
                <HeaderTemplate>
				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>Filename</B></Font></td>
				        <td align="center"><Font size=2><B>Filesize</B></Font></td>
				        <td align="center"><Font size=2><B>Uploaded</B></Font></td>
				        <td align="center"><Font size=2><B>Uploaded By</B></Font></td>
                        

				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td><asp:LinkButton ID="LinkButton1" CommandName="editfile4" CommandArgument='<%#Container.DataItem("FileId")%>' runat="server"><%#Container.DataItem("UserFilePath")%></asp:LinkButton></td>
				      <td><%#Container.DataItem("Filesize")%></td>
				      <td><%#Container.DataItem("UploadDT")%></td>
				      <td><%#Container.DataItem("UploadedByFirstAndLastName")%></td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater> 
        </table>
        
        <br />
        <br />

        Does your student data file contain <a class="links" href="Javascript:alert('A column header identifies the type of student data in a column (ie. Student Name, Sex, Race, etc...).');">column headers</a>:
               <asp:DropDownList ID="DropDownListColumnHeadersGrade4" runat="server" CssClass="efiledropdownlist">
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
        &nbsp;<b>New file: </b><asp:FileUpload ID="FileUploadGrade4" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="valFileUpload" runat="server" ValidationGroup="upload"
            ControlToValidate="FileUploadGrade4" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="regexpvalFileExtension" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx,  files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX)$"
                            ControlToValidate="FileUploadGrade4" Display="Dynamic" ValidationGroup="upload"></asp:RegularExpressionValidator>
        <br /><br />
         &nbsp;<asp:Button ID="ButtonUploadGrade4" runat="server" Text="Upload" ValidationGroup="upload" /> <asp:Label ID="LabelFileUploadGrade4" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />

    </asp:Panel>
    
    <asp:Panel ID="PanelGrade8ClassListNeeded" runat="server" Visible="false">
    
    <b>&nbsp;Grade 8</b>
    <br />
    <br />
    You need to submit your class list before you can submit your student list.
    </asp:Panel>
    <asp:Panel ID="PanelGrade8" runat="server" Visible="false">
        
<asp:Panel ID="PanelICILSUploadInstructions" runat="server" Visible="false">
    <p>ICILS needs a complete and current list of <b>all 8<sup>th</sup> grade students</b> in the selected grade 8 in order to draw a random sample of students to participate in the assessment. Your student data electronic file (E-File) must be submitted as a Microsoft Excel file.</p>
    <p>You may use the ICILS Student E-File Excel Template or you may provide an Excel file with the same information. Include the following information for each student:</p>
    <ul>
        <li>Student Name: The preferred format is First Name, Middle Name (or Initial), and Last Name in separate columns. However, ICILS will accept student names in one column. </li>
        <li>Class Name:  Name of homeroom class student is in.  If your school does not have homeroom, please use English/Language Arts class.  The class name must be a class that is mutually exclusive and exhaustive, i.e. each 8th-grader belongs to one and only one of these classes.</li>
        <li>Sex:  Codes for Male/Female. </li>
        <li>Date of Birth: The preferred format is Month of Birth and Year of Birth in separate columns. However, ICILS will accept Date of Birth in one column. </li>
        <li>Student with a Disability status</li>
        <li>English Language Learner status</li>
    </ul>
    It is preferred that you include column headers as the first row in your Student E-File (see templates for examples in <a href="documents.aspx">Documents</a>). 
    However, E-Files without column headers will be accepted. If you cannot submit your student data with this information in an Excel file, please call or email the ICILS Help Desk at <a href="mailto:ICILS@westat.com">ICILS@westat.com</a> or 1-855-445-5604. 
        
</asp:Panel>
<asp:Panel ID="PanelTIMSSUploadInstructions" runat="server" Visible="false">
    <p>Along with the list of classes, TIMSS needs a complete and current list of <b>all 8<sup>th</sup>-grade students</b> in the selected grade 8 classes in order to draw a random sample of classes (and therefore students) to participate in the assessment.  Your student data electronic file (E-File) must be submitted as a Microsoft Excel file.</p>
    <p>You may use the TIMSS E-File Excel Template or you may provide an Excel file with the same information.  Include the following information for each student:</p>
    <P><b><u>All 9 fields below are REQUIRED in your student list file (especially specific Mathematics Class Names that you entered in the previous Submit Class Lists page).</b></u></P>
    <ul>
        <li>Mathematics Class Name:  The mathematics class the student is enrolled in.  Please use the same class names you used in Submit Class List</li>
        <li>Mathematics Teacher Name:  Teacher of the above named class that the student is taking.</li>
        <li>Student Name:  The preferred format is First Name, Middle Name (or Initial), and Last Name in separate columns.  However, TIMSS will accept student names in one column.</li>
        <li>Sex</li>
        <li>Date of Birth:  The preferred format is Month of Birth and Year of Birth in separate columns.  However, TIMSS will accept Date of Birth in one column.</li>
        <li>Student with a Disability (SD) Status (Use 1=Yes, student has disability and/or IEP; 2=No, student does not have diability or IEP)</li>
        <li>English Language Learner (ELL) Status</li>
        <li>Science Teacher Name: You can add more columns if the student has multiple science teachers</li>
        <li>Science Teather Email: You can add more columns if the student has multiple science teachers</li>
        <!--
        <li>Student with a Disability status</li>
        <li>English Language Learner status</li>
        -->
    </ul>
        It is preferred that you include column headers as the first row in your E-File (see templates for examples in <a href="documents.aspx">Documents</a>).  However, E-Files without column headers will be accepted.

If you cannot submit your student data with this information in an Excel file, please call or email the TIMSS Help Desk at <a href="mailto:TIMSS@westat.com">TIMSS@westat.com</a> or 1-855-445-5604. 
</asp:Panel>
<br />
<br />
        <p>(1. All students in selected classes should be entered on the same excel worksheet in the same excel workbook file.<br/>
 2. All students on the excel file should be in the selected grade only).</p>
                        		        <table border="1" cellpadding="4" cellspacing="0">

    <asp:Repeater ID="RepeaterGrade8Uploads" runat="server">
                <HeaderTemplate>
				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>Filename</B></Font></td>
				        <td align="center"><Font size=2><B>Filesize</B></Font></td>
				        <td align="center"><Font size=2><B>Uploaded</B></Font></td>
				        <td align="center"><Font size=2><B>Uploaded By</B></Font></td>
                        

				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td><asp:LinkButton ID="LinkButton1" CommandName="editfile8" CommandArgument='<%#Container.DataItem("FileId")%>' runat="server"><%#Container.DataItem("UserFilePath")%></asp:LinkButton></td>
				      <td><%#Container.DataItem("Filesize")%></td>
				      <td><%#Container.DataItem("UploadDT")%></td>
				      <td><%#Container.DataItem("UploadedByFirstAndLastName")%></td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater> 

        </table>
        
        <br />
        <br />

        Does your student data file contain <a class="links" href="Javascript:alert('A column header identifies the type of student data in a column (ie. Student Name, Sex, Race, etc...).');">column headers</a>:
               <asp:DropDownList ID="DropDownListColumnHeadersGrade8" runat="server" CssClass="efiledropdownlist">
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
        &nbsp;<b>New file: </b><asp:FileUpload ID="FileUploadGrade8" runat="server" Width="500px" />&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidatorFileUploadGrade8" runat="server" ValidationGroup="upload"
            ControlToValidate="FileUploadGrade8" ErrorMessage="*Please select a file.">
        </asp:RequiredFieldValidator> 
        <asp:RegularExpressionValidator  id="RegularExpressionValidatorFileUploadGrade8" runat="server" 
                            ErrorMessage="<br/>*Only .xls, .xlsx,  files are allowed." 
                            ValidationExpression="^.*\.(xls|XLS|xlsx|XLSX)$"
                            ControlToValidate="FileUploadGrade8" Display="Dynamic" ValidationGroup="upload"></asp:RegularExpressionValidator>
                             <br /><br />
         &nbsp;
         <asp:Button ID="ButtonUploadGrade8" runat="server" Text="Upload" ValidationGroup="upload" /> <asp:Label ID="LabelFileUploadGrade8" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
    </asp:Panel>
    
    
    <asp:Panel ID="PanelGrade12" runat="server" Visible="false">
    <b>&nbsp;Grade 12 File</b>
    <br />
    <br />
    <p>TIMSS needs a complete and current list of <b>all 12th-grade students who have taken or are currently taking eligible advanced mathematics and physics courses</b> in order to draw a random sample of students to participate in the TIMSS Advanced assessment.  Eligible courses for your school were sent to you via email.  Your student data electronic file (E-File) must be submitted as a Microsoft Excel file.</p>
    <p>Include the following information for each student:</p>
    <ul>
        <li>Student Name:  The preferred format is First Name, Middle Name (or Initial), and Last Name in separate columns.  However, TIMSS Advanced will accept student names in one column.</li>
        <li>Student ID:  school or state ID.  Please do not use social security numbers.</li>
        <li>Date of Birth:  The preferred format is Month of Birth, Date of Birth, and Year of Birth in separate columns.  However, TIMSS Advanced will accept Date of Birth in one column.</li>
        <li>Sex</li>
        <li>Name of <i>each</i> eligible advanced mathematics course taken</li>
        <li>Grade level when <i>each</i> advanced mathematics course taken</li>
        <li>Teacher name of each advanced mathematics course</li>
        <li>Name of  <i>each</i> physics course taken</li>
        <li>Grade level when <i>each</i> physics course taken</li>
        <li>Teacher name of <i>each</i> physics course</li>
    </ul>
    <p>It is preferred that you include column headers as the first row in your E-File. However, E-Files without column headers will be accepted.  You may use one of the example templates in the <a href="documents.aspx">Documents</a> section.</p>
    <p>If you cannot submit your student data with this information in an Excel file, please email or call the TIMSS E-File Help Desk at <a href="mailto:TIMSSefile@westat.com">TIMSSefile@westat.com</a> or 1-855-457-1577.</p>
                        		        <table border="1" cellpadding="4" cellspacing="0">

    <asp:Repeater ID="RepeaterGrade12Uploads" runat="server">
                <HeaderTemplate>
				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>Filename</B></Font></td>
				        <td align="center"><Font size=2><B>Filesize</B></Font></td>
				        <td align="center"><Font size=2><B>Uploaded</B></Font></td>
				        <td align="center"><Font size=2><B>Uploaded By</B></Font></td>
                        

				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td><%#Container.DataItem("UserFilePath")%></td>
				      <td><%#Container.DataItem("Filesize")%></td>
				      <td><%#Container.DataItem("UploadDT")%></td>
				      <td><%#Container.DataItem("UploadedByFirstAndLastName")%></td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater> 
    
            
        </table>


        <br />
        <br />

        

        Does your student data file contain <a class="links" href="Javascript:alert('A column header identifies the type of student data in a column (ie. Student Name, Sex, Race, etc...).');">column headers</a>:
               <asp:DropDownList ID="DropDownListColumnHeadersGrade12" runat="server" CssClass="efiledropdownlist">
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
        &nbsp;<b>New file: </b><asp:FileUpload ID="FileUploadGrade12" runat="server" Width="500px" />&nbsp;
      
                             <br /><br />
         &nbsp;

        <asp:Button ID="ButtonUploadGrade12" runat="server" Text="Upload" ValidationGroup="upload" /> <asp:Label ID="LabelFileUploadGrade12" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />

        <p>Thank you for submitting your student list!  The TIMSS Advanced Team will contact you in a few weeks with information about the students selected for the assessment.</p>
    </asp:Panel>
    
    <asp:Panel ID="PanelEditFile" runat="server" Visible="false">
    <div style="text-align:right">
    <img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15"><font size="2">&nbsp;&nbsp;
</div>

    <asp:Panel ID="PanelColumns" runat="server" Visible="false">
    <b>Identify Your Columns</b>

<br />
<br />
We need to know what information is in each column of your student data file (E-File). If you provided column headers in your E-File (preferred), they are displayed in <b>Column Heading Is</b> in the table below. If you did not provide column headers, <b>Column Heading Is</b> will contain numbers for each column in your submitted E-File. Click on the down arrows in <b>Your Column Contains</b> to select descriptions for each column header. If there is no appropriate description in the drop-down list, please select N/A.
<br />
<br />
                        		        <table border="1" cellpadding="4" cellspacing="0" style="width:550px">
        
            <asp:Repeater ID="RepeaterIdentifyColumns" runat="server">
                <HeaderTemplate>
				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>Column Heading Is</B></Font></td>
				        <td align="center"><Font size=2><B>Your Column Contains</B></Font></td>
                        

				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td><%#Container.DataItem("UserColumnLabel")%></td>
				      <td>

                                
                                <asp:HiddenField ID="UserColumnLabel" runat="server" Value='<%#Container.DataItem("UserColumnLabel")%>' />
                                <asp:HiddenField ID="UserColumnId" runat="server" Value='<%#Container.DataItem("UserColumnId")%>' />
                                <asp:HiddenField ID="NaepLabelId" runat="server" Value='<%#Container.DataItem("NaepLabelId")%>' />
                                <asp:HiddenField ID="db_EditDT" runat="server" Value='getdate()' />
                                
                                <asp:DropDownList ID="db_NaepLabelId" runat="server" onchange = "Edited();" DataTextField="name" DataValueField="value" DataSource='<%#TimssBLL.GetNaepColumnLabels(me.theFile) %>' SelectedValue='<%#Container.DataItem("NaepLabelId")%>'>
                                </asp:DropDownList>
                                
                                                <asp:RequiredFieldValidator ID="NaepLabelIdRequired" runat="server" ControlToValidate="db_NaepLabelId"
                                                    ErrorMessage="TIMSS Column Mapping is required." ToolTip="TIMSS Column Mapping is required.">*</asp:RequiredFieldValidator>
                                                    </td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater> 

        </table>
        <br />
                            <asp:Button ID="ButtonColumnsPrevious" runat="server" Text="Previous" onclientclick="return ValidateColumnMapping();" CausesValidation="false" />
                    <asp:Button ID="ButtonColumnsNext" runat="server" Text="Next" onclientclick="return ValidateColumnMapping();" /> <asp:Label ID="LabelError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" />
        </asp:Panel>

        
        <asp:Panel ID="PanelValues" runat="server" Visible="false">
        <%--<b>Match Your Values to <%=TimssBll.ProjectName()%> Codes</b>--%>
        <asp:Panel ID="PanelValuesInstructionseTIMSS" runat="server">
        <p>TIMSS also needs to know the values for each of the columns in your E-File. The tables on the next few web pages contain the following information for each column:</p>
        <ul>
            <li>Your Values: The values in your E-File</li>
            <li>Codes: Click on the down arrow and select the Code that best matches Your Values</li>
            <li>Number: The number of students in your E-File with that value</li>
            <li>Percentage: The percentage in your E-File with that value</li>
        </ul>
        <p>Use the Previous and Next buttons to navigate from column header to column header.</p>
        </asp:Panel>
        <asp:Panel ID="PanelValuesInstructionsICILS" runat="server">
        <p>ICILS also needs to know the values for each of the columns in your E-File. The tables on the next few web pages contain the following information for each column:</p>
        <ul>
            <li>Your Values: The values in your Student E-File</li>
            <li>ICILS Codes: Click on the down arrow and select the ICILS Code that best matches Your Values</li>
            <li>Number: The number of students in your E-File with that value</li>
            <li>Percentage: The percentage in your Student E-File with that value </li>
        </ul>
        <p>Use the Previous and Next buttons to navigate from column header to column header.</p>
        </asp:Panel>
        <p>If there is an error in your data file, exit E-File, correct the problem in your Excel file, and upload the file again.</p>
        <p><b><asp:Label ID="LabelColumn" runat="server" /></b> - <asp:Label ID="LabelValueMappingProgress" runat="server" Font-Bold="true" /></p>
        
        <asp:Panel ID="PanelDateFormat" runat="server" Visible="false"><p>
        Update All Codes: <asp:DropDownList ID="DropDownListDateFormat" runat="server" DataTextField="name" DataValueField="value"  onchange = "BatchDateFormatUpdate();"></asp:DropDownList>
                </p>
        </asp:Panel>
                        		        <table border="1" cellpadding="4" cellspacing="0" style="width:550px">

        
            <asp:Repeater ID="RepeaterMapValues" runat="server">
                <HeaderTemplate>
				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>Your Values</B></Font></td>
				        <td align="center" runat="server" visible='<%#IsClassColumn() %>'><Font size=2><B>Class List</B></Font></td>
				        <td align="center" runat="server" visible='<%#Not IsClassColumn() %>'><Font size=2><B>Codes</B></Font></td>
				        <td id="Td7" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><Font size=2><B>Number</B></Font></td>
				        <td id="Td8" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><Font size=2><B>Percentage</B></Font></td>
				        <td id="Td9" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><Font size=2><B>Number</B></Font></td>
				        <td id="Td10" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><Font size=2><B>Percentage</B></Font></td>
				        <td id="Td11" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><Font size=2><B>Number</B></Font></td>
				        <td id="Td12" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><Font size=2><B>Percentage</B></Font></td>
                        

				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                    
				      <tr>
				      <td>
                      <%#Container.DataItem("Response")%>
                      <asp:HiddenField ID="currentresponsefreq" runat="server" Value='<%#Container.DataItem("Response")%>' />
                      <asp:HiddenField ID="ResponseFreqId" runat="server" Value='<%#Container.DataItem("ResponseFreqId")%>' />
                      </td>
				      <td runat="server" visible='<%#IsClassColumn() %>'>
                                <asp:HiddenField ID="ClassListingFormId" runat="server" Value='<%#Container.DataItem("ClassListingFormId")%>' />
                                <asp:HiddenField ID="dbcl_EditDT" runat="server" Value='getdate()' />
                                <asp:DropDownList ID="dbcl_ClassListingFormId" runat="server" onchange = "Edited();" DataTextField="name" DataValueField="value" DataSource='<%#TimssBLL.GetClassListingFormNameValuePairList(Container.DataItem("ID")) %>' SelectedValue='<%#Container.DataItem("ClassListingFormId")%>'>
                                </asp:DropDownList>
                                
                                                <asp:RequiredFieldValidator ID="ClassListingFormIdRequired" runat="server" ControlToValidate="dbcl_ClassListingFormId"
                                                    ErrorMessage="TIMSS Class Mapping is required." ToolTip="TIMSS Class Mapping is required.">*</asp:RequiredFieldValidator>
                                </td>
				      <td runat="server" visible='<%#Not IsClassColumn() %>'>
                                <asp:HiddenField ID="NaepCodeId" runat="server" Value='<%#Container.DataItem("NaepCodeId")%>' />
                                <asp:HiddenField ID="db_EditDT" runat="server" Value='getdate()' />
                                <asp:DropDownList ID="db_NaepCodeId" runat="server" onchange = "Edited();" DataTextField="name" DataValueField="value" DataSource='<%#TimssBLL.GetNaepCodes(Container.DataItem("NaepLabelId")) %>' SelectedValue='<%#Container.DataItem("NaepCodeId")%>'>
                                </asp:DropDownList>
                                
                                                <asp:RequiredFieldValidator ID="NaepCodeIdRequired" runat="server" ControlToValidate="db_NaepCodeId"
                                                    ErrorMessage="Code Mapping is required." ToolTip="Code Mapping is required.">*</asp:RequiredFieldValidator>
                                </td>
				      <td id="Td13" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><%#Container.DataItem("TotalResponsesGrade4")%></td>
				      <td id="Td14" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><%#TimssBll.CalcPercentage(Container.DataItem("TotalResponsesGrade4"), Me.theFile.TotalRows, 2)%>%</td>
				      <td id="Td15" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><%#Container.DataItem("TotalResponsesGrade8")%></td>
				      <td id="Td16" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><%#TimssBll.CalcPercentage(Container.DataItem("TotalResponsesGrade8"), Me.theFile.TotalRows, 2)%>%</td>
				      <td id="Td17" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><%#Container.DataItem("TotalResponsesGrade12")%></td>
				      <td id="Td18" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><%#TimssBll.CalcPercentage(Container.DataItem("TotalResponsesGrade12"), Me.theFile.TotalRows, 2)%>%</td>
					  </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater> 
        </table>
        <br />
                    <asp:Button ID="ButtonValuesPrevious" runat="server" Text="Previous" onclientclick="ClearEdited();" CausesValidation="false" />
                    <asp:Button ID="ButtonValuesNext" runat="server" Text="Next" onclientclick="ClearEdited();" /> <asp:Label ID="Label1" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" onclientclick="ClearEdited();" />
        </asp:Panel>
        
        <asp:Panel ID="PanelVerify" runat="server" Visible="false">
        
        <asp:Panel ID="PanelVerifyInstructionseTIMSS" runat="server">
            <b>Verify Your E-File</b>
                <br />
                <p>The table below summarizes the information you have provided on your student list (E-File). Please review this summary and verify that the information is correct. Total Enrollment at the bottom of the table should match the number of students in your E-File, and the total number of students currently enrolled in grade <%=Me.theFile.SmpGrd()%>.<p/>
                <p>Record whether the information is correct or incorrect by selecting the appropriate button at the bottom of the page. Then click the Submit button. If you select incorrect, you will need to correct your E-File and resubmit it.</p>
                
        </asp:Panel>
        
        <asp:Panel ID="PanelVerifyInstructionsICILS" runat="server">
            <b>Verify Your Student E-File</b>
                <br />
                <p>The table below summarizes the information you have provided on your student list (Student E-File). Please review this summary and verify that the information is correct. Total Enrollment at the bottom of the table should match the number of students in your E-File, and the total number of students currently enrolled in grade <%=Me.theFile.SmpGrd()%>.<p/>
                <p>Record whether the information is correct or incorrect by selecting the appropriate button at the bottom of the page. Then click the Submit button. If you select incorrect, you will need to correct your E-File and resubmit it.</p>
                
        </asp:Panel>
        
            <asp:Repeater ID="RepeaterVerify" runat="server">
                <HeaderTemplate>
                    
                
                        		        <table border="1" cellpadding="4" cellspacing="0" style="width:550px">

				        <tr style="background-color:#008BB0; color:#FFFFFF">
				        <td align="center"><Font size=2><B>&nbsp;</B></Font></td>
				        <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><Font size=2><B>Number</B></Font></td>
				        <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><Font size=2><B>Percentage</B></Font></td>
				        <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><Font size=2><B>Number</B></Font></td>
				        <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><Font size=2><B>Percentage</B></Font></td>
				        <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><Font size=2><B>Number</B></Font></td>
				        <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><Font size=2><B>Percentage</B></Font></td>
                        

				        </tr>
                </HeaderTemplate>
                <ItemTemplate>                      
				      <tr runat="server" visible='<%# HandleVerifyColumnHeaderVisibility(Container.DataItem("NaepLabel")) %>'>
				      <td colspan="7" bgcolor="#cccccc"><%#Container.DataItem("NaepLabel")%></td>
					  </tr>             
				      <tr>
				      <td>&nbsp;&nbsp;&nbsp;<%#Container.DataItem("CodeLabel")%></td>
				      <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><%#Container.DataItem("TotalResponsesGrade4")%></td>
				      <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><%#TimssBll.CalcPercentage(Container.DataItem("TotalResponsesGrade4"), Me.theFile.TotalRows, 2)%>%</td>
				      <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><%#Container.DataItem("TotalResponsesGrade8")%></td>
				      <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><%#TimssBll.CalcPercentage(Container.DataItem("TotalResponsesGrade8"), Me.theFile.TotalRows, 2)%>%</td>
				      <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><%#Container.DataItem("TotalResponsesGrade12")%></td>
				      <td align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><%#TimssBll.CalcPercentage(Container.DataItem("TotalResponsesGrade12"), Me.theFile.TotalRows, 2)%>%</td>
					  </tr>
                </ItemTemplate>
                
                <FooterTemplate>           
				      <tr bgcolor="#cccccc">
				      <td>Total Enrollment:</td>
				      <td id="Td1" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'><%#Me.theFile.TotalRows%></td>
				      <td id="Td2" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade4() %>'>&nbsp;</td>
				      <td id="Td3" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'><%#Me.theFile.TotalRows%></td>
				      <td id="Td4" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade8() %>'>&nbsp;</td>
				      <td id="Td5" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'><%#Me.theFile.TotalRows%></td>
				      <td id="Td6" align="center" runat="server" visible='<%#Me.theFile.FileHasGrade12() %>'>&nbsp;</td>
					  </tr>
        </table>
                </FooterTemplate>
            </asp:Repeater> 
            <table>
            <tr>
                <td colspan="7">
                <br />
                    <asp:RadioButtonList ID="RadioButtonListVerifyChoice" runat="server">
                    <asp:ListItem Value="1">Information is <b>CORRECT and CURRENT</b>.</asp:ListItem>
                    <asp:ListItem Value="0">Information is <b>INCORRECT</b>.</asp:ListItem>
                </asp:RadioButtonList> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="verify"
            ControlToValidate="RadioButtonListVerifyChoice" ErrorMessage="*Please select your verification option.">
        </asp:RequiredFieldValidator> 

                <br />
                <br />
                    <asp:Button ID="ButtonVerifyPrevious" runat="server" Text="Previous" onclientclick="ClearEdited();" CausesValidation="false" />
                    <asp:Button ID="ButtonVerifySubmit" runat="server" Text="Submit" onclientclick="ClearEdited();" ValidationGroup="verify" /> <asp:Label ID="Label2" runat="server" EnableViewState="false" ForeColor="Red" Visible="false" Font-Bold="true" onclientclick="ClearEdited();" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="PanelThanks" runat="server" Visible="false">
                <b>E-File: Next Steps</b>
                <br />
                <asp:Panel ID="PanelCorrect" runat="server" Visible="false">
                <p>Thank you for submitting your E-File!</p>
                <p>TIMSS will process your list submission and usually within 5-10 business days, you will receive an email with steps to proceed to the Prepare for Assessment section.</p>
                <p>A TIMSS representative will also contact you afterwards to make arrangements for the assessment.</p>
                </asp:Panel>
                
                <asp:Panel ID="PanelCorrectICILS" runat="server" Visible="false">
                <p>Thank you for submitting your E-File!</p>                
                <p>ICILS will process your submission and select the sample of <%=Me.theFile.SmpGrdString()%>-grade students to be assessed. You will soon receive an email explaining which students were selected.</p>
                <p>A Westat ICILS staff representative will contact you soon to make arrangements for the assessment. Please make sure to complete the Submit Teacher List page if you haven't already.</p>
                </asp:Panel>    

                <asp:Panel ID="PanelIncorrect" runat="server" Visible="false">
                <p>You have indicated that your student information is incorrect.   Please correct your E-File and resubmit.  If you need assistance, please contact the TIMSS Help Desk at <a href="mailto:TIMSS@westat.com">TIMSS@westat.com</a> or 855-445-5604.</p>
                </asp:Panel>                
                
                <asp:Panel ID="PanelIncorrectICILS" runat="server" Visible="false">
                <p>You have indicated that your student information is incorrect.   Please correct your E-File and resubmit.  If you need assistance, please contact the ICILS Help Desk at <a href="mailto:ICILS@westat.com">ICILS@westat.com</a> or 855-445-5604.</p>
                </asp:Panel>   

        </asp:Panel>
    </asp:Panel>
</asp:Content>

