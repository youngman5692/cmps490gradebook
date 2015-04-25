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
    public partial class SelectedClass : System.Web.UI.Page
    {
        protected int courseID;

        protected void Page_Load(object sender, EventArgs e)
        {
            String s = Request.QueryString["classID"];

            if (!IsPostBack)
            {
                if (s != null)
                {
                    courseID = Convert.ToInt32(s);
                    getClassName();
                    if (checkIfTeacher())
                    {
                        loadTeacherCourseGradeGrid();
                        loadTeacherCourseAssignmentGrid();
                        loadTeacherCourseStudentGrid();
                    }
                    else if (checkIfStudent())
                    {
                        loadStudentCourseAssignmentGrid();
                    }
                }
            }
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
            if(studentEmail == null)
                return false;
            return true;
        }

        protected void loadTeacherCourseGradeGrid()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from c in context.Courses where c.CourseID == courseID select c.GradeDistributions;
                var gradeDistribution = l2equery.FirstOrDefault();

                teacherCourseGradeGrid.DataSource = gradeDistribution;
                teacherCourseGradeGrid.DataBind();
            }
        }

        protected void loadTeacherCourseAssignmentGrid()
        {
            using (var context = new GradebookDataEntities())
            {
                var query = (from c in context.Courses from grade in c.GradeDistributions from assignment in grade.Assignments where c.CourseID == courseID select new { assignment.Description, assignment.PointsPossible }).ToList();

                teacherCourseAssignmentGrid.DataSource = query;
                teacherCourseAssignmentGrid.DataBind();
            }
            teacherCourseAssignmentGrid.GridLines = GridLines.None;
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

        protected void DeleteGradeDistribution()
        {

        }

        protected void AddNewGradeDistribution(Object sender, EventArgs e)
        {
            /*string category = ((TextBox)teacherCourseGradeGrid.FooterRow.FindControl("txtCategory")).Text;
            decimal weight = Convert.ToDecimal(((TextBox)teacherCourseGradeGrid.FooterRow.FindControl("txtWeight")).Text);

            using (var context = new GradebookDataEntities())
            {
                var result = context.AddGradeDistribution(category, weight, courseID);

                ClassLabel.Text = result.ToString();
            }
            */

            ClassLabel.Text = "shits not working";
            loadTeacherCourseAssignmentGrid();
        }
    }
}