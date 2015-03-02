using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using System.Data.SqlClient;
using gradebook.Models;

namespace gradebook.Account
{
    public partial class Register : Page
    {
        protected void CreateStudent_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = TextBox1.Text, Email = TextBox1.Text };
            IdentityResult result = manager.Create(user, TextBox3.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                using (SqlConnection connection = new SqlConnection("Server=tcp:ubqj72so2r.database.windows.net,1433;Database=gradebookdatabase_db;User ID=cyoung_dpopkin@ubqj72so2r;Password=War1.vhic;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"))
                {
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    using (var insertcmd = new SqlCommand("INSERT INTO Student VALUES ('" + TextBox1.Text + "', 'First', 'Last')"))
                    {
                        insertcmd.Connection = connection;
                        cmd.InsertCommand = insertcmd;
                        connection.Open();
                        cmd.InsertCommand.ExecuteNonQuery();
                    }
                }
                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void CreateTeacher_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                using (SqlConnection connection = new SqlConnection("Server=tcp:ubqj72so2r.database.windows.net,1433;Database=gradebookdatabase_db;User ID=cyoung_dpopkin@ubqj72so2r;Password=War1.vhic;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"))
                {
                    SqlDataAdapter cmd = new SqlDataAdapter();
                    using (var insertcmd = new SqlCommand("INSERT INTO Teacher VALUES ('" + Email.Text + "', 'First', 'Last')"))
                    {
                        insertcmd.Connection = connection;
                        cmd.InsertCommand = insertcmd;
                        connection.Open();
                        cmd.InsertCommand.ExecuteNonQuery();
                    }
                }
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}