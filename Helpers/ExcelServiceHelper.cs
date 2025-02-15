using NotaliaOnline.WebReference;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace NotaliaOnline.Helpers
{
    public static class ExcelServiceHelper
    {
        private static readonly string _sharePointBaseUrl = "http://194.169.179.128";

        public static ExcelService ExcelServiceProvider()
        {
            ExcelService service = new ExcelService
            {
                Credentials = new NetworkCredential("Administrateur", "Lv&1lft"),
                Url = string.Format("{0}/_vti_bin/ExcelService.asmx", _sharePointBaseUrl)
            };
            return service;
        }

        public static string GetSessionId(ExcelService excelService, string excelFileName, out Status[] status)
        {
            return excelService.OpenWorkbook(string.Format("{0}/Documents%20partages/{1}", _sharePointBaseUrl, excelFileName), "fr-FR", "fr-FR", out status); 
        }

        private static DataTable ConvertToDataTable(object[] data, RangeCoordinates range)
        {
            var dt = new DataTable();
            for (var i = 0; i < range.Width; i++)
            {
                dt.Columns.Add(new DataColumn());
            }

            foreach (var row in data)
            {
                var newrow = dt.NewRow();
                var objects = (object[])row;
                for (var i = 0; i < objects.Count(); i++)
                {
                    if (objects[i] == null) continue;

                    newrow[i] = objects[i].ToString();
                }
                dt.Rows.Add(newrow);
            }
            return dt;
        }

        public static DataTable GetRange(ExcelService excelService, string sessionId, string excelSheet, int row, int column, int height, int width, out Status[] status)
        {
            var range = new RangeCoordinates
            {
                Row = row,
                Column = column,
                Height = height,
                Width = width
            };
            var result = excelService.GetRange(sessionId, excelSheet, range, true, out status);
            //excelService.CloseWorkbook(sessionId);
            return ConvertToDataTable(result,range);
        }

        public static object[] GetRangeOrigin(ExcelService excelService, string sessionId, string excelSheet, int row, int column, int height, int width, out Status[] status)
        {
            var range = new RangeCoordinates
            {
                Row = row,
                Column = column,
                Height = height,
                Width = width
            };
            var result = excelService.GetRange(sessionId, excelSheet, range, true, out status);
            return result;
        }
    }
}