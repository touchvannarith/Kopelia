using NotaliaOnline.Helpers;
using NotaliaOnline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class Tarifs : Page
    {
        private List<Offer> _offerList = new List<Offer>();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("NextUrl", "Abonnement") { HttpOnly = false });
            if (Request.Cookies["AccountActivated"] != null)
                Helper.ShowToastr(Page, Request.Cookies["AccountActivated"].Value, "Notification", "success");
            if (Request.Cookies["SubscriptionExpired"] != null)
                Helper.ShowToastr(Page, Request.Cookies["SubscriptionExpired"].Value, "Notification", "error");

            divIframe.Visible = false; //divBillingInformation.Visible = divConfirmMessage.Visible = false;
            divOffer.Visible = divOfferMobile.Visible = true;
            _offerList = ApiDataAccess.LoadOffers();
            repeatOffer.DataSource =
                        Repeater1.DataSource =
                            Repeater2.DataSource =
                                Repeater3.DataSource =
                                    Repeater4.DataSource =
                                        Repeater5.DataSource =
                                            Repeater6.DataSource =
                                                Repeater7.DataSource =
                                                    Repeater8.DataSource = Repeater9.DataSource = RepeaterTitle1.DataSource = RepeaterTitle2.DataSource = _offerList;
            repeatOffer.DataBind();
            Repeater1.DataBind();
            Repeater2.DataBind();
            Repeater3.DataBind();
            Repeater4.DataBind();
            Repeater5.DataBind();
            Repeater6.DataBind();
            Repeater7.DataBind();
            Repeater8.DataBind();
            Repeater9.DataBind();
            RepeaterTitle1.DataBind();
            RepeaterTitle2.DataBind();
            repeatOfferHeader.DataSource = _offerList;
            repeatOfferHeader.DataBind();
            repeatOfferFooter.DataSource = _offerList;
            repeatOfferFooter.DataBind();
        }

        protected void repeatOffer_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var repeater = (Repeater)e.Item.FindControl("repeatFeature");
                repeater.DataSource = _offerList[e.Item.ItemIndex].Features;
                repeater.DataBind();
            }
        }

        protected void btnSouscrivez_OnServerClick(object sender, EventArgs e)
        {
            try
            {
                var type = (HtmlButton)sender;
                var child = type.FindControl("hdReferenceOffer");
                var referenceOffer = ((HiddenField)child).Value;


                if (referenceOffer != "50a655c7-7c5d-4477-89d3-4f3c624b8241")
                    return;


                if (Session["CLIENT_ID"] == null)
                    Response.Redirect("Connexion?offer=" + ((HiddenField)child).Value);
                else
                {
                    var offer = ApiDataAccess.RetrieveOffersToUpgradeCustomer(referenceOffer, Session["ReferenceCustomer"].ToString());
                    if (referenceOffer == "50a655c7-7c5d-4477-89d3-4f3c624b8241")
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Il n`est pas possible d`activer l`offre d`essai lorsque une Offre Standard a été activée.', 'Notification', {timeOut: 5000});", true);
                        return;
                    }
                    var href = ApiDataAccess.GetOfferLink(offer);
                    Page.ClientScript.RegisterStartupScript(GetType(), "iframeContent", "iframeContent('" + href + "');", true);
                    divIframe.Visible = true;
                    divOffer.Visible = divOfferMobile.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}