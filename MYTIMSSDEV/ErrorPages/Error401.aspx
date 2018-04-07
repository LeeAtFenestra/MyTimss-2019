<%@ Page Language="VB"  CodeFile="Error401.aspx.vb" Inherits="Error401" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Resource not found</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table>
        <tr>
            <td>
                <img src="../Common/images/error.png" />
            </td>
            <td>
                <h2>We're sorry, there is no Web page matching your request. </h2>
                <p>
                It's possible you typed the address incorrectly, or that the page no longer exists. 
                In this case, we profusely apologize for the <strong>inconvenience</strong> and for any damage
                this may cause.               
                </p>
                <hr />
                <b>Error code:&nbsp;</b>401<br />
                <b>Error path:&nbsp;</b><span runat="server" id="ErrorPathSPan"></span>
                <hr />
                As an option, we've provided a list of pages below that might have the information you're 
                looking for.
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
