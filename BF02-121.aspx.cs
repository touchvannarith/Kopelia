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
    public partial class BF02_121 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF02_121>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                chkNotoriete.Checked = data.chkNotoriete.TransformToBoolean();
                txtNumPages.Text = data.txtNumPages;
                txtNumCopies.Text = data.txtNumCopies;
                txtNumExp.Text = data.txtNumExp;
                txtEmolument_de_formalités_HT.Text = data.txtEmolument_de_formalités_HT;
                txtDébours.Text = data.txtDébours;
                hdUtilisation_du_futur_tarif.Value = data.chkUtilisation_du_futur_tarif;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SetRange()
        {
            _excelService.SetRange(_sessionId, "ENTREE_S",
                        new RangeCoordinates() { Row = 7, Column = 5, Height = 1, Width = 1 }, new object[]
                        {
                            new object[] {12}
                        });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                    new RangeCoordinates() { Row = 47, Column = 5, Height = 8, Width = 2 }, new object[]
                    {
                            new object[] {"VRAI", ""},
                            new object[] {"FAUX", ""},
                            new object[] {"FAUX", ""},
                            new object[] {"", ""},
                            new object[] {chkNotoriete.Checked.TransformToBooleanFr(), ""},
                            new object[] {txtNumPages.Text, txtEmolument_de_formalités_HT.Text},
                            new object[] {txtNumCopies.Text, txtDébours.Text},
                            new object[] {txtNumExp.Text, hdUtilisation_du_futur_tarif.Value}
                    });
        }

        private void SetValues(DataTable dt)
        {
            var html = "";
            const string show = "1";
            //TOTAL DES DROITS ET FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            D641.InnerText = dt.Rows[39][(int)ColumnBF02_121.Col1].ToString();
            D642.InnerText = dt.Rows[40][(int)ColumnBF02_121.Col1].ToString();
            D643.InnerText = dt.Rows[41][(int)ColumnBF02_121.Col1].ToString();
            D644.InnerText = dt.Rows[42][(int)ColumnBF02_121.Col1].ToString();
            html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D641.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D642.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D643.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D644.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF02-121\\chart.png");
            //DÉTAIL DES FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-66</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row607.Visible = false;
            if (dt.Rows[5][(int)ColumnBF02_121.Col4].ToString() == show)
            {
                row607.Visible = true;
                lblRow11.InnerText = dt.Rows[5][(int)ColumnBF02_121.Col1].ToString();
                lblRow12.InnerText = dt.Rows[5][(int)ColumnBF02_121.Col2].ToString();
                lblRow13.InnerText = dt.Rows[5][(int)ColumnBF02_121.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow11.InnerText, lblRow12.InnerText, lblRow13.InnerText);
            }
            row608.Visible = false;
            if (dt.Rows[6][(int)ColumnBF02_121.Col4].ToString() == show)
            {
                row608.Visible = true;
                lblRow21.InnerText = dt.Rows[6][(int)ColumnBF02_121.Col1].ToString();
                lblRow22.InnerText = dt.Rows[6][(int)ColumnBF02_121.Col2].ToString();
                lblRow23.InnerText = dt.Rows[6][(int)ColumnBF02_121.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow21.InnerText, lblRow22.InnerText, lblRow23.InnerText);
            }
            row609.Visible = false;
            if (dt.Rows[7][(int)ColumnBF02_121.Col4].ToString() == show)
            {
                row609.Visible = true;
                lblRow31.InnerText = dt.Rows[7][(int)ColumnBF02_121.Col1].ToString();
                lblRow32.InnerText = dt.Rows[7][(int)ColumnBF02_121.Col2].ToString();
                lblRow33.InnerText = dt.Rows[7][(int)ColumnBF02_121.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow31.InnerText, lblRow32.InnerText, lblRow33.InnerText);
            }
            row610.Visible = false;
            if (dt.Rows[8][(int)ColumnBF02_121.Col4].ToString() == show)
            {
                row610.Visible = true;
                lblRow41.InnerText = dt.Rows[8][(int)ColumnBF02_121.Col1].ToString();
                lblRow42.InnerText = dt.Rows[8][(int)ColumnBF02_121.Col2].ToString();
                lblRow43.InnerText = dt.Rows[8][(int)ColumnBF02_121.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow41.InnerText, lblRow42.InnerText, lblRow43.InnerText);
            }
            row611.Visible = false;
            if (dt.Rows[9][(int)ColumnBF02_121.Col4].ToString() == show)
            {
                row611.Visible = true;
                lblTotal.InnerText = dt.Rows[9][(int)ColumnBF02_121.Col2].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                    lblTotal.InnerText);
            }
            row613.Visible = false;
            if (dt.Rows[11][(int)ColumnBF02_121.Col1].ToString() == show)
            {
                row613.Visible = true;
                html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
            }
            D615.InnerText = dt.Rows[13][(int)ColumnBF02_121.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emolument réglementé :</td><td align='right'>{0}</td><td></td></tr>",
                D615.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D619.InnerText = dt.Rows[17][(int)ColumnBF02_121.Col1].ToString();
            D620.InnerText = dt.Rows[18][(int)ColumnBF02_121.Col1].ToString();
            D621.InnerText = dt.Rows[19][(int)ColumnBF02_121.Col1].ToString();
            D622.InnerText = dt.Rows[20][(int)ColumnBF02_121.Col1].ToString();
            D623.InnerText = dt.Rows[21][(int)ColumnBF02_121.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    D619.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    D620.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    D621.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    D622.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    D623.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D627.InnerText = dt.Rows[25][(int)ColumnBF02_121.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    D627.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td colspan='4'>Enregistrement :</td></tr>";
            D631.InnerText = dt.Rows[29][(int)ColumnBF02_121.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Enregistrement :</td><td align='right'>{0}</td><td></td></tr>",
                D631.InnerText);
            html += "<tr><td colspan='4' align='center'>Acte dispensé de formalité (CGI art 846 bis) - Paiement du droit sur état</td></tr>";
            row633.Visible = false;
            if (dt.Rows[31][(int)ColumnBF02_121.Col4].ToString() == show)
            {
                row633.Visible = true;
                row635.Visible = false;
                if (dt.Rows[33][(int)ColumnBF02_121.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='left'>Taxe de publicité :</td></tr>";
                    row635.Visible = true;
                    lblD627.InnerText = dt.Rows[33][(int)ColumnBF02_121.Col1].ToString();
                    lblE627.InnerText = dt.Rows[33][(int)ColumnBF02_121.Col2].ToString();
                    lblF627.InnerText = dt.Rows[33][(int)ColumnBF02_121.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td align='right'></td></tr>",
                        lblD627.InnerText, lblE627.InnerText, lblF627.InnerText);
                    row636.Visible = false;
                    if (dt.Rows[34][(int)ColumnBF02_121.Col1].ToString() == show)
                    {
                        row636.Visible = true;
                        html += "<tr><td colspan='4' align='center'>Prise en compte du minimum de perception.</td></tr>";
                    }
                }
                row638.Visible = false;
                if (dt.Rows[36][(int)ColumnBF02_121.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='left'>CSI (art. 879 du CGI) :</td></tr>";
                    row638.Visible = true;
                    lblD630.InnerText = dt.Rows[36][(int)ColumnBF02_121.Col1].ToString();
                    lblE630.InnerText = dt.Rows[36][(int)ColumnBF02_121.Col2].ToString();
                    lblF630.InnerText = dt.Rows[36][(int)ColumnBF02_121.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td align='right'></td></tr>",
                        lblD630.InnerText, lblE630.InnerText, lblF630.InnerText);
                    row639.Visible = false;
                    if (dt.Rows[37][(int)ColumnBF02_121.Col1].ToString() == show)
                    {
                        row639.Visible = true;
                        html += "<tr><td colspan='4' align='center'>Prise en compte du minimum de perception.</td></tr>";
                    }
                }
            }
            hdResult.Value = html + "</table>";
        }

        protected void btnSynthese_Click(object sender, EventArgs e)
        {
            try
            {
                //txtLibelle.Text = string.IsNullOrEmpty(txtDossier.Text) ? txtLibelle.Text : txtDossier.Text;



                var watch = System.Diagnostics.Stopwatch.StartNew();
                _excelService = ExcelServiceHelper.ExcelServiceProvider();
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF02-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 601, 3, 43, 4, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF02-121");
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
                           "','chkNotoriete':'" + chkNotoriete.Checked +
                           "','txtNumPages':'" + txtNumPages.Text + "','txtNumCopies':'" + txtNumCopies.Text +
                           "','txtNumExp':'" + txtNumExp.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF02-121", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chkNotoriete':'" + chkNotoriete.Checked +
                           "','txtNumPages':'" + txtNumPages.Text + "','txtNumCopies':'" + txtNumCopies.Text +
                           "','txtNumExp':'" + txtNumExp.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF02-121", false, Session["CLIENT_ID"].TransformToInt());
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
            try
            {
                PdfHelper.GeneratePdf("BF02-121", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
                EmailService.SendSimulationPdf(txtEmail.Value, pdf);
                Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Votre simulation a bien été envoyée', 'Notification', {timeOut: 5000});", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Échec de l`envoi', 'Notification', {timeOut: 5000});", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var filename = PdfHelper.GeneratePdf("BF02-121", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF02_121
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2,
        Col4 = 3
    }

    class DataModelBF02_121
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string radNotoriete1 { get; set; }
        public string radNotoriete2 { get; set; }
        public string radNotoriete3 { get; set; }
        public string txtNotoriété { get; set; }
        public string chkNotoriete { get; set; }
        public string txtNumPages { get; set; }
        public string txtNumCopies { get; set; }
        public string txtNumExp { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}