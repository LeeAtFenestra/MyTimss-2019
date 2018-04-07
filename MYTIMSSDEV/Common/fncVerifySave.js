
//<--
var m_blnSpecDebrief = false; //flag for Spec Debriefing data change

function hideObj(id) {
    if (document.getElementById) { // DOM3 = IE5, NS6 
        //alert(document.getElementById(id).style.display);
        //document.getElementById(id).style.visibility = 'hidden';
        document.getElementById(id).style.display = 'none';
        //alert(document.getElementById(id).style.display);
    }
    else {
        if (document.layers) { // Netscape 4 
            //document[id].visibility = 'hidden';
            document[id].display = 'none';
        }
        else { // IE 4 
            //document.all[id].style.visibility = 'hidden';
            document.all[id].display = 'none';
        }
    }
}

function showObj(id) {
    if (document.getElementById) { // DOM3 = IE5, NS6 
        //document.getElementById(id).style.visibility = 'visible';
        document.getElementById(id).style.display = 'block';
    }
    else {
        if (document.layers) { // Netscape 4 
            //document[id].visibility = 'visible';
            document[id].display = 'block';

        }
        else { // IE 4 
            //document.all[id].style.visibility = 'visible';
            document.all[id].display = 'block';
        }
    }
}

function setCSSClassName(id, classname) {
    if (document.getElementById(id) != null) {
        document.getElementById(id).className = classname;
    }
} 

function getFrm() {
    var frm = document.forms['frm'];
    if (frm == null) {
        frm = document.forms['aspnetForm'];
    }
    return frm;
}
function DisableWarning()
{
 getFrm().edited.value = 0;

}

function HasControl()
{
	if (window.document.frm)
		if (window.getFrm().edited)
			return true;
		else
			return false;
	else
		return false;

}

//.......set's the edit flag
	function Edited()
	{
	    getFrm().edited.value = 1;
		imgEdit.toggle();
		imgEdit.enabled = false;
	}
//.......set's the edit flag without an 'edited' image
	function EditedNI()
	{
	    getFrm().edited.value = 1;
	}


//......Resets the edit flag
	function ClearEdited()
	{
	    getFrm().edited.value = 0;
		if(imgEdit.enabled == false)
		{
			imgEdit.enabled = true
			imgEdit.toggle();
		}
	}

//......Resets the edit flag with no edited image
	function ClearEditedNI()
	{
	    getFrm().edited.value = 0;

	}


  function VerifySaveClose()
  { //alert(VerifySave());

  	if(VerifySave())
  	{
  		getFrm().submit();		//save it
  	}else{
  		window.close();
  	}

  }

  function VerifySaveClose_Refresh()
  {

  	if(VerifySave())
  	{
  		getFrm().submit();		//save it
  	}else{
  		window.opener.location.href=window.opener.location.href;
  		window.close();
  	}

  }

//.......Checks the edit flag, and displays a confirmation message
//.......if necessary.

	function VerifySave()
	{
		if (HasControl())
		{
			if(getFrm().edited.value == 1 && getFrm().reader.value == 'False')
				if(confirm("You have unsaved values, would you like to save them?"))
					return ValidateSCS();
				else
					return false;
			else
				return false;
		}
		else
			return false;

	}




	function SetPost(pgName)
	{


		if (VerifySave())
		{
			getFrm().action = pgName;
			getFrm().submit();		//save it
		}
		else
		{

			top.location.href = pgName;	//just move as a hyperlink
		}

	}

	function sgepLoadPage(url)
	{
		var pgName;

		if (window.getFrm().reader.value=='False')
		{
			if (VerifySave() )
			{

				window.getFrm().url.value = url;
				getFrm().submit();		//save it

			}
			else
			{

				top.location.href = url ;	//just move as a hyperlink
			}
		}
		else
		{
		    top.location.href = url ;	//just move as a hyperlink

		}
	}

	function sgepLoadPage2(url)
	{
		var pgName;


		if (VerifySave() )
		{

			window.getFrm().url.value = url;
			getFrm().submit();		//save it

		}
		else
		{

			top.location.href = url ;	//just move as a hyperlink
		}

	}



function Schoolautosave()
{ 

	if (getFrm().reader.value == 'False' && getFrm().edited.value == 1 )
	{ 

				var agree=confirm("You have modified data on this page. Do you wish to save these changes before proceeding? Click 'Ok' to save your changes, 'Cancel' to cancel the changes made.");
				if (agree) {
    				    getFrm().submit();
	    			    __doPostBack('ctl00$cphTopNavigationLevel2$ButtonSave', '');
                    }
				else {
				    getFrm().reset();
				    ClearEdited();
                    }

	}
}

function on_lose_focus() {

    //if ((modified_data == "Yes") && (checkflag == "No")) {
    if (getFrm().reader.value == 'False' && getFrm().edited.value == 1) {

        return ("If you leave this page, you will lose your changes!");

    }
}

