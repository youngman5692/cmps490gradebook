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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    email = User.Identity.Name;
                    checkRole();
                    loggedIn.Visible = false;
                }
                else
                    loggedIn.Visible = true;
            }
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
                var l2equery = (from t in context.Teachers where t.Email == email select t.Courses).FirstOrDefault();

                teacherCourseGridView.DataSource = l2equery;
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
    }
}