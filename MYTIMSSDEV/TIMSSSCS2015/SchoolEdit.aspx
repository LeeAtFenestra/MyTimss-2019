<%@ Page Title="School Information Screen" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="SchoolEdit.aspx.vb" Inherits="SchoolEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript">
    //<-- hide this script from non-javascript-enabled browsers
    var imgEdit = new hoverbutton('edit', '<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', '<%= Page.ResolveClientUrl("~/")%>common/images/edited.gif', true);
    //-->		

    function initial_ChgSCHEDATE(hide_show) {
        var e = document.getElementById("db.ReasonChgSCHEDATE.num");
        e.style.display = hide_show;
    }

    function initial_ChgTIMSSDISPComments(hide_show) {
        var e = document.getElementById("<%=divChgTIMSSDISPComments.ClientID%>");
        e.style.display = hide_show;
    }

    //Hide or Show Reason for Reason for changing assessment date
    function Reset_ReasonChgSCHEDATE() {
        var e = document.getElementById("db.ReasonChgSCHEDATE.num");

        if (e.style.display == 'block') {
            getFrm().elements["db.ReasonChgSCHEDATE.num"].value = '0';
        }
    }

    //Hide or Show Reason for Reason for changing assessment date
    function Reset_ReasonChgSCHEDATE2() {
        var e = document.getElementById("db.ReasonChgSCHEDATE2.num");

        if (e.style.display == 'block') {
            getFrm().elements["db.ReasonChgSCHEDATE2.num"].value = '0';
        }
    }
    //Hide or Schow Other-specify
    function toggle_ChgTIMSSDISPComments(obj) {
        var e = document.getElementById("<%=divChgTIMSSDISPComments.ClientID%>");

        var rIndex;
        //alert(obj.name);
        rIndex = getFrm().elements[obj.name].options.selectedIndex;
        switch (getFrm().elements[obj.name].options[rIndex].value) {
            case '03':
            case '05':
            case '30':
            case '32':
            case '33':
            case '34':
            case '40':
            case '41':
                e.style.display = 'block';
                ValidatorEnable(document.getElementById("<%=RequiredFieldValidatordbgrade_ChgTIMSSDISPComments.ClientID%>"), true);
                break;
            default:
                e.style.display = 'none';
                ValidatorEnable(document.getElementById("<%=RequiredFieldValidatordbgrade_ChgTIMSSDISPComments.ClientID%>"), false);
                break;
        }

        Edited();
    }

    function MakeUpCHK() {
      
<%--        var frm = getFrm();
        var Assessment = getFrm().elements["<%=dbgrade_AssessmentCompleted.ClientID%>"];
        var MakeUpDT = getFrm().elements["<%=dbgrade_DateOfMakeUp.ClientID%>"];

        if (Assessment.options[Assessment.options.selectedIndex].value == "MAKE-UP REQUIRED") {
            MakeUpDT.disabled = false;
            MakeUpDT.focus();
        }
        else {
            if (MakeUpDT.value == '') {
                MakeUpDT.disabled = true;
            }
        }--%>
       
    }

    function weekofday(obj) {
        var myindex = obj.selectedIndex;
        var SelValue = obj.options[myindex].value;
        var SelDate = new Date(SelValue);
        var weekDayName = getDayName(SelDate.getDay());

        document.getElementById("schedateOfWeek").innerText = weekDayName
    }

    function weekofdayonload(j) {
        var SelDate = new Date(j);
        var weekDayName = getDayName(SelDate.getDay());

        document.getElementById("schedateOfWeek").innerText = weekDayName
    }

    function weekofdayonload2(j) {
        var SelDate = new Date(j);
        var weekDayName = getDayName(SelDate.getDay());

        document.getElementById("schedateOfWeek2").innerText = weekDayName
    }

    function weekofday2(obj) {
        var myindex = obj.selectedIndex;
        var SelValue = obj.options[myindex].value;
        var SelDate = new Date(SelValue);
        var weekDayName = getDayName(SelDate.getDay());

        document.getElementById("schedateOfWeek2").innerText = weekDayName
    }
    function getDayName(dayIndex) {
        //alert(isNaN(dayIndex));
        if (isNaN(dayIndex) == false) {
            var weekday = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
            //alert(dayIndex + ', ' + weekday[dayIndex]);
            return weekday[dayIndex];
        }
        else {
            return "";
        }
    }

    function showSection(gen, sch, pre, smt, pst) {
        if (gen) {
            showObj('tblGeneral');
            getFrm().tblGeneralState.value = "block";
            setCSSClassName('tblGeneralLink', 'menuOn');
        }
        else {
            hideObj('tblGeneral');
            getFrm().tblGeneralState.value = "none";
            setCSSClassName('tblGeneralLink', 'menu');
        }
        /*
        if (sch) {
            showObj('tblContact');
            getFrm().tblContactState.value = "block";
            setCSSClassName('tblContactLink', 'menuOn');
        }
        else {
            hideObj('tblContact');
            getFrm().tblContactState.value = "none";
            setCSSClassName('tblContactLink', 'menu');
        }
        */
        /*
        if (pre) {
            showObj('tblPreAssessment');
            getFrm().tblPreAssessmentState.value = "block";
            setCSSClassName('tblPreAssessmentLink', 'menuOn');
        }
        else {
            hideObj('tblPreAssessment');
            getFrm().tblPreAssessmentState.value = "none";
            setCSSClassName('tblPreAssessmentLink', 'menu');
        }
        */
        if (smt) {
            showObj('tblAssessment');
            getFrm().tblAssessmentState.value = "block";
            setCSSClassName('tblAssessmentLink', 'menuOn');
        }
        else {
            hideObj('tblAssessment');
            getFrm().tblAssessmentState.value = "none";
            setCSSClassName('tblAssessmentLink', 'menu');
        }

        if (pst) {
            showObj('tblPostAssessment');
            getFrm().tblPostAssessmentState.value = "block";
            setCSSClassName('tblPostAssessmentLink', 'menuOn');
        }
        else {
            hideObj('tblPostAssessment');
            getFrm().tblPostAssessmentState.value = "none";
            setCSSClassName('tblPostAssessmentLink', 'menu');
        }
    }
    function launchFedExTrackin(tb) {
        var num = tb.value;
        if (num == "") {
            alert('Please enter a FedEx tracking number');
            tb.focus();
        }
        else {
            var url = 'http://www.fedex.com/Tracking?action=track&tracknumbers=' + num;
            var win = window.open(url, '_blank');
            win.focus();
        }

    }
