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
    public partial class BF17 : Page
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
                ddl2.SelectedValue = json["ddl2"].ToString();
                chk1.Checked = json["chk1"].TransformToBoolean();
                ddl3.SelectedValue = json["ddl3"].ToString();
                txtZone01.Text = json["txtZone01"].ToString();
                chk2.Checked = json["chk2"].TransformToBoolean();
                ddl4.SelectedValue = json["ddl4"].ToString();
                ddl5.SelectedValue = json["ddl5"].ToString();
                txtImmobilier_Article_1_Percent_1.Text = json["txtImmobilier_Article_1_Percent_1"].ToString();
                txtImmobilier_Article_1_Percent_2.Text = json["txtImmobilier_Article_1_Percent_2"].ToString();
                txtImmobilier_Article_1_Valeur.Text = json["txtImmobilier_Article_1_Valeur"].ToString();
                txtImmobilier_Article_2_Percent_1.Text = json["txtImmobilier_Article_2_Percent_1"].ToString();
                txtImmobilier_Article_2_Percent_2.Text = json["txtImmobilier_Article_2_Percent_2"].ToString();
                txtImmobilier_Article_2_Valeur.Text = json["txtImmobilier_Article_2_Valeur"].ToString();
                txtImmobilier_Article_3_Percent_1.Text = json["txtImmobilier_Article_3_Percent_1"].ToString();
                txtImmobilier_Article_3_Percent_2.Text = json["txtImmobilier_Article_3_Percent_2"].ToString();
                txtImmobilier_Article_3_Valeur.Text = json["txtImmobilier_Article_3_Valeur"].ToString();
                txtImmobilier_Article_4_Percent_1.Text = json["txtImmobilier_Article_4_Percent_1"].ToString();
                txtImmobilier_Article_4_Percent_2.Text = json["txtImmobilier_Article_4_Percent_2"].ToString();
                txtImmobilier_Article_4_Valeur.Text = json["txtImmobilier_Article_4_Valeur"].ToString();
                txtImmobilier_Article_5_Percent_1.Text = json["txtImmobilier_Article_5_Percent_1"].ToString();
                txtImmobilier_Article_5_Percent_2.Text = json["txtImmobilier_Article_5_Percent_2"].ToString();
                txtImmobilier_Article_5_Valeur.Text = json["txtImmobilier_Article_5_Valeur"].ToString();
                txtImmobilier_Article_6_Percent_1.Text = json["txtImmobilier_Article_6_Percent_1"].ToString();
                txtImmobilier_Article_6_Percent_2.Text = json["txtImmobilier_Article_6_Percent_2"].ToString();
                txtImmobilier_Article_6_Valeur.Text = json["txtImmobilier_Article_6_Valeur"].ToString();
                txtImmobilier_Article_7_Percent_1.Text = json["txtImmobilier_Article_7_Percent_1"].ToString();
                txtImmobilier_Article_7_Percent_2.Text = json["txtImmobilier_Article_7_Percent_2"].ToString();
                txtImmobilier_Article_7_Valeur.Text = json["txtImmobilier_Article_7_Valeur"].ToString();
                txtImmobilier_Article_8_Percent_1.Text = json["txtImmobilier_Article_8_Percent_1"].ToString();
                txtImmobilier_Article_8_Percent_2.Text = json["txtImmobilier_Article_8_Percent_2"].ToString();
                txtImmobilier_Article_8_Valeur.Text = json["txtImmobilier_Article_8_Valeur"].ToString();
                txtImmobilier_Article_9_Percent_1.Text = json["txtImmobilier_Article_9_Percent_1"].ToString();
                txtImmobilier_Article_9_Percent_2.Text = json["txtImmobilier_Article_9_Percent_2"].ToString();
                txtImmobilier_Article_9_Valeur.Text = json["txtImmobilier_Article_9_Valeur"].ToString();
                txtImmobilier_Article_10_Percent_1.Text = json["txtImmobilier_Article_10_Percent_1"].ToString();
                txtImmobilier_Article_10_Percent_2.Text = json["txtImmobilier_Article_10_Percent_2"].ToString();
                txtImmobilier_Article_10_Valeur.Text = json["txtImmobilier_Article_10_Valeur"].ToString();
                txtMobilier_Article_1_Percent_1.Text = json["txtMobilier_Article_1_Percent_1"].ToString();
                txtMobilier_Article_1_Percent_2.Text = json["txtMobilier_Article_1_Percent_2"].ToString();
                txtMobilier_Article_1_Valeur.Text = json["txtMobilier_Article_1_Valeur"].ToString();
                txtMobilier_Article_2_Percent_1.Text = json["txtMobilier_Article_2_Percent_1"].ToString();
                txtMobilier_Article_2_Percent_2.Text = json["txtMobilier_Article_2_Percent_2"].ToString();
                txtMobilier_Article_2_Valeur.Text = json["txtMobilier_Article_2_Valeur"].ToString();
                txtMobilier_Article_3_Percent_1.Text = json["txtMobilier_Article_3_Percent_1"].ToString();
                txtMobilier_Article_3_Percent_2.Text = json["txtMobilier_Article_3_Percent_2"].ToString();
                txtMobilier_Article_3_Valeur.Text = json["txtMobilier_Article_3_Valeur"].ToString();
                txtMobilier_Article_4_Percent_1.Text = json["txtMobilier_Article_4_Percent_1"].ToString();
                txtMobilier_Article_4_Percent_2.Text = json["txtMobilier_Article_4_Percent_2"].ToString();
                txtMobilier_Article_4_Valeur.Text = json["txtMobilier_Article_4_Valeur"].ToString();
                txtMobilier_Article_5_Percent_1.Text = json["txtMobilier_Article_5_Percent_1"].ToString();
                txtMobilier_Article_5_Percent_2.Text = json["txtMobilier_Article_5_Percent_2"].ToString();
                txtMobilier_Article_5_Valeur.Text = json["txtMobilier_Article_5_Valeur"].ToString();
                txtMobilier_Article_6_Percent_1.Text = json["txtMobilier_Article_6_Percent_1"].ToString();
                txtMobilier_Article_6_Percent_2.Text = json["txtMobilier_Article_6_Percent_2"].ToString();
                txtMobilier_Article_6_Valeur.Text = json["txtMobilier_Article_6_Valeur"].ToString();
                txtMobilier_Article_7_Percent_1.Text = json["txtMobilier_Article_7_Percent_1"].ToString();
                txtMobilier_Article_7_Percent_2.Text = json["txtMobilier_Article_7_Percent_2"].ToString();
                txtMobilier_Article_7_Valeur.Text = json["txtMobilier_Article_7_Valeur"].ToString();
                txtMobilier_Article_8_Percent_1.Text = json["txtMobilier_Article_8_Percent_1"].ToString();
                txtMobilier_Article_8_Percent_2.Text = json["txtMobilier_Article_8_Percent_2"].ToString();
                txtMobilier_Article_8_Valeur.Text = json["txtMobilier_Article_8_Valeur"].ToString();
                txtMobilier_Article_9_Percent_1.Text = json["txtMobilier_Article_9_Percent_1"].ToString();
                txtMobilier_Article_9_Percent_2.Text = json["txtMobilier_Article_9_Percent_2"].ToString();
                txtMobilier_Article_9_Valeur.Text = json["txtMobilier_Article_9_Valeur"].ToString();
                txtMobilier_Article_10_Percent_1.Text = json["txtMobilier_Article_10_Percent_1"].ToString();
                txtMobilier_Article_10_Percent_2.Text = json["txtMobilier_Article_10_Percent_2"].ToString();
                txtMobilier_Article_10_Valeur.Text = json["txtMobilier_Article_10_Valeur"].ToString();
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
                Height = 8,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {ddl1.SelectedValue},
                        new object[] {ddl2.SelectedValue},
                        new object[] {chk1.Checked.TransformToBooleanFr()},
                        new object[] {ddl3.SelectedValue},
                        new object[] {txtZone01.Text},
                        new object[] {chk2.Checked.TransformToBooleanFr()},
                        new object[] {ddl4.SelectedValue},
                        new object[] {ddl5.SelectedValue}
                });

            range = new RangeCoordinates()
            {
                Row = 16,
                Column = 3,
                Height = 10,
                Width = 3
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtImmobilier_Article_1_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_1_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_1_Valeur.Text},
                        new object[] {txtImmobilier_Article_2_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_2_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_2_Valeur.Text},
                        new object[] {txtImmobilier_Article_3_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_3_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_3_Valeur.Text},
                        new object[] {txtImmobilier_Article_4_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_4_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_4_Valeur.Text},
                        new object[] {txtImmobilier_Article_5_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_5_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_5_Valeur.Text},
                        new object[] {txtImmobilier_Article_6_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_6_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_6_Valeur.Text},
                        new object[] {txtImmobilier_Article_7_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_7_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_7_Valeur.Text},
                        new object[] {txtImmobilier_Article_8_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_8_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_8_Valeur.Text},
                        new object[] {txtImmobilier_Article_9_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_9_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_9_Valeur.Text},
                        new object[] {txtImmobilier_Article_10_Percent_1.Text.TransformToPercentage(), txtImmobilier_Article_10_Percent_2.Text.TransformToPercentage(), txtImmobilier_Article_10_Valeur.Text}
                });

            range = new RangeCoordinates()
            {
                Row = 29,
                Column = 3,
                Height = 10,
                Width = 3
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {txtMobilier_Article_1_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_1_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_1_Valeur.Text},
                        new object[] {txtMobilier_Article_2_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_2_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_2_Valeur.Text},
                        new object[] {txtMobilier_Article_3_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_3_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_3_Valeur.Text},
                        new object[] {txtMobilier_Article_4_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_4_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_4_Valeur.Text},
                        new object[] {txtMobilier_Article_5_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_5_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_5_Valeur.Text},
                        new object[] {txtMobilier_Article_6_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_6_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_6_Valeur.Text},
                        new object[] {txtMobilier_Article_7_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_7_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_7_Valeur.Text},
                        new object[] {txtMobilier_Article_8_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_8_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_8_Valeur.Text},
                        new object[] {txtMobilier_Article_9_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_9_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_9_Valeur.Text},
                        new object[] {txtMobilier_Article_10_Percent_1.Text.TransformToPercentage(), txtMobilier_Article_10_Percent_2.Text.TransformToPercentage(), txtMobilier_Article_10_Valeur.Text}
                });

            range = new RangeCoordinates()
            {
                Row = 41,
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
            //TOTAL DES DROITS ET FRAIS
            lblTrésor.InnerText = dt.Rows[0][(int)ColumnBF17.Col1].ToString();
            lblDébours.InnerText = dt.Rows[1][(int)ColumnBF17.Col1].ToString();
            lblEmoluments_HT_du_notaire.InnerText = dt.Rows[2][(int)ColumnBF17.Col1].ToString();
            lblMontant_des_frais.InnerText = dt.Rows[4][(int)ColumnBF17.Col1].ToString();
            //Attestation notariée
            subAttestation_notariée.Visible = dt.Rows[6][(int)ColumnBF17.Col4].ToString() == show;
            lblTrésor2.InnerText = dt.Rows[7][(int)ColumnBF17.Col1].ToString();
            lblEmoluments_HT_du_notaire2.InnerText = dt.Rows[8][(int)ColumnBF17.Col1].ToString();
            lblMontant_des_frais2.InnerText = dt.Rows[9][(int)ColumnBF17.Col1].ToString();
            //Emoluments du notaire
            lblRow11.InnerText = dt.Rows[18][(int)ColumnBF17.Col1].ToString();
            lblRow12.InnerText = dt.Rows[18][(int)ColumnBF17.Col2].ToString();
            lblRow13.InnerText = dt.Rows[18][(int)ColumnBF17.Col3].ToString();
            lblRow21.InnerText = dt.Rows[19][(int)ColumnBF17.Col1].ToString();
            lblRow22.InnerText = dt.Rows[19][(int)ColumnBF17.Col2].ToString();
            lblRow23.InnerText = dt.Rows[19][(int)ColumnBF17.Col3].ToString();
            lblRow31.InnerText = dt.Rows[20][(int)ColumnBF17.Col1].ToString();
            lblRow32.InnerText = dt.Rows[20][(int)ColumnBF17.Col2].ToString();
            lblRow33.InnerText = dt.Rows[20][(int)ColumnBF17.Col3].ToString();
            lblRow41.InnerText = dt.Rows[21][(int)ColumnBF17.Col1].ToString();
            lblRow42.InnerText = dt.Rows[21][(int)ColumnBF17.Col2].ToString();
            lblRow43.InnerText = dt.Rows[21][(int)ColumnBF17.Col3].ToString();
            lblTotal.InnerText = dt.Rows[23][(int)ColumnBF17.Col2].ToString();
            H129.InnerText = dt.Rows[27][(int)ColumnBF17.Col3].ToString();
            divWarning1.Visible = dt.Rows[25][(int)ColumnBF17.Col4].ToString() == show;
            div1.Visible = dt.Rows[18][(int)ColumnBF17.Col4].ToString() == show;
            div2.Visible = dt.Rows[19][(int)ColumnBF17.Col4].ToString() == show;
            div3.Visible = dt.Rows[20][(int)ColumnBF17.Col4].ToString() == show;
            div4.Visible = dt.Rows[21][(int)ColumnBF17.Col4].ToString() == show;
            divTotal1.Visible = dt.Rows[23][(int)ColumnBF17.Col4].ToString() == show;
            //Emoluments du Notaire (mutation de biens - disposition indépendante)
            lblRow51.InnerText = dt.Rows[32][(int)ColumnBF17.Col1].ToString();
            lblRow52.InnerText = dt.Rows[32][(int)ColumnBF17.Col2].ToString();
            lblRow53.InnerText = dt.Rows[32][(int)ColumnBF17.Col3].ToString();
            lblRow61.InnerText = dt.Rows[33][(int)ColumnBF17.Col1].ToString();
            lblRow62.InnerText = dt.Rows[33][(int)ColumnBF17.Col2].ToString();
            lblRow63.InnerText = dt.Rows[33][(int)ColumnBF17.Col3].ToString();
            lblRow71.InnerText = dt.Rows[34][(int)ColumnBF17.Col1].ToString();
            lblRow72.InnerText = dt.Rows[34][(int)ColumnBF17.Col2].ToString();
            lblRow73.InnerText = dt.Rows[34][(int)ColumnBF17.Col3].ToString();
            lblRow81.InnerText = dt.Rows[35][(int)ColumnBF17.Col1].ToString();
            lblRow82.InnerText = dt.Rows[35][(int)ColumnBF17.Col2].ToString();
            lblRow83.InnerText = dt.Rows[35][(int)ColumnBF17.Col3].ToString();
            lblTotal2.InnerText = dt.Rows[37][(int)ColumnBF17.Col2].ToString();
            H143.InnerText = dt.Rows[41][(int)ColumnBF17.Col3].ToString();
            divWarning2.Visible = dt.Rows[39][(int)ColumnBF17.Col4].ToString() == show;
            div5.Visible = dt.Rows[32][(int)ColumnBF17.Col4].ToString() == show;
            div6.Visible = dt.Rows[33][(int)ColumnBF17.Col4].ToString() == show;
            div7.Visible = dt.Rows[34][(int)ColumnBF17.Col4].ToString() == show;
            div8.Visible = dt.Rows[35][(int)ColumnBF17.Col4].ToString() == show;
            divTotal2.Visible = dt.Rows[37][(int)ColumnBF17.Col4].ToString() == show;
            divSub_Emoluments_du_notaire.Visible = dt.Rows[31][(int)ColumnBF17.Col4].ToString() == show;
            //Fiscalité
            lblRow91.InnerText = dt.Rows[62][(int)ColumnBF17.Col1].ToString();
            lblRow92.InnerText = dt.Rows[62][(int)ColumnBF17.Col2].ToString();
            lblRow93.InnerText = dt.Rows[62][(int)ColumnBF17.Col3].ToString();
            lblRow101.InnerText = dt.Rows[66][(int)ColumnBF17.Col1].ToString();
            lblRow102.InnerText = dt.Rows[66][(int)ColumnBF17.Col2].ToString();
            lblRow103.InnerText = dt.Rows[66][(int)ColumnBF17.Col3].ToString();
            divWarning3.Visible = dt.Rows[63][(int)ColumnBF17.Col4].ToString() == show;
            divWarning4.Visible = dt.Rows[67][(int)ColumnBF17.Col4].ToString() == show;
            divSub_Fiscalité.Visible = dt.Rows[57][(int)ColumnBF17.Col4].ToString() == show;
            div9.Visible = dt.Rows[61][(int)ColumnBF17.Col4].ToString() == show;
            div10.Visible = dt.Rows[65][(int)ColumnBF17.Col4].ToString() == show;
            //DEPOT DES PIECES DU JUGEMENT D'HOMOLOGATION
            divMain_DEPOT_DES_PIECES__.Visible = dt.Rows[69][(int)ColumnBF17.Col4].ToString() == show;
            lblEmoluments_fixes.InnerText = dt.Rows[70][(int)ColumnBF17.Col3].ToString();
            lblDroit_fixe.InnerText = dt.Rows[71][(int)ColumnBF17.Col3].ToString();
            lblTotal3.InnerText = dt.Rows[72][(int)ColumnBF17.Col3].ToString();
            //ATTESTATION NOTARIEE ULTERIEURE
            divMain_ATTESTATION_NOTARIEE_ULTERIEURE.Visible = dt.Rows[75][(int)ColumnBF17.Col4].ToString() == show;
            //Emoluments du Notaire
            lblRow111.InnerText = dt.Rows[77][(int)ColumnBF17.Col1].ToString();
            lblRow112.InnerText = dt.Rows[77][(int)ColumnBF17.Col2].ToString();
            lblRow113.InnerText = dt.Rows[77][(int)ColumnBF17.Col3].ToString();
            lblRow121.InnerText = dt.Rows[78][(int)ColumnBF17.Col1].ToString();
            lblRow122.InnerText = dt.Rows[78][(int)ColumnBF17.Col2].ToString();
            lblRow123.InnerText = dt.Rows[78][(int)ColumnBF17.Col3].ToString();
            lblRow131.InnerText = dt.Rows[79][(int)ColumnBF17.Col1].ToString();
            lblRow132.InnerText = dt.Rows[79][(int)ColumnBF17.Col2].ToString();
            lblRow133.InnerText = dt.Rows[79][(int)ColumnBF17.Col3].ToString();
            lblRow141.InnerText = dt.Rows[80][(int)ColumnBF17.Col1].ToString();
            lblRow142.InnerText = dt.Rows[80][(int)ColumnBF17.Col2].ToString();
            lblRow143.InnerText = dt.Rows[80][(int)ColumnBF17.Col3].ToString();
            lblTotal4.InnerText = dt.Rows[82][(int)ColumnBF17.Col2].ToString();
            lblTVA3.InnerText = dt.Rows[87][(int)ColumnBF17.Col3].ToString();
            divWarning5.Visible = dt.Rows[84][(int)ColumnBF17.Col4].ToString() == show;
            div11.Visible = dt.Rows[77][(int)ColumnBF17.Col4].ToString() == show;
            div12.Visible = dt.Rows[78][(int)ColumnBF17.Col4].ToString() == show;
            div13.Visible = dt.Rows[79][(int)ColumnBF17.Col4].ToString() == show;
            div14.Visible = dt.Rows[80][(int)ColumnBF17.Col4].ToString() == show;
            divTotal4.Visible = dt.Rows[82][(int)ColumnBF17.Col4].ToString() == show;
            divSub_Emoluments_du_notaire2.Visible = dt.Rows[76][(int)ColumnBF17.Col4].ToString() == show;
            //Fiscalité
            lblRow151.InnerText = dt.Rows[96][(int)ColumnBF17.Col1].ToString();
            lblRow152.InnerText = dt.Rows[96][(int)ColumnBF17.Col2].ToString();
            lblRow153.InnerText = dt.Rows[96][(int)ColumnBF17.Col3].ToString();
            lblRow161.InnerText = dt.Rows[100][(int)ColumnBF17.Col1].ToString();
            lblRow162.InnerText = dt.Rows[100][(int)ColumnBF17.Col2].ToString();
            lblRow163.InnerText = dt.Rows[100][(int)ColumnBF17.Col3].ToString();
            divWarning6.Visible = dt.Rows[97][(int)ColumnBF17.Col4].ToString() == show;
            divWarning7.Visible = dt.Rows[101][(int)ColumnBF17.Col4].ToString() == show;
            div15.Visible = dt.Rows[95][(int)ColumnBF17.Col4].ToString() == show;
            div16.Visible = dt.Rows[99][(int)ColumnBF17.Col4].ToString() == show;
            divSub_Fiscalité2.Visible = dt.Rows[91][(int)ColumnBF17.Col4].ToString() == show;

            var html = "";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblEmoluments_HT_du_notaire.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblDébours.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblTrésor.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblMontant_des_frais.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF17\\chart.png");
            //Attestation notariée
            if (dt.Rows[6][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Attestation notariée</font></b></td></tr>";
                html += "<tr><td colspan='5' bgcolor='#01ABE4'></td></tr>";
                html += string.Format(
                    "<tr><td align='right'>Trésor :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblTrésor2.InnerText);
                html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblEmoluments_HT_du_notaire2.InnerText);
                html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblMontant_des_frais2.InnerText);
                html += "<tr><td colspan='5'><br/><br/></td></tr>";
                html += "</table>";
            }
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du Notaire (état liquidatif) - C.com. Art. A 444-82
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Emoluments du Notaire (état liquidatif) - C.com. Art. A 444-82</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            if (dt.Rows[18][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow11.InnerText, lblRow12.InnerText, lblRow13.InnerText);
            }
            if (dt.Rows[19][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow21.InnerText, lblRow22.InnerText, lblRow23.InnerText);
            }
            if (dt.Rows[20][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow31.InnerText, lblRow32.InnerText, lblRow33.InnerText);
            }
            if (dt.Rows[21][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow41.InnerText, lblRow42.InnerText, lblRow43.InnerText);
            }
            if (dt.Rows[23][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'>{1}</td><td></td></tr>",
                    lblTotal.InnerText, lblTotal1.InnerText);
            }
            if (dt.Rows[25][(int)ColumnBF17.Col4].ToString() == show)
            {
                html +=
                    "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte de l'émolument minimum.</font></td></tr>";
            }
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    H129.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Emoluments du Notaire (mutation de biens - disposition indépendante) - C.com. Art. A 444-59
            if (dt.Rows[31][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du Notaire (mutation de biens - disposition indépendante) - C.com. Art. A 444-59</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                if (dt.Rows[32][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblRow51.InnerText, lblRow52.InnerText, lblRow53.InnerText);
                }
                if (dt.Rows[33][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblRow61.InnerText, lblRow62.InnerText, lblRow63.InnerText);
                }
                if (dt.Rows[34][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblRow71.InnerText, lblRow72.InnerText, lblRow73.InnerText);
                }
                if (dt.Rows[35][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        lblRow81.InnerText, lblRow82.InnerText, lblRow83.InnerText);
                }
                if (dt.Rows[37][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td align='right'></td><td></td></tr>",
                        lblTotal2.InnerText);
                }
                if (dt.Rows[39][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html +=
                        "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte de l'émolument minimum.</font></td></tr>";
                }
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    H143.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            row148.Visible = dt.Rows[46][(int)ColumnBF17.Col4].ToString() == show;
            if (dt.Rows[46][(int)ColumnBF17.Col4].ToString() == show)
            {
                H148.InnerText = dt.Rows[46][(int)ColumnBF17.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments du notaire - Etat liquidatif :</td><td align='right'>{0}</td><td></td></tr>",
                    H148.InnerText);
            }
            row149.Visible = dt.Rows[47][(int)ColumnBF17.Col4].ToString() == show;
            if (dt.Rows[47][(int)ColumnBF17.Col4].ToString() == show)
            {
                H149.InnerText = dt.Rows[47][(int)ColumnBF17.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments du notaire - Acte de disposition indépendante :</td><td align='right'>{0}</td><td></td></tr>",
                    H149.InnerText);
            }
            H150.InnerText = dt.Rows[48][(int)ColumnBF17.Col3].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités (HT) :</td><td align='right'>{0}</td><td></td></tr>",
                    H150.InnerText);
            row151.Visible = dt.Rows[49][(int)ColumnBF17.Col4].ToString() == show;
            if (dt.Rows[49][(int)ColumnBF17.Col4].ToString() == show)
            {
                H151.InnerText = dt.Rows[49][(int)ColumnBF17.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    H151.InnerText);
            }
            H152.InnerText = dt.Rows[50][(int)ColumnBF17.Col3].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    H152.InnerText);
            H153.InnerText = dt.Rows[51][(int)ColumnBF17.Col3].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    H153.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            H157.InnerText = dt.Rows[55][(int)ColumnBF17.Col3].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    H157.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            if (dt.Rows[57][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                //html += string.Format(
                //    "<tr><td colspan='2' align='right'>Fiscalité :</td><td align='right'>{0}</td><td></td></tr>",
                //    lblFiscalité.InnerText);
                html += "<tr><td colspan='4'>Droit fixe (Art. 680) :</td></tr>";
                html += "<tr><td colspan='4' align='center'>Exonération - Enregistrement gratuit.</td></tr>";
                if (dt.Rows[61][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td>Taxe de publicité :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        lblRow91.InnerText, lblRow92.InnerText, lblRow93.InnerText);
                }
                if (dt.Rows[63][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html +=
                        "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte du minimum de perception.</font></td></tr>";
                }
                if (dt.Rows[65][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td>CSI (art. 879 du CGI) :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        lblRow101.InnerText, lblRow102.InnerText, lblRow103.InnerText);
                }
                if (dt.Rows[67][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html +=
                        "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte du minimum de perception.</font></td></tr>";
                }
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Depot des pieces du jugement d'homologation
            if (dt.Rows[69][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Dépot des pièces du jugement d`homologation</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments fixes :</td><td align='right'>{0}</td><td></td></tr>",
                    lblEmoluments_fixes.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Droit fixe (Art. 680) :</td><td align='right'>{0}</td><td></td></tr>",
                    lblDroit_fixe.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Total :</td><td align='right'>{0}</td><td></td></tr>",
                    lblTotal3.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
            }
            //Attestation notariee ulterieure
            if (dt.Rows[75][(int)ColumnBF17.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Attestation notariée ultérieure</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                if (dt.Rows[76][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html +=
                        "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF'>Emoluments du Notaire - C.com. Art. A 444-59</font></b></td></tr>";
                    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    if (dt.Rows[77][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                            lblRow111.InnerText, lblRow112.InnerText, lblRow113.InnerText);
                    }
                    if (dt.Rows[78][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                            lblRow121.InnerText, lblRow122.InnerText, lblRow123.InnerText);
                    }
                    if (dt.Rows[79][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                            lblRow131.InnerText, lblRow132.InnerText, lblRow133.InnerText);
                    }
                    if (dt.Rows[80][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                            lblRow141.InnerText, lblRow142.InnerText, lblRow143.InnerText);
                    }
                    if (dt.Rows[82][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td></td><td></td></tr>",
                            lblTotal4.InnerText);
                    }
                    if (dt.Rows[84][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html +=
                            "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte de l'émolument minimum.</font></td></tr>";
                    }
                    //html += string.Format("<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    //    lblTotal_HT3.InnerText);
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emoluments réglementés :</td><td align='right'>{0}</td><td></td></tr>",
                        lblTVA3.InnerText);
                    //html += string.Format("<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    //    lblTotal_TTC3.InnerText);
                    html += "<tr><td colspan='4'></td></tr>";
                }
                //Récapitulatif et calcul de la TVA
                html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                H189.InnerText = dt.Rows[87][(int)ColumnBF17.Col3].ToString();
                H190.InnerText = dt.Rows[88][(int)ColumnBF17.Col3].ToString();
                H191.InnerText = dt.Rows[89][(int)ColumnBF17.Col3].ToString();
                html += string.Format("<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                        H189.InnerText);
                html += string.Format("<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                        H190.InnerText);
                html += string.Format("<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                        H191.InnerText);
                html += "<tr><td colspan='4'></td></tr>";
                //Fiscalité
                if (dt.Rows[91][(int)ColumnBF17.Col4].ToString() == show)
                {
                    html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Fiscalité</font></b></td></tr>";
                    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    //html += string.Format(
                    //    "<tr><td colspan='2' align='right'>Fiscalité :</td><td align='right'>{0}</td><td></td></tr>",
                    //    lblFiscalité2.InnerText);
                    html += "<tr><td colspan='4'>Droit fixe (Art. 847) :</td></tr>";
                    html += "<tr><td colspan='4' align='center'>Exonération - Enregistrement gratuit.</td></tr>";
                    if (dt.Rows[95][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td>Taxe de publicité :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            lblRow151.InnerText, lblRow152.InnerText, lblRow153.InnerText);
                    }
                    if (dt.Rows[97][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html +=
                            "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte du minimum de perception.</font></td></tr>";
                    }
                    if (dt.Rows[99][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html += string.Format(
                            "<tr><td>CSI (art. 879 du CGI) :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                            lblRow161.InnerText, lblRow162.InnerText, lblRow163.InnerText);
                    }
                    if (dt.Rows[101][(int)ColumnBF17.Col4].ToString() == show)
                    {
                        html +=
                            "<tr><td colspan='4' align='center'><font color='#FF0000'>Prise en compte du minimum de perception.</font></td></tr>";
                    }
                    html += "<tr><td colspan='4'></td></tr>";
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF17-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 2, 103, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF17");
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
                           "','chk1':'" + chk1.Checked +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','chk2':'" + chk2.Checked +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','txtImmobilier_Article_1_Percent_1':'" + txtImmobilier_Article_1_Percent_1.Text +
                           "','txtImmobilier_Article_1_Percent_2':'" + txtImmobilier_Article_1_Percent_2.Text +
                           "','txtImmobilier_Article_1_Valeur':'" + txtImmobilier_Article_1_Valeur.Text +
                           "','txtImmobilier_Article_2_Percent_1':'" + txtImmobilier_Article_2_Percent_1.Text +
                           "','txtImmobilier_Article_2_Percent_2':'" + txtImmobilier_Article_2_Percent_2.Text +
                           "','txtImmobilier_Article_2_Valeur':'" + txtImmobilier_Article_2_Valeur.Text +
                           "','txtImmobilier_Article_3_Percent_1':'" + txtImmobilier_Article_3_Percent_1.Text +
                           "','txtImmobilier_Article_3_Percent_2':'" + txtImmobilier_Article_3_Percent_2.Text +
                           "','txtImmobilier_Article_3_Valeur':'" + txtImmobilier_Article_3_Valeur.Text +
                           "','txtImmobilier_Article_4_Percent_1':'" + txtImmobilier_Article_4_Percent_1.Text +
                           "','txtImmobilier_Article_4_Percent_2':'" + txtImmobilier_Article_4_Percent_2.Text +
                           "','txtImmobilier_Article_4_Valeur':'" + txtImmobilier_Article_4_Valeur.Text +
                           "','txtImmobilier_Article_5_Percent_1':'" + txtImmobilier_Article_5_Percent_1.Text +
                           "','txtImmobilier_Article_5_Percent_2':'" + txtImmobilier_Article_5_Percent_2.Text +
                           "','txtImmobilier_Article_5_Valeur':'" + txtImmobilier_Article_5_Valeur.Text +
                           "','txtImmobilier_Article_6_Percent_1':'" + txtImmobilier_Article_6_Percent_1.Text +
                           "','txtImmobilier_Article_6_Percent_2':'" + txtImmobilier_Article_6_Percent_2.Text +
                           "','txtImmobilier_Article_6_Valeur':'" + txtImmobilier_Article_6_Valeur.Text +
                           "','txtImmobilier_Article_7_Percent_1':'" + txtImmobilier_Article_7_Percent_1.Text +
                           "','txtImmobilier_Article_7_Percent_2':'" + txtImmobilier_Article_7_Percent_2.Text +
                           "','txtImmobilier_Article_7_Valeur':'" + txtImmobilier_Article_7_Valeur.Text +
                           "','txtImmobilier_Article_8_Percent_1':'" + txtImmobilier_Article_8_Percent_1.Text +
                           "','txtImmobilier_Article_8_Percent_2':'" + txtImmobilier_Article_8_Percent_2.Text +
                           "','txtImmobilier_Article_8_Valeur':'" + txtImmobilier_Article_8_Valeur.Text +
                           "','txtImmobilier_Article_9_Percent_1':'" + txtImmobilier_Article_9_Percent_1.Text +
                           "','txtImmobilier_Article_9_Percent_2':'" + txtImmobilier_Article_9_Percent_2.Text +
                           "','txtImmobilier_Article_9_Valeur':'" + txtImmobilier_Article_9_Valeur.Text +
                           "','txtImmobilier_Article_10_Percent_1':'" + txtImmobilier_Article_10_Percent_1.Text +
                           "','txtImmobilier_Article_10_Percent_2':'" + txtImmobilier_Article_10_Percent_2.Text +
                           "','txtImmobilier_Article_10_Valeur':'" + txtImmobilier_Article_10_Valeur.Text +
                           "','ddl5':'" + ddl5.SelectedValue +
                           "','txtMobilier_Article_1_Percent_1':'" + txtMobilier_Article_1_Percent_1.Text +
                           "','txtMobilier_Article_1_Percent_2':'" + txtMobilier_Article_1_Percent_2.Text +
                           "','txtMobilier_Article_1_Valeur':'" + txtMobilier_Article_1_Valeur.Text +
                           "','txtMobilier_Article_2_Percent_1':'" + txtMobilier_Article_2_Percent_1.Text +
                           "','txtMobilier_Article_2_Percent_2':'" + txtMobilier_Article_2_Percent_2.Text +
                           "','txtMobilier_Article_2_Valeur':'" + txtMobilier_Article_2_Valeur.Text +
                           "','txtMobilier_Article_3_Percent_1':'" + txtMobilier_Article_3_Percent_1.Text +
                           "','txtMobilier_Article_3_Percent_2':'" + txtMobilier_Article_3_Percent_2.Text +
                           "','txtMobilier_Article_3_Valeur':'" + txtMobilier_Article_3_Valeur.Text +
                           "','txtMobilier_Article_4_Percent_1':'" + txtMobilier_Article_4_Percent_1.Text +
                           "','txtMobilier_Article_4_Percent_2':'" + txtMobilier_Article_4_Percent_2.Text +
                           "','txtMobilier_Article_4_Valeur':'" + txtMobilier_Article_4_Valeur.Text +
                           "','txtMobilier_Article_5_Percent_1':'" + txtMobilier_Article_5_Percent_1.Text +
                           "','txtMobilier_Article_5_Percent_2':'" + txtMobilier_Article_5_Percent_2.Text +
                           "','txtMobilier_Article_5_Valeur':'" + txtMobilier_Article_5_Valeur.Text +
                           "','txtMobilier_Article_6_Percent_1':'" + txtMobilier_Article_6_Percent_1.Text +
                           "','txtMobilier_Article_6_Percent_2':'" + txtMobilier_Article_6_Percent_2.Text +
                           "','txtMobilier_Article_6_Valeur':'" + txtMobilier_Article_6_Valeur.Text +
                           "','txtMobilier_Article_7_Percent_1':'" + txtMobilier_Article_7_Percent_1.Text +
                           "','txtMobilier_Article_7_Percent_2':'" + txtMobilier_Article_7_Percent_2.Text +
                           "','txtMobilier_Article_7_Valeur':'" + txtMobilier_Article_7_Valeur.Text +
                           "','txtMobilier_Article_8_Percent_1':'" + txtMobilier_Article_8_Percent_1.Text +
                           "','txtMobilier_Article_8_Percent_2':'" + txtMobilier_Article_8_Percent_2.Text +
                           "','txtMobilier_Article_8_Valeur':'" + txtMobilier_Article_8_Valeur.Text +
                           "','txtMobilier_Article_9_Percent_1':'" + txtMobilier_Article_9_Percent_1.Text +
                           "','txtMobilier_Article_9_Percent_2':'" + txtMobilier_Article_9_Percent_2.Text +
                           "','txtMobilier_Article_9_Valeur':'" + txtMobilier_Article_9_Valeur.Text +
                           "','txtMobilier_Article_10_Percent_1':'" + txtMobilier_Article_10_Percent_1.Text +
                           "','txtMobilier_Article_10_Percent_2':'" + txtMobilier_Article_10_Percent_2.Text +
                           "','txtMobilier_Article_10_Valeur':'" + txtMobilier_Article_10_Valeur.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF17", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','chk1':'" + chk1.Checked +
                           "','ddl3':'" + ddl3.SelectedValue +
                           "','txtZone01':'" + txtZone01.Text +
                           "','chk2':'" + chk2.Checked +
                           "','ddl4':'" + ddl4.SelectedValue +
                           "','txtImmobilier_Article_1_Percent_1':'" + txtImmobilier_Article_1_Percent_1.Text +
                           "','txtImmobilier_Article_1_Percent_2':'" + txtImmobilier_Article_1_Percent_2.Text +
                           "','txtImmobilier_Article_1_Valeur':'" + txtImmobilier_Article_1_Valeur.Text +
                           "','txtImmobilier_Article_2_Percent_1':'" + txtImmobilier_Article_2_Percent_1.Text +
                           "','txtImmobilier_Article_2_Percent_2':'" + txtImmobilier_Article_2_Percent_2.Text +
                           "','txtImmobilier_Article_2_Valeur':'" + txtImmobilier_Article_2_Valeur.Text +
                           "','txtImmobilier_Article_3_Percent_1':'" + txtImmobilier_Article_3_Percent_1.Text +
                           "','txtImmobilier_Article_3_Percent_2':'" + txtImmobilier_Article_3_Percent_2.Text +
                           "','txtImmobilier_Article_3_Valeur':'" + txtImmobilier_Article_3_Valeur.Text +
                           "','txtImmobilier_Article_4_Percent_1':'" + txtImmobilier_Article_4_Percent_1.Text +
                           "','txtImmobilier_Article_4_Percent_2':'" + txtImmobilier_Article_4_Percent_2.Text +
                           "','txtImmobilier_Article_4_Valeur':'" + txtImmobilier_Article_4_Valeur.Text +
                           "','txtImmobilier_Article_5_Percent_1':'" + txtImmobilier_Article_5_Percent_1.Text +
                           "','txtImmobilier_Article_5_Percent_2':'" + txtImmobilier_Article_5_Percent_2.Text +
                           "','txtImmobilier_Article_5_Valeur':'" + txtImmobilier_Article_5_Valeur.Text +
                           "','txtImmobilier_Article_6_Percent_1':'" + txtImmobilier_Article_6_Percent_1.Text +
                           "','txtImmobilier_Article_6_Percent_2':'" + txtImmobilier_Article_6_Percent_2.Text +
                           "','txtImmobilier_Article_6_Valeur':'" + txtImmobilier_Article_6_Valeur.Text +
                           "','txtImmobilier_Article_7_Percent_1':'" + txtImmobilier_Article_7_Percent_1.Text +
                           "','txtImmobilier_Article_7_Percent_2':'" + txtImmobilier_Article_7_Percent_2.Text +
                           "','txtImmobilier_Article_7_Valeur':'" + txtImmobilier_Article_7_Valeur.Text +
                           "','txtImmobilier_Article_8_Percent_1':'" + txtImmobilier_Article_8_Percent_1.Text +
                           "','txtImmobilier_Article_8_Percent_2':'" + txtImmobilier_Article_8_Percent_2.Text +
                           "','txtImmobilier_Article_8_Valeur':'" + txtImmobilier_Article_8_Valeur.Text +
                           "','txtImmobilier_Article_9_Percent_1':'" + txtImmobilier_Article_9_Percent_1.Text +
                           "','txtImmobilier_Article_9_Percent_2':'" + txtImmobilier_Article_9_Percent_2.Text +
                           "','txtImmobilier_Article_9_Valeur':'" + txtImmobilier_Article_9_Valeur.Text +
                           "','txtImmobilier_Article_10_Percent_1':'" + txtImmobilier_Article_10_Percent_1.Text +
                           "','txtImmobilier_Article_10_Percent_2':'" + txtImmobilier_Article_10_Percent_2.Text +
                           "','txtImmobilier_Article_10_Valeur':'" + txtImmobilier_Article_10_Valeur.Text +
                           "','ddl5':'" + ddl5.SelectedValue +
                           "','txtMobilier_Article_1_Percent_1':'" + txtMobilier_Article_1_Percent_1.Text +
                           "','txtMobilier_Article_1_Percent_2':'" + txtMobilier_Article_1_Percent_2.Text +
                           "','txtMobilier_Article_1_Valeur':'" + txtMobilier_Article_1_Valeur.Text +
                           "','txtMobilier_Article_2_Percent_1':'" + txtMobilier_Article_2_Percent_1.Text +
                           "','txtMobilier_Article_2_Percent_2':'" + txtMobilier_Article_2_Percent_2.Text +
                           "','txtMobilier_Article_2_Valeur':'" + txtMobilier_Article_2_Valeur.Text +
                           "','txtMobilier_Article_3_Percent_1':'" + txtMobilier_Article_3_Percent_1.Text +
                           "','txtMobilier_Article_3_Percent_2':'" + txtMobilier_Article_3_Percent_2.Text +
                           "','txtMobilier_Article_3_Valeur':'" + txtMobilier_Article_3_Valeur.Text +
                           "','txtMobilier_Article_4_Percent_1':'" + txtMobilier_Article_4_Percent_1.Text +
                           "','txtMobilier_Article_4_Percent_2':'" + txtMobilier_Article_4_Percent_2.Text +
                           "','txtMobilier_Article_4_Valeur':'" + txtMobilier_Article_4_Valeur.Text +
                           "','txtMobilier_Article_5_Percent_1':'" + txtMobilier_Article_5_Percent_1.Text +
                           "','txtMobilier_Article_5_Percent_2':'" + txtMobilier_Article_5_Percent_2.Text +
                           "','txtMobilier_Article_5_Valeur':'" + txtMobilier_Article_5_Valeur.Text +
                           "','txtMobilier_Article_6_Percent_1':'" + txtMobilier_Article_6_Percent_1.Text +
                           "','txtMobilier_Article_6_Percent_2':'" + txtMobilier_Article_6_Percent_2.Text +
                           "','txtMobilier_Article_6_Valeur':'" + txtMobilier_Article_6_Valeur.Text +
                           "','txtMobilier_Article_7_Percent_1':'" + txtMobilier_Article_7_Percent_1.Text +
                           "','txtMobilier_Article_7_Percent_2':'" + txtMobilier_Article_7_Percent_2.Text +
                           "','txtMobilier_Article_7_Valeur':'" + txtMobilier_Article_7_Valeur.Text +
                           "','txtMobilier_Article_8_Percent_1':'" + txtMobilier_Article_8_Percent_1.Text +
                           "','txtMobilier_Article_8_Percent_2':'" + txtMobilier_Article_8_Percent_2.Text +
                           "','txtMobilier_Article_8_Valeur':'" + txtMobilier_Article_8_Valeur.Text +
                           "','txtMobilier_Article_9_Percent_1':'" + txtMobilier_Article_9_Percent_1.Text +
                           "','txtMobilier_Article_9_Percent_2':'" + txtMobilier_Article_9_Percent_2.Text +
                           "','txtMobilier_Article_9_Valeur':'" + txtMobilier_Article_9_Valeur.Text +
                           "','txtMobilier_Article_10_Percent_1':'" + txtMobilier_Article_10_Percent_1.Text +
                           "','txtMobilier_Article_10_Percent_2':'" + txtMobilier_Article_10_Percent_2.Text +
                           "','txtMobilier_Article_10_Valeur':'" + txtMobilier_Article_10_Valeur.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF17", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF17", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF17", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF17
    {
        Col1 = 3,
        Col2 = 4,
        Col3 = 5,
        Col4 = 6,
    }
}