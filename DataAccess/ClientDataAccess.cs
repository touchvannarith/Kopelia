using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.DataAccess
{
    public static class ClientDataAccess
    {
        public static online_Client GetClient(int id)
        {
            using(var ctx = new NotaliaOnlineEntities())
            {
                return ctx.online_Client.FirstOrDefault(t=>t.Id == id);
            }
        }

        public static List<online_Client> GetClients(string strEmail)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                return ctx.online_Client.Where(t => t.EmailAddress.Contains(strEmail)).ToList();
            }
        }

        public static List<online_Client> GetClients(int intGroupId)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                return ctx.online_Client.Where(t => t.GroupId == intGroupId).ToList();
            }
        }

        public static void Remove(int id)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                var obj = ctx.online_Client.FirstOrDefault(t => t.Id == id);
                if (obj == null) return;
                ctx.online_Client.Remove(obj);
                ctx.SaveChanges();
            }
        }

        public static void RemoveRange(List<online_Client> clients)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                ctx.online_Client.RemoveRange(clients);
                ctx.SaveChanges();
            }
        }

        public static void LoggedInCount(int clientId, bool isLogin)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                var obj = ctx.online_Client.FirstOrDefault(t => t.Id == clientId);
                if (obj == null)
                    return;
                if (isLogin)
                {
                    if (obj.LoggedInCount == null || obj.LoggedInCount <= 0)
                        obj.LoggedInCount = 1;
                    else
                        obj.LoggedInCount = obj.LoggedInCount + 1;
                }
                else
                {
                    if (obj.LoggedInCount == null)
                        obj.LoggedInCount = 0;
                    else
                        obj.LoggedInCount = obj.LoggedInCount - 1;
                }
                ctx.SaveChanges();
            }
        }
    }
}