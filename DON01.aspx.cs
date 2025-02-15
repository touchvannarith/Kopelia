using Newtonsoft.Json.Linq;
using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using NotaliaOnline.Properties;
using NotaliaOnline.WebReference;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class DON01 : Page
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
                var json = JObject.Parse(obj.Value);
                txtLibelle.Text = obj.Libelle;
                txtDossier.Text = json["txtDossier"].ToString();
                txtDateSignature.Text = json["txtDateDeSignature"].ToString();
                txtRedacteur.Text = json["txtRedacteur"].ToString();
                txtZone01.Text = json["txtZone01"].ToString();
                chk1.Checked = json["chk1"].TransformToBoolean();
                txtZone02.Text = json["txtZone02"].ToString();
                ddl1.SelectedValue = json["ddl1"].ToString();
                ddlArticle1.SelectedValue = json["ddlArticle1"].ToString();
                chkArticle1.Checked = json["chkArticle1"].TransformToBoolean();
                valuePP1.Text = json["valuePP1"].ToString();
                ddlArticle2.SelectedValue = json["ddlArticle2"].ToString();
                chkArticle2.Checked = json["chkArticle2"].TransformToBoolean();
                valuePP2.Text = json["valuePP2"].ToString();
                ddlArticle3.SelectedValue = json["ddlArticle3"].ToString();
                chkArticle3.Checked = json["chkArticle3"].TransformToBoolean();
                valuePP3.Text = json["valuePP3"].ToString();
                ddlArticle4.SelectedValue = json["ddlArticle4"].ToString();
                chkArticle4.Checked = json["chkArticle4"].TransformToBoolean();
                valuePP4.Text = json["valuePP4"].ToString();
                ddlArticle5.SelectedValue = json["ddlArticle5"].ToString();
                chkArticle5.Checked = json["chkArticle5"].TransformToBoolean();
                valuePP5.Text = json["valuePP5"].ToString();
                ddlArticle6.SelectedValue = json["ddlArticle6"].ToString();
                chkArticle6.Checked = json["chkArticle6"].TransformToBoolean();
                valuePP6.Text = json["valuePP6"].ToString();
                ddlArticle7.SelectedValue = json["ddlArticle7"].ToString();
                chkArticle7.Checked = json["chkArticle7"].TransformToBoolean();
                valuePP7.Text = json["valuePP7"].ToString();
                ddlArticle8.SelectedValue = json["ddlArticle8"].ToString();
                chkArticle8.Checked = json["chkArticle8"].TransformToBoolean();
                valuePP8.Text = json["valuePP8"].ToString();
                ddlArticle9.SelectedValue = json["ddlArticle9"].ToString();
                chkArticle9.Checked = json["chkArticle9"].TransformToBoolean();
                valuePP9.Text = json["valuePP9"].ToString();
                ddlArticle10.SelectedValue = json["ddlArticle10"].ToString();
                chkArticle10.Checked = json["chkArticle10"].TransformToBoolean();
                valuePP10.Text = json["valuePP10"].ToString();
                txtEmolument_de_formalités_HT.Text = json["txtEmolument_de_formalités_HT"].ToString();
                txtDébours.Text = json["txtDébours"].ToString();
                hdUtilisation_du_futur_tarif.Value = json["chkUtilisation_du_futur_tarif"].ToString();
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
                Row = 5,
                Column = 4,
                Height = 4,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtZone01.Text},
                        new object[] {chk1.Checked ? "VRAI" : "FAUX"},
                        new object[] {txtZone02.Text},
                        new object[] {ddl1.SelectedValue}
                });
            range = new RangeCoordinates
            {
                Row = 12,
                Column = 3,
                Height = 10,
                Width = 3
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {ddlArticle1.SelectedValue, chkArticle1.Checked ? "VRAI" : "FAUX", valuePP1.Text},
                        new object[] {ddlArticle2.SelectedValue, chkArticle2.Checked ? "VRAI" : "FAUX", valuePP2.Text},
                        new object[] {ddlArticle3.SelectedValue, chkArticle3.Checked ? "VRAI" : "FAUX", valuePP3.Text},
                        new object[] {ddlArticle4.SelectedValue, chkArticle4.Checked ? "VRAI" : "FAUX", valuePP4.Text},
                        new object[] {ddlArticle5.SelectedValue, chkArticle5.Checked ? "VRAI" : "FAUX", valuePP5.Text},
                        new object[] {ddlArticle6.SelectedValue, chkArticle6.Checked ? "VRAI" : "FAUX", valuePP6.Text},
                        new object[] {ddlArticle7.SelectedValue, chkArticle7.Checked ? "VRAI" : "FAUX", valuePP7.Text},
                        new object[] {ddlArticle8.SelectedValue, chkArticle8.Checked ? "VRAI" : "FAUX", valuePP8.Text},
                        new object[] {ddlArticle9.SelectedValue, chkArticle9.Checked ? "VRAI" : "FAUX", valuePP9.Text},
                        new object[] {ddlArticle10.SelectedValue, chkArticle10.Checked ? "VRAI" : "FAUX", valuePP10.Text}
                });
            range = new RangeCoordinates
            {
                Row = 25,
                Column = 5,
                Height = 3,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtEmolument_de_formalités_HT.Text},
                        new object[] {txtDébours.Text},
                        new object[] {hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            var html = "";
            //Total des droits et frais
            F102.InnerText = dt.Rows[0][(int)ColumnDON01.Col1].ToString();
            F103.InnerText = dt.Rows[1][(int)ColumnDON01.Col1].ToString();
            F104.InnerText = dt.Rows[2][(int)ColumnDON01.Col1].ToString();
            F106.InnerText = dt.Rows[4][(int)ColumnDON01.Col1].ToString();
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F104.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F103.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F102.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    F106.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\DON01\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-67</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row112.Visible = false;
            if (dt.Rows[10][(int)ColumnDON01.Col4].ToString() == show)
            {
                row112.Visible = true;
                F112.InnerText = dt.Rows[10][(int)ColumnDON01.Col1].ToString();
                G112.InnerText = dt.Rows[10][(int)ColumnDON01.Col2].ToString();
                H112.InnerText = dt.Rows[10][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F112.InnerText, G112.InnerText, H112.InnerText);
            }
            row113.Visible = false;
            if (dt.Rows[11][(int)ColumnDON01.Col4].ToString() == show)
            {
                row113.Visible = true;
                F113.InnerText = dt.Rows[11][(int)ColumnDON01.Col1].ToString();
                G113.InnerText = dt.Rows[11][(int)ColumnDON01.Col2].ToString();
                H113.InnerText = dt.Rows[11][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F113.InnerText, G113.InnerText, H113.InnerText);
            }
            row114.Visible = false;
            if (dt.Rows[12][(int)ColumnDON01.Col4].ToString() == show)
            {
                row114.Visible = true;
                F114.InnerText = dt.Rows[12][(int)ColumnDON01.Col1].ToString();
                G114.InnerText = dt.Rows[12][(int)ColumnDON01.Col2].ToString();
                H114.InnerText = dt.Rows[12][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F114.InnerText, G114.InnerText, H114.InnerText);
            }
            row115.Visible = false;
            if (dt.Rows[13][(int)ColumnDON01.Col4].ToString() == show)
            {
                row115.Visible = true;
                F115.InnerText = dt.Rows[13][(int)ColumnDON01.Col1].ToString();
                G115.InnerText = dt.Rows[13][(int)ColumnDON01.Col2].ToString();
                H115.InnerText = dt.Rows[13][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F115.InnerText, G115.InnerText, H115.InnerText);
            }
            row117.Visible = false;
            if (dt.Rows[15][(int)ColumnDON01.Col4].ToString() == show)
            {
                row117.Visible = true;
                G117.InnerText = dt.Rows[15][(int)ColumnDON01.Col2].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                    G117.InnerText);
            }
            row118.Visible = false;
            if (dt.Rows[16][(int)ColumnDON01.Col4].ToString() == show)
            {
                row118.Visible = true;
                H118.InnerText = dt.Rows[16][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total Hors TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    H118.InnerText);
            }
            row120.Visible = false;
            if (dt.Rows[18][(int)ColumnDON01.Col4].ToString() == show)
            {
                row120.Visible = true;
                H120.InnerText = dt.Rows[18][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                    H120.InnerText);
            }
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H123.InnerText = dt.Rows[21][(int)ColumnDON01.Col3].ToString();
            H124.InnerText = dt.Rows[22][(int)ColumnDON01.Col3].ToString();
            H125.InnerText = dt.Rows[23][(int)ColumnDON01.Col3].ToString();
            H126.InnerText = dt.Rows[24][(int)ColumnDON01.Col3].ToString();
            H127.InnerText = dt.Rows[25][(int)ColumnDON01.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments du notaire HT :</td><td align='right'>{0}</td><td></td></tr>",
                H123.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                H124.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                H125.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                H126.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                H127.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H129.InnerText = dt.Rows[27][(int)ColumnDON01.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                H129.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Tresor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td colspan='4'>Détermination des bases taxables :</td></tr>";
            row138.Visible = false;
            if (dt.Rows[36][(int)ColumnDON01.Col4].ToString() == show)
            {
                row138.Visible = true;
                H138.InnerText = dt.Rows[36][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Rappel de la valeur en PP des biens immobiliers :</td><td align='right'>{0}</td><td></td></tr>",
                    H138.InnerText);
                row139.Visible = false;
                if (dt.Rows[37][(int)ColumnDON01.Col4].ToString() == show)
                {
                    row139.Visible = true;
                    H139.InnerText = dt.Rows[37][(int)ColumnDON01.Col3].ToString();
                    H140.InnerText = dt.Rows[38][(int)ColumnDON01.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Déduction de la valeur de l'usufruit des biens immobiliers :</td><td align='right'>{0}</td><td></td></tr>",
                        H139.InnerText);
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Base taxable immobilier :</td><td align='right'>{0}</td><td></td></tr>",
                        H140.InnerText);
                }
            }
            html += "<tr><td colspan='4'>Taxe de publicité :</td></tr>";
            row147.Visible = false;
            if (dt.Rows[45][(int)ColumnDON01.Col4].ToString() == show)
            {
                row147.Visible = true;
                F147.InnerText = dt.Rows[45][(int)ColumnDON01.Col1].ToString();
                G147.InnerText = dt.Rows[45][(int)ColumnDON01.Col2].ToString();
                H147.InnerText = dt.Rows[45][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F147.InnerText, G147.InnerText, H147.InnerText);
            }
            row148.Visible = false;
            if (dt.Rows[46][(int)ColumnDON01.Col4].ToString() == show)
            {
                row148.Visible = true;
                html += "<tr><td colspan='4' align='center'>Pour la TPF, il a été pris le minimum de perception soit 25 Euros.</td></tr>";
            }
            html += "<tr><td colspan='4'>CSI (art. 879 du CGI) :</td></tr>";
            row151.Visible = false;
            if (dt.Rows[49][(int)ColumnDON01.Col4].ToString() == show)
            {
                row151.Visible = true;
                F151.InnerText = dt.Rows[49][(int)ColumnDON01.Col1].ToString();
                G151.InnerText = dt.Rows[49][(int)ColumnDON01.Col2].ToString();
                H151.InnerText = dt.Rows[49][(int)ColumnDON01.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F151.InnerText, G151.InnerText, H151.InnerText);
            }
            row152.Visible = false;
            if (dt.Rows[50][(int)ColumnDON01.Col4].ToString() == show)
            {
                row152.Visible = true;
                html += "<tr><td colspan='4' align='center'>Pour la CSI, il a été pris le minimum de perception soit 15 Euros.</td></tr>";
            }
            html += "</table>";
            hdResult.Value = html;
        }

        protected void btnSynthese_Click(object sender, EventArgs e)
        {
            try
            {
                //txtLibelle.Text = string.IsNullOrEmpty(txtDossier.Text) ? txtLibelle.Text : txtDossier.Text;



                var watch = System.Diagnostics.Stopwatch.StartNew();
                _excelService = ExcelServiceHelper.ExcelServiceProvider();
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "DON01-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 2, 51, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "DON01");
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
                           "','txtZone01':'" + txtZone01.Text +
                           "','chk1':'" + chk1.Checked +
                           "','txtZone02':'" + txtZone02.Text +
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','ddlArticle1':'" + ddlArticle1.SelectedValue + "','chkArticle1':'" + chkArticle1.Checked +
                           "','valuePP1':'" + valuePP1.Text +
                           "','ddlArticle2':'" + ddlArticle2.SelectedValue + "','chkArticle2':'" + chkArticle2.Checked +
                           "','valuePP2':'" + valuePP2.Text +
                           "','ddlArticle3':'" + ddlArticle3.SelectedValue + "','chkArticle3':'" + chkArticle3.Checked +
                           "','valuePP3':'" + valuePP3.Text +
                           "','ddlArticle4':'" + ddlArticle4.SelectedValue + "','chkArticle4':'" + chkArticle4.Checked +
                           "','valuePP4':'" + valuePP4.Text +
                           "','ddlArticle5':'" + ddlArticle5.SelectedValue + "','chkArticle5':'" + chkArticle5.Checked +
                           "','valuePP5':'" + valuePP5.Text +
                           "','ddlArticle6':'" + ddlArticle6.SelectedValue + "','chkArticle6':'" + chkArticle6.Checked +
                           "','valuePP6':'" + valuePP6.Text +
                           "','ddlArticle7':'" + ddlArticle7.SelectedValue + "','chkArticle7':'" + chkArticle7.Checked +
                           "','valuePP7':'" + valuePP7.Text +
                           "','ddlArticle8':'" + ddlArticle8.SelectedValue + "','chkArticle8':'" + chkArticle8.Checked +
                           "','valuePP8':'" + valuePP8.Text +
                           "','ddlArticle9':'" + ddlArticle9.SelectedValue + "','chkArticle9':'" + chkArticle9.Checked +
                           "','valuePP9':'" + valuePP9.Text +
                           "','ddlArticle10':'" + ddlArticle10.SelectedValue + "','chkArticle10':'" +
                           chkArticle10.Checked + "','valuePP10':'" + valuePP10.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "DON01", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','txtZone01':'" + txtZone01.Text +
                           "','chk1':'" + chk1.Checked +
                           "','txtZone02':'" + txtZone02.Text +
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','ddlArticle1':'" + ddlArticle1.SelectedValue + "','chkArticle1':'" + chkArticle1.Checked +
                           "','valuePP1':'" + valuePP1.Text +
                           "','ddlArticle2':'" + ddlArticle2.SelectedValue + "','chkArticle2':'" + chkArticle2.Checked +
                           "','valuePP2':'" + valuePP2.Text +
                           "','ddlArticle3':'" + ddlArticle3.SelectedValue + "','chkArticle3':'" + chkArticle3.Checked +
                           "','valuePP3':'" + valuePP3.Text +
                           "','ddlArticle4':'" + ddlArticle4.SelectedValue + "','chkArticle4':'" + chkArticle4.Checked +
                           "','valuePP4':'" + valuePP4.Text +
                           "','ddlArticle5':'" + ddlArticle5.SelectedValue + "','chkArticle5':'" + chkArticle5.Checked +
                           "','valuePP5':'" + valuePP5.Text +
                           "','ddlArticle6':'" + ddlArticle6.SelectedValue + "','chkArticle6':'" + chkArticle6.Checked +
                           "','valuePP6':'" + valuePP6.Text +
                           "','ddlArticle7':'" + ddlArticle7.SelectedValue + "','chkArticle7':'" + chkArticle7.Checked +
                           "','valuePP7':'" + valuePP7.Text +
                           "','ddlArticle8':'" + ddlArticle8.SelectedValue + "','chkArticle8':'" + chkArticle8.Checked +
                           "','valuePP8':'" + valuePP8.Text +
                           "','ddlArticle9':'" + ddlArticle9.SelectedValue + "','chkArticle9':'" + chkArticle9.Checked +
                           "','valuePP9':'" + valuePP9.Text +
                           "','ddlArticle10':'" + ddlArticle10.SelectedValue + "','chkArticle10':'" +
                           chkArticle10.Checked + "','valuePP10':'" + valuePP10.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "DON01", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("DON01", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("DON01", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnDON01
    {
        Col1 = 3,
        Col2 = 4,
        Col3 = 5,
        Col4 = 6
    }
}