function BeforeUnloadAutoSave() {
    //alert(getFrm().reader.value + ' - ' + getFrm().edited.value);
    if (getFrm().reader.value == 'False' && getFrm().edited.value == 1) {
        //alert('1');
        var agree = confirm("Do you want to save your changes? \nClick 'Ok' to save your changes, or 'Cancel' if you do not want to save.");
        if (agree) {
            return true;
            __doPostBack('ctl00$cphTopNavigationLevel2$ButtonSave', '');
        }
        else {
            //getFrm().reset();
        }
    }
    else {
        //alert('2');
        return;
    }
}
var AutoPostbackControlId = 'ctl00$cphTopNavigationLevel2$ButtonSave';
function Personnelautosave()
{
    if (getFrm().reader.value == 'False' && getFrm().edited.value == 1)
	{

				var agree=confirm("Changes must be saved before adding or editing personnel information. \nClick 'Ok' to save your changes, or 'Cancel' if you do not want to save.");
				if (agree) {
				    //getFrm().submit();
				    //__doPostBack('ctl00$cphTopNavigationLevel2$ButtonSave', '');
				    __doPostBack(AutoPostbackControlId, '');
                }
				else {
				    getFrm().reset();
				    ClearEdited();
                }
		  return false;
	}
	else return true;
}

function SDCFautosave()
{

	if (getFrm().reader.value == 'False' && getFrm().edited.value == 1 )
	{

				var agree=confirm("Changes must be saved before approving SDCF information. \nClick 'Ok' to save your changes, or 'Cancel' if you do not want to save.");
				if (agree)
					{	getFrm().submit() ;}
				else {
				    getFrm().reset();
				    ClearEdited();
				}
		  return agree;
	}
	else return true;
}



function Schoolautosaveb(flag)
{

	if (getFrm().reader.value == 'False' && getFrm().edited.value == 1)
	{
		 if (flag) {
			}
		 else
			{
				var agree=confirm("You have modified data on this page. Do you wish to save these changes before proceeding? Click 'Ok' to save your changes, 'Cancel' to cancel the changes made.");
				if (agree)
					{	getFrm().submit() ;}
				else {
				    getFrm().reset();
				    ClearEdited();
				}
			}
  }
}

//*************Hui's Part**********************************
function ValidatPLInfo()
{
    var strVal
    strVal = document.all("db.HowSentCode.num").value
    if (strVal == 9)
        document.all("db.HowSent.char").disabled = false
    else
    {
        document.all("db.HowSent.char").disabled = true
        document.all("db.HowSent.char").value = ""
    }
}
function SetFallVisitStatus() //for genneral page
{
   try
	{
		document.all("db.FallVisitStatus.char").value = "1"
	}
	catch(e)
	{
		alert("fncVerifySave.js: SetFallVisitStatus()\n" + e.description)
	}
}
function SetAssmtMakeupDate() //for genneral page
{
   try
	{
		if (document.all("db.ScheDate.char").value == "")
		{
            document.all("db.MUDATE.char").disabled = true
		    document.all("db.MUDATE.char").value = ""
		}
		else
            document.all("db.MUDATE.char").disabled = false

	}
	catch(e)
	{
		alert("fncVerifySave.js: SetAssmtMakeupDate()\n" + e.description)
	}
}
//function SetPLRefusal() //For preassessment page
//{
//   try
//	{
//	/*
//	[Dward Moore requested IV#3048
//    SCS, School Edit, Preassessment tab - Parent Letter Information section
//    If the answer to the question "Has school notified parents?" is "No" or "Not Answered", 
//    then the system should not allow any data entry or changes to any of the fields in this section.  
//    Error pop up should state: "If the school has not notified parents, other fields cannot be updated 
//    in this section."
//    NOTE: This is done on page but requested disabling onLoad
//	*/
//	  if (document.all("SC").value == "No")
//      {
//      
//        //if (getFrm().elements["db.SchoolNotifiedParents.char"].selectedIndex != '1')
//	    //{
//	    //    getFrm().elements["db.DateSent.char"].disabled = true;
//	        //getFrm().elements["db.HowSentCode.num"].disabled = true;
//	        //getFrm().elements["db.HowSent.char"].disabled = true;
//	    //    getFrm().elements["db.SentTo.char"].disabled = true;
//	    //    getFrm().elements["db.rcvdAC.char"].disabled = true;
//	        //getFrm().elements["comboPSRefusal"].disabled = true;
//	    //    getFrm().elements["comboNewEnrlParentsNotified"].disabled = true;
//	        //alert("If the school has not notified parents, other fields cannot be updated in this section.");
//	    //}
//	    //else{
//	    //    getFrm().elements["db.DateSent.char"].disabled = false;
//	    //  getFrm().elements["db.HowSentCode.num"].disabled = false;
//	    //  getFrm().elements["db.HowSent.char"].disabled = false;
//	    //   getFrm().elements["db.SentTo.char"].disabled = false;
//	    //   getFrm().elements["db.rcvdAC.char"].disabled = false;
//	    //  getFrm().elements["comboPSRefusal"].disabled = false;
//	    //   getFrm().elements["comboNewEnrlParentsNotified"].disabled = false;
//	    }
//	   }
//	   
//		//if (document.all("comboPSRefusal").value == "Y")
//        //    document.all("spanRefusal").style.display='inline';
//		//else
//        //{
//        //    document.all("txtRefusalNum").value = "";
//        //    document.all("spanRefusal").style.display='none';
//        //}
//	}
//	catch(e)
//	{
//		alert("fncVerifySave.js: SetPLRefusal()\n" + e.description)
//	}
//}


