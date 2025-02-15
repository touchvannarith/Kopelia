using Newtonsoft.Json.Linq;
using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using NotaliaOnline.Properties;
using NotaliaOnline.WebReference;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class BF13 : Page
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
                var json = JObject.Parse(obj.Value);
                txtLibelle.Text = obj.Libelle;
                txtDossier.Text = json["txtDossier"].ToString();
                txtDateSignature.Text = json["txtDateDeSignature"].ToString();
                txtRedacteur.Text = json["txtRedacteur"].ToString();
                ddl1.SelectedValue = json["ddl1"].ToString();
                txtZone01.Text = json["txtZone01"].ToString();
                txtZone02.Text = json["txtZone02"].ToString();
                ddl2.SelectedValue = json["ddl2"].ToString();
                chk01.Checked = json["chk01"].TransformToBoolean();
                txtZone03.Text = json["txtZone03"].ToString();
                chk02.Checked = json["chk02"].TransformToBoolean();
                ddl3.SelectedValue = json["ddl3"].ToString();
                txtZone04.Text = json["txtZone04"].ToString();
                txtZone05.Text = json["txtZone05"].ToString();
                txtZone06.Text = json["txtZone06"].ToString();
                txtZone07.Text = json["txtZone07"].ToString();
                txtZone08.Text = json["txtZone08"].ToString();
                chk03.Checked = json["chk03"].TransformToBoolean();
                chk04.Checked = json["chk04"].TransformToBoolean();
                txtZone09.Text = json["txtZone09"].ToString();
                ddl4.SelectedValue = json["ddl4"].ToString();
                chk05.Checked = json["chk05"].TransformToBoolean();
                txtZone10.Text = json["txtZone10"].ToString();
                txtZone11.Text = json["txtZone11"].ToString();
                txtZone12.Text = json["txtZone12"].ToString();
                chk06.Checked = json["chk06"].TransformToBoolean();
                chk07.Checked = json["chk07"].TransformToBoolean();
                txtZone13.Text = json["txtZone13"].ToString();
                chk08.Checked = json["chk08"].TransformToBoolean();
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
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates() { Row = 9, Column = 3, Height = 28, Width = 1 }, new object[]
                {
                        new object[] {ddl1.SelectedValue},
                        new object[] {txtZone01.Text},
                        new object[] {txtZone02.Text},
                        new object[] {ddl2.SelectedValue},
                        new object[] {chk01.Checked.TransformToBooleanFr()},
                        new object[] {txtZone03.Text},
                        new object[] {chk02.Checked.TransformToBooleanFr()},
                        new object[] {ddl3.SelectedValue},
                        new object[] {txtZone04.Text},
                        new object[] {txtZone05.Text},
                        new object[] {txtZone06.Text},
                        new object[] {txtZone07.Text},
                        new object[] {txtZone08.Text},
                        new object[] {chk03.Checked.TransformToBooleanFr()},
                        new object[] {chk04.Checked.TransformToBooleanFr()},
                        new object[] {txtZone09.Text},
                        new object[] {ddl4.SelectedValue},
                        new object[] {chk05.Checked.TransformToBooleanFr()},
                        new object[] {txtZone10.Text},
                        new object[] {txtZone11.Text},
                        new object[] {txtZone12.Text},
                        new object[] {chk06.Checked.TransformToBooleanFr()},
                        new object[] {chk07.Checked.TransformToBooleanFr()},
                        new object[] {txtZone13.Text},
                        new object[] {chk08.Checked.TransformToBooleanFr()},
                        new object[]{txtEmolument_de_formalités_HT.Text},
                        new object[]{txtDébours.Text},
                        new object[]{hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            txtZone02_1.Text = (txtZone01.Text.TransformToDouble() - txtZone02.Text.TransformToDouble()).ToString(CultureInfo.InvariantCulture);
            var html = "";
            const string show = "1";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            H44.InnerText = dt.Rows[41][(int)ColumnBF13.Col2].ToString();
            H45.InnerText = dt.Rows[42][(int)ColumnBF13.Col2].ToString();
            H46.InnerText = dt.Rows[43][(int)ColumnBF13.Col2].ToString();
            H48.InnerText = dt.Rows[45][(int)ColumnBF13.Col2].ToString();
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    H45.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    H46.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    H44.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    H48.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF13\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-143</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row7.Visible = false;
            if (dt.Rows[4][(int)ColumnBF13.Col4].ToString() == show)
            {
                row7.Visible = true;
                G7.InnerText = dt.Rows[4][(int)ColumnBF13.Col1].ToString();
                H7.InnerText = dt.Rows[4][(int)ColumnBF13.Col2].ToString();
                I7.InnerText = dt.Rows[4][(int)ColumnBF13.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    G7.InnerText, H7.InnerText, I7.InnerText);
            }
            row8.Visible = false;
            if (dt.Rows[5][(int)ColumnBF13.Col4].ToString() == show)
            {
                row8.Visible = true;
                G8.InnerText = dt.Rows[5][(int)ColumnBF13.Col1].ToString();
                H8.InnerText = dt.Rows[5][(int)ColumnBF13.Col2].ToString();
                I8.InnerText = dt.Rows[5][(int)ColumnBF13.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    G8.InnerText, H8.InnerText, I8.InnerText);
            }
            row9.Visible = false;
            if (dt.Rows[6][(int)ColumnBF13.Col4].ToString() == show)
            {
                row9.Visible = true;
                G9.InnerText = dt.Rows[6][(int)ColumnBF13.Col1].ToString();
                H9.InnerText = dt.Rows[6][(int)ColumnBF13.Col2].ToString();
                I9.InnerText = dt.Rows[6][(int)ColumnBF13.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    G9.InnerText, H9.InnerText, I9.InnerText);
            }
            row10.Visible = false;
            if (dt.Rows[7][(int)ColumnBF13.Col4].ToString() == show)
            {
                row10.Visible = true;
                G10.InnerText = dt.Rows[7][(int)ColumnBF13.Col1].ToString();
                H10.InnerText = dt.Rows[7][(int)ColumnBF13.Col2].ToString();
                I10.InnerText = dt.Rows[7][(int)ColumnBF13.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    G10.InnerText, H10.InnerText, I10.InnerText);
            }
            row11.Visible = false;
            if (dt.Rows[8][(int)ColumnBF13.Col4].ToString() == show)
            {
                row11.Visible = true;
                H11.InnerText = dt.Rows[8][(int)ColumnBF13.Col2].ToString();
                I11.InnerText = dt.Rows[8][(int)ColumnBF13.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>Total HT :        {1}</td><td></td></tr>",
                    H11.InnerText, I11.InnerText);
            }
            row12.Visible = false;
            if (dt.Rows[9][(int)ColumnBF13.Col4].ToString() == show)
            {
                row12.Visible = true;
                G12.InnerText = dt.Rows[9][(int)ColumnBF13.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de caution (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    G12.InnerText);
            }
            row13.Visible = false;
            if (dt.Rows[10][(int)ColumnBF13.Col4].ToString() == show)
            {
                row13.Visible = true;
                G13.InnerText = dt.Rows[10][(int)ColumnBF13.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments d'antériorité (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    G13.InnerText);
            }
            row14.Visible = false;
            if (dt.Rows[11][(int)ColumnBF13.Col4].ToString() == show)
            {
                row14.Visible = true;
                G14.InnerText = dt.Rows[11][(int)ColumnBF13.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de nantissement (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    G14.InnerText);
            }
            G15.InnerText = dt.Rows[12][(int)ColumnBF13.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                G15.InnerText);
            G16.InnerText = dt.Rows[13][(int)ColumnBF13.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total :</td><td align='right'>{0}</td><td></td></tr>",
                G16.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G18.InnerText = dt.Rows[15][(int)ColumnBF13.Col1].ToString();
            G19.InnerText = dt.Rows[16][(int)ColumnBF13.Col1].ToString();
            G20.InnerText = dt.Rows[17][(int)ColumnBF13.Col1].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                G18.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                G19.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                G20.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G22.InnerText = dt.Rows[19][(int)ColumnBF13.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    G22.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td colspan='4' align='left'>Enregistrement</td></tr>";
            G26.InnerText = dt.Rows[23][(int)ColumnBF13.Col1].ToString();
            html += "<tr><td colspan='4' align='center'>" + G26.InnerText + "</td></tr>";
            row27.Visible = false;
            if (dt.Rows[24][(int)ColumnBF13.Col4].ToString() == show)
            {
                row27.Visible = true;
                G27.InnerText = dt.Rows[24][(int)ColumnBF13.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Droit sur état :</td><td align='right'>{0}</td><td></td></tr>",
                    G27.InnerText);
            }
            row28.Visible = false;
            if (dt.Rows[25][(int)ColumnBF13.Col4].ToString() == show)
            {
                row28.Visible = true;
                html += "<tr><td colspan='4' align='left'>Hypothèques</td></tr>";
                html += "<tr><td colspan='4' align='center'>CSI (art. 879 du CGI)</td></tr>";
                row29.Visible = false;
                if (dt.Rows[26][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row29.Visible = true;
                    G29.InnerText = dt.Rows[26][(int)ColumnBF13.Col1].ToString();
                    H29.InnerText = dt.Rows[26][(int)ColumnBF13.Col2].ToString();
                    I29.InnerText = dt.Rows[26][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td>Privilége de prêteur :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        G29.InnerText, H29.InnerText, I29.InnerText);
                }
                row30.Visible = false;
                if (dt.Rows[27][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row30.Visible = true;
                    G30.InnerText = dt.Rows[27][(int)ColumnBF13.Col1].ToString();
                    H30.InnerText = dt.Rows[27][(int)ColumnBF13.Col2].ToString();
                    I30.InnerText = dt.Rows[27][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td>Hypothèque :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        G30.InnerText, H30.InnerText, I30.InnerText);
                }
                row31.Visible = false;
                if (dt.Rows[28][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row31.Visible = true;
                    G31.InnerText = dt.Rows[28][(int)ColumnBF13.Col1].ToString();
                    H31.InnerText = dt.Rows[28][(int)ColumnBF13.Col2].ToString();
                    I31.InnerText = dt.Rows[28][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td>Privilége de vendeur :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        G31.InnerText, H31.InnerText, I31.InnerText);
                }
                row32.Visible = false;
                if (dt.Rows[29][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row32.Visible = true;
                    G32.InnerText = dt.Rows[29][(int)ColumnBF13.Col1].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>CSI complémentaire pour pluralité de biens ou de bureaux (partie afférente au(x) privilège(s)) :</td><td align='right'>{0}</td><td></td></tr>",
                        G32.InnerText);
                }
                row33.Visible = false;
                if (dt.Rows[30][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row33.Visible = true;
                    G33.InnerText = dt.Rows[30][(int)ColumnBF13.Col1].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>CSI complémentaire pour pluralité de biens ou de bureaux (partie afférente au cautionnement hypothécaire) :</td><td align='right'>{0}</td><td></td></tr>",
                        G33.InnerText);
                }
                row34.Visible = false;
                if (dt.Rows[31][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row34.Visible = true;
                    G34.InnerText = dt.Rows[31][(int)ColumnBF13.Col1].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Cession d'antériorité :</td><td align='right'>{0}</td><td></td></tr>",
                        G34.InnerText);
                }
                G35.InnerText = dt.Rows[32][(int)ColumnBF13.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Sous-total :</td><td align='right'>{0}</td><td></td></tr>",
                    G35.InnerText);
            }
            row36.Visible = false;
            if (dt.Rows[33][(int)ColumnBF13.Col4].ToString() == show)
            {
                row36.Visible = true;
                html += "<tr><td colspan='4' align='center'>Publicité foncière</td></tr>";
                row37.Visible = false;
                if (dt.Rows[34][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row37.Visible = true;
                    G37.InnerText = dt.Rows[34][(int)ColumnBF13.Col1].ToString();
                    H37.InnerText = dt.Rows[34][(int)ColumnBF13.Col2].ToString();
                    I37.InnerText = dt.Rows[34][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td>Taxe de publicité :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        G37.InnerText, H37.InnerText, I37.InnerText);
                }
                row38.Visible = false;
                if (dt.Rows[35][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row38.Visible = true;
                    G38.InnerText = dt.Rows[35][(int)ColumnBF13.Col1].ToString();
                    H38.InnerText = dt.Rows[35][(int)ColumnBF13.Col2].ToString();
                    I38.InnerText = dt.Rows[35][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td>Taxe de publicité :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        G38.InnerText, H38.InnerText, I38.InnerText);
                }
                row39.Visible = false;
                if (dt.Rows[36][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row39.Visible = true;
                    G39.InnerText = dt.Rows[36][(int)ColumnBF13.Col1].ToString();
                    H39.InnerText = dt.Rows[36][(int)ColumnBF13.Col2].ToString();
                    I39.InnerText = dt.Rows[36][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td>Taxe de publicité :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        G39.InnerText, H39.InnerText, I39.InnerText);
                }
                row40.Visible = false;
                if (dt.Rows[37][(int)ColumnBF13.Col4].ToString() == show)
                {
                    row40.Visible = true;
                    I40.InnerText = dt.Rows[37][(int)ColumnBF13.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Taxe complémentaire  sur les bordereaux d'inscriptions :</td><td align='right'>{0}</td><td></td></tr>",
                        I40.InnerText);
                }
                I41.InnerText = dt.Rows[38][(int)ColumnBF13.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Sous-total :</td><td align='right'>{0}</td><td></td></tr>",
                    I41.InnerText);
                G42.InnerText = dt.Rows[39][(int)ColumnBF13.Col1].ToString();
                html +=
                    string.Format("<tr><td colspan='4' align='center'><font color='#FF0000'>{0}</font></td></tr>",
                        G42.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF13-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 2, 1, 47, 9, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF01");
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
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','chk01':'" + chk01.Checked +
                           "','txtZone03':'" + txtZone03.Text +
                           "','chk02':'" + chk02.Checked +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone04':'" + txtZone04.Text +
                           "','txtZone05':'" + txtZone05.Text +
                           "','txtZone06':'" + txtZone06.Text +
                           "','txtZone07':'" + txtZone07.Text +
                           "','txtZone08':'" + txtZone08.Text +
                           "','chk03':'" + chk03.Checked +
                           "','chk04':'" + chk04.Checked +
                           "','txtZone09':'" + txtZone09.Text +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','chk05':'" + chk05.Checked +
                           "','txtZone10':'" + txtZone10.Text +
                           "','txtZone11':'" + txtZone11.Text +
                           "','txtZone12':'" + txtZone12.Text +
                           "','chk06':'" + chk06.Checked +
                           "','chk07':'" + chk07.Checked +
                           "','txtZone13':'" + txtZone13.Text +
                           "','chk08':'" + chk08.Checked +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF13", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','chk01':'" + chk01.Checked +
                           "','txtZone03':'" + txtZone03.Text +
                           "','chk02':'" + chk02.Checked +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone04':'" + txtZone04.Text +
                           "','txtZone05':'" + txtZone05.Text +
                           "','txtZone06':'" + txtZone06.Text +
                           "','txtZone07':'" + txtZone07.Text +
                           "','txtZone08':'" + txtZone08.Text +
                           "','chk03':'" + chk03.Checked +
                           "','chk04':'" + chk04.Checked +
                           "','txtZone09':'" + txtZone09.Text +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','chk05':'" + chk05.Checked +
                           "','txtZone10':'" + txtZone10.Text +
                           "','txtZone11':'" + txtZone11.Text +
                           "','txtZone12':'" + txtZone12.Text +
                           "','chk06':'" + chk06.Checked +
                           "','chk07':'" + chk07.Checked +
                           "','txtZone13':'" + txtZone13.Text +
                           "','chk08':'" + chk08.Checked +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF13", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF13", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF13", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
    enum ColumnBF13
    {
        Col1 = 5,
        Col2 = 6,
        Col3 = 7,
        Col4 = 8,
    }
}