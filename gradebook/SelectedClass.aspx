<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedClass.aspx.cs" Inherits="gradebook.SelectedClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Class Page</h1>
    <asp:Label runat="server" ID="ClassLabel" Text="Test" />

    <asp:GridView ID="teacherCourseGradeGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
        <Columns>
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Weight" HeaderText="Weight" />
        </Columns>
    </asp:GridView>

    <br />

    <asp:GridView ID="teacherCourseAssignmentGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
        <Columns>
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="PointsPossible" HeaderText="Points Possible" />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="teacherCourseStudentGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
        <Columns>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
        </Columns>
    </asp:GridView>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NotMainContent" runat="server">
</asp:Content>
