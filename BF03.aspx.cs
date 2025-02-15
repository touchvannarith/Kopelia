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
    public partial class BF03 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF03>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                ddl0.SelectedValue = data.ddl0;
                txtAmountRent.Text = data.txtAmountRent;
                txtAmountCharge.Text = data.txtAmountCharge;
                ddlDurée.SelectedValue = data.ddlDurée;
                chk1.Checked = data.chk1.TransformToBoolean();
                chk2.Checked = data.chk2.TransformToBoolean();
                chk3.Checked = data.chk3.TransformToBoolean();
                chk4.Checked = data.chk4.TransformToBoolean();
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
            var ddl0Value = ddl0.SelectedValue;
            var range = new RangeCoordinates
            {
                Row = 5,
                Column = 4,
                Height = 11,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                    new object[] {ddl0Value},
                    new object[] {txtAmountRent.Text},
                    new object[] {txtAmountCharge.Text},
                    new object[] {ddlDurée.SelectedValue},
                    new object[] {ddl0Value == "1" || ddl0Value == "2" || ddl0Value == "3" ? chk1.Checked.TransformToBooleanFr() : "FAUX"},
                    new object[] {chk2.Checked.TransformToBooleanFr()},
                    new object[] {ddl0Value == "5" ? chk3.Checked.TransformToBooleanFr() : "FAUX"},
                    new object[] {ddl0Value == "1" || ddl0Value == "2" || ddl0Value == "3" || ddl0Value == "5" ? chk4.Checked.TransformToBooleanFr() : "FAUX"},
                    new object[]{txtEmolument_de_formalités_HT.Text},
                    new object[]{txtDébours.Text},
                    new object[]{hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            var html = "";
            const string hidden = "0";
            //TOTAL DES DROITS ET FRAIS
            lblTrésor.InnerText = dt.Rows[0][3].ToString();
            lblDébours.InnerText = dt.Rows[1][3].ToString();
            lblEmoluments_TTC_du_notaire.InnerText = dt.Rows[2][3].ToString();
            lblFrais_droits_et_émoluments.InnerText = dt.Rows[3][3].ToString();
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblEmoluments_TTC_du_notaire.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblDébours.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblTrésor.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblFrais_droits_et_émoluments.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF03\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire(1)
            lblEmoluments_fixes.InnerText = dt.Rows[9][3].ToString();
            subEmoluments_du_notaire1.Visible = dt.Rows[9][6].ToString() != hidden;
            if (dt.Rows[9][6].ToString() != hidden)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments fixes :</td><td align='right'>{0}</td><td></td></tr>",
                    lblEmoluments_fixes.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments du notaire(2)
            lblRow11.InnerText = dt.Rows[12][3].ToString();
            lblRow12.InnerText = dt.Rows[12][4].ToString();
            lblRow13.InnerText = dt.Rows[12][5].ToString();
            divRow1.Visible = dt.Rows[12][6].ToString() != hidden;
            lblRow21.InnerText = dt.Rows[13][3].ToString();
            lblRow22.InnerText = dt.Rows[13][4].ToString();
            lblRow23.InnerText = dt.Rows[13][5].ToString();
            divRow2.Visible = dt.Rows[13][6].ToString() != hidden;
            lblRow31.InnerText = dt.Rows[14][3].ToString();
            lblRow32.InnerText = dt.Rows[14][4].ToString();
            lblRow33.InnerText = dt.Rows[14][5].ToString();
            divRow3.Visible = dt.Rows[14][6].ToString() != hidden;
            lblRow41.InnerText = dt.Rows[15][3].ToString();
            lblRow42.InnerText = dt.Rows[15][4].ToString();
            lblRow43.InnerText = dt.Rows[15][5].ToString();
            divRow4.Visible = dt.Rows[15][6].ToString() != hidden;
            lblRowTotal.InnerText = dt.Rows[17][4].ToString();
            divRowTotal.Visible = dt.Rows[17][6].ToString() != hidden;
            subEmoluments_du_notaire2.Visible = dt.Rows[11][6].ToString() != hidden;
            if (dt.Rows[11][6].ToString() != hidden)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                if (dt.Rows[12][6].ToString() != hidden)
                {
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow11.InnerText, lblRow12.InnerText, lblRow13.InnerText);
                }
                if (dt.Rows[13][6].ToString() != hidden)
                {
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow21.InnerText, lblRow22.InnerText, lblRow23.InnerText);
                }
                if (dt.Rows[14][6].ToString() != hidden)
                {
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow31.InnerText, lblRow32.InnerText, lblRow33.InnerText);
                }
                if (dt.Rows[15][6].ToString() != hidden)
                {
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow41.InnerText, lblRow42.InnerText, lblRow43.InnerText);
                }
                if (dt.Rows[17][6].ToString() != hidden)
                {
                    html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                    lblRowTotal.InnerText);
                }
                row121.Visible = dt.Rows[19][(int)ColumnBF03.Col4].ToString() != hidden;
                if (dt.Rows[19][(int)ColumnBF03.Col4].ToString() != hidden)
                {
                    H121.InnerText = dt.Rows[19][(int)ColumnBF03.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                        H121.InnerText);
                }
                row122.Visible = dt.Rows[20][(int)ColumnBF03.Col4].ToString() != hidden;
                if (dt.Rows[20][(int)ColumnBF03.Col4].ToString() != hidden)
                {
                    H122.InnerText = dt.Rows[20][(int)ColumnBF03.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                        H122.InnerText);
                }
                row123.Visible = dt.Rows[21][(int)ColumnBF03.Col4].ToString() != hidden;
                if (dt.Rows[21][(int)ColumnBF03.Col4].ToString() != hidden)
                {
                    H123.InnerText = dt.Rows[21][(int)ColumnBF03.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emolument fixe :</td><td align='right'>{0}</td><td></td></tr>",
                        H123.InnerText);
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments du notaire - Récapitulatif
            row125.Visible = dt.Rows[23][(int)ColumnBF03.Col4].ToString() != hidden;
            if (dt.Rows[23][(int)ColumnBF03.Col4].ToString() != hidden)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - Récapitulatif</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                H126.InnerText = dt.Rows[24][(int)ColumnBF03.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments du notaire (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    H126.InnerText);
                row127.Visible = dt.Rows[25][(int)ColumnBF03.Col4].ToString() != hidden;
                if (dt.Rows[25][(int)ColumnBF03.Col4].ToString() != hidden)
                {
                    H127.InnerText = dt.Rows[25][(int)ColumnBF03.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emoluments de caution (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                        H127.InnerText);
                }
                //H128.InnerText = dt.Rows[26][(int) ColumnBF03.Col3].ToString();
                //html += string.Format(
                //    "<tr><td colspan='2' align='right'>Emoluments de formalités :</td><td align='right'>{0}</td><td></td></tr>",
                //    H128.InnerText);
                H128.InnerText = dt.Rows[26][(int)ColumnBF03.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Montant total (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    H128.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H131.InnerText = dt.Rows[29][(int)ColumnBF03.Col3].ToString();
            H132.InnerText = dt.Rows[30][(int)ColumnBF03.Col3].ToString();
            H133.InnerText = dt.Rows[31][(int)ColumnBF03.Col3].ToString();
            H134.InnerText = dt.Rows[32][(int)ColumnBF03.Col3].ToString();
            H135.InnerText = dt.Rows[33][(int)ColumnBF03.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                H131.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités :</td><td align='right'>{0}</td><td></td></tr>",
                H132.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                H133.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                H134.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                H135.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H138.InnerText = dt.Rows[36][(int)ColumnBF03.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                H138.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row141.Visible = dt.Rows[39][(int)ColumnBF03.Col4].ToString() != hidden;
            if (dt.Rows[39][(int)ColumnBF03.Col4].ToString() != hidden)
            {
                H141.InnerText = dt.Rows[39][(int)ColumnBF03.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Présentation volontaire - Droit fixe (739 du CGI) :</td><td align='right'>{0}</td><td></td></tr>",
                    H141.InnerText);
            }
            row142.Visible = dt.Rows[40][(int)ColumnBF03.Col4].ToString() != hidden;
            if (dt.Rows[40][(int)ColumnBF03.Col4].ToString() != hidden)
            {
                html += "<tr><td colspan='4' align='center'>Pas de présentation - Exonération</td></tr>";
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF03-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 2, 41, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF03");
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
                           "','ddl0':'" + ddl0.SelectedValue + "','txtAmountRent':'" + txtAmountRent.Text +
                           "','txtAmountCharge':'" + txtAmountCharge.Text + "','ddlDurée':'" + ddlDurée.SelectedValue +
                           "','chk1':'" + chk1.Checked + "','chk2':'" + chk2.Checked + "','chk3':'" + chk3.Checked +
                           "','chk4':'" + chk4.Checked +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF03", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','ddl0':'" + ddl0.SelectedValue + "','txtAmountRent':'" + txtAmountRent.Text +
                           "','txtAmountCharge':'" + txtAmountCharge.Text + "','ddlDurée':'" + ddlDurée.SelectedValue +
                           "','chk1':'" + chk1.Checked + "','chk2':'" + chk2.Checked + "','chk3':'" + chk3.Checked +
                           "','chk4':'" + chk4.Checked +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF03", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF03", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF03", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
    enum ColumnBF03
    {
        Col1 = 3,
        Col2 = 4,
        Col3 = 5,
        Col4 = 6
    }

    class DataModelBF03
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string ddl0 { get; set; }
        public string txtAmountRent { get; set; }
        public string txtAmountCharge { get; set; }
        public string ddlDurée { get; set; }
        public string chk1 { get; set; }
        public string chk2 { get; set; }
        public string chk3 { get; set; }
        public string chk4 { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}