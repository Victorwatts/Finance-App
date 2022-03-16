using ClassLibrary1;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class MonthlyExpenses : System.Web.UI.Page
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
            if (string.IsNullOrWhiteSpace(txtGroceries.Text))
            {
                //MessageBox.Show("Enter amount for groceries!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
                

            }
            else if (string.IsNullOrWhiteSpace(txtUtilities.Text))
            {
               // MessageBox.Show("Enter amount for utilities!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtTravel.Text))
            {
               // MessageBox.Show("Enter amount for travel!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtCell.Text))
            {
               // MessageBox.Show("Enter amount for cellphone and telephone!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtOther.Text))
            {
               // MessageBox.Show("Enter amount for other!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                AddExpenses();
            }
        }


        private void AddExpenses()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                decimal Groceries = decimal.Parse(txtGroceries.Text);
                decimal Utilities = decimal.Parse(txtUtilities.Text);
                decimal Travel = decimal.Parse(txtTravel.Text);
                decimal CellPhone = decimal.Parse(txtCell.Text);
                decimal Other = decimal.Parse(txtOther.Text);

                ///calculates the total for expenses
                TotalMonthlyExpenditure total = new TotalMonthlyExpenditure();
                decimal Total = total.TotalExpenditure(Groceries, Utilities, Travel, CellPhone, Other);
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.AddExpenseData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@groceries    ", SqlDbType.Decimal).Value = Groceries;
                cmd.Parameters.Add("@utilities  ", SqlDbType.Decimal).Value = Utilities;
                cmd.Parameters.Add("@travel    ", SqlDbType.Decimal).Value = Travel;
                cmd.Parameters.Add("@cellPhone  ", SqlDbType.Decimal).Value = CellPhone;
                cmd.Parameters.Add("@other    ", SqlDbType.Decimal).Value = Other;
                cmd.Parameters.Add("@total    ", SqlDbType.Decimal).Value = Total;
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
                        Response.Redirect("~/Forms/BuyOrRent.aspx");


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