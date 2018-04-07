<%@ Page Title="Configure Users and Roles" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="ConfigureUser.aspx.vb" Inherits="admin_ConfigureUser" %>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
  <table style="width: 500px; height: 100%" border="0">
        <tr>
            <td style="width: 100%; vertical-align : top">
                <table border="0" id="tblMain" width="100%">
                        <tr>
                            <td colspan="3" style="width: 100%;" class="DefaultFont">
                                <asp:Label ID="lblChooseView" runat="server" Text="What would you like to do?"></asp:Label>
                                <asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <hr style="width: 100%" /></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%; height: 100%;" colspan="3">
                                <asp:MultiView ID="mvwMain" runat="server">
                                    <asp:View ID="CreateANewUser" runat="server">
                                        <table style="text-align:left; width:500px">
                                            <tr>
                                                <td colspan="3" align="left" class="webFormHeaderSmall" style="width: 100%">
                                                    <asp:Label ID="lblAddUser" runat="server" Text="Create a New User<br>* Indicates Required Field"></asp:Label>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="3" style="width: 100%" class="DefaultFont">
                                                    <asp:Table ID="tableAddUser" runat="server" Width="100%">
                                                        <asp:TableRow ID="rowUserName" runat="server">
                                                            <asp:TableCell ID="TableCell1" runat="server">
                                                                <asp:Label ID="lblNewUserName" runat="server" Text="User Name*"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="TableCell2" runat="server">
                                                                <asp:TextBox ID="txtUserName" runat="server" Width="149px"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="TableCell3" runat="server">
                                                                <asp:RequiredFieldValidator ID="rfvNewUserName" runat="server" ControlToValidate="txtUserName"
                                                                    ErrorMessage="*User Name Required" SetFocusOnError="True" Width="153px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow ID="rowPassword" runat="server">
                                                            <asp:TableCell ID="TableCell4" runat="server">
                                                                <asp:Label ID="lblPassword" runat="server" Text="Password*"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="TableCell5" runat="server">
                                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="149px"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="cellValStringPassword" runat="server">
                                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                                                    ErrorMessage="*Password Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revPassword" runat="server" ErrorMessage="*Must be between 4 and 12 characters and contain at least two of the following: uppercase letter, lowercase letter, numeral, special character" 
                                                                ValidationExpression="(?=^.{4,12}$)((?=.*\d)*(?=.*[a-z])*(?=.*[^A-Za-z0-9])(?=.*[A-Z])|(?=.*\d)(?=.*[a-z])*(?=.*[^A-Za-z0-9])*(?=.*[A-Z])|(?=.*\d)(?=.*[a-z])(?=.*[^A-Za-z0-9])*(?=.*[A-Z])*|(?=.*\d)*(?=.*[a-z])(?=.*[A-Z])*(?=.*[^A-Za-z0-9])|(?=.*\d)*(?=.*[a-z])(?=.*[^A-Za-z0-9])*(?=.*[A-Z])|(?=.*\d)(?=.*[a-z])*(?=.*[^A-Za-z0-9])(?=.*[A-Z])*)^.*$" 
                                                                ControlToValidate="txtPassword" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </asp:TableCell>
                                                          </asp:TableRow>
                                                          <asp:TableRow ID="rowPasswordCheck" runat="server">                                                                                                                      
                                                            <asp:TableCell ID="TableCell6"  runat="server">
                                                                <asp:Label ID="lblPasswordCheck" runat="server" Text="Re-Enter Password*"></asp:Label>
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="TableCell7" runat="server">
                                                                <asp:TextBox ID="txtPasswordCheck" runat="server" TextMode="Password" Width="149px"></asp:TextBox>
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="TableCell8" runat="server">
                                                                <asp:RequiredFieldValidator ID="valPasswordCheckRequired" runat="server" ControlToValidate="txtPasswordCheck"
                                                                    ErrorMessage="*Required" Display="Dynamic"></asp:RequiredFieldValidator>                                                            
                                                                <asp:CompareValidator ID="valPasswordCheckCompare" ControlToCompare="txtPassword" 
                                                                ControlToValidate="txtPasswordCheck" 
                                                                runat="server" Text="*Passwords must match" Display="Dynamic"
                                                                />
                                                            </asp:TableCell></asp:TableRow><asp:TableRow ID="rowEmailAddress" runat="server">
                                                            <asp:TableCell ID="TableCell9" runat="server">
                                                                <asp:Label ID="lblEmail" runat="server" Text="Email Address*"></asp:Label>
                                                            </asp:TableCell><asp:TableCell ID="TableCell10" runat="server">
                                                                <asp:TextBox ID="txtEmail" runat="server" Width="149px"></asp:TextBox>
                                                            </asp:TableCell><asp:TableCell ID="TableCell11" runat="server"><asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                                Display="Dynamic" ErrorMessage="*Email Address required "></asp:RequiredFieldValidator>
                                                                   <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic"
                                                                        ErrorMessage="*Email must be 'AAA...@AAA.AA(A)'" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                                            </asp:TableCell></asp:TableRow><asp:TableRow ID="rowPasswordQuestion" runat="server">
                                                            <asp:TableCell ID="TableCell12" runat="server">
                                                                <asp:Label ID="lblPassQues" runat="server" Text="Password Question*"></asp:Label>
                                                            </asp:TableCell><asp:TableCell ID="TableCell13" runat="server">
                                                                <asp:TextBox ID="txtPassQues" runat="server" Width="149px"></asp:TextBox>
                                                            </asp:TableCell><asp:TableCell ID="TableCell14" runat="server">
                                                                <asp:RequiredFieldValidator ID="rfvPassQues" runat="server" ControlToValidate="txtPassQues"
                                                                    ErrorMessage="*Question Required" Display="Dynamic"></asp:RequiredFieldValidator> 
                                                            </asp:TableCell></asp:TableRow><asp:TableRow ID="rowPasswordAnswer" runat="server">
                                                            <asp:TableCell ID="TableCell15" runat="server">
                                                                <asp:Label ID="lblPassAns" runat="server" Text="Password Answer*"></asp:Label></asp:TableCell><asp:TableCell ID="TableCell16" runat="server">
                                                                <asp:TextBox ID="txtPassAns" runat="server" Width="149px"></asp:TextBox></asp:TableCell><asp:TableCell ID="TableCell17" runat="server">
                                                                <asp:RequiredFieldValidator ID="valPassAns" runat="server" ControlToValidate="txtPassAns"
                                                                    ErrorMessage="*Answer Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </asp:TableCell></asp:TableRow></asp:Table></td></tr><tr>
                                                <td align="left" colspan="3" style="width: 100%">
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 100%" colspan ="3" align="center" valign="middle">
                                                    <table border="0", style="width:100%; height:100%">
                                                        <tr>    
                                                            <td style="width:100%">
                                                                &nbsp;<asp:Button ID="btnAddUser" runat="server" CssClass="button3" Text="Create User" />
                                                                <asp:XmlDataSource ID="dsxmlState" runat="server" DataFile="~/App_Data/states.xml">
                                                                </asp:XmlDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>    
                                                            <td style="width:100%">
                                                                <asp:Label ID="lblNewUserResult" runat="server" Width="100%"></asp:Label></td></tr></table></td></tr></table></asp:View><asp:View ID="UpdateUserRoles" runat="server">
                                        <table border="0" id="AddUserToRole" width="100%" class="DefaultFont">
                                            <tr>
                                                <td align="left" style="width: 100%; height: 15px;" colspan="3" class="webFormHeaderSmall">
                                                    <asp:Label ID="lblUserRole" runat="server" Text="Add or Remove a User from a Role" Width="226px" Font-Bold="True"></asp:Label></td></tr><tr>
                                                <td align="left" colspan="3" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; height: 22px;" align="right"> 
                                                    <asp:Label ID="lblUserName" runat="server" Text="Select a User Name" Height="6px" Width="127px"></asp:Label></td><td style="width: 162px; height: 22px; padding-left: 20px;" align="left">
                                                    <asp:DropDownList ID="ddlUser" runat="server" Width="149px" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                 <td style="width: 129px" align="left">
                                                    <asp:RequiredFieldValidator ID="valChangeRoleUserName" Display="dynamic" runat="server" ControlToValidate="ddlUser"
                                                    ErrorMessage="Please select a User" InitialValue="Select a User" SetFocusOnError="True" Width="128px" ToolTip="User Name Required"></asp:RequiredFieldValidator></td></tr><tr>
                                                <td align="right" style="height: 22px" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; height: 97px;" align="right" valign="top">
                                                    <asp:Label ID="lblRole" runat="server" Text="Check a Role to add the User to the Role<br><br>Remove checks to Remove the User from a Role" Width="172px"></asp:Label></td><td style="width: 75%; height: 97px; padding-left: 20px;" align="left" colspan="2" valign="top">
                                                    <asp:CheckBoxList ID="ckltRemoveUsersRoles" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
                                                    </asp:CheckBoxList></td>
                     
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan ="3" align="center" valign="middle">
                                                    <table border="0", style="width:100%; height:100%">
                                                        <tr>    
                                                            <td style="width:100%">
                                                                <asp:Button ID="btnUpdateRoles" runat="server" Text="Update User Roles" cssClass="button3" Width="154px"/>                   
                                                            </td>
                                                        </tr>
                                                        <tr>    
                                                            <td style="width:100%">
                                                                <asp:Label ID="lblAddUserToRoleResult" runat="server" Width="100%"></asp:Label></td></tr></table></td></tr></table></asp:View><asp:View ID="RemoveAUser" runat="server">
                                        <table id="tblRemoveUser" width="100%" class="DefaultFont">
                                            <tr>
                                                <td colspan="2" align="left" class="webFormHeaderSmall">
                                                    <asp:Label ID="lblDeleteUsers" runat="server" Text="Delete Users"></asp:Label></td></tr><tr>
                                                <td align="right" colspan="2" style="height: 10px" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 35%; height: 40px;" valign="top" align="right">
                                                     <asp:Label ID="lblDelete" runat="server" Text="Select Users to Delete"></asp:Label></td><td align="left" style="width: 65%; padding-left: 20px;" valign="top">
                                                    <asp:CheckBoxList ID="ckltDeleteUsers" runat="server" RepeatColumns="5">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnRemoveUser" runat="server" Text="Remove Selected Users" cssClass="button3" Width="191px"/></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; height: 15px;" colspan="2" align="center" valign="middle">
                                                     <asp:Label ID="lblDeleteResult" runat="server" Width="100%"></asp:Label></td></tr></table></asp:View><asp:View ID="UpdateUser" runat="server">
                                        <table id="tblUpdateUserDetails" width="100%" class="DefaultFont">
                                            <tr>
                                                <td colspan="2" align="left" style="height: 18px" class="webFormHeaderSmall">
                                                    <asp:Label ID="lblUpdateUserDetails" runat="server" Text="Update User"
                                                       ></asp:Label></td></tr><tr>
                                                <td colspan="2" style="height: 10px" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%" valign="top">
                                                    <asp:Label ID="lblUserUpdateUserName" runat="server" Text="Select a User to Update"
                                                        Width="139px" Height="23px"></asp:Label></td><td align="left" style="padding-left: 20px; width: 80%; height: 10px" valign="top">
                                                <asp:DropDownList ID="ddlUserUpdateUserName" runat="server" Width="149px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="valUpdateUserUserName" runat="server" ErrorMessage="Please Select a User"
                                                        SetFocusOnError="True" ToolTip="User Required" ControlToValidate="ddlUserUpdateUserName" InitialValue="Select a User" Height="16px" Width="231px"></asp:RequiredFieldValidator></td></tr><tr>
                                                <td align="left" colspan="2" style="padding-left: 20px; height: 10px" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%" valign="top" align="right">
                                                    <asp:Label ID="lblUpdateUserEmail" runat="server" Text="Email Address" Width="86px" Height="2px"></asp:Label></td><td align="left" style="width: 80%; height: 26px; padding-left: 20px;" valign="top">
                                                    <asp:TextBox ID="txtUpdateUserEmail" runat="server" Width="149px"></asp:TextBox><asp:RegularExpressionValidator ID="valUpdateEmailAddress" runat="server" ControlToValidate="txtUpdateUserEmail"
                                                        ErrorMessage="Email Must be a Valid" ToolTip="Email must be valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="valRequireEmailAddress" runat="server" ControlToValidate="txtUpdateUserEmail"
                                                        ErrorMessage="Email is Required" ToolTip="Enter Email Address"></asp:RequiredFieldValidator></td></tr><tr>
                                                <td style="width: 25%; height: 15px;" valign="top">
                                                    <asp:Label ID="lblResetPassword" runat="server" Height="2px" Text="Check Here to<br>Reset User Password"
                                                        Width="86px" Visible="False"></asp:Label></td><td align="left" style="padding-left: 20px; width: 80%; height: 15px" valign="top">
                                                    &nbsp;<asp:CheckBox ID="chkResetPassword" runat="server" Text="Reset User's Password" Visible="False"/></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" style="height: 19px">
                                                    <asp:Button ID="btnUpdateEmailOrPassword" runat="server" CssClass="button3" Text="Submit" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; height: 15px;" colspan="2" align="center" valign="middle">
                                                    <asp:Label ID="lblUpdateUserDetailsResult" runat="server" Width="100%"></asp:Label></td></tr></table></asp:View><asp:View ID="CreateANewRole" runat="server" EnableTheming="False"> 
                                        <table id="tblAddNewRole" width="100%" class="DefaultFont">
                                            <tr>
                                                <td colspan="2" align="left" class="webFormHeaderSmall">
                                                    <asp:Label ID="lblAddNewRole" runat="server" Text="Create A New Role"></asp:Label></td></tr><tr>
                                                <td colspan="2" style="height: 10px" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%; height: 26px;" valign="top">
                                                     <asp:Label ID="lblNewRoleName" runat="server" Width="100%" Text="Role Name"></asp:Label></td><td align="left" style="width: 82%; height: 26px; padding-left: 20px;">
                                                    &nbsp;<asp:TextBox ID="txtNewRoleName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="valAddNewRole" runat="server" ControlToValidate="txtNewRoleName"
                                                        ErrorMessage="Please enter a Role Name" SetFocusOnError="True" ToolTip="Role Name Required"></asp:RequiredFieldValidator></td></tr><tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnNewRole" runat="server" Text="Create Role" cssClass="button3"/>&nbsp; </td></tr><tr>
                                                <td style="width: 100%; height: 15px;" colspan="2" align="center" valign="middle">
                                                    <asp:Label ID="lblNewRoleResult" runat="server" Width="100%"></asp:Label></td></tr></table></asp:View><asp:View ID="RemoveARole" runat="server">
                                        <table id="Table1" width="100%" class="DefaultFont">
                                            <tr>
                                                <td colspan="2" align="left" class="webFormHeaderSmall">
                                                    <asp:Label ID="lblRemoveRole" runat="server" Text="Remove Existing Role"
                                                        Width="35%"></asp:Label></td></tr><tr>
                                                <td colspan="2" style="height: 10px" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%" valign="top">
                                                    <asp:Label ID="lblRemoveRoleName" runat="server" Text="Check all<br>Role Names<br>to Remove"
                                                        Width="73%"></asp:Label></td><td align="left" style="width: 75%; height: 26px; padding-left: 20px;" valign="top">
                                                    <asp:CheckBoxList ID="ckltRemoveRoles" runat="server" RepeatColumns="5">
                                                    </asp:CheckBoxList></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnRemoveRoles" runat="server" Text="Remove Selected Roles" CssClass="button3" Width="192px" />&nbsp; </td></tr><tr>
                                                <td style="width: 100%; height: 15px;" colspan="2" align="center" valign="middle">
                                                     <asp:Label ID="lblRemoveRoleResult" runat="server" Width="100%"></asp:Label></td></tr></table></asp:View><asp:View ID="ViewAllUsers" runat="server">
                                        <table id="Table2" width="100%" class="DefaultFont">
                                            <tr>
                                                <td colspan="2" align="left" class="webFormHeaderSmall">
                                                    <asp:Label ID="lblAllUsers" runat="server" Text="All Existing Users and Their Roles"
                                                        Width="58%"></asp:Label></td></tr><tr>
                                                <td align="center" colspan="2" style="width: 300px; height: 10px" valign="top">
                                                    <br />
                                                    <asp:Label ID="lblSearch" runat="server" Text="Username starts with:"></asp:Label><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp; <br /></td></tr><tr>
                                                <td align="center" colspan="2" style="height: 10px" valign="top">
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="button3" Text="Search" />
                                                    <asp:Button ID="btnRemoveFilter" runat="server" CssClass="button3" Text="Remove Filter" />
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="width: 100%" valign="top">
                                                    <asp:LinkButton ID="lbtnRefresh" runat="server">This page is cached, click here to refresh data</asp:LinkButton></td></tr><tr>
                                                <td align="center" colspan="2" style="width: 100%" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" valign="top">
                                                    <asp:GridView ID="gvwUser" runat="server" CellPadding="10" GridLines="None" AllowPaging="True" Width="429px" AutoGenerateEditButton="True" AllowSorting="True" EmptyDataText="Sorry, no users found.">
                                                        
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                </table>
           </td>
        </tr>

       </table> 
</td></tr></table>
</asp:Content>

