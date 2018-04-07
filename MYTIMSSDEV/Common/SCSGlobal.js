function IsNumeric(sText)
{
   var ValidChars = "0123456789.";
   var IsNumber=true;
   var Char;


   for (i = 0; i < sText.length && IsNumber == true; i++)
      {
      Char = sText.charAt(i);
      if (ValidChars.indexOf(Char) == -1)
         {
         IsNumber = false;
         }
      }
   return IsNumber;

   }

// Removes leading whitespaces
function LTrim( value ) {

	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");

}

// Removes ending whitespaces
function RTrim( value ) {

	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");

}

// Removes leading and ending whitespaces
function trim( value ) {

	return LTrim(RTrim(value));

}

function echeck(str) {

		str = trim(str);

		if ( str == ''){
			return true;
		}

		var at="@"
		var dot="."
		var lat=str.indexOf(at)
		var lstr=str.length
		var ldot=str.indexOf(dot)
		if (str.indexOf(at)==-1){
		   return false;
		}

		if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr){
		   return false;
		}

		if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr){
		    return false;
		}

		 if (str.indexOf(at,(lat+1))!=-1){
		    return false;
		 }

		 if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot){
		    return false;
		 }

		 if (str.indexOf(dot,(lat+2))==-1){
		    return false;
		 }

		 if (str.indexOf(" ")!=-1){
		    return false;
		 }

 		 return true;
	}


function launchSendEmail(template, pk, to, referer, cc, bcc, aditionalparameters) {

    if (to.length == 0) {
        alert('Email is required!');
        return false;
    }

    if (echeck(to) == false) {
        alert('Email format is invalid');
        return false;
    }

    if (eval(cc)) {
        if (cc.length > 0) {
            if (echeck(cc) == false) {
                alert('CC Email format is invalid');
                return false;
            }
        }
    }

    if (eval(bcc)) {
        if (bcc.length > 0) {
            if (echeck(bcc) == false) {
                alert('BCC Email format is invalid');
                return false;
            }
        }
    }
    var url = 'SendEmail.aspx?t=' + encodeURIComponent(template) + '&to=' + encodeURIComponent(to);
   
    //if (eval(referer)) {
        if (referer.length > 0) {
            url = url + '&referer=' + encodeURIComponent(referer);
        }
    //}

    if (eval(cc)) {
        if (cc.length > 0) {
            url = url + '&cc=' + encodeURIComponent(cc);
        }
    }

    if (eval(bcc)) {
        if (bcc.length > 0) {
            url = url + '&bcc=' + encodeURIComponent(bcc);
        }
    }

    //alert(eval(pk));
    //if (eval(pk)) {
        if (pk.length > 0) {
            url = url + '&pk=' + encodeURIComponent(pk);
        }
    //}
    //if (eval(aditionalparameters)) {
        //alert(aditionalparameters);
        if (aditionalparameters.length > 0) {
            url = url + '' + aditionalparameters;
        }
    //}


    //var win = window.open(url, '_self');
    document.location = url;
    //win.focus();

}


    function datecheck(obj, fieldname) {

        var err = 0
        var psj = 0;
        a = obj.value

        if (a.length == 0) return true;

        if (a.length != 10) err = 1
        b = a.substring(0, 2)// month
        c = a.substring(2, 3)// '/'
        d = a.substring(3, 5)// day
        e = a.substring(5, 6)// '/'
        f = a.substring(6, 10)// year

        //basic error checking
        if (b < 1 || b > 12) err = 1
        if (c != '/') err = 1
        if (d < 1 || d > 31) err = 1
        if (e != '/') err = 1
        //if (f<2000 || f>2003) err = 1
        //if (f !=2007) err = 1

        //advanced error checking

        // months with 30 days
        if (b == 4 || b == 6 || b == 9 || b == 11) {
            if (d == 31) err = 1
        }

        // february, leap year
        if (b == 2) {
            // feb
            var g = parseInt(f / 4)
            if (isNaN(g)) {
                err = 1
            }

            if (d > 29) err = 1
            if (d == 29 && ((f / 4) != parseInt(f / 4))) err = 1
        }
        if (err == 1) {

            alert('Wrong ' + fieldname + ', Please Re-Enter it !  Date format should be (mm/dd/yyyy)');
            obj.value = "";
            obj.focus();


        }
        modified_data = "Yes";
    }

function MM_findObj(n, d) 
{ //v4.01
  var p,i,x;  if(!d) d=document; 
  if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; 
  for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); 
  return x;
}


function IsNumeric(sText)
{
    var ValidChars = "0123456789.";
    var Char;
    var intCount = 0;
    
    for (i = 0; i < sText.length; i++) 
    { 
        Char = sText.charAt(i); 
        if (Char == ".") intCount++;
        if (ValidChars.indexOf(Char) == -1) 
        {
            return false;
        }
    }
    if (intCount>1) return false;
    return true;   
}

function Trim(TRIM_VALUE)
{
    if(TRIM_VALUE.length < 1) return"";

    TRIM_VALUE = RTrim(TRIM_VALUE);
    TRIM_VALUE = LTrim(TRIM_VALUE);
    if(TRIM_VALUE=="") 
        return "";
    else
        return TRIM_VALUE;
} //End Function

