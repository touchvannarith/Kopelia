using NotaliaOnline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class Administrator_login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text.Trim();
            if (login == "Jmv83390" && password == "Lv1lftk")
            {
                Session["administrator"] = "Jmv83390";
                Response.Redirect("UserManagement");
            }
            else
            {
                Helper.ShowToastr(Page, "Veuillez entrer un identifiant et un mot de passe valides", "Connexion échouée", "error");
            }
        }
    }
}