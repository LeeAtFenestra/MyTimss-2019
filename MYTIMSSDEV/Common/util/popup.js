
var popup;

function popUp( pageToLoad, winName, width, height, center) {

                                         
    xposition=0; yposition=0;
    if ((parseInt(navigator.appVersion) >= 4 ) && (center)){
        xposition = (screen.width - width) / 2;
        yposition = (screen.height - height) / 2;
    }
    args = "width=" + width + "," 
    + "height=" + height + "," 
    + "location=0," 
    + "menubar=0,"
    + "resizable=1,"
    + "scrollbars=1,"
    + "status=0," 
    + "titlebar=0,"
    + "toolbar=0,"
    + "hotkeys=0,"
    + "screenx=" + xposition + ","  //NN Only
    + "screeny=" + yposition + ","  //NN Only
    + "left=" + xposition + ","     //IE Only
    + "top=" + yposition;           //IE Only

	if (popup && !popup.closed)
	{	  
			popup.focus;
			popup.close();
			//alert("popup= window.open(" + pageToLoad + "," +winName +"," + args +")");
			setTimeout("popup= window.open('" + pageToLoad + "','" +winName +"','" + args +"')",100);

		}	
		else popup= window.open( pageToLoad,winName,args );
 
}


function openAWindowMenu( pageToLoad, winName, width, height, center) {

                                         
    xposition=0; yposition=0;
    if ((parseInt(navigator.appVersion) >= 4 ) && (center)){
        xposition = (screen.width - width) / 2;
        yposition = (screen.height - height) / 2;
    }
    args = "width=" + width + "," 
    + "height=" + height + "," 
    + "location=0," 
    + "menubar=1,"
    + "resizable=1,"
    + "scrollbars=1,"
    + "status=0," 
    + "titlebar=0,"
    + "toolbar=0,"
    + "hotkeys=0,"
    + "screenx=" + xposition + ","  //NN Only
    + "screeny=" + yposition + ","  //NN Only
    + "left=" + xposition + ","     //IE Only
    + "top=" + yposition;           //IE Only

	if (popup && !popup.closed)
	{	  
			popup.focus;
			popup.close();
			//alert("popup= window.open(" + pageToLoad + "," +winName +"," + args +")");
			setTimeout("popup= window.open('" + pageToLoad + "','" +winName +"','" + args +"')",100);

		}	
		else popup= window.open( pageToLoad,winName,args );
 
}



