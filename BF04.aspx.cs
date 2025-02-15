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
    public partial class BF04 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF04>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                ddl1.SelectedValue = data.ddl1;
                ddl2.SelectedValue = data.ddl2;
                ddl3.SelectedValue = data.ddl3;
                ddl4.SelectedValue = data.ddl4;
                ddl5.SelectedValue = data.ddl5;
                txtZone01.Text = data.txtZone01;
                txtZone02.Text = data.txtZone02;
                txtZone03.Text = data.txtZone03;
                txtZone04.Text = data.txtZone04;
                txtZone05.Text = data.txtZone05;
                txtZone06.Text = data.txtZone06;
                txtZone07.Text = data.txtZone07;
                txtZone08.Text = data.txtZone08;
                chk1.Checked = data.chk1.TransformToBoolean();
                chk2.Checked = data.chk2.TransformToBoolean();
                chk3.Checked = data.chk3.TransformToBoolean();
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
                Height = 19,
                Width = 1
            };
            var rangeValues = new object[]{
                    new object[] {ddl1.SelectedValue},
                    new object[] {chk1.Checked.TransformToBooleanFr()},
                    new object[] {ddl2.SelectedValue},
                    new object[] {ddl3.SelectedValue},
                    new object[] {txtZone01.Text},
                    new object[] {txtZone02.Text},
                    new object[] {txtZone03.Text},
                    new object[] {txtZone04.Text},
                    new object[] {ddl4.SelectedValue},
                    new object[] {txtZone05.Text},
                    new object[] {txtZone06.Text},
                    new object[] {ddl5.SelectedValue},
                    new object[] {txtZone07.Text},
                    new object[] {chk2.Checked.TransformToBooleanFr()},
                    new object[] {chk3.Checked.TransformToBooleanFr()},
                    new object[] {txtZone08.Text},
                    new object[] {txtEmolument_de_formalités_HT.Text},
                    new object[] {txtDébours.Text},
                    new object[] {hdUtilisation_du_futur_tarif.Value}
                };
            _excelService.SetRange(_sessionId, "ENTREE_S", range, rangeValues);
        }

        private void SetValuesTaxation(DataTable dt)
        {
            const string show = "1";
            var html = "";
            //Total des droits et frais
            G45.InnerText = dt.Rows[39][(int)ColumnBF04.Column3].ToString();
            G46.InnerText = dt.Rows[40][(int)ColumnBF04.Column3].ToString();
            G47.InnerText = dt.Rows[41][(int)ColumnBF04.Column3].ToString();
            G48.InnerText = dt.Rows[42][(int)ColumnBF04.Column3].ToString();
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G47.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G46.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G45.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G48.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF04\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments proportionnels
            row12.Visible = false;
            if (dt.Rows[6][(int)ColumnBF04.Column4].Equals(show))
            {
                row12.Visible = true;
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments proportionnels</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row13.Visible = false;
                if (dt.Rows[7][(int)ColumnBF04.Column4].Equals(show))
                {
                    row13.Visible = true;
                    E13.InnerText = dt.Rows[7][(int)ColumnBF04.Column1].ToString();
                    F13.InnerText = dt.Rows[7][(int)ColumnBF04.Column2].ToString();
                    G13.InnerText = dt.Rows[7][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E13.InnerText, F13.InnerText, G13.InnerText);
                }
                row14.Visible = false;
                if (dt.Rows[8][(int)ColumnBF04.Column4].Equals(show))
                {
                    row14.Visible = true;
                    E14.InnerText = dt.Rows[8][(int)ColumnBF04.Column1].ToString();
                    F14.InnerText = dt.Rows[8][(int)ColumnBF04.Column2].ToString();
                    G14.InnerText = dt.Rows[8][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E14.InnerText, F14.InnerText, G14.InnerText);
                }
                row15.Visible = false;
                if (dt.Rows[9][(int)ColumnBF04.Column4].Equals(show))
                {
                    row15.Visible = true;
                    E15.InnerText = dt.Rows[9][(int)ColumnBF04.Column1].ToString();
                    F15.InnerText = dt.Rows[9][(int)ColumnBF04.Column2].ToString();
                    G15.InnerText = dt.Rows[9][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E15.InnerText, F15.InnerText, G15.InnerText);
                }
                row16.Visible = false;
                if (dt.Rows[10][(int)ColumnBF04.Column4].Equals(show))
                {
                    row16.Visible = true;
                    E16.InnerText = dt.Rows[10][(int)ColumnBF04.Column1].ToString();
                    F16.InnerText = dt.Rows[10][(int)ColumnBF04.Column2].ToString();
                    G16.InnerText = dt.Rows[10][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E16.InnerText, F16.InnerText, G16.InnerText);
                }
                F17.InnerText = dt.Rows[11][(int)ColumnBF04.Column2].ToString();
                html += string.Format(
                "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                F17.InnerText);
                row18.Visible = false;
                if (dt.Rows[12][(int)ColumnBF04.Column4].Equals(show))
                {
                    row18.Visible = true;
                    G18.InnerText = dt.Rows[12][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G18.InnerText);
                }
                row19.Visible = false;
                if (dt.Rows[13][(int)ColumnBF04.Column4].Equals(show))
                {
                    row19.Visible = true;
                    G19.InnerText = dt.Rows[13][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G19.InnerText);
                }
                row20.Visible = false;
                if (dt.Rows[14][(int)ColumnBF04.Column4].Equals(show))
                {
                    row20.Visible = true;
                    G20.InnerText = dt.Rows[14][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de caution (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    G20.InnerText);
                }
                G21.InnerText = dt.Rows[15][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    G21.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G23.InnerText = dt.Rows[17][(int)ColumnBF04.Column3].ToString();
            G24.InnerText = dt.Rows[18][(int)ColumnBF04.Column3].ToString();
            G25.InnerText = dt.Rows[19][(int)ColumnBF04.Column3].ToString();
            G26.InnerText = dt.Rows[20][(int)ColumnBF04.Column3].ToString();
            G27.InnerText = dt.Rows[21][(int)ColumnBF04.Column3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                G23.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités :</td><td align='right'>{0}</td><td></td></tr>",
                G24.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                G25.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                G26.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                G27.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G30.InnerText = dt.Rows[24][(int)ColumnBF04.Column3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                G30.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row34.Visible = false;
            if (dt.Rows[28][(int)ColumnBF04.Column4].Equals(show))
            {
                row34.Visible = true;
                G35.InnerText = dt.Rows[29][(int)ColumnBF04.Column3].ToString();
                html += "<tr><td colspan='4' align='left'>Droit d'enregistrement : </td></tr>";
                html += string.Format(
                "<tr><td colspan='2' align='right'>Droits fixes :</td><td align='right'>{0}</td><td></td></tr>",
                G35.InnerText);
            }
            row36.Visible = false;
            if (dt.Rows[30][(int)ColumnBF04.Column4].Equals(show) && ddl1.SelectedValue != "4")
            {
                row36.Visible = true;
                html += "<tr><td colspan='4' align='left'>Taxe de Publicité foncière :</td></tr>";
                row37.Visible = false;
                if (dt.Rows[31][(int)ColumnBF04.Column4].Equals(show))
                {
                    row37.Visible = true;
                    E37.InnerText = dt.Rows[31][(int)ColumnBF04.Column1].ToString();
                    F37.InnerText = dt.Rows[31][(int)ColumnBF04.Column2].ToString();
                    G37.InnerText = dt.Rows[31][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E37.InnerText, F37.InnerText, G37.InnerText);
                }
                row38.Visible = false;
                if (dt.Rows[32][(int)ColumnBF04.Column4].Equals(show))
                {
                    row38.Visible = true;
                    G38.InnerText = dt.Rows[32][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                    G38.InnerText);
                }
                row39.Visible = false;
                if (dt.Rows[33][(int)ColumnBF04.Column4].Equals(show))
                {
                    row39.Visible = true;
                    html += "<tr><td colspan='4' align='center'>Exonération.</td></tr>";
                }
            }
            html += "<tr><td colspan='4' align='left'>Contribution pour la sécurité immobilière (art. 879 du CGI) :</td></tr>";
            row41.Visible = false;
            if (dt.Rows[35][(int)ColumnBF04.Column4].Equals(show))
            {
                row41.Visible = true;
                E41.InnerText = dt.Rows[35][(int)ColumnBF04.Column1].ToString();
                F41.InnerText = dt.Rows[35][(int)ColumnBF04.Column2].ToString();
                G41.InnerText = dt.Rows[35][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                E41.InnerText, F41.InnerText, G41.InnerText);
            }
            row42.Visible = false;
            if (dt.Rows[36][(int)ColumnBF04.Column4].Equals(show))
            {
                row42.Visible = true;
                G42.InnerText = dt.Rows[36][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                G42.InnerText);
            }
            html += "</table>";
            hdResult.Value = html;
        }

        private void SetValuesTaxationC(DataTable dt)
        {
            const string show = "1";
            var html = "";
            //Total des droits et frais
            G122.InnerText = dt.Rows[68][(int)ColumnBF04.Column3].ToString();
            G123.InnerText = dt.Rows[69][(int)ColumnBF04.Column3].ToString();
            G124.InnerText = dt.Rows[70][(int)ColumnBF04.Column3].ToString();
            G125.InnerText = dt.Rows[71][(int)ColumnBF04.Column3].ToString();
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G124.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G123.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G122.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    G125.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF04\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire sur les versements des 5 premières années
            row60.Visible = false;
            if (dt.Rows[6][(int)ColumnBF04.Column4].Equals(show))
            {
                row60.Visible = true;
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire sur les versements des 5 premières années</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                E61.InnerText = dt.Rows[7][(int)ColumnBF04.Column1].ToString();
                F61.InnerText = dt.Rows[7][(int)ColumnBF04.Column2].ToString();
                G61.InnerText = dt.Rows[7][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='center' colspan='4'>{0}</td></tr>",
                E61.InnerText);
                html += string.Format(
                "<tr><td align='center' colspan='4'>{0}</td></tr>",
                F61.InnerText);
                html += string.Format(
                "<tr><td colspan='2' align='right'>Base :</td><td align='right'>{0}</td><td></td></tr>",
                G61.InnerText);
                row62.Visible = false;
                if (dt.Rows[8][(int)ColumnBF04.Column4].Equals(show))
                {
                    row62.Visible = true;
                    E62.InnerText = dt.Rows[8][(int)ColumnBF04.Column1].ToString();
                    F62.InnerText = dt.Rows[8][(int)ColumnBF04.Column2].ToString();
                    G62.InnerText = dt.Rows[8][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E62.InnerText, F62.InnerText, G62.InnerText);
                }
                row63.Visible = false;
                if (dt.Rows[9][(int)ColumnBF04.Column4].Equals(show))
                {
                    row63.Visible = true;
                    E63.InnerText = dt.Rows[9][(int)ColumnBF04.Column1].ToString();
                    F63.InnerText = dt.Rows[9][(int)ColumnBF04.Column2].ToString();
                    G63.InnerText = dt.Rows[9][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E63.InnerText, F63.InnerText, G63.InnerText);
                }
                row64.Visible = false;
                if (dt.Rows[10][(int)ColumnBF04.Column4].Equals(show))
                {
                    row64.Visible = true;
                    E64.InnerText = dt.Rows[10][(int)ColumnBF04.Column1].ToString();
                    F64.InnerText = dt.Rows[10][(int)ColumnBF04.Column2].ToString();
                    G64.InnerText = dt.Rows[10][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E64.InnerText, F64.InnerText, G64.InnerText);
                }
                row65.Visible = false;
                if (dt.Rows[11][(int)ColumnBF04.Column4].Equals(show))
                {
                    row65.Visible = true;
                    E65.InnerText = dt.Rows[11][(int)ColumnBF04.Column1].ToString();
                    F65.InnerText = dt.Rows[11][(int)ColumnBF04.Column2].ToString();
                    G65.InnerText = dt.Rows[11][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E65.InnerText, F65.InnerText, G65.InnerText);
                }
                F66.InnerText = dt.Rows[12][(int)ColumnBF04.Column2].ToString();
                html += string.Format(
                "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                F66.InnerText);
                row67.Visible = false;
                if (dt.Rows[13][(int)ColumnBF04.Column4].Equals(show))
                {
                    row67.Visible = true;
                    G67.InnerText = dt.Rows[13][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right' colspan='2'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G67.InnerText);
                }
                G68.InnerText = dt.Rows[14][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right' colspan='2'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                G68.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments du notaire sur le surplus des versements après correction au-delà des 5 années
            row70.Visible = false;
            if (dt.Rows[16][(int)ColumnBF04.Column4].Equals(show))
            {
                row70.Visible = true;
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire sur le surplus des versements après correction au-delà des 5 années</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row72.Visible = false;
                if (dt.Rows[18][(int)ColumnBF04.Column4].Equals(show))
                {
                    row72.Visible = true;
                    E72.InnerText = dt.Rows[18][(int)ColumnBF04.Column1].ToString();
                    F72.InnerText = dt.Rows[18][(int)ColumnBF04.Column2].ToString();
                    F73.InnerText = dt.Rows[19][(int)ColumnBF04.Column2].ToString();
                    G73.InnerText = dt.Rows[19][(int)ColumnBF04.Column3].ToString();
                    html += "<tr><td colspan='4'>b) Versements à effectuer entre la 6ème et la 20ème année :</td></tr>";
                    html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    E72.InnerText);
                    html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    F72.InnerText);
                    html += string.Format(
                    "<tr><td align='right'>Retenus pour :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    F73.InnerText, G73.InnerText);
                }
                row74.Visible = false;
                if (dt.Rows[20][(int)ColumnBF04.Column4].Equals(show))
                {
                    row74.Visible = true;
                    E74.InnerText = dt.Rows[20][(int)ColumnBF04.Column1].ToString();
                    F74.InnerText = dt.Rows[20][(int)ColumnBF04.Column2].ToString();
                    F75.InnerText = dt.Rows[21][(int)ColumnBF04.Column2].ToString();
                    G75.InnerText = dt.Rows[21][(int)ColumnBF04.Column3].ToString();
                    html += "<tr><td colspan='4'>b1) Versements à effectuer entre la 21ème et la 60ème année :</td></tr>";
                    html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    E74.InnerText);
                    html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    F74.InnerText);
                    html += string.Format(
                    "<tr><td align='right'>Retenus pour 1/2 :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    F75.InnerText, G75.InnerText);
                }
                row76.Visible = false;
                if (dt.Rows[22][(int)ColumnBF04.Column4].Equals(show))
                {
                    row76.Visible = true;
                    E76.InnerText = dt.Rows[22][(int)ColumnBF04.Column1].ToString();
                    F76.InnerText = dt.Rows[22][(int)ColumnBF04.Column2].ToString();
                    F77.InnerText = dt.Rows[23][(int)ColumnBF04.Column2].ToString();
                    G77.InnerText = dt.Rows[23][(int)ColumnBF04.Column3].ToString();
                    html += "<tr><td colspan='4'>b2) Versements à effectuer entre la 61ème et le terme du bail :</td></tr>";
                    html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    E76.InnerText);
                    html += string.Format(
                    "<tr><td align='center' colspan='4'>{0}</td></tr>",
                    F76.InnerText);
                    html += string.Format(
                    "<tr><td align='right'>Retenus pour 1/4 :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    F77.InnerText, G77.InnerText);
                }
                G79.InnerText = dt.Rows[25][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td colspan='2' align='right'>Base :</td><td align='right'>{0}</td><td></td></tr>",
                G79.InnerText);
                row80.Visible = false;
                if (dt.Rows[26][(int)ColumnBF04.Column4].Equals(show))
                {
                    row80.Visible = true;
                    E80.InnerText = dt.Rows[26][(int)ColumnBF04.Column1].ToString();
                    F80.InnerText = dt.Rows[26][(int)ColumnBF04.Column2].ToString();
                    G80.InnerText = dt.Rows[26][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E80.InnerText, F80.InnerText, G80.InnerText);
                }
                row81.Visible = false;
                if (dt.Rows[27][(int)ColumnBF04.Column4].Equals(show))
                {
                    row81.Visible = true;
                    E81.InnerText = dt.Rows[27][(int)ColumnBF04.Column1].ToString();
                    F81.InnerText = dt.Rows[27][(int)ColumnBF04.Column2].ToString();
                    G81.InnerText = dt.Rows[27][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E81.InnerText, F81.InnerText, G81.InnerText);
                }
                row82.Visible = false;
                if (dt.Rows[28][(int)ColumnBF04.Column4].Equals(show))
                {
                    row82.Visible = true;
                    E82.InnerText = dt.Rows[28][(int)ColumnBF04.Column1].ToString();
                    F82.InnerText = dt.Rows[28][(int)ColumnBF04.Column2].ToString();
                    G82.InnerText = dt.Rows[28][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E82.InnerText, F82.InnerText, G82.InnerText);
                }
                row83.Visible = false;
                if (dt.Rows[29][(int)ColumnBF04.Column4].Equals(show))
                {
                    row83.Visible = true;
                    E83.InnerText = dt.Rows[29][(int)ColumnBF04.Column1].ToString();
                    F83.InnerText = dt.Rows[29][(int)ColumnBF04.Column2].ToString();
                    G83.InnerText = dt.Rows[29][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E83.InnerText, F83.InnerText, G83.InnerText);
                }
                F84.InnerText = dt.Rows[30][(int)ColumnBF04.Column2].ToString();
                html += string.Format(
                "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                F84.InnerText);
                row85.Visible = false;
                if (dt.Rows[31][(int)ColumnBF04.Column4].Equals(show))
                {
                    row85.Visible = true;
                    G85.InnerText = dt.Rows[31][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right' colspan='2'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G85.InnerText);
                }
                G86.InnerText = dt.Rows[32][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right' colspan='2'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                G86.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments du notaire sur la valeur résiduelle des constructions
            row88.Visible = false;
            if (dt.Rows[34][(int)ColumnBF04.Column4].Equals(show))
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire sur la valeur résiduelle des constructions</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row88.Visible = true;
                G89.InnerText = dt.Rows[35][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td colspan='2' align='right'>Base :</td><td align='right'>{0}</td><td></td></tr>",
                G89.InnerText);
                row90.Visible = false;
                if (dt.Rows[36][(int)ColumnBF04.Column4].Equals(show))
                {
                    row90.Visible = true;
                    E90.InnerText = dt.Rows[36][(int)ColumnBF04.Column1].ToString();
                    F90.InnerText = dt.Rows[36][(int)ColumnBF04.Column2].ToString();
                    G90.InnerText = dt.Rows[36][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E90.InnerText, F90.InnerText, G90.InnerText);
                }
                row91.Visible = false;
                if (dt.Rows[37][(int)ColumnBF04.Column4].Equals(show))
                {
                    row91.Visible = true;
                    E91.InnerText = dt.Rows[37][(int)ColumnBF04.Column1].ToString();
                    F91.InnerText = dt.Rows[37][(int)ColumnBF04.Column2].ToString();
                    G91.InnerText = dt.Rows[37][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E91.InnerText, F91.InnerText, G91.InnerText);
                }
                row92.Visible = false;
                if (dt.Rows[38][(int)ColumnBF04.Column4].Equals(show))
                {
                    row92.Visible = true;
                    E92.InnerText = dt.Rows[38][(int)ColumnBF04.Column1].ToString();
                    F92.InnerText = dt.Rows[38][(int)ColumnBF04.Column2].ToString();
                    G92.InnerText = dt.Rows[38][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E92.InnerText, F92.InnerText, G92.InnerText);
                }
                row93.Visible = false;
                if (dt.Rows[39][(int)ColumnBF04.Column4].Equals(show))
                {
                    row93.Visible = true;
                    E93.InnerText = dt.Rows[39][(int)ColumnBF04.Column1].ToString();
                    F93.InnerText = dt.Rows[39][(int)ColumnBF04.Column2].ToString();
                    G93.InnerText = dt.Rows[39][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    E93.InnerText, F93.InnerText, G93.InnerText);
                }
                F94.InnerText = dt.Rows[40][(int)ColumnBF04.Column2].ToString();
                html += string.Format(
                "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                F94.InnerText);
                row95.Visible = false;
                if (dt.Rows[41][(int)ColumnBF04.Column4].Equals(show))
                {
                    row95.Visible = true;
                    G95.InnerText = dt.Rows[41][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right' colspan='2'>Minimum de perception HT :</td><td align='right'>{0}</td><td></td></tr>",
                    G95.InnerText);
                }
                G96.InnerText = dt.Rows[42][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right' colspan='2'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                G96.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments du notaire - Récapitulatif
            row98.Visible = false;
            if (dt.Rows[44][(int)ColumnBF04.Column4].Equals(show))
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - Récapitulatif</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row98.Visible = true;
                G100.InnerText = dt.Rows[46][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right' colspan='2'>Emoluments du notaire (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                G100.InnerText);
                row101.Visible = false;
                if (dt.Rows[47][(int)ColumnBF04.Column4].Equals(show))
                {
                    row101.Visible = true;
                    G101.InnerText = dt.Rows[47][(int)ColumnBF04.Column3].ToString();
                    html += string.Format(
                    "<tr><td align='right' colspan='2'>Emoluments de caution (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    G101.InnerText);
                }
                G102.InnerText = dt.Rows[48][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                    "<tr><td align='right' colspan='2'>Total des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    G102.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G105.InnerText = dt.Rows[51][(int)ColumnBF04.Column3].ToString();
            G106.InnerText = dt.Rows[52][(int)ColumnBF04.Column3].ToString();
            G107.InnerText = dt.Rows[53][(int)ColumnBF04.Column3].ToString();
            G108.InnerText = dt.Rows[54][(int)ColumnBF04.Column3].ToString();
            G109.InnerText = dt.Rows[55][(int)ColumnBF04.Column3].ToString();
            html += string.Format(
                "<tr><td align='right' colspan='2'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                G105.InnerText);
            html += string.Format(
                "<tr><td align='right' colspan='2'>Emoluments de formalités :</td><td align='right'>{0}</td><td></td></tr>",
                G106.InnerText);
            html += string.Format(
                "<tr><td align='right' colspan='2'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                G107.InnerText);
            html += string.Format(
                "<tr><td align='right' colspan='2'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                G108.InnerText);
            html += string.Format(
                "<tr><td align='right' colspan='2'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                G109.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            G113.InnerText = dt.Rows[59][(int)ColumnBF04.Column3].ToString();
            html += string.Format(
                "<tr><td align='right' colspan='2'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                G113.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            //html += "<tr><td colspan='4' align='left'>Taxe de Publicité foncière :</td></tr>";
            //html += "<tr><td colspan='4' align='center'>Exonération.</td></tr>";
            html += "<tr><td colspan='4' align='left'>Contribution pour la sécurité immobilière (art. 879 du CGI) :</td></tr>";
            row118.Visible = false;
            if (dt.Rows[64][(int)ColumnBF04.Column4].Equals(show))
            {
                row118.Visible = true;
                E118.InnerText = dt.Rows[64][(int)ColumnBF04.Column1].ToString();
                F118.InnerText = dt.Rows[64][(int)ColumnBF04.Column2].ToString();
                G118.InnerText = dt.Rows[64][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                E118.InnerText, F118.InnerText, G118.InnerText);
            }
            row119.Visible = false;
            if (dt.Rows[65][(int)ColumnBF04.Column4].Equals(show))
            {
                row119.Visible = true;
                G119.InnerText = dt.Rows[65][(int)ColumnBF04.Column3].ToString();
                html += string.Format(
                "<tr><td align='right' colspan='2'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                G119.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF04-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = new DataTable();
                switch (ddl1.SelectedValue)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "7":
                        data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 5, 1, 44, 7, out _status);
                        divTaxation.Visible = true;
                        divTaxationC.Visible = false;
                        break;
                    case "5":
                    case "6":
                        data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 53, 1, 73, 7, out _status);
                        divTaxationC.Visible = true;
                        divTaxation.Visible = false;
                        break;
                }
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                switch (ddl1.SelectedValue)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "7":
                        SetValuesTaxation(data);
                        divTaxation.Visible = true;
                        divTaxationC.Visible = false;
                        break;
                    case "5":
                    case "6":
                        SetValuesTaxationC(data);
                        divTaxationC.Visible = true;
                        divTaxation.Visible = false;
                        break;
                }
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF04");
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
                           "','chk1':'" + chk1.Checked +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','txtZone05':'" + txtZone05.Text +
                           "','txtZone06':'" + txtZone06.Text +
                           "','ddl5':'" + ddl5.SelectedValue +
                           "','txtZone07':'" + txtZone07.Text +
                           "','chk2':'" + chk2.Checked +
                           "','chk3':'" + chk3.Checked +
                           "','txtZone08':'" + txtZone08.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF04", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chk1':'" + chk1.Checked +
                           "','ddl2':'" + ddl2.SelectedValue +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','txtZone05':'" + txtZone05.Text +
                           "','txtZone06':'" + txtZone06.Text +
                           "','ddl5':'" + ddl5.SelectedValue +
                           "','txtZone07':'" + txtZone07.Text +
                           "','chk2':'" + chk2.Checked +
                           "','chk3':'" + chk3.Checked +
                           "','txtZone08':'" + txtZone08.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF04", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF04", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF04", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

     enum ColumnBF04
     {
        Column1 = 3,
        Column2 = 4,
        Column3 = 5,
        Column4 = 6
     }

    class DataModelBF04
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string ddl1 { get; set; }
        public string ddl2 { get; set; }
        public string ddl3 { get; set; }
        public string ddl4 { get; set; }
        public string ddl5 { get; set; }
        public string chk1 { get; set; }
        public string chk2 { get; set; }
        public string chk3 { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtZone05 { get; set; }
        public string txtZone06 { get; set; }
        public string txtZone07 { get; set; }
        public string txtZone08 { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}