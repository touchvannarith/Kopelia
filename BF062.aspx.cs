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
    public partial class BF062 : Page
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
                var data = new JavaScriptSerializer().Deserialize<DataModelBF062>(obj.Value);
                txtDossier.Text = data.txtDossier;
                txtDateSignature.Text = data.txtDateDeSignature;
                txtRedacteur.Text = data.txtRedacteur;
                txtDate_de_la_cession.Text = data.txtDate_de_la_cession;
                ddl1.SelectedValue = data.ddl1;
                txtZone01.Text = data.txtZone01;
                txtZone02.Text = data.txtZone02;
                txtZone03.Text = data.txtZone03;
                txtZone04.Text = data.txtZone04;
                txtZone05.Text = data.txtZone05;
                txtZone06.Text = data.txtZone06;
                chk1.Checked = data.chk1.TransformToBoolean();
                chk2.Checked = data.chk2.TransformToBoolean();
                chk3.Checked = data.chk3.TransformToBoolean();
                txtDébours.Text = data.txtDébours;
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
                new RangeCoordinates() { Row = 6, Column = 4, Height = 11, Width = 1 }, new object[]
                {
                        new object[] {ddl1.SelectedValue},
                        new object[] {txtZone01.Text},
                        new object[] {txtZone02.Text},
                        new object[] {txtZone03.Text},
                        new object[] {chk1.Checked.TransformToBooleanFr()},
                        new object[] {chk2.Checked.TransformToBooleanFr()},
                        new object[] {chk3.Checked.TransformToBooleanFr()},
                        new object[] {txtZone04.Text},
                        new object[] {txtZone05.Text},
                        new object[] {txtZone06.Text},
                        new object[]{txtDébours.Text}
                });
        }

        private void SetValues(DataTable dt)
        {
            const string show = "1";
            lblF102.InnerText = dt.Rows[0][(int)ColumnBF062.Col1].ToString();
            lblF103.InnerText = dt.Rows[1][(int)ColumnBF062.Col1].ToString();
            lblF104.InnerText = dt.Rows[2][(int)ColumnBF062.Col1].ToString();
            lblF105.InnerText = dt.Rows[3][(int)ColumnBF062.Col1].ToString();

            lblF107.InnerText = dt.Rows[5][(int)ColumnBF062.Col1].ToString();
            lblF108.InnerText = dt.Rows[6][(int)ColumnBF062.Col1].ToString();
            lblF109.InnerText = dt.Rows[7][(int)ColumnBF062.Col1].ToString();

            lblF111.InnerText = dt.Rows[9][(int)ColumnBF062.Col1].ToString();

            div114.Visible = dt.Rows[12][(int)ColumnBF062.Col3].ToString() == show;
            lblF116.InnerText = dt.Rows[14][(int)ColumnBF062.Col1].ToString();
            lblF116.Visible = dt.Rows[14][(int)ColumnBF062.Col3].ToString() == show;
            lblF117.InnerText = dt.Rows[15][(int)ColumnBF062.Col1].ToString();
            lblF117.Visible = dt.Rows[15][(int)ColumnBF062.Col3].ToString() == show;
            lblF118.InnerText = dt.Rows[16][(int)ColumnBF062.Col1].ToString();
            lblF118.Visible = dt.Rows[16][(int)ColumnBF062.Col3].ToString() == show;
            lblF120.InnerText = dt.Rows[18][(int)ColumnBF062.Col1].ToString();

            div121.Visible = dt.Rows[19][(int)ColumnBF062.Col3].ToString() == show;
            lblF121.InnerText = dt.Rows[19][(int)ColumnBF062.Col1].ToString();
            lblF122.InnerText = dt.Rows[20][(int)ColumnBF062.Col1].ToString();

            div123.Visible = dt.Rows[21][(int)ColumnBF062.Col3].ToString() == show;
            lblF123.InnerText = dt.Rows[21][(int)ColumnBF062.Col1].ToString();

            div124.Visible = dt.Rows[22][(int)ColumnBF062.Col3].ToString() == show;
            lblF124.InnerText = dt.Rows[22][(int)ColumnBF062.Col1].ToString();

            div125.Visible = dt.Rows[22][(int)ColumnBF062.Col3].ToString() == show;
            lblF125.InnerText = dt.Rows[23][(int)ColumnBF062.Col1].ToString();

            div126.Visible = dt.Rows[24][(int)ColumnBF062.Col3].ToString() == show;
            lblF126.InnerText = dt.Rows[24][(int)ColumnBF062.Col1].ToString();

            div127.Visible = dt.Rows[25][(int)ColumnBF062.Col3].ToString() == show;
            lblF127.InnerText = dt.Rows[25][(int)ColumnBF062.Col1].ToString();

            var html = "";
            //Total des droits et frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Total des droits et frais</font></b></td></tr>";
            html += "<tr><td colspan='5' bgcolor='#304F73'></td></tr>";
            html += "<tr><td colspan='5'><br/><br/></td></tr>";
            html += string.Format(
                    "<tr><td align='right'>Convention d'honoraires :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF102.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Débours :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF103.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Trésor public :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF104.InnerText);
            html += string.Format(
                    "<tr><td align='right'>Montant des frais :</td><td align='right'>{0}</td><td align='right' colspan='3'></td></tr>",
                    lblF105.InnerText);
            html += "</table>";
            html += string.Format("<div><br/><br/></div><div align='right'><img width='50%' src='{0}' id='msg' /></div>",
                Request.PhysicalApplicationPath + "tmp\\BF062\\chart.png");
            //Détail des frais
            html += "<table border-collapse='collapse' border='0' width='100%' size='1'>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "<tr><td bgcolor='#304F73' align='center' colspan='5'><b><font color='#FFFFFF' size='11px'>Détail des frais</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            //Emoluments du notaire
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Convention d`honoraires</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Montant HT :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF107.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>TVA :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF108.InnerText);
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Total TTC :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF109.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Débours
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += string.Format(
                    "<tr><td colspan='2' align='right'>Débours :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF111.InnerText);
            html += "<tr><td colspan='4'></td></tr>";
            //Tresor public
            html += "<tr><td bgcolor='#01ABE4' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Tresor public</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            if (dt.Rows[14][(int)ColumnBF062.Col3].ToString() == show)
            {
                html += string.Format(
                    "<tr><td colspan='4' align='center'>{0}</td></tr>",
                    lblF116.InnerText);
            }
            if (dt.Rows[15][(int)ColumnBF062.Col3].ToString() == show)
            {
                html += string.Format(
                    "<tr><td colspan='4' align='center'>{0}</td></tr>",
                    lblF117.InnerText);
            }
            if (dt.Rows[16][(int)ColumnBF062.Col3].ToString() == show)
            {
                html += string.Format(
                    "<tr><td colspan='4' align='center'>{0}</td></tr>",
                    lblF118.InnerText);
            }
            if (dt.Rows[12][(int)ColumnBF062.Col3].ToString() == show)
            {
                html += string.Format(
                    "<tr><td colspan='2' align='right'>Rappel base taxable brute :</td><td align='right'>{0}</td><td></td></tr>",
                    lblF120.InnerText);
                if (dt.Rows[19][(int)ColumnBF062.Col3].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Abattement prévu à l'art. 726 du CGI :</td><td align='right'>{0}</td><td></td></tr>",
                        lblF121.InnerText);
                    html += string.Format(
                        "<tr><td colspan='4' align='center'>{0}</td></tr>",
                        lblF122.InnerText);
                }
                if (dt.Rows[21][(int)ColumnBF062.Col3].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Abattement maximal disponible Art.732 ter :</td><td align='right'>{0}</td><td></td></tr>",
                        lblF123.InnerText);
                }
                if (dt.Rows[22][(int)ColumnBF062.Col3].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Assiette de perception :</td><td align='right'>{0}</td><td></td></tr>",
                        lblF124.InnerText);
                }
                if (dt.Rows[22][(int)ColumnBF062.Col3].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Taux :</td><td align='right'>{0}</td><td></td></tr>",
                        lblF125.InnerText);
                }
                if (dt.Rows[24][(int)ColumnBF062.Col3].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Montant de la taxe :</td><td align='right'>{0}</td><td></td></tr>",
                        lblF126.InnerText);
                }
                if (dt.Rows[25][(int)ColumnBF062.Col3].ToString() == show)
                {
                    html += string.Format(
                        "<tr><td colspan='2' align='right'>Minimum de perception :</td><td align='right'>{0}</td><td></td></tr>",
                        lblF127.InnerText);
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
                _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "BF062-ONLINE.xlsm", out _status);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetRange();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                var data = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 101, 4, 26, 4, out _status);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                SetValues(data);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;




                CalculezMaintenantDataAccess.Save(Session["CLIENT_ID"].TransformToInt(), "BF062");
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
                           "','txtDate_de_la_cession':'" + txtDate_de_la_cession.Text + "','ddl1':'" +
                           ddl1.SelectedValue + "','txtZone01':'" + txtZone01.Text + "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text + "','chk1':'" + chk1.Checked + "','chk2':'" +
                           chk2.Checked + "','chk3':'" + chk3.Checked +
                           "','txtZone04':'" + txtZone04.Text + "','txtZone05':'" + txtZone05.Text + "','txtZone06':'" +
                           txtZone06.Text +
                           "','txtDébours':'" + txtDébours.Text + "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "BF062", false, Session["CLIENT_ID"].TransformToInt());
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
                           "','txtDate_de_la_cession':'" + txtDate_de_la_cession.Text + "','ddl1':'" +
                           ddl1.SelectedValue + "','txtZone01':'" + txtZone01.Text + "','txtZone02':'" + txtZone02.Text +
                           "','txtZone03':'" + txtZone03.Text + "','chk1':'" + chk1.Checked + "','chk2':'" +
                           chk2.Checked + "','chk3':'" + chk3.Checked +
                           "','txtZone04':'" + txtZone04.Text + "','txtZone05':'" + txtZone05.Text + "','txtZone06':'" +
                           txtZone06.Text +
                           "','txtDébours':'" + txtDébours.Text + "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "BF062", false, Session["CLIENT_ID"].TransformToInt());
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
            var filename = PdfHelper.GeneratePdf("BF062", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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
            var filename = PdfHelper.GeneratePdf("BF062", hdPiechart.Value, "", 1, hdSaisie.Value, hdResult.Value, out string pdf);
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

    enum ColumnBF062
    {
        Col1 = 1,
        Col2 = 2,
        Col3 = 3
    }

    class DataModelBF062
    {
        public string txtDossier { get; set; }
        public string txtDateDeSignature { get; set; }
        public string txtRedacteur { get; set; }
        public string txtDate_de_la_cession { get; set; }
        public string ddl1 { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtZone05 { get; set; }
        public string txtZone06 { get; set; }
        public string chk1 { get; set; }
        public string chk2 { get; set; }
        public string chk3 { get; set; }
        public string txtDébours { get; set; }
    }
}