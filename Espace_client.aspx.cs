using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using NotaliaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class Espace_client : Page
    {
        online_Client _client = new online_Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CLIENT_ID"] == null)
                return; //Response.Redirect("/Connexion");
            var id = Session["CLIENT_ID"].TransformToInt();
            _client = ClientDataAccess.GetClient(id);
            ApiDataAccess.AuditLicenceChanged(_client.SubscriptionId, _client.Id);
            if (IsPostBack) return;
            BindClientInformation(_client);
            BindUserManagement(_client);
            //BindSimulationList(0, _client);
            //BindSimulationArchiveList(0, _client);
        }

        private void BindClientInformation(online_Client client)
        {
            var pwd = SecurePasswordHasher.Decrypt(client.Password.Trim()).Trim();
            txt_MOT_DE_PASSE.Attributes.Add("value", pwd);
            txt_CONFIRMATION_MOT_DE_PASSE.Attributes.Add("value", pwd);
            txt_TÉLÉPHONE.Value = client.PhoneNumber;
            txt_ADRESSE_EMAIL.Value = client.EmailAddress.Trim();
        }

        private void BindUserManagement(online_Client client)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                if (client.IsAdmin != true)
                    return;
                if (client.Package == "Offre d'essai")
                {
                    divUserManagement.Visible = false;
                    return;
                }
                divUserManagement.Visible = true;
                var lstDevice = new List<Device>();
                var i = 0;
                lstDevice.Add(new Device()
                {
                    No = i,
                    Id = client.Id,
                    DeviceNumber = client.DeviceIdentity,
                    Email = client.EmailAddress,
                    UserRole = "Administrateur"
                });
                RepeaterDeviceForOwner.DataSource = lstDevice;
                RepeaterDeviceForOwner.DataBind();
                lstDevice = new List<Device>();
                var lstDeiviceUser = ctx.online_Client.Where(t => t.GroupId == client.Id).ToList();
                foreach (var each in lstDeiviceUser)
                {
                    i += 1;
                    lstDevice.Add(new Device()
                    {
                        No = i,
                        Id = each.Id,
                        DeviceNumber = each.DeviceIdentity,
                        Email = each.EmailAddress,
                        UserRole = "Utilisateur"
                    });
                }
                RepeaterDeviceForUser.DataSource = lstDevice;
                RepeaterDeviceForUser.DataBind();
                var intTotalLicenceActivated = lstDevice.Count(t => !string.IsNullOrEmpty(t.DeviceNumber)) + 1;
                var intTotalLicence = client.LimitDevice;
                lblTotalLicense.InnerText = intTotalLicence > 1
                    ? "Vous possédez " + intTotalLicence + " licences."
                    : "Vous possédez une licence.";
                var intLicenceRemain = intTotalLicence - intTotalLicenceActivated;
                lblLicenseActivated.InnerText = intTotalLicenceActivated > 1
                    ? intTotalLicenceActivated + " licences sont activées et " + (intLicenceRemain > 1 ? intLicenceRemain + " licences sont disponibles." : intLicenceRemain + " licence est disponible.")
                    : intTotalLicenceActivated + " licence est activée et " + (intLicenceRemain > 1 ? intLicenceRemain + " licences sont disponibles." : intLicenceRemain + " licence est disponible.");
                divAllLicensesActivated.Visible = intLicenceRemain == 0;
                btnInviteUser.Visible = intLicenceRemain > 0;
            }
        }

        //private void BindSimulationList(int pageIndex, online_Client client)
        //{
        //    var simulationList = SimulationActeDataAccess.GetSimulationActes(client.Id, false);
        //    foreach (var each in simulationList)
        //    {
        //        each.AllowDelete = each.ClientId == client.Id;
        //    }
        //    var totalRecord = simulationList.Count;
        //    const int pageSize = 10;
        //    var startRow = pageIndex * pageSize;
        //    repeatSimulationActe.DataSource = simulationList.Skip(startRow).Take(pageSize).ToList();
        //    repeatSimulationActe.DataBind();
        //    BindPager(totalRecord, pageIndex, pageSize, rptPager);
        //}

        #region WebMethod

        [WebMethod]
        public static List<vw_online_Simulation> GetSimulationActe(int clientId, bool archive, int pageIndex)
        {
            var simulationList = SimulationActeDataAccess.GetSimulationActes(clientId, archive);
            foreach (var each in simulationList)
            {
                each.AllowDelete = each.ClientId == clientId;
            }
            return simulationList;
            //var totalRecord = simulationList.Count;
            //const int pageSize = 10;
            //var startRow = pageIndex * pageSize;
            //repeatSimulationActe.DataSource = simulationList.Skip(startRow).Take(pageSize).ToList();
            //repeatSimulationActe.DataBind();
            //BindPager(totalRecord, pageIndex, pageSize, rptPager);
        }

        [WebMethod]
        public static string Archive(int simulationId)
        {
            SimulationActeDataAccess.Archive(simulationId, true);
            return "";
        }

        [WebMethod]
        public static string Restart(int simulationId)
        {
            SimulationActeDataAccess.Archive(simulationId, false);
            return "";
        }

        [WebMethod]
        public static string Remove(int simulationId)
        {
            SimulationActeDataAccess.Remove(simulationId);
            return "";
        }
        #endregion WebMethod


        //private void BindSimulationArchiveList(int pageIndex, online_Client client)
        //{
        //    var simulationList = SimulationActeDataAccess.GetSimulationActes(client.Id, true);
        //    foreach (var each in simulationList)
        //    {
        //        each.AllowDelete = each.ClientId == client.Id;
        //    }
        //    var totalRecord = simulationList.Count;
        //    const int pageSize = 10;
        //    var startRow = pageIndex * pageSize;
        //    repeatSimulationActeArchive.DataSource = simulationList.Skip(startRow).Take(pageSize).ToList();//simulationList.ToList();
        //    repeatSimulationActeArchive.DataBind();
        //    BindPager(totalRecord, pageIndex, pageSize, rptPager2);
        //}

        private static void BindPager(int totalRecordCount, int currentPageIndex, int pageSize, Repeater rpt)
        {
            var getPageCount = (double)((decimal)totalRecordCount / (decimal)pageSize);
            var pageCount = (int)Math.Ceiling(getPageCount);
            var pages = new List<ListItem>();
            if (pageCount > 1)
            {
                pages.Add(new ListItem("FIRST", "1", currentPageIndex > 0));

                //Add The Previous Button
                if (currentPageIndex >= 1)
                {
                    pages.Add(new ListItem("PREV", (currentPageIndex).ToString()));
                }

                for (var i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPageIndex + 1));
                }

                //Add The Next Button
                if (currentPageIndex <= pageCount - 2)
                {
                    pages.Add(new ListItem("NEXT", (currentPageIndex + 2).ToString()));
                }

                pages.Add(new ListItem("LAST", pageCount.ToString(), currentPageIndex < pageCount - 1));
            }
            rpt.DataSource = pages;
            rpt.DataBind();
        }

        private void RemoveUser(int intId)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                var objClient = ctx.online_Client.FirstOrDefault(t => t.Id == intId);
                if (objClient == null)
                    return;
                var objToken = ctx.online_token.FirstOrDefault(t => t.client_id == objClient.Id);
                if (objToken != null)
                    ctx.online_token.Remove(objToken);
                ctx.online_Client.Remove(objClient);
                var simulations = ctx.online_SIMULATION_ACTE.Where(t => t.ClientId == intId);
                foreach (var each in simulations)
                {
                    each.Libelle = each.Libelle + " (créée par " + objClient.EmailAddress.Trim() + ")";
                    each.ClientId = objClient.GroupId;
                }
                ctx.SaveChanges();
            }
        }

        private static string ConfirmUrl(string email, int groupClientId)
        {
            return string.Format(@"{0}Confirmation_du_compte?InviteUser={1}", WebConfigurationManager.AppSettings["HOST_URL"], email + ";" + groupClientId);
        }

        private static bool VerifyHasChanged(OnlineClient clientNew, OnlineClient clientOld)
        {
            return !clientNew.LOGIN.Equals(clientOld.LOGIN, StringComparison.InvariantCultureIgnoreCase) ||
                   !clientNew.MOT_DE_PASSE.Equals(clientOld.MOT_DE_PASSE, StringComparison.CurrentCultureIgnoreCase) ||
                   !clientNew.TÉLÉPHONE.Equals(clientOld.TÉLÉPHONE, StringComparison.CurrentCultureIgnoreCase) ||
                   !clientNew.ADRESSE_EMAIL.Equals(clientOld.ADRESSE_EMAIL, StringComparison.CurrentCultureIgnoreCase);
        }

        protected void btnDeleteDevice_ServerClick(object sender, EventArgs e)
        {
            if (_client.IsAdmin == false)
            {
                Helper.ShowToastr(Page, "Permission refusée", "Opération a échoué", "error");
                Page.ClientScript.RegisterStartupScript(GetType(), "Do-click", "$('#collapse2').click();", true);
                return;
            }
            var type = (HtmlButton)sender;
            var child = type.FindControl("hdId");
            var intId = int.Parse(((HiddenField)child).Value);
            RemoveUser(intId);
            BindUserManagement(_client);
        }

        protected void btnVoir_ServerClick(object sender, EventArgs e)
        {
            var type = (HtmlButton)sender;
            var child = type.FindControl("hiddenVoir");
            var hdValue = ((HiddenField)child).Value;
            Response.Redirect(hdValue);
        }

        protected void btnArchiver_ServerClick(object sender, EventArgs e)
        {
            var type = (HtmlButton)sender;
            var child = type.FindControl("hiddenId");
            var id = ((HiddenField)child).Value.TransformToInt();
            using (var ctx = new NotaliaOnlineEntities())
            {
                var clientAct = ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == id);
                if (clientAct == null)
                    return;
                clientAct.Archive = true;
                ctx.SaveChanges();
                //BindSimulationList(0, _client);
                //BindSimulationArchiveList(0, _client);
            }
            Helper.ShowToastr(Page, @"Archivage effectué avec succès", "Notification", "success");
        }

        protected void btnSupprimer_ServerClick(object sender, EventArgs e)
        {
            var type = (HtmlButton)sender;
            var child = type.FindControl("hiddenDeleteId");
            var intId = ((HiddenField)child).Value.TransformToInt();
            using (var ctx = new NotaliaOnlineEntities())
            {
                var objSimulation = ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == intId);
                if (objSimulation != null) ctx.online_SIMULATION_ACTE.Remove(objSimulation);
                ctx.SaveChanges();
                //BindSimulationList(0, _client);
                //BindSimulationArchiveList(0, _client);
            }
            Helper.ShowToastr(Page, @"Suppression terminée avec succès", "Notification","success");
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            var pageIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);
            //BindSimulationList(pageIndex - 1, _client);
        }

        protected void btnRestaurer_ServerClick(object sender, EventArgs e)
        {
            var type = (HtmlButton)sender;
            var child = type.FindControl("hiddenId");
            var id = ((HiddenField)child).Value.TransformToInt();
            using (var ctx = new NotaliaOnlineEntities())
            {
                var clientAct = ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == id);
                if (clientAct == null)
                    return;
                clientAct.Archive = false;
                ctx.SaveChanges();
                //BindSimulationList(0, _client);
                //BindSimulationArchiveList(0, _client);
            }
            Helper.ShowToastr(Page, @"Restauration effectuée avec succès", "Notification", "success");
        }

        protected void lnkPage2_Click(object sender, EventArgs e)
        {
            var pageIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);
            //BindSimulationArchiveList(pageIndex - 1, _client);
        }

        protected void btnEnvoyerModal_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var splitEmail = txtEmail.Text.Trim().Split(';');
                    if ((ctx.online_Client.Count(t => t.GroupId == _client.Id) + splitEmail.Count(t => t != "") + 1) > (_client.LimitDevice))
                    {
                        Helper.ShowToastr(Page, "Licence is not enough.", "Notification", "error");
                        return;
                    }
                    for (var i = 0; i < splitEmail.Count(); i++)
                    {
                        var email = splitEmail[i];
                        if (string.IsNullOrEmpty(email))
                            continue;
                        var token = new online_token
                        {
                            token = Guid.NewGuid().ToString(),
                            createt_at = DateTime.Now,
                            is_expired = false,
                            is_past = false,
                            updated_at = DateTime.Now,
                            is_deleted = false,
                            expired_at = DateTime.Now.AddHours(1)
                        };
                        var client = new online_Client
                        {
                            Login = email,
                            Password = SecurePasswordHasher.Hash("1"),
                            PhoneNumber = "",
                            EmailAddress = email,
                            IsActivated = false,
                            LimitDevice = 1,
                            ReferenceCustomer = _client.ReferenceCustomer,
                            SubscriptionId = _client.SubscriptionId,
                            IsAdmin = false,
                            CreatedDate = DateTime.Now,
                            Package = _client.Package,
                            GroupId = _client.Id
                        };
                        ctx.online_Client.Add(client);
                        ctx.online_token.Add(token);
                        ctx.SaveChanges();
                        var subject = "Kopelia – Vous avez été invité à utiliser l'application Kopelia";
                        var body = string.Format(@"
                            <html>
                            <head>
                            </head>
                                <body>
                                    <p>Bonjour,          
                                    <br><br>
                                    {0} vous a invité à utiliser KOPELIA.<br><br>
                                    Acceptez cette invitation en cliquant sur lien ci-dessous :<br>
                                    <a href='{1}'>Lien de confirmation</a>
                                    <br><br>     
                                    L’équipe Notalia.
                                    </p><br>
                                </body>
                            </html>", txt_ADRESSE_EMAIL.Value.Trim(), ConfirmUrl(email, client.Id));
                        EmailService.SendEmail(email, subject, body);
                    }
                }
                Helper.ShowToastr(Page, "L`invitation a été envoyée", "Notification", "success");
                BindUserManagement(_client);
            }
            catch (Exception exception)
            {
                Helper.ShowToastr(Page, exception.Message, "Notification", "error");
            }
        }

        protected void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var clientNew = new OnlineClient
                    {
                        LOGIN = txt_ADRESSE_EMAIL.Value.Trim(),
                        CLIENTID = _client.Id,
                        TÉLÉPHONE = txt_TÉLÉPHONE.Value.Trim(),
                        ADRESSE_EMAIL = txt_ADRESSE_EMAIL.Value.Trim(),
                        MOT_DE_PASSE = txt_MOT_DE_PASSE.Text.Trim(),
                        CONFIRMATION_MOT_DE_PASSE = txt_CONFIRMATION_MOT_DE_PASSE.Text.Trim()
                    };
                    var objClient = ctx.online_Client.Find(clientNew.CLIENTID);
                    if (objClient != null)
                    {
                        var modified = VerifyHasChanged(clientNew, new OnlineClient
                        {
                            LOGIN = objClient.Login.Trim(),
                            ADRESSE_EMAIL = objClient.EmailAddress.Trim(),
                            CLIENTID = objClient.Id,
                            MOT_DE_PASSE = SecurePasswordHasher.Decrypt(objClient.Password.Trim()).Trim(),
                            TÉLÉPHONE = objClient.PhoneNumber
                        });
                        if (modified)
                        {
                            var oldEmail = objClient.EmailAddress;
                            objClient.Login = clientNew.LOGIN;
                            objClient.EmailAddress = clientNew.ADRESSE_EMAIL;
                            objClient.Password = SecurePasswordHasher.Hash(clientNew.MOT_DE_PASSE);
                            objClient.PhoneNumber = clientNew.TÉLÉPHONE;
                            ctx.SaveChanges();
                            //Response.Redirect(@"/Clear_User");
                        }
                    }
                }
                Helper.ShowToastr(Page, "", "Modification réussie", "success");
            }
            catch (Exception ex)
            {
                Helper.ShowToastr(Page, ex.Message, "Error!", "error");
            }
        }
    }
}