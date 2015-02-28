<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Query.aspx.cs" Inherits="gradebook.Query" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="Panel" runat="server" DefaultButton="Button2">
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Search" OnClick="Button2_Click" />
    </asp:Panel>

    <br />
    <asp:GridView ID="GridView3" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />

    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
        <Columns>
            <asp:BoundField DataField="CourseNumber" HeaderText="Course" ItemStyle-Width="150" />
            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150" />
            <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-Width="150" />
            <asp:BoundField DataField="Term" HeaderText="Term" ItemStyle-Width="150" />
    </Columns>
    </asp:GridView>

    <br />
    <asp:UpdatePanel ID="search" runat="server"  DefaultButton="Button1">
        <Triggers>
            <asp:AsyncPostBackTrigger controlid="DropDownList1" eventname="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList2" runat="server" DataValueField="CourseNumber">
            </asp:DropDownList>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />

    </asp:Content>
