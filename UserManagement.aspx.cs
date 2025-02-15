using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class UserManagement : Page
    {
        readonly List<ClientModel> _Clients = new List<ClientModel>();
        private int iPageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["administrator"] == null)
            {
                Response.Redirect("Administrator_login");
            }
            if (!IsPostBack)
            {
                ClientList(txtEmail.Text);
                CalculezMaintenantList();
            }
        }

        private void ClientList(string strEmail = "")
        {
            var clients = ClientDataAccess.GetClients(strEmail).Where(t => t.IsAdmin == true).ToList();
            foreach (var client in clients)
            {
                var subClients = ClientDataAccess.GetClients(client.Id).ToList();
                _Clients.Add(new ClientModel
                {
                    Client = client,
                    SubClients = subClients
                });
            }
            //var totalRecord = _Clients.Count;
            //const int pageSize = 20;
            //var startRow = pageIndex * pageSize;
            //repeatClient.DataSource = _Clients.Skip(startRow).Take(pageSize).ToList();
            //repeatClient.DataBind();
            //BindPager(totalRecord, pageIndex, pageSize, rptPager);
            var pdsData = new PagedDataSource();
            var dv = new DataView(clients.TranformToDataTable());
            pdsData.DataSource = dv;
            pdsData.AllowPaging = true;
            pdsData.PageSize = iPageSize;
            if (ViewState["rptPager1PageNumber"] != null)
                pdsData.CurrentPageIndex = Convert.ToInt32(ViewState["rptPager1PageNumber"]);
            else
                pdsData.CurrentPageIndex = 0;
            if (pdsData.PageCount > 1)
            {
                rptPager2.Visible = true;
                ArrayList alPages = new ArrayList();
                for (int i = 1; i <= pdsData.PageCount; i++)
                    alPages.Add((i).ToString());
                rptPager1.DataSource = alPages;
                rptPager1.DataBind();
            }
            else
            {
                rptPager2.Visible = false;
            }
            repeatClient.DataSource = pdsData;
            repeatClient.DataBind();
        }

        private void CalculezMaintenantList()
        {
            var list = CalculezMaintenantDataAccess.GetCalculezMaintenants();
            //var totalRecord = list.Count;
            //const int pageSize = 5;
            //var startRow = pageIndex * pageSize;
            //repeatCalculezMaintenant.DataSource = list.Skip(startRow).Take(pageSize).ToList();
            //repeatCalculezMaintenant.DataBind();
            //BindPager(totalRecord, pageIndex, pageSize, rptPager2);
            var pdsData = new PagedDataSource();
            var dv = new DataView(list.TranformToDataTable());
            pdsData.DataSource = dv;
            pdsData.AllowPaging = true;
            pdsData.PageSize = iPageSize;
            if (ViewState["rptPager2PageNumber"] != null)
                pdsData.CurrentPageIndex = Convert.ToInt32(ViewState["rptPager2PageNumber"]);
            else
                pdsData.CurrentPageIndex = 0;
            if (pdsData.PageCount > 1)
            {
                rptPager2.Visible = true;
                ArrayList alPages = new ArrayList();
                for (int i = 1; i <= pdsData.PageCount; i++)
                    alPages.Add((i).ToString());
                rptPager2.DataSource = alPages;
                rptPager2.DataBind();
            }
            else
            {
                rptPager2.Visible = false;
            }
            repeatCalculezMaintenant.DataSource = pdsData;
            repeatCalculezMaintenant.DataBind();
        }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClientList(txtEmail.Text);
            CalculezMaintenantList();
        }

        protected void btnUploadLogo_Click(object sender, EventArgs e)
        {
            try
            {
                var id = hdClientId.Value;
                if (!fileUpload.HasFile) return;
                var extension = System.IO.Path.GetExtension(fileUpload.FileName);
                if (extension == null)
                    return;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                {
                    var filename = System.IO.Path.GetFileName(fileUpload.FileName);
                    using (var db = new NotaliaOnlineEntities())
                    {
                        var clientId = Convert.ToInt16(id);
                        var client = db.online_Client.FirstOrDefault(t => t.Id == clientId);
                        if (client == null)
                            return;
                        var logoName = client.Id + "_" + filename;
                        fileUpload.SaveAs(Request.PhysicalApplicationPath + "images/logo/" + logoName);
                        client.ImageLogo = logoName;
                        db.SaveChanges();
                        Helper.ShowToastr(Page, @"Téléchargement réussi", "Notification", "success");
                    }
                }
                ClientList(txtEmail.Text);
            }
            catch (Exception)
            {
                Helper.ShowToastr(Page, "Téléchargement d'erreur", "Notification", "error");
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                var intClientId = int.Parse(hdOwnerId.Value);
                ClientDataAccess.Remove(intClientId);
                var list = ClientDataAccess.GetClients(intClientId);
                ClientDataAccess.RemoveRange(list);
                ClientList(txtEmail.Text);
                Helper.ShowToastr(Page, @"All licences for owner have been removed.", "Notification", "success");
            }
            catch (Exception ex)
            {
                Helper.ShowToastr(Page, ex.Message, "Error", "error");
            }
        }

        protected void repeatClient_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var repeater = (Repeater)e.Item.FindControl("repeatUser");
                repeater.DataSource = _Clients[e.Item.ItemIndex].SubClients;
                repeater.DataBind();
            }
        }

        protected void rptPager2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["rptPager2PageNumber"] = Convert.ToInt32(e.CommandArgument) - 1;
            CalculezMaintenantList();
        }

        protected void rptPager1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["rptPager1PageNumber"] = Convert.ToInt32(e.CommandArgument) - 1;
            ClientList(txtEmail.Text);
        }

        protected void btnRemoveSubClient_Click(object sender, EventArgs e)
        {
            try
            {
                var type = (LinkButton)sender;
                var child = type.FindControl("hdRemoveId");
                var id = int.Parse(((HiddenField)child).Value);
                ClientDataAccess.Remove(id);
                ClientList(txtEmail.Text);
                Helper.ShowToastr(Page, @"Licence for user has been removed.", "Notification", "success");
            }
            catch (Exception ex)
            {
                Helper.ShowToastr(Page, ex.Message, "Error", "error");
            }

        }
    }

    public class ClientModel
    {
        public online_Client Client { get; set; }
        public List<online_Client> SubClients { get; set; }
    }
}