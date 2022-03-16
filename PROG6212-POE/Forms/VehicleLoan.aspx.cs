using ClassLibrary1;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class VehicleLoan : System.Web.UI.Page
    {
        decimal Repayment;

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
            if (string.IsNullOrWhiteSpace(txtMake.Text))
            {
                x = "Enter vehicle make!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                x = "Enter vehicle model!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(txtVehiclePrice.Text))
            {
                x = "Enter vehicle price!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(txtDeposit.Text))
            {
                x = "Enter total desposit!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(txtInterest.Text))
            {
                x = "Enter interest rate!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else if (string.IsNullOrWhiteSpace(txtInsurance.Text))
            {
                x = "Enter insurance premium!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;

            }
            else
            {
                AddVehicleLoan();
            }
        }


        private void AddVehicleLoan()
        {

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                string Make = txtMake.Text.ToString();
                string Model = txtModel.Text.ToString();
                decimal Price = decimal.Parse(txtVehiclePrice.Text);
                decimal Deposit = decimal.Parse(txtDeposit.Text);
                decimal Interest = decimal.Parse(txtInterest.Text);
                decimal Insurance = decimal.Parse(txtInsurance.Text);

                /// <summary>
                /// calculates neccessary variables using the classlibrary
                /// </summary>
                VehicleRepayment VR = new VehicleRepayment();
                decimal y = VR.DepositRemoved(Price, Deposit);
                decimal z = VR.InterestRate(Interest);
                decimal Total = VR.TotalRepayment(y, z);
                Repayment = VR.MonthlyRepayment(Total, Insurance);

                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.AddVehicleLoanData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@make    ", SqlDbType.NVarChar).Value = Model;
                cmd.Parameters.Add("@Model    ", SqlDbType.NVarChar).Value = Deposit;
                cmd.Parameters.Add("@price    ", SqlDbType.Decimal).Value = Price;
                cmd.Parameters.Add("@deposit    ", SqlDbType.Decimal).Value = Deposit;
                cmd.Parameters.Add("@interest    ", SqlDbType.Decimal).Value = Interest;
                cmd.Parameters.Add("@insurance    ", SqlDbType.Decimal).Value = Insurance;
                cmd.Parameters.Add("@vrepayment    ", SqlDbType.Decimal).Value = Repayment;
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
                        VehicleDetailsMsg();
                       


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


        /// <summary>
        /// Displays details aboutt the vehicle and the monthly repayment
        /// </summary>
        public void VehicleDetailsMsg()
        {
            pnlAlertBox.Visible = true;
            Make.Text = "Vehicle Make: " + txtMake.Text;
            Model.Text = "Vehicle Model: " + txtModel.Text;
            Price.Text = "Vehicle Price: " + txtVehiclePrice.Text;
            Deposit.Text = "Total Deposit: " + txtDeposit.Text;
            Interest.Text = "Interest Rate: " + txtInterest.Text + "%";
            Insurance.Text = "Total Deposit: " + txtInsurance.Text;
            MonthlyRepayment.Text = "Monthly Repayment Amount: " + Repayment.ToString("N");
           
        }


        protected void btnOk_Click(object sender, EventArgs e){
        Response.Redirect("~/Forms/MoneySummary.aspx");
        }
    }
}