function ISInPersonPAVRequired() //For preassessment page: “Is an in-person pre-assessment visit required?”
{
   try
	{
	    if (document.all("SC").value == "No") {

	        if (getFrm().elements["db.InPersonPAVRequired.char"].selectedIndex != '1') {
	            getFrm().elements["db.PreAssessDate.char"].disabled = true;
	            getFrm().elements["db.PreAssessArriveTime.char"].disabled = true
	        }
	        else {
	            getFrm().elements["db.PreAssessDate.char"].disabled = false;
	            getFrm().elements["db.PreAssessArriveTime.char"].disabled = false
	        }
	    }
	    return true;
	}
	catch(e)
	{
		alert("fncVerifySave.js: SetPLRefusal()\n" + e.description)
	}
}

function ValidateSCS() //For Validating SCS pages
{
   try
	{
		if (document.URL.indexOf("default_pre.asp")>-1)
			if (! ValidatePreassessment()) return false;
		if (document.URL.indexOf("default_gen.asp")>-1)
		    if (! ValidateGen()) return false;
		if (document.URL.indexOf("default_spec.asp")>-1)
		{
        	//alert(getFrm().NIES1.value);
//The following "NIES1" is a flag to fix the SAVE error if special study is NOT NIES from default_spec.asp
		    if (getFrm().NIES1.value == "true")
		    {
		        if (! ValidateSpec()) return false;
            }
            if (m_blnSpecDebrief) if (! ValidateSpecDebrief()) return false;
		    if (! ValidateSpecHSTS()) return false;
        }
		return true;
	}
	catch(e)
	{
		alert("fncVerifySave.js: ValidateSCS()\n" + e.description)
	}
}

