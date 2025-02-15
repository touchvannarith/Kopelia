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
    public partial class Confirmation_du_compte : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Ref"]))
                {
                    var split = Request.QueryString["Ref"].Split(';');
                    if (split[0].Equals("UserCreateAccount", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(split[4]))
                            CreateUserAccount(int.Parse(split[1]), split[2], split[3]);
                        else
                        {
                            CreateUserAccount(int.Parse(split[1]), split[2], split[3], split[4]);
                        }
                    }
                }
                //activate account
                else if (!string.IsNullOrEmpty(Request.QueryString["idc"]) &&
                         !string.IsNullOrEmpty(Request.QueryString["idsu"]))
                {
                    using (var ctx = new NotaliaOnlineEntities())
                    {
                        var customer = ApiDataAccess.Customer(int.Parse(Request.QueryString["idc"]));
                        var subscription = ApiDataAccess.Subscription(customer.ReferenceCustomer);
                        var offer = ApiDataAccess.Offer(subscription.ReferenceOffer);
                        var feature = subscription.Features.FirstOrDefault(t => t.QuantityCurrent >= 1);
                        var client = ctx.online_Client.FirstOrDefault(t => t.ReferenceCustomer == customer.ReferenceCustomer);
                        if (client == null) return;
                        client.LimitDevice = feature == null ? 0 : feature.QuantityCurrent;
                        client.IsActivated = true;
                        client.SubscriptionId = subscription.Id;
                        client.Package = offer.Name;
                        ctx.SaveChanges();
                        //Helper.Login(client.EmailAddress, client.Id, client.ReferenceCustomer, client.IsAdmin);
                        Session["CLIENT_ID"] = client.Id.ToString();
                        Session["ReferenceCustomer"] = client.ReferenceCustomer;
                        Session["Administrateur"] = client.IsAdmin.ToString();
                        ClientDataAccess.LoggedInCount(client.Id, true);
                        "Votre compte est activé et vous pouvez désormais utiliser l’application.".AddCookie("AccountActivated", Response, false, DateTime.Now.AddSeconds(5));
                        Response.Redirect("Espace_client");
                    }
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["idc"]))
                {
                    using (var ctx = new NotaliaOnlineEntities())
                    {
                        var customer = ApiDataAccess.Customer(int.Parse(Request.QueryString["idc"]));
                        var subscription = ApiDataAccess.Subscription(customer.ReferenceCustomer);
                        var offer = ApiDataAccess.Offer(subscription.ReferenceOffer);
                        var feature = subscription.Features.FirstOrDefault(t => t.QuantityCurrent >= 1);
                        var client = ctx.online_Client.FirstOrDefault(t => t.ReferenceCustomer == customer.ReferenceCustomer);
                        if (client == null) return;
                        client.LimitDevice = feature != null && client.IsAdmin == true ? feature.QuantityCurrent : 1;
                        client.Package = offer.Name;
                        ctx.SaveChanges();
                        //Helper.Login(client.EmailAddress, client.Id, client.ReferenceCustomer, client.IsAdmin);
                        Session["CLIENT_ID"] = client.Id.ToString();
                        Session["ReferenceCustomer"] = client.ReferenceCustomer;
                        Session["Administrateur"] = client.IsAdmin.ToString();
                        ClientDataAccess.LoggedInCount(client.Id, true);
                        Response.Redirect("Espace_client");
                    }
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["InviteUser"]))
                {
                    divActivateAccount.Visible = true;
                    var split = Request.QueryString["InviteUser"].Split(';');
                    txtEmail.Text = split[0];
                }
            }
            catch(Exception ex)
            {
                Helper.ShowToastr(Page, ex.Message, "Error!", "error");
            }
        }

        private void CreateUserAccount(int clientId, string email, string strToken, string referenceOffer = "50a655c7-7c5d-4477-89d3-4f3c624b8241")
        {
            try
            {
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var client = ctx.online_Client.FirstOrDefault(t => t.Id == clientId && t.EmailAddress.Equals(email));
                    if (client == null) return;
                    var token = ctx.online_token.FirstOrDefault(t => t.client_id == client.Id);
                    if (token == null) return;
                    if (!token.token.Equals(strToken)) return;
                    if (client.IsActivated)
                    {
                        if (ApiDataAccess.SubscriptionExpired(client.SubscriptionId))
                        {
                            var offer = ApiDataAccess.RetrieveOffersToUpgradeCustomer(referenceOffer, client.ReferenceCustomer);
                            var href = ApiDataAccess.GetOfferLink(offer);
                            Page.ClientScript.RegisterStartupScript(GetType(), "iframeContent", "iframeContent('" + href + "');", true);
                        }
                        else
                        {
                            "Your account is already activated. You can connect now.".AddCookie("AccountActivated", Response, false, DateTime.Now.AddSeconds(5));
                            Response.Redirect("Espace_client");
                        }
                    }
                    else
                    {
                        var customer = ApiDataAccess.CustomerByEmail(email);
                        client.ReferenceCustomer = customer.ReferenceCustomer;
                        ctx.SaveChanges();
                        if (referenceOffer == "50a655c7-7c5d-4477-89d3-4f3c624b8241")
                        {
                            var offer = ApiDataAccess.Offer(referenceOffer);
                            var subscription = ApiDataAccess.Subscription(customer.ReferenceCustomer, referenceOffer);
                            client.LimitDevice = 1;
                            client.IsActivated = true;
                            client.SubscriptionId = subscription.Id;
                            client.Package = offer.Name;
                            ctx.SaveChanges();
                            //Helper.Login(client.EmailAddress, client.Id, client.ReferenceCustomer, client.IsAdmin);
                            Session["CLIENT_ID"] = client.Id.ToString();
                            Session["ReferenceCustomer"] = client.ReferenceCustomer;
                            Session["Administrateur"] = client.IsAdmin.ToString();
                            ClientDataAccess.LoggedInCount(client.Id, true);
                            "Your account is already activated. You can connect now.".AddCookie("AccountActivated", Response, false, DateTime.Now.AddSeconds(5));
                            Response.Redirect("Espace_client");
                        }
                        else
                        {
                            var offer = ApiDataAccess.RetrieveOffersToUpgradeCustomer(referenceOffer, client.ReferenceCustomer);
                            var href = ApiDataAccess.GetOfferLink(offer);
                            Page.ClientScript.RegisterStartupScript(GetType(), "iframeContent", "iframeContent('" + href + "');", true);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {

        }
    }
}