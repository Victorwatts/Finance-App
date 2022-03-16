using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class MonthlyIncome : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Forms/LoginForm.aspx");
            }
             Label1.Text = Session["User"].ToString();
           
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            Validation();
        }

      
        private void Validation()
        {
            string x;
            if (string.IsNullOrWhiteSpace(Income.Text))
            {
                //MessageBox.Show("Enter username!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(Tax.Text))
            {
                //MessageBox.Show("Enter password!");
                x = "Enter tax deduction!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                AddMonthlyIncome();
            }

        }

        private void AddMonthlyIncome()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                decimal income = decimal.Parse(Income.Text);
                decimal tax = decimal.Parse(Tax.Text);
                int UserID = Convert.ToInt32( Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.AddIncomeData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@income    ", SqlDbType.Decimal).Value = income;
                cmd.Parameters.Add("@tax  ", SqlDbType.Decimal).Value = tax;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                    if (response == "Success" || response == "SuccessUpdate")
                    {
                        LabelAlert.BackColor = Color.Green;
                        LabelAlert.Text = "Data Saved";
                        Response.Redirect("~/Forms/MonthlyExpenses.aspx");


                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Fail");
                    LabelAlert.BackColor = Color.Red;
                    LabelAlert.Text = "Error!";
                }


            }
        }

        
    }
}