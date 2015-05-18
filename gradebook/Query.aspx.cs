using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using gradebook.DAL2;

namespace gradebook
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) //On first page load
            {
                fillGrid3();
                studentLabel.Text = "";
                //fillDefault();
                fillDropDownClass();
                studentView.Visible = false;
            }
            //if (User.Identity.Name != "")
                //studentPanel.Visible = true;
        }

        protected void fillDropDownClass()
        {
            TermDropDownList.Items.Add(new ListItem("Fall", "Fall"));
            TermDropDownList.Items.Add(new ListItem("Winter", "Winter"));
            TermDropDownList.Items.Add(new ListItem("Spring", "Spring"));
            IndexDropDownList.Items.Add(new ListItem("--Select--", ""));
            IndexDropDownList.Items.Add(new ListItem("Student", "Student"));
            IndexDropDownList.Items.Add(new ListItem("Teacher", "Teacher"));
            IndexDropDownList.Items.Add(new ListItem("Course", "Course"));
        }

        public void fillDefault()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from c in context.Courses select c;
                var courses = l2equery.ToList();

                courseGridView.DataSource = courses;
                courseGridView.DataBind();

            }
        }

        public void fillGrid3()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from s in context.Students where s.Email.Contains(searchStudentsTextBox.Text) select s;
                var s1 = l2equery.ToList();

                studentGridView.DataSource = s1;
                studentGridView.DataBind();
            }
        }

        protected void searchStudentsTextBox_TextChanged(object sender, EventArgs e)
        {
            /*studentLabel.Text = "'s classes";
            
            fillCourseGridView();
            fillGrid3();
            studentLabel.Text = searchStudentsTextBox.Text + studentLabel.Text;
            if (searchStudentsTextBox.Text == "")
                studentLabel.Text = "";*/
        }
        public void fillCourseGridView()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from s in context.Students where s.Email.Contains(searchStudentsTextBox.Text) select s.Courses;
                var courses = l2equery.FirstOrDefault();

                courseGridView.DataSource = courses;
                courseGridView.DataBind();
            }
        }

        protected void AddStudentButton_Click(object sender, EventArgs e)
        {
            if (AddStudentTextBox.Text != "")
            {
                using (var context = new GradebookDataEntities())
                {
                    Course course = context.Courses.FirstOrDefault(c => c.CourseNumber == CourseNumberDropDownList.SelectedValue);
                    Student student = context.Students.FirstOrDefault(s => s.Email == AddStudentTextBox.Text);
                    course.Students.Add(student);

                    context.SaveChanges();

                    var l2equery = from s in context.Students where s.Email.Contains(searchStudentsTextBox.Text) select s.Courses;
                    var courses = l2equery.FirstOrDefault();

                    courseGridView.DataSource = courses;
                    courseGridView.DataBind();
                }
            }
            else
            {
                string email = studentGridView.Rows[studentGridView.SelectedIndex].Cells[0].Text.ToString(); ;
                using (var context = new GradebookDataEntities())
                {
                    Course course = context.Courses.FirstOrDefault(c => c.CourseNumber == CourseNumberDropDownList.SelectedValue);
                    Student student = context.Students.FirstOrDefault(s => s.Email == email);
                    course.Students.Add(student);

                    context.SaveChanges();

                    var l2equery = from s in context.Students where s.Email.Contains(searchStudentsTextBox.Text) select s.Courses;
                    var courses = l2equery.FirstOrDefault();

                    courseGridView.DataSource = courses;
                    courseGridView.DataBind();
                }     
            }
        }

        protected void searchStudents_Click(object sender, EventArgs e)
        {
            fillGrid3();
        }

        protected void TermDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from c in context.Courses where c.Year >= DateTime.Now.Year && c.Term == TermDropDownList.SelectedValue select c;
                var courses = l2equery.ToList();

                CourseNumberDropDownList.DataSource = courses;
                CourseNumberDropDownList.DataBind();
            }
        }

        protected void IndexDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexDropDownList.SelectedValue == "Student")
                studentView.Visible = true;
            else
                studentView.Visible = false;
        }

        protected void studentGridView_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(studentGridView, "Select$" + e.Row.RowIndex);
            }
        }
            
        protected void studentGridView_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string email = studentGridView.Rows[studentGridView.SelectedIndex].Cells[0].Text.ToString();
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from s in context.Students where s.Email.Contains(email) select s.Courses;
                var courses = l2equery.FirstOrDefault();

                courseGridView.DataSource = courses;
                courseGridView.DataBind();
            }
            searchStudentsTextBox.Text = email;
        }

        protected void courseGridView_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(courseGridView, "Select$" + e.Row.RowIndex);

                LinkButton deleteButton = (LinkButton)e.Row.FindControl("LinkButtonDelete");
                if (deleteButton != null)
                    deleteButton.Visible = false;
            }
        }

        protected void courseGridView_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow row = courseGridView.Rows[courseGridView.SelectedIndex];
            toggleDelete(courseGridView.SelectedIndex);
        }

        public void toggleDelete(int selectedRowIndex)
        {
            foreach(GridViewRow row in courseGridView.Rows)
            {
                bool show = (row.RowIndex == selectedRowIndex);
                LinkButton deleteButton = (LinkButton)row.FindControl("LinkButtonDelete");
                if (deleteButton != null)
                    deleteButton.Visible = show;
            }
        }
    }
}