</script>
    <style type="text/css">
        .style2
        {
            height: 25px;
        }
        .auto-style1 {
            height: 22px;
        }
    </style>

  <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-ui.css" />

  <script src="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-1.10.2.js"></script>
  <script src="<%= Page.ResolveClientUrl("~/")%>common/jquerycalendar/jquery-ui.js"></script>
  <script>
      $(function () {

          //$("#<%=dbgrade_DateOfMakeUp.ClientID%>").datepicker({ minDate: new Date(2014, 7 - 1, 4), maxDate: new Date(2014, 7 - 1, 25) });
          $("#<%=dbgrade_DateOfMakeUp.ClientID%>").datepicker();
          

      });
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopNavigationLevel2" Runat="Server">

    <tr>
    <td height="29" width="4"></td>
    <td class="hotcell">
              <table border="0" width="100%" cellspacing="0" cellpadding="0" height="24">
                <tr>
                  <td width="220"><b><font size="2">&nbsp;School Edit&nbsp;&nbsp;&nbsp;&nbsp;</font></b>
                     <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                     <!-- 'Display Save and Reset buttons for all staff except ACs -->
                     <asp:Button ID="ButtonSave" runat="server" Text="Save" class="hotcell2" onclientclick="ClearEdited();" />
                     <!--<input type="submit" value="Save" name="button" onclick='return ValidateSCS();' class="hotcell2">-->
                     <input type="reset"  value="Reset" name="B1" onclick="ClearEdited();" class="hotcell2" />
                  </td>

                  <td width="475" align="center">
                    <!-- 'Display District Link for current School -->
                    <b><font size="2">District: <asp:HyperLink ID="DistrictHyperLink" runat="server">DistrictHyperLink</asp:HyperLink>&nbsp;
                    <!-- 'Display the new Region and Area information on for Field Managers, Supervisors and ACs -->
                    <br />               Territory: <asp:Label ID="Territory" runat="server"></asp:Label>&nbsp;&nbsp;
                                       Region: <asp:Label ID="Region" runat="server"></asp:Label>&nbsp;&nbsp;
                                       Area:&nbsp;<asp:Label ID="Area" runat="server"></asp:Label></font></b>
                  </td>

                    <!-- 'SET UP DISPLAY OF SEARCH FOR SCHOOL TEXTBOX AND BUTTON. -->
	                <td>
	       
	                    <!-- <font size="2"><b>Search for TIMSS ID</b></font>
			            <input value="" type="text" name="srchgid" size="7" maxlength="7"   onkeypress="enterevent(event);" >   
                        <input type="submit" value="Find" name="button" onclick="Schoolautosave();">-->
            
                        <input type="button"  value="EROC" class="hotcell2" runat="server" id="ButtonErroc" />
                        <input type="button"  value="Print Page" onclick="window.print();" class="hotcell2" />

                        <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&print=1&gen='  +<%=Server.HtmlEncode("getFrm().tblGeneralState.value") %>+ '&pre=' + getFrm().tblPreAssessmentState.value + '&smt=' + getFrm().tblAssessmentState.value + '&pst=' + getFrm().tblPostAssessmentState.value));"><img src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        
              <%-- <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&print=1&gen=' +<%=Server.HtmlEncode("getFrm().tblGeneralState.value") %>+ '&pre=' + getFrm().tblPreAssessmentState.value + '&smt=' + getFrm().tblAssessmentState.value + '&pst=' + getFrm().tblPostAssessmentState.value));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>    
        --%>

                       
                    </td>

                  <td align="right"><img name="edit" border="0" src="<%= Page.ResolveClientUrl("~/")%>common/images/shim.gif" alt="Record Edited" width="95" height="15" /><b><font size="2">&nbsp;&nbsp;</font></b></td>

                </tr>
              </table>       
      </td>
    <td height="29" width="4"></td>
  </tr>
  <tr>
    <td height="29" width="4"></td>
    <td>
      <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#2a3985" border="0"><tbody>
        <tr>
          <td colspan="2">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
              <tbody>
              <tr>
                <td height="28" nowrap="nowrap"><font size="2">&nbsp;<font color="#ffffff">
                 
                  <a title="General" id="tblGeneralLink" class='<%=Server.HtmlEncode(Me.tblGeneralLinkCSS()) %>' href="#" onclick="showSection(true, false, false, false, false);">General</a> 
                  <!--| 
                  <A title="Pre-Assessment" id="tblPreAssessmentLink" class='<%= Server.HtmlEncode(Me.tblPreAssessmentLinkCSS()) %>' href="#" onclick="showSection(false, false, true, false, false);">Pre-Assessment</A> -->
                  | 
                  <a title="Assessment" id="tblAssessmentLink" class='<%= Server.HtmlEncode(Me.tblAssessmentLinkCSS()) %>' href="#" onclick="showSection(false, false, false, true, false);">Assessment</a> 
                  | 
                  <a title="Post-Assessment" id="tblPostAssessmentLink" class='<%= Server.HtmlEncode(Me.tblPostAssessmentLinkCSS()) %>' href="#" onclick="showSection(false, false, false, false, true);">Post-Assessment</a> 
                  |
                  <a title="Show All Sections" class='menu' href="#" onclick="showSection(true, true, true, true, true);">Show All Sections</a>
                  |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:HyperLink ForeColor="White" ID="HyperLinkMyTIMSS" runat="server">MyTIMSS</asp:HyperLink>   
                
                </font></font>
                  </td>
                  <%--<td align="center" style="width:50%"></td>--%>
              </tr></tbody></table>
                  </td></tr></tbody></table>
      </td>
    <td height="29" width="4"></td></tr>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">

    <input type="hidden"  name="edited" value = "0" size="5"/>
    <input type="hidden"  name="reader" value = 'False' size="8"/>
	<input type="hidden"  name="tblGeneralState" value="<%=Server.HtmlEncode(Me.tblGeneralCSSDisplay()) %>"/>
	<input type="hidden"  name="tblPreAssessmentState" value="<%=Server.HtmlEncode(Me.tblPreAssessmentCSSDisplay()) %>"/>
	<input type="hidden"  name="tblAssessmentState" value="<%= Server.HtmlEncode(Me.tblAssessmentCSSDisplay()) %>"/>
	<input type="hidden"  name="tblPostAssessmentState" value="<%= Server.HtmlEncode(Me.tblPostAssessmentCSSDisplay()) %>"/>

    <table width="100%">
        <tr>
            <td bgcolor="#FFFFFF">      
                <!-- SET UP OF NEW SDEP "TABS" FOR DISPLAYING AND ACCESS TO DIFFERENT DATA ON THIS PAGE. -->
	<table border="0">
    <!--
	<tr>
           	<td align=center class="TabLavendarOn" width="152"><b><a  HREF="#">General</a></b></td>	
            <td align=center class="TabOrange" width="152"><b><a  HREF="#">Contact</a></b></td>
            <td align=center class="TabPopsicleBlue" width="160"><b><a  HREF="#">Pre-Assessment</a></b></td>
            <td align=center nowrap class="TabDarkYellow" width="162"><b><a  HREF="#">Assessment</a></b></td>
            <td align=center class="TabGreen" width="152"><b><a  HREF="#">Post-Assessment</a></b></td>
	</tr>
	-->
	<tr>
	<td nowrap align="center"><font size="2">School Name: <b><asp:Label ID="SchoolName" runat="server"></asp:Label></b></font></td>
     <td width="25">&nbsp;</td>
	<td align="center" nowrap="nowrap"><font size="2">Sampled Grade: <b><asp:Label ID="SampledGrade" runat="server"></asp:Label></b></font></td> 
     <td width="25">&nbsp;</td>
	<td align="center" nowrap="nowrap"><font size="2"><asp:Label ID="ProjectName2" runat="server"></asp:Label>&nbsp;ID:&nbsp;<b><asp:Label ID="TIMSSID" runat="server"></asp:Label></b> </font></td> 
     <td width="25">&nbsp;</td>
	<td align="center" nowrap="nowrap"><font size="2">Project Name: <b><asp:Label ID="ProjectName" runat="server"></asp:Label></b> </font></td>  
	 <td width="25">&nbsp;</td>
    <td nowrap align="center"><font size="2">Orig or Sub: <b><asp:Label ID="lblOrigOrSub" runat="server"></asp:Label></b></font></td>
	</tr>
