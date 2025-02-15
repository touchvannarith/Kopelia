using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using NotaliaOnline.Properties;
using NotaliaOnline.WebReference;
using System;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace NotaliaOnline
{
    public partial class BF01 : Page
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
                txtLibelle.Text = obj.Libelle;
                var data = new JavaScriptSerializer().Deserialize<DataModelBF01>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                ddl1Choix.SelectedValue = data.ddl1;
                ddl2Situation.SelectedValue = data.ddl2;
                ddl3Agriculture.SelectedValue = data.ddl3;
                ddl4LogementsSociaux.SelectedValue = data.ddl4;
                ddl5Vendeur.SelectedValue = data.ddl5;
                ddl6Acquereur.SelectedValue = data.ddl6;
                ddl7AucuneOption.SelectedValue = data.ddl7;
                ddl8TvasurleprixTotal.SelectedValue = data.ddl8;
                ddl9AucumEngagement.SelectedValue = data.ddl9;
                ddl10AucumEngagement.SelectedValue = data.ddl10;
                txtZone1.Text = data.txtZone1;
                chkBox1.Checked = data.chkBox1.TransformToBoolean();
                txtZone2.Text = data.txtZone2;
                txtZone3.Text = data.txtZone3;
                txtZone4.Text = data.txtZone4;
                chkBox2.Checked = data.chkBox2.TransformToBoolean();
                txtZone5.Text = data.txtZone5;
                chkBox3.Checked = data.chkBox3.TransformToBoolean();
                txtZone6.Text = data.txtZone6;
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
                Row = 10,
                Column = 3,
                Height = 10,
                Width = 1
            };
            var rangeValues = new object[]{
                new object[] {ddl1Choix.SelectedValue},
                new object[] {ddl2Situation.SelectedValue},
                new object[] {ddl3Agriculture.SelectedValue},
                new object[] {ddl4LogementsSociaux.SelectedValue},
                new object[] {ddl5Vendeur.SelectedValue},
                new object[] {ddl6Acquereur.SelectedValue},
                new object[] {ddl7AucuneOption.SelectedValue},
                new object[] {ddl8TvasurleprixTotal.SelectedValue},
                new object[] {ddl9AucumEngagement.SelectedValue},
                new object[] {ddl10AucumEngagement.SelectedValue}
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range, rangeValues);
            range = new RangeCoordinates
            {
                Row = 21,
                Column = 3,
                Height = 9,
                Width = 1
            };
            rangeValues = new object[]
            {
                new object[] {txtZone1.Text},
                new object[] {chkBox1.Checked.TransformToBooleanFr()},
                new object[] {txtZone2.Text},
                new object[] {txtZone3.Text},
                new object[] {txtZone4.Text},
                new object[] {chkBox2.Checked.TransformToBooleanFr()},
                new object[] {txtZone5.Text + "%"},
                new object[] {chkBox3.Checked.TransformToBooleanFr()},
                new object[] {txtZone6.Text}
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range, rangeValues);
            range = new RangeCoordinates
            {
                Row = 73,
                Column = 3,
                Height = 3,
                Width = 1
            };
            rangeValues = new object[]
            {
                new object[] {txtEmolument_de_formalités_HT.Text},
                new object[] {txtDébours.Text},
                new object[] { hdUtilisation_du_futur_tarif.Value}
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range, rangeValues);
        }

        private void SetValues(DataTable dt)
        {
            lblG56.InnerText = dt.Rows[55][(int)ColumnBF01.Column3].ToString();
            lblG57.InnerText = dt.Rows[56][(int)ColumnBF01.Column3].ToString();
            lblG58.InnerText = dt.Rows[57][(int)ColumnBF01.Column3].ToString();
            lblG59.InnerText = dt.Rows[58][(int)ColumnBF01.Column3].ToString();

            //Emoluments proportionnels de la vente
            var row = dt.Rows[2];
            lblX1.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            lblY1.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblZ1.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow1.Visible = dt.Rows[2][ColumnBF01.Column4.ToString()].TransformToBoolean();
            row = dt.Rows[3];
            lblX2.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            lblY2.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblZ2.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow2.Visible = dt.Rows[3][ColumnBF01.Column4.ToString()].TransformToBoolean();
            row = dt.Rows[4];
            lblX3.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            lblY3.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblZ3.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow3.Visible = dt.Rows[4][ColumnBF01.Column4.ToString()].TransformToBoolean();
            row = dt.Rows[5];
            lblX4.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            lblY4.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblZ4.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow4.Visible = dt.Rows[5][ColumnBF01.Column4.ToString()].TransformToBoolean();
            row = dt.Rows[6];
            lblTotal.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            divRow5.Visible = dt.Rows[6][ColumnBF01.Column4.ToString()].TransformToBoolean();
            row = dt.Rows[7];
            lblEmolument_min.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow6.Visible = dt.Rows[7][ColumnBF01.Column4.ToString()].TransformToBoolean();
            row = dt.Rows[8];
            G9.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            //Récapitulatif et calcul de la TVA
            row = dt.Rows[11];
            G12.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            row = dt.Rows[12];
            G13.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            row = dt.Rows[13];
            G14.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            row = dt.Rows[14];
            G15.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            row = dt.Rows[15];
            G16.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            //Débours
            row = dt.Rows[18];
            G19.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            //Contribution pour la sécurité immobilière(art. 879 du CGI);***********************
            row = dt.Rows[24];
            lblVente_X.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            lblVente_Y.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblVente_Z.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow18.Visible = dt.Rows[24][ColumnBF01.Column4.ToString()].TransformToBoolean();

            row = dt.Rows[25];
            lblVente_NA.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            divRow19.Visible = dt.Rows[25][ColumnBF01.Column4.ToString()].TransformToBoolean();

            row = dt.Rows[27];
            lblPrivilege_de_vendeur_X1.InnerText = row[ColumnBF01.Column1.ToString()].ToString();
            lblPrivilege_de_vendeur_Y1.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblPrivilege_de_vendeur_Z1.InnerText = row[ColumnBF01.Column3.ToString()].ToString();
            divRow21.Visible = dt.Rows[27][ColumnBF01.Column4.ToString()].TransformToBoolean();

            //Fiscalité immobilière*************************
            //TVA immobilière : 
            row = dt.Rows[30];
            lblPrix_TTC.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            row = dt.Rows[31];
            lblTaux_de_la_TVA.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            row = dt.Rows[32];
            lblMontant_de_la_TVA_sur_le_prix_total.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            row = dt.Rows[33];
            lblMontant_de_la_TVA_sur_marge.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            row = dt.Rows[34];
            lblBaseHT.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            if (dt.Rows[29][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                divRow23.Visible = true;
                divRow26.Visible = dt.Rows[32][ColumnBF01.Column4.ToString()].TransformToBoolean();
                divRow27.Visible = dt.Rows[33][ColumnBF01.Column4.ToString()].TransformToBoolean();
            }
            else
                divRow23.Visible = false;

            //Base de taxation de la TPF :
            row = dt.Rows[39];
            lblBase_taxable.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            //Montant des droits:
            row = dt.Rows[41];
            lblTaxe_departementale_X.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblTaxe_departementale_Y.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            row = dt.Rows[42];
            lblprelevement_de_lEtat_sur_taxe_departementale_X.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblprelevement_de_lEtat_sur_taxe_departementale_Y.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            row = dt.Rows[43];
            lblTaxe_locale_X.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblTaxe_locale_Y.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            row = dt.Rows[44];
            lblDroits_fixes.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            row = dt.Rows[45];
            lblTaxe_additionnelle_X.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            lblTaxe_additionnelle_Y.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            row = dt.Rows[46];
            lblAssiette_de_taxation.InnerText = row[ColumnBF01.Column2.ToString()].ToString();

            row = dt.Rows[47];
            lblTotal_Montant_des_droits.InnerText = row[ColumnBF01.Column3.ToString()].ToString();

            row = dt.Rows[48];
            lblNA.InnerText = row[ColumnBF01.Column2.ToString()].ToString();
            divRow29.Visible = dt.Rows[35][ColumnBF01.Column4.ToString()].TransformToBoolean();
            if (dt.Rows[40][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                divRow34.Visible = true;
                divRow35.Visible = dt.Rows[41][ColumnBF01.Column4.ToString()].TransformToBoolean();
                divRow36.Visible = dt.Rows[42][ColumnBF01.Column4.ToString()].TransformToBoolean();
                divRow37.Visible = dt.Rows[43][ColumnBF01.Column4.ToString()].TransformToBoolean();
                divRow38.Visible = dt.Rows[44][ColumnBF01.Column4.ToString()].TransformToBoolean();
                divRow39.Visible = dt.Rows[45][ColumnBF01.Column4.ToString()].TransformToBoolean();
                lblAssiette_de_taxation.Visible = divRow39.Visible;
                divRow41.Visible = dt.Rows[47][ColumnBF01.Column4.ToString()].TransformToBoolean();
                divRow42.Visible = dt.Rows[48][ColumnBF01.Column4.ToString()].TransformToBoolean();
            }
            else
                divRow34.Visible = false;

            var html = "";
            //TOTAL DES DROITS ET FRAIS
            html +=
                "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" +
                "<html xmlns = 'http://www.w3.org/1999/xhtml'><head><meta http - equiv = 'Content-Type' content = 'text/html; charset=utf-8' /> " +
                "</head><body>";
            html += Convert.ToString(@"<table border-collapse='collapse' border='0' width='100%' size='1'>");
            html += Convert.ToString(@"<tr><td bgcolor=""#304F73"" align=""center"" colspan=""5""><b><font color=""#FFFFFF"" size='11px'>Total des droits et frais</font></b></td></tr>");
            html += Convert.ToString(@"<tr><td colspan=""5"" bgcolor=""#304F73""></td></tr>");
            html += Convert.ToString(@"<tr><td colspan=""5""><br/><br/></td></tr>");
            html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblG58.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblG57.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblG59.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblG56.InnerText);
            html += "</table>";

            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF01\\chart.png");

            //DÉTAIL DES FRAIS
            //
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += Convert.ToString(@"<tr><td colspan=""4""></td></tr>");
            html += Convert.ToString(@"<tr><td bgcolor=""#304F73"" align=""center"" colspan=""4""><b><font color=""#FFFFFF"" size='11px'>Détail des frais</font></b></td></tr>");
            html += Convert.ToString(@"<tr><td colspan=""4"" bgcolor=""#304F73""></td></tr>");

            //Emoluments du notaire
            html = html + Convert.ToString(@"<tr><td bgcolor=""#01ABE4"" align=""center"" colspan=""4""><b><font color=""#FFFFFF"" size='11px'>Emoluments du notaire</font></b></td></tr>");
            html = html + Convert.ToString(@"<tr><td colspan=""4"" bgcolor=""#01ABE4""></td></tr>");
            if (dt.Rows[2][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblX1.InnerText + "     sur</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblY1.InnerText + "     =</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblZ1.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
            }
            if (dt.Rows[3][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblX2.InnerText + "     sur</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblY2.InnerText + "     =</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblZ2.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
            }
            if (dt.Rows[4][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblX3.InnerText + "     sur</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblY3.InnerText + "     =</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblZ3.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
            }
            if (dt.Rows[5][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblX4.InnerText + "     sur</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblY4.InnerText + "     =</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblZ4.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
            }
            if (dt.Rows[6][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"">" + "Total :" + "</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblTotal.InnerText + "</td>");
                html = html + Convert.ToString(@"<td colspan=""2""></td>");
                html = html + Convert.ToString(@"</tr>");
            }
            if (dt.Rows[7][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html +=
                    string.Format(
                        "<tr><td align='right' colspan='2'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                        lblEmolument_min.InnerText);
            }
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">Total HT des émoluments réglementés :</td>");
            html = html + Convert.ToString(@"<td align=""right"">" + G9.InnerText + "</td>");
            html = html + Convert.ToString(@"<td></td>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr><td colspan=""4""></td></tr>");

            //Récapitulatif et calcul de la TVA
            html = html + Convert.ToString(@"<tr><td bgcolor=""#01ABE4"" align=""center"" colspan=""4""><b><font color=""#FFFFFF"" size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>");
            html += Convert.ToString(@"<tr><td colspan=""4"" bgcolor=""#01ABE4""></td></tr>");
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">Total HT des émoluments du notaire :</td>");
            html = html + Convert.ToString(@"<td align=""right"">" + G12.InnerText + "</td>");
            html = html + Convert.ToString(@"<td></td>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">Emoluments de formalités :</td>");
            html = html + Convert.ToString(@"<td align=""right"">" + G13.InnerText + "</td>");
            html = html + Convert.ToString(@"<td></td>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">Total HT :</td>");
            html = html + Convert.ToString(@"<td align=""right"">" + G14.InnerText + "</td>");
            html = html + Convert.ToString(@"<td></td>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">TVA :</td>");
            html = html + Convert.ToString(@"<td align=""right"">" + G15.InnerText + "</td>");
            html = html + Convert.ToString(@"<td></td>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">Total TTC :</td>");
            html = html + Convert.ToString(@"<td align=""right"">" + G16.InnerText + "</td>");
            html = html + Convert.ToString(@"<td></td>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr><td colspan=""4""></td></tr>");

            //Débours
            html = html + Convert.ToString(@"<tr><td bgcolor=""#01ABE4"" align=""center"" colspan=""4""><b><font color=""#FFFFFF"" size='11px'>Débours</font></b></td></tr>");
            html += Convert.ToString(@"<tr><td colspan=""4"" bgcolor=""#01ABE4""></td></tr>");
            html = html + Convert.ToString(@"<tr>");
            html = html + Convert.ToString(@"<td width=""40%"" colspan=""2"" align=""right""><font face=""Calibri"">Débours : </font></td>");
            html = html + Convert.ToString(@"<td width=""100%"" align=""right"">");
            html = html + G19.InnerText;
            html = html + Convert.ToString(@"</td>");
            html = html + Convert.ToString(@"<tr><td colspan=""4""></td></tr>");
            html = html + Convert.ToString(@"</tr>");
            html = html + Convert.ToString(@"<tr><td colspan=""4""></td></tr>");

            //Trésor public
            html = html + Convert.ToString(@"<tr><td bgcolor=""#01ABE4"" align=""center"" colspan=""4""><b><font color=""#FFFFFF"" size='11px'>Trésor public</font></b></td></tr>");
            html = html + Convert.ToString(@"<tr><td colspan=""4"" bgcolor=""#01ABE4""></td></tr>");

            html = html + Convert.ToString(@"<tr><td colspan=""4"">Contribution pour la sécurité immobilière (art. 879 du CGI)</td></tr>");
            if (dt.Rows[24][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblVente_X.InnerText + "     sur</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblVente_Y.InnerText + "     =</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblVente_Z.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
            }
            if (dt.Rows[25][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr><td colspan=""4"" align=""center"">" + lblVente_NA.InnerText + "</td></tr>");
            }
            html = html + Convert.ToString(@"<tr><td colspan=""4"">Taxe de publicité foncière</td></tr>");
            if (dt.Rows[29][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr><td colspan=""4"" align=""center"">TVA immobilière :</td></tr>");
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Prix TTC (charges comprises) :" + "</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblPrix_TTC.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");

                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Taux de la TVA :" + "</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblTaux_de_la_TVA.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");

                if (dt.Rows[32][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Montant de la TVA sur le prix total :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblMontant_de_la_TVA_sur_le_prix_total.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                }
                if (dt.Rows[33][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Montant de la TVA sur marge :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblMontant_de_la_TVA_sur_marge.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                }

                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Base HT :" + "</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblBaseHT.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
                html = html + Convert.ToString(@"<tr><td colspan=""4"" align=""center"">Le vendeur doit être informé de son obligation d'inclure dans ses déclarations CA12 la présente TVA via Internet. Les frais calculés ne sont pas impactés par cette formalité.</td></tr>");
            }

            if (dt.Rows[35][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr><td colspan=""2"" align=""right"">Base de taxation de la TPF :</td><td colspan=""2""></td></tr>");
                html = html + Convert.ToString(@"<tr>");
                html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Base taxable :" + "</td>");
                html = html + Convert.ToString(@"<td align=""right"">" + lblBase_taxable.InnerText + "</td>");
                html = html + Convert.ToString(@"<td></td>");
                html = html + Convert.ToString(@"</tr>");
            }

            if (dt.Rows[40][ColumnBF01.Column4.ToString()].TransformToBoolean())
            {
                html = html + Convert.ToString(@"<tr><td colspan=""2"" align=""right"">Montant des droits :</td><td colspan=""2""></td></tr>");
                if (dt.Rows[41][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"">" + "Taxe départementale :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTaxe_departementale_X.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTaxe_departementale_Y.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                }
                if (dt.Rows[42][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"">" + "Prélèvement de l'Etat sur taxe départementale :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblprelevement_de_lEtat_sur_taxe_departementale_X.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblprelevement_de_lEtat_sur_taxe_departementale_Y.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                }
                if (dt.Rows[43][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"">" + "Taxe locale :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTaxe_locale_X.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTaxe_locale_Y.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                }
                if (dt.Rows[44][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Droits fixes (Art.691 bis CGI) :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblDroits_fixes.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                }
                if (dt.Rows[45][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"">Taxe additionnelle (Art.1599 sexies CGI) :</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTaxe_additionnelle_X.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTaxe_additionnelle_Y.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td></td>");
                    html = html + Convert.ToString(@"</tr>");
                    html = html + Convert.ToString(@"<tr><td colspan=""4"" align=""center"">Assiette de taxation : 0 Euros.</td></tr>");
                }
                if (dt.Rows[47][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr>");
                    html = html + Convert.ToString(@"<td align=""right"" colspan=""2"">" + "Total :" + "</td>");
                    html = html + Convert.ToString(@"<td align=""right"">" + lblTotal_Montant_des_droits.InnerText + "</td>");
                    html = html + Convert.ToString(@"<td ></td>");
                    html = html + Convert.ToString(@"</tr>");
                }
                if (dt.Rows[48][ColumnBF01.Column4.ToString()].TransformToBoolean())
                {
                    html = html + Convert.ToString(@"<tr><td colspan=""4"" align=""center"">" + lblNA.InnerText + "</td></tr>");
                }
            }
            html += Convert.ToString("</table></body></html>");
            hdResult.Value = html;
        }

        protected void btnSynthese_Click(object sender, EventArgs e)
        {
            try
            {
                //txtLibelle.Text = string.IsNullOrEmpty(txtDossier.Text) ? txtLibelle.Text : txtDossier.Text;



                var watch = System.Diagnostics.Stopwatch.StartNew();
                _excelService = ExcelServiceHelper.ExcelServiceProvider();
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF01-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 0, 4, 59, 4, out _status);
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
                           "','ddl1':'" + ddl1Choix.SelectedValue + "','ddl2':'" + ddl2Situation.SelectedValue +
                           "','ddl3':'" + ddl3Agriculture.SelectedValue + "','ddl4':'" + ddl4LogementsSociaux.SelectedValue +
                           "','ddl5':'" + ddl5Vendeur.SelectedValue + "','ddl6':'" + ddl6Acquereur.SelectedValue +
                           "','ddl7':'" + ddl7AucuneOption.SelectedValue + "','ddl8':'" + ddl8TvasurleprixTotal.SelectedValue +
                           "','ddl9':'" + ddl9AucumEngagement.SelectedValue +
                           "','ddl10':'" + ddl10AucumEngagement.SelectedValue + "','txtZone1':'" + txtZone1.Text +
                           "','chkBox1':'" + chkBox1.Checked + "','txtZone2':'" + txtZone2.Text +
                           "','txtZone3':'" + txtZone3.Text + "','txtZone4':'" + txtZone4.Text +
                           "','chkBox2':'" + chkBox2.Checked + "','txtZone5':'" + txtZone5.Text +
                           "','chkBox3':'" + chkBox3.Checked + "','txtZone6':'" + txtZone6.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text + "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value +
                           "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF01", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','ddl1':'" + ddl1Choix.SelectedValue + "','ddl2':'" + ddl2Situation.SelectedValue +
                           "','ddl3':'" + ddl3Agriculture.SelectedValue + "','ddl4':'" + ddl4LogementsSociaux.SelectedValue +
                           "','ddl5':'" + ddl5Vendeur.SelectedValue + "','ddl6':'" + ddl6Acquereur.SelectedValue +
                           "','ddl7':'" + ddl7AucuneOption.SelectedValue + "','ddl8':'" + ddl8TvasurleprixTotal.SelectedValue +
                           "','ddl9':'" + ddl9AucumEngagement.SelectedValue +
                           "','ddl10':'" + ddl10AucumEngagement.SelectedValue + "','txtZone1':'" + txtZone1.Text +
                           "','chkBox1':'" + chkBox1.Checked + "','txtZone2':'" + txtZone2.Text +
                           "','txtZone3':'" + txtZone3.Text + "','txtZone4':'" + txtZone4.Text +
                           "','chkBox2':'" + chkBox2.Checked + "','txtZone5':'" + txtZone5.Text +
                           "','chkBox3':'" + chkBox3.Checked + "','txtZone6':'" + txtZone6.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text + "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value +
                           "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF01", false, Session["CLIENT_ID"].TransformToInt());
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var filename = PdfHelper.GeneratePdf("BF01", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
            //Page.ClientScript.RegisterStartupScript(GetType(), "scrollToElement", "scrollToElement('#btnPrint',15);", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.AddHeader("Content-Length", pdf.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(pdf);
            Response.End();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                PdfHelper.GeneratePdf("BF01", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
                EmailService.SendSimulationPdf(txtEmail.Value, pdf);
                Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Votre simulation a bien été envoyée', 'Notification', {timeOut: 5000});", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Échec de l`envoi', 'Notification', {timeOut: 5000});", true);
            }
        }
    }

    enum ColumnBF01
    {
        Column1 = 0,
        Column2 = 1,
        Column3 = 2,
        Column4 = 3
    }

    class DataModelBF01
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string ddl1 { get; set; }
        public string ddl2 { get; set; }
        public string ddl3 { get; set; }
        public string ddl4 { get; set; }
        public string ddl5 { get; set; }
        public string ddl6 { get; set; }
        public string ddl7 { get; set; }
        public string ddl8 { get; set; }
        public string ddl9 { get; set; }
        public string ddl10 { get; set; }
        public string txtZone1 { get; set; }
        public string chkBox1 { get; set; }
        public string txtZone2 { get; set; }
        public string txtZone3 { get; set; }
        public string txtZone4 { get; set; }
        public string chkBox2 { get; set; }
        public string txtZone5 { get; set; }
        public string chkBox3 { get; set; }
        public string txtZone6 { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}