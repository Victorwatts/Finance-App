using System;

namespace PROG6212_POE.Forms
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Session["UserID"] = null;
            Response.Redirect("~/Forms/LoginForm.aspx");
        }
    }
}