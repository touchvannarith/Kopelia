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
    public partial class BF12_1b : Page
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
                    chkImmobilier_Article_1_partage.Checked =
                        json["chkImmobilier_Article_1_partage"].TransformToBoolean();
                    chkImmobilier_Article_1_usufruit.Checked =
                        json["chkImmobilier_Article_1_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_1_Valeur_PP.Text = json["txtImmobilier_Article_1_Valeur_PP"].ToString();
                    ddlImmobilier_Article_1.SelectedValue = json["ddlImmobilier_Article_1"].ToString();
                    txtImmobilier_Article_1_Valeur_NP.Text = json["txtImmobilier_Article_1_Valeur_NP"].ToString();
                    chkImmobilier_Article_2_partage.Checked =
                        json["chkImmobilier_Article_2_partage"].TransformToBoolean();
                    chkImmobilier_Article_2_usufruit.Checked =
                        json["chkImmobilier_Article_2_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_2_Valeur_PP.Text = json["txtImmobilier_Article_2_Valeur_PP"].ToString();
                    ddlImmobilier_Article_2.SelectedValue = json["ddlImmobilier_Article_2"].ToString();
                    txtImmobilier_Article_2_Valeur_NP.Text = json["txtImmobilier_Article_2_Valeur_NP"].ToString();
                    chkImmobilier_Article_3_partage.Checked =
                        json["chkImmobilier_Article_3_partage"].TransformToBoolean();
                    chkImmobilier_Article_3_usufruit.Checked =
                        json["chkImmobilier_Article_3_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_3_Valeur_PP.Text = json["txtImmobilier_Article_3_Valeur_PP"].ToString();
                    ddlImmobilier_Article_3.SelectedValue = json["ddlImmobilier_Article_3"].ToString();
                    txtImmobilier_Article_3_Valeur_NP.Text = json["txtImmobilier_Article_3_Valeur_NP"].ToString();
                    chkImmobilier_Article_4_partage.Checked =
                        json["chkImmobilier_Article_4_partage"].TransformToBoolean();
                    chkImmobilier_Article_4_usufruit.Checked =
                        json["chkImmobilier_Article_4_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_4_Valeur_PP.Text = json["txtImmobilier_Article_4_Valeur_PP"].ToString();
                    ddlImmobilier_Article_4.SelectedValue = json["ddlImmobilier_Article_4"].ToString();
                    txtImmobilier_Article_4_Valeur_NP.Text = json["txtImmobilier_Article_4_Valeur_NP"].ToString();
                    chkImmobilier_Article_5_partage.Checked =
                        json["chkImmobilier_Article_5_partage"].TransformToBoolean();
                    chkImmobilier_Article_5_usufruit.Checked =
                        json["chkImmobilier_Article_5_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_5_Valeur_PP.Text = json["txtImmobilier_Article_5_Valeur_PP"].ToString();
                    ddlImmobilier_Article_5.SelectedValue = json["ddlImmobilier_Article_5"].ToString();
                    txtImmobilier_Article_5_Valeur_NP.Text = json["txtImmobilier_Article_5_Valeur_NP"].ToString();
                    chkImmobilier_Article_6_partage.Checked =
                        json["chkImmobilier_Article_6_partage"].TransformToBoolean();
                    chkImmobilier_Article_6_usufruit.Checked =
                        json["chkImmobilier_Article_6_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_6_Valeur_PP.Text = json["txtImmobilier_Article_6_Valeur_PP"].ToString();
                    ddlImmobilier_Article_6.SelectedValue = json["ddlImmobilier_Article_6"].ToString();
                    txtImmobilier_Article_6_Valeur_NP.Text = json["txtImmobilier_Article_6_Valeur_NP"].ToString();
                    chkImmobilier_Article_7_partage.Checked =
                        json["chkImmobilier_Article_7_partage"].TransformToBoolean();
                    chkImmobilier_Article_7_usufruit.Checked =
                        json["chkImmobilier_Article_7_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_7_Valeur_PP.Text = json["txtImmobilier_Article_7_Valeur_PP"].ToString();
                    ddlImmobilier_Article_7.SelectedValue = json["ddlImmobilier_Article_7"].ToString();
                    txtImmobilier_Article_7_Valeur_NP.Text = json["txtImmobilier_Article_7_Valeur_NP"].ToString();
                    chkImmobilier_Article_8_partage.Checked =
                        json["chkImmobilier_Article_8_partage"].TransformToBoolean();
                    chkImmobilier_Article_8_usufruit.Checked =
                        json["chkImmobilier_Article_8_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_8_Valeur_PP.Text = json["txtImmobilier_Article_8_Valeur_PP"].ToString();
                    ddlImmobilier_Article_8.SelectedValue = json["ddlImmobilier_Article_8"].ToString();
                    txtImmobilier_Article_8_Valeur_NP.Text = json["txtImmobilier_Article_8_Valeur_NP"].ToString();
                    chkImmobilier_Article_9_partage.Checked =
                        json["chkImmobilier_Article_9_partage"].TransformToBoolean();
                    chkImmobilier_Article_9_usufruit.Checked =
                        json["chkImmobilier_Article_9_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_9_Valeur_PP.Text = json["txtImmobilier_Article_9_Valeur_PP"].ToString();
                    ddlImmobilier_Article_9.SelectedValue = json["ddlImmobilier_Article_9"].ToString();
                    txtImmobilier_Article_9_Valeur_NP.Text = json["txtImmobilier_Article_9_Valeur_NP"].ToString();
                    chkImmobilier_Article_10_partage.Checked =
                        json["chkImmobilier_Article_10_partage"].TransformToBoolean();
                    chkImmobilier_Article_10_usufruit.Checked =
                        json["chkImmobilier_Article_10_usufruit"].TransformToBoolean();
                    txtImmobilier_Article_10_Valeur_PP.Text = json["txtImmobilier_Article_10_Valeur_PP"].ToString();
                    ddlImmobilier_Article_10.SelectedValue = json["ddlImmobilier_Article_10"].ToString();
                    txtImmobilier_Article_10_Valeur_NP.Text = json["txtImmobilier_Article_10_Valeur_NP"].ToString();
                    ddl2.SelectedValue = json["ddl2"].ToString();
                    chkMobilier_Article_1_usufruit.Checked =
                        json["chkMobilier_Article_1_usufruit"].TransformToBoolean();
                    txtMobilier_Article_1_Valeur_PP.Text = json["txtMobilier_Article_1_Valeur_PP"].ToString();
                    ddlMobilier_Article_1.SelectedValue = json["ddlMobilier_Article_1"].ToString();
                    txtMobilier_Article_1_Valeur_NP.Text = json["txtMobilier_Article_1_Valeur_NP"].ToString();
                    chkMobilier_Article_2_usufruit.Checked =
                        json["chkMobilier_Article_2_usufruit"].TransformToBoolean();
                    txtMobilier_Article_2_Valeur_PP.Text = json["txtMobilier_Article_2_Valeur_PP"].ToString();
                    ddlMobilier_Article_2.SelectedValue = json["ddlMobilier_Article_2"].ToString();
                    txtMobilier_Article_2_Valeur_NP.Text = json["txtMobilier_Article_2_Valeur_NP"].ToString();
                    chkMobilier_Article_3_usufruit.Checked =
                        json["chkMobilier_Article_3_usufruit"].TransformToBoolean();
                    txtMobilier_Article_3_Valeur_PP.Text = json["txtMobilier_Article_3_Valeur_PP"].ToString();
                    ddlMobilier_Article_3.SelectedValue = json["ddlMobilier_Article_3"].ToString();
                    txtMobilier_Article_3_Valeur_NP.Text = json["txtMobilier_Article_3_Valeur_NP"].ToString();
                    chkMobilier_Article_4_usufruit.Checked =
                        json["chkMobilier_Article_4_usufruit"].TransformToBoolean();
                    txtMobilier_Article_4_Valeur_PP.Text = json["txtMobilier_Article_4_Valeur_PP"].ToString();
                    ddlMobilier_Article_4.SelectedValue = json["ddlMobilier_Article_4"].ToString();
                    txtMobilier_Article_4_Valeur_NP.Text = json["txtMobilier_Article_4_Valeur_NP"].ToString();
                    chkMobilier_Article_5_usufruit.Checked =
                        json["chkMobilier_Article_5_usufruit"].TransformToBoolean();
                    txtMobilier_Article_5_Valeur_PP.Text = json["txtMobilier_Article_5_Valeur_PP"].ToString();
                    ddlMobilier_Article_5.SelectedValue = json["ddlMobilier_Article_5"].ToString();
                    txtMobilier_Article_5_Valeur_NP.Text = json["txtMobilier_Article_5_Valeur_NP"].ToString();
                    chkMobilier_Article_6_usufruit.Checked =
                        json["chkMobilier_Article_6_usufruit"].TransformToBoolean();
                    txtMobilier_Article_6_Valeur_PP.Text = json["txtMobilier_Article_6_Valeur_PP"].ToString();
                    ddlMobilier_Article_6.SelectedValue = json["ddlMobilier_Article_6"].ToString();
                    txtMobilier_Article_6_Valeur_NP.Text = json["txtMobilier_Article_6_Valeur_NP"].ToString();
                    chkMobilier_Article_7_usufruit.Checked =
                        json["chkMobilier_Article_7_usufruit"].TransformToBoolean();
                    txtMobilier_Article_7_Valeur_PP.Text = json["txtMobilier_Article_7_Valeur_PP"].ToString();
                    ddlMobilier_Article_7.SelectedValue = json["ddlMobilier_Article_7"].ToString();
                    txtMobilier_Article_7_Valeur_NP.Text = json["txtMobilier_Article_7_Valeur_NP"].ToString();
                    chkMobilier_Article_8_usufruit.Checked =
                        json["chkMobilier_Article_8_usufruit"].TransformToBoolean();
                    txtMobilier_Article_8_Valeur_PP.Text = json["txtMobilier_Article_8_Valeur_PP"].ToString();
                    ddlMobilier_Article_8.SelectedValue = json["ddlMobilier_Article_8"].ToString();
                    txtMobilier_Article_8_Valeur_NP.Text = json["txtMobilier_Article_8_Valeur_NP"].ToString();
                    chkMobilier_Article_9_usufruit.Checked =
                        json["chkMobilier_Article_9_usufruit"].TransformToBoolean();
                    txtMobilier_Article_9_Valeur_PP.Text = json["txtMobilier_Article_9_Valeur_PP"].ToString();
                    ddlMobilier_Article_9.SelectedValue = json["ddlMobilier_Article_9"].ToString();
                    txtMobilier_Article_9_Valeur_NP.Text = json["txtMobilier_Article_9_Valeur_NP"].ToString();
                    chkMobilier_Article_10_usufruit.Checked =
                        json["chkMobilier_Article_10_usufruit"].TransformToBoolean();
                    txtMobilier_Article_10_Valeur_PP.Text = json["txtMobilier_Article_10_Valeur_PP"].ToString();
                    ddlMobilier_Article_10.SelectedValue = json["ddlMobilier_Article_10"].ToString();
                    txtMobilier_Article_10_Valeur_NP.Text = json["txtMobilier_Article_10_Valeur_NP"].ToString();
                    ddl3.SelectedValue = json["ddl3"].ToString();
                    txtPassif_Article_1_Valeur.Text = json["txtPassif_Article_1_Valeur"].ToString();
                    txtPassif_Article_2_Valeur.Text = json["txtPassif_Article_2_Valeur"].ToString();
                    txtPassif_Article_3_Valeur.Text = json["txtPassif_Article_3_Valeur"].ToString();
                    txtPassif_Article_4_Valeur.Text = json["txtPassif_Article_4_Valeur"].ToString();
                    txtPassif_Article_5_Valeur.Text = json["txtPassif_Article_5_Valeur"].ToString();
                    txtPassif_Article_6_Valeur.Text = json["txtPassif_Article_6_Valeur"].ToString();
                    txtPassif_Article_7_Valeur.Text = json["txtPassif_Article_7_Valeur"].ToString();
                    txtPassif_Article_8_Valeur.Text = json["txtPassif_Article_8_Valeur"].ToString();
                    txtPassif_Article_9_Valeur.Text = json["txtPassif_Article_9_Valeur"].ToString();
                    txtPassif_Article_10_Valeur.Text = json["txtPassif_Article_10_Valeur"].ToString();

                    chk1.Checked = json["chk1"].TransformToBoolean();
                
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
                new RangeCoordinates { Row = 5, Column = 4, Height = 5, Width = 1 },
                new object[]
                {
                        new object[] {"1b"},
                        new object[] {ddl1.SelectedValue},
                        new object[] {ddl2.SelectedValue},
                        new object[] {ddl3.SelectedValue},
                        new object[] {chk1.Checked.TransformToBooleanFr()}
                });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates { Row = 13, Column = 3, Height = 10, Width = 4 },
                new object[]
                {
                        new object[]{chkImmobilier_Article_1_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_1.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_1_Valeur_PP.Text, chkImmobilier_Article_1_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_2_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_2.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_2_Valeur_PP.Text, chkImmobilier_Article_2_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_3_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_3.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_3_Valeur_PP.Text, chkImmobilier_Article_3_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_4_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_4.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_4_Valeur_PP.Text, chkImmobilier_Article_4_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_5_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_5.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_5_Valeur_PP.Text, chkImmobilier_Article_5_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_6_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_6.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_6_Valeur_PP.Text, chkImmobilier_Article_6_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_7_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_7.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_7_Valeur_PP.Text, chkImmobilier_Article_7_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_8_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_8.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_8_Valeur_PP.Text, chkImmobilier_Article_8_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_9_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_9.SelectedValue.TransformToPercentage(),
                            txtImmobilier_Article_9_Valeur_PP.Text, chkImmobilier_Article_9_partage.Checked.TransformToBooleanFr()},
                        new object[]{chkImmobilier_Article_10_usufruit.Checked.TransformToBooleanFr(), ddlImmobilier_Article_10.SelectedValue.TransformToPercentage(),
                           txtImmobilier_Article_10_Valeur_PP.Text, chkImmobilier_Article_10_partage.Checked.TransformToBooleanFr()}
                });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates { Row = 26, Column = 3, Height = 10, Width = 3 },
                new object[]
                {
                        new object[]{chkMobilier_Article_1_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_1.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_1_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_2_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_2.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_2_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_3_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_3.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_3_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_4_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_4.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_4_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_5_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_5.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_5_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_6_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_6.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_6_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_7_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_7.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_7_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_8_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_8.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_8_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_9_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_9.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_9_Valeur_PP.Text},
                            new object[]{chkMobilier_Article_10_usufruit.Checked.TransformToBooleanFr(), ddlMobilier_Article_10.SelectedValue.TransformToPercentage(),
                            txtMobilier_Article_10_Valeur_PP.Text}
                });
            _excelService.SetRange(_sessionId, "ENTREE_S",
                new RangeCoordinates { Row = 39, Column = 5, Height = 10, Width = 1 },
                new object[]
                {
                        new object[]{txtPassif_Article_1_Valeur.Text},
                        new object[]{txtPassif_Article_2_Valeur.Text},
                        new object[]{txtPassif_Article_3_Valeur.Text},
                        new object[]{txtPassif_Article_4_Valeur.Text},
                        new object[]{txtPassif_Article_5_Valeur.Text},
                        new object[]{txtPassif_Article_6_Valeur.Text},
                        new object[]{txtPassif_Article_7_Valeur.Text},
                        new object[]{txtPassif_Article_8_Valeur.Text},
                        new object[]{txtPassif_Article_9_Valeur.Text},
                        new object[]{txtPassif_Article_10_Valeur.Text}
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
            lblF102.InnerText = dt.Rows[0][(int)ColumnBF12.Col1].ToString();
            lblF103.InnerText = dt.Rows[1][(int)ColumnBF12.Col1].ToString();
            lblF104.InnerText = dt.Rows[2][(int)ColumnBF12.Col1].ToString();
            lblF106.InnerText = dt.Rows[4][(int)ColumnBF12.Col1].ToString();
            div108.Visible = dt.Rows[6][(int)ColumnBF12.Col4].ToString() == show;
            lblF108.InnerText = dt.Rows[6][(int)ColumnBF12.Col1].ToString();
            //TVA
            H171.InnerText = dt.Rows[69][(int)ColumnBF12.Col3].ToString();
            H173.InnerText = dt.Rows[71][(int)ColumnBF12.Col3].ToString();
            H175.InnerText = dt.Rows[73][(int)ColumnBF12.Col3].ToString();
            H176.InnerText = dt.Rows[74][(int)ColumnBF12.Col3].ToString();
            H177.InnerText = dt.Rows[75][(int)ColumnBF12.Col3].ToString();
            //Débours
            lblH182.InnerText = dt.Rows[80][(int)ColumnBF12.Col3].ToString();
            //Trésor public
            div187.Visible = dt.Rows[85][(int)ColumnBF12.Col4].ToString() == show;
            div188.Visible = dt.Rows[86][(int)ColumnBF12.Col4].ToString() == show;
            lblF188.InnerText = dt.Rows[86][(int)ColumnBF12.Col1].ToString();
            lblG188.InnerText = dt.Rows[86][(int)ColumnBF12.Col2].ToString();
            lblH188.InnerText = dt.Rows[86][(int)ColumnBF12.Col3].ToString();
            lblF189.Visible = dt.Rows[87][(int)ColumnBF12.Col4].ToString() == show;
            lblF189.InnerText = dt.Rows[87][(int)ColumnBF12.Col1].ToString();
            div190.Visible = dt.Rows[88][(int)ColumnBF12.Col4].ToString() == show;

            var html = "";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += dt.Rows[6][(int)ColumnBF12.Col4].ToString() == show
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
            if (dt.Rows[6][(int)ColumnBF12.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>Pour ordre, il est précisé que les frais déductibles s'élèvent à la somme de :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF108.InnerText);
            }
            html += "</table>";
            if (dt.Rows[6][(int)ColumnBF12.Col4].ToString() == show)
            {
                html += string.Format(
                        "<div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                        Request.PhysicalApplicationPath + "tmp\\BF12-1b\\chart.png");
            }
            else
            {
                html += string.Format(
                        "<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                        Request.PhysicalApplicationPath + "tmp\\BF12-1b\\chart.png");
            }
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire - C.com. Art. A 444-122
            row127.Visible = dt.Rows[25][(int)ColumnBF12.Col4].ToString() == show;
            if (dt.Rows[25][(int)ColumnBF12.Col4].ToString() == show)
            {
                html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments du notaire - C.com. Art. A 444-122</font></b></td></tr>";
                html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                row128.Visible = dt.Rows[26][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[26][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F128.InnerText = dt.Rows[26][(int)ColumnBF12.Col1].ToString();
                    G128.InnerText = dt.Rows[26][(int)ColumnBF12.Col2].ToString();
                    H128.InnerText = dt.Rows[26][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F128.InnerText, G128.InnerText, H128.InnerText);
                }
                row129.Visible = dt.Rows[27][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[27][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F129.InnerText = dt.Rows[27][(int)ColumnBF12.Col1].ToString();
                    G129.InnerText = dt.Rows[27][(int)ColumnBF12.Col2].ToString();
                    H129.InnerText = dt.Rows[27][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F129.InnerText, G129.InnerText, H129.InnerText);
                }
                row130.Visible = dt.Rows[28][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[28][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F130.InnerText = dt.Rows[28][(int)ColumnBF12.Col1].ToString();
                    G130.InnerText = dt.Rows[28][(int)ColumnBF12.Col2].ToString();
                    H130.InnerText = dt.Rows[28][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F130.InnerText, G130.InnerText, H130.InnerText);
                }
                row131.Visible = dt.Rows[29][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[29][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F131.InnerText = dt.Rows[29][(int)ColumnBF12.Col1].ToString();
                    G131.InnerText = dt.Rows[29][(int)ColumnBF12.Col2].ToString();
                    H131.InnerText = dt.Rows[29][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                        F131.InnerText, G131.InnerText, H131.InnerText);
                }
                row133.Visible = dt.Rows[31][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[31][(int)ColumnBF12.Col4].ToString() == show)
                {
                    G133.InnerText = dt.Rows[31][(int)ColumnBF12.Col2].ToString();
                    html += string.Format(
                        "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                        G133.InnerText);
                }
                row134.Visible = dt.Rows[32][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[32][(int)ColumnBF12.Col4].ToString() == show)
                {
                    H134.InnerText = dt.Rows[32][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                        H134.InnerText);
                }
                row136.Visible = dt.Rows[34][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[34][(int)ColumnBF12.Col4].ToString() == show)
                {
                    H136.InnerText = dt.Rows[34][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Emolument minimum :</td><td align='right'>{0}</td><td></td></tr>",
                        H136.InnerText);
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
            if (dt.Rows[85][(int)ColumnBF12.Col4].ToString() == show)
            {
                if (dt.Rows[86][(int)ColumnBF12.Col4].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td>Taxe publicité foncière :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        lblF188.InnerText, lblG188.InnerText, lblH188.InnerText);
                }
                if (dt.Rows[87][(int)ColumnBF12.Col4].ToString() == show)
                {
                    html += string.Format("<tr><td colspan='4' align='center'>{0}</td></tr>", lblF189.InnerText);
                }
                if (dt.Rows[88][(int)ColumnBF12.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='4' align='center'>Pour la TPF, il a été pris le minimum de perception soit 25 Euros.</td></tr>";
                }
            }
            row192.Visible = dt.Rows[90][(int)ColumnBF12.Col4].ToString() == show;
            if (dt.Rows[90][(int)ColumnBF12.Col4].ToString() == show)
            {
                H192.InnerText = dt.Rows[90][(int)ColumnBF12.Col3].ToString();
                H193.InnerText = dt.Rows[91][(int)ColumnBF12.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Imputation des droits payés antérieurement :</td><td align='right'>{0}</td><td></td></tr>",
                    H192.InnerText);
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Droits dûs après imputation :</td><td align='right'>{0}</td><td></td></tr>",
                    H193.InnerText);
            }
            row195.Visible = dt.Rows[93][(int)ColumnBF12.Col4].ToString() == show;
            if (dt.Rows[93][(int)ColumnBF12.Col4].ToString() == show)
            {
                row196.Visible = dt.Rows[94][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[94][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F196.InnerText = dt.Rows[94][(int)ColumnBF12.Col1].ToString();
                    G196.InnerText = dt.Rows[94][(int)ColumnBF12.Col2].ToString();
                    H196.InnerText = dt.Rows[94][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td>CSI (art. 879 du CGI) :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F196.InnerText, G196.InnerText, H196.InnerText);
                }
                row197.Visible = dt.Rows[95][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[95][(int)ColumnBF12.Col4].ToString() == show)
                {
                    html += "<tr><td colspan='2'>CSI (art. 879 du CGI) :</td><td colspan='2' align='center'>Pour la CSI, il a été pris le minimum de perception soit 15 Euros.</td></tr>";
                }
            }
            row199.Visible = dt.Rows[97][(int)ColumnBF12.Col4].ToString() == show;
            if (dt.Rows[97][(int)ColumnBF12.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4' align='left'>Droit de mutation sur le prix de cession :</td></tr>";
                row200.Visible = dt.Rows[98][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[98][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F200.InnerText = dt.Rows[98][(int)ColumnBF12.Col1].ToString();
                    G200.InnerText = dt.Rows[98][(int)ColumnBF12.Col2].ToString();
                    H200.InnerText = dt.Rows[98][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F200.InnerText, G200.InnerText, H200.InnerText);
                }
                row201.Visible = dt.Rows[99][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[99][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F201.InnerText = dt.Rows[99][(int)ColumnBF12.Col1].ToString();
                    G201.InnerText = dt.Rows[99][(int)ColumnBF12.Col2].ToString();
                    H201.InnerText = dt.Rows[99][(int)ColumnBF12.Col3].ToString();
                    html += string.Format(
                        "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                        F201.InnerText, G201.InnerText, H201.InnerText);
                }
                row202.Visible = dt.Rows[100][(int)ColumnBF12.Col4].ToString() == show;
                if (dt.Rows[100][(int)ColumnBF12.Col4].ToString() == show)
                {
                    F202.InnerText = dt.Rows[100][(int)ColumnBF12.Col1].ToString();
                    G202.InnerText = dt.Rows[100][(int)ColumnBF12.Col2].ToString();
                    H202.InnerText = dt.Rows[100][(int)ColumnBF12.Col3].ToString();
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




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF12-1b");
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
                        "','chkImmobilier_Article_1_partage':'" + chkImmobilier_Article_1_partage.Checked +
                        "','chkImmobilier_Article_1_usufruit':'" + chkImmobilier_Article_1_usufruit.Checked +
                        "','txtImmobilier_Article_1_Valeur_PP':'" + txtImmobilier_Article_1_Valeur_PP.Text +
                        "','ddlImmobilier_Article_1':'" + ddlImmobilier_Article_1.SelectedValue +
                        "','txtImmobilier_Article_1_Valeur_NP':'" + txtImmobilier_Article_1_Valeur_NP.Text +
                        "','chkImmobilier_Article_2_partage':'" + chkImmobilier_Article_2_partage.Checked +
                        "','chkImmobilier_Article_2_usufruit':'" + chkImmobilier_Article_2_usufruit.Checked +
                        "','txtImmobilier_Article_2_Valeur_PP':'" + txtImmobilier_Article_2_Valeur_PP.Text +
                        "','ddlImmobilier_Article_2':'" + ddlImmobilier_Article_2.SelectedValue +
                        "','txtImmobilier_Article_2_Valeur_NP':'" + txtImmobilier_Article_2_Valeur_NP.Text +
                        "','chkImmobilier_Article_3_partage':'" + chkImmobilier_Article_3_partage.Checked +
                        "','chkImmobilier_Article_3_usufruit':'" + chkImmobilier_Article_3_usufruit.Checked +
                        "','txtImmobilier_Article_3_Valeur_PP':'" + txtImmobilier_Article_3_Valeur_PP.Text +
                        "','ddlImmobilier_Article_3':'" + ddlImmobilier_Article_3.SelectedValue +
                        "','txtImmobilier_Article_3_Valeur_NP':'" + txtImmobilier_Article_3_Valeur_NP.Text +
                        "','chkImmobilier_Article_4_partage':'" + chkImmobilier_Article_4_partage.Checked +
                        "','chkImmobilier_Article_4_usufruit':'" + chkImmobilier_Article_4_usufruit.Checked +
                        "','txtImmobilier_Article_4_Valeur_PP':'" + txtImmobilier_Article_4_Valeur_PP.Text +
                        "','ddlImmobilier_Article_4':'" + ddlImmobilier_Article_4.SelectedValue +
                        "','txtImmobilier_Article_4_Valeur_NP':'" + txtImmobilier_Article_4_Valeur_NP.Text +
                        "','chkImmobilier_Article_5_partage':'" + chkImmobilier_Article_5_partage.Checked +
                        "','chkImmobilier_Article_5_usufruit':'" + chkImmobilier_Article_5_usufruit.Checked +
                        "','txtImmobilier_Article_5_Valeur_PP':'" + txtImmobilier_Article_5_Valeur_PP.Text +
                        "','ddlImmobilier_Article_5':'" + ddlImmobilier_Article_5.SelectedValue +
                        "','txtImmobilier_Article_5_Valeur_NP':'" + txtImmobilier_Article_5_Valeur_NP.Text +
                        "','chkImmobilier_Article_6_partage':'" + chkImmobilier_Article_6_partage.Checked +
                        "','chkImmobilier_Article_6_usufruit':'" + chkImmobilier_Article_6_usufruit.Checked +
                        "','txtImmobilier_Article_6_Valeur_PP':'" + txtImmobilier_Article_6_Valeur_PP.Text +
                        "','ddlImmobilier_Article_6':'" + ddlImmobilier_Article_6.SelectedValue +
                        "','txtImmobilier_Article_6_Valeur_NP':'" + txtImmobilier_Article_6_Valeur_NP.Text +
                        "','chkImmobilier_Article_7_partage':'" + chkImmobilier_Article_7_partage.Checked +
                        "','chkImmobilier_Article_7_usufruit':'" + chkImmobilier_Article_7_usufruit.Checked +
                        "','txtImmobilier_Article_7_Valeur_PP':'" + txtImmobilier_Article_7_Valeur_PP.Text +
                        "','ddlImmobilier_Article_7':'" + ddlImmobilier_Article_7.SelectedValue +
                        "','txtImmobilier_Article_7_Valeur_NP':'" + txtImmobilier_Article_7_Valeur_NP.Text +
                        "','chkImmobilier_Article_8_partage':'" + chkImmobilier_Article_8_partage.Checked +
                        "','chkImmobilier_Article_8_usufruit':'" + chkImmobilier_Article_8_usufruit.Checked +
                        "','txtImmobilier_Article_8_Valeur_PP':'" + txtImmobilier_Article_8_Valeur_PP.Text +
                        "','ddlImmobilier_Article_8':'" + ddlImmobilier_Article_8.SelectedValue +
                        "','txtImmobilier_Article_8_Valeur_NP':'" + txtImmobilier_Article_8_Valeur_NP.Text +
                        "','chkImmobilier_Article_9_partage':'" + chkImmobilier_Article_9_partage.Checked +
                        "','chkImmobilier_Article_9_usufruit':'" + chkImmobilier_Article_9_usufruit.Checked +
                        "','txtImmobilier_Article_9_Valeur_PP':'" + txtImmobilier_Article_9_Valeur_PP.Text +
                        "','ddlImmobilier_Article_9':'" + ddlImmobilier_Article_9.SelectedValue +
                        "','txtImmobilier_Article_9_Valeur_NP':'" + txtImmobilier_Article_9_Valeur_NP.Text +
                        "','chkImmobilier_Article_10_partage':'" + chkImmobilier_Article_10_partage.Checked +
                        "','chkImmobilier_Article_10_usufruit':'" + chkImmobilier_Article_10_usufruit.Checked +
                        "','txtImmobilier_Article_10_Valeur_PP':'" + txtImmobilier_Article_10_Valeur_PP.Text +
                        "','ddlImmobilier_Article_10':'" + ddlImmobilier_Article_10.SelectedValue +
                        "','txtImmobilier_Article_10_Valeur_NP':'" + txtImmobilier_Article_10_Valeur_NP.Text +
                        "','ddl2':'" + ddl2.SelectedValue +
                        "','chkMobilier_Article_1_usufruit':'" + chkMobilier_Article_1_usufruit.Checked +
                        "','txtMobilier_Article_1_Valeur_PP':'" + txtMobilier_Article_1_Valeur_PP.Text +
                        "','ddlMobilier_Article_1':'" + ddlMobilier_Article_1.SelectedValue +
                        "','txtMobilier_Article_1_Valeur_NP':'" + txtMobilier_Article_1_Valeur_NP.Text +
                        "','chkMobilier_Article_2_usufruit':'" + chkMobilier_Article_2_usufruit.Checked +
                        "','txtMobilier_Article_2_Valeur_PP':'" + txtMobilier_Article_2_Valeur_PP.Text +
                        "','ddlMobilier_Article_2':'" + ddlMobilier_Article_2.SelectedValue +
                        "','txtMobilier_Article_2_Valeur_NP':'" + txtMobilier_Article_2_Valeur_NP.Text +
                        "','chkMobilier_Article_3_usufruit':'" + chkMobilier_Article_3_usufruit.Checked +
                        "','txtMobilier_Article_3_Valeur_PP':'" + txtMobilier_Article_3_Valeur_PP.Text +
                        "','ddlMobilier_Article_3':'" + ddlMobilier_Article_3.SelectedValue +
                        "','txtMobilier_Article_3_Valeur_NP':'" + txtMobilier_Article_3_Valeur_NP.Text +
                        "','chkMobilier_Article_4_usufruit':'" + chkMobilier_Article_4_usufruit.Checked +
                        "','txtMobilier_Article_4_Valeur_PP':'" + txtMobilier_Article_4_Valeur_PP.Text +
                        "','ddlMobilier_Article_4':'" + ddlMobilier_Article_4.SelectedValue +
                        "','txtMobilier_Article_4_Valeur_NP':'" + txtMobilier_Article_4_Valeur_NP.Text +
                        "','chkMobilier_Article_5_usufruit':'" + chkMobilier_Article_5_usufruit.Checked +
                        "','txtMobilier_Article_5_Valeur_PP':'" + txtMobilier_Article_5_Valeur_PP.Text +
                        "','ddlMobilier_Article_5':'" + ddlMobilier_Article_5.SelectedValue +
                        "','txtMobilier_Article_5_Valeur_NP':'" + txtMobilier_Article_5_Valeur_NP.Text +
                        "','chkMobilier_Article_6_usufruit':'" + chkMobilier_Article_6_usufruit.Checked +
                        "','txtMobilier_Article_6_Valeur_PP':'" + txtMobilier_Article_6_Valeur_PP.Text +
                        "','ddlMobilier_Article_6':'" + ddlMobilier_Article_6.SelectedValue +
                        "','txtMobilier_Article_6_Valeur_NP':'" + txtMobilier_Article_6_Valeur_NP.Text +
                        "','chkMobilier_Article_7_usufruit':'" + chkMobilier_Article_7_usufruit.Checked +
                        "','txtMobilier_Article_7_Valeur_PP':'" + txtMobilier_Article_7_Valeur_PP.Text +
                        "','ddlMobilier_Article_7':'" + ddlMobilier_Article_7.SelectedValue +
                        "','txtMobilier_Article_7_Valeur_NP':'" + txtMobilier_Article_7_Valeur_NP.Text +
                        "','chkMobilier_Article_8_usufruit':'" + chkMobilier_Article_8_usufruit.Checked +
                        "','txtMobilier_Article_8_Valeur_PP':'" + txtMobilier_Article_8_Valeur_PP.Text +
                        "','ddlMobilier_Article_8':'" + ddlMobilier_Article_8.SelectedValue +
                        "','txtMobilier_Article_8_Valeur_NP':'" + txtMobilier_Article_8_Valeur_NP.Text +
                        "','chkMobilier_Article_9_usufruit':'" + chkMobilier_Article_9_usufruit.Checked +
                        "','txtMobilier_Article_9_Valeur_PP':'" + txtMobilier_Article_9_Valeur_PP.Text +
                        "','ddlMobilier_Article_9':'" + ddlMobilier_Article_9.SelectedValue +
                        "','txtMobilier_Article_9_Valeur_NP':'" + txtMobilier_Article_9_Valeur_NP.Text +
                        "','chkMobilier_Article_10_usufruit':'" + chkMobilier_Article_10_usufruit.Checked +
                        "','txtMobilier_Article_10_Valeur_PP':'" + txtMobilier_Article_10_Valeur_PP.Text +
                        "','ddlMobilier_Article_10':'" + ddlMobilier_Article_10.SelectedValue +
                        "','txtMobilier_Article_10_Valeur_NP':'" + txtMobilier_Article_10_Valeur_NP.Text +
                        "','ddl3':'" + ddl3.SelectedValue +
                        "','txtPassif_Article_1_Valeur':'" + txtPassif_Article_1_Valeur.Text +
                        "','txtPassif_Article_2_Valeur':'" + txtPassif_Article_2_Valeur.Text +
                        "','txtPassif_Article_3_Valeur':'" + txtPassif_Article_3_Valeur.Text +
                        "','txtPassif_Article_4_Valeur':'" + txtPassif_Article_4_Valeur.Text +
                        "','txtPassif_Article_5_Valeur':'" + txtPassif_Article_5_Valeur.Text +
                        "','txtPassif_Article_6_Valeur':'" + txtPassif_Article_6_Valeur.Text +
                        "','txtPassif_Article_7_Valeur':'" + txtPassif_Article_7_Valeur.Text +
                        "','txtPassif_Article_8_Valeur':'" + txtPassif_Article_8_Valeur.Text +
                        "','txtPassif_Article_9_Valeur':'" + txtPassif_Article_9_Valeur.Text +
                        "','txtPassif_Article_10_Valeur':'" + txtPassif_Article_10_Valeur.Text +
                    "','chk1':'" + chk1.Checked +
                    "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                    "','txtDébours':'" + txtDébours.Text +
                    "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value;
                data += "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF12-1b", false, Session["CLIENT_ID"].TransformToInt());
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
                        "','chkImmobilier_Article_1_partage':'" + chkImmobilier_Article_1_partage.Checked +
                        "','chkImmobilier_Article_1_usufruit':'" + chkImmobilier_Article_1_usufruit.Checked +
                        "','txtImmobilier_Article_1_Valeur_PP':'" + txtImmobilier_Article_1_Valeur_PP.Text +
                        "','ddlImmobilier_Article_1':'" + ddlImmobilier_Article_1.SelectedValue +
                        "','txtImmobilier_Article_1_Valeur_NP':'" + txtImmobilier_Article_1_Valeur_NP.Text +
                        "','chkImmobilier_Article_2_partage':'" + chkImmobilier_Article_2_partage.Checked +
                        "','chkImmobilier_Article_2_usufruit':'" + chkImmobilier_Article_2_usufruit.Checked +
                        "','txtImmobilier_Article_2_Valeur_PP':'" + txtImmobilier_Article_2_Valeur_PP.Text +
                        "','ddlImmobilier_Article_2':'" + ddlImmobilier_Article_2.SelectedValue +
                        "','txtImmobilier_Article_2_Valeur_NP':'" + txtImmobilier_Article_2_Valeur_NP.Text +
                        "','chkImmobilier_Article_3_partage':'" + chkImmobilier_Article_3_partage.Checked +
                        "','chkImmobilier_Article_3_usufruit':'" + chkImmobilier_Article_3_usufruit.Checked +
                        "','txtImmobilier_Article_3_Valeur_PP':'" + txtImmobilier_Article_3_Valeur_PP.Text +
                        "','ddlImmobilier_Article_3':'" + ddlImmobilier_Article_3.SelectedValue +
                        "','txtImmobilier_Article_3_Valeur_NP':'" + txtImmobilier_Article_3_Valeur_NP.Text +
                        "','chkImmobilier_Article_4_partage':'" + chkImmobilier_Article_4_partage.Checked +
                        "','chkImmobilier_Article_4_usufruit':'" + chkImmobilier_Article_4_usufruit.Checked +
                        "','txtImmobilier_Article_4_Valeur_PP':'" + txtImmobilier_Article_4_Valeur_PP.Text +
                        "','ddlImmobilier_Article_4':'" + ddlImmobilier_Article_4.SelectedValue +
                        "','txtImmobilier_Article_4_Valeur_NP':'" + txtImmobilier_Article_4_Valeur_NP.Text +
                        "','chkImmobilier_Article_5_partage':'" + chkImmobilier_Article_5_partage.Checked +
                        "','chkImmobilier_Article_5_usufruit':'" + chkImmobilier_Article_5_usufruit.Checked +
                        "','txtImmobilier_Article_5_Valeur_PP':'" + txtImmobilier_Article_5_Valeur_PP.Text +
                        "','ddlImmobilier_Article_5':'" + ddlImmobilier_Article_5.SelectedValue +
                        "','txtImmobilier_Article_5_Valeur_NP':'" + txtImmobilier_Article_5_Valeur_NP.Text +
                        "','chkImmobilier_Article_6_partage':'" + chkImmobilier_Article_6_partage.Checked +
                        "','chkImmobilier_Article_6_usufruit':'" + chkImmobilier_Article_6_usufruit.Checked +
                        "','txtImmobilier_Article_6_Valeur_PP':'" + txtImmobilier_Article_6_Valeur_PP.Text +
                        "','ddlImmobilier_Article_6':'" + ddlImmobilier_Article_6.SelectedValue +
                        "','txtImmobilier_Article_6_Valeur_NP':'" + txtImmobilier_Article_6_Valeur_NP.Text +
                        "','chkImmobilier_Article_7_partage':'" + chkImmobilier_Article_7_partage.Checked +
                        "','chkImmobilier_Article_7_usufruit':'" + chkImmobilier_Article_7_usufruit.Checked +
                        "','txtImmobilier_Article_7_Valeur_PP':'" + txtImmobilier_Article_7_Valeur_PP.Text +
                        "','ddlImmobilier_Article_7':'" + ddlImmobilier_Article_7.SelectedValue +
                        "','txtImmobilier_Article_7_Valeur_NP':'" + txtImmobilier_Article_7_Valeur_NP.Text +
                        "','chkImmobilier_Article_8_partage':'" + chkImmobilier_Article_8_partage.Checked +
                        "','chkImmobilier_Article_8_usufruit':'" + chkImmobilier_Article_8_usufruit.Checked +
                        "','txtImmobilier_Article_8_Valeur_PP':'" + txtImmobilier_Article_8_Valeur_PP.Text +
                        "','ddlImmobilier_Article_8':'" + ddlImmobilier_Article_8.SelectedValue +
                        "','txtImmobilier_Article_8_Valeur_NP':'" + txtImmobilier_Article_8_Valeur_NP.Text +
                        "','chkImmobilier_Article_9_partage':'" + chkImmobilier_Article_9_partage.Checked +
                        "','chkImmobilier_Article_9_usufruit':'" + chkImmobilier_Article_9_usufruit.Checked +
                        "','txtImmobilier_Article_9_Valeur_PP':'" + txtImmobilier_Article_9_Valeur_PP.Text +
                        "','ddlImmobilier_Article_9':'" + ddlImmobilier_Article_9.SelectedValue +
                        "','txtImmobilier_Article_9_Valeur_NP':'" + txtImmobilier_Article_9_Valeur_NP.Text +
                        "','chkImmobilier_Article_10_partage':'" + chkImmobilier_Article_10_partage.Checked +
                        "','chkImmobilier_Article_10_usufruit':'" + chkImmobilier_Article_10_usufruit.Checked +
                        "','txtImmobilier_Article_10_Valeur_PP':'" + txtImmobilier_Article_10_Valeur_PP.Text +
                        "','ddlImmobilier_Article_10':'" + ddlImmobilier_Article_10.SelectedValue +
                        "','txtImmobilier_Article_10_Valeur_NP':'" + txtImmobilier_Article_10_Valeur_NP.Text +
                        "','ddl2':'" + ddl2.SelectedValue +
                        "','chkMobilier_Article_1_usufruit':'" + chkMobilier_Article_1_usufruit.Checked +
                        "','txtMobilier_Article_1_Valeur_PP':'" + txtMobilier_Article_1_Valeur_PP.Text +
                        "','ddlMobilier_Article_1':'" + ddlMobilier_Article_1.SelectedValue +
                        "','txtMobilier_Article_1_Valeur_NP':'" + txtMobilier_Article_1_Valeur_NP.Text +
                        "','chkMobilier_Article_2_usufruit':'" + chkMobilier_Article_2_usufruit.Checked +
                        "','txtMobilier_Article_2_Valeur_PP':'" + txtMobilier_Article_2_Valeur_PP.Text +
                        "','ddlMobilier_Article_2':'" + ddlMobilier_Article_2.SelectedValue +
                        "','txtMobilier_Article_2_Valeur_NP':'" + txtMobilier_Article_2_Valeur_NP.Text +
                        "','chkMobilier_Article_3_usufruit':'" + chkMobilier_Article_3_usufruit.Checked +
                        "','txtMobilier_Article_3_Valeur_PP':'" + txtMobilier_Article_3_Valeur_PP.Text +
                        "','ddlMobilier_Article_3':'" + ddlMobilier_Article_3.SelectedValue +
                        "','txtMobilier_Article_3_Valeur_NP':'" + txtMobilier_Article_3_Valeur_NP.Text +
                        "','chkMobilier_Article_4_usufruit':'" + chkMobilier_Article_4_usufruit.Checked +
                        "','txtMobilier_Article_4_Valeur_PP':'" + txtMobilier_Article_4_Valeur_PP.Text +
                        "','ddlMobilier_Article_4':'" + ddlMobilier_Article_4.SelectedValue +
                        "','txtMobilier_Article_4_Valeur_NP':'" + txtMobilier_Article_4_Valeur_NP.Text +
                        "','chkMobilier_Article_5_usufruit':'" + chkMobilier_Article_5_usufruit.Checked +
                        "','txtMobilier_Article_5_Valeur_PP':'" + txtMobilier_Article_5_Valeur_PP.Text +
                        "','ddlMobilier_Article_5':'" + ddlMobilier_Article_5.SelectedValue +
                        "','txtMobilier_Article_5_Valeur_NP':'" + txtMobilier_Article_5_Valeur_NP.Text +
                        "','chkMobilier_Article_6_usufruit':'" + chkMobilier_Article_6_usufruit.Checked +
                        "','txtMobilier_Article_6_Valeur_PP':'" + txtMobilier_Article_6_Valeur_PP.Text +
                        "','ddlMobilier_Article_6':'" + ddlMobilier_Article_6.SelectedValue +
                        "','txtMobilier_Article_6_Valeur_NP':'" + txtMobilier_Article_6_Valeur_NP.Text +
                        "','chkMobilier_Article_7_usufruit':'" + chkMobilier_Article_7_usufruit.Checked +
                        "','txtMobilier_Article_7_Valeur_PP':'" + txtMobilier_Article_7_Valeur_PP.Text +
                        "','ddlMobilier_Article_7':'" + ddlMobilier_Article_7.SelectedValue +
                        "','txtMobilier_Article_7_Valeur_NP':'" + txtMobilier_Article_7_Valeur_NP.Text +
                        "','chkMobilier_Article_8_usufruit':'" + chkMobilier_Article_8_usufruit.Checked +
                        "','txtMobilier_Article_8_Valeur_PP':'" + txtMobilier_Article_8_Valeur_PP.Text +
                        "','ddlMobilier_Article_8':'" + ddlMobilier_Article_8.SelectedValue +
                        "','txtMobilier_Article_8_Valeur_NP':'" + txtMobilier_Article_8_Valeur_NP.Text +
                        "','chkMobilier_Article_9_usufruit':'" + chkMobilier_Article_9_usufruit.Checked +
                        "','txtMobilier_Article_9_Valeur_PP':'" + txtMobilier_Article_9_Valeur_PP.Text +
                        "','ddlMobilier_Article_9':'" + ddlMobilier_Article_9.SelectedValue +
                        "','txtMobilier_Article_9_Valeur_NP':'" + txtMobilier_Article_9_Valeur_NP.Text +
                        "','chkMobilier_Article_10_usufruit':'" + chkMobilier_Article_10_usufruit.Checked +
                        "','txtMobilier_Article_10_Valeur_PP':'" + txtMobilier_Article_10_Valeur_PP.Text +
                        "','ddlMobilier_Article_10':'" + ddlMobilier_Article_10.SelectedValue +
                        "','txtMobilier_Article_10_Valeur_NP':'" + txtMobilier_Article_10_Valeur_NP.Text +
                        "','ddl3':'" + ddl3.SelectedValue +
                        "','txtPassif_Article_1_Valeur':'" + txtPassif_Article_1_Valeur.Text +
                        "','txtPassif_Article_2_Valeur':'" + txtPassif_Article_2_Valeur.Text +
                        "','txtPassif_Article_3_Valeur':'" + txtPassif_Article_3_Valeur.Text +
                        "','txtPassif_Article_4_Valeur':'" + txtPassif_Article_4_Valeur.Text +
                        "','txtPassif_Article_5_Valeur':'" + txtPassif_Article_5_Valeur.Text +
                        "','txtPassif_Article_6_Valeur':'" + txtPassif_Article_6_Valeur.Text +
                        "','txtPassif_Article_7_Valeur':'" + txtPassif_Article_7_Valeur.Text +
                        "','txtPassif_Article_8_Valeur':'" + txtPassif_Article_8_Valeur.Text +
                        "','txtPassif_Article_9_Valeur':'" + txtPassif_Article_9_Valeur.Text +
                        "','txtPassif_Article_10_Valeur':'" + txtPassif_Article_10_Valeur.Text +
                    "','chk1':'" + chk1.Checked +
                    "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                    "','txtDébours':'" + txtDébours.Text +
                    "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value;
                data += "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF12-1b", false, Session["CLIENT_ID"].TransformToInt());
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
                PdfHelper.GeneratePdf("BF12-1b", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF12-1b", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
}