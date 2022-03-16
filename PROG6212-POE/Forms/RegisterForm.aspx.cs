using System;
using System.Data;
using System.Data.SqlClient;

namespace PROG6212_POE.Forms
{
    public partial class RegisterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
           Validation();
        }


        private void Validation()
        {
            string x;
            if (string.IsNullOrWhiteSpace(Username.Text))
            {
                //MessageBox.Show("Enter username!");
                 x = "Enter username!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(password.Text))
            {
                //MessageBox.Show("Enter password!");
                x = "Enter password!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (confirm_password.Text == "")
            {
               // MessageBox.Show("Enter password!");
                x = "Enter confirm password!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (confirm_password.Text != password.Text)
            {
                //MessageBox.Show(" Passwords do not match!");
                x = "Passwords do not match!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                Adduser();
            }

        }



        private void Adduser()
        {

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {

                string username = Username.Text.ToString(); ;
                string Password = password.Text.ToString();

                SqlCommand cmd = new SqlCommand("dbo.AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username    ", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@upassword  ", SqlDbType.VarChar).Value = Password;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                   
                    if (response == "Success")
                    {
                        Response.Redirect("~/Forms/LoginForm.aspx");
                    }
                    else
                    {
                        LabelAlert.Text = "User sign up failed";
                        LabelAlert.Visible = true;
                    }


                }
                catch (Exception)
                {

                }



            }


        }

        protected void GoToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/LoginForm.aspx");
        }
    }
}