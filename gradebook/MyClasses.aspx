<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyClasses.aspx.cs" Inherits="gradebook.MyClasses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Classes</h2>
    <asp:Label ID="loggedIn" runat="server" Text="Login to See Your Current Classes!" Visible="true"></asp:Label>

    <asp:UpdatePanel ID="studentPanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:GridView ID="studentCourseGridView" runat="server" autogeneratecolumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="CourseNumber" HeaderText="Course" />
                    <asp:BoundField DataField="Description" HeaderText="Course Description" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="teacherPanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:GridView ID="teacherCourseGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="CourseNumber" HeaderText="Course" />
                    <asp:BoundField DataField="Description" HeaderText="Course Description" />
                </Columns>
            </asp:GridView>
            <div class="form-group">
                <asp:Label runat="server" Text="Register A New Class!" />
                <br />
                <asp:TextBox runat="server"></asp:TextBox>
                <asp:TextBox runat="server"></asp:TextBox>
                <br />
                <asp:Button runat="server" Text="Register" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
