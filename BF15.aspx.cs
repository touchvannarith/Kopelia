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
    public partial class BF15 : Page
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
                ddl1.SelectedValue = json["ddl1"].ToString();
                if (json["radio1"].ToString().ToLower() == "true")
                {
                    rad1Oui.Checked = true;
                    rad1Non.Checked = false;
                }
                else
                {
                    rad1Non.Checked = true;
                    rad1Oui.Checked = false;
                }
                ddl2.SelectedValue = json["ddl2"].ToString();
                txtZone01.Text = json["txtZone01"].ToString();
                ddl3.SelectedValue = json["ddl3"].ToString();
                txtZone02.Text = json["txtZone02"].ToString();
                txtZone03.Text = json["txtZone03"].ToString();
                if (json["radio2"].ToString().ToLower() == "true")
                {
                    rad2Oui.Checked = true;
                    rad2Non.Checked = false;
                }
                else
                {
                    rad2Non.Checked = true;
                    rad2Oui.Checked = false;
                }
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
                Height = 12,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {9},
                        new object[] {ddl1.SelectedValue},
                        new object[] {rad1Oui.Checked ? "VRAI" : "FAUX"},
                        new object[] {ddl2.SelectedValue},
                        new object[] {txtZone01.Text + "%"},
                        new object[] {ddl3.SelectedValue},
                        new object[] {txtZone02.Text},
                        new object[] {txtZone03.Text},
                        new object[] {rad2Oui.Checked ? "VRAI" : "FAUX"},
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
            F102.InnerText = dt.Rows[0][(int)ColumnBF15.Col1].ToString();
            F103.InnerText = dt.Rows[1][(int)ColumnBF15.Col1].ToString();
            F104.InnerText = dt.Rows[2][(int)ColumnBF15.Col1].ToString();
            F106.InnerText = dt.Rows[4][(int)ColumnBF15.Col1].ToString();
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
                Request.PhysicalApplicationPath + "tmp\\BF15\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row114.Visible = false;
            if (dt.Rows[12][(int)ColumnBF15.Col4].Equals(show))
            {
                row114.Visible = true;
                F114.InnerText = dt.Rows[12][(int)ColumnBF15.Col1].ToString();
                G114.InnerText = dt.Rows[12][(int)ColumnBF15.Col2].ToString();
                H114.InnerText = dt.Rows[12][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F114.InnerText, G114.InnerText, H114.InnerText);
            }
            row115.Visible = false;
            if (dt.Rows[13][(int)ColumnBF15.Col4].Equals(show))
            {
                row115.Visible = true;
                F115.InnerText = dt.Rows[13][(int)ColumnBF15.Col1].ToString();
                G115.InnerText = dt.Rows[13][(int)ColumnBF15.Col2].ToString();
                H115.InnerText = dt.Rows[13][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F115.InnerText, G115.InnerText, H115.InnerText);
            }
            row116.Visible = false;
            if (dt.Rows[14][(int)ColumnBF15.Col4].Equals(show))
            {
                row116.Visible = true;
                F116.InnerText = dt.Rows[14][(int)ColumnBF15.Col1].ToString();
                G116.InnerText = dt.Rows[14][(int)ColumnBF15.Col2].ToString();
                H116.InnerText = dt.Rows[14][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F116.InnerText, G116.InnerText, H116.InnerText);
            }
            row117.Visible = false;
            if (dt.Rows[15][(int)ColumnBF15.Col4].Equals(show))
            {
                row117.Visible = true;
                F117.InnerText = dt.Rows[15][(int)ColumnBF15.Col1].ToString();
                G117.InnerText = dt.Rows[15][(int)ColumnBF15.Col2].ToString();
                H117.InnerText = dt.Rows[15][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F117.InnerText, G117.InnerText, H117.InnerText);
            }
            row119.Visible = false;
            if (dt.Rows[17][(int)ColumnBF15.Col4].Equals(show))
            {
                row119.Visible = true;
                G119.InnerText = dt.Rows[17][(int)ColumnBF15.Col2].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                    G119.InnerText);
            }
            row122.Visible = false;
            if (dt.Rows[20][(int)ColumnBF15.Col4].Equals(show))
            {
                row122.Visible = true;
                H122.InnerText = dt.Rows[20][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                    H122.InnerText);
            }
            H124.InnerText = dt.Rows[22][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total des émoluments réglementés :</td><td align='right'>{0}</td><td></td></tr>",
                H124.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H126.InnerText = dt.Rows[24][(int)ColumnBF15.Col3].ToString();
            H127.InnerText = dt.Rows[25][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                H126.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                H127.InnerText);
            H128.InnerText = dt.Rows[26][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                H128.InnerText);
            H129.InnerText = dt.Rows[27][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                H129.InnerText);
            H130.InnerText = dt.Rows[28][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                H130.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H132.InnerText = dt.Rows[30][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                H132.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Tresor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row137.Visible = false;
            if (dt.Rows[35][(int)ColumnBF15.Col4].Equals(show))
            {
                row137.Visible = true;
                html += "<tr><td colspan='4'>CSI (art. 879 du CGI) :</td></tr>";
                row138.Visible = false;
                if (dt.Rows[36][(int)ColumnBF15.Col4].Equals(show))
                {
                    row138.Visible = true;
                    F138.InnerText = dt.Rows[36][(int)ColumnBF15.Col1].ToString();
                    G138.InnerText = dt.Rows[36][(int)ColumnBF15.Col2].ToString();
                    H138.InnerText = dt.Rows[36][(int)ColumnBF15.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F138.InnerText, G138.InnerText, H138.InnerText);
                }
                row139.Visible = false;
                if (dt.Rows[37][(int)ColumnBF15.Col4].Equals(show))
                {
                    row139.Visible = true;
                    H139.InnerText = dt.Rows[37][(int)ColumnBF15.Col1].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                    H139.InnerText);
                }
            }
            row141.Visible = false;
            if (dt.Rows[39][(int)ColumnBF15.Col4].Equals(show))
            {
                row141.Visible = true;
                html += "<tr><td colspan='4'>Contrat en mains - Frais déductibles :</td></tr>";
                H142.InnerText = dt.Rows[40][(int)ColumnBF15.Col3].ToString();
                H143.InnerText = dt.Rows[41][(int)ColumnBF15.Col3].ToString();
                H144.InnerText = dt.Rows[42][(int)ColumnBF15.Col3].ToString();
                H145.InnerText = dt.Rows[43][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Frais fixes :</td><td align='right'>{0}</td><td></td></tr>",
                    H142.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    H143.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>CSI sur l'immeuble (art. 879 du CGI) :</td><td align='right'>{0}</td><td></td></tr>",
                    H144.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total :</td><td align='right'>{0}</td><td></td></tr>",
                    H145.InnerText);
            }
            html += "<tr><td colspan='4'>Fiscalité immobilière :</td></tr>";
            html += "<tr><td colspan='2' align='right'>TVA immobilière :</td><td colspan='2'></td></tr>";
            H148.InnerText = dt.Rows[46][(int)ColumnBF15.Col3].ToString();
            H149.InnerText = dt.Rows[47][(int)ColumnBF15.Col3].ToString();
            H150.InnerText = dt.Rows[48][(int)ColumnBF15.Col3].ToString();
            H151.InnerText = dt.Rows[49][(int)ColumnBF15.Col3].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Prix TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    H148.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Taux de la TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    H149.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Montant de la TVA sur le prix total :</td><td align='right'>{0}</td><td></td></tr>",
                    H150.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Base HT :</td><td align='right'>{0}</td><td></td></tr>",
                    H151.InnerText);
            F152.InnerText = dt.Rows[50][(int)ColumnBF15.Col1].ToString();
            html += "<tr><td colspan='2' align='right'>" + F152.InnerText + "</td><td colspan='2'></td></tr>";
            row153.Visible = false;
            if (dt.Rows[51][(int)ColumnBF15.Col4].Equals(show))
            {
                row153.Visible = true;
                H153.InnerText = dt.Rows[51][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Base HT du prix exprimé :</td><td align='right'>{0}</td><td></td></tr>",
                    H153.InnerText);
            }
            row154.Visible = false;
            if (dt.Rows[52][(int)ColumnBF15.Col4].Equals(show))
            {
                row154.Visible = true;
                H154.InnerText = dt.Rows[52][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Déduction des frais :</td><td align='right'>{0}</td><td></td></tr>",
                    H154.InnerText);
            }
            row155.Visible = false;
            if (dt.Rows[53][(int)ColumnBF15.Col4].Equals(show))
            {
                row155.Visible = true;
                H155.InnerText = dt.Rows[53][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Montant des droits venant en déduction :</td><td align='right'>{0}</td><td></td></tr>",
                    H155.InnerText);
            }
            row156.Visible = false;
            if (dt.Rows[54][(int)ColumnBF15.Col4].Equals(show))
            {
                row156.Visible = true;
                H156.InnerText = dt.Rows[54][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Base taxable :</td><td align='right'>{0}</td><td></td></tr>",
                    H156.InnerText);
            }
            html += "<tr><td colspan='2' align='right'>Montant des droits :</td><td colspan='2'></td></tr>";
            row158.Visible = false;
            if (dt.Rows[56][(int)ColumnBF15.Col4].Equals(show))
            {
                row158.Visible = true;
                html += "<tr><td colspan='4'>Taxe départementale :</td></tr>";
                F159.InnerText = dt.Rows[57][(int)ColumnBF15.Col1].ToString();
                G159.InnerText = dt.Rows[57][(int)ColumnBF15.Col2].ToString();
                H159.InnerText = dt.Rows[57][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F159.InnerText, G159.InnerText, H159.InnerText);
            }
            row161.Visible = false;
            if (dt.Rows[59][(int)ColumnBF15.Col4].Equals(show))
            {
                row161.Visible = true;
                html += "<tr><td colspan='4'>Prélèvement de l'Etat sur taxe départementale :</td></tr>";
                F162.InnerText = dt.Rows[60][(int)ColumnBF15.Col1].ToString();
                G162.InnerText = dt.Rows[60][(int)ColumnBF15.Col2].ToString();
                H162.InnerText = dt.Rows[60][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    F162.InnerText, G162.InnerText, H162.InnerText);
            }
            row164.Visible = false;
            if (dt.Rows[62][(int)ColumnBF15.Col4].Equals(show))
            {
                row164.Visible = true;
                H164.InnerText = dt.Rows[62][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                    H164.InnerText);
            }
            row166.Visible = false;
            if (dt.Rows[64][(int)ColumnBF15.Col4].Equals(show))
            {
                row166.Visible = true;
                H166.InnerText = dt.Rows[64][(int)ColumnBF15.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total :</td><td align='right'>{0}</td><td></td></tr>",
                    H166.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF15-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 2, 65, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF15");
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
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','radio1':'" + rad1Oui.Checked +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text +
                           "','radio2':'" + rad2Oui.Checked +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF15", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','radio1':'" + rad1Oui.Checked +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text +
                           "','radio2':'" + rad2Oui.Checked +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF15", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF15", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF15", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
    enum ColumnBF15
    {
        Col1 = 3,
        Col2 = 4,
        Col3 = 5,
        Col4 = 6
    }
}