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
    public partial class SUCC01 : Page
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
                chk2.Checked = json["chk2"].TransformToBoolean();
                chk3.Checked = json["chk3"].TransformToBoolean();
                chk4.Checked = json["chk4"].TransformToBoolean();
                ddl1.SelectedValue = json["ddl1"].ToString();
                ddlArticle1.SelectedValue = json["ddlArticle1"].ToString();
                ddlArticle2.SelectedValue = json["ddlArticle2"].ToString();
                ddlArticle3.SelectedValue = json["ddlArticle3"].ToString();
                ddlArticle4.SelectedValue = json["ddlArticle4"].ToString();
                ddlArticle5.SelectedValue = json["ddlArticle5"].ToString();
                ddlArticle6.SelectedValue = json["ddlArticle6"].ToString();
                ddlArticle7.SelectedValue = json["ddlArticle7"].ToString();
                ddlArticle8.SelectedValue = json["ddlArticle8"].ToString();
                ddlArticle9.SelectedValue = json["ddlArticle9"].ToString();
                ddlArticle10.SelectedValue = json["ddlArticle10"].ToString();
                value1.Text = json["value1"].ToString();
                value2.Text = json["value2"].ToString();
                value3.Text = json["value3"].ToString();
                value4.Text = json["value4"].ToString();
                value5.Text = json["value5"].ToString();
                value6.Text = json["value6"].ToString();
                value7.Text = json["value7"].ToString();
                value8.Text = json["value8"].ToString();
                value9.Text = json["value9"].ToString();
                value10.Text = json["value10"].ToString();
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
                Height = 6,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtZone01.Text},
                        new object[] {chk1.Checked ? "VRAI" : "FAUX"},
                        new object[] {chk2.Checked ? "VRAI" : "FAUX"},
                        new object[] {chk3.Checked ? "VRAI" : "FAUX"},
                        new object[] {chk4.Checked ? "VRAI" : "FAUX"},
                        new object[] {ddl1.SelectedValue}
                });
            range = new RangeCoordinates
            {
                Row = 14,
                Column = 3,
                Height = 10,
                Width = 2
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {ddlArticle1.SelectedValue, value1.Text},
                        new object[] {ddlArticle2.SelectedValue, value2.Text},
                        new object[] {ddlArticle3.SelectedValue, value3.Text},
                        new object[] {ddlArticle4.SelectedValue, value4.Text},
                        new object[] {ddlArticle5.SelectedValue, value5.Text},
                        new object[] {ddlArticle6.SelectedValue, value6.Text},
                        new object[] {ddlArticle7.SelectedValue, value7.Text},
                        new object[] {ddlArticle8.SelectedValue, value8.Text},
                        new object[] {ddlArticle9.SelectedValue, value9.Text},
                        new object[] {ddlArticle10.SelectedValue, value10.Text}
                });
            range = new RangeCoordinates
            {
                Row = 27,
                Column = 4,
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
            F102.InnerText = dt.Rows[2][(int)ColumnSUCC01.Col1].ToString();
            F103.InnerText = dt.Rows[3][(int)ColumnSUCC01.Col1].ToString();
            F104.InnerText = dt.Rows[4][(int)ColumnSUCC01.Col1].ToString();
            F106.InnerText = dt.Rows[6][(int)ColumnSUCC01.Col1].ToString();
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
                Request.PhysicalApplicationPath + "tmp\\SUCC01\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments sur l'attestation immobilière - C.com. Art. A 444-59
            row111.Visible = dt.Rows[11][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[11][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments sur l`attestation immobilière - C.com. Art. A 444-59</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row113.Visible = dt.Rows[13][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[13][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F113.InnerText = dt.Rows[13][(int)ColumnSUCC01.Col1].ToString();
                    G113.InnerText = dt.Rows[13][(int)ColumnSUCC01.Col2].ToString();
                    H113.InnerText = dt.Rows[13][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F113.InnerText, G113.InnerText, H113.InnerText);
                }
                row114.Visible = dt.Rows[14][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[14][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F114.InnerText = dt.Rows[14][(int)ColumnSUCC01.Col1].ToString();
                    G114.InnerText = dt.Rows[14][(int)ColumnSUCC01.Col2].ToString();
                    H114.InnerText = dt.Rows[14][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F114.InnerText, G114.InnerText, H114.InnerText);
                }
                row115.Visible = dt.Rows[15][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[15][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F115.InnerText = dt.Rows[15][(int)ColumnSUCC01.Col1].ToString();
                    G115.InnerText = dt.Rows[15][(int)ColumnSUCC01.Col2].ToString();
                    H115.InnerText = dt.Rows[15][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F115.InnerText, G115.InnerText, H115.InnerText);
                }
                row116.Visible = dt.Rows[16][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[16][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F116.InnerText = dt.Rows[16][(int)ColumnSUCC01.Col1].ToString();
                    G116.InnerText = dt.Rows[16][(int)ColumnSUCC01.Col2].ToString();
                    H116.InnerText = dt.Rows[16][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F116.InnerText, G116.InnerText, H116.InnerText);
                }
                row118.Visible = dt.Rows[18][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[18][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    G118.InnerText = dt.Rows[18][(int)ColumnSUCC01.Col2].ToString();
                    html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                        G118.InnerText);
                }
                row119.Visible = dt.Rows[19][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[19][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    H119.InnerText = dt.Rows[19][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Total Hors TVA :</td><td align='right'>{0}</td><td></td></tr>",
                        H119.InnerText);
                }
                row121.Visible = dt.Rows[21][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[21][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    H121.InnerText = dt.Rows[21][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                        H121.InnerText);
                }
                row123.Visible = dt.Rows[23][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[23][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='center'>Dispense de l'attestation immobilière.</td></tr>";
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments sur la déclaration de succession - C.com. Art. A 444-63
            row128.Visible = dt.Rows[28][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[28][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments sur la déclaration de succession - C.com. Art. A 444-63</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row130.Visible = dt.Rows[30][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[30][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F130.InnerText = dt.Rows[30][(int)ColumnSUCC01.Col1].ToString();
                    G130.InnerText = dt.Rows[30][(int)ColumnSUCC01.Col2].ToString();
                    H130.InnerText = dt.Rows[30][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F130.InnerText, G130.InnerText, H130.InnerText);
                }
                row131.Visible = dt.Rows[31][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[31][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F131.InnerText = dt.Rows[31][(int)ColumnSUCC01.Col1].ToString();
                    G131.InnerText = dt.Rows[31][(int)ColumnSUCC01.Col2].ToString();
                    H131.InnerText = dt.Rows[31][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F131.InnerText, G131.InnerText, H131.InnerText);
                }
                row132.Visible = dt.Rows[32][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[32][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F132.InnerText = dt.Rows[32][(int)ColumnSUCC01.Col1].ToString();
                    G132.InnerText = dt.Rows[32][(int)ColumnSUCC01.Col2].ToString();
                    H132.InnerText = dt.Rows[32][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F132.InnerText, G132.InnerText, H132.InnerText);
                }
                row133.Visible = dt.Rows[33][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[33][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F133.InnerText = dt.Rows[33][(int)ColumnSUCC01.Col1].ToString();
                    G133.InnerText = dt.Rows[33][(int)ColumnSUCC01.Col2].ToString();
                    H133.InnerText = dt.Rows[33][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F133.InnerText, G133.InnerText, H133.InnerText);
                }
                row135.Visible = dt.Rows[35][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[35][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    G135.InnerText = dt.Rows[35][(int)ColumnSUCC01.Col2].ToString();
                    html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                        G135.InnerText);
                }
                row136.Visible = dt.Rows[36][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[36][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    H136.InnerText = dt.Rows[36][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Total Hors TVA :</td><td align='right'>{0}</td><td></td></tr>",
                        H136.InnerText);
                }
                row140.Visible = dt.Rows[40][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[40][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='center'>Dispense de dépôt de la déclaration de succession.</td></tr>";
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Emoluments du notaire pour l'acte de notoriété
            row144.Visible = dt.Rows[44][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[44][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire pour l`acte de notoriété - C.com. Art. A 444-66</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                H146.InnerText = dt.Rows[46][(int)ColumnSUCC01.Col3].ToString();
                //H148.InnerText = dt.Rows[48][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument fixe :</td><td align='right'>{0}</td><td></td></tr>",
                    H146.InnerText);
            }
            //Emoluments du notaire pour l'acte d'inventaire
            row150.Visible = dt.Rows[50][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[50][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire pour l`acte d`inventaire - C.com. Art. A 444-155</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                H152.InnerText = dt.Rows[52][(int)ColumnSUCC01.Col3].ToString();
                //H154.InnerText = dt.Rows[54][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emolument fixe :</td><td align='right'>{0}</td><td></td></tr>",
                    H152.InnerText);
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row158.Visible = dt.Rows[58][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[58][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                H158.InnerText = dt.Rows[58][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments sur l'attestation immobilière :</td><td align='right'>{0}</td><td></td></tr>",
                    H158.InnerText);
            }
            row159.Visible = dt.Rows[59][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[59][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                H159.InnerText = dt.Rows[59][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments sur la déclaration de succession :</td><td align='right'>{0}</td><td></td></tr>",
                    H159.InnerText);
            }
            row160.Visible = dt.Rows[60][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[60][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                H160.InnerText = dt.Rows[60][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments du notaire pour la notoriété :</td><td align='right'>{0}</td><td></td></tr>",
                    H160.InnerText);
            }
            row161.Visible = dt.Rows[61][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[61][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                H161.InnerText = dt.Rows[61][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments du notaire pour l`inventaire :</td><td align='right'>{0}</td><td></td></tr>",
                    H161.InnerText);
            }
            H162.InnerText = dt.Rows[62][(int)ColumnSUCC01.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                H162.InnerText);
            H166.InnerText = dt.Rows[66][(int)ColumnSUCC01.Col3].ToString();
            H167.InnerText = dt.Rows[67][(int)ColumnSUCC01.Col3].ToString();
            H168.InnerText = dt.Rows[68][(int)ColumnSUCC01.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total HT des émoluments :</td><td align='right'>{0}</td><td></td></tr>",
                H166.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                H167.InnerText);
            html += string.Format(
                "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                H168.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H172.InnerText = dt.Rows[72][(int)ColumnSUCC01.Col3].ToString();
            html += string.Format(
                "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                H172.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row178.Visible = dt.Rows[78][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[78][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>Notoriété :</td></tr>";
                H180.InnerText = dt.Rows[80][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Enregistrement :</td><td align='right'>{0}</td><td></td></tr>",
                    H180.InnerText);
                html += "<tr><td colspan='4' align='center'>Acte dispensé de la formalité (CGI art 846 bis)</td></tr>";
                html += "<tr><td colspan='4' align='center'>Paiement du droit sur état</td></tr>";
            }
            row182.Visible = dt.Rows[82][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[82][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>Inventaire :</td></tr>";
                H184.InnerText = dt.Rows[84][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Enregistrement :</td><td align='right'>{0}</td><td></td></tr>",
                    H184.InnerText);
                html += "<tr><td colspan='4' align='center'>Acte dispensé de la formalité (CGI art 635 1)</td></tr>";
                html += "<tr><td colspan='4' align='center'>Paiement du droit sur état</td></tr>";
            }
            row186.Visible = dt.Rows[86][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[86][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                H186.InnerText = dt.Rows[86][(int)ColumnSUCC01.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2'>Taxe fixe d'enregistrement :</td><td align='right'>{0}</td><td></td></tr>",
                    H186.InnerText);
            }
            row188.Visible = dt.Rows[88][(int)ColumnSUCC01.Col4].ToString() == show;
            if (dt.Rows[88][(int)ColumnSUCC01.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4'>CSI (art. 879 du CGI) :</td></tr>";
                row189.Visible = dt.Rows[89][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[89][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    F189.InnerText = dt.Rows[89][(int)ColumnSUCC01.Col1].ToString();
                    G189.InnerText = dt.Rows[89][(int)ColumnSUCC01.Col2].ToString();
                    H189.InnerText = dt.Rows[89][(int)ColumnSUCC01.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F189.InnerText, G189.InnerText, H189.InnerText);
                }
                row190.Visible = dt.Rows[90][(int)ColumnSUCC01.Col4].ToString() == show;
                if (dt.Rows[90][(int)ColumnSUCC01.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Pour la CSI, il a été pris le minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                        "15.00 €");
                }
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "SUCC01-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 99, 2, 92, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "SUCC01");
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
                           "','chk2':'" + chk2.Checked +
                           "','chk3':'" + chk3.Checked +
                           "','chk4':'" + chk4.Checked +
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','ddlArticle1':'" + ddlArticle1.SelectedValue + "','value1':'" + value1.Text +
                           "','ddlArticle2':'" + ddlArticle2.SelectedValue + "','value2':'" + value2.Text +
                           "','ddlArticle3':'" + ddlArticle3.SelectedValue + "','value3':'" + value3.Text +
                           "','ddlArticle4':'" + ddlArticle4.SelectedValue + "','value4':'" + value4.Text +
                           "','ddlArticle5':'" + ddlArticle5.SelectedValue + "','value5':'" + value5.Text +
                           "','ddlArticle6':'" + ddlArticle6.SelectedValue + "','value6':'" + value6.Text +
                           "','ddlArticle7':'" + ddlArticle7.SelectedValue + "','value7':'" + value7.Text +
                           "','ddlArticle8':'" + ddlArticle8.SelectedValue + "','value8':'" + value8.Text +
                           "','ddlArticle9':'" + ddlArticle9.SelectedValue + "','value9':'" + value9.Text +
                           "','ddlArticle10':'" + ddlArticle10.SelectedValue + "','value10':'" + value10.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "SUCC01", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chk2':'" + chk2.Checked +
                           "','chk3':'" + chk3.Checked +
                           "','chk4':'" + chk4.Checked +
                           "','ddl1':'" + ddl1.SelectedValue +
                           "','ddlArticle1':'" + ddlArticle1.SelectedValue + "','value1':'" + value1.Text +
                           "','ddlArticle2':'" + ddlArticle2.SelectedValue + "','value2':'" + value2.Text +
                           "','ddlArticle3':'" + ddlArticle3.SelectedValue + "','value3':'" + value3.Text +
                           "','ddlArticle4':'" + ddlArticle4.SelectedValue + "','value4':'" + value4.Text +
                           "','ddlArticle5':'" + ddlArticle5.SelectedValue + "','value5':'" + value5.Text +
                           "','ddlArticle6':'" + ddlArticle6.SelectedValue + "','value6':'" + value6.Text +
                           "','ddlArticle7':'" + ddlArticle7.SelectedValue + "','value7':'" + value7.Text +
                           "','ddlArticle8':'" + ddlArticle8.SelectedValue + "','value8':'" + value8.Text +
                           "','ddlArticle9':'" + ddlArticle9.SelectedValue + "','value9':'" + value9.Text +
                           "','ddlArticle10':'" + ddlArticle10.SelectedValue + "','value10':'" + value10.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "SUCC01", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("SUCC01", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("SUCC01", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnSUCC01
    {
        Col1 = 3,
        Col2 = 4,
        Col3 = 5,
        Col4 = 6
    }
}