<%@ Page Title="" Language="VB" MasterPageFile="~/TIMSSSCS2015/TIMSSSCS2015.master" AutoEventWireup="false" CodeFile="Select.aspx.vb" Inherits="TIMSSSCS2015_Select" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" Runat="Server">
    <table  width = "100%">
        <tr>
            <td bgcolor="#FFFFFF">
                <p>&nbsp;</p>
                <b>Choose WorkArea you wish to view:</b>
                &nbsp;
                <asp:DropDownList ID="DropDownWorkArea" runat="server" DataTextField="Name" DataValueField="Value">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="ButtonGo" runat="server" Text="Go" />
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
            </td>
        </tr>
    </table>
</asp:Content>

