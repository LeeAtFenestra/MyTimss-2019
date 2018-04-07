<%@ Page Title="Health Monitoring Log Administration" Language="VB" MasterPageFile="~/admin/Admin.master" AutoEventWireup="false" CodeFile="LogReports.aspx.vb" Inherits="admin_LogReports" %>

<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">  <tr>   <td bgcolor="#FFFFFF">      
    <table style="margin-top:20px; margin:20px" width="700px" align="center">
         <tr>
            <td align="center" colspan="2" rowspan="1" valign="top" class="webFormHeaderSmall" width="700" >
         
               FAB Health Monitoring Log Administration</td>
        </tr>
          <tr style="background-color: #ffffff">
              <td align="center" colspan="2" rowspan="1" valign="top" width="700" style="text-align: center" class="DefaultFont">
                  <strong>
                      <br />
                  Select Administrative Area to View:</strong>&nbsp;<asp:DropDownList ID="ddlViews" runat="server" AutoPostBack="True" Width="187px">
                      <asp:ListItem Value="Select One"></asp:ListItem>
                      <asp:ListItem Value="View SQL Logs"></asp:ListItem>
                      <asp:ListItem Value="View Text Logs">View Text Logs</asp:ListItem>
                      <asp:ListItem Value="Administer Logs"></asp:ListItem>
                      
                  </asp:DropDownList>
                  <br />
                </td>
          </tr>
        <tr style="background-color: #ffffff">
           <td align="left" colspan="3" rowspan="1" valign="top" class="DefaultFont">
            <table width="700px" align="center">
                <tr>
                    <td align="center"> 
                        <asp:MultiView ID="mvViewLogs" runat="server">
                            <asp:View ID="vwButtons" runat="server"><hr />
                               <asp:Button ID="btnViewAll" runat="server" CssClass="button3" Text="View All" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnFilter" runat="server" CssClass="button3" Text="Apply Filter" ValidationGroup="vgFilter" Enabled="False" TabIndex="1" ToolTip="Apply defined filter and view results" />
                            
                <table width="700px" align="center">
                 <tr>
            <td align="left" colspan="2" rowspan="1" valign="top">
            <hr />
                Click View All Or define the filter criteria (one to many) and click Apply Filter to view log
                records</td>          
        </tr>
        <tr>
            <td align="left" colspan="1" style="WIDTH: 150px" valign="top">
                <strong>Event Type:</strong><strong>
                <asp:SqlDataSource ID="cnFilterEventType" runat="server" ConnectionString="<%$ ConnectionStrings:FAB_DefaultConnectionString %>"
                    SelectCommand="FAB_FilterWebEventType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </strong>
            </td>
            <td align="left" colspan="1" valign="top" style="width:200px">                                                 <asp:DropDownList ID="ddlEventType" runat="server"
                    DataTextField="EventType" DataValueField="EventType" Width="320px" TabIndex="2" DataSourceID="cnFilterEventType">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" colspan="1" style="width: 150px" valign="top">
                <strong>Event Detail:
                <asp:SqlDataSource ID="cnEventCodeDetails" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:FAB_DefaultConnectionString %>" 
                    SelectCommand="FAB_FilterWebEventDetails" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="EventCode" Type="Int32" />
                        <asp:Parameter Name="EventDetailCode" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </strong>
            </td>
            <td align="left" colspan="1" style="width: 200px" valign="top">
                <asp:DropDownList ID="ddlEventCode" runat="server" 
                    DataSourceID="cnEventCodeDetails" DataTextField="WebEventName" 
                    DataValueField="Code" TabIndex="2" Width="320px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="1" rowspan="1" style="WIDTH: 150px" valign="top">
                <strong>Request URL:<asp:SqlDataSource ID="cnFilterRequestURL" runat="server"
                    ConnectionString="<%$ ConnectionStrings:FAB_DefaultConnectionString %>" 
                    SelectCommand="FAB_FilterRequestURLWebEvent" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                </strong></td>
            <td style="width: 200px" valign="top" align="left">
                <asp:DropDownList ID="ddlRequestURL" runat="server"
                    DataTextField="RequestURL" DataValueField="RequestURL" Width="320px" TabIndex="3" DataSourceID="cnFilterRequestURL"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" colspan="1" rowspan="1" style="WIDTH: 150px;" valign="top">
                <strong>Start
                    Date:<br />
                </strong>
                    Select only the start date to view just one day.</td>
            <td valign="top" align="left">
                <asp:TextBox ID="txtStartDate" runat="server" Width="220px" TabIndex="5"></asp:TextBox><asp:ImageButton ID="ibtnSearchStart" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/images/calendar.gif" TabIndex="4" />
                <asp:Panel ID="pnlStartDate" runat="server" CssClass="popupControl">
                       <asp:Calendar ID="calStart" runat="server" BackColor="White" BorderColor="#3366CC" Font-Names="Verdana"
                         Font-Size="8pt" ForeColor="#003399" Width="220px" Height="200px" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Visible="False">
                            <SelectedDayStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" Font-Bold="True" BorderWidth="1px" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    </asp:Calendar>
        
    </asp:Panel>                                
            </td>
        </tr>
        <tr>
            <td align="left" colspan="1" rowspan="1" style="width: 150px" valign="top">
                <strong>End Date:</strong></td>
            <td valign="top" align="left">
                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="7" Width="220px"></asp:TextBox><asp:ImageButton ID="ibtnSearchEnd" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/images/calendar.gif" TabIndex="6" />
                <asp:Panel ID="pnlEndDate" runat="server" CssClass="popupControl">
                  
                        <asp:Calendar ID="calEnd" runat="server" BackColor="White" BorderColor="#3366CC" Font-Names="Verdana" 
                        Font-Size="8pt" ForeColor="#003399" Width="220px" Height="200px" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Visible="False">
                            <SelectedDayStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" Font-Bold="True" BorderWidth="1px" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        </asp:Calendar>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" rowspan="1" valign="top">
                    <hr />
                </td>
        </tr>
        <tr>
            <td align="left" colspan="2" rowspan="1" valign="top">
                <asp:DetailsView ID="dvMessageDetails" runat="server" AutoGenerateRows="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None"
                    Height="50px" Width="810px" DataSourceID="cnDetails" DataKeyNames="EventID">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <RowStyle BackColor="#E3EAEB" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                        <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details" />
                    </Fields>
                    <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:DetailsView>
                <asp:SqlDataSource ID="cnDetails" runat="server" ConnectionString="<%$ ConnectionStrings:FAB_DefaultConnectionString %>"
                    SelectCommand="FAB_FilterDetailsWebEvent" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="gvwLog" Name="EventID" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="lblRecordCount" visible="false" runat="server" Text="Label"></asp:Label>
                <asp:GridView ID="gvwLog" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                    GridLines="None" 
                    EmptyDataText="Sorry, No Records Found." OnSelectedIndexChanged="gvwLog_SelectedIndexChanged" DataSourceID="cnAll" DataKeyNames="EventID" Width="700px" >
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="EventTime" HeaderText="Event Time" />
                        <asp:BoundField DataField="EventType" HeaderText="Event Type" />
                        <asp:BoundField DataField="EventCode" HeaderText="Event Code" />
                        <asp:BoundField DataField="EventDetailCode" HeaderText="Event Detail Code" />
                        <asp:BoundField DataField="ApplicationPath" HeaderText="Application Path" />
                        <asp:BoundField DataField="ApplicationVirtualPath" HeaderText="App Virtual Path" />
                        <asp:BoundField DataField="MachineName" HeaderText="Machine Name" />
                        <asp:BoundField DataField="RequestURL" HeaderText="Request URL" />
                        <asp:BoundField DataField="EventID" HeaderText="Event ID" Visible="False" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
                <asp:SqlDataSource ID="cnAll" runat="server" 
                    SelectCommand="FAB_FilterLog" SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:FAB_DefaultConnectionString %>">
                    <SelectParameters>
                        <asp:Parameter Name="EventType" Type="String" DefaultValue="All" />
                        <asp:Parameter Name="RequestURL" Type="String" DefaultValue="All" />
                        <asp:Parameter Name="StartDate" Type="DateTime" DefaultValue="1/1/1900" />
                        <asp:Parameter Name="EndDate" Type="DateTime" DefaultValue="1/1/1900" />
                        <asp:Parameter DefaultValue="0" Name="EventCode" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="EventDetailCode" Type="Int32" />
                        <asp:Parameter Direction="Output" Name="RecordCount" Type="Int32" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>                
            </td>

        </tr>
       </table>
     </asp:View>
                            <asp:View ID="View2" runat="server">
                            <table width="700px" align="center" class="DefaultFont">
                                <tr>
                                    <td style="width: 650px">
                                        <hr />
                                        <br />
                                            <asp:Button ID="btnDeleteSQLLog" runat="server" CssClass="button3" Text="Delete SQL Logs"
                                                Width="122px" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnDeleteTextLog" runat="server" CssClass="button3" Text="Delete Text Logs"
                                                Width="127px" />&nbsp;
                                        <br />
                                        <br />
                                        <asp:Label ID="lblAnswer" runat="server"></asp:Label>
                                                <hr />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                                <td style="width: 650px"><table width="700px" align="center">
                                                    <tr>
                                                        <td align="left" colspan="1" rowspan="1" style="width: 150px" valign="top">
                                                            <strong>Number of asdfasdf
                                                                <br />
                                                                days to keep</strong></td>
                                                        <td align="left" valign="top">
                                                            <asp:DropDownList ID="ddlDaysToKeep" runat="server">
                                                                <asp:ListItem Value="7">7 Days</asp:ListItem>
                                                                <asp:ListItem Value="14">14 Days</asp:ListItem>
                                                                <asp:ListItem Value="21">21 Days</asp:ListItem>
                                                                <asp:ListItem Value="28">28 Days</asp:ListItem>
                                                                <asp:ListItem Value="42">42 Days</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:Label ID="lblRecordCountDeleted" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" rowspan="1" valign="top">
                                                            <strong>--------------------OR--------------------</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="1" rowspan="1" style="WIDTH: 150px; " valign="top">
                                                            <strong>Start Date to Delete:<br />
                                                            </strong></td>
                                                        <td valign="top" align="left">
                                                            <asp:TextBox ID="txtStartDateDelete" runat="server" TabIndex="5" Width="220px"></asp:TextBox>
                                                            <asp:ImageButton ID="ibtnStartDelete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/images/calendar.gif" TabIndex="4" />
                                                            <asp:Panel ID="pnlStartDateDelete" runat="server" CssClass="popupControl">
                                                                    <asp:Calendar ID="calStartDelete" runat="server" BackColor="White" BorderColor="#3366CC" Font-Names="Verdana"
                         Font-Size="8pt" ForeColor="#003399" Width="220px" Height="200px" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Visible="False">
                                                                        <SelectedDayStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
                                                                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                                        <WeekendDayStyle BackColor="#CCCCFF" />
                                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" Font-Bold="True" BorderWidth="1px" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                                    </asp:Calendar>
                                                                </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="1" rowspan="1" style="width: 150px" valign="top">
                                                            <strong>End Date to Delete:</strong></td>
                                                        <td valign="top" align="left">
                                                            <asp:TextBox ID="txtEndDateDelete" runat="server" TabIndex="7" Width="220px"></asp:TextBox>
                                                            <asp:ImageButton ID="ibtnEndDelete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/images/calendar.gif" TabIndex="6" />
                                                            <asp:Panel ID="pnlEndDateDelete" runat="server" CssClass="popupControl">
                                                                <asp:Calendar ID="calEndDelete" runat="server" BackColor="White" BorderColor="#3366CC" Font-Names="Verdana" 
                        Font-Size="8pt" ForeColor="#003399" Width="220px" Height="200px" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Visible="False">
                                                                    <SelectedDayStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
                                                                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                                                    <OtherMonthDayStyle ForeColor="#999999" />
                                                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" Font-Bold="True" BorderWidth="1px" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                                                </asp:Calendar>
                                                            </asp:Panel>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                </td>
                                                </tr>
                                                </table>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                                <asp:Panel ID="pnlDoc" runat="server" Height="100%" Width="700px" 
                                HorizontalAlign="center" Visible="true" Font-Bold="True" >
                                    Click on a log to view it...
                                    <br />
                                    <br />
                                    <asp:ListBox ID="lstTextLogs" runat="server" AutoPostBack="True" Width="400px"></asp:ListBox>
                                    <br />
                                    <br />
                                </asp:Panel>
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

