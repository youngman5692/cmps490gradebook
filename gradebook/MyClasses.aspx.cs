using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using gradebook.DAL2;

namespace gradebook
{
    public partial class MyClasses : System.Web.UI.Page
    {
        string email;
        int courseID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    email = User.Identity.Name;
                    checkRole();
                    loggedIn.Visible = false;

                    initializeTeacher();
                    initializeStudent();
                }
                else
                {
                    loggedIn.Visible = true;
                    studentPanel.Visible = false;
                    teacherPanel.Visible = false;
                }
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                    email = User.Identity.Name;
            }
        }

        protected void initializeStudent()
        {
            studentCourseDropDown.Items.Clear();
            studentCourseDropDown.Items.Add(new ListItem("--Select--", null));
            using(var context = new GradebookDataEntities())
            {
                var query = from c in context.Courses select new { c.CourseID, c.CourseNumber, c.Term, c.Year };
                foreach(var course in query)
                {
                    string courseFormat = String.Format("{0,-10}\t{1,-10}\t{2, 10}", course.CourseID, course.CourseNumber, course.Term, course.Year);
                    studentCourseDropDown.Items.Add(new ListItem(courseFormat,courseFormat));
                }
            }
        }

        protected void initializeTeacher()
        {
            termDropDownList.Items.Add(new ListItem("--Select--", ""));
            termDropDownList.Items.Add(new ListItem("Fall", "Fall"));
            termDropDownList.Items.Add(new ListItem("Winter", "Winter"));
            termDropDownList.Items.Add(new ListItem("Spring", "Spring"));

            yearDropDownList.DataSource = Enumerable.Range(DateTime.Now.Year, 10);
            yearDropDownList.DataBind();
        }

        protected void studentCourseGridViewFill()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from s in context.Students where s.Email == email select s.Courses;
                var courses = l2equery.FirstOrDefault();

                studentCourseGridView.DataSource = courses;
                studentCourseGridView.DataBind();

            }
        }

        protected void teacherCourseGridViewFill()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from t in context.Teachers where t.Email == email select t.Courses;
                var teacherCourses = l2equery.FirstOrDefault();

                teacherCourseGridView.DataSource = teacherCourses;
                teacherCourseGridView.DataBind();
            }
        }

        protected void checkRole()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = (from s in context.Students where s.Email == email select s).FirstOrDefault();

                if (l2equery != null)
                {
                    studentPanel.Visible = true;
                    teacherPanel.Visible = false;
                    studentCourseGridViewFill();
                }
                else
                {
                    teacherPanel.Visible = true;
                    studentPanel.Visible = false;
                    teacherCourseGridViewFill();
                }
            }
        }

        protected void registerCourseButton_Click(object sender, EventArgs e)
        {
            using (var context = new GradebookDataEntities())
            {
                var query = (from t in context.Teachers where t.Email == email select t.TeacherID).FirstOrDefault();
                int teacher = Convert.ToInt32(query);
                string cNumber = CourseNumber.Text;
                string term = termDropDownList.SelectedValue;
                int year = Convert.ToInt32(yearDropDownList.SelectedValue);

                
                var result = context.AddClass(cNumber,term,year,CourseDescription.Text,query);

            }
            teacherCourseGridViewFill();
        }

        protected void studentCourseDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] value = studentCourseDropDown.SelectedValue.Split(' ');
            courseID = Convert.ToInt32(value[0]);
        }

        protected void registerStudentCourseButton_Click(object sender, EventArgs e)
        {
            if(courseID != 0)
                using(var context = new GradebookDataEntities())
                {
                    Course course = context.Courses.FirstOrDefault(c => c.CourseID == courseID);
                    Student student = context.Students.FirstOrDefault(s => s.Email == email);
                    course.Students.Add(student);

                    context.SaveChanges();
                }
            studentCourseGridViewFill();
        }
    }
}