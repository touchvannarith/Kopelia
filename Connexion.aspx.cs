using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class Connexion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDefaultButton(txtEmail, btnLogin);
            SetDefaultButton(txtPassword, btnLogin);

            if (Request.Cookies["AccountActivated"] != null)
                Helper.ShowToastr(Page, Request.Cookies["AccountActivated"].Value, "Notification", "success");
            if (string.IsNullOrEmpty(Request.QueryString["offer"]))
                divUserCreate.Visible = false;
            if (IsPostBack)
                return;
            divUser.Visible = true;
            divConfirm.Visible = divForgetPassword.Visible = false;
            var email = Request.QueryString["Email"];
            if (!string.IsNullOrEmpty(email))
            {
                txtEmail.Text = txtEmail1.Text = email;
                divUserCreate.Visible = true;
            }
        }

        private void SetDefaultButton(TextBox txt, LinkButton defaultButton)
        {
            txt.Attributes.Add("onkeydown", "defaultEnterKey(" + defaultButton.ClientID + ",event)");
        }

        private static string ConfirmUrlPayment(int clientId, string email, string token, string offer)
        {
            var root = WebConfigurationManager.AppSettings["HOST_URL"];
            return string.Format(@"{0}Confirmation_du_compte?Ref=UserCreateAccount;{1};{2};{3};{4}", root, clientId, email, token, offer);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //var deviceIdentity = hdDeviceInfo.Value + "-" + Functions.SetCookie(Request);
            var email = txtEmail.Text.Trim();
            var password = SecurePasswordHasher.Hash(txtPassword.Text.Trim());
            using (var ctx = new NotaliaOnlineEntities())
            {
                var client = ctx.online_Client.FirstOrDefault(t => t.EmailAddress == email && t.Password == password);
                if (client == null)
                {
                    Helper.ShowToastr(Page, "Veuillez renseigner un adresse email et un mot de passe valides.", "Notification", "error");
                    return;
                }
                if (client.LoggedInCount >= client.LimitDevice)
                {
                    Helper.ShowToastr(Page, "Vous êtes déjà connecté sur un autre poste de travail. Veuillez d`abord vous déconnecter de ce dernier afin de pouvoir utiliser l`application à partir de ce poste de travail.", "Notification", "error");
                    return;
                }



                //For free user : 8 days valid
                if(client.Package == "Offre d'essai" && client.CreatedDate < DateTime.Now.AddDays(-8))
                {
                    Helper.ShowToastr(Page, "Votre offre gratuite est expirée. Vous devez maintenant souscrire à notre offre.", "Notification", "error");
                    return;
                }



                ApiDataAccess.AuditLicenceChanged(client.SubscriptionId, client.Id);
                var nextUrl = HttpContext.Current.Request.Cookies["NextUrl"] == null ? "/" : HttpContext.Current.Request.Cookies["NextUrl"].Value;
                ClientDataAccess.LoggedInCount(client.Id, true);
                Session["CLIENT_ID"] = client.Id.ToString();
                Session["ReferenceCustomer"] = "refcustomer"; //client.ReferenceCustomer;
                Session["Administrateur"] = client.IsAdmin.ToString();
                Response.Redirect(nextUrl, false);
                return;



                var token = ctx.online_token.FirstOrDefault(t => t.client_id == client.Id);
                if (token == null) return;
                if (!client.IsActivated)
                {
                    var emailBody = string.Format(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <p>Bonjour,
                                <br><br>
                                Vous venez de créer un compte et toute l’équipe de Kopelia vous en remercie.
                                <br><br>   
                                Afin de confirmer la création de ce compte, veuillez cliquer sur cette URL de vérification de votre adresse email : <a href='{0}'>URL d’activation</a>
                                <br><br>   
                                Merci de votre confiance.
                                <br><br>      
                                L’équipe de Kopelia.
                                </p><br>
                            </body>
                        </html>", ConfirmUrlPayment(client.Id, client.EmailAddress.Trim(), token.token, Request.QueryString["offer"]));
                    EmailService.SendEmail(txtEmail.Text.Trim(), "Kopelia – Création de compte", emailBody);
                    lblMessage.InnerText = "Un email de confirmation vient de vous être envoyé. Veuillez cliquer sur le lien d’activation reçu pour activer votre compte.";
                    divConfirm.Visible = true;
                    divUser.Visible = false;
                    return;
                }
                else
                {
                    var customer = ApiDataAccess.Customer(client.ReferenceCustomer);
                    var subscription = ApiDataAccess.Subscription(client.SubscriptionId);
                    if (customer == null || subscription == null)
                    {
                        client.IsActivated = false;
                        ctx.SaveChanges();
                        var emailBody = string.Format(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <p>Bonjour,
                                <br><br>
                                Vous venez de créer un compte et toute l’équipe de Kopelia vous en remercie.
                                <br><br>   
                                Afin de confirmer la création de ce compte, veuillez cliquer sur cette URL de vérification de votre adresse email : <a href='{0}'>URL d’activation</a>
                                <br><br>   
                                Merci de votre confiance.
                                <br><br>      
                                L’équipe de Kopelia.
                                </p><br>
                            </body>
                        </html>", ConfirmUrlPayment(client.Id, client.EmailAddress.Trim(), token.token, Request.QueryString["offer"]));
                        EmailService.SendEmail(txtEmail.Text.Trim(), "Kopelia – Création de compte", emailBody);
                        lblMessage.InnerText = "Un email de confirmation vient de vous être envoyé. Veuillez cliquer sur le lien d’activation reçu pour activer votre compte.";
                        divConfirm.Visible = true;
                        divUser.Visible = false;
                        return;
                    }
                    else
                    {
                        if (subscription.IsTrial)
                        {
                            if (subscription.StateSubscription != "Running")
                            {
                                if (!string.IsNullOrEmpty(Request.QueryString["offer"]))
                                    Response.Redirect(string.Format(@"Confirmation_du_compte?Ref=UserCreateAccount;{0};{1};{2};{3}", client.Id, client.EmailAddress.Trim(), token.token, Request.QueryString["offer"]));
                                "Votre offre gratuite est expirée. Vous devez maintenant souscrire à notre offre.".AddCookie("SubscriptionExpired", Response, false, DateTime.Now.AddSeconds(5));
                                Response.Redirect("Tarifs");
                            }
                            //if (client.DeviceIdentity != deviceIdentity)
                            //{
                            //    "2".AddCookie("Message", Response, false, DateTime.Now.AddSeconds(5));
                            //    Response.Redirect("Confirmation");
                            //}
                        }
                        else
                        {
                            if (subscription.StateSubscription != "Running")
                            {
                                if (!string.IsNullOrEmpty(Request.QueryString["offer"]))
                                    Response.Redirect(string.Format(@"Confirmation_du_compte?Ref=UserCreateAccount;{0};{1};{2};{3}", client.Id, client.EmailAddress.Trim(), token.token, Request.QueryString["offer"]));
                                "Votre offre est expirée, vous devez souscrire à nouveau.".AddCookie("SubscriptionExpired", Response, false, DateTime.Now.AddSeconds(5));
                                Response.Redirect("Tarifs");
                            }
                            //if (client.DeviceIdentity != deviceIdentity)
                            //{
                            //    "3".AddCookie("Message", Response, false, DateTime.Now.AddSeconds(5));
                            //    Response.Redirect("Confirmation");
                            //}
                        }
                    }
                    //ApiDataAccess.AuditLicenceChanged(client.SubscriptionId, client.Id);
                    //var nextUrl = HttpContext.Current.Request.Cookies["NextUrl"] == null ? "/" : HttpContext.Current.Request.Cookies["NextUrl"].Value;
                    //ClientDataAccess.LoggedInCount(client.Id, true);
                    //Session["CLIENT_ID"] = client.Id.ToString();
                    //Session["ReferenceCustomer"] = client.ReferenceCustomer;
                    //Session["Administrateur"] = client.IsAdmin.ToString();
                    //Response.Redirect(nextUrl, false);
                }
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                using(var ctx = new NotaliaOnlineEntities())
                {
                    var client = ctx.online_Client.FirstOrDefault(t => t.EmailAddress.Trim().Equals(txtEmail1.Text.Trim()));
                    if (client != null)
                    {
                        Helper.ShowToastr(Page, "Cette adresse email est déjà utilisée.", "Notification", "success");
                        return;
                    }
                    client = new online_Client
                    {
                        Login = txtEmail1.Text.Trim(),
                        EmailAddress = txtEmail1.Text.Trim(),
                        Password = SecurePasswordHasher.Hash(txtPassword1.Text.Trim()),
                        PhoneNumber = txtTelephone.Text.Trim(),
                        IsActivated = false,
                        LimitDevice = 0,
                        IsAdmin = true,
                        CreatedDate = DateTime.Now,
                        LoggedInCount = 0,
                        //DeviceIdentity = hdDeviceInfo.Value + "-" + Functions.SetCookie(Request),
                    };
                    var transaction = ctx.Database.BeginTransaction();
                    ctx.online_Client.Add(client);
                    ctx.SaveChanges();

                    var token = new online_token
                    {
                        token = Guid.NewGuid().ToString(),
                        createt_at = DateTime.Now,
                        is_expired = false,
                        is_past = false,
                        updated_at = DateTime.Now,
                        is_deleted = false,
                        expired_at = DateTime.Now.AddHours(1),
                        client_id = client.Id
                    };
                    ctx.online_token.Add(token);
                    ctx.SaveChanges();
                    transaction.Commit();

                    var emailBody = string.Format(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <p>Bonjour,
                                <br><br>
                                Vous venez de créer un compte et toute l’équipe de Kopelia vous en remercie.
                                <br><br>   
                                Afin de confirmer la création de ce compte, veuillez cliquer sur cette URL de vérification de votre adresse email : <a href='{0}'>URL d’activation</a>
                                <br><br>   
                                Merci de votre confiance.
                                <br><br>      
                                L’équipe de Kopelia.
                                </p><br>
                            </body>
                        </html>", ConfirmUrlPayment(client.Id, client.EmailAddress.Trim(), token.token, Request.QueryString["offer"]));
                    EmailService.SendEmail(txtEmail1.Text.Trim(), "Kopelia – Création de compte", emailBody);
                    divConfirm.Visible = true;
                    divUser.Visible = false;
                    lblMessage.InnerText = "Un email de confirmation vient de vous être envoyé. Veuillez cliquer sur le lien d’activation reçu pour activer votre compte.";
                }
            }
            catch (Exception exception)
            {
                Helper.ShowToastr(Page, exception.Message, "Notification", "error");
            }
        }

        protected void btnEnvoyer_Click(object sender, EventArgs e)
        {
            try
            {
                var email = txtEmailAddress.Text.Trim();
                if(string.IsNullOrEmpty(email))
                {
                    Helper.ShowToastr(Page, "Veuillez entrer une adresse e-mail.", "Connexion échouée", "error");
                    return;
                }
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var client = ctx.online_Client.FirstOrDefault(t => t.EmailAddress == email);
                    if(client == null)
                    {
                        Helper.ShowToastr(Page, "Veuillez renseigner l’adresse e-mail correspondant à l’utilisateur.", "Connexion échouée", "error");
                        return;
                    }
                    var emailBody = string.Format(@"
                    <html>
                    <head>
                    </head>
                        <body>
                            <p>Bonjour,<br><br> 
                            Voici les informations relatives à votre compte : <br>
                                <ul>
                                    <li>
                                        ADRESSE E-MAIL : <a href='mailto:{0}'>{0}</a>
                                    </li>
                                    <li>
                                        MOT DE PASSE : {1}
                                    </li>
                                </ul>
                                <br>
                                Merci de votre confiance.
                                <br><br>
                                L’équipe de Kopelia.
                            </p><br>
                        </body>
                    </html>", email, SecurePasswordHasher.Decrypt(client.Password));
                    EmailService.SendEmail(email, "Kopelia – Mot de passe perdu", emailBody);
                    Helper.ShowToastr(Page, "Un e-mail vous a été envoyé avec les informations de votre compte.", "Mot de passe perdu", "success");
                    divUser.Visible = true;
                    divForgetPassword.Visible = false;
                }
            }
            catch (Exception exception)
            {
                Helper.ShowToastr(Page, exception.Message, "Notification", "error");
            }
        }

        protected void linkForgetPassword_ServerClick(object sender, EventArgs e)
        {
            divUser.Visible = divConfirm.Visible = false;
            divForgetPassword.Visible = true;
        }
    }
}