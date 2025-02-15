using NotaliaOnline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NotaliaOnline.Models;
using System.Web.Script.Serialization;
using System.IO;
using NotaliaOnline.Properties;

namespace NotaliaOnline
{
    public partial class Abonnement : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Message"] != null)
                Helper.ShowToastr(Page, Request.Cookies["Message"].Value, "Notification", "success");
            if (Session["CLIENT_ID"] != null)
            {
                var clientId = Session["CLIENT_ID"].TransformToInt();
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var client = ctx.online_Client.FirstOrDefault(obj => obj.Id == clientId);
                    if (client == null)
                        return;
                    if (string.IsNullOrEmpty(client.ReferenceCustomer))
                        return;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Subscription/" + client.SubscriptionId);
                    httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        httpResponse.Close();
                        var subscription = new JavaScriptSerializer().Deserialize<Subscription>(result);
                        var link = subscription.Links.FirstOrDefault(t => t.rel == "hosted-related-subscription");
                        var href = link == null ? "" : link.href;
                        Page.ClientScript.RegisterStartupScript(GetType(), "iframeContent", "iframeContent('" + href + "');", true);
                    }

                }
            }
        }
    }
}