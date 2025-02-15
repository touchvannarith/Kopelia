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
    public partial class BF09 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF09>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                txtNoms1.Text = data.txtNoms1;
                txtNoms2.Text = data.txtNoms2;
                ddl01.SelectedValue = data.ddl01;
                ddl02.SelectedValue = data.ddl02;
                txtZone01.Text = data.txtZone01;
                txtZone02.Text = data.txtZone02;
                txtZone03.Text = data.txtZone03;
                txtZone04.Text = data.txtZone04;
                hdUtilisation_du_futur_tarif.Value = data.chkUtilisation_du_futur_tarif;
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
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates() { Row = 9, Column = 3, Height = 7, Width = 1 }, new object[]
                {
                        new object[] {ddl01.SelectedValue},
                        new object[] {txtZone01.Text},
                        new object[] {ddl02.SelectedValue},
                        new object[] {txtZone02.Text},
                        new object[] {txtZone03.Text},
                        new object[] {txtZone04.Text},
                        new object[] {hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            var html = "";
            const string show = "1";

            //Total des droits et frais
            F42.InnerText = dt.Rows[38][(int)ColumnBF09.Col2].ToString();
            F43.InnerText = dt.Rows[39][(int)ColumnBF09.Col2].ToString();
            F44.InnerText = dt.Rows[40][(int)ColumnBF09.Col2].ToString();
            F45.InnerText = dt.Rows[41][(int)ColumnBF09.Col2].ToString();
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td width='700' bgcolor='#304F73' valign='middle' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F44.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F43.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F42.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F45.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF09\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td width='700' bgcolor='#304F73' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            row6.Visible = false;
            if (dt.Rows[0][(int)ColumnBF09.Col4].ToString() == show)
            {
                row6.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-117</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row8.Visible = false;
                if (dt.Rows[2][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row8.Visible = true;
                    E8.InnerText = dt.Rows[2][(int)ColumnBF09.Col1].ToString();
                    F8.InnerText = dt.Rows[2][(int)ColumnBF09.Col2].ToString();
                    G8.InnerText = dt.Rows[2][(int)ColumnBF09.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E8.InnerText, F8.InnerText, G8.InnerText);
                }
                row9.Visible = false;
                if (dt.Rows[3][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row9.Visible = true;
                    E9.InnerText = dt.Rows[3][(int)ColumnBF09.Col1].ToString();
                    F9.InnerText = dt.Rows[3][(int)ColumnBF09.Col2].ToString();
                    G9.InnerText = dt.Rows[3][(int)ColumnBF09.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E9.InnerText, F9.InnerText, G9.InnerText);
                }
                row10.Visible = false;
                if (dt.Rows[4][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row10.Visible = true;
                    E10.InnerText = dt.Rows[4][(int)ColumnBF09.Col1].ToString();
                    F10.InnerText = dt.Rows[4][(int)ColumnBF09.Col2].ToString();
                    G10.InnerText = dt.Rows[4][(int)ColumnBF09.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E10.InnerText, F10.InnerText, G10.InnerText);
                }
                row11.Visible = false;
                if (dt.Rows[5][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row11.Visible = true;
                    E11.InnerText = dt.Rows[5][(int)ColumnBF09.Col1].ToString();
                    F11.InnerText = dt.Rows[5][(int)ColumnBF09.Col2].ToString();
                    G11.InnerText = dt.Rows[5][(int)ColumnBF09.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E11.InnerText, F11.InnerText, G11.InnerText);
                }
                row12.Visible = false;
                if (dt.Rows[6][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row12.Visible = true;
                    F12.InnerText = dt.Rows[6][(int)ColumnBF09.Col2].ToString();
                    html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                        F12.InnerText);
                }
                row14.Visible = false;
                if (dt.Rows[8][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row14.Visible = true;
                    G14.InnerText = dt.Rows[8][(int)ColumnBF09.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                        G14.InnerText);
                }
                G17.InnerText = dt.Rows[10][(int)ColumnBF09.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument réglementé :</td><td align='right'>{0}</td><td></td></tr>",
                    G17.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
                //Récapitulatif et calcul de la TVA
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                G19.InnerText = dt.Rows[13][(int)ColumnBF09.Col3].ToString();
                G20.InnerText = dt.Rows[14][(int)ColumnBF09.Col3].ToString();
                G21.InnerText = dt.Rows[15][(int)ColumnBF09.Col3].ToString();
                G22.InnerText = dt.Rows[16][(int)ColumnBF09.Col3].ToString();
                G23.InnerText = dt.Rows[17][(int)ColumnBF09.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    G19.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    G20.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G21.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    G22.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    G23.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
                //Débours
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                G27.InnerText = dt.Rows[21][(int)ColumnBF09.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    G27.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Trésor public
            row29.Visible = false;
            if (dt.Rows[23][(int)ColumnBF09.Col4].ToString() == show)
            {
                row29.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row31.Visible = false;
                if (dt.Rows[25][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row31.Visible = true;
                    html += "<tr><td colspan='4' align='left'>Droit d'échange :</td></tr>";
                    row32.Visible = false;
                    if (dt.Rows[26][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row32.Visible = true;
                        E32.InnerText = dt.Rows[26][(int)ColumnBF09.Col1].ToString();
                        F32.InnerText = dt.Rows[26][(int)ColumnBF09.Col2].ToString();
                        G32.InnerText = dt.Rows[26][(int)ColumnBF09.Col3].ToString();
                        html += string.Format(
                            "<tr><td colspan='2' align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            E32.InnerText, F32.InnerText, G32.InnerText);
                    }
                    row33.Visible = false;
                    if (dt.Rows[27][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row33.Visible = true;
                        html += "<tr><td colspan='4' align='center'>Exonération du droit d'échange</td></tr>";
                    }
                }
                row35.Visible = false;
                if (dt.Rows[29][(int)ColumnBF09.Col4].ToString() == show)
                {
                    row35.Visible = true;
                    html += "<tr><td colspan='4' align='left'>Droit de mutation :</td></tr>";
                    row36.Visible = false;
                    if (dt.Rows[30][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row36.Visible = true;
                        E36.InnerText = dt.Rows[30][(int)ColumnBF09.Col1].ToString();
                        F36.InnerText = dt.Rows[30][(int)ColumnBF09.Col2].ToString();
                        G36.InnerText = dt.Rows[30][(int)ColumnBF09.Col3].ToString();
                        html += string.Format(
                            "<tr><td>Taxe départementale :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            E36.InnerText, F36.InnerText, G36.InnerText);
                    }
                    row37.Visible = false;
                    if (dt.Rows[31][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row37.Visible = true;
                        E37.InnerText = dt.Rows[31][(int)ColumnBF09.Col1].ToString();
                        F37.InnerText = dt.Rows[31][(int)ColumnBF09.Col2].ToString();
                        G37.InnerText = dt.Rows[31][(int)ColumnBF09.Col3].ToString();
                        html += string.Format(
                            "<tr><td>Prélèvement de l'état sur taxe départementale :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            E37.InnerText, F37.InnerText, G37.InnerText);
                    }
                    row38.Visible = false;
                    if (dt.Rows[32][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row38.Visible = true;
                        E38.InnerText = dt.Rows[32][(int)ColumnBF09.Col1].ToString();
                        F38.InnerText = dt.Rows[32][(int)ColumnBF09.Col2].ToString();
                        G38.InnerText = dt.Rows[32][(int)ColumnBF09.Col3].ToString();
                        html += string.Format(
                            "<tr><td>Taxe locale :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            E38.InnerText, F38.InnerText, G38.InnerText);
                    }
                    html += "<tr><td colspan='4' align='left'>CSI :</td></tr>";
                    row41.Visible = false;
                    if (dt.Rows[35][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row41.Visible = true;
                        E41.InnerText = dt.Rows[35][(int)ColumnBF09.Col1].ToString();
                        F41.InnerText = dt.Rows[35][(int)ColumnBF09.Col2].ToString();
                        G41.InnerText = dt.Rows[35][(int)ColumnBF09.Col3].ToString();
                        html += string.Format(
                            "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            E41.InnerText, F41.InnerText, G41.InnerText);
                    }
                    row42.Visible = false;
                    if (dt.Rows[36][(int)ColumnBF09.Col4].ToString() == show)
                    {
                        row42.Visible = true;
                        html += "<tr><td colspan='4' align='center'>Exonération totale.</td></tr>";
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF09-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 5, 3, 43, 5, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF09");
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
                           "','txtNoms1':'" + txtNoms1.Text + "','ddl01':'" + ddl01.SelectedValue + "','txtZone01':'" +
                           txtZone01.Text +
                           "','txtNoms2':'" + txtNoms2.Text + "','ddl02':'" + ddl02.SelectedValue + "','txtZone02':'" +
                           txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text + "','txtZone04':'" + txtZone04.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF09", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','txtNoms1':'" + txtNoms1.Text + "','ddl01':'" + ddl01.SelectedValue + "','txtZone01':'" +
                           txtZone01.Text +
                           "','txtNoms2':'" + txtNoms2.Text + "','ddl02':'" + ddl02.SelectedValue + "','txtZone02':'" +
                           txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text + "','txtZone04':'" + txtZone04.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF09", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF09", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF09", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF09
    {
        Col1 = 1,
        Col2 = 2,
        Col3 = 3,
        Col4 = 4
    }

    class DataModelBF09
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string ddl01 { get; set; }
        public string ddl02 { get; set; }
        public string txtNoms1 { get; set; }
        public string txtNoms2 { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}