/*Dan weber created for NIES 2009*/
function ValidateSpec()
{
   try
	{
	var retval = true;
	var msg = "";
	var math1 = 0;
	var math2 = 0;
	
//alert("getFrm().survey_make.value="+getFrm().survey_make.value);	
	//Make sure not empty
	if (getFrm().ses_WD.value == "" | getFrm().ses_Excl.value == "" | 
	getFrm().add_stud.value == "" | getFrm().ses_Ref.value == "" | getFrm().ses_abs.value == "" | getFrm().survey_make.value  == ""
	)
	{
	    msg = msg + "All regular NIES session data must be entered before NIES data can be saved.\n";
	    msg = msg + "You do not have to enter makeup NIES session data until after the makeup NIES session is held\n";
	    msg = msg + "(enter 0 if no makeup NIES session).";   
	    retval = false;
	}
	
	var rIndex;
        rIndex = getFrm().elements["db.ScheDate.char"].options.selectedIndex;
        
	if (
	  (getFrm().ses_WD.value != "" &&
      getFrm().ses_Excl.value != "" && 
	  getFrm().add_stud.value != "" && 
	  getFrm().ses_Ref.value != "" && 
	  getFrm().ses_abs.value != "" && 
	  getFrm().survey_make.value  != "" ) &&
	  (getFrm().elements["db.ScheDate.char"].options[rIndex].value =="NULL")
	  )
	{
	    msg = msg + "\nNIES Survey Date value can not be equal to 'No Date'\n";
	    msg = msg + "When all NIES session data have been entered.";
	    retval = false;
	}
	
	math1 = parseInt(getFrm().ses_WD.value);
	math2 = parseInt(getFrm().orig_samp.value) + parseInt(getFrm().add_stud.value);
	//alert("FIRST: math1 = "+math1 +" math2 = "+math2+"\n");
	
// Math RULES
//NIES_ses_tot = orig_samp + add_stud
//var tmp_NIES_ses_tot = parseInt(getFrm().orig_samp.value) + parseInt(getFrm().add_stud.value)

//NIES_ses_tba = NIES_ses_tot - ses_WD - ses_Ineligible_AIAN - ses_Ineligible_NAEP
//var tmp_NIES_ses_tba = tmp_NIES_ses_tot - parseInt(getFrm().ses_WD.value) - parseInt(getFrm().ses_Ineligible_AIAN.value) - parseInt(getFrm().ses_Ineligible_NAEP.value)

//NIES_survey_tot_original = NIES_ses_tba - NIES_ses_Excl - NIES_ses_Ref - NIES_NAEP_Ref - NIES_ses_Abs
//var tmp_NIES_survey_tot_original = tmp_NIES_ses_tba - parseInt(getFrm().ses_Excl.value) - parseInt(getFrm().ses_Ref.value) - parseInt(getFrm().NAEP_REF.value) - parseInt(getFrm().ses_abs.value)

//NIES_survey_tot = NIES_survey_tot_original + NIES_survey_make
//var tmp_NIES_survey_tot = tmp_NIES_survey_tot_original + parseInt(getFrm().survey_make.value)
	
	//NIES Data Entry Rules
	//if ((getFrm().ses_WD.value + getFrm().ses_Ineligible_AIAN.value + getFrm().ses_Ineligible_NAEP.value) 
	//> (getFrm().orig_samp.value + getFrm().add_stud.value))
    if (math1 > math2)
	{
	    msg = msg + "\nWithdrawn/Ineligible for NIES must be less than or equal to\n the sum of Original NIES Sample and Sampled New Enrollees for NIES.\n";
	    retval = false;
	}

    math1 = 0;
    math1 = parseInt(getFrm().ses_WD.value) + parseInt(getFrm().ses_Excl.value) + parseInt(getFrm().ses_Ref.value) + parseInt(getFrm().ses_abs.value);
	//alert("SECOND: math1 = "+math1 +" math2 = "+math2+"\n");
	
	//if ((getFrm().ses_WD.value + getFrm().ses_Ineligible_AIAN.value + getFrm().ses_Ineligible_NAEP.value + 
	//getFrm().ses_Excl.value + getFrm().ses_Ref.value + getFrm().NAEP_REF.value + getFrm().ses_abs.value) 
	//> (getFrm().orig_samp.value + getFrm().add_stud.value))
    if (math1 > math2)
	{
	    msg = msg + "\nThe sum of Withdrawn/Ineligible for NIES, Excluded from NIES, Refused NIES, and Absent from NIES\n must be less than or equal to the sum of Original Sample and Added Students.\n";
	    retval = false;
	}
	
    if (parseInt(getFrm().survey_make.value) > parseInt(getFrm().ses_abs.value))
    {
	    msg = msg + "\nThe value of Surveyed Makeup must be less than or equal to the value of Absent.\n";
	    retval = false;
    
    }
    
    if (
	  (getFrm().ses_WD.value == "" &&
      getFrm().ses_Excl.value == "" && 
	  getFrm().add_stud.value == "" && 
	  getFrm().ses_Ref.value == "" && 
	  getFrm().ses_abs.value == "" && 
	  getFrm().survey_make.value  == "" ) &&
	  (getFrm().elements["db.ScheDate.char"].options[rIndex].value =="NULL")
	  )
	{
	   
	    msg = "";
	    retval = true;
	}
    
	if (retval == false)
	{
	    alert(msg);
	}

    }
	catch(e)
	{
		alert("fncVerifySave.js: ValidateSpec()\n" + e.description)
	}
	return retval;
}

