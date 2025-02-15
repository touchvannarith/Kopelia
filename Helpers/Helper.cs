using NotaliaOnline.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace NotaliaOnline.Helpers
{
    public static class Helper
    {
        public static string BasicAuthorization()
        {
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Resource.AGENT_KEY + ":" + Resource.API_KEY));
        }

        public static void AddCookie(this string value, string key, HttpResponse response, bool httponly = true, DateTime expired = default(DateTime))
        {
            if (expired == default(DateTime)) expired = DateTime.MaxValue; //DateTime.Now.AddHours(2);
            var cookie = response.Cookies[key];
            if (cookie != null) response.Cookies.Remove(key);
            cookie = new HttpCookie(key, value) { Expires = expired, HttpOnly = httponly };
            response.Cookies.Add(cookie);
        }

        public static bool AuthChecked()
        {
            var cook = HttpContext.Current.Request.Cookies["AuthChecked"];
            var authchecked = cook != null && cook.Value == "1";
            return authchecked;
        }

        public static void Login(string userEmail, int userId, string referenceCustomer, bool? isAdmin)
        {
            "1".AddCookie("AuthChecked", HttpContext.Current.Response, false);
            userEmail.AddCookie("LOGIN", HttpContext.Current.Response, false);
            userId.ToString().AddCookie("CLIENTID", HttpContext.Current.Response, false);
            referenceCustomer.AddCookie("ReferenceCustomer", HttpContext.Current.Response, false);
            isAdmin.ToString().AddCookie("Administrateur", HttpContext.Current.Response, false);
        }

        public static void ShowToastr(Page page, string message, string title, string type)
        {
            switch (type)
            {
                case "success":
                    page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message", @"toastr.success('" + message + "', '" + title + "', {timeOut: 5000});", true);
                    break;
                case "error":
                    page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message", @"toastr.error('" + message + "', '" + title + "', {timeOut: 5000});", true);
                    break;
                default:
                    break;
            }
        }
    }
}