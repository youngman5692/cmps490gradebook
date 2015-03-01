﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using gradebook.DAL2;

namespace gradebook
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) //On first page load load default tables
            {
                fillGrid3();
                Label1.Text = "";
                fillDefault();
                fillDropDownClass();  
            }
            if (User.Identity.Name != "")
                studentPanel.Visible = false;
        }

        protected void fillDropDownClass()
        {
            DropDownList1.Items.Add(new ListItem("Fall", "Fall"));
            DropDownList1.Items.Add(new ListItem("Winter", "Winter"));
            DropDownList1.Items.Add(new ListItem("Spring", "Spring"));
        }

        public void fillDefault()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from c in context.Courses select c;
                var courses = l2equery.ToList();

                GridView2.DataSource = courses;
                GridView2.DataBind();

            }
        }

        public void fillGrid3()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from s in context.Students where s.Email.Contains(TextBox1.Text) select s;
                var s1 = l2equery.ToList();

                GridView3.DataSource = s1;
                GridView3.DataBind();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Label1.Text = "'s classes";
            
            fillGrid2();
            fillGrid3();
            Label1.Text = TextBox1.Text + Label1.Text;
            if (TextBox1.Text == "")
                Label1.Text = "";
        }
        public void fillGrid2()
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from s in context.Students where s.Email.Contains(TextBox1.Text) select s.Courses;
                var courses = l2equery.FirstOrDefault();

                GridView2.DataSource = courses;
                GridView2.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
            {
                using (var context = new GradebookDataEntities())
                {
                    Course course = context.Courses.FirstOrDefault(c => c.CourseNumber == DropDownList2.SelectedValue);
                    Student student = context.Students.FirstOrDefault(s => s.Email == TextBox2.Text);
                    course.Students.Add(student);

                    context.SaveChanges();

                    var l2equery = from s in context.Students where s.Email.Contains(TextBox1.Text) select s.Courses;
                    var courses = l2equery.FirstOrDefault();

                    GridView2.DataSource = courses;
                    GridView2.DataBind();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            fillGrid3();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new GradebookDataEntities())
            {
                var l2equery = from c in context.Courses where c.Year >= DateTime.Now.Year && c.Term == DropDownList1.SelectedValue select c;
                var courses = l2equery.ToList();

                DropDownList2.DataSource = courses;
                DropDownList2.DataBind();
            }
        }
    }
}