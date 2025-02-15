using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.DataAccess
{
    public static class CalculezMaintenantDataAccess
    {
        public static List<vw_online_Calculez_Maintenant> GetCalculezMaintenants()
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                return ctx.vw_online_Calculez_Maintenant.OrderByDescending(t => t.DateCreated).ToList();
            }
        }

        public static void Save(int userId, string simulation)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                ctx.online_Calculez_Maintenant.Add(new online_Calculez_Maintenant
                {
                    DateCreated = DateTime.Now,
                    SimulationUsed = simulation,
                    UserLogged = userId
                });
                ctx.SaveChanges();
            }
        }
    }
}