function ValidateGen() //For Validating SCS General page
{
   try
	{
	    var retval;
	    var RegExPattern = /^\d+$/; //makes sure it is a positive digit
	    var privFlg = Trim(document.all("privatFlag").value);

		//var strNewEnrollees = document.all("NumNewEnrollees").value.toUpperCase()
        var objSchdDate = new Date(document.all("db.ScheDate.char").value)
       // var objMakeupDate = new Date(document.all("db.MUDATE.char").value)
        var objPLSentDate = new Date(document.all("hdnPLDateSent").value)
       // if (objSchdDate!="NaN" && objMakeupDate!="NaN" && objSchdDate > objMakeupDate)
        //    alert("The Make-up Date cannot be earlier than Scheduled Assessment Date. \n Please try agan.");
	    retval = true;

         if (objSchdDate!="NaN" && objPLSentDate!="NaN" && objSchdDate < objPLSentDate)
         {
            alert("The Scheduled Assessment Date cannot be earlier than the Parent Notice Sent date on the Preaasessment Tab. \n Please try agan.");
	        retval = false;
	     }

	     //NOTE: Taken out 11/20/2009--It must be able to be blank and have characters and integers public AND private
        //if (privFlg == 0)
        //{        
            //if (!document.all("SEASCH").value.match(RegExPattern)&& document.all("SEASCH").value == "")
            //{
            //    alert("School State ID must be must be an integer value.\n");
		    //    retval = false;
		    //}
	    //}
    //alert("retval = "+retval+"\n");
		return retval;
	}
	catch(e)
	{
		alert("fncVerifySave.js: ValidateGen()\n" + e.description)
	}
}
function ValidatePreassessment() //For validating preassessment page
{
   try
	{
		//var oDate1 = new Date("") // new date object
		//var oDate2 = new Date("02/29/2001") // new date object
        //alert(oDate1>oDate2) //set date

		//alert(document.all("db.SchoolPktSentDate.char").value + " " + document.all("db.PreAssessDate.char").value)
		//alert(document.all("db.SchoolPktSentDate.char").value > document.all("db.PreAssessDate.char").value)
		if (document.all("SC").value == "Yes")
        {
            //var strPLToSchool = document.all("db.StateProvidedPLToSchool.char").value.toUpperCase()
            //var strNCESPrt = document.all("db.SchoolUsingNCESPL.char").value.toUpperCase()
            //if (strNCESPrt.length > 0  && strNCESPrt != "Y" && strNCESPrt != "N")
		    //    alert("The Schools using NCES parent letter field requires an entry of (Y) or (N).")
	        //else if (strPLToSchool.length > 0 && strPLToSchool != "Y" && strPLToSchool != "N")
		    //    alert("The State provided its version of parent letter to schools requires an entry of (Y) or (N).")
	        //else if (Trim(document.all("txtRefusalNum").value)!="" && !IsNumeric(document.all("txtRefusalNum").value))
		    //    alert("Please input a number in the parent/student refusal number field.")
	        /*
	        else if (strNewEnrollees !="" && !IsNumeric(strNewEnrollees))
		        alert("Please input a number for the number of students as new enrollees.")
	        else if (Trim(document.all("NumNEAddedToSample").value.toUpperCase()) !="" && !IsNumeric(Trim(document.all("NumNEAddedToSample").value)))
		        alert("Please input a number for the # of new enrollees sampled.")
	        */
	        //else
	            return true;
	    }
	    else if (document.all("SC").value == "No")
        {
            var strNewEnrollees = document.all("NumNewEnrollees").value.toUpperCase()
            //var strNotifyPrt = Trim(document.all("db.SchoolNotifiedParents.char").value.toUpperCase())
	        var objSentDate = new Date(document.all("db.SchoolPktSentDate.char").value)
	        var objPreVisitDate = new Date(document.all("db.PreAssessDate.char").value)
	        var objScheDate = new Date(document.all("hdnScheDate").value)
	        var objPLDateSent = new Date(document.all("db.DateSent.char").value)
	        var objPLDateReceived = new Date(document.all("db.rcvdAC.char").value)
	        var strValue = "";
	        //alert(document.all("NumNEAddedToSample").value.toUpperCase())
	        if (typeof(document.all["textTeachNumSci"]) == "object") strValue = Trim(document.all("textTeachNumSci").value)

	        //School Packet, Preassessment Visit,Teacher Information
	        if (Trim(document.all("db.SchoolPktSentDate.char").value)!="" && Trim(document.all("db.PreAssessDate.char").value) != "" && objSentDate > objPreVisitDate)
	            alert("The Preassessment Visit Date must be after the  School Packet Sent Date. \n");
	        else if (strValue!="" && isNaN(strValue))
		        alert("Please input a number for the Number of Science Teachers.")
	        //New Enrollees Procedure
	        else if (Trim(document.all("NumNewEnrollees").value)!="" && !IsNumeric(document.all("NumNewEnrollees").value))
		        alert("Please input a number for the number of students as new enrollees.")
	        else if (Trim(document.all("NumNEAddedToSample").value)!="" && !IsNumeric(document.all("NumNEAddedToSample").value))
		        alert("Please input a number for the # of new enrollees sampled.")
	        else if (Trim(document.all("NumNewEnrollees").value)!="" && Trim(document.all("NumNEAddedToSample").value)!="" && parseInt(Trim(document.all("NumNewEnrollees").value)) < parseInt(Trim(document.all("NumNEAddedToSample").value)))
		        alert("The number of new enrollees sampled cannot be larger than new enrollees.")
	        //Parent Letter Section
	       // else if (strNotifyPrt.length > 0 && strNotifyPrt != "Y" && strNotifyPrt != "N")
		       // alert("The field \"Has school notified parents\" requires an entry of (Y) or (N).")
	        else if (Trim(document.all("db.DateSent.char").value)!="" && Trim(document.all("hdnScheDate").value) != "" && objPLDateSent > objScheDate )
	            alert("The Parent Notice Sent date cannot be after the Assessment Date.\n Please try agan.");
	        else if (Trim(document.all("db.DateSent.char").value)!="" && Trim(document.all("db.rcvdAC.char").value) != "" && objPLDateSent > objPLDateReceived )
	            alert("The Parent Notice sent date cannot be after the Date Parent Notice received by NAEP rep.\n Please try agan.");
	        //Comment out due to IV4978 by Hui
	        //else if (Trim(document.all("db.HowSent.char").value) == "" && document.all("db.HowSentCode.num").value == 9)
		    //    alert("An entry in the (Other) field is required, since \"Other\" was selected from the (How Sent) dropdown list. \n Please try agan.")
	        //else if (Trim(document.all("txtRefusalNum").value)!="" && !IsNumeric(document.all("txtRefusalNum").value))
		    //    alert("Please input a number in the parent/student refusal number field.")
		    else
		        return true;
	    }
		return false;
	}
	catch(e)
	{
		alert("fncVerifySave.js: ValidatePreassessment()\n" + e.description)
	}
}

