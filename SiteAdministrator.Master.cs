using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class SiteAdministrator : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["administrator"] == null)
            {
                btnSignIn.Visible = true;
                btnSignOut.Visible = false;
                return;
            }
            lblUserLoggedIn.InnerText = Session["administrator"].ToString();
            btnSignIn.Visible = false;
            btnSignOut.Visible = true;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Administrator_login");
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session["administrator"] = null;
            Response.Redirect("/");
        }
    }
}