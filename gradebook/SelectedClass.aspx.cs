using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityFramework.Extensions;
using System.IO;
using System.Text;
using System.Threading;
using gradebook.DAL2;

namespace gradebook
{
    public partial class SelectedClass : System.Web.UI.Page
    {
        SqlConnection sqlconn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected int courseID;
        String s;

        protected void Page_Load(object sender, EventArgs e)
        {
            s = Request.QueryString["classID"];
            courseID = Convert.ToInt32(s);
            

            if (!Page.IsPostBack)
            {               
                s = Request.QueryString["classID"];
                studentGrid.Visible = false;
                teacherPanel.Visible = false;
                //Added stuff
                FileUpload1.Visible = false;
                btnsave.Visible = false;
                FilesGrid.Visible = false;
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                create.Visible = false;
                limitationsText.Visible = false;
                textBoxHeader.Visible = false;
                btnsave2.Visible = false;
                NameText.Visible = false;
                fHeader.Visible = false;
                //
                if (s != null)
                {
                    courseID = Convert.ToInt32(s);
                    getClassName();
                    if (checkIfTeacher())
                    {
                        //BindGrid();
                        loadTeacherCourseGradeGrid();
                        loadTeacherCourseAssignmentGrid();
                        loadTeacherCourseStudentGrid();
                        loadTeacherStudentGradesGrid();
                        //Added Stuff
                        loadTeacherFileGrid();
                        FileUpload1.Visible = true;
                        btnsave.Visible = true;
                        FilesGrid.Visible = true;
                        teacherPanel.Visible = true;
                        TextBox1.Visible = true;
                        TextBox2.Visible = true;
                        create.Visible = true;
                        limitationsText.Visible = true;
                        textBoxHeader.Visible = true;
                        btnsave2.Visible = true;
                        NameText.Visible = true;
                        fHeader.Visible = true;
                        //
                    }
                    else if (checkIfStudent())
                    {
                        loadStudentCourseAssignmentGrid();
                        //AddedStuff
                        loadStudentFileGrid();
                        studentGrid.Visible = true;
                        teacherPanel.Visible = true;
                        fHeader.Visible = true;
                        loadStudentCourseAssignmentGrid();
                        //
                    }
                }
            }
            /*else
            {
                if (checkIfTeacher())
                {
                    loadTeacherCourseGradeGrid();
                    loadTeacherCourseAssignmentGrid();
                    loadTeacherCourseStudentGrid();
                    loadTeacherStudentGradesGrid();
                }
            }*/
        }

