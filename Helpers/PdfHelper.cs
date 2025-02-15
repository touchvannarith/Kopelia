using iTextSharp.text;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Helpers
{
    public class PdfHelper
    {
        private static void GenerateChartImage(string chartValue, string imagePath)
        {
            var base64 = chartValue;
            var bytes = Convert.FromBase64String(base64.Split(',')[1]);
            var fileName = imagePath;
            using (var imageFile = new FileStream(fileName, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
                imageFile.Close();
            }
        }

        private static void ConvertToPdf(string htmlSaisie, string htmlResult, string pdfPath)
        {
            if (File.Exists(pdfPath))
                File.Delete(pdfPath);
            var builder = new HtmlToPdfBuilder(PageSize.A4);
            var first = builder.AddPage();
            first.AppendHtml(htmlSaisie);
            first = builder.AddPage();
            first.AppendHtml(htmlResult);
            var file = builder.RenderPdf();
            File.WriteAllBytes(pdfPath, file);
        }

        public static string GeneratePdf(string folderName, string piechart, string image, int userGroupId, string saisie, string result, out string pdf)
        {
            var folder = HttpContext.Current.Request.PhysicalApplicationPath + @"tmp\" + folderName;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var chartImage = folder + @"\chart.png";
            GenerateChartImage(piechart, chartImage);
            var logo = HttpContext.Current.Request.PhysicalApplicationPath + @"images\logo\" + image;
            if (string.IsNullOrEmpty(image))
            {
                using(var ctx = new NotaliaOnlineEntities())
                {
                    var objClient = ctx.online_Client.FirstOrDefault(t => t.Id == userGroupId);
                    if (objClient != null)
                        logo = HttpContext.Current.Request.PhysicalApplicationPath + @"images\logo\" + objClient.ImageLogo;
                }
            }
            var fileNameFormat = folderName + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour +
                                 DateTime.Now.Minute + DateTime.Now.Second;
            pdf = folder + @"\" + fileNameFormat + ".pdf";
            var htmlSaisie = "";
            htmlSaisie = htmlSaisie + Convert.ToString(@"<div>");
            if (File.Exists(logo))
                htmlSaisie = htmlSaisie + Convert.ToString(@"<img align=""center"" src=" + logo + "></img>");
            htmlSaisie = htmlSaisie + Convert.ToString(@"<br/></div>");
            htmlSaisie += saisie;
            var htmlResult = result;
            ConvertToPdf(htmlSaisie, htmlResult, pdf);
            return fileNameFormat + ".pdf";
        }

        public static string GeneratePdf(string folderName, string image, int userGroupId, string saisie, string result, out string pdf)
        {
            var folder = HttpContext.Current.Request.PhysicalApplicationPath + @"tmp\" + folderName;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var logo = HttpContext.Current.Request.PhysicalApplicationPath + @"images\logo\" + image;
            if (string.IsNullOrEmpty(image))
            {
                using (var ctx = new NotaliaOnlineEntities())
                {
                    var objClient = ctx.online_Client.FirstOrDefault(t => t.Id == userGroupId);
                    if (objClient != null)
                        logo = HttpContext.Current.Request.PhysicalApplicationPath + @"images\logo\" + objClient.ImageLogo;
                }
            }
            var fileNameFormat = folderName + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour +
                                 DateTime.Now.Minute + DateTime.Now.Second;
            pdf = folder + @"\" + fileNameFormat + ".pdf";
            var htmlSaisie = "";
            htmlSaisie = htmlSaisie + Convert.ToString(@"<div>");
            if (File.Exists(logo))
                htmlSaisie = htmlSaisie + Convert.ToString(@"<img align=""center"" src=" + logo + "></img>");
            htmlSaisie = htmlSaisie + Convert.ToString(@"<br/></div>");
            htmlSaisie += saisie;
            var htmlResult = result;
            ConvertToPdf(htmlSaisie, htmlResult, pdf);
            return fileNameFormat + ".pdf";
        }
    }
}