using System;
using ClassLibrary1;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class SavingsForm : System.Web.UI.Page
    {
        decimal MonthlyAmount;
        decimal Interest;
        decimal Period;
        decimal Amount;

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
            if (string.IsNullOrWhiteSpace(txtTarget.Text))
            {
                //MessageBox.Show("Enter amount for groceries!");
                x = "Enter target amount!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;


            }
            else if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                // MessageBox.Show("Enter amount for utilities!");
                x = "Enter reason for saving!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtTime.Text))
            {
                // MessageBox.Show("Enter amount for utilities!");
                x = "Enter period(years)!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtInterest.Text))
            {
                // MessageBox.Show("Enter amount for travel!");
                x = "Enter interest rate!";
                LabelAlert.Text = x;
                LabelAlert.Visible = true;
                return;
            }
            else
            {
                SavingsCalc();
            }
        }


        private void SavingsCalc()
        {
            Interest = decimal.Parse(txtInterest.Text);
            Period = decimal.Parse(txtTime.Text);
            Amount = decimal.Parse(txtTarget.Text);
            SavingsGoal SG = new SavingsGoal();
            MonthlyAmount = SG.SavingsCalculation(Interest, Period, Amount);
            SavingsDetails();
        }

        public void SavingsDetails()
        {
            pnlAlertBox.Visible = true;
            lblAmount.Text = "Monthly Saving Amount To Reach Goal: " + Math.Round(MonthlyAmount, 2); 
          
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            pnlAlertBox.Visible = false;
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Response.Redirect("~/Forms/LoginForm.aspx");
        }
    }
}