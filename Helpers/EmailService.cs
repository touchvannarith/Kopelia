using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace NotaliaOnline.Helpers
{
    public class EmailService
    {
        public static void SendSimulationPdf(string toEmail, string pdf)
        {
            var mailMessage = new MailMessage();
            var smtpClient = new SmtpClient();
            mailMessage.From = new MailAddress("relation-clients@notalia.fr");
            smtpClient.Host = "webmail13.hosteam.fr";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("relation-clients@notalia.fr", "re98vi08");
            var to = toEmail.Split(';');
            to.ToList().ForEach(x => { mailMessage.To.Add(new MailAddress(x)); });
            mailMessage.Subject = "NOTALIA - Votre calcul de frais";
            mailMessage.IsBodyHtml = true;
            if (File.Exists(pdf)){ mailMessage.Attachments.Add(new Attachment(pdf)); }
            var body = @"
                    <html>
                    <head>

                    </head>
                        <body>
                            <p>Madame, monsieur,</p><br>
                            Je vous prie de bien vouloir trouver en pièces-jointes votre calcul de frais.<br>
                            Vous en souhaitant une bonne réception.<br>
                            Votre bien dévoué.</p><br>
                            <img src=""cid:ImageId"">
                    <table>
                    <tr>
                    <td>
                    <span style=""color:#1F4994;font-weight:bold;"">Jean-Marc VIOLET</span><br>
                    <a href=""mailto:jmviolet@notalia.fr"" style=""color:#1F4994;"">jmviolet@notalia.fr</a>
                    </td>
                    <td style=""width: 25px;""></td>
                    <td>
                    <span style=""color:#1F4994;font-weight:bold;"">Notalia</span><br>
                    <span style=""color:#1F4994"">583, Avenue des</span><br>
                    <span style=""color:#1F4994"">Bousquets</span><br>
                    <span style=""color:#1F4994"">ZAC des bousquets</span><br>
                    <span style=""color:#1F4994"">83390 - CUERS</span>
                    </td>
                    </tr>
                    <tr><td colspan=""3""><div></div></td></tr>
                    <tr>
                    <td style=""color:#1F4994"">Site : <a href=""http://www.notalia.fr"">http://www.notalia.fr</a></td>
                    <td></td>
                    <td style=""padding-top:20px;""><span style=""color:#1F4994"">Tel : 04 94 12 20 53</span><br>
                    <span style=""color:#1F4994"">Fax : 04 88 10 05 54</span></td>
                    </tr>
                    </table>
                        </body>
                    </html>";
            var altView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            var notaliaLogo = new LinkedResource(HttpContext.Current.Request.PhysicalApplicationPath + @"\images\signature.png", MediaTypeNames.Image.Jpeg) { ContentId = "ImageId" };
            altView.LinkedResources.Add(notaliaLogo);
            mailMessage.AlternateViews.Add(altView);
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
        }

        public static void SendEmail(string mailTo, string subject, string body)
        {
            var mailMessage = new MailMessage();
            var smtpClient = new SmtpClient();
            mailMessage.From = new MailAddress("relation-clients@notalia.fr");
            mailMessage.To.Add(new MailAddress(mailTo));
            smtpClient.Host = "webmail13.hosteam.fr";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("relation-clients@notalia.fr", "re98vi08");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml=true;
            var altView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            mailMessage.AlternateViews.Add(altView);
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
        }
    }
}