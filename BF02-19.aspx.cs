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
    public partial class BF02_19 : Page
    {
        private string _sessionId;
        private Status[] _status;
        private ExcelService _excelService;

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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF02_19>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                chkAttestation1.Checked = data.chkAttestation1.TransformToBoolean();
                radAttestation1.Checked = data.radAttestation1.TransformToBoolean();
                radAttestation2.Checked = data.radAttestation2.TransformToBoolean();
                txtZone01.Text = data.txtZone01;
                txtZone02.Text = data.txtZone02;
                txtZone03.Text = data.txtZone03;
                txtZone04.Text = data.txtZone04;
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
                                new object[] {19}
                            });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates() { Row = 70, Column = 5, Height = 7, Width = 2 }, new object[]
                {
                        new object[] {chkAttestation1.Checked.TransformToBooleanFr(), ""},
                        new object[] {radAttestation1.Checked.TransformToBooleanFr(), ""},
                        new object[] {radAttestation2.Checked.TransformToBooleanFr(), ""},
                        new object[] {txtZone01.Text, ""},
                        new object[] {txtZone02.Text, txtEmolument_de_formalités_HT.Text},
                        new object[] {txtZone03.Text, txtDébours.Text},
                        new object[] {txtZone04.Text, hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            var html = "";
            const string show = "1";
            D942.InnerText = dt.Rows[40][(int)ColumnBF02_19.Col1].ToString();
            D943.InnerText = dt.Rows[41][(int)ColumnBF02_19.Col1].ToString();
            D944.InnerText = dt.Rows[42][(int)ColumnBF02_19.Col1].ToString();
            D945.InnerText = dt.Rows[43][(int)ColumnBF02_19.Col1].ToString();
            //TOTAL DES DROITS ET FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D942.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D943.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D944.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D945.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF02-19\\chart.png");
            //DÉTAIL DES FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            lblSubEmoluments.InnerText = chkAttestation1.Checked
                ? "Emoluments du notaire - A444-69-1"
                : "Emoluments du notaire - A444-59";
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>" +
                (chkAttestation1.Checked ? "Emoluments du notaire - A444-69-1" : "Emoluments du notaire - A444-59") +
                "</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row907.Visible = false;
            if (dt.Rows[5][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row907.Visible = true;
                lblRow11.InnerText = dt.Rows[5][(int)ColumnBF02_19.Col1].ToString();
                lblRow12.InnerText = dt.Rows[5][(int)ColumnBF02_19.Col2].ToString();
                lblRow13.InnerText = dt.Rows[5][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow11.InnerText, lblRow12.InnerText, lblRow13.InnerText);
            }
            row908.Visible = false;
            if (dt.Rows[6][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row908.Visible = true;
                lblRow21.InnerText = dt.Rows[6][(int)ColumnBF02_19.Col1].ToString();
                lblRow22.InnerText = dt.Rows[6][(int)ColumnBF02_19.Col2].ToString();
                lblRow23.InnerText = dt.Rows[6][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow21.InnerText, lblRow22.InnerText, lblRow23.InnerText);
            }
            row909.Visible = false;
            if (dt.Rows[7][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row909.Visible = true;
                lblRow31.InnerText = dt.Rows[7][(int)ColumnBF02_19.Col1].ToString();
                lblRow32.InnerText = dt.Rows[7][(int)ColumnBF02_19.Col2].ToString();
                lblRow33.InnerText = dt.Rows[7][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow31.InnerText, lblRow32.InnerText, lblRow33.InnerText);
            }
            row910.Visible = false;
            if (dt.Rows[8][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row910.Visible = true;
                lblRow41.InnerText = dt.Rows[8][(int)ColumnBF02_19.Col1].ToString();
                lblRow42.InnerText = dt.Rows[8][(int)ColumnBF02_19.Col2].ToString();
                lblRow43.InnerText = dt.Rows[8][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow41.InnerText, lblRow42.InnerText, lblRow43.InnerText);
            }
            row911.Visible = false;
            if (dt.Rows[9][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row911.Visible = true;
                D911.InnerText = dt.Rows[9][(int)ColumnBF02_19.Col1].ToString();
                E911.InnerText = dt.Rows[9][(int)ColumnBF02_19.Col2].ToString();
                F911.InnerText = dt.Rows[9][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    D911.InnerText, E911.InnerText, F911.InnerText);
            }
            row912.Visible = false;
            if (dt.Rows[10][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row912.Visible = true;
                lblTotal.InnerText = dt.Rows[10][(int)ColumnBF02_19.Col2].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                    lblTotal.InnerText);
            }
            row913.Visible = false;
            if (dt.Rows[11][(int)ColumnBF02_19.Col1].ToString() == show)
            {
                row913.Visible = true;
                html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
            }
            row914.Visible = false;
            if (dt.Rows[12][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                row914.Visible = true;
                D914.InnerText = dt.Rows[12][(int)ColumnBF02_19.Col1].ToString();
                html += "<tr><td colspan='4' align='center'>" + D914.InnerText + "</td></tr>";
            }
            D917.InnerText = dt.Rows[15][(int)ColumnBF02_19.Col1].ToString();
            //D919.InnerText = dt.Rows[17][(int)ColumnBF02_19.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments réglementés (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    D917.InnerText);
            //html += string.Format(
            //        "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
            //        D919.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D921.InnerText = dt.Rows[19][(int)ColumnBF02_19.Col1].ToString();
            D922.InnerText = dt.Rows[20][(int)ColumnBF02_19.Col1].ToString();
            D923.InnerText = dt.Rows[21][(int)ColumnBF02_19.Col1].ToString();
            D924.InnerText = dt.Rows[22][(int)ColumnBF02_19.Col1].ToString();
            D925.InnerText = dt.Rows[23][(int)ColumnBF02_19.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    D921.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    D922.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    D923.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    D924.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    D925.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D929.InnerText = dt.Rows[27][(int)ColumnBF02_19.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    D929.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td colspan='4'>Droit fixe (art. 680)</td></tr>";
            D935.InnerText = dt.Rows[33][(int)ColumnBF02_19.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Taxe fixe :</td><td align='right'>{0}</td><td></td></tr>",
                D935.InnerText);
            row937.Visible = false;
            if (dt.Rows[35][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>Taxe de publicité</td></tr>";
                row937.Visible = true;
                lblCase19CSIRow11.InnerText = dt.Rows[35][(int)ColumnBF02_19.Col1].ToString();
                lblCase19CSIRow12.InnerText = dt.Rows[35][(int)ColumnBF02_19.Col2].ToString();
                lblCase19CSIRow13.InnerText = dt.Rows[35][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td align='right'></td></tr>",
                    lblCase19CSIRow11.InnerText, lblCase19CSIRow12.InnerText, lblCase19CSIRow13.InnerText);
                row938.Visible = false;
                if (dt.Rows[36][(int)ColumnBF02_19.Col1].ToString() == show)
                {
                    row938.Visible = true;
                    html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
                }
            }
            row939.Visible = false;
            if (dt.Rows[37][(int)ColumnBF02_19.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>CSI (art. 879 du CGI)</td></tr>";
                row939.Visible = true;
                lblCase19CSIRow21.InnerText = dt.Rows[37][(int)ColumnBF02_19.Col1].ToString();
                lblCase19CSIRow22.InnerText = dt.Rows[37][(int)ColumnBF02_19.Col2].ToString();
                lblCase19CSIRow23.InnerText = dt.Rows[37][(int)ColumnBF02_19.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td align='right'></td></tr>",
                    lblCase19CSIRow21.InnerText, lblCase19CSIRow22.InnerText, lblCase19CSIRow23.InnerText);
                row940.Visible = false;
                if (dt.Rows[38][(int)ColumnBF02_19.Col1].ToString() == show)
                {
                    row940.Visible = true;
                    html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
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
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 901, 3, 44, 4, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;

                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF02-19");
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
                           "','chkAttestation1':'" + chkAttestation1.Checked + "','radAttestation1':'" +
                           radAttestation1.Checked + "','radAttestation2':'" + radAttestation2.Checked +
                           "','txtZone01':'" + txtZone01.Text + "','txtZone02':'" + txtZone02.Text + "','txtZone03':'" +
                           txtZone03.Text + "','txtZone04':'" + txtZone04.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF02-19", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chkAttestation1':'" + chkAttestation1.Checked + "','radAttestation1':'" +
                           radAttestation1.Checked + "','radAttestation2':'" + radAttestation2.Checked +
                           "','txtZone01':'" + txtZone01.Text + "','txtZone02':'" + txtZone02.Text + "','txtZone03':'" +
                           txtZone03.Text + "','txtZone04':'" + txtZone04.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF02-19", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF02-19", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF02-19", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF02_19
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2,
        Col4 = 3
    }

    class DataModelBF02_19
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string chkAttestation1 { get; set; }
        public string radAttestation1 { get; set; }
        public string radAttestation2 { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}