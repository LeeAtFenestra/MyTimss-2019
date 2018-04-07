<%@ Page Title="Login Activity" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="LoginActivity.aspx.vb" Inherits="admin_LoginActivity" %>

<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" style="height: 96px">
                <center>
                    <table border="0" cellpadding="5" cellspacing="0" class="sectiontitle" >
                        <tr>
                            <td align="left" class="webFormHeaderSmall" colspan="2" valign="middle">
                                View Login Activities
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" colspan="2" class="webFormSimpleItem">
                                <table cellpadding="6">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbViewAll" runat="server" GroupName="FilterOptions" 
                                                Text="View All" AutoPostBack="true"  CssClass="bdyText"
                                                />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbFilterByLoginStatus" runat="server" GroupName="FilterOptions" 
                                                Text="Filter By LoginStatus" AutoPostBack="true" CssClass="bdyText"
                                                />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbAdvFilter" runat="server" GroupName="FilterOptions" 
                                                Text="Advanced Filter" AutoPostBack="true" CssClass="bdyText"
                                                 /> 
                                        </td>
                                    </tr>
                                </table>                       
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="webFormSubHeader" colspan="2" valign="middle">
                                <asp:MultiView runat="server" id="mvwLoginSearch" ActiveViewIndex="0" Visible="False" >
	                                <asp:View runat="server" id="vFilterByLoginStatus">
                                        <asp:Label ID="lblLoginStatuse" runat="server">LoginStatus:</asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;
                                        <asp:DropDownList ID="ddlLoginStatus" runat="server" Width="157px" AutoPostBack="True" CausesValidation="True">
                                            <asp:ListItem>Select a Login Status</asp:ListItem>
                                            <asp:ListItem>Succeeded</asp:ListItem>
                                            <asp:ListItem>Failed</asp:ListItem>
                                        </asp:DropDownList> &nbsp;	&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:CompareValidator ID="cvLoginStatus" runat="server" ControlToValidate="ddlLoginStatus"
                                            Display="Dynamic" ErrorMessage="Enter a valid login status." Operator="NotEqual"
                                            Type="String" ValueToCompare="Select a Login Status" SetFocusOnError="True">
                                            *Not a valid login status
                                        </asp:CompareValidator>
                                    </asp:View>
                                    <asp:View runat="server" id="vAdvancedFilter">
		                                <table  cellspacing="0" cellpadding="5" border="0" class="webFormSimple" width="100%">
                                            <tr >
                                                <td valign="middle" align="right" class="webFormLabel">
                                                    LoginStatus:</td>
                                                <td valign="top" align="left" class="webFormSimpleItem">
                                                    <asp:DropDownList ID="ddlLoginStatus1" runat="server" Width="157px" CausesValidation="True" ValidationGroup="AdvSearch">
                                                        <asp:ListItem>Select a Login Status</asp:ListItem>
                                                        <asp:ListItem>Succeeded</asp:ListItem>
                                                        <asp:ListItem>Failed</asp:ListItem>
                                                    </asp:DropDownList>&nbsp;	
                                                    <asp:CompareValidator ID="cvLoginStatus1" runat="server" ControlToValidate="ddlLoginStatus1"
                                                        Display="Dynamic" ErrorMessage="Enter a valid login status." Operator="NotEqual"
                                                        Type="String" ValueToCompare="Select a Login Status" SetFocusOnError="True" ValidationGroup="AdvSearch">
                                                        *Not a valid login status
                                                    </asp:CompareValidator>
                                                </td>
                                            </tr>		                                
                                            <tr >
                                                <td valign="middle" align="right" class="webFormLabel">
                                                    Starting Date:</td>
                                                <td valign="top" align="left" class="webFormSimpleItem">
                                                    <asp:TextBox ID="txtBeginDate" runat="server" CssClass="textbox" ValidationGroup="AdvSearch" CausesValidation="True" ></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/images/calendar.gif" />
                                                    <asp:RegularExpressionValidator ID="valBeginDate" runat="server" ControlToValidate="txtBeginDate"
                                                        ErrorMessage="mm/dd/yyyy" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"
                                                        ValidationGroup="AdvSearch"></asp:RegularExpressionValidator>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="valReqStartDate" runat="server" ErrorMessage="Starting date is required." ControlToValidate="txtBeginDate" ValidationGroup="AdvSearch" ></asp:RequiredFieldValidator>
                                                    &nbsp;
                                                    <asp:Calendar ID="calStartDate" runat="server" Visible="False" ShowGridLines="True" VisibleDate="2006-09-21"></asp:Calendar>
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td valign="middle" align="right" class="webFormLabel" style="height: 46px">
                                                    Ending Date:</td>
                                                <td valign="top" align="left" class="webFormSimpleItem" style="height: 46px">
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="textbox" ValidationGroup="AdvSearch" CausesValidation="True" ></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnEndDate" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/images/calendar.gif" />
                                                    <asp:RegularExpressionValidator ID="valEndDate" runat="server" ControlToValidate="txtEndDate"
                                                        ErrorMessage="mm/dd/yyyy" ValidationGroup="AdvSearch" ValidationExpression="\d{1,2}/\d{1,2}/\d{4}"></asp:RegularExpressionValidator><br />
                                                    <asp:RequiredFieldValidator ID="valReqEndDate" runat="server" ControlToValidate="txtEndDate"
                                                        ErrorMessage="Ending date is required." ValidationGroup="AdvSearch"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <asp:CompareValidator ID="cvDatesCompare" runat="server" ControlToCompare="txtEndDate"
                                                        ControlToValidate="txtBeginDate" ErrorMessage="Start date couldn't exceed the ending date."
                                                        Operator="LessThanEqual" Type="Date" ValidationGroup="AdvSearch"></asp:CompareValidator>
                                                    <asp:Calendar ID="calEndDate" runat="server" Visible="False" ShowGridLines="True" VisibleDate="2006-09-21"></asp:Calendar>
                                                </td>
                                            </tr>
			                                <tr>
				                                <td valign="middle" align="right" class="webFormLabel" style="height: 27px">&nbsp;</td>
				                                <td valign="top" align="left" class="webFormSimpleItem" style="height: 27px">
                                                    <asp:Button ID="btnSearch2" runat="server" Text="Search" cssClass="button3" ValidationGroup="AdvSearch" />
                                                </td>
			                                </tr>
		                                </table>
                                    </asp:View>                        
                                </asp:MultiView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="webFormSubHeader" colspan="1" valign="top">
                                <asp:SqlDataSource ID="dsSqlLoginLookup" runat="server" ConnectionString="<%$ ConnectionStrings:FAB_DefaultConnectionString %>"
                                    SelectCommand="SELECT [LogID], [EventID], [Title], [Timestamp], [Message] FROM [Log] WHERE ([Title] LIKE '%' + @Title + '%')">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="LoginActivity" Name="Title" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>                                
                                <asp:GridView ID="gvwLoginLookup" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="dsSqlLoginLookup" PageSize="8" Visible="False"  >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" >
                                            <ControlStyle Font-Bold="True" Font-Underline="True" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="LogID" HeaderText="LogID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="LogID" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EventID" HeaderText="EventID" SortExpression="EventID" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" SortExpression="Timestamp" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="InactiveCaptionText" BorderStyle="Inset" />
                                </asp:GridView>                                                            
                            </td>
                            <td align="left" class="webFormSubHeader" colspan="1" valign="top">
                                <asp:XmlDataSource ID="dsXmlLoginInfo" runat="server" XPath="LoginNode" EnableCaching="False">
                                </asp:XmlDataSource>
                                <asp:DetailsView ID="dvwLoginActivityDetail" runat="server" 
                                    AutoGenerateRows="False" DataSourceID="dsXmlLoginInfo"
                                    Height="50px" Width="125px" BackColor="White" HeaderText="User Details View" Visible="False"  >
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                    <Fields>
                                        <asp:TemplateField HeaderText="LogID:" >
                                        <ItemTemplate><%#gvwLoginLookup.Rows(gvwLoginLookup.SelectedIndex).Cells(1).Text.ToString%></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UserName:" >
                                        <ItemTemplate><%#XPath("UserName")%></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LoginStatus:">
                                        <ItemTemplate><%#XPath("LoginStatus")%></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IPAddress:">
                                        <ItemTemplate><%#XPath("IPAddress")%></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BrowserType:">
                                        <ItemTemplate><%#XPath("BrowserType")%></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>                    
                                        <asp:CommandField ButtonType="Button" SelectText="Cancel" ShowSelectButton="True" >
                                            <ItemStyle Font-Names="Verdana" ForeColor="#585880" />
                                            <ControlStyle BorderColor="#669CB7" BorderStyle="None" Font-Bold="False" Font-Names="Verdana"
                                                Font-Size="Small" ForeColor="White" BackColor="#669CB7" />
                                        </asp:CommandField>
                                    </Fields>
                                    <CommandRowStyle BackColor="#E0E0E0" />
                                </asp:DetailsView>
                            </td>
                        </tr>
                    </table>
                </center>
                <br />
            </td>
        </tr>
    </table>
</td></tr></table>
</asp:Content>

