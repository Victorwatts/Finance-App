using System;
using System.Web;

namespace PROG6212_POE.Forms
{
    public partial class BuyOrRent : System.Web.UI.Page
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

        protected void Buy_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/HomeLoan.aspx");
        }

        protected void Rent_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/RentalAmount.aspx");
        }
    }
}