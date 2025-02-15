using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NotaliaOnline.Helpers
{
    public static class Transformer
    {
        public static int TransformToInt(this object obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string TransformToBooleanFr(this object obj)
        {
            try
            {
                return (bool)obj ? "VRAI" : "FAUX";
            }
            catch (Exception)
            {
                try
                {
                    return (string)obj == "true" ? "VRAI" : "FAUX";
                }
                catch (Exception)
                {
                    return "FAUX";
                }
            }
        }

        public static bool TransformToBoolean(this object obj)
        {
            try
            {
                var str = obj.ToString();
                return str == "1" || str.ToLower() == "true";
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string TransformToPercentage(this object obj)
        {
            try
            {
                return (string)obj + "%";
            }
            catch (Exception)
            {
                return "0%";
            }
        }

        public static double TransformToDouble(this object obj)
        {
            try
            {
                var value = obj.ToString().Trim().Replace(",", ".");
                return double.Parse(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string TransformToDuration(int duration, string unit)
        {
            try
            {
                string result;
                switch (unit.ToLower())
                {
                    case "day":
                        result = duration == 1 ? "jour" : "jours";
                        break;
                    case "week":
                        result = duration == 1 ? "semaine" : "semaines";
                        break;
                    case "month":
                        result = "mois";
                        break;
                    case "year":
                        result = duration == 1 ? "an" : "ans";
                        break;
                    default:
                        result = "";
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static DataTable TranformToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    try
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    catch (Exception)
                    {
                        dataTable.Rows.Add(values);
                        return dataTable;
                    }
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;

        }
    }
}