        protected void loadTeacherFileGrid()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads"));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            FilesGrid.DataSource = files;
            FilesGrid.DataBind();
        }
        protected void loadStudentFileGrid()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads"));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            studentGrid.DataSource = files;
            studentGrid.DataBind();
        }

        void BindGrid()
        {
            using (var context = new GradebookDataEntities())
            {
                if (context.GradeDistributions.Count() > 0)
                {
                    var gradeDistribution = (from c in context.Courses from g in c.GradeDistributions where c.CourseID == courseID select new { g.GradeID, g.Category, g.Weight }).ToList();

                    teacherCourseGradeGrid.DataSource = gradeDistribution;
                    teacherCourseGradeGrid.DataBind();
                }
                else
                {
                    var obj = new List<GradeDistribution>();
                    obj.Add(new GradeDistribution());
                    // Bind the DataTable which contain a blank row to the GridView
                    teacherCourseGradeGrid.DataSource = obj;
                    teacherCourseGradeGrid.DataBind();
                    int columnsCount = teacherCourseGradeGrid.Columns.Count;
                    teacherCourseGradeGrid.Rows[0].Cells.Clear();// clear all the cells in the row
                    teacherCourseGradeGrid.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
                    teacherCourseGradeGrid.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell
 
                    //You can set the styles here
                    teacherCourseGradeGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    teacherCourseGradeGrid.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                    teacherCourseGradeGrid.Rows[0].Cells[0].Font.Bold = true;
                    //set No Results found to the new added cell
                    teacherCourseGradeGrid.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
                }
            }
            loadTeacherCourseAssignmentGrid();
        }

        protected void getClassName()
        {
            string cName;
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses where c.CourseID == courseID select c).FirstOrDefault();
                cName = query.CourseNumber.ToString();
                ClassLabel.Text = cName;
            }
        }

        protected bool checkIfTeacher()
        {
            string teacherEmail;
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses where c.CourseID == courseID select c.Teacher).FirstOrDefault();
                teacherEmail = query.Email;
            }

            if (teacherEmail == User.Identity.Name)
                return true;
            else
                return false;
        }

        protected bool checkIfStudent()
        {
            string studentEmail;
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses from s in c.Students where c.CourseID == courseID && s.Email == User.Identity.Name select s.Email);
                studentEmail = query.FirstOrDefault();
            }
            if (studentEmail == null)
                return false;
            return true;
        }

        protected void loadTeacherCourseGradeGrid()
        {
            courseID = Convert.ToInt32(Request.QueryString["classID"]);
            using (var context = new GradebookDataEntities())
            {
                var gradeDistribution = (from c in context.Courses from g in c.GradeDistributions where c.CourseID == courseID select new { g.GradeID, g.Category, g.Weight }).ToList();

                teacherCourseGradeGrid.DataSource = gradeDistribution;
                teacherCourseGradeGrid.DataBind();
            }
            loadTeacherCourseAssignmentGrid();
        }

        protected void loadTeacherCourseAssignmentGrid()
        {
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses from grade in c.GradeDistributions from assignment in grade.Assignments where c.CourseID == courseID select new {assignment.AssignmentID, assignment.Description, assignment.PointsPossible, grade.Category}).ToList();

                teacherCourseAssignmentGrid.DataSource = query;
                teacherCourseAssignmentGrid.DataBind();
            }
        }

        protected void loadTeacherCourseStudentGrid()
        {
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses where c.CourseID == courseID select c.Students).FirstOrDefault();

                teacherCourseStudentGrid.DataSource = query;
                teacherCourseStudentGrid.DataBind();
            }
        }

        protected void loadStudentCourseAssignmentGrid()
        {
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses from grade in c.GradeDistributions from assignment in grade.Assignments where c.CourseID == courseID select new { assignment.Description, assignment.PointsPossible }).ToList();

                studentCourseAssignmentGrid.DataSource = query;
                studentCourseAssignmentGrid.DataBind();
            }
        }

        protected void loadTeacherStudentGradesGrid()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "teacherGradesSP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlconn;
            cmd.Parameters.Add("@CourseID", SqlDbType.NVarChar).Value = courseID.ToString();

            sqlconn.Open();

            teacherStudentGradesGrid.DataSource = cmd.ExecuteReader();
            teacherStudentGradesGrid.DataBind();

            sqlconn.Close();


                //context.test(courseID.ToString());
                //var query = from x in context.teacherGrades.AsNoTracking() select x;
                //dynamicBoundFields();
                //var query = context.teacherGradesSP(2012);
                //teacherStudentGradesGrid.DataSource = query;
                //teacherStudentGradesGrid.DataBind();
        }

        protected void dynamicBoundFields()
        {
            BoundField bfield = new BoundField();
            bfield.HeaderText = "Student ID";
            bfield.DataField = "StudentID";
            teacherStudentGradesGrid.Columns.Add(bfield);

            List<TemplateField> tfields = new List<TemplateField>();

            string[,] headers = new string[,]
            {
                {"Homework 1", "Homework_1"},
                {"Spring Final", "Spring_Final"},
                {"Homework 3", "Homework_3"}
            };

            for (int i = 0; i < headers.Length/2; i++)
                tfields.Add(new TemplateField());

            for (int i = 0; i < tfields.Count; i++)
            {
                tfields[i].HeaderText = headers[i, 0];
                tfields[i].ItemTemplate = new AddTemplateToGridView(ListItemType.Item, headers[i, 1]);
                teacherStudentGradesGrid.Columns.Add(tfields[i]);
            }
        }

        protected void DeleteGrade(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            var rowIndex = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;
            int gradeID = Convert.ToInt32(((Label)teacherCourseGradeGrid.Rows[rowIndex].FindControl("lblGradeID")).Text);
            using (var context = new GradebookDataEntities())
            {
                var grade = (from g in context.GradeDistributions where g.GradeID == gradeID select g).FirstOrDefault();
                context.GradeDistributions.Attach(grade);
                context.GradeDistributions.Remove(grade);
                context.SaveChanges();
            }
            loadTeacherCourseGradeGrid();
        }

        protected void gridSample_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int gradeID = Convert.ToInt32(teacherCourseGradeGrid.DataKeys[e.RowIndex].Value);
            using (var context = new GradebookDataEntities())
            {
                GradeDistribution obj = context.GradeDistributions.First(x => x.GradeID == gradeID);
                context.Assignments.Where(x => x.GradeID == gradeID).Delete();
                context.GradeDistributions.Remove(obj);
                context.SaveChanges();
                BindGrid();
            }
        }

        protected void gridSample_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "InsertNew")
            {
                courseID = Convert.ToInt32(Request.QueryString["classID"]);
                string category = ((TextBox)teacherCourseGradeGrid.FooterRow.FindControl("txtCategory")).Text;
                decimal weight = Convert.ToDecimal(((TextBox)teacherCourseGradeGrid.FooterRow.FindControl("txtWeight")).Text);

                using (var context = new GradebookDataEntities())
                {
                    var result = context.AddGradeDistribution(category, weight, courseID);
                }
                BindGrid();
            }
        }

        protected void gridSample_RowEditing(object sender, GridViewEditEventArgs e)
        {
            teacherCourseGradeGrid.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void gridSample_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            teacherCourseGradeGrid.EditIndex = -1;
            BindGrid();
        }

        protected void gridSample_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = teacherCourseGradeGrid.Rows[e.RowIndex];
            TextBox txtCategory = row.FindControl("txtCategory") as TextBox;
            TextBox txtWeight = row.FindControl("txtWeight") as TextBox;
            if (txtCategory != null && txtWeight != null)
            {
                using (var context = new GradebookDataEntities())
                {
                    int gradeID = Convert.ToInt32(teacherCourseGradeGrid.DataKeys[e.RowIndex].Value);
                    GradeDistribution obj = context.GradeDistributions.First(x => x.GradeID == gradeID);
                    obj.Category = txtCategory.Text;
                    obj.Weight = Convert.ToDecimal(txtWeight.Text);
                    context.SaveChanges();
                    teacherCourseGradeGrid.EditIndex = -1;
                    BindGrid();
                }
            }
        }
        protected void teacherAssignment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            teacherCourseAssignmentGrid.EditIndex = e.NewEditIndex;
            loadTeacherCourseAssignmentGrid();
        }
        protected void teacherAssignment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            teacherCourseAssignmentGrid.EditIndex = -1;
            loadTeacherCourseAssignmentGrid();
        }

        protected void teacherAssignment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = teacherCourseAssignmentGrid.Rows[e.RowIndex];
            TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
            TextBox txtPP = row.FindControl("txtPP") as TextBox;
            if (txtDescription != null && txtPP != null)
            {
                using (var context = new GradebookDataEntities())
                {
                    int assignmentID = Convert.ToInt32(teacherCourseAssignmentGrid.DataKeys[e.RowIndex].Value);
                    Assignment obj = context.Assignments.First(x => x.AssignmentID == assignmentID);
                    DropDownList ddlGrade = row.FindControl("ddlGrade") as DropDownList;
                    obj.Description = txtDescription.Text;
                    obj.PointsPossible = Convert.ToInt32(txtPP.Text);
                    obj.GradeID = Convert.ToInt32(ddlGrade.SelectedValue);
                    context.SaveChanges();
                    teacherCourseAssignmentGrid.EditIndex = -1;
                    loadTeacherCourseAssignmentGrid();
                }
            }
        }

        protected void teacherAssignment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int assignmentID = Convert.ToInt32(teacherCourseAssignmentGrid.DataKeys[e.RowIndex].Value);
            using (var context = new GradebookDataEntities())
            {
                Assignment obj = context.Assignments.First(x => x.AssignmentID == assignmentID);
                //Undertake und = context.Undertakes.First(x => x.AssignmentID == assignmentID);
                //context.Undertakes.Remove(und);
                context.Assignments.Remove(obj);
                context.SaveChanges();
                loadTeacherCourseAssignmentGrid();
            }
        }

        protected void teacherAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "InsertNew")
            {
                GridViewRow row = teacherCourseAssignmentGrid.FooterRow;
                string description = ((TextBox)teacherCourseAssignmentGrid.FooterRow.FindControl("txtDescription")).Text;
                int pp = Convert.ToInt32(((TextBox)teacherCourseAssignmentGrid.FooterRow.FindControl("txtPP")).Text);
                DropDownList ddlGrade = row.FindControl("ddlGradeNew") as DropDownList;
                int gradeID = Convert.ToInt32(ddlGrade.SelectedValue);
                using (var context = new GradebookDataEntities())
                {
                    var result = context.AddAssignment(pp, description, gradeID);
                }
                loadTeacherCourseAssignmentGrid();
            }
            if(e.CommandName == "Assign")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = teacherCourseAssignmentGrid.Rows[index];
                int assignmentID = Convert.ToInt32(teacherCourseAssignmentGrid.DataKeys[index].Value);
                using (var context = new GradebookDataEntities())
                {
                    context.Assign(courseID, assignmentID);
                    teacherCourseGradeGrid.DataBind();
                }             
            }
        }

        protected void teacherAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddl = null;
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ddl = e.Row.FindControl("ddlGradeNew") as DropDownList;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddl = e.Row.FindControl("ddlGrade") as DropDownList;
            }
            if (ddl != null)
            {
                using (var context = new GradebookDataEntities())
                {
                    var gradeDistribution = (from c in context.Courses from g in c.GradeDistributions where c.CourseID == courseID select new { g.GradeID, g.Category}).ToList();
                    ddl.DataSource = gradeDistribution;
                    ddl.DataTextField = "Category";
                    ddl.DataValueField = "GradeID";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem(""));
                }
                /*if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    ddl.SelectedValue = drv["Category"].ToString();
                }*/
            }
        }


        //Added Stuff
        protected void get(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                FileInfo file = new FileInfo(filePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                if (file.Extension == ".txt")
                {
                    Response.ContentType = "text/plain";
                }
                if (file.Extension == ".jpg")
                {
                    Response.ContentType = "image/jpeg";
                }
                if (file.Extension == ".doc")
                {
                    Response.ContentType = "application/octet-stream";
                }
                if (file.Extension == ".odt")
                {
                    Response.ContentType = "application/vnd.oasis.opendocument.text";
                }
                Response.Flush();
                Response.TransmitFile(file.FullName);
                try
                {
                    Response.End();
                }
                catch (ThreadAbortException E) { }
            }
            catch (Exception E) { }
        }
        protected void deleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            //Response.Redirect(Request.Url.AbsoluteUri);
            loadStudentFileGrid();
            loadTeacherFileGrid();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (FileUpload1.HasFile)
            {
                try
                {
                    string fileName = Server.HtmlEncode(FileUpload1.FileName);
                    string fileType = FileUpload1.PostedFile.ContentType;
                    //Find file content type by uncommenting this line and viewing output.
                    //  System.Diagnostics.Debug.WriteLine(FileUpload1.PostedFile.ContentType.ToString());
                    if ((fileType == "image/jpeg") || (fileType == "text/plain") || (fileType == "application/octet-stream") || (fileType == "application/vnd.oasis.opendocument.text"))
                    {
                        if (FileUpload1.PostedFile.ContentLength < 2100000)
                        {
                            sb.AppendFormat(" Uploading file: {0}", FileUpload1.FileName);

                            //saving the file
                            FileUpload1.SaveAs(Server.MapPath("~/Uploads/") + FileUpload1.FileName);

                            //Showing the file information
                            sb.AppendFormat("<br/> File Saved.");
                            Label1.Text = sb.ToString();
                            //Response.Redirect(Request.Url.AbsoluteUri);
                            loadTeacherFileGrid();
                        }
                        else
                        {
                            sb.AppendFormat("File must not be bigger than 2mb!");
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<br> jpeg, doc, or odt only.");
                        Label1.Text = sb.ToString();
                    }
                }
                catch (Exception ex)
                {
                    sb.Append("<br/> Error <br/>");
                    sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
                    Label1.Text = sb.ToString();
                }
            }
            else
            {
                sb.AppendFormat("<br>Please add a file.<br/>");
                Label1.Text = sb.ToString();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void saveFile(object sender, EventArgs e)
        {
            string fileName = TextBox1.Text.ToString();
            if (fileName.Length > 25)
            {
                //Response.Redirect(Request.Url.AbsoluteUri);
                Label1.Text = "Not 25 characters.";
            }
            else
            {
                fileName = fileName.Trim();
                fileName += ".txt";
                string filepath = Server.MapPath("~/Uploads/") + fileName;
                if (File.Exists(filepath))
                { File.Delete(filepath); }
                try
                {
                    using (FileStream fs = File.OpenWrite(filepath))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(TextBox2.Text.ToString());
                        fs.Write(info, 0, info.Length);
                    }
                    //Response.Redirect(Request.Url.AbsoluteUri);
                    loadTeacherFileGrid();
                }
                catch (Exception RIOT)
                {
                    Label1.Text = RIOT.ToString();
                }

            }
        }
        //End of Added Stuff




        public class AddTemplateToGridView : ITemplate
        {
            ListItemType _type;
            string _colName;

            public AddTemplateToGridView(ListItemType type, string colname)
            {
                _type = type;

                _colName = colname;

            }
            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {

                switch (_type)
                {
                    case ListItemType.Item:

                        TextBox tb = new TextBox();
                        tb.DataBinding += new EventHandler(ht_DataBinding);
                        container.Controls.Add(tb);

                        break;
                }

            }

            void ht_DataBinding(object sender, EventArgs e)
            {
                TextBox tbx = (TextBox)sender;
                GridViewRow container = (GridViewRow)tbx.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _colName);
                if (dataValue != DBNull.Value)
                {
                    tbx.Text = dataValue.ToString();
                }

            }
        }
    }
}