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
    public partial class BF11 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF11>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                ddl1.SelectedValue = data.ddl1;
                ddl2.SelectedValue = data.ddl2;
                ddl3.SelectedValue = data.ddl3;
                ddl4.SelectedValue = data.ddl4;
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
            var range = new RangeCoordinates
            {
                Row = 7,
                Column = 3,
                Height = 12,
                Width = 1
            };
            var rangeValues = new object[]{
                    new object[] {ddl1.SelectedValue},
                    new object[] {ddl2.SelectedValue},
                    new object[] {ddl3.SelectedValue},
                    new object[] {ddl4.SelectedValue},
                    new object[] {null},
                    new object[] {txtZone01.Text},
                    new object[] {txtZone02.Text},
                    new object[] {txtZone03.Text},
                    new object[] {txtZone04.Text},
                    new object[] {txtEmolument_de_formalités_HT.Text},
                    new object[] {txtDébours.Text},
                    new object[] {hdUtilisation_du_futur_tarif.Value}
                };
            _excelService.SetRange(_sessionId, "ENTREE_S", range, rangeValues);
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            var html = "";
            //Total des droits et frais
            G83.InnerText = dt.Rows[79][(int)ColumnBF11.Column3].ToString();
            G82.InnerText = dt.Rows[78][(int)ColumnBF11.Column3].ToString();
            G81.InnerText = dt.Rows[77][(int)ColumnBF11.Column3].ToString();
            G84.InnerText = dt.Rows[80][(int)ColumnBF11.Column3].ToString();
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td width='700' bgcolor='#304F73' valign='middle' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G83.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G82.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G81.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G84.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF11\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td width='700' bgcolor='#304F73' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments proportionnels 1
            row5.Visible = false;
            if (dt.Rows[1][(int)ColumnBF11.Column4].Equals(show))
            {
                row5.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments proportionnels</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row6.Visible = false;
                if (dt.Rows[2][(int)ColumnBF11.Column4].Equals(show))
                {
                    row6.Visible = true;
                    E6.InnerText = dt.Rows[2][(int)ColumnBF11.Column1].ToString();
                    F6.InnerText = dt.Rows[2][(int)ColumnBF11.Column2].ToString();
                    G6.InnerText = dt.Rows[2][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E6.InnerText, F6.InnerText, G6.InnerText);
                }
                row7.Visible = false;
                if (dt.Rows[3][(int)ColumnBF11.Column4].Equals(show))
                {
                    row7.Visible = true;
                    E7.InnerText = dt.Rows[3][(int)ColumnBF11.Column1].ToString();
                    F7.InnerText = dt.Rows[3][(int)ColumnBF11.Column2].ToString();
                    G7.InnerText = dt.Rows[3][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E7.InnerText, F7.InnerText, G7.InnerText);
                }
                row8.Visible = false;
                if (dt.Rows[4][(int)ColumnBF11.Column4].Equals(show))
                {
                    row8.Visible = true;
                    E8.InnerText = dt.Rows[4][(int)ColumnBF11.Column1].ToString();
                    F8.InnerText = dt.Rows[4][(int)ColumnBF11.Column2].ToString();
                    G8.InnerText = dt.Rows[4][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E8.InnerText, F8.InnerText, G8.InnerText);
                }
                row9.Visible = false;
                if (dt.Rows[5][(int)ColumnBF11.Column4].Equals(show))
                {
                    row9.Visible = true;
                    E9.InnerText = dt.Rows[5][(int)ColumnBF11.Column1].ToString();
                    F9.InnerText = dt.Rows[5][(int)ColumnBF11.Column2].ToString();
                    G9.InnerText = dt.Rows[5][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E9.InnerText, F9.InnerText, G9.InnerText);
                }
                row10.Visible = false;
                if (dt.Rows[6][(int)ColumnBF11.Column4].Equals(show))
                {
                    row10.Visible = true;
                    F10.InnerText = dt.Rows[6][(int)ColumnBF11.Column2].ToString();
                    html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                    F10.InnerText);
                }
                row11.Visible = false;
                if (dt.Rows[7][(int)ColumnBF11.Column4].Equals(show))
                {
                    row11.Visible = true;
                    G11.InnerText = dt.Rows[7][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                    G11.InnerText);
                }
                row12.Visible = false;
                if (dt.Rows[8][(int)ColumnBF11.Column4].Equals(show))
                {
                    row12.Visible = true;
                    F12.InnerText = dt.Rows[8][(int)ColumnBF11.Column2].ToString();
                    G12.InnerText = dt.Rows[8][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>" + F12.InnerText + "</td><td align='right'>{0}</td><td></td></tr>",
                    G12.InnerText);
                }
                row13.Visible = false;
                if (dt.Rows[9][(int)ColumnBF11.Column4].Equals(show))
                {
                    row13.Visible = true;
                    G13.InnerText = dt.Rows[9][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument fixe HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G13.InnerText);
                }
                row14.Visible = false;
                if (dt.Rows[10][(int)ColumnBF11.Column4].Equals(show))
                {
                    row14.Visible = true;
                    G14.InnerText = dt.Rows[10][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G14.InnerText);
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments proportionnels 2
            row16.Visible = false;
            if (dt.Rows[12][(int)ColumnBF11.Column4].Equals(show))
            {
                row16.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments proportionnels</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row17.Visible = false;
                if (dt.Rows[13][(int)ColumnBF11.Column4].Equals(show))
                {
                    row17.Visible = true;
                    E17.InnerText = dt.Rows[13][(int)ColumnBF11.Column1].ToString();
                    F17.InnerText = dt.Rows[13][(int)ColumnBF11.Column2].ToString();
                    G17.InnerText = dt.Rows[13][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E17.InnerText, F17.InnerText, G17.InnerText);
                }
                row18.Visible = false;
                if (dt.Rows[14][(int)ColumnBF11.Column4].Equals(show))
                {
                    row18.Visible = true;
                    E18.InnerText = dt.Rows[14][(int)ColumnBF11.Column1].ToString();
                    F18.InnerText = dt.Rows[14][(int)ColumnBF11.Column2].ToString();
                    G18.InnerText = dt.Rows[14][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E18.InnerText, F18.InnerText, G18.InnerText);
                }
                row19.Visible = false;
                if (dt.Rows[15][(int)ColumnBF11.Column4].Equals(show))
                {
                    row19.Visible = true;
                    E19.InnerText = dt.Rows[15][(int)ColumnBF11.Column1].ToString();
                    F19.InnerText = dt.Rows[15][(int)ColumnBF11.Column2].ToString();
                    G19.InnerText = dt.Rows[15][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E19.InnerText, F19.InnerText, G19.InnerText);
                }
                row20.Visible = false;
                if (dt.Rows[16][(int)ColumnBF11.Column4].Equals(show))
                {
                    row20.Visible = true;
                    E20.InnerText = dt.Rows[16][(int)ColumnBF11.Column1].ToString();
                    F20.InnerText = dt.Rows[16][(int)ColumnBF11.Column2].ToString();
                    G20.InnerText = dt.Rows[16][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E20.InnerText, F20.InnerText, G20.InnerText);
                }
                row21.Visible = false;
                if (dt.Rows[17][(int)ColumnBF11.Column4].Equals(show))
                {
                    row21.Visible = true;
                    F21.InnerText = dt.Rows[17][(int)ColumnBF11.Column2].ToString();
                    html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                    F21.InnerText);
                }
                row22.Visible = false;
                if (dt.Rows[18][(int)ColumnBF11.Column4].Equals(show))
                {
                    row22.Visible = true;
                    G22.InnerText = dt.Rows[18][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                    G22.InnerText);
                }
                row23.Visible = false;
                if (dt.Rows[19][(int)ColumnBF11.Column4].Equals(show))
                {
                    row23.Visible = true;
                    G23.InnerText = dt.Rows[19][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G23.InnerText);
                }
                html += "<tr><td colspan='4'></td></tr>";
            }

            //Emoluments proportionnels 3
            row25.Visible = false;
            if (dt.Rows[21][(int)ColumnBF11.Column4].Equals(show))
            {
                row25.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments proportionnels</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                html += "<tr><td colspan='4' align='center'>Au titre de la quittance</td></tr>";
                row27.Visible = false;
                if (dt.Rows[23][(int)ColumnBF11.Column4].Equals(show))
                {
                    row27.Visible = true;
                    E27.InnerText = dt.Rows[23][(int)ColumnBF11.Column1].ToString();
                    F27.InnerText = dt.Rows[23][(int)ColumnBF11.Column2].ToString();
                    G27.InnerText = dt.Rows[23][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E27.InnerText, F27.InnerText, G27.InnerText);
                }
                row28.Visible = false;
                if (dt.Rows[24][(int)ColumnBF11.Column4].Equals(show))
                {
                    row28.Visible = true;
                    E28.InnerText = dt.Rows[24][(int)ColumnBF11.Column1].ToString();
                    F28.InnerText = dt.Rows[24][(int)ColumnBF11.Column2].ToString();
                    G28.InnerText = dt.Rows[24][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E28.InnerText, F28.InnerText, G28.InnerText);
                }
                row29.Visible = false;
                if (dt.Rows[25][(int)ColumnBF11.Column4].Equals(show))
                {
                    row29.Visible = true;
                    E29.InnerText = dt.Rows[25][(int)ColumnBF11.Column1].ToString();
                    F29.InnerText = dt.Rows[25][(int)ColumnBF11.Column2].ToString();
                    G29.InnerText = dt.Rows[25][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E29.InnerText, F29.InnerText, G29.InnerText);
                }
                row30.Visible = false;
                if (dt.Rows[26][(int)ColumnBF11.Column4].Equals(show))
                {
                    row30.Visible = true;
                    E30.InnerText = dt.Rows[26][(int)ColumnBF11.Column1].ToString();
                    F30.InnerText = dt.Rows[26][(int)ColumnBF11.Column2].ToString();
                    G30.InnerText = dt.Rows[26][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E30.InnerText, F30.InnerText, G30.InnerText);
                }
                row31.Visible = false;
                if (dt.Rows[27][(int)ColumnBF11.Column4].Equals(show))
                {
                    row31.Visible = true;
                    F31.InnerText = dt.Rows[27][(int)ColumnBF11.Column2].ToString();
                    html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                    F31.InnerText);
                }
                row32.Visible = false;
                if (dt.Rows[28][(int)ColumnBF11.Column4].Equals(show))
                {
                    row32.Visible = true;
                    G32.InnerText = dt.Rows[28][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                    G32.InnerText);
                }
                row33.Visible = false;
                if (dt.Rows[29][(int)ColumnBF11.Column4].Equals(show))
                {
                    row33.Visible = true;
                    G33.InnerText = dt.Rows[29][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G33.InnerText);
                }
                //
                row35.Visible = false;
                if (dt.Rows[31][(int)ColumnBF11.Column4].Equals(show))
                {
                    row35.Visible = true;
                    html += "<tr><td colspan='4' align='center'>Au titre de la mainlevée</td></tr>";
                    row37.Visible = false;
                    if (dt.Rows[33][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row37.Visible = true;
                        E37.InnerText = dt.Rows[33][(int)ColumnBF11.Column1].ToString();
                        F37.InnerText = dt.Rows[33][(int)ColumnBF11.Column2].ToString();
                        G37.InnerText = dt.Rows[33][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E37.InnerText, F37.InnerText, G37.InnerText);
                    }
                    row38.Visible = false;
                    if (dt.Rows[34][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row38.Visible = true;
                        E38.InnerText = dt.Rows[34][(int)ColumnBF11.Column1].ToString();
                        F38.InnerText = dt.Rows[34][(int)ColumnBF11.Column2].ToString();
                        G38.InnerText = dt.Rows[34][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E38.InnerText, F38.InnerText, G38.InnerText);
                    }
                    row39.Visible = false;
                    if (dt.Rows[35][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row39.Visible = true;
                        E39.InnerText = dt.Rows[35][(int)ColumnBF11.Column1].ToString();
                        F39.InnerText = dt.Rows[35][(int)ColumnBF11.Column2].ToString();
                        G39.InnerText = dt.Rows[35][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E39.InnerText, F39.InnerText, G39.InnerText);
                    }
                    row40.Visible = false;
                    if (dt.Rows[36][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row40.Visible = true;
                        E40.InnerText = dt.Rows[36][(int)ColumnBF11.Column1].ToString();
                        F40.InnerText = dt.Rows[36][(int)ColumnBF11.Column2].ToString();
                        G40.InnerText = dt.Rows[36][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        E40.InnerText, F40.InnerText, G40.InnerText);
                    }
                    row41.Visible = false;
                    if (dt.Rows[37][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row41.Visible = true;
                        F41.InnerText = dt.Rows[37][(int)ColumnBF11.Column2].ToString();
                        html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                        F41.InnerText);
                    }
                    row42.Visible = false;
                    if (dt.Rows[38][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row42.Visible = true;
                        G42.InnerText = dt.Rows[38][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                        G42.InnerText);
                    }
                    row43.Visible = false;
                    if (dt.Rows[39][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row43.Visible = true;
                        G43.InnerText = dt.Rows[39][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td colspan='2' align='right'>Emolument fixe HT :</td><td align='right'>{0}</td><td></td></tr>",
                        G43.InnerText);
                    }
                    row44.Visible = false;
                    if (dt.Rows[40][(int)ColumnBF11.Column4].Equals(show))
                    {
                        row44.Visible = true;
                        G44.InnerText = dt.Rows[40][(int)ColumnBF11.Column3].ToString();
                        html += string.Format(
                        "<tr><td colspan='2' align='right'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                        G44.InnerText);
                    }
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G53.InnerText = dt.Rows[49][(int)ColumnBF11.Column3].ToString();
            G54.InnerText = dt.Rows[50][(int)ColumnBF11.Column3].ToString();
            G55.InnerText = dt.Rows[51][(int)ColumnBF11.Column3].ToString();
            G56.InnerText = dt.Rows[52][(int)ColumnBF11.Column3].ToString();
            G57.InnerText = dt.Rows[53][(int)ColumnBF11.Column3].ToString();
            html += string.Format(
            "<tr><td colspan='2' align='right'>Emoluments du notaire HT :</td><td align='right'>{0}</td><td></td></tr>",
            G53.InnerText);
            html += string.Format(
            "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
            G54.InnerText);
            html += string.Format(
            "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
            G55.InnerText);
            html += string.Format(
            "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
            G56.InnerText);
            html += string.Format(
            "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
            G57.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G61.InnerText = dt.Rows[57][(int)ColumnBF11.Column3].ToString();
            html += string.Format(
            "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
            G61.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td width='700' bgcolor='#304F73' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Enregistrement
            html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Enregistrement</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            D67.Visible = false;
            if (dt.Rows[63][(int)ColumnBF11.Column4].Equals(show))
            {
                D67.Visible = true;
                html += "<tr><td colspan='4' align='center'>Acte dispensé de la formalité (CGI art 846 bis)</td></tr>";
            }
            D68.Visible = false;
            if (dt.Rows[64][(int)ColumnBF11.Column4].Equals(show))
            {
                D68.Visible = true;
                html += "<tr><td colspan='4' align='center'>Acte dispensé de la formalité (CGI art 680)</td></tr>";
            }
            G69.InnerText = dt.Rows[65][(int)ColumnBF11.Column3].ToString();
            html += string.Format(
            "<tr><td colspan='2' align='right'>Paiement du droit sur état :</td><td align='right'>{0}</td><td></td></tr>",
            G69.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Contribution pour la securite immobiliere
            row71.Visible = false;
            if (dt.Rows[67][(int)ColumnBF11.Column4].Equals(show))
            {
                row71.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Contribution pour la sécurité immobilière :</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row72.Visible = false;
                if (dt.Rows[68][(int)ColumnBF11.Column4].Equals(show))
                {
                    row72.Visible = true;
                    E72.InnerText = dt.Rows[68][(int)ColumnBF11.Column1].ToString();
                    F72.InnerText = dt.Rows[68][(int)ColumnBF11.Column2].ToString();
                    G72.InnerText = dt.Rows[68][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E72.InnerText, F72.InnerText, G72.InnerText);
                }
                row73.Visible = false;
                if (dt.Rows[69][(int)ColumnBF11.Column4].Equals(show))
                {
                    row73.Visible = true;
                    G73.InnerText = dt.Rows[69][(int)ColumnBF11.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                    G73.InnerText);
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Greffe
            row75.Visible = false;
            if (dt.Rows[71][(int)ColumnBF11.Column4].Equals(show))
            {
                row75.Visible = true;
                html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Greffe</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                G77.InnerText = dt.Rows[73][(int)ColumnBF11.Column3].ToString();
                html += string.Format(
                "<tr><td colspan='2' align='right'>Coût pour une radiation totale :</td><td align='right'>{0}</td><td></td></tr>",
                G73.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF11-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 3, 1, 83, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF11");
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
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF11", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF11", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF11", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF11", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF11
    {
        Column1 = 3,
        Column2 = 4,
        Column3 = 5,
        Column4 = 6
    }

    class DataModelBF11
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string ddl1 { get; set; }
        public string ddl2 { get; set; }
        public string ddl3 { get; set; }
        public string ddl4 { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}