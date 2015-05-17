<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedClass.aspx.cs" Inherits="gradebook.SelectedClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Class Page</h1>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="Label1" Text="Login to see your grades." Font-Size="12" />
            <h3 id="fHeader" runat="server">Files</h3>
            <asp:GridView ID="FilesGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="Text" HeaderText="File Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("Value") %>' OnClick="get" Text="Download"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="Remove" runat="server" Text="Delete" CommandArgument='<%# Eval("Value") %>' OnClick="deleteFile"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <asp:GridView ID="studentGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed">
                <Columns>
                    <asp:BoundField DataField="Text" HeaderText="File Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("Value") %>' OnClick="get" Text="Download"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" Style="width: 85px" />
            <br />
            <br />
            <h2 id="create" runat="server">Create .txt file:</h2>
            <h3 id="NameText" runat="server">File Name:</h3>
            <p runat="server" id="limitationsText">Files are saved as .txt. 25 character limit on filenames.</p>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <h3 id="textBoxHeader" runat="server">Text:</h3>
            <asp:TextBox ID="TextBox2" runat="server" Height="120px" TextMode="MultiLine" Width="486px"></asp:TextBox>
            <br />
            <asp:Button ID="btnsave2" runat="server" Visible="true" OnClick="saveFile" Text="save" />
            <br />
            <br />
            </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="teacherPanel" runat="server">
        <ContentTemplate>
            <asp:Label runat="server" ID="ClassLabel" Text="Test" Font-Size="32" />
            <h3>Grade Weights</h3>
            <asp:GridView ID="teacherCourseGradeGrid" runat="server" EnableViewState="true" AutoGenerateColumns="false" ShowFooter="true" CssClass="table table-striped table-bordered table-condensed table-hover"
                OnRowEditing="gridSample_RowEditing" OnRowDeleting="gridSample_RowDeleting" OnRowCommand="gridSample_RowCommand" DataKeyNames="GradeID"
                OnRowUpdating="gridSample_RowUpdating" OnRowCancelingEdit="gridSample_RowCancelingEdit">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="" CommandName="Edit" ToolTip="Edit"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/Edit.png" /></asp:LinkButton>
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument=''
                                OnClientClick="return confirm('Do you want to delete?')"
                                Text="" CommandName="Delete"><img src="http://cs.csubak.edu/~cyoung/490/Icons/Delete.gif" /></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkInsert" runat="server" Text="" CommandName="Update" ToolTip="Save"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/InsertS.png" /></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" Text="" CommandName="Cancel" ToolTip="Cancel"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/Undo.png" /></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkInsert" runat="server" ClientIDMode="AutoID" Text="" CommandName="InsertNew" ToolTip="Add New Entry"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/Insert.png" /></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCategory" runat="server"
                                Text='<%# Eval("Category")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weight">
                        <ItemTemplate>
                            <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWeight" runat="server"
                                Text='<%# Eval("Weight")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


            <h3>Assignments</h3>
            <asp:GridView ID="teacherCourseAssignmentGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-condensed"
                ShowFooter="true" OnRowEditing="teacherAssignment_RowEditing" OnRowDeleting="teacherAssignment_RowDeleting" OnRowCommand="teacherAssignment_RowCommand" DataKeyNames="AssignmentID"
                OnRowUpdating="teacherAssignment_RowUpdating" OnRowCancelingEdit="teacherAssignment_RowCancelingEdit" onrowdatabound="teacherAssignment_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="" CommandName="Edit" ToolTip="Edit"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/Edit.png" /></asp:LinkButton>
                            <asp:LinkButton ID="lnkRemove" runat="server"
                                CommandArgument=''
                                OnClientClick="return confirm('Do you want to delete?')"
                                Text="" CommandName="Delete"><img src="http://cs.csubak.edu/~cyoung/490/Icons/Delete.gif" /></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkInsert" runat="server" Text="" CommandName="Update" ToolTip="Save"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/InsertS.png" /></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" Text="" CommandName="Cancel" ToolTip="Cancel"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/Undo.png" /></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkInsert" runat="server" Text="" CommandName="InsertNew" ToolTip="Add New Entry"
                                CommandArgument=''><img src="http://cs.csubak.edu/~cyoung/490/Icons/Insert.png" /></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="GradeID">
                        <ItemTemplate>
                            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlGrade" AppendDataBoundItems="True" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlGradeNew" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server"
                                Text='<%# Eval("Description")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Points Possible">
                        <ItemTemplate>
                            <asp:Label ID="lblPP" runat="server" Text='<%# Eval("PointsPossible") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPP" runat="server"
                                Text='<%# Eval("PointsPossible")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPP" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAssign" runat="server" Text="" CommandName="Assign"
                                CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><img src="http://cs.csubak.edu/~cyoung/490/Icons/Assign.png" /></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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

            <h3>Grades</h3>
            <asp:GridView ID="teacherStudentGradesGrid" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-condensed">
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
