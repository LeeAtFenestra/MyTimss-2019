// ..............................................................this creates a hoverbutton object

//..constructor
function hoverbutton(ctrlname,stdimg,hoverimg,clickimg,hoverEnabled)
{
	// data members .....
	this.name = ctrlname;
	this.state = 0;							// 0 = stdimg, 1= hoverimg, -1 = clickimg
	this.enabled = hoverEnabled;			 //..state [true|false]
	this.stdimg = hover_load(stdimg);
	this.hoverimg = hover_load(hoverimg);
	this.clickimg = hover_load(clickimg);
	
	// function's ...
	this.toggle = hover_toggle;
	this.print = hover_print;
	this.click = hover_click;
	this.enable = hover_enable;
	
}

//...method
function hover_enable()
{
	//...RESET IMAGE AFTER A CLICK
	if (Canhover())
	{
		this.enabled = true;
		this.state = 0;
		document[this.name].src = this.stdimg.src;
	}

}


function Canhover()
{
        if (((navigator.appName == "Netscape") && (parseInt(navigator.appVersion) >= 3 ))  || ((navigator.appName == "Microsoft Internet Explorer") && (parseInt(navigator.appVersion) >= 4 )))
                 return true;    
        else
                return false;
                
}



//.... method
	function hover_toggle()
	{
	
		if (this.enabled)
		{
			if (Canhover())
			{
				if( this.state == 1)
				{
					  this.state = 0;
					  document[this.name].src = this.stdimg.src;
				 }
				 else
				 {
				 	this.state = 1;
					document[this.name].src = this.hoverimg.src;
				}
			}
		}
}	
//.... method
function hover_click()
{
		if (Canhover())
		{
			if( this.enabled)
			{
				  this.enabled = false;
				  document[this.name].src = this.clickimg.src;
			 }
		}

}	
// used in constructor to preload the image into memory

function hover_load(img)
{
  if(Canhover())
  {
	var a=new Image();
	a.src=img;
	return a;
	}
}



// ... dumps values to the screen for debugging
function hover_print()
{
	var newline = "<br>";
	document.write("<strong> Name: </strong>" + this.name + newline);
	document.write("<strong> State: </strong>" + this.state + newline);
	document.write("<strong> Standard Image: </strong>" + this.stdimg.src + newline);
	document.write("<strong> Hover Image: </strong>" + this.hoverimg.src + newline);
	document.write("<strong> Click Image: </strong>" + this.clickimg.src + newline);
}