function ValidateSpecDebrief() //For Validating SCS General page
{
   try
	{
        if (typeof(frm.CheckQuestion1)=="undefined" || typeof(frm.CheckQuestion1)==null ) return true;
        if (frm.CheckQuestion1[0].checked||frm.CheckQuestion1[1].checked||frm.CheckQuestion1[2].checked||frm.CheckQuestion1[3].checked)
            return true;
        else
        {
            alert("Please answer the first Grade 12 Strategies Form question.");
		    return false;
		}
	}
	catch(e)
	{
		alert("fncVerifySave.js: ValidateSpecDebrief()\n" + e.description)
	}
}
function ValidateSpecHSTS()
{
   try
	{
        if (typeof(frm.textTranscriptNum)=="undefined" || typeof(frm.textTranscriptNum)==null ) return true;
        if (isNaN(frm.textTranscriptNum.value))
        {
            alert("Please input a number in the 'Number of transcripts collected' field.");
            frm.textTranscriptNum.focus();
		    return false;
		}
        if (isNaN(frm.textTranscriptCost.value))
        {
            alert("Please input a number in the 'Cost per transcripts' field.");
            frm.textTranscriptCost.focus();
		    return false;
		}
        return true;
	}
	catch(e)
	{
		alert("fncVerifySave.js: ValidateSpecHSTS()\n" + e.description)
	}
}
//For NaepQC
function AddNewAdhoc(pid, gid)
{ 
    var strUrl, strType ="CBA";
    try
	{
	    //alert(frm.radioAssesstype[0].checked);return;
	    //strType = frm.hdnAssessType.value
	    if (strType=="OL")//OVERLAP
	    {
	        if (frm.radioAssesstype[0].checked)
	            strType = "PB";
	        else
	            strType = "CBA";
	    }
	    strUrl= "NaepQCAd.ASP?page=adnew&pid=" + pid + "&gid=" + gid + "&type=" + strType
	    if (isNaN(document.all("txtAdRows").value))
	    {
	        alert("Please input a valid student number.")
	        return;
	    }
	    else
	        strUrl += "&num=" + document.all("txtAdRows").value
	    //alert(strUrl);
	    //openAWindow(strUrl)
	    openAWindow(strUrl, "", 925, 600,0)
 	}
	catch(e)
	{
		alert("fncVerifySave.js: AddNewAdhoc()\n" + e.description)
	}
}
function SetNaepQCStatus(iid, status, obj)
{ 
    var strUrl;
    var objXML;
    try
	{
        if (!confirm("Are you sure you want to change the status?")) return;
	    strUrl= "NaepQCVerify.asp?Page=status&IsAjaxPostBack=Y&iid=" + iid + "&status=" + status;
        
	    //alert(strUrl);
	    if (window.ActiveXObject)
	    {
	        objXML=new ActiveXObject("Microsoft.XMLDOM");
	        objXML.async=false;
		    if (!objXML.load(strUrl)) 
		    {					
			    alert(objXML.parseError.reason+objXML.parseError.srcText);			
			    return;
		    }
	    }
	    else
	    {
	        alert("This function is under construction for none-IE browser.");
	        return;
	        //var parser=new DOMParser();  
	        //objXML = parser.parseFromString("<a>XXX</a>","text/xml");
	        objXML =document.implementation.createDocument("","",null);
            objXML.load("<a>XXX</a>");
            //objXML.onload=getmessage;
	        alert(objXML.getElementsByTagName("a")[0].childNodes[0].nodeValue);
	    }
	    //alert(objXML.xml);
	    document.getElementById("tdStatus"+iid).innerHTML=objXML.text;
	    if (status =="2")
	    {
            document.all("btnEdit"+iid).disabled = false
            document.all("btnVerify"+iid).disabled = true
            document.all("btnView"+iid).disabled = true
	    }
	    else if (status =="3")
	    {
            document.all("btnEdit"+iid).disabled = true
            document.all("btnVerify"+iid).disabled = true
            document.all("btnView"+iid).disabled = false
	    }
	    else if (status =="8")
	    {
            document.all("btnEdit"+iid).disabled = true
            document.all("btnVerify"+iid).disabled = true
            document.all("btnView"+iid).disabled = false
	    }
	    obj.disabled = true;
        obj.style.borderStyle = "none";
        obj.style.cursor = "default";
	    
 	}
	catch(e)
	{
		alert("fncVerifySave.js: SetStatus()\n" + e.description)
	}
}
function SetNAEPQCLogo(obj,dir)
{ 
    try
	{
        if(dir==1)
        {
            //obj.disabled = true;
            if (obj.style.borderStyle=="outset") obj.style.borderStyle = "inset";
        }
        else
        {
            //obj.disabled = true;
            if (obj.style.borderStyle=="inset") obj.style.borderStyle = "outset";
        }
 	}
	catch(e)
	{
		alert(e.description)
	}
}



