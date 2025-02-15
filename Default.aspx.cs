using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegisterForFree_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Connexion?Email=" + txtEmail.Value.Trim());
        }

        protected void btnRegisterForFree1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Connexion?Email=" + txtEmail1.Value.Trim());
        }
    }
}