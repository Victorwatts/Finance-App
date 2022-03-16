using System;
using System.Data;
using System.Data.SqlClient;

namespace PROG6212_POE.Forms
{
    public partial class LoginForm : System.Web.UI.Page
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
            else
            {
                LoginUser();
            }

        }

        private void LoginUser()
        {

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {

                string username = Username.Text.ToString(); ;
                string Password = password.Text.ToString();

                SqlCommand cmd = new SqlCommand("dbo.userLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@loginname    ", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@upassword  ", SqlDbType.NVarChar).Value = Password;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                    if (response == "User successfully logged in")
                    {
                        ///gets the user id once they have logged in, the id is stored in the class library for later reference.
                        SqlCommand cmdUid = new SqlCommand("dbo.GetUserID", con);
                        cmdUid.CommandType = CommandType.StoredProcedure;
                        cmdUid.Parameters.Add("@loginname  ", SqlDbType.VarChar).Value = username;
                        cmdUid.Parameters.Add("@upassword  ", SqlDbType.NVarChar).Value = Password;
                        var resultParamId = cmdUid.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                        resultParamId.Direction = ParameterDirection.Output;
                        try
                        {
                            con.Open();
                            int I = (int)cmdUid.ExecuteScalar();
                            con.Close();
                            Session["UserID"] = I;
                            Session["User"] = username;
                            response = resultParamId.Value.ToString();
                            ///Used for testing, will display the response returned from the db
                            Console.WriteLine(response);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Fail");
                        }

                        Response.Redirect("~/Forms/MonthlyIncome.aspx");

                    }
                    else if (response == "Invalid login")
                    {
                        LabelAlert.Text = "Username or password is incorrect";
                        LabelAlert.Visible = true;
                    }
                    else if (response == "Incorrect password")
                    {
                        LabelAlert.Text = "Password is incorrect";
                        LabelAlert.Visible = true;
                    }

                }
                catch (Exception)
                {

                }



            }


        }

        protected void GoToRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/RegisterForm.aspx");
        }
    }
}