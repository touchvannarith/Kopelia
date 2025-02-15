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
    public partial class BF02_16 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF02_16>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                radTestament1.Checked = data.radTestament1.TransformToBoolean();
                radTestament2.Checked = data.radTestament2.TransformToBoolean();
                radTestament3.Checked = data.radTestament3.TransformToBoolean();
                radTestament4.Checked = data.radTestament4.TransformToBoolean();
                radTestament5.Checked = data.radTestament5.TransformToBoolean();
                chkTestament1.Checked = data.chkTestament1.TransformToBoolean();
                txtTestament.Text = data.txtTestament;
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
                                new object[] {16}
                        });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                        new RangeCoordinates() { Row = 60, Column = 5, Height = 10, Width = 2 }, new object[]
                        {
                                new object[] {radTestament1.Checked.TransformToBooleanFr(), ""},
                                new object[] {radTestament2.Checked.TransformToBooleanFr(), ""},
                                new object[] {radTestament3.Checked.TransformToBooleanFr(), ""},
                                new object[] {radTestament4.Checked.TransformToBooleanFr(), ""},
                                new object[] {radTestament5.Checked.TransformToBooleanFr(), ""},
                                new object[] {chkTestament1.Checked.TransformToBooleanFr(), ""},
                                new object[] {txtTestament.Text, ""},
                                new object[] {txtNumPages.Text, txtEmolument_de_formalités_HT.Text},
                                new object[] {txtNumCopies.Text, txtDébours.Text},
                                new object[] {txtNumExp.Text, hdUtilisation_du_futur_tarif.Value}
                        });
        }

        private void SetValues(DataTable dt)
        {
            var html = "";
            const string show = "1";
            D828.InnerText = dt.Rows[26][(int)ColumnBF02_16.Col1].ToString();
            D829.InnerText = dt.Rows[27][(int)ColumnBF02_16.Col1].ToString();
            D830.InnerText = dt.Rows[28][(int)ColumnBF02_16.Col1].ToString();
            D831.InnerText = dt.Rows[29][(int)ColumnBF02_16.Col1].ToString();
            //TOTAL DES DROITS ET FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D828.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D829.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D830.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D831.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF02-16\\chart.png");
            //DÉTAIL DES FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-60</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D805.InnerText = dt.Rows[3][(int)ColumnBF02_16.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument réglementé :</td><td align='right'>{0}</td><td></td></tr>",
                    D805.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D809.InnerText = dt.Rows[7][(int)ColumnBF02_16.Col1].ToString();
            D810.InnerText = dt.Rows[8][(int)ColumnBF02_16.Col1].ToString();
            D811.InnerText = dt.Rows[9][(int)ColumnBF02_16.Col1].ToString();
            D812.InnerText = dt.Rows[10][(int)ColumnBF02_16.Col1].ToString();
            D813.InnerText = dt.Rows[11][(int)ColumnBF02_16.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    D809.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    D810.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    D811.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    D812.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    D813.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D817.InnerText = dt.Rows[15][(int)ColumnBF02_16.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                D817.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            row824.Visible = false;
            if (dt.Rows[22][(int)ColumnBF02_16.Col1].ToString() == show)
            {
                row824.Visible = true;
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                html += "<tr><td colspan='4'>Enregistrement :</td></tr>";
                D825.InnerText = dt.Rows[23][(int)ColumnBF02_16.Col1].ToString();
                html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    D825.InnerText);
                D826.Visible = false;
                if (dt.Rows[24][(int)ColumnBF02_16.Col1].ToString() == "1")
                {
                    D826.Visible = true;
                    html += "<tr><td align='center' colspan='4'>Paiement sur état</td></tr>";
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
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 801, 3, 30, 1, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF02-16");
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
                           "','radTestament1':'" + radTestament1.Checked + "','radTestament2':'" + radTestament2.Checked +
                           "','radTestament3':'" + radTestament3.Checked + "','radTestament4':'" + radTestament4.Checked +
                           "','radTestament5':'" + radTestament5.Checked + "','chkTestament1':'" + chkTestament1.Checked +
                           "','txtTestament':'" + txtTestament.Text +
                           "','txtNumPages':'" + txtNumPages.Text + "','txtNumCopies':'" + txtNumCopies.Text +
                           "','txtNumExp':'" + txtNumExp.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF02-16", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','radTestament1':'" + radTestament1.Checked + "','radTestament2':'" + radTestament2.Checked +
                           "','radTestament3':'" + radTestament3.Checked + "','radTestament4':'" + radTestament4.Checked +
                           "','radTestament5':'" + radTestament5.Checked + "','chkTestament1':'" + chkTestament1.Checked +
                           "','txtTestament':'" + txtTestament.Text +
                           "','txtNumPages':'" + txtNumPages.Text + "','txtNumCopies':'" + txtNumCopies.Text +
                           "','txtNumExp':'" + txtNumExp.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF02-16", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF02-16", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF02-16", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF02_16
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2
    }

    class DataModelBF02_16
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string radTestament1 { get; set; }
        public string radTestament2 { get; set; }
        public string radTestament3 { get; set; }
        public string radTestament4 { get; set; }
        public string radTestament5 { get; set; }
        public string chkTestament1 { get; set; }
        public string txtTestament { get; set; }
        public string txtNumPages { get; set; }
        public string txtNumCopies { get; set; }
        public string txtNumExp { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}