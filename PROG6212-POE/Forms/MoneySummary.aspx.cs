using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class MoneySummary : System.Web.UI.Page
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

            AddUserID();
           
            GetSummary();


        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Response.Redirect("~/Forms/LoginForm.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            pnlAlertBox.Visible = false;
        }

        protected void ViewMore_Click(object sender, EventArgs e)
        {
            pnlAlertBox.Visible = true;
            GetIcome();
            GetExpense();
            GetHomeLoan();
            GetRent();
            GetVehicleLoan();
        }

        private void GetIcome()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.GetMonthlyIncome", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridIncome.DataSource = dt;
                    GridIncome.DataBind();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        private void GetExpense()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.GetMonthlyExpenses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridExpenses.DataSource = dt;
                    GridExpenses.DataBind();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        private void GetHomeLoan()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.GetHomeLoanRepayment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridHomeLoan.DataSource = dt;
                    GridHomeLoan.DataBind();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        private void GetRent()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.GetMonthlyRent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridRent.DataSource = dt;
                    GridRent.DataBind();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        private void GetVehicleLoan()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.GetVehicleLoanRepayment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridVehicleLoan.DataSource = dt;
                    GridVehicleLoan.DataBind();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        private void GetSummary()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.GetSummary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridMoneyLeft.DataSource = dt;
                    GridMoneyLeft.DataBind();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        private void AddUserID()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.constr))
            {
                int UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlCommand cmd = new SqlCommand("dbo.AddUserID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid     ", SqlDbType.Int).Value = UserID;
                var resultParam = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar, 250);
                resultParam.Direction = ParameterDirection.Output;
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string response = resultParam.Value.ToString();
                    Console.WriteLine(response);
                }
                catch (Exception)
                {

                }
            }
        }

        protected void GoToSavings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/SavingsForm.aspx");
        }
    }
}