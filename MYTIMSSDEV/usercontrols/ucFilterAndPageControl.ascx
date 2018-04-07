<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucFilterAndPageControl.ascx.vb" Inherits="usercontrols_ucFilterAndPageControl" %>
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table border="0" class="celldata" bgcolor="#EBEBEB" cellspacing="1" bordercolor="#FFFFFF">
                    <tr>
                        <td>Search for</td>
                        <td>
                            <asp:TextBox ID="searchSTR" runat="server" size="20" ToolTip="Enter Criteria for Search."></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;IN&nbsp;
                        </td>
                        <td title = "Column to Search">                                
                            <asp:DropDownList ID="searchFLD" runat="server" DataValueField="Value" DataTextField="Text">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="ButtonFind" runat="server" Text="Find" />

                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <font size ="2">Number of Records: <b><asp:Label ID="LabelNumberOfRecords" runat="server"></asp:Label></b></font> 
            <td>
            <td nowrap class="celldata" align="right">
                <font size="2"> Page: 
                    <asp:DropDownList ID="CurrentPage" runat="server" DataValueField="Value" DataTextField="Text" AutoPostBack="true">
                    </asp:DropDownList>
                 of <asp:Label ID="LabelPageCount" runat="server"></asp:Label></font>
            </td>
            <td nowrap class='celldata' align='right'>
                <font size='2'> Page Size: </font>
                    <asp:DropDownList ID="PageSize" runat="server" DataValueField="Value" DataTextField="Name" AutoPostBack="true">
                    </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />