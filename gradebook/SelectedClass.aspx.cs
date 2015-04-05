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
            ClassLabel.Text = s;

            if (s != null)
            {
                courseID = Convert.ToInt32(s);
                if (checkAuthority())
                {
                    loadTeacherCourseGradeGrid();
                    loadTeacherCourseAssignmentGrid();
                    loadTeacherCourseStudentGrid();
                }
            }
        }

        protected bool checkAuthority()
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
    }
}