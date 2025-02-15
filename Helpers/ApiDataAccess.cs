using NotaliaOnline.Models;
using NotaliaOnline.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace NotaliaOnline.Helpers
{
    public class ApiDataAccess
    {
        /// <summary>
        /// GET : customer by ReferenceCustomer
        /// </summary>
        /// <param name="referenceCustomer"></param>
        /// <returns></returns>
        public static Customer Customer(string referenceCustomer)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Customer?ReferenceCustomer=" + referenceCustomer);
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    httpResponse.Close();
                    return new JavaScriptSerializer().Deserialize<Customer>(result);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GET : customer by id
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns></returns>
        public static Customer Customer(int idCustomer)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Customer/" + idCustomer);
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var customer = new JavaScriptSerializer().Deserialize<Customer>(result);
                    httpResponse.Close();
                    return customer;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// POST: create/update customer by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Customer CustomerByEmail(string email)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Customer");
                httpRequest.Method = "POST";
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                httpRequest.ContentType = "application/json";
                var jsonString = @"{" +
                                 "'Email':'" + email +
                                 "'}";
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                }
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var customer = new JavaScriptSerializer().Deserialize<Customer>(result);
                    httpResponse.Close();
                    return customer;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GET : subscription by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Subscription Subscription(int? id)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Subscription/" + id);
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    httpResponse.Close();
                    return new JavaScriptSerializer().Deserialize<Subscription>(result);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GET : subscription by reference customer
        /// </summary>
        /// <param name="referenceCustomer"></param>
        /// <returns></returns>
        public static Subscription Subscription(string referenceCustomer)
        {
            try
            {
                return new Subscription
                {
                    TitleLocalized = "test standard subcription"
                };
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Subscription?ReferenceCustomer=" + referenceCustomer);
                //httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                //var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //    httpResponse.Close();
                //    return new JavaScriptSerializer().Deserialize<Subscription>(result);
                //}
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// POST : subscribe offer to Proabono
        /// </summary>
        /// <param name="referenceCustomer"></param>
        /// <param name="referenceOffer"></param>
        /// <returns></returns>
        public static Subscription Subscription(string referenceCustomer, string referenceOffer)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Subscription");
                httpRequest.Method = "POST";
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                httpRequest.ContentType = "application/json";
                var jsonString = @"{" +
                                 "'ReferenceCustomer':'" + referenceCustomer +
                                 "','ReferenceOffer':'" + referenceOffer +
                                 "'}";
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                }
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    httpResponse.Close();
                    return new JavaScriptSerializer().Deserialize<Subscription>(result);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void AuditLicenceChanged(int? subscriptionId, int clientId)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Subscription/" + subscriptionId);
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    httpResponse.Close();
                    var subscription = new JavaScriptSerializer().Deserialize<Subscription>(result);
                    var feature = subscription.Features.FirstOrDefault(t => t.QuantityCurrent >= 1);
                    using(var ctx = new NotaliaOnlineEntities())
                    {
                        var onlineClient = ctx.online_Client.FirstOrDefault(t => t.Id == clientId);
                        if (onlineClient == null)
                            return;
                        onlineClient.LimitDevice = feature != null && onlineClient.IsAdmin == true ? feature.QuantityCurrent : 1;
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public static Offer RetrieveOffersToUpgradeCustomer(string referenceOffer, string referenceCustomer)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest =
                    (HttpWebRequest)
                        WebRequest.Create(Resource.API_URI + "/v1/Offer?ReferenceOffer=" + referenceOffer
                                          + "&ReferenceCustomer=" + referenceCustomer
                                          + "&html=true&Upgrade=true");
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    httpResponse.Close();
                    return new JavaScriptSerializer().Deserialize<Offer>(result);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetNumberFromString(string str)
        {
            var strNumber = string.Empty;
            for (var i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]) || str[i] == '.')
                    strNumber += str[i];
            }
            if (strNumber.Substring(strNumber.Length - 1) == ".")
                strNumber = strNumber.Remove(strNumber.Length - 1, 1);
            strNumber = strNumber.Substring(0, strNumber.Length - 3);
            return strNumber;
        }

        public static string GetOfferLink(Offer offer, string rel = "")
        {
            if (offer == null)
                return "";
            var links = offer.Links.ToList();
            var link = links.FirstOrDefault();
            return link.href;
        }

        public static List<Offer> LoadOffers()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Offers?html=false&IsVisible=true&language=fr");
            httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                httpResponse.Close();
                var offers = new JavaScriptSerializer().Deserialize<Offers>(result);
                var offerList = offers.Items.OrderByDescending(t => t.Pricing).ToList();
                foreach (var offer in offerList)
                {
                    offer.UnitRecurrence = Transformer.TransformToDuration(offer.DurationRecurrence, offer.UnitRecurrence);
                    if (offer.Pricing == 0)
                    {
                        offer.TextButton = "Essayez Kopelia gratuitement";
                        foreach (var feature in offer.Features)
                        {
                            feature.PricingLocalized = "Offre gratuite pour UNE LICENCE d'utilisation et ne nécessitant pas de carte de crédit.";
                            break;
                        }
                        continue;
                    }
                    offer.TextButton = "Souscrivez maintenant";
                    foreach (var feature in offer.Features)
                    {
                        feature.PricingLocalized = "+ " + GetNumberFromString(feature.PricingLocalized) + " € HT par licence supplémentaire.";
                    }
                    offer.PricingLocalized = GetNumberFromString(offer.PricingLocalized) + " € HT";
                }
                return offerList;
            }
        }

        /// <summary>
        /// GET : offer by reference offer
        /// </summary>
        /// <param name="referenceOffer"></param>
        /// <returns></returns>
        public static Offer Offer(string referenceOffer)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest =
                    (HttpWebRequest)
                        WebRequest.Create(Resource.API_URI + "/v1/Offer?ReferenceOffer=" + referenceOffer
                                          + "&html=false&language=en");
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    httpResponse.Close();
                    return new JavaScriptSerializer().Deserialize<Offer>(result);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GET : check subscription expired
        /// </summary>
        /// <param name="suid"></param>
        /// <returns></returns>
        public static bool SubscriptionExpired(int? suid)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var httpRequest = (HttpWebRequest)WebRequest.Create(Resource.API_URI + "/v1/Subscription/" + suid);
                httpRequest.Headers["Authorization"] = Helper.BasicAuthorization();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var data = new JavaScriptSerializer().Deserialize<dynamic>(result);
                    httpResponse.Close();
                    return data["StateSubscription"].ToString() != "Running";
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}