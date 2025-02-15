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
    public partial class BF12_12 : Page
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

                    txtZone832.Text = json["txtZone832"].ToString();
                    txtZone834.Text = json["txtZone834"].ToString();

                    chk904.Checked = json["chk904"].TransformToBoolean();
                    txtZone906.Text = json["txtZone906"].ToString();
                    txtZone916.Text = json["txtZone916"].ToString();
                    txtZone918.Text = json["txtZone918"].ToString();
                    txtZone920.Text = json["txtZone920"].ToString();

                txtEmolument_de_formalités_HT.Text = json["txtEmolument_de_formalités_HT"].ToString();
                txtDébours.Text = json["txtDébours"].ToString();
                hdUtilisation_du_futur_tarif.Value = json["chkUtilisation_du_futur_tarif"].ToString();
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
                new RangeCoordinates { Row = 5, Column = 4, Height = 1, Width = 1 },
                new object[]
                {
                        new object[] {"12"}
                });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates { Row = 54, Column = 5, Height = 2, Width = 1 },
                new object[]
                {
                        new object[]{txtZone832.Text},
                        new object[]{txtZone834.Text}
                });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates { Row = 66, Column = 5, Height = 5, Width = 1 },
                new object[]
                {
                        new object[]{chk904.Checked.TransformToBooleanFr()},
                        new object[]{float.Parse(txtZone906.Text.Replace(",","."))/100},
                        new object[]{txtZone916.Text},
                        new object[]{txtZone918.Text},
                        new object[]{float.Parse(txtZone920.Text.Replace(",","."))/100}
                });

            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates { Row = 73, Column = 5, Height = 3, Width = 1 },
                new object[]
                {
                        new object[]{txtEmolument_de_formalités_HT.Text},
                        new object[]{txtDébours.Text},
                        new object[]{hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            lblF102.InnerText = dt.Rows[0][(int)ColumnBF12_12.Col1].ToString();
            lblF103.InnerText = dt.Rows[1][(int)ColumnBF12_12.Col1].ToString();
            lblF104.InnerText = dt.Rows[2][(int)ColumnBF12_12.Col1].ToString();
            lblF106.InnerText = dt.Rows[4][(int)ColumnBF12_12.Col1].ToString();
            //TVA
            H171.InnerText = dt.Rows[69][(int)ColumnBF12_12.Col3].ToString();
            H173.InnerText = dt.Rows[71][(int)ColumnBF12_12.Col3].ToString();
            H175.InnerText = dt.Rows[73][(int)ColumnBF12_12.Col3].ToString();
            H176.InnerText = dt.Rows[74][(int)ColumnBF12_12.Col3].ToString();
            H177.InnerText = dt.Rows[75][(int)ColumnBF12_12.Col3].ToString();
            //Débours
            lblH182.InnerText = dt.Rows[80][(int)ColumnBF12_12.Col3].ToString();
            //Trésor public
            div187.Visible = dt.Rows[85][(int)ColumnBF12_12.Col4].ToString() == show;
            div188.Visible = dt.Rows[86][(int)ColumnBF12_12.Col4].ToString() == show;
            lblF188.InnerText = dt.Rows[86][(int)ColumnBF12_12.Col1].ToString();
            lblG188.InnerText = dt.Rows[86][(int)ColumnBF12_12.Col2].ToString();
            lblH188.InnerText = dt.Rows[86][(int)ColumnBF12_12.Col3].ToString();
            lblF189.Visible = dt.Rows[87][(int)ColumnBF12_12.Col4].ToString() == show;
            lblF189.InnerText = dt.Rows[87][(int)ColumnBF12_12.Col1].ToString();
            div190.Visible = dt.Rows[88][(int)ColumnBF12_12.Col4].ToString() == show;

            var html = "";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += dt.Rows[6][(int)ColumnBF12_12.Col4].ToString() == show
                ? "<tr><td colspan='5'><br/></td></tr>"
                : "<tr><td colspan='5'><br/><br/></td></tr>";
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
            if (dt.Rows[6][(int)ColumnBF12_12.Col4].ToString() == show)
            {
                html += string.Format(
                        "<div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                        Request.PhysicalApplicationPath + "tmp\\BF12-12\\chart.png");
            }
            else
            {
                html += string.Format(
                        "<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                        Request.PhysicalApplicationPath + "tmp\\BF12-12\\chart.png");
            }
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire - C.com. Art. A 444-87b
            row155.Visible = dt.Rows[53][(int)ColumnBF12_12.Col4].ToString() == show;
            if (dt.Rows[53][(int)ColumnBF12_12.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-87b</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row156.Visible = dt.Rows[54][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[54][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F156.InnerText = dt.Rows[54][(int)ColumnBF12_12.Col1].ToString();
                    G156.InnerText = dt.Rows[54][(int)ColumnBF12_12.Col2].ToString();
                    H156.InnerText = dt.Rows[54][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F156.InnerText, G156.InnerText, H156.InnerText);
                }
                row157.Visible = dt.Rows[55][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[55][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F157.InnerText = dt.Rows[55][(int)ColumnBF12_12.Col1].ToString();
                    G157.InnerText = dt.Rows[55][(int)ColumnBF12_12.Col2].ToString();
                    H157.InnerText = dt.Rows[55][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F157.InnerText, G157.InnerText, H157.InnerText);
                }
                row158.Visible = dt.Rows[56][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[56][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F158.InnerText = dt.Rows[56][(int)ColumnBF12_12.Col1].ToString();
                    G158.InnerText = dt.Rows[56][(int)ColumnBF12_12.Col2].ToString();
                    H158.InnerText = dt.Rows[56][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F158.InnerText, G158.InnerText, H158.InnerText);
                }
                row159.Visible = dt.Rows[57][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[57][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F159.InnerText = dt.Rows[57][(int)ColumnBF12_12.Col1].ToString();
                    G159.InnerText = dt.Rows[57][(int)ColumnBF12_12.Col2].ToString();
                    H159.InnerText = dt.Rows[57][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F159.InnerText, G159.InnerText, H159.InnerText);
                }
                row161.Visible = dt.Rows[59][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[59][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    G161.InnerText = dt.Rows[59][(int)ColumnBF12_12.Col2].ToString();
                    html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                        G161.InnerText);
                }
                row162.Visible = dt.Rows[60][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[60][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    H162.InnerText = dt.Rows[60][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                        H162.InnerText);
                }
                row164.Visible = dt.Rows[62][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[62][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    H164.InnerText = dt.Rows[62][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                        H164.InnerText);
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                H171.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                H173.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                H175.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                H176.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                H177.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                lblH182.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Tresor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            if (dt.Rows[85][(int)ColumnBF12_12.Col4].ToString() == show)
            {
                if (dt.Rows[86][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td>Taxe publicité foncière :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        lblF188.InnerText, lblG188.InnerText, lblH188.InnerText);
                }
                if (dt.Rows[87][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    html += string.Format("<tr><td colspan='4' align='center'>{0}</td></tr>", lblF189.InnerText);
                }
                if (dt.Rows[88][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='center'>Pour la TPF, il a été pris le minimum de perception soit 25 Euros.</td></tr>";
                }
            }
            row192.Visible = dt.Rows[90][(int)ColumnBF12_12.Col4].ToString() == show;
            if (dt.Rows[90][(int)ColumnBF12_12.Col4].ToString() == show)
            {
                H192.InnerText = dt.Rows[90][(int)ColumnBF12_12.Col3].ToString();
                H193.InnerText = dt.Rows[91][(int)ColumnBF12_12.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Imputation des droits payés antérieurement :</td><td align='right'>{0}</td><td></td></tr>",
                    H192.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Droits dûs après imputation :</td><td align='right'>{0}</td><td></td></tr>",
                    H193.InnerText);
            }
            row195.Visible = dt.Rows[93][(int)ColumnBF12_12.Col4].ToString() == show;
            if (dt.Rows[93][(int)ColumnBF12_12.Col4].ToString() == show)
            {
                row196.Visible = dt.Rows[94][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[94][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F196.InnerText = dt.Rows[94][(int)ColumnBF12_12.Col1].ToString();
                    G196.InnerText = dt.Rows[94][(int)ColumnBF12_12.Col2].ToString();
                    H196.InnerText = dt.Rows[94][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td>CSI (art. 879 du CGI) :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F196.InnerText, G196.InnerText, H196.InnerText);
                }
                row197.Visible = dt.Rows[95][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[95][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='2'>CSI (art. 879 du CGI) :</td><td colspan='2' align='center'>Pour la CSI, il a été pris le minimum de perception soit 15 Euros.</td></tr>";
                }
            }
            row199.Visible = dt.Rows[97][(int)ColumnBF12_12.Col4].ToString() == show;
            if (dt.Rows[97][(int)ColumnBF12_12.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4' align='left'>Droit de mutation sur le prix de cession :</td></tr>";
                row200.Visible = dt.Rows[98][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[98][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F200.InnerText = dt.Rows[98][(int)ColumnBF12_12.Col1].ToString();
                    G200.InnerText = dt.Rows[98][(int)ColumnBF12_12.Col2].ToString();
                    H200.InnerText = dt.Rows[98][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F200.InnerText, G200.InnerText, H200.InnerText);
                }
                row201.Visible = dt.Rows[99][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[99][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F201.InnerText = dt.Rows[99][(int)ColumnBF12_12.Col1].ToString();
                    G201.InnerText = dt.Rows[99][(int)ColumnBF12_12.Col2].ToString();
                    H201.InnerText = dt.Rows[99][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F201.InnerText, G201.InnerText, H201.InnerText);
                }
                row202.Visible = dt.Rows[100][(int)ColumnBF12_12.Col4].ToString() == show;
                if (dt.Rows[100][(int)ColumnBF12_12.Col4].ToString() == show)
                {
                    F202.InnerText = dt.Rows[100][(int)ColumnBF12_12.Col1].ToString();
                    G202.InnerText = dt.Rows[100][(int)ColumnBF12_12.Col2].ToString();
                    H202.InnerText = dt.Rows[100][(int)ColumnBF12_12.Col3].ToString();
                    html += string.Format(
                        "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F202.InnerText, G202.InnerText, H202.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF12-ONLINE-FUTURE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 5, 101, 4, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF12-12");
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
                           "','txtZone832':'" + txtZone832.Text +
                           "','txtZone834':'" + txtZone834.Text +
                           "','chk904':'" + chk904.Checked +
                           "','txtZone906':'" + txtZone906.Text +
                           "','txtZone916':'" + txtZone916.Text +
                           "','txtZone918':'" + txtZone918.Text +
                           "','txtZone920':'" + txtZone920.Text +
                    "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                    "','txtDébours':'" + txtDébours.Text +
                    "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value +
                    "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF12-12", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','txtZone832':'" + txtZone832.Text +
                           "','txtZone834':'" + txtZone834.Text +
                           "','chk904':'" + chk904.Checked +
                           "','txtZone906':'" + txtZone906.Text +
                           "','txtZone916':'" + txtZone916.Text +
                           "','txtZone918':'" + txtZone918.Text +
                           "','txtZone920':'" + txtZone920.Text +
                    "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                    "','txtDébours':'" + txtDébours.Text +
                    "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value +
                    "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF12-12", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF12-12", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF12-12", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
    enum ColumnBF12_12
    {
        Col1 = 0,
        Col2 = 1,
        Col3 = 2,
        Col4 = 3
    }
}