<%@ Page Title="Calendar" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="Calendar.aspx.vb" Inherits="Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%" height="100%" cellpadding="10">
        <tr>
            <td bgcolor="#FFFFFF">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="33%">
            <a href="#" title="Save as PDF" onclick="window.open('<%= Page.ResolveClientUrl("~/")%>PDFGenerator.aspx?url=' + encodeURIComponent('<%=Request.Url.PathAndQuery %>?s=<%=Me.DropDownListREPSBGRP.SelectedValue %>&r=<%=Me.DropDownListRegion.SelectedValue %>&d=<%=Me.CalendarAssesment.VisibleDate %>'));"><img id="Img1" src="~/common/images/pdficon_small.png" border="0" alt="Save as pdf" style="margin-right:10px" runat="server" />Save as PDF</a>
            
            <input type="button"  value="Print Page" onclick="window.print();" />
                </td>
                <td width="33%" align="center">
                    <b>TIMSS and ICLIS</b>
                    <br />
                  <%--  <b>State: </b>
                    <asp:DropDownList ID="DropDownListState" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>--%>
                    <b>REPSBGRP: </b>
                    <asp:DropDownList ID="DropDownListREPSBGRP" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                    <b>Region: </b>
                    <asp:DropDownList ID="DropDownListRegion" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
                
                </td>
                <td width="33%" align="right">
            Run Date: <asp:Label ID="LabelRunDate" runat="server"></asp:Label>
                <br />
            Run Time: <asp:Label ID="LabelRunTime" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
            <br />
                <asp:Calendar ID="CalendarAssesment" runat="server" BackColor="#FFFFFF" 
                    BorderColor="#000000" BorderWidth="1px" DayNameFormat="Full" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#000000" Height="100%" 
                    ShowGridLines="True" Width="100%"><%--Visible="false" --%>
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#FFFFFF" BackColor="#CECECE" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFFFFF" />
                    <TitleStyle BackColor="#123F75" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            <br />
                <asp:Calendar ID="CalendarAssesment2" runat="server" BackColor="#FFFFFF" 
                    BorderColor="#000000" BorderWidth="1px" DayNameFormat="Full" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#000000" Height="100%" 
                    ShowGridLines="True" Width="100%" ShowNextPrevMonth="false">
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#FFFFFF" BackColor="#CECECE" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFFFFF" />
                    <TitleStyle BackColor="#123F75" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            <br />
                <asp:Calendar ID="CalendarAssesment3" runat="server" BackColor="#FFFFFF" 
                    BorderColor="#000000" BorderWidth="1px" DayNameFormat="Full" 
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="#000000" Height="100%" 
                    ShowGridLines="True" Width="100%" ShowNextPrevMonth="false" >
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#FFFFFF" BackColor="#CECECE" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                    <SelectorStyle BackColor="#FFFFFF" />
                    <TitleStyle BackColor="#123F75" Font-Bold="True" Font-Size="9pt" 
                        ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                </asp:Calendar>
            <br />
            </td>
        </tr>
    </table>
</asp:Content>

