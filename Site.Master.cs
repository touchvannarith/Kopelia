using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            if (!absolutePath.Contains("Connexion"))
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("NextUrl", absolutePath) { HttpOnly = false });
            if (Session["CLIENT_ID"] == null)
            {
                btnSignIn.Visible = true;
                btnSignOut.Visible = false;
                if (absolutePath.Contains("Espace_client") || absolutePath.Contains("Abonnement"))
                {
                    Response.Redirect("/Connexion");
                }
                return;
            }
            if (Session["CLIENT_ID"] != null)
            {
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var id = Session["CLIENT_ID"].TransformToInt();
                    var client = ctx.online_Client.FirstOrDefault(t => t.Id == id);
                    if (client != null)
                    {
                        lblUserLoggedIn.InnerText = client.Login;
                        Abonnement.Visible = (bool)client.IsAdmin;
                    }
                }
            }
            btnSignIn.Visible = false;
            btnSignOut.Visible = true;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Connexion");
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            if (Session["CLIENT_ID"] != null)
            {
                var id = Session["CLIENT_ID"].TransformToInt();
                ClientDataAccess.LoggedInCount(id, false);
                Session["CLIENT_ID"] = null;
                Session["ReferenceCustomer"] = null;
                Session["Administrateur"] = null;
            }
            btnSignOut.Visible = false;
            btnSignIn.Visible = true;
            Response.Redirect("Connexion");
        }
    }
}