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
    public partial class BF10 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF10>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                //chk1.Checked = data.chk1.TransformToBoolean();
                chk2.Checked = data.chk2.TransformToBoolean();
                chk3.Checked = data.chk3.TransformToBoolean();
                ddl1.SelectedValue = data.ddl1;
                txtImmobilier_Article_1_Valeur_PP.Text = data.txtImmobilier_Article_1_Valeur_PP;
                txtImmobilier_Article_2_Valeur_PP.Text = data.txtImmobilier_Article_2_Valeur_PP;
                txtImmobilier_Article_3_Valeur_PP.Text = data.txtImmobilier_Article_3_Valeur_PP;
                txtImmobilier_Article_4_Valeur_PP.Text = data.txtImmobilier_Article_4_Valeur_PP;
                txtImmobilier_Article_5_Valeur_PP.Text = data.txtImmobilier_Article_5_Valeur_PP;
                txtImmobilier_Article_6_Valeur_PP.Text = data.txtImmobilier_Article_6_Valeur_PP;
                txtImmobilier_Article_7_Valeur_PP.Text = data.txtImmobilier_Article_7_Valeur_PP;
                txtImmobilier_Article_8_Valeur_PP.Text = data.txtImmobilier_Article_8_Valeur_PP;
                txtImmobilier_Article_9_Valeur_PP.Text = data.txtImmobilier_Article_9_Valeur_PP;
                txtImmobilier_Article_10_Valeur_PP.Text = data.txtImmobilier_Article_10_Valeur_PP;
                ddl2.SelectedValue = data.ddl2;
                txtMobilier_Article_1_Valeur_PP.Text = data.txtMobilier_Article_1_Valeur_PP;
                txtMobilier_Article_2_Valeur_PP.Text = data.txtMobilier_Article_2_Valeur_PP;
                txtMobilier_Article_3_Valeur_PP.Text = data.txtMobilier_Article_3_Valeur_PP;
                txtMobilier_Article_4_Valeur_PP.Text = data.txtMobilier_Article_4_Valeur_PP;
                txtMobilier_Article_5_Valeur_PP.Text = data.txtMobilier_Article_5_Valeur_PP;
                txtMobilier_Article_6_Valeur_PP.Text = data.txtMobilier_Article_6_Valeur_PP;
                txtMobilier_Article_7_Valeur_PP.Text = data.txtMobilier_Article_7_Valeur_PP;
                txtMobilier_Article_8_Valeur_PP.Text = data.txtMobilier_Article_8_Valeur_PP;
                txtMobilier_Article_9_Valeur_PP.Text = data.txtMobilier_Article_9_Valeur_PP;
                txtMobilier_Article_10_Valeur_PP.Text = data.txtMobilier_Article_10_Valeur_PP;
                ddl3.SelectedValue = data.ddl3;
                txtPassif_Article_1_Valeur.Text = data.txtPassif_Article_1_Valeur;
                txtPassif_Article_2_Valeur.Text = data.txtPassif_Article_2_Valeur;
                txtPassif_Article_3_Valeur.Text = data.txtPassif_Article_3_Valeur;
                txtPassif_Article_4_Valeur.Text = data.txtPassif_Article_4_Valeur;
                txtPassif_Article_5_Valeur.Text = data.txtPassif_Article_5_Valeur;
                txtPassif_Article_6_Valeur.Text = data.txtPassif_Article_6_Valeur;
                txtPassif_Article_7_Valeur.Text = data.txtPassif_Article_7_Valeur;
                txtPassif_Article_8_Valeur.Text = data.txtPassif_Article_8_Valeur;
                txtPassif_Article_9_Valeur.Text = data.txtPassif_Article_9_Valeur;
                txtPassif_Article_10_Valeur.Text = data.txtPassif_Article_10_Valeur;
                txtEmolument_de_formalités_HT.Text = data.txtEmolument_de_formalités_HT;
                txtDébours.Text = data.txtDébours;
                hdUtilisation_du_futur_tarif.Value = data.chkUtilisation_du_futur_tarif;
                txtZone01.Text = data.txtZone01;
                ddl01.SelectedValue = data.ddl01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SetRange()
        {
            var range = new RangeCoordinates
            {
                Row = 4,
                Column = 4,
                Height = 8,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtZone01.Text},
                        new object[] {ddl01.SelectedValue},
                        new object[] {"FAUX"},
                        new object[] {chk2.Checked.TransformToBooleanFr()},
                        new object[] {chk3.Checked.TransformToBooleanFr()},
                        new object[] {ddl1.SelectedValue},
                        new object[] {ddl2.SelectedValue},
                        new object[] {ddl3.SelectedValue}
                });

            range = new RangeCoordinates()
            {
                Row = 14,
                Column = 4,
                Height = 10,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[]{txtImmobilier_Article_1_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_2_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_3_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_4_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_5_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_6_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_7_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_8_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_9_Valeur_PP.Text},
                        new object[]{txtImmobilier_Article_10_Valeur_PP.Text}
                });

            range = new RangeCoordinates()
            {
                Row = 27,
                Column = 4,
                Height = 10,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[]{txtMobilier_Article_1_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_2_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_3_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_4_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_5_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_6_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_7_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_8_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_9_Valeur_PP.Text},
                        new object[]{txtMobilier_Article_10_Valeur_PP.Text}
                });

            range = new RangeCoordinates()
            {
                Row = 40,
                Column = 4,
                Height = 14,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[]{txtPassif_Article_1_Valeur.Text},
                        new object[]{txtPassif_Article_2_Valeur.Text},
                        new object[]{txtPassif_Article_3_Valeur.Text},
                        new object[]{txtPassif_Article_4_Valeur.Text},
                        new object[]{txtPassif_Article_5_Valeur.Text},
                        new object[]{txtPassif_Article_6_Valeur.Text},
                        new object[]{txtPassif_Article_7_Valeur.Text},
                        new object[]{txtPassif_Article_8_Valeur.Text},
                        new object[]{txtPassif_Article_9_Valeur.Text},
                        new object[]{txtPassif_Article_10_Valeur.Text},
                        new object[]{""},
                        new object[]{txtEmolument_de_formalités_HT.Text},
                        new object[]{txtDébours.Text},
                        new object[]{hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            lblF102.InnerText = dt.Rows[0][(int)ColumnBF10.Col1].ToString();
            lblF103.InnerText = dt.Rows[1][(int)ColumnBF10.Col1].ToString();
            lblF104.InnerText = dt.Rows[2][(int)ColumnBF10.Col1].ToString();
            lblF106.InnerText = dt.Rows[4][(int)ColumnBF10.Col1].ToString();

            lblF112.InnerText = dt.Rows[10][(int)ColumnBF10.Col1].ToString();
            lblG112.InnerText = dt.Rows[10][(int)ColumnBF10.Col2].ToString();
            lblH112.InnerText = dt.Rows[10][(int)ColumnBF10.Col3].ToString();
            div112.Visible = dt.Rows[10][(int)ColumnBF10.Col4].ToString() == show;
            lblF113.InnerText = dt.Rows[11][(int)ColumnBF10.Col1].ToString();
            lblG113.InnerText = dt.Rows[11][(int)ColumnBF10.Col2].ToString();
            lblH113.InnerText = dt.Rows[11][(int)ColumnBF10.Col3].ToString();
            div113.Visible = dt.Rows[11][(int)ColumnBF10.Col4].ToString() == show;
            lblF114.InnerText = dt.Rows[12][(int)ColumnBF10.Col1].ToString();
            lblG114.InnerText = dt.Rows[12][(int)ColumnBF10.Col2].ToString();
            lblH114.InnerText = dt.Rows[12][(int)ColumnBF10.Col3].ToString();
            div114.Visible = dt.Rows[12][(int)ColumnBF10.Col4].ToString() == show;
            lblF115.InnerText = dt.Rows[13][(int)ColumnBF10.Col1].ToString();
            lblG115.InnerText = dt.Rows[13][(int)ColumnBF10.Col2].ToString();
            lblH115.InnerText = dt.Rows[13][(int)ColumnBF10.Col3].ToString();
            div115.Visible = dt.Rows[13][(int)ColumnBF10.Col4].ToString() == show;
            lblG117.InnerText = dt.Rows[15][(int)ColumnBF10.Col2].ToString();
            div117.Visible = dt.Rows[15][(int)ColumnBF10.Col4].ToString() == show;
            lblH118.InnerText = dt.Rows[16][(int)ColumnBF10.Col3].ToString();
            div118.Visible = dt.Rows[16][(int)ColumnBF10.Col4].ToString() == show;
            lblH120.InnerText = dt.Rows[18][(int)ColumnBF10.Col3].ToString();
            div120.Visible = dt.Rows[18][(int)ColumnBF10.Col4].ToString() == show;

            var html = "";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF104.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF103.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF102.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF106.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF10\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-121</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            if (dt.Rows[10][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblF112.InnerText, lblG112.InnerText, lblH112.InnerText);
            }
            if (dt.Rows[11][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblF113.InnerText, lblG113.InnerText, lblH113.InnerText);
            }
            if (dt.Rows[12][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblF114.InnerText, lblG114.InnerText, lblH114.InnerText);
            }
            if (dt.Rows[13][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblF115.InnerText, lblG115.InnerText, lblH115.InnerText);
            }
            if (dt.Rows[15][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                    lblG117.InnerText);
            }
            if (dt.Rows[16][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total des émoluments réglementés :</td><td align='right'>{0}</td><td></td></tr>",
                    lblH118.InnerText);
            }
            if (dt.Rows[18][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                    lblH120.InnerText);
            }
            row122.Visible = dt.Rows[20][(int)ColumnBF10.Col4].ToString() == show;
            if (dt.Rows[20][(int)ColumnBF10.Col4].ToString() == show)
            {
                H122.InnerText = dt.Rows[20][(int)ColumnBF10.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Dépôt Art.229-1 du Code civil - C.com A444-173-1 Tableau 5-222 :</td><td align='right'>{0}</td><td></td></tr>",
                    H122.InnerText);
            }
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H127.InnerText = dt.Rows[25][(int)ColumnBF10.Col3].ToString();
            H128.InnerText = dt.Rows[26][(int)ColumnBF10.Col3].ToString();
            H129.InnerText = dt.Rows[27][(int)ColumnBF10.Col3].ToString();
            H130.InnerText = dt.Rows[28][(int)ColumnBF10.Col3].ToString();
            H131.InnerText = dt.Rows[29][(int)ColumnBF10.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                H127.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                H128.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                H129.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                H130.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                H131.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H135.InnerText = dt.Rows[33][(int)ColumnBF10.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                H135.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row138.Visible = dt.Rows[36][(int)ColumnBF10.Col4].ToString() == show;
            if (dt.Rows[36][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>Taxe de publicité foncière :</td></tr>";
                row139.Visible = dt.Rows[37][(int)ColumnBF10.Col4].ToString() == show;
                if (dt.Rows[37][(int)ColumnBF10.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='center'>Exonération (CGI art. 1090 A-I)</td></tr>";
                }
                row140.Visible = dt.Rows[38][(int)ColumnBF10.Col4].ToString() == show;
                if (dt.Rows[38][(int)ColumnBF10.Col4].ToString() == show)
                {
                    lblF140.InnerText = dt.Rows[38][(int)ColumnBF10.Col1].ToString();
                    lblG140.InnerText = dt.Rows[38][(int)ColumnBF10.Col2].ToString();
                    lblH140.InnerText = dt.Rows[38][(int)ColumnBF10.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblF140.InnerText, lblG140.InnerText, lblH140.InnerText);
                }
            }
            row142.Visible = dt.Rows[40][(int)ColumnBF10.Col4].ToString() == show;
            if (dt.Rows[40][(int)ColumnBF10.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>CSI (art. 879 du CGI) :</td></tr>";
                lblF143.InnerText = dt.Rows[41][(int)ColumnBF10.Col1].ToString();
                lblG143.InnerText = dt.Rows[41][(int)ColumnBF10.Col2].ToString();
                lblH143.InnerText = dt.Rows[41][(int)ColumnBF10.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblF143.InnerText, lblG143.InnerText, lblH143.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF10-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 5, 43, 4, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF10");
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
                           "','chk1':'" + "False" + "','chk2':'" + chk2.Checked + "','chk3':'" + chk3.Checked +
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','txtImmobilier_Article_1_Valeur_PP':'" + txtImmobilier_Article_1_Valeur_PP.Text +
                           "','txtImmobilier_Article_2_Valeur_PP':'" + txtImmobilier_Article_2_Valeur_PP.Text +
                           "','txtImmobilier_Article_3_Valeur_PP':'" + txtImmobilier_Article_3_Valeur_PP.Text +
                           "','txtImmobilier_Article_4_Valeur_PP':'" + txtImmobilier_Article_4_Valeur_PP.Text +
                           "','txtImmobilier_Article_5_Valeur_PP':'" + txtImmobilier_Article_5_Valeur_PP.Text +
                           "','txtImmobilier_Article_6_Valeur_PP':'" + txtImmobilier_Article_6_Valeur_PP.Text +
                           "','txtImmobilier_Article_7_Valeur_PP':'" + txtImmobilier_Article_7_Valeur_PP.Text +
                           "','txtImmobilier_Article_8_Valeur_PP':'" + txtImmobilier_Article_8_Valeur_PP.Text +
                           "','txtImmobilier_Article_9_Valeur_PP':'" + txtImmobilier_Article_9_Valeur_PP.Text +
                           "','txtImmobilier_Article_10_Valeur_PP':'" + txtImmobilier_Article_10_Valeur_PP.Text +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','txtMobilier_Article_1_Valeur_PP':'" + txtMobilier_Article_1_Valeur_PP.Text +
                           "','txtMobilier_Article_2_Valeur_PP':'" + txtMobilier_Article_2_Valeur_PP.Text +
                           "','txtMobilier_Article_3_Valeur_PP':'" + txtMobilier_Article_3_Valeur_PP.Text +
                           "','txtMobilier_Article_4_Valeur_PP':'" + txtMobilier_Article_4_Valeur_PP.Text +
                           "','txtMobilier_Article_5_Valeur_PP':'" + txtMobilier_Article_5_Valeur_PP.Text +
                           "','txtMobilier_Article_6_Valeur_PP':'" + txtMobilier_Article_6_Valeur_PP.Text +
                           "','txtMobilier_Article_7_Valeur_PP':'" + txtMobilier_Article_7_Valeur_PP.Text +
                           "','txtMobilier_Article_8_Valeur_PP':'" + txtMobilier_Article_8_Valeur_PP.Text +
                           "','txtMobilier_Article_9_Valeur_PP':'" + txtMobilier_Article_9_Valeur_PP.Text +
                           "','txtMobilier_Article_10_Valeur_PP':'" + txtMobilier_Article_10_Valeur_PP.Text +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtPassif_Article_1_Valeur':'" + txtPassif_Article_1_Valeur.Text +
                           "','txtPassif_Article_2_Valeur':'" + txtPassif_Article_2_Valeur.Text +
                           "','txtPassif_Article_3_Valeur':'" + txtPassif_Article_3_Valeur.Text +
                           "','txtPassif_Article_4_Valeur':'" + txtPassif_Article_4_Valeur.Text +
                           "','txtPassif_Article_5_Valeur':'" + txtPassif_Article_5_Valeur.Text +
                           "','txtPassif_Article_6_Valeur':'" + txtPassif_Article_6_Valeur.Text +
                           "','txtPassif_Article_7_Valeur':'" + txtPassif_Article_7_Valeur.Text +
                           "','txtPassif_Article_8_Valeur':'" + txtPassif_Article_8_Valeur.Text +
                           "','txtPassif_Article_9_Valeur':'" + txtPassif_Article_9_Valeur.Text +
                           "','txtPassif_Article_10_Valeur':'" + txtPassif_Article_10_Valeur.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value +
                           "','txtZone01':'" + txtZone01.Text +
                           "','ddl01':'" + ddl01.SelectedValue +
                           "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF10", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chk1':'" + "False" + "','chk2':'" + chk2.Checked + "','chk3':'" + chk3.Checked +
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','txtImmobilier_Article_1_Valeur_PP':'" + txtImmobilier_Article_1_Valeur_PP.Text +
                           "','txtImmobilier_Article_2_Valeur_PP':'" + txtImmobilier_Article_2_Valeur_PP.Text +
                           "','txtImmobilier_Article_3_Valeur_PP':'" + txtImmobilier_Article_3_Valeur_PP.Text +
                           "','txtImmobilier_Article_4_Valeur_PP':'" + txtImmobilier_Article_4_Valeur_PP.Text +
                           "','txtImmobilier_Article_5_Valeur_PP':'" + txtImmobilier_Article_5_Valeur_PP.Text +
                           "','txtImmobilier_Article_6_Valeur_PP':'" + txtImmobilier_Article_6_Valeur_PP.Text +
                           "','txtImmobilier_Article_7_Valeur_PP':'" + txtImmobilier_Article_7_Valeur_PP.Text +
                           "','txtImmobilier_Article_8_Valeur_PP':'" + txtImmobilier_Article_8_Valeur_PP.Text +
                           "','txtImmobilier_Article_9_Valeur_PP':'" + txtImmobilier_Article_9_Valeur_PP.Text +
                           "','txtImmobilier_Article_10_Valeur_PP':'" + txtImmobilier_Article_10_Valeur_PP.Text +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','txtMobilier_Article_1_Valeur_PP':'" + txtMobilier_Article_1_Valeur_PP.Text +
                           "','txtMobilier_Article_2_Valeur_PP':'" + txtMobilier_Article_2_Valeur_PP.Text +
                           "','txtMobilier_Article_3_Valeur_PP':'" + txtMobilier_Article_3_Valeur_PP.Text +
                           "','txtMobilier_Article_4_Valeur_PP':'" + txtMobilier_Article_4_Valeur_PP.Text +
                           "','txtMobilier_Article_5_Valeur_PP':'" + txtMobilier_Article_5_Valeur_PP.Text +
                           "','txtMobilier_Article_6_Valeur_PP':'" + txtMobilier_Article_6_Valeur_PP.Text +
                           "','txtMobilier_Article_7_Valeur_PP':'" + txtMobilier_Article_7_Valeur_PP.Text +
                           "','txtMobilier_Article_8_Valeur_PP':'" + txtMobilier_Article_8_Valeur_PP.Text +
                           "','txtMobilier_Article_9_Valeur_PP':'" + txtMobilier_Article_9_Valeur_PP.Text +
                           "','txtMobilier_Article_10_Valeur_PP':'" + txtMobilier_Article_10_Valeur_PP.Text +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtPassif_Article_1_Valeur':'" + txtPassif_Article_1_Valeur.Text +
                           "','txtPassif_Article_2_Valeur':'" + txtPassif_Article_2_Valeur.Text +
                           "','txtPassif_Article_3_Valeur':'" + txtPassif_Article_3_Valeur.Text +
                           "','txtPassif_Article_4_Valeur':'" + txtPassif_Article_4_Valeur.Text +
                           "','txtPassif_Article_5_Valeur':'" + txtPassif_Article_5_Valeur.Text +
                           "','txtPassif_Article_6_Valeur':'" + txtPassif_Article_6_Valeur.Text +
                           "','txtPassif_Article_7_Valeur':'" + txtPassif_Article_7_Valeur.Text +
                           "','txtPassif_Article_8_Valeur':'" + txtPassif_Article_8_Valeur.Text +
                           "','txtPassif_Article_9_Valeur':'" + txtPassif_Article_9_Valeur.Text +
                           "','txtPassif_Article_10_Valeur':'" + txtPassif_Article_10_Valeur.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value +
                           "','txtZone01':'" + txtZone01.Text +
                           "','ddl01':'" + ddl01.SelectedValue +
                           "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF10", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF10", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF10", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF10
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2,
        Col4 = 3
    }

    class DataModelBF10
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string chk1 { get; set; }
        public string chk2 { get; set; }
        public string chk3 { get; set; }
        public string txtZone01 { get; set; }
        public string ddl01 { get; set; }
        public string ddl1 { get; set; }
        public string ddl2 { get; set; }
        public string ddl3 { get; set; }
        public string txtImmobilier_Article_1_Valeur_PP { get; set; }
        public string txtImmobilier_Article_2_Valeur_PP { get; set; }
        public string txtImmobilier_Article_3_Valeur_PP { get; set; }
        public string txtImmobilier_Article_4_Valeur_PP { get; set; }
        public string txtImmobilier_Article_5_Valeur_PP { get; set; }
        public string txtImmobilier_Article_6_Valeur_PP { get; set; }
        public string txtImmobilier_Article_7_Valeur_PP { get; set; }
        public string txtImmobilier_Article_8_Valeur_PP { get; set; }
        public string txtImmobilier_Article_9_Valeur_PP { get; set; }
        public string txtImmobilier_Article_10_Valeur_PP { get; set; }
        public string txtMobilier_Article_1_Valeur_PP { get; set; }
        public string txtMobilier_Article_2_Valeur_PP { get; set; }
        public string txtMobilier_Article_3_Valeur_PP { get; set; }
        public string txtMobilier_Article_4_Valeur_PP { get; set; }
        public string txtMobilier_Article_5_Valeur_PP { get; set; }
        public string txtMobilier_Article_6_Valeur_PP { get; set; }
        public string txtMobilier_Article_7_Valeur_PP { get; set; }
        public string txtMobilier_Article_8_Valeur_PP { get; set; }
        public string txtMobilier_Article_9_Valeur_PP { get; set; }
        public string txtMobilier_Article_10_Valeur_PP { get; set; }
        public string txtPassif_Article_1_Valeur { get; set; }
        public string txtPassif_Article_2_Valeur { get; set; }
        public string txtPassif_Article_3_Valeur { get; set; }
        public string txtPassif_Article_4_Valeur { get; set; }
        public string txtPassif_Article_5_Valeur { get; set; }
        public string txtPassif_Article_6_Valeur { get; set; }
        public string txtPassif_Article_7_Valeur { get; set; }
        public string txtPassif_Article_8_Valeur { get; set; }
        public string txtPassif_Article_9_Valeur { get; set; }
        public string txtPassif_Article_10_Valeur { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}