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
    public partial class BF02_5 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF02_5>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                chkContratDeMariage.Checked = data.chkContratDeMariage.TransformToBoolean();
                txtValueContratDeMariage1.Text = data.txtValueContratDeMariage1;
                txtValueContratDeMariage2.Text = data.txtValueContratDeMariage2;
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
                            new object[] {5}
                        });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                        new RangeCoordinates() { Row = 14, Column = 5, Height = 6, Width = 2 }, new object[]
                        {
                            new object[] {chkContratDeMariage.Checked.TransformToBooleanFr(), ""},
                            new object[] {txtValueContratDeMariage1.Text, ""},
                            new object[] {txtValueContratDeMariage2.Text, ""},
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
            D240.InnerText = dt.Rows[38][(int)ColumnBF02_5.Col1].ToString();
            D241.InnerText = dt.Rows[39][(int)ColumnBF02_5.Col1].ToString();
            D242.InnerText = dt.Rows[40][(int)ColumnBF02_5.Col1].ToString();
            D243.InnerText = dt.Rows[41][(int)ColumnBF02_5.Col1].ToString();
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D240.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D241.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D242.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    D243.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF02-5\\chart.png");
            //DÉTAIL DES FRAIS
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-82</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row207.Visible = false;
            if (dt.Rows[5][(int)ColumnBF02_5.Col4].ToString() == show)
            {
                row207.Visible = true;
                lblRow11.InnerText = dt.Rows[5][(int)ColumnBF02_5.Col1].ToString();
                lblRow12.InnerText = dt.Rows[5][(int)ColumnBF02_5.Col2].ToString();
                lblRow13.InnerText = dt.Rows[5][(int)ColumnBF02_5.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow11.InnerText, lblRow12.InnerText, lblRow13.InnerText);
            }
            row208.Visible = false;
            if (dt.Rows[6][(int)ColumnBF02_5.Col4].ToString() == show)
            {
                row208.Visible = true;
                lblRow21.InnerText = dt.Rows[6][(int)ColumnBF02_5.Col1].ToString();
                lblRow22.InnerText = dt.Rows[6][(int)ColumnBF02_5.Col2].ToString();
                lblRow23.InnerText = dt.Rows[6][(int)ColumnBF02_5.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow21.InnerText, lblRow22.InnerText, lblRow23.InnerText);
            }
            row209.Visible = false;
            if (dt.Rows[7][(int)ColumnBF02_5.Col4].ToString() == show)
            {
                row209.Visible = true;
                lblRow31.InnerText = dt.Rows[7][(int)ColumnBF02_5.Col1].ToString();
                lblRow32.InnerText = dt.Rows[7][(int)ColumnBF02_5.Col2].ToString();
                lblRow33.InnerText = dt.Rows[7][(int)ColumnBF02_5.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow31.InnerText, lblRow32.InnerText, lblRow33.InnerText);
            }
            row210.Visible = false;
            if (dt.Rows[8][(int)ColumnBF02_5.Col4].ToString() == show)
            {
                row210.Visible = true;
                lblRow41.InnerText = dt.Rows[8][(int)ColumnBF02_5.Col1].ToString();
                lblRow42.InnerText = dt.Rows[8][(int)ColumnBF02_5.Col2].ToString();
                lblRow43.InnerText = dt.Rows[8][(int)ColumnBF02_5.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow41.InnerText, lblRow42.InnerText, lblRow43.InnerText);
            }
            row211.Visible = false;
            if (dt.Rows[9][(int)ColumnBF02_5.Col4].ToString() == show)
            {
                row211.Visible = true;
                lblTotal.InnerText = dt.Rows[9][(int)ColumnBF02_5.Col2].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                    lblTotal.InnerText);
            }
            row213.Visible = false;
            if (dt.Rows[11][(int)ColumnBF02_5.Col1].ToString() == show)
            {
                row213.Visible = true;
                html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
            }
            D215.InnerText = dt.Rows[13][(int)ColumnBF02_5.Col1].ToString();
            //D216.InnerText = dt.Rows[14][(int)ColumnBF02_5.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument réglementé :</td><td align='right'>{0}</td><td></td></tr>",
                    D215.InnerText);
            //html += string.Format(
            //        "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
            //        D216.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D218.InnerText = dt.Rows[16][(int)ColumnBF02_5.Col1].ToString();
            D219.InnerText = dt.Rows[17][(int)ColumnBF02_5.Col1].ToString();
            D220.InnerText = dt.Rows[18][(int)ColumnBF02_5.Col1].ToString();
            D221.InnerText = dt.Rows[19][(int)ColumnBF02_5.Col1].ToString();
            D222.InnerText = dt.Rows[20][(int)ColumnBF02_5.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    D218.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    D219.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    D220.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    D221.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    D222.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D226.InnerText = dt.Rows[24][(int)ColumnBF02_5.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    D226.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td colspan='4'>Enregistrement :</td></tr>";
            row230.Visible = false;
            if (dt.Rows[28][(int)ColumnBF02_5.Col2].ToString() == show)
            {
                row230.Visible = true;
                D230.InnerText = dt.Rows[28][(int)ColumnBF02_5.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Enregistrement :</td><td align='right'>{0}</td><td></td></tr>",
                    D230.InnerText);
            }
            html += "<tr><td colspan='4' align='center'>Acte enregistré gratuitement (CGI art 847-1)</td></tr>";
            row232.Visible = false;
            if (dt.Rows[30][(int)ColumnBF02_5.Col2].ToString() == show)
            {
                row232.Visible = true;
                row234.Visible = false;
                if (dt.Rows[32][(int)ColumnBF02_5.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='left'>Taxe de publicité :</td></tr>";
                    row234.Visible = true;
                    lblTaxeRow11.InnerText = dt.Rows[32][(int)ColumnBF02_5.Col1].ToString();
                    lblTaxeRow12.InnerText = dt.Rows[32][(int)ColumnBF02_5.Col2].ToString();
                    lblTaxeRow13.InnerText = dt.Rows[32][(int)ColumnBF02_5.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblTaxeRow11.InnerText, lblTaxeRow12.InnerText, lblTaxeRow13.InnerText);
                    row235.Visible = false;
                    if (dt.Rows[33][(int)ColumnBF02_5.Col1].ToString() == show)
                    {
                        row235.Visible = true;
                        html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
                    }
                }
                row237.Visible = false;
                if (dt.Rows[35][(int)ColumnBF02_5.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='left'>CSI (art. 879 du CGI) :</td></tr>";
                    row237.Visible = true;
                    lblCSIRow11.InnerText = dt.Rows[35][(int)ColumnBF02_5.Col1].ToString();
                    lblCSIRow12.InnerText = dt.Rows[35][(int)ColumnBF02_5.Col2].ToString();
                    lblCSIRow13.InnerText = dt.Rows[35][(int)ColumnBF02_5.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblCSIRow11.InnerText, lblCSIRow12.InnerText, lblCSIRow13.InnerText);
                    row238.Visible = false;
                    if (dt.Rows[36][(int)ColumnBF02_5.Col1].ToString() == show)
                    {
                        row238.Visible = true;
                        html += "<tr><td colspan='4' align='center'>Prise en compte de l'émolument minimum.</td></tr>";
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
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 201, 3, 42, 4, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF02-5");
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
                           "','chkContratDeMariage':'" + chkContratDeMariage.Checked + "','txtValueContratDeMariage1':'" +
                           txtValueContratDeMariage1.Text + "','txtValueContratDeMariage2':'" +
                           txtValueContratDeMariage2.Text +
                           "','txtNumPages':'" + txtNumPages.Text + "','txtNumCopies':'" + txtNumCopies.Text +
                           "','txtNumExp':'" + txtNumExp.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF02-5", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chkContratDeMariage':'" + chkContratDeMariage.Checked + "','txtValueContratDeMariage1':'" +
                           txtValueContratDeMariage1.Text + "','txtValueContratDeMariage2':'" +
                           txtValueContratDeMariage2.Text +
                           "','txtNumPages':'" + txtNumPages.Text + "','txtNumCopies':'" + txtNumCopies.Text +
                           "','txtNumExp':'" + txtNumExp.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF02-5", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF02-5", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF02-5", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF02_5
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2,
        Col4 = 3
    }

    class DataModelBF02_5
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string chkContratDeMariage { get; set; }
        public string txtValueContratDeMariage1 { get; set; }
        public string txtValueContratDeMariage2 { get; set; }
        public string txtNumPages { get; set; }
        public string txtNumCopies { get; set; }
        public string txtNumExp { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}