<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedClass.aspx.cs" Inherits="gradebook.SelectedClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Class Page</h1>
    <asp:Label runat="server" ID="ClassLabel" Text="Test" Font-Size="32"/>

    <asp:UpdatePanel ID="teacherPanel" runat="server">
        <ContentTemplate>
            <h3>Grade Weights</h3>
            <asp:GridView ID="teacherCourseGradeGrid" runat="server" AutoGenerateColumns="false" ShowFooter = "true" CssClass="table table-striped table-bordered table-condensed table-hover" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Category" >
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weight">
                        <ItemTemplate>
                            <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument='<%# Eval("GradeID")%>'
                                Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="AddNewGradeDistribution" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField  ShowEditButton="True" />
                </Columns>
            </asp:GridView>

            <h3>Assignments</h3>
            <asp:GridView ID="teacherCourseAssignmentGrid" runat="server" AutoGenerateColumns="false"  CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="PointsPossible" HeaderText="Points Possible" />
                </Columns>
            </asp:GridView>

            <h3>Student List</h3>
            <asp:GridView ID="teacherCourseStudentGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="studentPanel" runat="server">
        <ContentTemplate>
            <asp:GridView ID="studentCourseAssignmentGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="PointsPossible" HeaderText="Points Possible" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NotMainContent" runat="server">
</asp:Content>
