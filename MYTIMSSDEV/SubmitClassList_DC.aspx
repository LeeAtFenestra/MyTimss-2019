<%@ Page Title="Submit Class List" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SubmitClassList.aspx.vb" Inherits="SubmitClassList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>

        //<-- hide this script from non-javascript-enabled browsers
        var imgEdit = new hoverbutton('edit', '<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', true);
        //-->	

  </script>

  <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/")%>Common/jquerypopup/jquery.min.js'></script>
    <STYLE type="text/css">
        .popbox {
    display: none;
    position: absolute;
    z-index: 99999;
    width: 400px;
    padding: 10px;
    background: #EEEFEB;
    color: #000000;
    border: 1px solid #4D4F53;
    margin: 0px;
    -webkit-box-shadow: 0px 0px 5px 0px rgba(164, 164, 164, 1);
    box-shadow: 0px 0px 5px 0px rgba(164, 164, 164, 1);
}
.popbox h2
{
    background-color: #4D4F53;
    color:  #E3E5DD;
    font-size: 14px;
    display: block;
    width: 100%;
    margin: -10px 0px 8px -10px;
    padding: 5px 10px;
}
    </STYLE>
<script>
    $(function () {
        var moveLeft = 0;
        var moveDown = 0;
        $('a.popper').hover(function (e) {

            var target = '#' + ($(this).attr('data-popbox'));

            $(target).show();
            moveLeft = $(this).outerWidth();
            moveDown = ($(target).outerHeight() / 2);
        }, function () {
            var target = '#' + ($(this).attr('data-popbox'));
            $(target).hide();
        });

        $('a.popper').mousemove(function (e) {
            var target = '#' + ($(this).attr('data-popbox'));

            leftD = e.pageX + parseInt(moveLeft);
            maxRight = leftD + $(target).outerWidth();
            windowLeft = $(window).width() - 40;
            windowRight = 0;
            maxLeft = e.pageX - (parseInt(moveLeft) + $(target).outerWidth() + 20);

            if (maxRight > windowLeft && maxLeft > windowRight) {
                leftD = maxLeft;
            }

            topD = e.pageY - parseInt(moveDown);
            maxBottom = parseInt(e.pageY + parseInt(moveDown) + 20);
            windowBottom = parseInt(parseInt($(document).scrollTop()) + parseInt($(window).height()));
            maxTop = topD;
            windowTop = parseInt($(document).scrollTop());
            if (maxBottom > windowBottom) {
                topD = windowBottom - $(target).outerHeight() - 20;
            } else if (maxTop < windowTop) {
                topD = windowTop + 20;
            }

            $(target).css('top', topD).css('left', leftD);


        });

    });
    function ExclusionWarning(drp) {
        var index = drp.selectedIndex;
        if (index > 0) {
            return confirm('Exclusion status must apply to all students in the class.\n\nIf even one student does not fall in this status then this selection should be removed.');
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <input type="hidden"  name="edited" value = "0" size="5">
<input type="hidden"  name="reader" value = 'False' size="8">
      

 <asp:Label ID="lblNotAvailable"  Visible="false"  runat="server" Text="The page will go live in mid-January, 2018, after we have emailed you instructions for submitting lists."></asp:Label>        

                <div style="text-align:right">
    <img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15"><font size="2">&nbsp;&nbsp;</font>
</div>

   <h3><asp:Label ID="labelHeaderGrade" runat="server" /></h3>
            <asp:Label ID="LabelDeleteComplete" runat="server" ViewStateMode="Disabled" Visible="false" ForeColor="Blue"><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*The class has been deleted!<br /><br /></div></asp:Label>
            <asp:Label ID="LabelSaveComplete" runat="server" ViewStateMode="Disabled" Visible="false" ForeColor="Blue"><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Your information has been saved.<br /><br /></div></asp:Label>
            <asp:Label ID="LabelFinished" runat="server" ViewStateMode="Disabled" Visible="false" ForeColor="Blue"><div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Thank you for submitting your class list.<br /><br /></div></asp:Label>
            
            <asp:Label ID="LabelError" runat="server" ViewStateMode="Disabled" Visible="false" ForeColor="Red">
                <div><table style="border:solid;border-color:red;border-width:2px;width:100%;padding:2em"><tr><td>
                    <p><asp:Label ID="lblErrorHeader" runat="server" Text="" /></p>
                    <p><asp:Label ID="lblRequiredFields" runat="server" Text="<li>*Please fill out each required field for every row and re-enter/retry again.</li>" /></p>
                    <p><asp:Label ID="lblClassNameError" runat="server" Text="<li>*The class name you entered was not unique. The name should be descriptive enough so you can differentiate and identify this class from all others.<br /><br />Please provide a unique class name in each row, in a format like: Subject-Teacher-Class-Period.<br />For example, Mathematics-Mrs. Johnson-Period 4</li>" /></p>
                    <p><asp:Label ID="lblClassSizeError" runat="server" Text="<li>*The class size you entered was over 75 students.</li>" /></p>
            </td></tr></table></div></asp:Label><asp:Panel ID="PanelGrade4" runat="server" Visible="false">
   
                <p>TIMSS needs a complete and current list of all of your school’s fourth-grade classes in order to randomly select classes to be assessed. Typically, two classes are sampled in each school, and all students in the selected classes will be assessed.</p><p>Enter the data for each class in the form below. After you have entered information for a class, click on save, and another row will appear for you to enter information about another class. Continue until you have entered information for all classes that contain fourth-grade students.</p><p>
                    Include the following information for each class (<b><font color="red">&#10038;</font></b> indicates a required field): <table border="1" cellpadding="15" cellspacing="0">
                        <tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Class Name (each class name must be unique)</td><td>Record the class name that is typically used by your school to refer to the class. For example, it may be that your school uses the grade plus a letter for the class name (4a, 4b, etc.), the grade plus a number (4.1, 4.2, etc.), the teacher name, the class period (Period 1, Period 2, etc.), the class location (Room 7, Room 8, etc.), or some other combination of these items. <b>It is important that unique class names are entered because these names will be used to indicate to the Test Administrator which classes will be tested.</b></td></tr><tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Class Group or Track (if applicable)</td><td>If your school assigns students to specific classes based on their ability, please indicate the relevant level: Low ability, Average ability, or High ability.</td></tr><tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Number of 4<sup>th</sup>-grade students</td><td>Enter the number of fourth-grade students in each class. In the case of <i>multi-grade classes</i> (e.g., students from more than one grade level in the same class), only the fourth-grade students should be counted as a class in the list. For example, if three Grade 3 students, five Grade 4 students, and ten Grade 5 students form a multi-grade class, then you should record five students for the number of students in this multi-grade class.</td></tr><tr valign="top">
                            <td>Class Exclusion Status (if applicable; if at least one 4th-grade student in this class is able to be assessed, do not use this field)</td><td>
                           
                            <b>1</b> = Students with functional disabilities; i.e., students who have physical disabilities in such a way that they cannot perform in the TIMSS testing situation. Students with functional disabilities who are able to perform should be accommodated in the test situation, within reason, rather than excluded. <br/><br/><b>2</b> = Students with intellectual disabilities; i.e., students who are considered, in the professional opinion of the school principal or by other qualified staff members, to have severe intellectual disabilities or who have been tested as such. This category includes students who are emotionally or mentally unable to follow even the general instructions of the test. Students should not be excluded solely because of poor academic performance or normal disciplinary problems. It should be noted that students with dyslexia, or other such learning disabilities, should be accommodated in the test situation, within reason, rather than excluded. <br/><br/><b>3</b> = Non-native language speakers; i.e., students who are unable to read or speak the language(s) of the test and would be unable to overcome the language barrier in the test situation. <br/><br/>If all students in the excluded class do not belong to the same exclusion category, please identify the category corresponding to the majority of students. </td></tr><tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Name of Mathematics Teacher</td><td>Name of the Mathematics Teacher of class.</td></tr><tr><td><b><font color="red">&#10038;</font></b>Email address of Mathematics Teacher</td><td>Email address of Mathematics Teacher of class.</td></tr><tr><td>Name of Science Teacher (only if different from Mathematics Teacher)</td><td>Name of Science Teacher of class (only if different from Mathematics Teacher).</td></tr><tr><td>Email address of Science Teacher (only if different from Mathematics Teacher)</td><td>Email address of Science Teacher of class (only if different from Mathematics Teacher).</td></tr></table><p></p>
                    Tips: <p>
                        <ul>
                            <li>Use the <i>Save</i> button to save information you have entered for each class.</li><li>If you need to delete a class, click on the <img border="0" 
                            src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/Critical.png' 
                            width="15"></img> at the beginning of a saved class row.</li><li>Use the <i>Print</i> button to print a list of the classes you included for 
                        reference.</li><li>Click on the <i>Finished</i> button when you have entered all fourth-grade 
                        classes in the form.</li><li>Each class name must be unique. Do not just type in “Grade 4 class” several times. We must be able to differentiate between classes, so please use different class names for each class with at least one 4th-grade student.</li></ul><p>
                        </p>
                        <div ID="popG4ClassName" class="popbox">
                            <b><i>Class Name:</i></b> (each class name must be unique) Record the class 
                            name that is typically used by your school to refer to the class. For example, 
                            it may be that your school uses the grade plus a letter for the class name (4a, 
                            4b, etc.), the grade plus a number (4.1, 4.2, etc.), the teacher name, the class 
                            period (Period 1, Period 2, etc.), the class location (Room 7, Room 8, etc.), or 
                            some other combination of these items. <b>It is important that unique class 
                            names are entered because these names will be used to indicate to the Test 
                            Administrator which classes will be tested.</b> <br /></div><div ID="popG4ClassGroup" class="popbox">
                            <b><i>Class Group or Track</i></b> (if applicable): If your school assigns 
                            students to specific classes based on their ability, please indicate the 
                            relevant level: Low ability, Average ability, or High ability. <br /></div><div ID="popG4NumStu" class="popbox">
                            <b><i>Number of 4<sup>th</sup>-grade students:</i></b> Enter 
                            the number of fourth-grade students in each class. In the case of multi-grade 
                            classes (e.g., students from more than one grade level in the same class), only 
                            the fourth-grade students should be counted as a class in the list. For example, 
                            if three Grade 3 students, five Grade 4 students, and ten Grade 5 students form 
                            a multi-grade class, then you should record five students for the number of 
                            students in this multi-grade class. <br /></div><div ID="popG4ClassExclusion" class="popbox">
                            <b><i>Class Exclusion Status</i></b> (if applicable; if at least one 4th-grade 
                            student in this class is able to to be assessed, do not use this field): As a 
                            rule, all classes are to be included. 
                            TIMSS will offer many accommodations that should allow most students to participate. Please contact <a href="mailto:TIMSS@westat.com">TIMSS@westat.com</a> if you have questions. <br /><br /><ol>
                                <li>Students with functional disabilities; i.e., students who have physical 
                                    disabilities in such a way that they cannot perform in the TIMSS testing 
                                    situation. Students with functional disabilities who are able to perform should 
                                    be accommodated in the test situation, within reason, rather than excluded.</li><li>Students with intellectual disabilities; i.e., students who are considered, in 
                                    the professional opinion of the school principal or by other qualified staff 
                                    members, to have severe intellectual disabilities or who have been tested as 
                                    such. This category includes students who are emotionally or mentally unable to 
                                    follow even the general instructions of the test. Students should not be 
                                    excluded solely because of poor academic performance or normal disciplinary 
                                    problems. It should be noted that students with dyslexia, or other such learning 
                                    disabilities, should be accommodated in the test situation, within reason, 
                                    rather than excluded.</li><li>Non-native language speakers; i.e., students who are unable to read or speak the 
                                    language(s) of the test and would be unable to overcome the language barrier in 
                                    the test situation. <br /><br />If all students in the excluded class do not belong to the same exclusion 
                                    category, please identify the category corresponding to the majority of 
                                    students.</li></ol>If all students in the excluded class do not belong to the same exclusion 
                            category, please identify the category corresponding to the majority of 
                            students. <br /></div><div ID="popG4NameofSci" class="popbox">
                            <b><i>Name of Teacher:</i></b> Name of Teacher <br /></div><div ID="popG4NameofMath" class="popbox">
                            <b><i>Name of Mathematics Teacher:</i></b> Name of Mathematics Teacher of class.<br /></div><div ID="popG4NameofScience" class="popbox">
                            <b><i>Name of Mathematics Teacher:</i></b> Name of Mathematics Teacher of class.<br /></div><div ID="popG4NameofMathEmail" class="popbox">
                            <b><i>Email address of Mathematics Teacher:</i></b> Email address of Mathematics Teacher of class.<br /></div><div ID="popG4NameofScienceEmail" class="popbox">
                            <b><i>Email address of Mathematics Teacher:</i></b> Email address of Mathematics Teacher of class.<br /></div><table border="1" cellpadding="4" cellspacing="0">
                            <tr ID="trG4Header" runat="server" 
                                style="background-color:#008BB0; color:#FFFFFF">
                                <td colspan="9">
                                    <b>&nbsp;Grade 4</b></td></tr><tr style="background-color:#008BB0; color:#FFFFFF">
                                <td align="center">
                                    &nbsp;</td><td align="center">
                                    <a class="popper" data-popbox="popG4ClassName" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<b><font color="white">&#10038;</font></b><font size="2"><b>Class Name<br />(each class name must be unique)</b></font> </td><td align="center">
                                    <a class="popper" data-popbox="popG4ClassGroup" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a></Font>&nbsp;<b><font color="white">&#10038;</font></b><font size="2"><b>Class Group</b></font> </td><td align="center">
                                    <a class="popper" data-popbox="popG4NumStu" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<b><font color="white">&#10038;</font></b><font size="2"><b>Number<br />of<br />Students</b> </font></td><td align="center">
                                    <a class="popper" data-popbox="popG4ClassExclusion" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<font size="2"><b>Class<br />Exclusion<br />Status (If 
                                    applicable)</b></font> </td><td align="center">
                                    <a class="popper" data-popbox="popG4NameofMath" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<b><font color="white">&#10038;</font></b><font size="2"><b>Name of Mathematics Teacher</b></font> </td><td align="center"> <a class="popper" data-popbox="popG4NameofMathEmail" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<b><font color="white">&#10038;</font></b><font size="2"><b>Email address of Mathematics Teacher</b></font></td><td align="center">
                                    <a class="popper" data-popbox="popG4NameofScience" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<font size="2"><b>Name of Science Teacher</b></font> </td><td align="center"> <a class="popper" data-popbox="popG4NameofScienceEmail" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a>&nbsp;<font size="2"><b>Email address of Science Teacher</b></font></td></tr><asp:Repeater ID="RepeaterClassList4ReadOnly" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            &nbsp;</td><td>
                                            <%#Container.DataItem("ClassName")%>
                                        </td>
                                        <td>
                                            <%#TimssBll.GetClassGroupText(Container.DataItem("ClassGroup"))%>
                                        </td>
                                        <td align="center">
                                            <%#Container.DataItem("NumberofStudents")%>
                                        </td>
                                        <td>
                                            <%#TimssBll.GetClassExclusionStatusText(Container.DataItem("ClassExclusionStatus"))%>
                                        </td>
                                        <td>
                                            <%#Container.DataItem("NameofMathematicsTeacherG4")%>
                                        </td>
                                         <td>
                                            <%#Container.DataItem("NameofMathematicsTeacherEmailG4")%>
                                        </td>
                                         <td>
                                            <%#Container.DataItem("NameofScienceTeacherG4")%>
                                        </td>
                                         <td>
                                            <%#Container.DataItem("NameofScienceTeacherEmailG4")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="RepeaterClassList4" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="ImageButtonDeleteClass" runat="server" 
                                                CommandArgument='<%#Eval("id") & "|" & Eval("ClassListingFormId")%>' 
                                                CommandName="deleteclass" ImageUrl="~/Common/images/tooltip/Critical.png" 
                                                OnClientClick="return confirm('Are you sure you want to delete this class?');" 
                                                ToolTip="Delete this class" Width="20px" />
                                            <asp:HiddenField ID="id" runat="server" Value='<%#Eval("id")%>' />
                                            <asp:HiddenField ID="ClassListingFormId" runat="server" 
                                                Value='<%#Eval("ClassListingFormId")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="db_ClassName" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("ClassName")%>'></asp:TextBox></td><td>
                                            <asp:DropDownList ID="db_ClassGroup" runat="server" 
                                                DataSource="<%#TimssBll.GetClassGroupNameValuePairList()%>" 
                                                DataTextField="Name" DataValueField="Value" onchange="Edited();ExclusionWarning(this);" 
                                                SelectedValue='<%#Container.DataItem("ClassGroup")%>'>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="db_NumberofStudents" runat="server" MaxLength="10" 
                                                onchange="Edited();" size="2" 
                                                Text='<%#Container.DataItem("NumberofStudents")%>'></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidatordb_NumberofStudents" 
                                                runat="server" ControlToValidate="db_NumberofStudents" Display="Dynamic" 
                                                EnableClientScript="true" ErrorMessage="Only Numbers" 
                                                ValidationExpression="^\d+"></asp:RegularExpressionValidator></td><td>
                                            <asp:DropDownList ID="db_ClassExclusionStatus" runat="server" 
                                                DataSource="<%#TimssBll.GetClassExclusionStatusNameValuePairList()%>" 
                                                DataTextField="Name" DataValueField="Value" onchange="Edited();" 
                                                SelectedValue='<%#Container.DataItem("ClassExclusionStatus")%>'>
                                            </asp:DropDownList>
                                         
                                        </td>
                                        <td>
                                            <asp:TextBox ID="db_NameOfMathematicsTeacherG4" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("NameOfMathematicsTeacherG4")%>'></asp:TextBox></td><td>
                                            <asp:TextBox ID="db_NameOfMathematicsTeacherEmailG4" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("NameOfMathematicsTeacherEmailG4")%>'></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValiddb_NameOfMathematicsTeacherEmailG4" runat="server" 
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                         ControlToValidate="db_NameOfMathematicsTeacherEmailG4" Display="Dynamic" 
                                                         ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator></td><td>
                                            <asp:TextBox ID="db_NameOfScienceTeacherG4" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("NameOfScienceTeacherG4")%>'></asp:TextBox></td><td>
                                            <asp:TextBox ID="db_NameOfScienceTeacherEmailG4" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("NameOfScienceTeacherEmailG4")%>'></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValiddb_NameOfScienceTeacherEmailG4" runat="server" 
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                         ControlToValidate="db_NameOfScienceTeacherEmailG4" Display="Dynamic" 
                                                         ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator></td></tr></ItemTemplate></asp:Repeater><tr ID="trGrade4Add" runat="server">
                                <td align="center">
                                    &nbsp;<asp:HiddenField ID="dbG4_id" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="dbG4_ClassName" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox></td><td>
                                    <asp:DropDownList ID="dbG4_ClassGroup" runat="server" DataTextField="Name" 
                                        DataValueField="Value" onchange="Edited();">
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="dbG4_NumberofStudents" runat="server" MaxLength="10" 
                                        onchange="Edited();" size="2"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidatordbG4_NumberofStudents" 
                                        runat="server" ControlToValidate="dbG4_NumberofStudents" Display="Dynamic" 
                                        EnableClientScript="true" ErrorMessage="Only Numbers" 
                                        ValidationExpression="^\d+"></asp:RegularExpressionValidator></td><td>
                                    <asp:DropDownList ID="dbG4_ClassExclusionStatus" runat="server" 
                                        DataTextField="Name" DataValueField="Value" onchange="Edited();ExclusionWarning(this);">
                                    </asp:DropDownList>
                                
                                </td>
                                <td>
                                    <asp:TextBox ID="dbG4_NameOfMathematicsTeacherG4" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox></td><td>
                                    <asp:TextBox ID="dbG4_NameOfMathematicsTeacherEmailG4" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValiddbG4_NameOfMathematicsTeacherEmailG4" runat="server" 
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                         ControlToValidate="dbG4_NameOfMathematicsTeacherEmailG4" Display="Dynamic" 
                                                         ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator></td><td>
                                    <asp:TextBox ID="dbG4_NameOfScienceTeacherG4" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox></td><td>
                                    <asp:TextBox ID="dbG4_NameOfScienceEmailG4" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValiddbG4_NameOfScienceTeacherEmailG4" runat="server" 
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                         ControlToValidate="dbG4_NameOfScienceEmailG4" Display="Dynamic" 
                                                         ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator></td></tr></table><br /><p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                    <p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p></p>
                </p>

    </asp:Panel>




    <asp:Panel ID="PanelGrade8" runat="server" Visible="false">
                <p>TIMSS needs a complete and current list of all of your school’s eighth-grade Mathematics classes in order to randomly select classes to be assessed. Typically, two mathematics classes are sampled in each school, and all students in the selected classes will be assessed.</p><p>Enter the data for each class in the form below. After you have entered information for a class, click on save, and another row will appear for you to enter information about another class. Continue until you have entered information for all classes that contain eighth-grade students.</p><p>
                    Include the following information for each class (<b><font color="red">&#10038;</font></b> indicates a required field): <table border="1" cellpadding="15" cellspacing="0">
                        <tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Mathematics Class Name (each class name must be unique)</td><td>Record the class name that is typically used by your school to refer to the class. For example, it may be that your school uses the grade plus a letter for the class name (8a, 8b, etc.), the grade plus a number (8.1, 8.2, etc.), the teacher name, the class period (Period 1, Period 2, etc.), the class location (Room 7, Room 8, etc.), or some other combination of these items. <b>It is important that unique class names are entered because these names will be used to indicate to the Test Administrator which classes will be tested.</b></td></tr><tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Class Group or Track (if applicable)</td><td>If your school assigns students to specific classes based on their ability, please indicate the relevant level: Low ability, Average ability, or High ability.</td></tr><tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Number of 8<sup>th</sup>-grade students</td><td>Enter the number of eighth-grade students in each class. In the case of <i>multi-grade classes</i> (e.g., students from more than one grade level in the same class), only the eighth-grade students should be counted as a class in the list. For example, if three Grade 7 students, five Grade 8 students, and ten Grade 9 students form a multi-grade class, then you should record five students for the number of students in this multi-grade class.</td></tr><tr valign="top">
                            <td>Class Exclusion Status (if applicable; if at least one 8th-grade student in this class is able to be assessed, do not use this field)</td><td>
                           
                            <b>1</b> = Students with functional disabilities; i.e., students who have physical disabilities in such a way that they cannot perform in the TIMSS testing situation. Students with functional disabilities who are able to perform should be accommodated in the test situation, within reason, rather than excluded. <br/><br/><b>2</b> = Students with intellectual disabilities; i.e., students who are considered, in the professional opinion of the school principal or by other qualified staff members, to have severe intellectual disabilities or who have been tested as such. This category includes students who are emotionally or mentally unable to follow even the general instructions of the test. Students should not be excluded solely because of poor academic performance or normal disciplinary problems. It should be noted that students with dyslexia, or other such learning disabilities, should be accommodated in the test situation, within reason, rather than excluded. <br/><br/><b>3</b> = Non-native language speakers; i.e., students who are unable to read or speak the language(s) of the test and would be unable to overcome the language barrier in the test situation. <br/><br/>If all students in the excluded class do not belong to the same exclusion category, please identify the category corresponding to the majority of students. </td></tr><tr valign="top">
                            <td><b><font color="red">&#10038;</font></b>Name of Mathematics Teacher</td><td>Name of the Mathematics Teacher of class.</td></tr><tr><td><b><font color="red">&#10038;</font></b>Email address of Mathematics Teacher</td><td>Email address of Mathematics Teacher of class.</td></tr></table><p>
                    </p>
                    Tips: <p>
                        <ul>
                            <li>Use the <i>Save</i> button to save information you have entered for each class.</li><li>If you need to delete a class, click on the <img border="0" 
                            src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/Critical.png' 
                            width="15"></img> at the beginning of a saved class row.</li><li>Use the <i>Print</i> button to print a list of the classes you included for 
                        reference.</li><li>Click on the <i>Finished</i> button when you have entered all fourth-grade 
                        classes in the form.</li><li>Each class name should be unique. Do not just type in “Grade 8 class” several times. We must be able to differentiate between classes, so please use different class names for each class with at least one 8th-grade student.</li></ul><p>
                        </p>
                        <div ID="popG8ClassName" class="popbox">
                            <b><i>Mathematics Class Name:</i></b> (each class name must be unique)
                            Record the class name that is typically used by your school to refer to the class. For example, it may be that your school uses the grade plus a letter for the class name (8a, 8b, etc.), the grade plus a number (8.1, 8.2, etc.), the teacher name, the class period (Period 1, Period 2, etc.), the class location (Room 7, Room 8, etc.), or some other combination of these items. <b>It is important that unique class names are entered because these names will be used to indicate to the Test Administrator which classes will be tested.</b> <br /></div><div ID="popG8ClassGroup" class="popbox">
                            <b><i>Class Group or Track</i></b> (if applicable): 
                            If your school assigns students to specific classes based on their ability, please indicate the relevant level: Low ability, Average ability, or High ability. <br /></div><div ID="popG8NumStu" class="popbox">
                            <b><i>Number of 8<sup>th</sup>-grade students:</i></b> Enter the number of eighth-grade students in each class. In the case of <i>multi-grade classes</i> (e.g., students from more than one grade level in the same class), only the eighth-grade students should be counted as a class in the list. For example, if three Grade 7 students, five Grade 8 students, and ten Grade 9 students form a multi-grade class, then you should record five students for the number of students in this multi-grade class. <br /></div><div ID="popG8ClassExclusion" class="popbox">
                            <b><i>Class Exclusion Status</i></b> (if applicable; if at least one 8th-grade student in this class is able to be assessed, do not use this field):
                             As a rule, all classes are to be included. TIMSS will offer many accommodations that should allow most students to participate. Click on the Documents link on the left panel to see what accommodations are provided or allowed. All class-level exclusions must be approved. If you indicate a class-level exclusion, a TIMSS representative will contact you to discuss. <br /><br /><ol>
                                <li>Students with functional disabilities; i.e., students who have physical disabilities in such a way that they cannot perform in the TIMSS testing situation. Students with functional disabilities who are able to perform should be accommodated in the test situation, within reason, rather than excluded.</li><li>Students with intellectual disabilities; i.e., students who are considered, in the professional opinion of the school principal or by other qualified staff members, to have severe intellectual disabilities or who have been tested as such. This category includes students who are emotionally or mentally unable to follow even the general instructions of the test. Students should not be excluded solely because of poor academic performance or normal disciplinary problems. It should be noted that students with dyslexia, or other such learning disabilities, should be accommodated in the test situation, within reason, rather than excluded. </li><li>Non-native language speakers; i.e., students who are unable to read or speak the language(s) of the test and would be unable to overcome the language barrier in the test situation.</li></ol>If all students in the excluded class do not belong to the same exclusion category, please identify the category corresponding to the majority of students. <br /></div><div ID="popG8NameofSci" class="popbox">
                            <b><i>Name of Mathematics Teacher:</i></b> Name of the Mathematics Teacher of class.<br /></div><div ID="popG8NameofMathEmail" class="popbox">
                            <b><i>Email address of Mathematics Teacher:</i></b> Email address of the Mathematics Teacher <br /></div><table border="1" cellpadding="4" cellspacing="0">
                            <tr ID="trG8Header" runat="server" 
                                style="background-color:#008BB0; color:#FFFFFF">
                                <td colspan="7">
                                    <b>&nbsp;Grade 8</b></td></tr><tr style="background-color:#008BB0; color:#FFFFFF">
                                <td align="center">
                                    &nbsp;</td><td align="center">
                                    <a class="popper" data-popbox="popG8ClassName" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a> <b><font color="white">&#10038;</font></b><font size="2"><b>Mathematics Class Name</b> (each class name must be unique)</font> </td><td align="center">
                                    <a class="popper" data-popbox="popG8ClassGroup" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a></Font> <b><font color="white">&#10038;</font></b><font size="2"><b>Class Group or Track</b></font> </td><td align="center">
                                    <a class="popper" data-popbox="popG8NumStu" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a> <b><font color="white">&#10038;</font></b><font size="2"><b>Number of 8<sup>th</sup>-grade students<br />in entire school</b> </font></td><td align="center">
                                    <a class="popper" data-popbox="popG8ClassExclusion" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a> <font size="2"><b>Class<br />Exclusion<br />Status (If 
                                    applicable)</b></font> </td><td align="center">
                                    <a class="popper" data-popbox="popG8NameofSci" href="#">
                                    <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' 
                                        width="15"></img></a> <b><font color="white">&#10038;</font></b><font size="2"><b>Name of Mathematics Teacher</b></font> </td><td align ="center">
                                            <a class="popper" data-popbox ="popG8NameofSciEmail" href="#">
                                                 <img border="0" 
                                        src='<%= Page.ResolveClientUrl("~/")%>common/images/tooltip/info.png' width="15"></img></a><b><font color="white">&#10038;</font></b><font size="2"><b>Email address of Mathematics Teacher</b></font></td></tr><asp:Repeater ID="RepeaterClassList8ReadOnly" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            &nbsp;</td><td>
                                            <%#Container.DataItem("ClassName")%>
                                        </td>
                                        <td>
                                            <%#TimssBll.GetClassGroupText(Container.DataItem("ClassGroup"))%>
                                        </td>
                                        <td align="center">
                                            <%#Container.DataItem("NumberofStudents")%>
                                        </td>
                                        <td>
                                            <%#TimssBll.GetClassExclusionStatusText(Container.DataItem("ClassExclusionStatus"))%>
                                        </td>
                                        <td>
                                            <%#Container.DataItem("NameOfMathematicsTeacherG8")%>
                                        </td>
                                        <td>
                                            <%#Container.DataItem("NameOfMathematicsTeacherEmailG8")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="RepeaterClassList8" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="ImageButtonDeleteClass" runat="server" 
                                                CommandArgument='<%#Eval("id") & "|" & Eval("ClassListingFormId")%>' 
                                                CommandName="deleteclass" ImageUrl="~/Common/images/tooltip/Critical.png" 
                                                OnClientClick="return confirm('Are you sure you want to delete this class?');" 
                                                ToolTip="Delete this class" Width="20px" />
                                            <asp:HiddenField ID="id" runat="server" Value='<%#Eval("id")%>' />
                                            <asp:HiddenField ID="ClassListingFormId" runat="server" 
                                                Value='<%#Eval("ClassListingFormId")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="db_ClassName" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("ClassName")%>'></asp:TextBox></td><td>
                                            <asp:DropDownList ID="db_ClassGroup" runat="server" 
                                                DataSource="<%#TimssBll.GetClassGroupNameValuePairList()%>" 
                                                DataTextField="Name" DataValueField="Value" onchange="Edited();" 
                                                SelectedValue='<%#Container.DataItem("ClassGroup")%>'>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="db_NumberofStudents" runat="server" MaxLength="10" 
                                                onchange="Edited();" size="2" 
                                                Text='<%#Container.DataItem("NumberofStudents")%>'></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidatordb_NumberofStudents" 
                                                runat="server" ControlToValidate="db_NumberofStudents" Display="Dynamic" 
                                                EnableClientScript="true" ErrorMessage="Only Numbers" 
                                                ValidationExpression="^\d+"></asp:RegularExpressionValidator></td><td>
                                            <asp:DropDownList ID="db_ClassExclusionStatus" runat="server" 
                                                DataSource="<%#TimssBll.GetClassExclusionStatusNameValuePairList()%>" 
                                                DataTextField="Name" DataValueField="Value" onchange="Edited();ExclusionWarning(this);" 
                                                SelectedValue='<%#Container.DataItem("ClassExclusionStatus")%>'>
                                            </asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="db_NameofMathematicsTeacherG8" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("NameofMathematicsTeacherG8")%>'></asp:TextBox></td><td>
                                            <asp:TextBox ID="db_NameofMathematicsTeacherEmailG8" runat="server" MaxLength="60" 
                                                onchange="Edited();" size="35" Text='<%#Container.DataItem("NameofMathematicsTeacherEmailG8")%>'></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValiddb_NameofMathematicsTeacherEmailG8" runat="server" 
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                         ControlToValidate="db_NameofMathematicsTeacherEmailG8" Display="Dynamic" 
                                                         ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator></td></tr></ItemTemplate></asp:Repeater><tr ID="trGrade8Add" runat="server">
                                <td align="center">
                                    &nbsp;<asp:HiddenField ID="dbG8_id" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="dbG8_ClassName" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox></td><td>
                                    <asp:DropDownList ID="dbG8_ClassGroup" runat="server" DataTextField="Name" 
                                        DataValueField="Value" onchange="Edited();">
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="dbG8_NumberofStudents" runat="server" MaxLength="10" 
                                        onchange="Edited();" size="2"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidatordbG8_NumberofStudents" 
                                        runat="server" ControlToValidate="dbG8_NumberofStudents" Display="Dynamic" 
                                        EnableClientScript="true" ErrorMessage="Only Numbers" 
                                        ValidationExpression="^\d+"></asp:RegularExpressionValidator></td><td>
                                    <asp:DropDownList ID="dbG8_ClassExclusionStatus" runat="server" 
                                        DataTextField="Name" DataValueField="Value" onchange="Edited();ExclusionWarning(this);">
                                    </asp:DropDownList>
                                  
                                </td>
                                <td>
                                    <asp:TextBox ID="dbG8_NameofMathematicsTeacherG8" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox></td><td>
                                    <asp:TextBox ID="dbG8_NameofMathematicsTeacherEmailG8" runat="server" MaxLength="60" 
                                        onchange="Edited();" size="35"></asp:TextBox><asp:RegularExpressionValidator ID="regexEmailValidNameofMathematicsTeacherEmailG8" runat="server" 
                                                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                         ControlToValidate="dbG8_NameofMathematicsTeacherEmailG8" Display="Dynamic" 
                                                         ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator></td></tr></table><br /><p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                        <p>
                        </p>
                        <p>
                        </p>
                        
                    <p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p></p>
                </p>
    </asp:Panel>
    
    <asp:Panel ID="PanelGrade12" runat="server" Visible="false">
        		        <table border="1">
    <tr style="background-color:#008BB0; color:#FFFFFF" ID="tr1" runat="server">
        <td><b>&nbsp;Grade 12</b></td></tr><tr>
        <td>
        <br />
        <a href="SubmitStudentList.aspx">Class list is not required for Grade 12, click here to submit your student list.</a> </td></tr></table></asp:Panel><asp:Panel ID="PanelSave" runat="server">
    
<br />
            <asp:Button ID="ButtonSave" runat="server" Text="Save" onclientclick="ClearEdited();" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type="button"  value="Print" name="Print" onclick="window.print();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="ButtonFinished" runat="server" Text="Finished" onclientclick="if (confirm('Are you sure?')) {ClearEdited();} else {return false;}" />
            
            <asp:Label ID="LabelClassListSubmittedBy" runat="server" />

</asp:Panel>
            
</asp:Content>

