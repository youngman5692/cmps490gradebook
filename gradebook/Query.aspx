<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Query.aspx.cs" Inherits="gradebook.Query" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="IndexDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="IndexDropDownList_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <asp:Panel ID="studentView" runat="server">
                <asp:UpdatePanel ID="studentPanel" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="studentGridView" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:TextBox ID="searchStudentsTextBox" runat="server" OnTextChanged="searchStudentsTextBox_TextChanged"></asp:TextBox>
                        <asp:Button ID="searchStudents" runat="server" Text="Search" OnClick="searchStudents_Click" />
                        <br />
                        <asp:GridView ID="studentGridView" runat="server" autogeneratecolumns="false" autogenerateselectbutton="false" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="studentGridView_RowCreated" OnSelectedIndexChanged="studentGridView_SelectedIndexChanged">
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
                            <Columns>
                                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="150" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Label ID="studentLabel" runat="server" Text="Label"></asp:Label>
                        <br />
                        <asp:GridView ID="courseGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed" GridLines="Vertical" OnRowCreated="courseGridView_RowCreated" OnSelectedIndexChanged="courseGridView_SelectedIndexChanged"> 
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete" CommandName="Delete" Text="Delete" runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:UpdatePanel ID="search" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="TermDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TermDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="CourseNumberDropDownList" runat="server" DataValueField="CourseNumber">
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="AddStudentTextBox" runat="server"></asp:TextBox>
                        <asp:Button ID="AddStudentButton" runat="server" OnClick="AddStudentButton_Click" Text="Add" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <br />
    <br />
  

    </asp:Content>