function checkdate(NewDate) 
{
    var result = true;
    result =(NewDate!='');
    if (result)
    { 
       var elems = NewDate.split("/");
       result = (elems.length == 3); // should be three components
	   if (result)
	   {
	     	
  			var myDayStr = parseInt(elems[1]);
			var myMonthStr =  parseInt(elems[0]);
			var myYearStr = parseInt(elems[2]);
			var myMonth = new Array('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'); 
			var myDateStr = myDayStr + ' ' + myMonth[myMonthStr] + ' ' + myYearStr;

			var myDate = new Date();
			myDate.setFullYear( myYearStr, myMonthStr, myDayStr );

			if ( myDate.getMonth() != myMonthStr ) {
  		           result = false;
			}
		}
		else
		{
		 result = false;
		}
		
		
		
		if(result==false)
		{  
		    	alert(NewDate +" is not valid date. Please enter a date in the format MM/DD/YYYY.");
				event.srcElement.focus();
				return false;
		}		
	
	}
 
}
//Grade 12 debref
function FormSpecLoad()
{ 
    try
	{
        if (typeof(frm.TextQuestion2)=="undefined" || typeof(frm.TextQuestion2)==null ) return;
        //frm.TextQuestion1.readOnly = !frm.CheckQuestion1[14].checked;
        frm.TextQuestion2.readOnly = !frm.CheckQuestion2[16].checked;
        frm.TextQuestion4.readOnly = !frm.CheckQuestion4[15].checked;
        frm.TextQuestion3.readOnly = !frm.CheckQuestion3[5].checked;
        //alert(!frm.CheckQuestion5[9].checked)
        frm.TextQuestion5.readOnly = !frm.CheckQuestion5[7].checked;
 	}
	catch(e)
	{
		alert(e.description)
	}
}
//Grade 12 Strategies Form/debriefing validation
function SetComments(obj)
{
    try
	{
        //alert(obj.type + obj.name + "  "+obj.value)
        m_blnSpecDebrief = true;//determine whether the Debriefing form changed
        if(obj.type =="radio")
        {
            if (obj.name =="CheckQuestion1")
            {
                //if (obj.value == "2") frm.TextQuestion3.value="";
                //frm.TextQuestion3.readOnly = (frm.CheckQuestion3[1].checked);
            }
            else if (obj.name =="CheckQuestion5")
            {
                if (obj.value != "10") frm.TextQuestion5.value="";
                frm.TextQuestion5.readOnly = (!frm.CheckQuestion5[9].checked);
            }
        }
        else
        {
            if (obj.name =="CheckQuestion2")
            {
                //IV2088, selection of 14 exclude other choices;  "The school did nothing to publicize NAEP."
                if (obj.value == "18" && obj.checked)
                {
                    for (var i=0; i < frm.CheckQuestion2.length-1; i++)
  		                frm.CheckQuestion2[i].checked = false;
  		            //frm.CheckQuestion2[17].checked = false;
  		            frm.TextQuestion2.value = "";
                    frm.TextQuestion2.readOnly = true;
                }
                else if (obj.value == "17")
                {
                    if (frm.CheckQuestion2[17].checked)
                    {
                        obj.checked = false;
                        return;
                    }
                    if (!obj.checked) frm.TextQuestion2.value="";
                    frm.TextQuestion2.readOnly = !obj.checked;
                }
                else
                    if (frm.CheckQuestion2[17].checked) obj.checked = false;
            }
            else if (obj.name =="CheckQuestion3")
            {
                //IV2088, selection of 11 exclude other choices; "The school did nothing to motivate the students"
                if (obj.value == "7" && obj.checked)
                {
                    for (var i=0; i < frm.CheckQuestion3.length-1; i++)
  		                frm.CheckQuestion3[i].checked = false;
  		            //frm.CheckQuestion3[6].checked = false;
  		            frm.TextQuestion3.value = "";
                    frm.TextQuestion3.readOnly = true;
                }
                else if (obj.value == "6")
                {
                    if (frm.CheckQuestion3[6].checked)
                    {
                        obj.checked = false;
                        return;
                    }
                    if (!obj.checked) frm.TextQuestion3.value="";
                    frm.TextQuestion3.readOnly = !obj.checked;
                }
                else
                    if (frm.CheckQuestion3[6].checked) obj.checked = false;
            }
            else if (obj.name =="CheckQuestion4")
            {
                //IV2088, selection of 8 exclude other choices; "The school provided no incentives for the students"
                if (obj.value == "17" && obj.checked)
                {
                    for (var i=0; i < frm.CheckQuestion4.length-1; i++)
  		                frm.CheckQuestion4[i].checked = false;
  		            //frm.CheckQuestion4[12].checked = false;
  		            frm.TextQuestion4.value = "";
                    frm.TextQuestion4.readOnly = true;
                }
                else if (obj.value == "16")
                {
                    if (frm.CheckQuestion4[16].checked)
                    {
                        obj.checked = false;
                        return;
                    }
                    if (!obj.checked) frm.TextQuestion4.value="";
                    frm.TextQuestion4.readOnly = !obj.checked;
                }
                else
                {
                    if (frm.CheckQuestion4[16].checked)
                    {
                        obj.checked = false; 
                        return;
                    }
                    if (!frm.CheckQuestion4[12].checked &&(obj.value == "14"||obj.value == "15")) obj.checked = false;
                    if (obj.value == "13"&& !obj.checked)
                    {
                        frm.CheckQuestion4[13].checked = false;
                        frm.CheckQuestion4[14].checked = false;
                    }
                    if (obj.value == "14"&& obj.checked) frm.CheckQuestion4[14].checked = false;
                    if (obj.value == "15"&& obj.checked) frm.CheckQuestion4[13].checked = false;
                }
            }
            else if (obj.name =="CheckQuestion5")
            {
                if (obj.value == "9" && obj.checked)
                {
                    for (var i=0; i < frm.CheckQuestion5.length-1; i++)
  		                frm.CheckQuestion5[i].checked = false;
  		            frm.TextQuestion5.value = "";
                    frm.TextQuestion5.readOnly = true;
                }
                else if (obj.value == "8")
                {
                    if (frm.CheckQuestion5[8].checked)
                    {
                        obj.checked = false;
                        return;
                    }
                    if (!obj.checked) frm.TextQuestion5.value="";
                    frm.TextQuestion5.readOnly = !obj.checked;
                }
                else
                    if (frm.CheckQuestion5[8].checked) obj.checked = false;
            }
        }
 	}
	catch(e)
	{
		alert("fncVerifySave.js: SetComments()\n" + e.description)
	}
}
//HSTS
function CalculateHSTSTotalCost()
{ 
    var intTransNum = "";
    var fltCostPerTrans ="";
    var total;
    try
	{
	    intTransNum = parseInt(document.all("textTranscriptNum").value)
	    fltCostPerTrans = parseFloat(document.all("textTranscriptCost").value)
	    
	    if(isNaN(intTransNum)||isNaN(fltCostPerTrans)) return;
	    total = intTransNum * fltCostPerTrans
	    //document.all("textTotalCost").value = total.toFixed(2).toString();
	    document.getElementById("textTotalCost").value = total.toFixed(2).toString();

 	}
	catch(e)
	{
		alert("fncVerifySave.js: CalculateHSTSTotalCost()\n" + e.description)
	}
}
//WCBA 2010 Online Session Debriefing Form
function ValidateCheckBox(obj, v)
{ 
    var intTotalChecked = 0;
    var objChecks = document.all(obj.name)
    try
	{
	    //document.getElementById("textTotalCost").value = total.toFixed(2).toString();
        if (v == 1)
        {
            if (obj.checked)
            {
                for (var i=0; i < objChecks.length; i++)
                {
                    objChecks[i].checked = false                
                    OpenCloseText(objChecks[i], 1)
                }
                obj.checked = true
            }
        }
        else if (v<20)
        {
            for (var i=0; i < objChecks.length; i++)
            {
                if (objChecks[i].checked)
                    intTotalChecked += 1;
            }
            if (obj.checked && intTotalChecked >v) obj.checked = false
        }
 	}
	catch(e)
	{
		alert("fncVerifySave.js: ValidateCheckBox()\n" + e.description)
	}
}
function OpenCloseText(obj, flgcmmnt)
{ 
    var strObjName = "";
    var objTextbox =null;
    try
	{
	    //document.getElementById("textTotalCost").value = total.toFixed(2).toString();
	    if (flgcmmnt != 1) return;
	    strObjName = obj.name.substr(5);
        strObjName = "Text" + strObjName + "_" + obj.value;
        objTextbox = document.getElementById(strObjName);
        //alert(strObjName + " " +objTextbox)
        if (objTextbox == null) return;        
        if (obj.checked)
            objTextbox.style.display = "block";
        else
        {
            objTextbox.style.display = "none";
            objTextbox.value = "";
        }
 	}
	catch(e)
	{
		alert("fncVerifySave.js: OpenCloseText()\n" + e.description)
	}
}


function Schoolautosave_AIF()
{ 

	if (getFrm().reader.value == 'False' && getFrm().edited.value == 1 )
	{ 

				var agree=confirm("You have modified data on this page. Do you wish to save these changes before proceeding? Click 'Ok' to save your changes, 'Cancel' to cancel the changes made.");
				if (agree)
					{	return true; }
				else
					{ return false;}

	}
}

//*********************************************************