function RTrim(VALUE)
{
    var w_space = String.fromCharCode(32);
    var v_length = VALUE.length;
    var strTemp = "";
    if(v_length < 0)    return"";
    var iTemp = v_length -1;

    while(iTemp > -1)
    {
        if(VALUE.charAt(iTemp) == w_space)
        {    }
        else
        {
            strTemp = VALUE.substring(0,iTemp +1);
            break;
        }
        iTemp = iTemp-1;
    } //End While
    return strTemp;

} //End Function

function LTrim(VALUE)
{
    var w_space = String.fromCharCode(32);
    if(v_length < 1)    return"";
    var v_length = VALUE.length;
    var strTemp = "";

    var iTemp = 0;
    while(iTemp < v_length)
    {
        if(VALUE.charAt(iTemp) == w_space)
        {}
        else
        {
            strTemp = VALUE.substring(iTemp,v_length);
            break;
        }
        iTemp = iTemp + 1;
    } //End While
    return strTemp;
} //End Function

function CCollection() /* --CCollection object-- */
{
     var count = 0;

     this.add = _add;
     this.remove = _remove;
     this.isEmpty = _isEmpty;
     this.count = _size;
     this.clear = _clear;
     this.clone = _clone;

     /* --adds a new item to the collection-- */
     function _add(key, newItem) 
     {
          if (newItem == null) return;
          this[key] = newItem;
          count++;
     }

     /* --removes the item at the specified index-- */
     function _remove(index) 
     {
          //if (index < 0 || index > this.length - 1) return;
          this[index] = null;
          /* --reindex collection-- */          
          count--;
     }

     function _isEmpty() { return count==0 }     /* --returns boolean if collection is/isn't empty-- */

     function _size() { return count}     /* --returns the size of the collection-- */

     function _clear() {
     /* --clears the collection-- */
          for (var i = 0; i < count; i++)
               this[i] = null;

          lsize = 0;
     }

     function _clone() {
     /* --returns a copy of the collection-- */
          var c = new CCollection();

          for (var i = 0; i < count; i++)
               c.add(this[i]);

          return c;
     }
}
function GetRadioValue(obj)
{
    for (var i=0; i<obj.length;i++)
    {
        if (obj[i].checked) return obj[i].value;        
    }
    return "";
}
function SetLimitToTextArea(obj, intLimit, CountDownDisplayClientID)
{
    //var strName = "";
	var intCharNum = 0;
    if (obj.value.length>intLimit) obj.value = obj.value.substr(0,intLimit);
    //strName = obj.name +="span";
    //alert(CountDownDisplayClientID);
    if (document.all(CountDownDisplayClientID) != null)
    { 
        intCharNum = intLimit - obj.value.length;
        document.all(CountDownDisplayClientID).innerHTML = "<font color='red'>"+ intCharNum +" characters left</font>";
    }
    
}
function SetLimitToTextAreaORG(obj, intLimit)
{
    var strName = "";
	var intCharNum = 0;
    if (obj.value.length>intLimit) obj.value = obj.value.substr(0,intLimit);
    strName = obj.name.replace("text","span");
    if (document.all(strName) != null)
    { 
        intCharNum = intLimit - obj.value.length;
        document.all(strName).innerHTML = "<font color='red'>"+ intCharNum +" characters left</font>";
    }
    
}

    // Keep user from entering more than maxLength characters
    function maxLength(field, maxChars) {
        return (field.value.length < maxChars);
    }
    // Keep user from entering more than maxLength pasted characters
    function maxLengthPaste(field, maxChars) {
        event.returnValue = false;
        if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
            return false;
        }
        event.returnValue = true;
    }

//This function validates date type field
function ValidateDateTime(obj)
{
    if (Trim(obj.value) == "") return true;
    var filter = /^([1-9]|0[1-9]|1[012])[- /]([1-9]|0[1-9]|[12][0-9]|3[01])[- /]((19|20)\d\d|\d{2})/;
    if (!filter.test(obj.value))
    {
        obj.focus();
        return false;
    }
    return true;
}
function SCSWait(msecs)
{
    var start = new Date().getTime();
    var cur = start;
    while(cur - start < msecs)
    {
        cur = new Date().getTime();
    }
}
//fix array indexof for IE 
Array.prototype.indexOf = function(obj, start) 
{
    for (var i = (start || 0), j = this.length; i < j; i++) 
    {
        if (this[i] === obj) { return i; }
    }
    return -1;
}
function GetControl(controlName) 
{
    var control = window.document.getElementById(controlName);
    if (control == null) 
    {
        control = window.document.all[controlName];
        if (control == null) 
        {
            throw controlName + ' is missing.';
        }
    }
    return control;
}

function SCS_GetXmlDoc() 
{
    var xmlDoc = null;
    try //Internet Explorer
    {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
    }
    catch(e)
    {
        try //Firefox, Mozilla, Opera, etc.
        {
            xmlDoc=document.implementation.createDocument("","",null);
        }
        catch(e) {alert("SCS_GetXmlDoc()\n" + e.description)}
    }
    finally
    {
	    return xmlDoc;
    }
}

navigator.Browser_Name = (function() {
//detect browser
    var N = navigator.appName, ua = navigator.userAgent, tem;
    var M = ua.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
    if (M && (tem = ua.match(/version\/([\.\d]+)/i)) != null) M[2] = tem[1];
    M = M ? [M[1], M[2]] : [N, navigator.appVersion, '-?'];
    return M;
})();