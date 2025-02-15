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
    public partial class BF08 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF08>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                ddl1.SelectedValue = data.ddl1;
                ddl2.SelectedValue = data.ddl2;
                txtZone01.Text = data.txtZone01;
                txtZone02.Text = data.txtZone02;
                txtZone03.Text = data.txtZone03;
                txtZone04.Text = data.txtZone04;
                txtZone05.Text = data.txtZone05;
                txtZone06.Text = data.txtZone06;
                txtZone07.Text = data.txtZone07;
                chk1.Checked = data.chk1.TransformToBoolean();
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
            var ddl1Value = ddl1.SelectedValue;
            var range = new RangeCoordinates
            {
                Row = 5,
                Column = 4,
                Height = 13,
                Width = 1
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                        new object[] {ddl1Value},
                        new object[] {ddl1Value == "1" ? ddl2.SelectedValue : "0"},
                        new object[] {txtZone01.Text},
                        new object[] {txtZone02.Text},
                        new object[] {txtZone03.Text},
                        new object[] {txtZone04.Text},
                        new object[] {chk1.Checked.TransformToBooleanFr()},
                        new object[] {txtZone05.Text},
                        new object[] {txtZone06.Text},
                        new object[] {txtZone07.Text},
                        new object[]{txtEmolument_de_formalités_HT.Text},
                        new object[]{txtDébours.Text},
                        new object[]{hdUtilisation_du_futur_tarif.Value}
                });
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            lblEmoluments_HT_du_notaire.InnerText = dt.Rows[0][(int)ColumnBF08.Col1].ToString();
            lblDébours_et_émoluments.InnerText = dt.Rows[1][(int)ColumnBF08.Col1].ToString();
            lblTrésor_public.InnerText = dt.Rows[2][(int)ColumnBF08.Col1].ToString();
            lblMontant_des_frais.InnerText = dt.Rows[3][(int)ColumnBF08.Col1].ToString();

            lblRow11.InnerText = dt.Rows[9][(int)ColumnBF08.Col1].ToString();
            lblRow12.InnerText = dt.Rows[9][(int)ColumnBF08.Col2].ToString();
            lblRow13.InnerText = dt.Rows[9][(int)ColumnBF08.Col3].ToString();
            div1.Visible = dt.Rows[9][(int)ColumnBF08.Col4].ToString() == show;
            lblRow21.InnerText = dt.Rows[10][(int)ColumnBF08.Col1].ToString();
            lblRow22.InnerText = dt.Rows[10][(int)ColumnBF08.Col2].ToString();
            lblRow23.InnerText = dt.Rows[10][(int)ColumnBF08.Col3].ToString();
            div2.Visible = dt.Rows[10][(int)ColumnBF08.Col4].ToString() == show;
            lblRow31.InnerText = dt.Rows[11][(int)ColumnBF08.Col1].ToString();
            lblRow32.InnerText = dt.Rows[11][(int)ColumnBF08.Col2].ToString();
            lblRow33.InnerText = dt.Rows[11][(int)ColumnBF08.Col3].ToString();
            div3.Visible = dt.Rows[11][(int)ColumnBF08.Col4].ToString() == show;
            lblRow41.InnerText = dt.Rows[12][(int)ColumnBF08.Col1].ToString();
            lblRow42.InnerText = dt.Rows[12][(int)ColumnBF08.Col2].ToString();
            lblRow43.InnerText = dt.Rows[12][(int)ColumnBF08.Col3].ToString();
            div4.Visible = dt.Rows[12][(int)ColumnBF08.Col4].ToString() == show;
            lblTotal.InnerText = dt.Rows[14][(int)ColumnBF08.Col2].ToString();
            divTotal.Visible = dt.Rows[14][(int)ColumnBF08.Col4].ToString() == show;
            lblTOTAL_Hors_TVA.InnerText = dt.Rows[16][(int)ColumnBF08.Col1].ToString();
            lblEmoluments_de_caution_HT.InnerText = dt.Rows[17][(int)ColumnBF08.Col1].ToString();
            //F120.InnerText = dt.Rows[18][(int)ColumnBF08.Col1].ToString();
            lblTotal_HT_des_émoluments.InnerText = dt.Rows[19][(int)ColumnBF08.Col1].ToString();

            var html = "";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Emoluments HT du notaire :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblEmoluments_HT_du_notaire.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblDébours_et_émoluments.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblTrésor_public.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblMontant_des_frais.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF08\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            lblEmolumentTitle.InnerText = ddl1.SelectedValue == "1"
                ? "Emoluments du notaire - C.com. Art. A 444-130"
                : "Emoluments du notaire - C.com. Art. A 444-131";
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>" + lblEmolumentTitle.InnerText + "</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            if (dt.Rows[9][(int)ColumnBF08.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow11.InnerText, lblRow12.InnerText, lblRow13.InnerText);
            }
            if (dt.Rows[10][(int)ColumnBF08.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow21.InnerText, lblRow22.InnerText, lblRow23.InnerText);
            }
            if (dt.Rows[11][(int)ColumnBF08.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow31.InnerText, lblRow32.InnerText, lblRow33.InnerText);
            }
            if (dt.Rows[12][(int)ColumnBF08.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td><td></td></tr>",
                    lblRow41.InnerText, lblRow42.InnerText, lblRow43.InnerText);
            }
            if (dt.Rows[14][(int)ColumnBF08.Col4].ToString() == show)
            {
                html += string.Format(
                    "<tr><td align='right'>Total :</td><td align='right'>{0}</td><td colspan='2'></td></tr>",
                    lblTotal.InnerText);
            }
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TOTAL Hors T.V.A :</td><td align='right'>{0}</td><td></td></tr>",
                    lblTOTAL_Hors_TVA.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de caution HT :</td><td align='right'>{0}</td><td></td></tr>",
                    lblEmoluments_de_caution_HT.InnerText);
            //html += string.Format(
            //        "<tr><td colspan='2' align='right'>Emoluments de formalités :</td><td align='right'>{0}</td><td></td></tr>",
            //        F120.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments réglementés :</td><td align='right'>{0}</td><td></td></tr>",
                    lblTotal_HT_des_émoluments.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Récapitulatif et calcul de la TVA
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Récapitulatif et calcul de la TVA</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            F124.InnerText = dt.Rows[22][(int)ColumnBF08.Col1].ToString();
            F125.InnerText = dt.Rows[23][(int)ColumnBF08.Col1].ToString();
            F126.InnerText = dt.Rows[24][(int)ColumnBF08.Col1].ToString();
            F127.InnerText = dt.Rows[25][(int)ColumnBF08.Col1].ToString();
            F128.InnerText = dt.Rows[26][(int)ColumnBF08.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT des émoluments du notaire :</td><td align='right'>{0}</td><td></td></tr>",
                    F124.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Emoluments de formalités :</td><td align='right'>{0}</td><td></td></tr>",
                    F125.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total HT :</td><td align='right'>{0}</td><td></td></tr>",
                    F126.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    F127.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    F128.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            F130.InnerText = dt.Rows[28][(int)ColumnBF08.Col1].ToString();
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    F130.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Trésor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Trésor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr><td colspan='4' align='left'>Contribution pour la sécurité immobilière (art. 879 du CGI)</td></tr>";
            row134.Visible = dt.Rows[32][(int)ColumnBF08.Col4].ToString() == show;
            if (dt.Rows[32][(int)ColumnBF08.Col4].ToString() == show)
            {
                F134.InnerText = dt.Rows[32][(int)ColumnBF08.Col1].ToString();
                G134.InnerText = dt.Rows[32][(int)ColumnBF08.Col2].ToString();
                H134.InnerText = dt.Rows[32][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td></td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F134.InnerText, G134.InnerText, G134.InnerText);
            }
            row135.Visible = dt.Rows[33][(int)ColumnBF08.Col4].ToString() == show;
            if (dt.Rows[33][(int)ColumnBF08.Col4].ToString() == show)
            {
                H135.InnerText = dt.Rows[33][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                    H135.InnerText);
            }
            row138.Visible = dt.Rows[36][(int)ColumnBF08.Col4].ToString() == show;
            if (dt.Rows[36][(int)ColumnBF08.Col4].ToString() == show)
            {
                html += "<tr><td colspan='4' align='left'>Fiscalité immobilière</td></tr>";
                F139.InnerText = dt.Rows[37][(int)ColumnBF08.Col1].ToString();
                G139.InnerText = dt.Rows[37][(int)ColumnBF08.Col2].ToString();
                H139.InnerText = dt.Rows[37][(int)ColumnBF08.Col3].ToString();
                F140.InnerText = dt.Rows[38][(int)ColumnBF08.Col1].ToString();
                G140.InnerText = dt.Rows[38][(int)ColumnBF08.Col2].ToString();
                H140.InnerText = dt.Rows[38][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td>Taxe départementale :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F139.InnerText, G139.InnerText, H139.InnerText);
                html += string.Format(
                    "<tr><td>Prélèvement de l'Etat sur taxe départementale :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F140.InnerText, G140.InnerText, H140.InnerText);
                row141.Visible = dt.Rows[39][(int)ColumnBF08.Col4].ToString() == show;
                if (dt.Rows[39][(int)ColumnBF08.Col4].ToString() == show)
                {
                    H141.InnerText = dt.Rows[39][(int)ColumnBF08.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='3' align='right'>Minimum de perception :</td><td align='right'>{0}</td></tr>",
                        H141.InnerText);
                }
                row142.Visible = dt.Rows[40][(int)ColumnBF08.Col4].ToString() == show;
                if (dt.Rows[40][(int)ColumnBF08.Col4].ToString() == show)
                {
                    H142.InnerText = dt.Rows[40][(int)ColumnBF08.Col3].ToString();
                    html += string.Format(
                        "<tr><td colspan='3' align='right'>Total :</td><td align='right'>{0}</td></tr>",
                        H142.InnerText);
                }
            }
            row144.Visible = dt.Rows[42][(int)ColumnBF08.Col4].ToString() == show;
            if (dt.Rows[42][(int)ColumnBF08.Col4].ToString() == show)
            {
                F144.InnerText = dt.Rows[42][(int)ColumnBF08.Col1].ToString();
                html += string.Format(
                    "<tr><td colspan='3' align='right'>Assiette de perception :</td><td align='right'>{0}</td></tr>",
                    F144.InnerText);
                F146.InnerText = dt.Rows[44][(int)ColumnBF08.Col1].ToString();
                G146.InnerText = dt.Rows[44][(int)ColumnBF08.Col2].ToString();
                H146.InnerText = dt.Rows[44][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td>Jusqu'à 23.000€ :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F146.InnerText, G146.InnerText, H146.InnerText);
                F147.InnerText = dt.Rows[45][(int)ColumnBF08.Col1].ToString();
                G147.InnerText = dt.Rows[45][(int)ColumnBF08.Col2].ToString();
                H147.InnerText = dt.Rows[45][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td>De 23.000 à 107.000€ :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F147.InnerText, G147.InnerText, H147.InnerText);
                F148.InnerText = dt.Rows[46][(int)ColumnBF08.Col1].ToString();
                G148.InnerText = dt.Rows[46][(int)ColumnBF08.Col2].ToString();
                H148.InnerText = dt.Rows[46][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td>De 107.000 à 200.000€ :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F148.InnerText, G148.InnerText, H148.InnerText);
                F149.InnerText = dt.Rows[47][(int)ColumnBF08.Col1].ToString();
                G149.InnerText = dt.Rows[47][(int)ColumnBF08.Col2].ToString();
                H149.InnerText = dt.Rows[47][(int)ColumnBF08.Col3].ToString();
                html += string.Format(
                    "<tr><td>Supérieur à 200.000€ :</td><td align='right'>{0}    sur</td><td align='right'>{1}    =</td><td align='right'>{2}</td></tr>",
                    F149.InnerText, G149.InnerText, H149.InnerText);
                row151.Visible = dt.Rows[49][(int)ColumnBF08.Col4].ToString() == show;
                if (dt.Rows[49][(int)ColumnBF08.Col4].ToString() == show)
                {
                    F151.InnerText = dt.Rows[49][(int)ColumnBF08.Col1].ToString();
                    html += string.Format(
                        "<tr><td colspan='3' align='right'>Minimum de perception :</td><td align='right'>{0}</td></tr>",
                        F151.InnerText);
                }
                row152.Visible = dt.Rows[50][(int)ColumnBF08.Col4].ToString() == show;
                if (dt.Rows[50][(int)ColumnBF08.Col4].ToString() == show)
                {
                    F152.InnerText = dt.Rows[50][(int)ColumnBF08.Col1].ToString();
                    html += string.Format(
                        "<tr><td colspan='3' align='right'>Total :</td><td align='right'>{0}</td></tr>",
                        F152.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF08-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 2, 52, 7, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF08");
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
                           "','ddl1':'" + ddl1.SelectedValue + "','ddl2':'" + ddl2.SelectedValue + "','txtZone01':'" +
                           txtZone01.Text + "','txtZone02':'" + txtZone02.Text + "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text + "','chk1':'" + chk1.Checked + "','txtZone05':'" +
                           txtZone05.Text + "','txtZone06':'" + txtZone06.Text + "','txtZone07':'" + txtZone07.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF08", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','ddl1':'" + ddl1.SelectedValue + "','ddl2':'" + ddl2.SelectedValue + "','txtZone01':'" +
                           txtZone01.Text + "','txtZone02':'" + txtZone02.Text + "','txtZone03':'" + txtZone03.Text +
                           "','txtZone04':'" + txtZone04.Text + "','chk1':'" + chk1.Checked + "','txtZone05':'" +
                           txtZone05.Text + "','txtZone06':'" + txtZone06.Text + "','txtZone07':'" + txtZone07.Text +
                           "','txtEmolument_de_formalités_HT':'" + txtEmolument_de_formalités_HT.Text +
                           "','txtDébours':'" + txtDébours.Text +
                           "','chkUtilisation_du_futur_tarif':'" + hdUtilisation_du_futur_tarif.Value + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF08", false, Session["CLIENT_ID"].TransformToInt());
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
            var filename = PdfHelper.GeneratePdf("BF08", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
            //Page.ClientScript.RegisterStartupScript(GetType(), "scrollToElement", "scrollToElement('#btnPrint',15);", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.AddHeader("Content-Length", pdf.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(pdf);
            Response.End();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var filename = PdfHelper.GeneratePdf("BF08", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF08
    {
        Col1 = 3,
        Col2 = 4,
        Col3 = 5,
        Col4 = 6,
    }

    class DataModelBF08
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string ddl1 { get; set; }
        public string ddl2 { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtZone05 { get; set; }
        public string txtZone06 { get; set; }
        public string txtZone07 { get; set; }
        public string chk1 { get; set; }
        public string txtEmolument_de_formalités_HT { get; set; }
        public string txtDébours { get; set; }
        public string chkUtilisation_du_futur_tarif { get; set; }
    }
}