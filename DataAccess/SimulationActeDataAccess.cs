using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.DataAccess
{
    public static class SimulationActeDataAccess
    {
        public static List<vw_online_Simulation> GetSimulationActes(int clientId, bool archive)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                List<vw_online_Simulation> simulationList;
                List<online_Client> clientList;
                var client = ClientDataAccess.GetClient(clientId);
                if (client.IsAdmin == true)
                {
                    //Owner
                    simulationList = ctx.vw_online_Simulation.Where(t => t.Archive == archive && t.ClientId == client.Id).ToList();
                    clientList = ctx.online_Client.Where(t => t.GroupId == client.Id).ToList();
                    foreach (var each in clientList)
                    {
                        simulationList.AddRange(ctx.vw_online_Simulation.Where(t => t.ClientId == each.Id && t.Archive == archive));
                    }
                }
                else
                {
                    //User
                    simulationList = ctx.vw_online_Simulation.Where(t => t.Archive == archive && t.ClientId == client.GroupId).ToList();
                    clientList = ctx.online_Client.Where(t => t.GroupId == client.GroupId).ToList();
                    foreach (var each in clientList)
                    {
                        simulationList.AddRange(ctx.vw_online_Simulation.Where(t => t.ClientId == each.Id && t.Archive == archive));
                    }
                }
                foreach (var each in simulationList)
                {
                    each.AllowDelete = each.ClientId == client.Id;
                }
                simulationList = simulationList.OrderByDescending(t => t.DateUpdated).ToList();
                return simulationList;
            }
        }

        public static online_SIMULATION_ACTE GetSimulationActe(int id)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                return ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == id);
            }
        }

        public static online_SIMULATION_ACTE GetSimulationActe(string name)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                return ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Libelle == name);
            }
        }

        public static online_SIMULATION_ACTE GetSimulationActe(int id, string name)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                return ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Libelle == name && t.Id != id);
            }
        }

        public static void Archive(int id, bool status)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                var obj = ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == id);
                if (obj == null) return;
                obj.Archive = status;
                ctx.SaveChanges();
            }
        }

        public static void Remove(int id)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                var obj = ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == id);
                if (obj == null) return;
                ctx.online_SIMULATION_ACTE.Remove(obj);
                ctx.SaveChanges();
            }
        }

        public static void Save(string data, string name, string pageName, bool archive, int userId)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                ctx.online_SIMULATION_ACTE.Add(new online_SIMULATION_ACTE
                {
                    Value = data,
                    Libelle = name,
                    DateUpdated = DateTime.Now,
                    PageName = pageName,
                    Archive = archive,
                    ClientId = userId
                });
                ctx.SaveChanges();
            }
        }

        public static void Update(int id, string data, int userId)
        {
            using (var ctx = new NotaliaOnlineEntities())
            {
                var obj = ctx.online_SIMULATION_ACTE.FirstOrDefault(t => t.Id == id);
                if(obj == null) return;
                obj.Value = data;
                obj.ClientId = userId;
                obj.DateUpdated = DateTime.Now;
                ctx.SaveChanges();
            }
        }
    }
}