using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using NotaliaOnline.Properties;
using NotaliaOnline.WebReference;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class BF061 : Page
    {
        private string _sessionId;
        private ExcelService _excelService;
        private Status[] _status;

        protected void Page_Load(object sender, EventArgs e)
        {
            postback.Value = IsPostBack.ToString().ToLower();

            if (Session["CLIENT_ID"] == null)
                return;
            var subscriber = ApiDataAccess.Subscription(Session["ReferenceCustomer"].ToString());
            if (subscriber != null)
            {
                if (subscriber.TitleLocalized == Resource.Trial)
                    divLibelleSimulation.Visible = divSendEmail.Visible = divPrint.Visible = false;
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Voir"]))
                {
                    if (FillValues(Request.QueryString["Voir"].TransformToInt(), Session["CLIENT_ID"].TransformToInt()))
                    {
                        btnSynthese_Click(sender, e);
                    }
                }
            }
        }

        private bool FillValues(int id, int clientId)
        {
            try
            {
                var obj = SimulationActeDataAccess.GetSimulationActe(id);
                if (obj == null) return false;
                txtLibelle.Enabled = false;
                btnEnregistrerSous.Visible = true;
                btnEnregistrer.Visible = clientId == obj.ClientId;
                txtLibelle.Text = obj.Libelle;
                var data = new JavaScriptSerializer().Deserialize<DataModelBF061>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                txtZone01.Text = data.txtZone01;
                txtZone02.Text = data.txtZone02;
                txtZone03.Text = data.txtZone03;
                txtZone04.Text = data.txtZone04;
                chk1.Checked = data.chk1.TransformToBoolean();
                txtDébours.Text = data.txtDébours;
                return true;
            }
            catch (Exception)
            {
                //throw;
                return false;
            }
        }

        private void SetRange()
        {
            var range = new RangeCoordinates
            {
                Row = 6,
                Column = 4,
                Height = 6,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtZone01.Text},
                        new object[] {txtZone02.Text},
                        new object[] {txtZone03.Text},
                        new object[] {chk1.Checked.TransformToBooleanFr()},
                        new object[] {txtZone04.Text.TransformToPercentage()},
                        new object[]{txtDébours.Text}
                });
        }

        private void SetValues(DataTable dt)
        {
            lblF102.InnerText = dt.Rows[0][(int)ColumnBF061.Col1].ToString();
            lblF103.InnerText = dt.Rows[1][(int)ColumnBF061.Col1].ToString();
            lblF104.InnerText = dt.Rows[2][(int)ColumnBF061.Col1].ToString();
            lblF105.InnerText = dt.Rows[3][(int)ColumnBF061.Col1].ToString();

            lblF107.InnerText = dt.Rows[5][(int)ColumnBF061.Col1].ToString();
            lblF108.InnerText = dt.Rows[6][(int)ColumnBF061.Col1].ToString();
            lblF109.InnerText = dt.Rows[7][(int)ColumnBF061.Col1].ToString();

            lblF111.InnerText = dt.Rows[9][(int)ColumnBF061.Col1].ToString();

            lblG114.InnerText = dt.Rows[12][(int)ColumnBF061.Col2].ToString();
            lblF116.InnerText = dt.Rows[14][(int)ColumnBF061.Col1].ToString();
            lblG116.InnerText = dt.Rows[14][(int)ColumnBF061.Col2].ToString();
            lblF117.InnerText = dt.Rows[15][(int)ColumnBF061.Col1].ToString();
            lblG117.InnerText = dt.Rows[15][(int)ColumnBF061.Col2].ToString();
            lblF118.InnerText = dt.Rows[16][(int)ColumnBF061.Col1].ToString();
            lblG118.InnerText = dt.Rows[16][(int)ColumnBF061.Col2].ToString();
            lblG119.InnerText = dt.Rows[17][(int)ColumnBF061.Col2].ToString();

            var html = "";
            //TOTAL DES DROITS ET FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Convention d'honoraires :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF102.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF103.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF104.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF105.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF061\\chart.png");
            //DÉTAIL DES FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Convention d`honoraires</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Montant HT :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF107.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF108.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF109.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF111.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td align='right'>Montant des droits de mutation :</td><td colspan='3'></td></tr>";
            html += string.Format(
                    "<tr><td align='right'></td><td align='right'>Base taxable :</td><td align='right'>{0}</td><td></td></tr>",
                    lblG114.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Taxe départementale :</td><td align='right'>{0}    =</td><td align='right'>{1}</td><td></td></tr>",
                    lblF116.InnerText, lblG116.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Prélèvement de l'Etat sur taxe départementale :</td><td align='right'>{0}    =</td><td align='right'>{1}</td><td></td></tr>",
                    lblF117.InnerText, lblG117.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Taxe locale :</td><td align='right'>{0}    =</td><td align='right'>{1}</td><td></td></tr>",
                    lblF118.InnerText, lblG118.InnerText);
            html += string.Format(
                    "<tr><td align='right'></td><td align='right'>Total :</td><td align='right'>{0}</td><td></td></tr>",
                    lblG119.InnerText);
            hdResult.Value = html + "</table>";
        }

        protected void btnSynthese_Click(object sender, EventArgs e)
        {
            try
            {
                //txtLibelle.Text = string.IsNullOrEmpty(txtDossier.Text) ? txtLibelle.Text : txtDossier.Text;



                var watch = System.Diagnostics.Stopwatch.StartNew();
                _excelService = ExcelServiceHelper.ExcelServiceProvider();
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF061-ONLINE.xlsm", out _status);
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "http://80.14.175.214/Documents%20partages/BF061-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 5, 18, 3, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF061");
                Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Le serveur de calculs est temporairement indisponible. Veuillez réessayer plus tard.', 'Échec du calcul', {timeOut: 5000});", true);
            }
        }

        protected void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                var simulationId = Request.QueryString["Voir"].TransformToInt();
                var obj = SimulationActeDataAccess.GetSimulationActe(simulationId, txtLibelle.Text.Trim());
                if (obj != null)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Une simulation portant le même nom est déjà enregistrée. Veuillez modifier le nom de la simulation.', 'Échec de enregistrement', {timeOut: 5000});", true);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "scrollToElement", "scrollToElement('#divLibelleSimulation',15);", true);
                    Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
                    return;
                }
                var data = @"{" +
                           "'txtDossier':'" + txtDossier.Text +
                           "','txtDateDeSignature':'" + txtDateSignature.Text +
                           "','txtRedacteur':'" + txtRedacteur.Text +
                           "','txtZone01':'" + txtZone01.Text + "','txtZone02':'" + txtZone02.Text + "','txtZone03':'" +
                           txtZone03.Text + "','chk1':'" + chk1.Checked + "','txtZone04':'" + txtZone04.Text +
                           "','txtDébours':'" + txtDébours.Text + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF061", false, Session["CLIENT_ID"].TransformToInt());
                }
                else
                {
                    SimulationActeDataAccess.Update(simulationId, data, Session["CLIENT_ID"].TransformToInt());
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Cette simulation a été enregistrée avec succès.', 'Notification', {timeOut: 5000});", true);
                //BindSimulationList(0);
                //BindSimulationArchiveList(0);
                Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Le serveur de calculs est temporairement indisponible. Veuillez réessayer plus tard.', 'Échec du calcul', {timeOut: 5000});", true);
            }
        }

        protected void btnEnregistrerSous_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = SimulationActeDataAccess.GetSimulationActe(txtLibelleSous.Text.Trim());
                if (obj != null)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Une simulation portant le même nom est déjà enregistrée. Veuillez modifier le nom de la simulation.', 'Échec de enregistrement', {timeOut: 5000});", true);
                    //Page.ClientScript.RegisterStartupScript(GetType(), "scrollToElement", "scrollToElement('#divLibelleSimulation',15);", true);
                    Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
                    return;
                }
                var data = @"{" +
                           "'txtDossier':'" + txtDossier.Text +
                           "','txtDateDeSignature':'" + txtDateSignature.Text +
                           "','txtRedacteur':'" + txtRedacteur.Text +
                           "','txtZone01':'" + txtZone01.Text + "','txtZone02':'" + txtZone02.Text + "','txtZone03':'" +
                           txtZone03.Text + "','chk1':'" + chk1.Checked + "','txtZone04':'" + txtZone04.Text +
                           "','txtDébours':'" + txtDébours.Text + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF061", false, Session["CLIENT_ID"].TransformToInt());
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Cette simulation a été enregistrée avec succès.', 'Notification', {timeOut: 5000});", true);
                //BindSimulationList(0);
                //BindSimulationArchiveList(0);
                Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Le serveur de calculs est temporairement indisponible. Veuillez réessayer plus tard.', 'Échec de enregistrement', {timeOut: 5000});", true);
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            var filename = PdfHelper.GeneratePdf("BF061", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
            //Page.ClientScript.RegisterStartupScript(GetType(), "scrollToElement", "scrollToElement('#btnPrint',15);", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.AddHeader("Content-Length", pdf.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(pdf);
            Response.End();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var filename = PdfHelper.GeneratePdf("BF061", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
            //Page.ClientScript.RegisterStartupScript(GetType(), "scrollToElement", "scrollToElement('#btnPrint',15);", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.AddHeader("Content-Length", pdf.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(pdf);
            Response.End();
        }
    }

    enum ColumnBF061
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2
    }

    class DataModelBF061
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string chk1 { get; set; }
        public string txtDébours { get; set; }
    }
}