</table>
<!-- END OF "TABS" SETUP -->


<!-- INCLUDE FILE THAT SETS UP NEW SGEP "TABS" FOR DISPLAYING AND ACCESS TO DIFFERENT DATA. -->

<!-- GENERAL "TAB" - The following displays the content of the "General" tab. -->
	
    <table border="1" width= "100%" id="tblGeneral" style="display:<%= me.tblGeneralCSSDisplay() %>">
		<tr>
			<td align="Top" valign="top" width="50%">
				<table border="0" cellspacing="0" cellpadding="0">
					<tr>
					    <td width="174"><font size="2">Name:<br></font></td>
						<td><asp:TextBox ID="db_s_name" runat="server" size="38" maxlength="60" onchange="Edited();"></asp:TextBox></td>
						<td>&nbsp;<a href="#" runat="server" id="WeatherHref" >Weather</a>&nbsp;</td>
						<td>&nbsp;<a href="#" runat="server" id="TimeHref" >Time</a>&nbsp;</td>
					</tr>
                    <tr>
						<td width="174"><font size="2">Address:</font></td>
						<td><asp:TextBox ID="db_s_addr1" runat="server" size="38" maxlength="50" onchange="Edited();"></asp:TextBox></td>
                        <td>&nbsp;<a href="#" runat="server" id="DirectionsHref" >Directions</a>&nbsp;</td>
					</tr>
					<tr>
						<td width="174"><font size="2">Address (cont'd):</font></td>
						<td><asp:TextBox ID="db_s_addr2" runat="server" size="38" maxlength="50" onchange="Edited();"></asp:TextBox></td>
					</tr>
					<tr>
						<td valign="top" width="174"><font size="2">City:, State: Zip:</font></td>
						<td valign="top">
                        <asp:TextBox ID="db_s_city" runat="server" size="25" maxlength="30" onchange="Edited();"></asp:TextBox>,
                        <asp:TextBox ID="db_s_state" runat="server" size="2" maxlength="2" onchange="Edited();"></asp:TextBox>
                        <asp:TextBox ID="db_s_zip" runat="server" size="10" maxlength="10" onchange="Edited();"></asp:TextBox></td>
					</tr>
					<tr>
						<td width="174"><font size="2">County:</font></td>
						<td><asp:TextBox ID="db_s_county" runat="server" size="25" maxlength="45" onchange="Edited();"></asp:TextBox></td>
					</tr>		
					<tr>
						<td width="174"><font size="2">Mailing Address:</font></td>
						<td><asp:TextBox ID="db_MailAddr1" runat="server" size="38" maxlength="30" onchange="Edited();"></asp:TextBox></td>
					</tr>
					<tr>
						<td width="174"><font size="2">Mailing Address (cont'd):</font></td>
						<td><asp:TextBox ID="db_MailAddr2" runat="server" size="38" maxlength="30" onchange="Edited();"></asp:TextBox></td>
					</tr>
					<tr>
						<td valign="top" width="174"><font size="2">Mailing City:, State: Zip:</font></td>
						<td valign="top"><asp:TextBox ID="db_MailCity" runat="server" size="25" maxlength="30" onchange="Edited();"></asp:TextBox>,
						<asp:TextBox ID="db_MailState" runat="server" size="2" maxlength="2" onchange="Edited();"></asp:TextBox>
						<asp:TextBox ID="db_MailZip" runat="server" size="10" maxlength="10" onchange="Edited();"></asp:TextBox></td>
					</tr>
					<tr>
						<td width="174"><font size="2">Phone:</font></td>
						<td><asp:TextBox ID="db_s_phone" runat="server" size="16" maxlength="27" onchange="Edited();"></asp:TextBox>
							<font size="2">(XXX) XXX-XXXX</font>
                            <asp:RegularExpressionValidator ID="RegularExpressionPhone" 
                                                  runat="server" 
                                                  ControlToValidate="db_s_phone"
                                                  Display="None" 
                                                  ErrorMessage="Invalid phone number" 
                                                  SetFocusOnError="True" 
                                                  ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>   
                         </td>
					</tr>			
					<tr>
						<td width="174"><font size="2">Principal:</font></td>
						<td>
                            <asp:DropDownList ID="db_principalID" runat="server" onchange = "Edited();" DataTextField="fullname" DataValueField="pid"></asp:DropDownList>
							<a href="#" runat="server" id="HrefPrincipal">Add</a>&nbsp;
						</td>
					</tr>
					<tr>
						<td width="174" class="style2"><font size="2">Coordinator:</font></td>
						<td class="style2">
                           <asp:DropDownList ID="db_coordinatorid" runat="server" onchange = "Edited();" DataTextField="fullname" DataValueField="pid"></asp:DropDownList>
						   <a href="#" runat="server" id="HrefCoordinator">Add</a>
						</td>
					</tr>
                  <tr>
				    <td width="35%"><font size="2">School Type: </font></td>
					<td><font size="2"><asp:Label ID="SchoolType" runat="server"></asp:Label></font>
					    <div id="nonpublic"></div></td>
				 </tr>

                   

                    <tr>
                        <td><font size="2">School State ID: </font></td>
                        <td><font size="2"><asp:TextBox ID="db_SEASCH" runat="server" size="20" maxlength="20" onchange="Edited();"></asp:TextBox></font></td>
                    </tr>

                     <tr id="rowSpecialCase" runat="server">
                        <td ><font size="2">Special Case? </font></td>
                        <td >
                            <asp:DropDownList ID="dbgrade_SPECIAL_CASE" runat="server" onchange="Edited();">
                                 <asp:ListItem Text="No" Value="N" />
                                 <asp:ListItem Text="Yes" Value="Y" />
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr id="rowIEAID" runat="server"> 
                        <td>
                            <font size="2">IEA ID: </font>
                        </td>
                        <td>
                            <font size="2"><asp:Label ID="LabelIEAID" runat="server"></asp:Label></font>
                        </td>
                    </tr>

                    <tr id="rowRegID" runat="server">
                    <td>
                    <font size="2">Registration ID: </font>
                    </td>
                    <td>
                    <font size="2"><asp:Label ID="LabelRegistrationID" runat="server"></asp:Label></font>
                    </td>
                    </tr>

	    </table>
  </td>
		
   <td align="Top" valign="top" width="50%">
				
			
			<table border="1" cellspacing="1" cellpadding="1" width="100%">
			<%--	<tr>
								<td colspan="2"><font size="2"><B>Original School (No Substitute Available)</b></font></td>
                </tr>--%>
							
				<tr>
					<td height="25" width="195"><font size="2">Date Initial School Selection Letter Sent:</font></td>
					<td height="25" width="355" valign="bottom">                                     
                        <asp:DropDownList ID="dbgrade_sch_partltrsentdt" runat="server" onchange = "Edited();Reset_ReasonChgSCHEDATE();" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
					</td>
				</tr>
				
				<tr>
					<td height="25" width="195"><font size="2">Date School Assessment Date Letter Sent:</font></td>
					<td height="25" width="355" valign="bottom">
                        <asp:DropDownList ID="dbgrade_SchAsmtLtrSentDT" runat="server" onchange = "Edited();Reset_ReasonChgSCHEDATE();" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
					</td>
				</tr>
                
                <tr>
					<td height="25" width="195"><font size="2">Date Sch Coord Resp Mailing Sent:</font></td>
					<td height="25" width="355" valign="bottom">
                        <asp:DropDownList ID="dbgrade_AugSchLtrSentDT" runat="server" onchange = "Edited();Reset_ReasonChgSCHEDATE();" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
					</td>
				</tr>

			<!--	07/23/05  Marta added back Charter School flag pulldown list per new spec update from Kathy
				08/25/05 was taken off again through email threads
				03/27/06 aded back the charter school flag picklist per issueview # 139
			    01/27/09 Christian Kapombe: Charter School Flag Removed
			    04/15/09 Christian Kapombe: Charter School Flag Restored 
                04/30/14 Christian Kapombe: Charter School Flag Restored -->

				<tr>
					<td><font size="2"><b>Enrollment</b><br />
                        Estimated: <asp:Label ID="Enrollment" runat="server"></asp:Label></font></td>
					<td><font size="2"<b>&nbsp;</b><br />
                        Actual: <asp:TextBox ID="dbG4_EnrollmentAtGrade" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox>
                        <asp:TextBox ID="dbG8_EnrollmentAtGrade" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox>
                        <asp:TextBox ID="dbG12_EnrollmentAtGrade" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></font></td>
				</tr>
               <tr>
					<td colspan="2"><font size="2">Status:</font>
                        <asp:DropDownList ID="dbgrade_DISP" runat="server" onchange="toggle_ChgTIMSSDISPComments(this);" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
					    <asp:TextBox ID="hiddenOriginalDISPcode" runat="server" Visible="false" />
                        <asp:TextBox ID="hiddenOriginalDISPtext" runat="server" Visible="false" />

                    </td>
				</tr>            
				<tr>
					<td colspan="2">
					    <div id="divChgTIMSSDISPComments" runat="server"> 
					            <font size="2">Reason for Ineligibility or Refusal (Up to 1024 characters):<span id='ChgTIMSSDISPCommentsspan'></span></font><br>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatordbgrade_ChgTIMSSDISPComments" runat="server" 
                                        ErrorMessage="Please provide a reason for ineligibility or refusal!" ControlToValidate="dbgrade_ChgTIMSSDISPComments" ForeColor="Red" Font-Bold="true" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="dbgrade_ChgTIMSSDISPComments" runat="server" rows="5" Columns="55" onkeyup="Edited();SetLimitToTextArea(this,1024, 'ChgTIMSSDISPCommentsspan')" TextMode="MultiLine"></asp:TextBox>
					            <asp:ValidationSummary ID="MyValidationSummary" runat="server" ShowMessageBox="true" ShowSummary="false" />
					    </div>
					</td>
				</tr>
				
				<script type='text/javascript'>//initial_ChgTIMSSDISPComments('block');</script>
                
				<tr>
					<td title="" width="120"><font size="2">Scheduled Assessment Date:</font></td>
					<td title="" nowrap="nowrap">
                        <asp:DropDownList ID="dbgrade_ScheDate" runat="server" onchange = "weekofday(this);Edited();Reset_ReasonChgSCHEDATE();" DataValueField="Value" DataTextField="Name"></asp:DropDownList>&nbsp;
						<font size="2"><label id="schedateOfWeek"></label></font>
						<input type ="hidden" name="HiddenSchedate" value ="" />
                        <input type ="hidden" name="db.ReasonChgSCHEDATE.num" value ="" />
			    	</td>
				</tr>

			   <script type='text/javascript'>//initial_ChgSCHEDATE('none');</script>	
				
				<tr>
					<td width="120" title=""><font size="2">Scheduled Assessment Time:</font></td>
					<td title="">
                        <asp:TextBox ID="dbgrade_ScheTime" runat="server" size="8" maxlength="8" onchange="Edited();"></asp:TextBox>
					</td>
				</tr>
				
                 <tr>
					<td width="120" title=""><font size="2">Assessment Arrival Time:</font></td>
					<td title="">
                        <asp:TextBox ID="dbgrade_ArrivalTime" runat="server" size="8" maxlength="8" onchange="Edited();"></asp:TextBox>
					</td>
				  </tr>

    <asp:Panel ID="PanelScheDateTime2" runat="server">
        
				<%--<tr>

					<td title="" width="120"><font size="2">Scheduled Assessment Date 2:</font></td>
					<td title="" nowrap>
                    
                        <asp:DropDownList ID="dbgrade_ScheDate2" runat="server" onchange = "weekofday2(this);Edited();Reset_ReasonChgSCHEDATE2();" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
                        &nbsp;

						 <font size="2">
					<label   id="schedateOfWeek2"></label> </font>
					
						<input type ="hidden" name="HiddenSchedate2" value ="">
			    	</td>
				</tr>
                     <input type ="hidden" name="db.ReasonChgSCHEDATE2.num" value ="">
				     <script type='text/javascript'>				         //initial_ChgSCHEDATE('none');</script>	

				<tr>
					<td width="120" title=""><font size="2">Scheduled Assessment Time 2:</font></td>
					<td title="">
                        <asp:TextBox ID="dbgrade_ScheTime2" runat="server" size="8" maxlength="8" onchange="Edited();"></asp:TextBox>
					</td>
				
				</tr>

                   <tr>
					<td width="120" title=""><font size="2">Assessment Arrival Time 2:</font></td>
					<td title="">
                        <asp:TextBox ID="dbgrade_ArrivalTime2" runat="server" size="8" maxlength="8" onchange="Edited();"></asp:TextBox>
					</td>
				  </tr>--%>

    </asp:Panel>

    <asp:Panel ID="PanelG4" runat="server">
        <tr>
        <td valign="top">Date school returns from winter break:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG4_DateSchoolReturnsFromWinterBreak" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Date school returns from spring break:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG4_DateSchoolReturnsFromSpringBreak" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Last day of school for the year:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG4_LastDayofSchool" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Number of 4<sup>th</sup> grade classes:</td>
        <td valign="top"><asp:TextBox ID="dbG4_NumberOfClasses" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td>
    </tr>
    
    </asp:Panel>
    <asp:Panel ID="PanelG8" runat="server">
        <tr>
        <td valign="top">Date school returns from winter break:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG8_DateSchoolReturnsFromWinterBreak" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Date school returns from spring break:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG8_DateSchoolReturnsFromSpringBreak" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Last day of school for the year:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG8_LastDayofSchool" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top"><asp:Label ID="lblG8Math" runat="server" /></td>
        <td valign="top"><asp:TextBox ID="dbG8_NumberOfMathClasses" runat="server" Columns="3" MaxLength="4" onchange="Edited();"></asp:TextBox></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="PanelG12" runat="server">
        <tr>
        <td valign="top">Date school returns from winter break:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG12_DateSchoolReturnsFromWinterBreak" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Date school returns from spring break:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG12_DateSchoolReturnsFromSpringBreak" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Last day of school for the year for 12<sup>th</sup> graders:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG12_LastDayofSchool" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Advanced Eligibility:</td>
        <td valign="top">
                        <asp:DropDownList ID="dbG12_AdvancedEligibility" runat="server" DataValueField="Value" DataTextField="Name" onchange="Edited();">
                        </asp:DropDownList></td>
    </tr>
    <tr>
        <td valign="top">Advanced Eligibility Comments:  <span id='s_AdvancedEligibilityComments'></span></td>
        <td valign="top"><asp:TextBox ID="dbG12_AdvancedEligibilityComments" runat="server" rows="5" 
                        Columns="55" onkeyup="Edited();SetLimitToTextArea(this,1024, 's_AdvancedEligibilityComments')" 
                        onKeyPress="return maxLength(this,'1024');" 
                        onpaste="return maxLengthPaste(this,'1024');" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top">Advanced Math Comments:  <span id='s_AdvancedMathComments'></span></td>
        <td valign="top"><asp:TextBox ID="dbG12_AdvancedMathComments" runat="server" rows="5" 
                        Columns="55" onkeyup="Edited();SetLimitToTextArea(this,1024, 's_AdvancedMathComments')" 
                        onKeyPress="return maxLength(this,'1024');" 
                        onpaste="return maxLengthPaste(this,'1024');" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top">Physics Comments:  <span id='s_AdvancedPhysicsComments'></span></td>
        <td valign="top"><asp:TextBox ID="dbG12_AdvancedPhysicsComments" runat="server" rows="5" 
                        Columns="55" onkeyup="Edited();SetLimitToTextArea(this,1024, 's_AdvancedPhysicsComments')" 
                        onKeyPress="return maxLength(this,'1024');" 
                        onpaste="return maxLengthPaste(this,'1024');" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    </asp:Panel>
				<tr>
					<td colspan="2" width="322"><font size="2">Comments (Up to 1024 characters): <span id='s_commentspan'></span></font></td>
				</tr>
				<tr>
                <td colspan="2" width="322"><asp:TextBox ID="db_s_comment" runat="server" rows="5" 
                        Columns="55" onkeyup="Edited();SetLimitToTextArea(this,1024, 's_commentspan')" 
                      TextMode="MultiLine"></asp:TextBox></td>
				</tr>
			</table>
            </td>
        </tr>
    </table>

<table class="tbform" border="0" width="100%" id="tblPreAssessment" style="display:<%=me.tblPreAssessmentCSSDisplay() %>">
<tr><td colspan="3"><hr /></td></tr>
<tr>
    <td colspan=3><u>
        <strong>Pre-Assessment</strong></u>
        <br />&nbsp;
    </td>
</tr>

<tr>
      <td>
            <strong><asp:Label ID="LabelSampleListAvail" runat="server" Text="Class List Available:"></asp:Label></strong><br />
            <asp:Label ID="SampleListAvail" runat="server"></asp:Label>
            <p>&nbsp;</p>
      </td>
       <td nowrap="nowrap">
            <strong><asp:Label ID="LabelClassListAvailableDate" runat="server" Text="Class List Available Date:"></asp:Label></strong><br />
            <asp:Label ID="ClassListAvailableDate" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
</tr>
<tr>
        <td nowrap="nowrap" runat="server" id="tdCLFStatusCode">
            <strong><asp:Label ID="LabelCLFStatusCode" runat="server" Text="Class List Status Code:"></asp:Label></strong><br />
            <asp:Label ID="CLFStatusCode" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
        <td nowrap="nowrap" runat="server" id="tdCLFCompletedDate">
            <strong><asp:Label ID="LabelCLFCompletedDate" runat="server" Text="Class List Completed Date:"></asp:Label></strong><br />
            <asp:Label ID="CLFCompletedDate" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
</tr>
<tr>
    <td runat="server" id="tdNumberofClasses">
        <strong><asp:Label ID="LabelNumberofClasses" runat="server" Text="Number of Classes Sampled:"></asp:Label></strong><br />
        <asp:Label ID="NumberofClasses" runat="server"></asp:Label>
            <p>&nbsp;</p>
    </td>
    <td runat="server" id="tdStudentSampled">
        <strong><asp:Label ID="LabelStudentSampled" runat="server" Text="Classes Sampled:"></asp:Label></strong><BR>
        <asp:Label ID="StudentSampled" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
    <td nowrap="nowrap" runat="server" id="tdStudentsSampled">
        <strong><asp:Label ID="LabelStudentsSampled" runat="server" Text="Number of students sampled:"></asp:Label></strong><br/>
        Advanced Science: 
        <asp:Label ID="StudentsSampledAdvancedScience" runat="server" Text="Number of students sampled:"></asp:Label><br />
        Advanced Mathematics: 
        <asp:Label ID="StudentsSampledAdvancedMathematics" runat="server" Text="Number of students sampled:"></asp:Label>
            <p>&nbsp;</p>
    </td>
</tr>
<tr>

    <td nowrap="nowrap">
        <strong><asp:Label ID="LabelSTLFStatusCode" runat="server" Text="STLF Status Code:"></asp:Label></strong><br/>
        <asp:Label ID="STLFStatusCode" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
    <td nowrap="nowrap">
        <strong><asp:Label ID="LabelDateOfSTLFMailing" runat="server" Text="Date of STLF Mailing:"></asp:Label></strong><br/>
        <asp:Label ID="DateOfSTLFMailing" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
    <td nowrap="nowrap">
        <strong><asp:Label ID="LabelSTLFCompletedDate" runat="server" Text="STLF Completed Date:"></asp:Label></strong><br/>
        <asp:Label ID="STLFCompletedDate" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
</tr>
<tr>
    <td nowrap="nowrap">
        <strong><asp:Label ID="LabelTTFStatusCode" runat="server" Text="TTF Status Code:"></asp:Label></strong><br/>
        <asp:Label ID="TTFStatusCode" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
</tr>
<tr>
    <td nowrap="nowrap">
        <strong><asp:Label ID="LabelSTFStatusCode" runat="server" Text="STF Status Code:"></asp:Label></strong><br/>
        <asp:Label ID="STFStatusCode" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
    <td nowrap="nowrap">
        <strong><asp:Label ID="LabelSTFSentToTADate" runat="server" Text="STF sent to TA Date:"></asp:Label></strong><br/>
        <asp:Label ID="STFSentToTADate" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
</tr>

<tr><td colspan="3"><hr /></td></tr>

<!--end-->
</table>

<table class="tbform" border="0" width="100%" id="tblAssessment" style="display:<%=me.tblAssessmentCSSDisplay() %>">
<tr><td colspan="2"><hr /></td></tr>
<tr>
    <td colspan=2><u>
        <strong>Assessment</strong></u>
        <br />&nbsp;
    </td>
</tr>
<tr>
    <td nowrap="nowrap">
        <strong>Assessment Date:</strong><br />
            <asp:Label ID="AssessmentDate" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
    <td nowrap="nowrap">
        <strong>Assessment Time:</strong><br />
            <asp:Label ID="AssessmentTime" runat="server"></asp:Label>
        <p>&nbsp;</p>
    </td>
</tr>

<asp:Panel ID="PanelLabelAssessmentDateTime2" runat="server">
  <%--  <tr>
        <td nowrap="nowrap">
            <strong>Assessment Date 2:</strong><br />
                <asp:Label ID="AssessmentDate2" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
        <td nowrap="nowrap">
            <strong>Assessment Time 2:</strong><br />
                <asp:Label ID="AssessmentTime2" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
    </tr>--%>
</asp:Panel>

<tr>
    <td nowrap="nowrap">
        <strong>Assessment Location: (Maximum 80 Characters)</strong><span id='AssessmentLocationspan'></span><br />
            <asp:TextBox ID="dbgrade_AssessmentLocation" runat="server" size="50" MaxLength="80" onkeyup="Edited();SetLimitToTextArea(this,80, 'AssessmentLocationspan')" onchange="Edited();SetLimitToTextArea(this,80, 'AssessmentLocationspan')" onpaste="return maxLengthPaste(this,'80');"></asp:TextBox>
        <p>&nbsp;</p>
    </td> 
</tr>
<tr>
    <td colspan="2" valign="top">
        <strong>Assessment Day Logistics information: (Maximum 250 Characters)</strong><span id='AssessmentDayLogisticsInformationspan'></span><br/>
            <asp:TextBox ID="dbgrade_AssessmentDayLogisticsInformation" runat="server" Columns="60" Rows="3" onkeyup="Edited();SetLimitToTextArea(this,250, 'AssessmentDayLogisticsInformationspan')" TextMode="MultiLine" onKeyPress="return maxLength(this,250);" onpaste="return maxLengthPaste(this,250);"></asp:TextBox>
        <p>&nbsp;</p>
    </td>
</tr>
<tr>
    <td nowrap="nowrap">
        <strong>Parent Consent Type:</strong><br />
        <asp:DropDownList ID="dbgrade_ParentConsentType" runat="server" onchange = "Edited();" DataValueField="Value" DataTextField="Name"></asp:DropDownList>
        <p>&nbsp;</p>
    </td>
    <td nowrap="nowrap">
        <strong>Parent Consent Language:</strong><br />
        <asp:DropDownList ID="dbgrade_ParentConsentLanguage" runat="server" onchange = "Edited();" DataValueField="Value" DataTextField="Name">
        </asp:DropDownList>
        <p>&nbsp;</p>
    </td>
</tr>
<tr>
	<TD nowrap="nowrap">
        <strong>Pre-Assessment Call Completed?</strong><br />
        <asp:DropDownList ID="dbgrade_PreAssessmentCallCompleted" runat="server" onchange = "Edited();" DataValueField="Value" DataTextField="Name">
        </asp:DropDownList>
        <p>&nbsp;</p>
    </TD>
</tr>
<tr>
    <td colspan="2" valign="top">
        <strong>School incentive check made out to: (Maximum 80 Characters)</strong><span id='SchoolIncentiveCheckSentTxtspan'></span><br/>
            <asp:TextBox ID="dbgrade_SchoolIncentiveCheckSentTxt" runat="server" Columns="60" Rows="2" MaxLength="80" onkeyup="Edited();SetLimitToTextArea(this,80, 'SchoolIncentiveCheckSentTxtspan')" TextMode="MultiLine" onKeyPress="return maxLength(this,'80');" onpaste="return maxLengthPaste(this,'80');"></asp:TextBox>
        <p>&nbsp;</p>
	</td>
</tr>
<tr>
    <td colspan="2" valign="top">
        <strong>SC incentive check made out to: (Maximum 50 Characters)</strong><span id='SCIncentiveCheckSentTxtspan'></span><br/>
            <asp:TextBox ID="dbgrade_SCIncentiveCheckSentTxt" runat="server" Columns="60" Rows="1" onkeyup="Edited();SetLimitToTextArea(this,50, 'SCIncentiveCheckSentTxtspan')" TextMode="MultiLine" onKeyPress="return maxLength(this,'50');" onpaste="return maxLengthPaste(this,'50');"></asp:TextBox>
        <p>&nbsp;</p>
    </td>
</tr>

<tr><td colspan="2"><hr /></td></tr>

</table>

<table class="tbform" border="0" width="100%" id="tblPostAssessment" style="display:<%= me.tblPostAssessmentCSSDisplay() %>">

    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td colspan=3><u>
            <strong>Post-Assessment</strong></u>
            <br />&nbsp;
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <strong>Assessment  Completed?</strong> <br />
            <asp:DropDownList ID="dbgrade_AssessmentCompleted" runat="server" onchange = "Edited();MakeUpCHK();" DataValueField="Value" DataTextField="Name">
            </asp:DropDownList>
            <p>&nbsp;</p>
        </td>
        <td nowrap="nowrap">
                <!--
            <strong>Date of Make-up:</strong><BR>
                <asp:TextBox ID="dbgrade_DateOfMakeUp" runat="server" size="12" onchange="Edited();datecheck(this, 'Date of Make-up')"></asp:TextBox>
            <strong>(mm/dd/yyyy)</strong>
            -->
            <p>&nbsp;</p>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <strong>Date School Incentive Check Sent:</strong><br />
               <asp:Label ID="SchoolIncentiveCheckSent" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
        <td nowrap="nowrap">
            <strong>Date SC Incentive Check Sent:</strong><br />
               <asp:Label ID="SCIncentiveCheckSent" runat="server"></asp:Label>
            <p>&nbsp;</p>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <strong>Assessment Materials Mailed to Pearson?</strong> <br />
            <asp:DropDownList ID="dbgrade_AssessmentMaterialsMailedToPearson" runat="server" onchange = "Edited();" DataValueField="Value" DataTextField="Name">
                <asp:ListItem value="N" >No</asp:ListItem>
                <asp:ListItem value="Y" >Yes</asp:ListItem></asp:DropDownList>
            <p>&nbsp;</p>
        </td>
    </tr>

    <tr><td nowrap="nowrap" colspan="2"><hr /></td></tr>
    <tr><td nowrap="nowrap"><strong>FedEx Tracking</strong></td></tr>

    <tr>
        <td nowrap="nowrap">
           <table  class="dataheader" border="0" width="100%" cellpadding="4">
                <tr><td nowrap="nowrap">
                     <strong>FedEx Tracking #1:</strong><br />
                     <asp:TextBox ID="dbgrade_FedExNumber1" runat="server" size="18" maxlength="12" onchange="Edited();"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <strong>FedEx Tracking #2:</strong><br />
                    <asp:TextBox ID="dbgrade_FedExNumber2" runat="server" size="18" maxlength="12" onchange="Edited();"></asp:TextBox>
                </td>
                </tr>
          </table>
        </td> 
    </tr>

    <tr><td nowrap="nowrap" colspan="2"><hr /></td></tr>
    <tr><td nowrap="nowrap"><strong>UPS Tracking</strong></td></tr>
    <tr><td nowrap="nowrap"><table class="dataheader" border="0" width="100%" cellpadding="4"></td></tr>
    <tr>
        <td nowrap="nowrap">
            <strong>UPS Tracking #1:</strong><br />
            <asp:TextBox ID="dbgrade_UPSNumber1" runat="server" size="18" maxlength="18" onchange="Edited();"></asp:TextBox>
        </td>
        <td nowrap="nowrap">
            <strong>UPS Tracking #2:</strong><br />
            <asp:TextBox ID="dbgrade_UPSNumber2" runat="server" size="18" maxlength="18" onchange="Edited();"></asp:TextBox>
        </td>
    </tr>

</table>

<table class="tbform" border="0" width="100%">
    <tr><td nowrap="nowrap" colspan="2"><hr /></td></tr>

    <tr>
        <td>
	        <strong>District Approval From:</strong>
	        <br /><asp:HyperLink ID="DistrictUpdatePageHyperLink" runat="server">District Infomation Page</asp:HyperLink>
        </td>
    </tr>

    <tr><td colspan="2"><hr /></td></tr>
</table>

<br />
      <table border="0" width="100%" cellspacing="4" cellpadding="0" height="24" class="hotcell">
        <tr>
          <td width="220">
          &nbsp;
                <asp:Button ID="ButtonSave2" runat="server" Text="Save" class="hotcell2" onclientclick="ClearEdited();" />
                <input type="reset"  value="Reset" name="B1" onclick="ClearEdited();" class="hotcell2" />
              <%--  <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&print=1&gen=' + getFrm().tblGeneralState.value + '&pre=' + getFrm().tblPreAssessmentState.value + '&smt=' + getFrm().tblAssessmentState.value + '&pst=' + getFrm().tblPostAssessmentState.value));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>    --%>

               <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>&print=1&gen=' +<%=Server.HtmlEncode("getFrm().tblGeneralState.value") %>+ '&pre=' + <%Server.HtmlEncode("getFrm().tblPreAssessmentState.value") %>  + '&smt=' + <%=Server.HtmlEncode("getFrm().tblAssessmentState.value") %> + '&pst=' + <%=Server.HtmlEncode("getFrm().tblPostAssessmentState.value") %>));"><img id="Img2" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>    
          	</td>         

        </tr>
      </table>     
</asp:Content>

