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

            <asp:DropDownList runat="server" ID="studentCourseDropDown" OnSelectedIndexChanged="studentCourseDropDown_SelectedIndexChanged" />
            <asp:Button runat="server" ID="registerStudentCourseButton" text="Register" CssClass="btn btn-default" onclick="registerStudentCourseButton_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="teacherPanel" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="registerCourseButton" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView ID="teacherCourseGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="CourseNumber" HeaderText="Course" />
                    <asp:BoundField DataField="Description" HeaderText="Course Description" />
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div style="display: inline-block">
                <h4>Register New Classes Below!</h4>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="CourseNumber" CssClass="col-md-2 control-label">Course Number</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="CourseNumber" CssClass="form-control" TextMode="SingleLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="CourseNumber"
                            CssClass="text-danger" ErrorMessage="" ValidationGroup="registerCourseGroup" />
                    </div>
                </div>

                <asp:DropDownList runat="server" ID="termDropDownList" CssClass="btn btn-default dropdown-toggle" />
                <asp:DropDownList runat="server" ID="yearDropDownList" CssClass="btn btn-default dropdown-toggle" />

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="CourseDescription" CssClass="col-md-2 control-label">Course Description</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="CourseDescription" CssClass="form-control" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="CourseDescription"
                            CssClass="text-danger" ErrorMessage="" ValidationGroup="registerCourseGroup" />
                    </div>
                </div>

                <asp:Button runat="server" ID="registerCourseButton" Text="Register" CssClass="btn btn-default" OnClick="registerCourseButton_Click"  ValidationGroup="registerCourseGroup"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
