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
    public partial class SUCC_DROITS : Page
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
                txtZone01.Text = json["txtZone01"].ToString();
                txtZone02.Text = json["txtZone02"].ToString();
                chk1.Checked = json["chk1"].TransformToBoolean();
                txtZone03.Text = json["txtZone03"].ToString();
                txtZone04.Text = json["txtZone04"].ToString();
                chk2.Checked = json["chk2"].TransformToBoolean();
                txtZone05.Text = json["txtZone05"].ToString();
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
                Height = 8,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {ddl1.SelectedValue},
                        new object[] {txtZone01.Text},
                        new object[] {txtZone02.Text},
                        new object[] {chk1.Checked ? "VRAI" : "FAUX"},
                        new object[] {txtZone03.Text},
                        new object[] {txtZone04.Text},
                        new object[] {chk2.Checked ? "VRAI" : "FAUX"},
                        new object[] {txtZone05.Text}
                });
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            var html = "";
            var ddl1Value = int.Parse(ddl1.SelectedValue);
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Calcul des droits</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Détermination de l'assiette taxable
            html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination de la base taxable</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G10.InnerText = dt.Rows[0][(int)ColumnSUCC_DROITS.Col2].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Montant hérité :</td><td align='right'>{0}</td><td></td></tr>",
                G10.InnerText);
            row12.Visible = false;
            if (dt.Rows[2][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
            {
                row12.Visible = true;
                G12.InnerText = dt.Rows[2][(int)ColumnSUCC_DROITS.Col2].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Abattement général : </td><td align='right'>{0}</td><td></td></tr>",
                    G12.InnerText);
            }
            row14.Visible = false;
            if (dt.Rows[4][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
            {
                row14.Visible = true;
                H14.InnerText = dt.Rows[4][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Abattement général déjà utilisé :</td><td align='right'>{0}</td><td></td></tr>",
                    H14.InnerText);
                H15.InnerText = dt.Rows[5][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Abattement résiduel : </td><td align='right'>{0}</td><td></td></tr>",
                    H15.InnerText);
            }
            row17.Visible = false;
            if (dt.Rows[7][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
            {
                row17.Visible = true;
                G17.InnerText = dt.Rows[7][(int)ColumnSUCC_DROITS.Col2].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Abattement pour infirmité :</td><td align='right'>{0}</td><td></td></tr>",
                    G17.InnerText);
                row19.Visible = false;
                if (dt.Rows[9][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row19.Visible = true;
                    H19.InnerText = dt.Rows[9][(int)ColumnSUCC_DROITS.Col3].ToString();
                    H20.InnerText = dt.Rows[10][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Abattemet pour infirmité déjà utilisé :</td><td align='right'>{0}</td><td align='right'></td></tr>",
                        H19.InnerText);
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Abattement résiduel :</td><td align='right'>{0}</td><td align='right'></td></tr>",
                        H20.InnerText);
                }
            }
            G22.InnerText = dt.Rows[12][(int)ColumnSUCC_DROITS.Col2].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Base taxable :</td><td align='right'>{0}</td><td></td></tr>",
                G22.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Détermination des droits à payer - case 1
            case1.Visible = false;
            if (ddl1Value == 1)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case1.Visible = true;
                row100.Visible = false;
                if (dt.Rows[90][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row100.Visible = true;
                    F100.InnerText = dt.Rows[90][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G100.InnerText = dt.Rows[90][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H100.InnerText = dt.Rows[90][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F100.InnerText, G100.InnerText, H100.InnerText);
                }
                row101.Visible = false;
                if (dt.Rows[91][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row101.Visible = true;
                    F101.InnerText = dt.Rows[91][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G101.InnerText = dt.Rows[91][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F101.InnerText, G101.InnerText);
                }
                row102.Visible = false;
                if (dt.Rows[92][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row102.Visible = true;
                    F102.InnerText = dt.Rows[92][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G102.InnerText = dt.Rows[92][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H102.InnerText = dt.Rows[92][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F102.InnerText, G102.InnerText, H102.InnerText);
                }
                row103.Visible = false;
                if (dt.Rows[93][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row103.Visible = true;
                    F103.InnerText = dt.Rows[93][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G103.InnerText = dt.Rows[93][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F103.InnerText, G103.InnerText);
                }
                row104.Visible = false;
                if (dt.Rows[94][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row104.Visible = true;
                    F104.InnerText = dt.Rows[94][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G104.InnerText = dt.Rows[94][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H104.InnerText = dt.Rows[94][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F104.InnerText, G104.InnerText, H104.InnerText);
                }
                row105.Visible = false;
                if (dt.Rows[95][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row105.Visible = true;
                    F105.InnerText = dt.Rows[95][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G105.InnerText = dt.Rows[95][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F105.InnerText, G105.InnerText);
                }
                row106.Visible = false;
                if (dt.Rows[96][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row106.Visible = true;
                    F106.InnerText = dt.Rows[96][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G106.InnerText = dt.Rows[96][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H106.InnerText = dt.Rows[96][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F106.InnerText, G106.InnerText, H106.InnerText);
                }
                row107.Visible = false;
                if (dt.Rows[97][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row107.Visible = true;
                    F107.InnerText = dt.Rows[97][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G107.InnerText = dt.Rows[97][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F107.InnerText, G107.InnerText);
                }
                row108.Visible = false;
                if (dt.Rows[98][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row108.Visible = true;
                    F108.InnerText = dt.Rows[98][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G108.InnerText = dt.Rows[98][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H108.InnerText = dt.Rows[98][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F108.InnerText, G108.InnerText, H108.InnerText);
                }
                row109.Visible = false;
                if (dt.Rows[99][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row109.Visible = true;
                    F109.InnerText = dt.Rows[99][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G109.InnerText = dt.Rows[99][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F109.InnerText, G109.InnerText);
                }
                row110.Visible = false;
                if (dt.Rows[100][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row110.Visible = true;
                    F110.InnerText = dt.Rows[100][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G110.InnerText = dt.Rows[100][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H110.InnerText = dt.Rows[100][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F110.InnerText, G110.InnerText, H110.InnerText);
                }
                row111.Visible = false;
                if (dt.Rows[101][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row111.Visible = true;
                    F111.InnerText = dt.Rows[101][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G111.InnerText = dt.Rows[101][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F111.InnerText, G111.InnerText);
                }
                row112.Visible = false;
                if (dt.Rows[102][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row112.Visible = true;
                    F112.InnerText = dt.Rows[102][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G112.InnerText = dt.Rows[102][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H112.InnerText = dt.Rows[102][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F112.InnerText, G112.InnerText, H112.InnerText);
                }
                row113.Visible = false;
                if (dt.Rows[103][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row113.Visible = true;
                    F113.InnerText = dt.Rows[103][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G113.InnerText = dt.Rows[103][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F113.InnerText, G113.InnerText);
                }
                G114.InnerText = dt.Rows[104][(int)ColumnSUCC_DROITS.Col2].ToString();
                H114.InnerText = dt.Rows[104][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G114.InnerText, H114.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 2
            case2.Visible = false;
            if (ddl1Value == 2)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case2.Visible = true;
                row200.Visible = false;
                if (dt.Rows[190][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row200.Visible = true;
                    F200.InnerText = dt.Rows[190][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G200.InnerText = dt.Rows[190][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H200.InnerText = dt.Rows[190][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F200.InnerText, G200.InnerText, H200.InnerText);
                }
                row201.Visible = false;
                if (dt.Rows[191][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row201.Visible = true;
                    F201.InnerText = dt.Rows[191][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G201.InnerText = dt.Rows[191][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F201.InnerText, G201.InnerText);
                }
                row202.Visible = false;
                if (dt.Rows[192][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row202.Visible = true;
                    F202.InnerText = dt.Rows[192][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G202.InnerText = dt.Rows[192][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H202.InnerText = dt.Rows[192][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F202.InnerText, G202.InnerText, H202.InnerText);
                }
                row203.Visible = false;
                if (dt.Rows[193][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row203.Visible = true;
                    F203.InnerText = dt.Rows[193][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G203.InnerText = dt.Rows[193][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F203.InnerText, G203.InnerText);
                }
                row204.Visible = false;
                if (dt.Rows[194][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row204.Visible = true;
                    F204.InnerText = dt.Rows[194][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G204.InnerText = dt.Rows[194][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H204.InnerText = dt.Rows[194][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F204.InnerText, G204.InnerText, H204.InnerText);
                }
                row205.Visible = false;
                if (dt.Rows[195][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row205.Visible = true;
                    F205.InnerText = dt.Rows[195][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G205.InnerText = dt.Rows[195][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F205.InnerText, G205.InnerText);
                }
                row206.Visible = false;
                if (dt.Rows[196][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row206.Visible = true;
                    F206.InnerText = dt.Rows[196][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G206.InnerText = dt.Rows[196][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H206.InnerText = dt.Rows[196][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F206.InnerText, G206.InnerText, H206.InnerText);
                }
                row207.Visible = false;
                if (dt.Rows[197][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row207.Visible = true;
                    F207.InnerText = dt.Rows[197][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G207.InnerText = dt.Rows[197][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F207.InnerText, G207.InnerText);
                }
                row208.Visible = false;
                if (dt.Rows[198][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row208.Visible = true;
                    F208.InnerText = dt.Rows[198][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G208.InnerText = dt.Rows[198][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H208.InnerText = dt.Rows[198][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F208.InnerText, G208.InnerText, H208.InnerText);
                }
                row209.Visible = false;
                if (dt.Rows[199][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row209.Visible = true;
                    F209.InnerText = dt.Rows[199][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G209.InnerText = dt.Rows[199][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F209.InnerText, G209.InnerText);
                }
                row210.Visible = false;
                if (dt.Rows[200][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row210.Visible = true;
                    F210.InnerText = dt.Rows[200][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G210.InnerText = dt.Rows[200][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H210.InnerText = dt.Rows[200][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F210.InnerText, G210.InnerText, H210.InnerText);
                }
                row211.Visible = false;
                if (dt.Rows[201][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row211.Visible = true;
                    F211.InnerText = dt.Rows[201][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G211.InnerText = dt.Rows[201][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F211.InnerText, G211.InnerText);
                }
                row212.Visible = false;
                if (dt.Rows[202][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row212.Visible = true;
                    F212.InnerText = dt.Rows[202][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G212.InnerText = dt.Rows[202][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H212.InnerText = dt.Rows[202][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F212.InnerText, G212.InnerText, H212.InnerText);
                }
                row213.Visible = false;
                if (dt.Rows[203][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row213.Visible = true;
                    F213.InnerText = dt.Rows[203][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G213.InnerText = dt.Rows[203][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F213.InnerText, G213.InnerText);
                }
                G214.InnerText = dt.Rows[204][(int)ColumnSUCC_DROITS.Col2].ToString();
                H214.InnerText = dt.Rows[204][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G214.InnerText, H214.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 3
            case3.Visible = false;
            if (ddl1Value == 3)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case3.Visible = true;
                row300.Visible = false;
                if (dt.Rows[290][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row300.Visible = true;
                    F300.InnerText = dt.Rows[290][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G300.InnerText = dt.Rows[290][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H300.InnerText = dt.Rows[290][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F300.InnerText, G300.InnerText, H300.InnerText);
                }
                row301.Visible = false;
                if (dt.Rows[291][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row301.Visible = true;
                    F301.InnerText = dt.Rows[291][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G301.InnerText = dt.Rows[291][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F301.InnerText, G301.InnerText);
                }
                row302.Visible = false;
                if (dt.Rows[292][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row302.Visible = true;
                    F302.InnerText = dt.Rows[292][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G302.InnerText = dt.Rows[292][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H302.InnerText = dt.Rows[292][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F302.InnerText, G302.InnerText, H302.InnerText);
                }
                row303.Visible = false;
                if (dt.Rows[293][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row303.Visible = true;
                    F303.InnerText = dt.Rows[293][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G303.InnerText = dt.Rows[293][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F303.InnerText, G303.InnerText);
                }
                G304.InnerText = dt.Rows[294][(int)ColumnSUCC_DROITS.Col2].ToString();
                H304.InnerText = dt.Rows[294][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G304.InnerText, H304.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 4
            case4.Visible = false;
            if (ddl1Value == 4)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case4.Visible = true;
                row400.Visible = false;
                if (dt.Rows[390][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row400.Visible = true;
                    F400.InnerText = dt.Rows[390][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G400.InnerText = dt.Rows[390][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H400.InnerText = dt.Rows[390][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F400.InnerText, G400.InnerText, H400.InnerText);
                }
                row401.Visible = false;
                if (dt.Rows[391][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row401.Visible = true;
                    F401.InnerText = dt.Rows[391][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G401.InnerText = dt.Rows[391][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F401.InnerText, G401.InnerText);
                }
                row402.Visible = false;
                if (dt.Rows[392][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row402.Visible = true;
                    F402.InnerText = dt.Rows[392][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G402.InnerText = dt.Rows[392][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H402.InnerText = dt.Rows[392][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F402.InnerText, G402.InnerText, H402.InnerText);
                }
                row403.Visible = false;
                if (dt.Rows[393][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row403.Visible = true;
                    F403.InnerText = dt.Rows[393][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G403.InnerText = dt.Rows[393][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F403.InnerText, G403.InnerText);
                }
                G404.InnerText = dt.Rows[394][(int)ColumnSUCC_DROITS.Col2].ToString();
                H404.InnerText = dt.Rows[394][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G404.InnerText, H404.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 5
            case5.Visible = false;
            if (ddl1Value == 5)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case5.Visible = true;
                row500.Visible = false;
                if (dt.Rows[490][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row500.Visible = true;
                    F500.InnerText = dt.Rows[490][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G500.InnerText = dt.Rows[490][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H500.InnerText = dt.Rows[490][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F500.InnerText, G500.InnerText, H500.InnerText);
                }
                row501.Visible = false;
                if (dt.Rows[491][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row501.Visible = true;
                    F501.InnerText = dt.Rows[491][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G501.InnerText = dt.Rows[491][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F501.InnerText, G501.InnerText);
                }
                row502.Visible = false;
                if (dt.Rows[492][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row502.Visible = true;
                    F502.InnerText = dt.Rows[492][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G502.InnerText = dt.Rows[492][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H502.InnerText = dt.Rows[492][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F502.InnerText, G502.InnerText, H502.InnerText);
                }
                row503.Visible = false;
                if (dt.Rows[493][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row503.Visible = true;
                    F503.InnerText = dt.Rows[493][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G503.InnerText = dt.Rows[493][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F503.InnerText, G503.InnerText);
                }
                G504.InnerText = dt.Rows[494][(int)ColumnSUCC_DROITS.Col2].ToString();
                H504.InnerText = dt.Rows[494][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G504.InnerText, H504.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 6
            case6.Visible = false;
            if (ddl1Value == 6)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case6.Visible = true;
                F600.InnerText = dt.Rows[590][(int)ColumnSUCC_DROITS.Col1].ToString();
                G600.InnerText = dt.Rows[590][(int)ColumnSUCC_DROITS.Col2].ToString();
                H600.InnerText = dt.Rows[590][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                    F600.InnerText, G600.InnerText, H600.InnerText);
                G601.InnerText = dt.Rows[591][(int)ColumnSUCC_DROITS.Col2].ToString();
                H601.InnerText = dt.Rows[591][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G601.InnerText, H601.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 7
            case7.Visible = false;
            if (ddl1Value == 7)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case7.Visible = true;
                F700.InnerText = dt.Rows[690][(int)ColumnSUCC_DROITS.Col1].ToString();
                G700.InnerText = dt.Rows[690][(int)ColumnSUCC_DROITS.Col2].ToString();
                H700.InnerText = dt.Rows[690][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                    F700.InnerText, G700.InnerText, H700.InnerText);
                G701.InnerText = dt.Rows[691][(int)ColumnSUCC_DROITS.Col2].ToString();
                H701.InnerText = dt.Rows[691][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G701.InnerText, H701.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 8
            case8.Visible = false;
            if (ddl1Value == 8)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case8.Visible = true;
                row800.Visible = false;
                if (dt.Rows[790][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row800.Visible = true;
                    F800.InnerText = dt.Rows[790][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G800.InnerText = dt.Rows[790][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H800.InnerText = dt.Rows[790][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F800.InnerText, G800.InnerText, H800.InnerText);
                }
                row801.Visible = false;
                if (dt.Rows[791][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row801.Visible = true;
                    F801.InnerText = dt.Rows[791][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G801.InnerText = dt.Rows[791][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F801.InnerText, G801.InnerText);
                }
                row802.Visible = false;
                if (dt.Rows[792][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row802.Visible = true;
                    F802.InnerText = dt.Rows[792][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G802.InnerText = dt.Rows[792][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H802.InnerText = dt.Rows[792][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F802.InnerText, G802.InnerText, H802.InnerText);
                }
                row803.Visible = false;
                if (dt.Rows[793][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row803.Visible = true;
                    F803.InnerText = dt.Rows[793][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G803.InnerText = dt.Rows[793][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F803.InnerText, G803.InnerText);
                }
                row804.Visible = false;
                if (dt.Rows[794][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row804.Visible = true;
                    F804.InnerText = dt.Rows[794][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G804.InnerText = dt.Rows[794][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H804.InnerText = dt.Rows[794][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F804.InnerText, G804.InnerText, H804.InnerText);
                }
                row805.Visible = false;
                if (dt.Rows[795][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row805.Visible = true;
                    F805.InnerText = dt.Rows[795][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G805.InnerText = dt.Rows[795][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F805.InnerText, G805.InnerText);
                }
                row806.Visible = false;
                if (dt.Rows[796][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row806.Visible = true;
                    F806.InnerText = dt.Rows[796][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G806.InnerText = dt.Rows[796][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H806.InnerText = dt.Rows[796][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F806.InnerText, G806.InnerText, H806.InnerText);
                }
                row807.Visible = false;
                if (dt.Rows[797][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row807.Visible = true;
                    F807.InnerText = dt.Rows[797][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G807.InnerText = dt.Rows[797][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F807.InnerText, G807.InnerText);
                }
                row808.Visible = false;
                if (dt.Rows[798][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row808.Visible = true;
                    F808.InnerText = dt.Rows[798][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G808.InnerText = dt.Rows[798][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H808.InnerText = dt.Rows[798][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F808.InnerText, G808.InnerText, H808.InnerText);
                }
                row809.Visible = false;
                if (dt.Rows[799][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row809.Visible = true;
                    F809.InnerText = dt.Rows[799][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G809.InnerText = dt.Rows[799][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F809.InnerText, G809.InnerText);
                }
                row810.Visible = false;
                if (dt.Rows[800][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row810.Visible = true;
                    F810.InnerText = dt.Rows[800][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G810.InnerText = dt.Rows[800][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H810.InnerText = dt.Rows[800][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F810.InnerText, G810.InnerText, H810.InnerText);
                }
                row811.Visible = false;
                if (dt.Rows[801][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row811.Visible = true;
                    F811.InnerText = dt.Rows[801][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G811.InnerText = dt.Rows[801][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F811.InnerText, G811.InnerText);
                }
                row812.Visible = false;
                if (dt.Rows[802][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row812.Visible = true;
                    F812.InnerText = dt.Rows[802][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G812.InnerText = dt.Rows[802][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H812.InnerText = dt.Rows[802][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F812.InnerText, G812.InnerText, H812.InnerText);
                }
                row813.Visible = false;
                if (dt.Rows[803][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row813.Visible = true;
                    F813.InnerText = dt.Rows[803][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G813.InnerText = dt.Rows[803][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F813.InnerText, G813.InnerText);
                }
                G814.InnerText = dt.Rows[804][(int)ColumnSUCC_DROITS.Col2].ToString();
                H814.InnerText = dt.Rows[804][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G814.InnerText, H814.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Détermination des droits à payer - case 9
            case9.Visible = false;
            if (ddl1Value == 9)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination des droits à payer</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                case9.Visible = true;
                row900.Visible = false;
                if (dt.Rows[890][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row900.Visible = true;
                    F900.InnerText = dt.Rows[890][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G900.InnerText = dt.Rows[890][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H900.InnerText = dt.Rows[890][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F900.InnerText, G900.InnerText, H900.InnerText);
                }
                row901.Visible = false;
                if (dt.Rows[891][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row901.Visible = true;
                    F901.InnerText = dt.Rows[891][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G901.InnerText = dt.Rows[891][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F901.InnerText, G901.InnerText);
                }
                row902.Visible = false;
                if (dt.Rows[892][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row902.Visible = true;
                    F902.InnerText = dt.Rows[892][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G902.InnerText = dt.Rows[892][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H902.InnerText = dt.Rows[892][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F902.InnerText, G902.InnerText, H902.InnerText);
                }
                row903.Visible = false;
                if (dt.Rows[893][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row903.Visible = true;
                    F903.InnerText = dt.Rows[893][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G903.InnerText = dt.Rows[893][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F903.InnerText, G903.InnerText);
                }
                row904.Visible = false;
                if (dt.Rows[894][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row904.Visible = true;
                    F904.InnerText = dt.Rows[894][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G904.InnerText = dt.Rows[894][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H904.InnerText = dt.Rows[894][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F904.InnerText, G904.InnerText, H904.InnerText);
                }
                row905.Visible = false;
                if (dt.Rows[895][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row905.Visible = true;
                    F905.InnerText = dt.Rows[895][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G905.InnerText = dt.Rows[895][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F905.InnerText, G905.InnerText);
                }
                row906.Visible = false;
                if (dt.Rows[896][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row906.Visible = true;
                    F906.InnerText = dt.Rows[896][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G906.InnerText = dt.Rows[896][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H906.InnerText = dt.Rows[896][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F906.InnerText, G906.InnerText, H906.InnerText);
                }
                row907.Visible = false;
                if (dt.Rows[897][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row907.Visible = true;
                    F907.InnerText = dt.Rows[897][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G907.InnerText = dt.Rows[897][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F907.InnerText, G907.InnerText);
                }
                row908.Visible = false;
                if (dt.Rows[898][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row908.Visible = true;
                    F908.InnerText = dt.Rows[898][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G908.InnerText = dt.Rows[898][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H908.InnerText = dt.Rows[898][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F908.InnerText, G908.InnerText, H908.InnerText);
                }
                row909.Visible = false;
                if (dt.Rows[899][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row909.Visible = true;
                    F909.InnerText = dt.Rows[899][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G909.InnerText = dt.Rows[899][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F909.InnerText, G909.InnerText);
                }
                row910.Visible = false;
                if (dt.Rows[900][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row910.Visible = true;
                    F910.InnerText = dt.Rows[900][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G910.InnerText = dt.Rows[900][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H910.InnerText = dt.Rows[900][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F910.InnerText, G910.InnerText, H910.InnerText);
                }
                row911.Visible = false;
                if (dt.Rows[901][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row911.Visible = true;
                    F911.InnerText = dt.Rows[901][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G911.InnerText = dt.Rows[901][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F911.InnerText, G911.InnerText);
                }
                row912.Visible = false;
                if (dt.Rows[902][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row912.Visible = true;
                    F912.InnerText = dt.Rows[902][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G912.InnerText = dt.Rows[902][(int)ColumnSUCC_DROITS.Col2].ToString();
                    H912.InnerText = dt.Rows[902][(int)ColumnSUCC_DROITS.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}</td><td align='right'>{2}</td><td></td></tr>",
                        F912.InnerText, G912.InnerText, H912.InnerText);
                }
                row913.Visible = false;
                if (dt.Rows[903][(int)ColumnSUCC_DROITS.Col4].ToString() == show)
                {
                    row913.Visible = true;
                    F913.InnerText = dt.Rows[903][(int)ColumnSUCC_DROITS.Col1].ToString();
                    G913.InnerText = dt.Rows[903][(int)ColumnSUCC_DROITS.Col2].ToString();
                    html += string.Format("<tr><td align='right'>{0}</td><td colspan='3' align='center'>{1}</td></tr>",
                        F913.InnerText, G913.InnerText);
                }
                G914.InnerText = dt.Rows[904][(int)ColumnSUCC_DROITS.Col2].ToString();
                H914.InnerText = dt.Rows[904][(int)ColumnSUCC_DROITS.Col3].ToString();
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    G914.InnerText, H914.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "SUCC-DROITS.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 9, 3, 905, 7, out _status);
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
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','chk1':'" + chk1.Checked +
                           "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text +
                           "','chk2':'" + chk2.Checked +
                           "','txtZone05':'" + txtZone05.Text + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "SUCC-DROITS", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chk1':'" + chk1.Checked +
                           "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text +
                           "','chk2':'" + chk2.Checked +
                           "','txtZone05':'" + txtZone05.Text + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "SUCC-DROITS", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("SUCC-DROITS", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("SUCC-DROITS", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnSUCC_DROITS
    {
        Col1 = 2,
        Col2 = 3,
        Col3 = 4,
        Col4 = 5
    }
}