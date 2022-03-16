using ClassLibrary1;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class HomeLoan : System.Web.UI.Page
    {
        decimal Repayment;
        string Warning;

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
            if (string.IsNullOrWhiteSpace(txtPropertyPrice.Text))
            {
                //MessageBox.Show("Enter amount for groceries!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;


            }
            else if (string.IsNullOrWhiteSpace(txtDeposit.Text))
            {
                // MessageBox.Show("Enter amount for utilities!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtInterest.Text))
            {
                // MessageBox.Show("Enter amount for travel!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtMonths.Text))
            {
                // MessageBox.Show("Enter amount for cellphone and telephone!");
                x = "Enter gross income!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                AddHomeLaon();
            }
        }


        private void AddHomeLaon()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                decimal Price = decimal.Parse(txtPropertyPrice.Text);
                decimal Deposit = decimal.Parse(txtDeposit.Text);
                decimal Interest = decimal.Parse(txtInterest.Text);
                int Months = int.Parse(txtMonths.Text);

                ///calculates the neccessary variables using the classlibrary
                HomeLoanRepayment HLP = new HomeLoanRepayment();
                decimal x = HLP.MonthsToYears(Months);
                decimal y = HLP.DepositRemoved(Price, Deposit);
                decimal z = HLP.InterestRate(Interest);
                decimal Total = HLP.TotalRepayment(y, z, x);
                Repayment = HLP.MonthlyRepayment(Total, Months);
                decimal grossIncom = 0;
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.AddHomeLoanData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@price    ", SqlDbType.Decimal).Value = Price;
                cmd.Parameters.Add("@deposit    ", SqlDbType.Decimal).Value = Deposit;
                cmd.Parameters.Add("@interest    ", SqlDbType.Decimal).Value = Interest;
                cmd.Parameters.Add("@months    ", SqlDbType.Int).Value = Months;
                cmd.Parameters.Add("@repayment    ", SqlDbType.Decimal).Value = Repayment;
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



                        SqlCommand cmdGrossIncome = new SqlCommand("SELECT GrossIncome FROM MonthlyIncome WHERE UserID= @userid ", con);
                        cmdGrossIncome.CommandType = CommandType.Text;
                        cmdGrossIncome.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                        try
                        {
                            con.Open();
                            grossIncom = (decimal)cmdGrossIncome.ExecuteScalar();
                            con.Close();
                        }
                        catch (Exception)
                        {

                        }
                        Warning = HLP.RepaymentWarining(grossIncom, Repayment);

                        ///determines if the homeloan monthly repayment is more than a third of the users income
                        if (grossIncom - Repayment < grossIncom / 3)
                        {
                           MessageOutputWarning();
                        }
                        else
                        {
                           MessageOutput();
                        }

                       
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/BuyAcar.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAlertBox.Visible = false;
        }

        // <summary>
        ///Method to output message if amount is over a third of the users income
        // </summary>
        public void MessageOutputWarning()
        {
            pnlAlertBox.Visible = true;
            Price.Text = "Property Price: " + txtPropertyPrice.Text;
            Deposit.Text = "Total Deposit: " + txtDeposit.Text;
            Interest.Text = "Interest Rate: " + txtInterest.Text + "%";
            Months.Text = "Months To Repay: " + txtMonths.Text;
            MonthlyRepayment.Text = "Monthly Repayment Amount: " + Repayment.ToString("N");
            WarningX.Text = "Warning!  " + Warning;
            WarningX.ForeColor = Color.Red;
        }

        /// <summary>
        /// Method to output message if amount is less than a third of the users income
        /// </summary>
        public void MessageOutput()
        {
            pnlAlertBox.Visible = true;
            Price.Text = "Property Price: " + txtPropertyPrice.Text;
            Deposit.Text = "Total Deposit: " + txtDeposit.Text;
            Interest.Text = "Interest Rate: " + txtInterest.Text + "%";
            Months.Text = "Months To Repay: " + txtMonths.Text;
            MonthlyRepayment.Text = "Monthly Repayment Amount: " + Repayment.ToString("N");
            WarningX.Text = "" + Warning;
            WarningX.ForeColor = Color.Green;
        }
    }
}