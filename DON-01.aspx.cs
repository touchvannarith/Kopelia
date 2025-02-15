using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotaliaOnline.DataAccess;
using NotaliaOnline.Helpers;
using NotaliaOnline.Properties;
using NotaliaOnline.WebReference;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotaliaOnline
{
    public partial class DON_01 : Page
    {
        private static string _sessionId;
        private static ExcelService _excelService;
        private static Status[] _status;

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
                    postback.Value = "true";
                    if (FillValues(Request.QueryString["Voir"].TransformToInt(), Session["CLIENT_ID"].TransformToInt()))
                    {
                        btnSynthese_Click(sender, e);
                    }
                }
            }

        }

        #region private/public method

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
                hdJsonDeterminationDesDonataires.Value = json["inputForm1"].ToString();
                hdJsonDeterminationDesBiens.Value = json["inputForm2"].ToString();
                hdJsonRappelDeDonationsAnterieures.Value = json["inputForm3"].ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable ConvertRangeDataToDataTable(object[] data, RangeCoordinates range)
        {
            var dt = new DataTable();
            for (var i = 0; i < range.Width; i++)
            {
                dt.Columns.Add(new DataColumn());
            }

            foreach (var row in data)
            {
                var newrow = dt.NewRow();
                var objects = (object[])row;
                for (var i = 0; i < objects.Count(); i++)
                {
                    if (objects[i] == null) continue;

                    newrow[i] = objects[i].ToString();
                }
                dt.Rows.Add(newrow);
            }
            return dt;
        }

        private static void SetRangeValueToExcelService(string strInputForm1, string strInputForm2, string strInputForm3, out ExcelService _excelService, out string _sessionId)
        {
            _excelService = ExcelServiceHelper.ExcelServiceProvider();
            _sessionId = ExcelServiceHelper.GetSessionId(_excelService, "http://80.14.175.214/Documents%20partages/DONATION01.xlsm", out _status);

            var js = new JavaScriptSerializer();
            var inputForm1 = js.Deserialize<DeterminationDesDonatairesModel>(strInputForm1);
            var inputForm2 = js.Deserialize<DeterminationDesBiensModel>(strInputForm2);
            var range = new RangeCoordinates
            {
                Row = 10,
                Column = 1,
                Height = 82,
                Width = 9
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                    new object[] {"", "", inputForm1.txtZone00, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm1.ddl1, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm1.txtZone01a, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm1.txtZone01b, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm1.ddl3, "", "", "", "", "", ""},
                    new object[] {"DONATAIRES", "", "", "", "", "", "", "", ""},
                    new object[] {1, "*.a", inputForm1.arrDonatairesDeDegre[0].txtZone01, inputForm1.arrDonatairesDeDegre[0].cb1, inputForm1.arrDonatairesDeDegre[0].cb2, inputForm1.arrDonatairesDeDegre[0].ddl4, inputForm1.arrDonatairesDeDegre[0].ddl5, inputForm1.arrNatureDeLaDonation[0].txtZone02, inputForm1.arrNatureDeLaDonation[0].txtZone03 },
                    new object[] {2, "*.b", inputForm1.arrDonatairesDeDegre[1].txtZone01, inputForm1.arrDonatairesDeDegre[1].cb1, inputForm1.arrDonatairesDeDegre[1].cb2, inputForm1.arrDonatairesDeDegre[1].ddl4, inputForm1.arrDonatairesDeDegre[1].ddl5, inputForm1.arrNatureDeLaDonation[1].txtZone02, inputForm1.arrNatureDeLaDonation[1].txtZone03},
                    new object[] {3, "*.c", inputForm1.arrDonatairesDeDegre[2].txtZone01, inputForm1.arrDonatairesDeDegre[2].cb1, inputForm1.arrDonatairesDeDegre[2].cb2, inputForm1.arrDonatairesDeDegre[2].ddl4, inputForm1.arrDonatairesDeDegre[2].ddl5, inputForm1.arrNatureDeLaDonation[2].txtZone02, inputForm1.arrNatureDeLaDonation[2].txtZone03},
                    new object[] {4, "*.d", inputForm1.arrDonatairesDeDegre[3].txtZone01, inputForm1.arrDonatairesDeDegre[3].cb1, inputForm1.arrDonatairesDeDegre[3].cb2, inputForm1.arrDonatairesDeDegre[3].ddl4, inputForm1.arrDonatairesDeDegre[3].ddl5, inputForm1.arrNatureDeLaDonation[3].txtZone02, inputForm1.arrNatureDeLaDonation[3].txtZone03},
                    new object[] {5, "*.e", inputForm1.arrDonatairesDeDegre[4].txtZone01, inputForm1.arrDonatairesDeDegre[4].cb1, inputForm1.arrDonatairesDeDegre[4].cb2, inputForm1.arrDonatairesDeDegre[4].ddl4, inputForm1.arrDonatairesDeDegre[4].ddl5, inputForm1.arrNatureDeLaDonation[4].txtZone02, inputForm1.arrNatureDeLaDonation[4].txtZone03},
                    new object[] {6, "*.f", inputForm1.arrDonatairesDeDegre[5].txtZone01, inputForm1.arrDonatairesDeDegre[5].cb1, inputForm1.arrDonatairesDeDegre[5].cb2, inputForm1.arrDonatairesDeDegre[5].ddl4, inputForm1.arrDonatairesDeDegre[5].ddl5, inputForm1.arrNatureDeLaDonation[5].txtZone02, inputForm1.arrNatureDeLaDonation[5].txtZone03},
                    new object[] {7, "*.g", inputForm1.arrDonatairesDeDegre[6].txtZone01, inputForm1.arrDonatairesDeDegre[6].cb1, inputForm1.arrDonatairesDeDegre[6].cb2, inputForm1.arrDonatairesDeDegre[6].ddl4, inputForm1.arrDonatairesDeDegre[6].ddl5, inputForm1.arrNatureDeLaDonation[6].txtZone02, inputForm1.arrNatureDeLaDonation[6].txtZone03},
                    new object[] {8, "*.h", inputForm1.arrDonatairesDeDegre[7].txtZone01, inputForm1.arrDonatairesDeDegre[7].cb1, inputForm1.arrDonatairesDeDegre[7].cb2, inputForm1.arrDonatairesDeDegre[7].ddl4, inputForm1.arrDonatairesDeDegre[7].ddl5, inputForm1.arrNatureDeLaDonation[7].txtZone02, inputForm1.arrNatureDeLaDonation[7].txtZone03},
                    new object[] {9, "*.i", inputForm1.arrDonatairesDeDegre[8].txtZone01, inputForm1.arrDonatairesDeDegre[8].cb1, inputForm1.arrDonatairesDeDegre[8].cb2, inputForm1.arrDonatairesDeDegre[8].ddl4, inputForm1.arrDonatairesDeDegre[8].ddl5, inputForm1.arrNatureDeLaDonation[8].txtZone02, inputForm1.arrNatureDeLaDonation[8].txtZone03},
                    new object[] {10, "*.j", inputForm1.arrDonatairesDeDegre[9].txtZone01, inputForm1.arrDonatairesDeDegre[9].cb1, inputForm1.arrDonatairesDeDegre[9].cb2, inputForm1.arrDonatairesDeDegre[9].ddl4, inputForm1.arrDonatairesDeDegre[9].ddl5, inputForm1.arrNatureDeLaDonation[9].txtZone02, inputForm1.arrNatureDeLaDonation[9].txtZone03},
                    new object[] {"", "", inputForm1.ddl6, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm1.ddl7, "", "", "", "", "", ""},

                    new object[] {"", "", inputForm2.immobilierCount, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm2.immobilierexonereCount.TransformToInt() >= 1 ? "VRAI" : "FAUX", "", "", "", "", "", ""},
                    new object[] {"", "", inputForm2.mobilierCount, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm2.sommeargentCount, "", "", "", "", "", ""},
                    new object[] {"", "", inputForm2.mobilierexonereCount.TransformToInt() >= 1 ? "VRAI" : "FAUX", "", "", "", "", "", ""},

                    new object[] {"IMMOBILIER", "", "", "", "", "", "", "", ""},
                    new object[] {1, "*.a", "", inputForm2.arrImmobilier[0].origine, inputForm2.arrImmobilier[0].attribution, inputForm2.arrImmobilier[0].reserve, "Maison 1", inputForm2.arrImmobilier[0].valeur, inputForm2.arrImmobilier[0].passif},
                    new object[] {2, "*.b", "", inputForm2.arrImmobilier[1].origine, inputForm2.arrImmobilier[1].attribution, inputForm2.arrImmobilier[1].reserve, "Maison 2", inputForm2.arrImmobilier[1].valeur, inputForm2.arrImmobilier[1].passif},
                    new object[] {3, "*.c", "", inputForm2.arrImmobilier[2].origine, inputForm2.arrImmobilier[2].attribution, inputForm2.arrImmobilier[2].reserve, "Maison 3", inputForm2.arrImmobilier[2].valeur, inputForm2.arrImmobilier[2].passif},
                    new object[] {4, "*.d", "", inputForm2.arrImmobilier[3].origine, inputForm2.arrImmobilier[3].attribution, inputForm2.arrImmobilier[3].reserve, "Maison 4", inputForm2.arrImmobilier[3].valeur, inputForm2.arrImmobilier[3].passif},
                    new object[] {5, "*.e", "", inputForm2.arrImmobilier[4].origine, inputForm2.arrImmobilier[4].attribution, inputForm2.arrImmobilier[4].reserve, "Maison 5", inputForm2.arrImmobilier[4].valeur, inputForm2.arrImmobilier[4].passif},
                    new object[] {6, "*.f", "", inputForm2.arrImmobilier[5].origine, inputForm2.arrImmobilier[5].attribution, inputForm2.arrImmobilier[5].reserve, "Maison 6", inputForm2.arrImmobilier[5].valeur, inputForm2.arrImmobilier[5].passif},
                    new object[] {7, "*.g", "", inputForm2.arrImmobilier[6].origine, inputForm2.arrImmobilier[6].attribution, inputForm2.arrImmobilier[6].reserve, "Maison 7", inputForm2.arrImmobilier[6].valeur, inputForm2.arrImmobilier[6].passif},
                    new object[] {8, "*.h", "", inputForm2.arrImmobilier[7].origine, inputForm2.arrImmobilier[7].attribution, inputForm2.arrImmobilier[7].reserve, "Maison 8", inputForm2.arrImmobilier[7].valeur, inputForm2.arrImmobilier[7].passif},
                    new object[] {9, "*.i", "", inputForm2.arrImmobilier[8].origine, inputForm2.arrImmobilier[8].attribution, inputForm2.arrImmobilier[8].reserve, "Maison 9", inputForm2.arrImmobilier[8].valeur, inputForm2.arrImmobilier[8].passif},
                    new object[] {10, "*.j", "", inputForm2.arrImmobilier[9].origine, inputForm2.arrImmobilier[9].attribution, inputForm2.arrImmobilier[9].reserve, "Maison 10", inputForm2.arrImmobilier[9].valeur, inputForm2.arrImmobilier[9].passif},
                    new object[] {11, "*.k", "", inputForm2.arrImmobilier[10].origine, inputForm2.arrImmobilier[10].attribution, inputForm2.arrImmobilier[10].reserve, "Maison 11", inputForm2.arrImmobilier[10].valeur, inputForm2.arrImmobilier[10].passif},
                    new object[] {12, "*.l", "", inputForm2.arrImmobilier[11].origine, inputForm2.arrImmobilier[11].attribution, inputForm2.arrImmobilier[11].reserve, "Maison 12", inputForm2.arrImmobilier[11].valeur, inputForm2.arrImmobilier[11].passif},
                    new object[] {13, "*.m", "", inputForm2.arrImmobilier[12].origine, inputForm2.arrImmobilier[12].attribution, inputForm2.arrImmobilier[12].reserve, "Maison 13", inputForm2.arrImmobilier[12].valeur, inputForm2.arrImmobilier[12].passif},
                    new object[] {14, "*.n", "", inputForm2.arrImmobilier[13].origine, inputForm2.arrImmobilier[13].attribution, inputForm2.arrImmobilier[13].reserve, "Maison 14", inputForm2.arrImmobilier[13].valeur, inputForm2.arrImmobilier[13].passif},
                    new object[] {15, "*.o", "", inputForm2.arrImmobilier[14].origine, inputForm2.arrImmobilier[14].attribution, inputForm2.arrImmobilier[14].reserve, "Maison 15", inputForm2.arrImmobilier[14].valeur, inputForm2.arrImmobilier[14].passif},
                    new object[] {16, "*.p", "", inputForm2.arrImmobilier[15].origine, inputForm2.arrImmobilier[15].attribution, inputForm2.arrImmobilier[15].reserve, "Maison 16", inputForm2.arrImmobilier[15].valeur, inputForm2.arrImmobilier[15].passif},
                    new object[] {17, "*.q", "", inputForm2.arrImmobilier[16].origine, inputForm2.arrImmobilier[16].attribution, inputForm2.arrImmobilier[16].reserve, "Maison 17", inputForm2.arrImmobilier[16].valeur, inputForm2.arrImmobilier[16].passif},
                    new object[] {18, "*.r", "", inputForm2.arrImmobilier[17].origine, inputForm2.arrImmobilier[17].attribution, inputForm2.arrImmobilier[17].reserve, "Maison 18", inputForm2.arrImmobilier[17].valeur, inputForm2.arrImmobilier[17].passif},
                    new object[] {19, "*.s", "", inputForm2.arrImmobilier[18].origine, inputForm2.arrImmobilier[18].attribution, inputForm2.arrImmobilier[18].reserve, "Maison 19", inputForm2.arrImmobilier[18].valeur, inputForm2.arrImmobilier[18].passif},
                    new object[] {20, "*.t", "", inputForm2.arrImmobilier[19].origine, inputForm2.arrImmobilier[19].attribution, inputForm2.arrImmobilier[19].reserve, "Maison 20", inputForm2.arrImmobilier[19].valeur, inputForm2.arrImmobilier[19].passif},
                    new object[] {"IMMOBILIER EXONERE", "", "", "", "", "", "", "", ""},
                    new object[] {1, "*.a", inputForm2.arrImmobilierexonere[0].typedebien, inputForm2.arrImmobilierexonere[0].origine, inputForm2.arrImmobilierexonere[0].attribution, inputForm2.arrImmobilierexonere[0].reserve, "", inputForm2.arrImmobilierexonere[0].valeur, inputForm2.arrImmobilierexonere[0].passif},
                    new object[] {2, "*.b", inputForm2.arrImmobilierexonere[1].typedebien, inputForm2.arrImmobilierexonere[1].origine, inputForm2.arrImmobilierexonere[1].attribution, inputForm2.arrImmobilierexonere[1].reserve, "", inputForm2.arrImmobilierexonere[1].valeur, inputForm2.arrImmobilierexonere[1].passif},
                    new object[] {"MOBILIER", "", "", "", "", "", "", "", ""},
                    new object[] {1, "*.a", "", inputForm2.arrMobilier[0].origine, inputForm2.arrMobilier[0].attribution, inputForm2.arrMobilier[0].reserve, "Mobilier 1", inputForm2.arrMobilier[0].valeur, inputForm2.arrMobilier[0].passif},
                    new object[] {2, "*.b", "", inputForm2.arrMobilier[1].origine, inputForm2.arrMobilier[1].attribution, inputForm2.arrMobilier[1].reserve, "Mobilier 2", inputForm2.arrMobilier[1].valeur, inputForm2.arrMobilier[1].passif},
                    new object[] {3, "*.c", "", inputForm2.arrMobilier[2].origine, inputForm2.arrMobilier[2].attribution, inputForm2.arrMobilier[2].reserve, "Mobilier 3", inputForm2.arrMobilier[2].valeur, inputForm2.arrMobilier[2].passif},
                    new object[] {4, "*.d", "", inputForm2.arrMobilier[3].origine, inputForm2.arrMobilier[3].attribution, inputForm2.arrMobilier[3].reserve, "Mobilier 4", inputForm2.arrMobilier[3].valeur, inputForm2.arrMobilier[3].passif},
                    new object[] {5, "*.e", "", inputForm2.arrMobilier[4].origine, inputForm2.arrMobilier[4].attribution, inputForm2.arrMobilier[4].reserve, "Mobilier 5", inputForm2.arrMobilier[4].valeur, inputForm2.arrMobilier[4].passif},
                    new object[] {6, "*.f", "", inputForm2.arrMobilier[5].origine, inputForm2.arrMobilier[5].attribution, inputForm2.arrMobilier[5].reserve, "Mobilier 6", inputForm2.arrMobilier[5].valeur, inputForm2.arrMobilier[5].passif},
                    new object[] {7, "*.g", "", inputForm2.arrMobilier[6].origine, inputForm2.arrMobilier[6].attribution, inputForm2.arrMobilier[6].reserve, "Mobilier 7", inputForm2.arrMobilier[6].valeur, inputForm2.arrMobilier[6].passif},
                    new object[] {8, "*.h", "", inputForm2.arrMobilier[7].origine, inputForm2.arrMobilier[7].attribution, inputForm2.arrMobilier[7].reserve, "Mobilier 8", inputForm2.arrMobilier[7].valeur, inputForm2.arrMobilier[7].passif},
                    new object[] {9, "*.i", "", inputForm2.arrMobilier[8].origine, inputForm2.arrMobilier[8].attribution, inputForm2.arrMobilier[8].reserve, "Mobilier 9", inputForm2.arrMobilier[8].valeur, inputForm2.arrMobilier[8].passif},
                    new object[] {10, "*.j", "", inputForm2.arrMobilier[9].origine, inputForm2.arrMobilier[9].attribution, inputForm2.arrMobilier[9].reserve, "Mobilier 10", inputForm2.arrMobilier[9].valeur, inputForm2.arrMobilier[9].passif},
                    new object[] {11, "*.k", "", inputForm2.arrMobilier[10].origine, inputForm2.arrMobilier[10].attribution, inputForm2.arrMobilier[10].reserve, "Mobilier 11", inputForm2.arrMobilier[10].valeur, inputForm2.arrMobilier[10].passif},
                    new object[] {12, "*.l", "", inputForm2.arrMobilier[11].origine, inputForm2.arrMobilier[11].attribution, inputForm2.arrMobilier[11].reserve, "Mobilier 12", inputForm2.arrMobilier[11].valeur, inputForm2.arrMobilier[11].passif},
                    new object[] {13, "*.m", "", inputForm2.arrMobilier[12].origine, inputForm2.arrMobilier[12].attribution, inputForm2.arrMobilier[12].reserve, "Mobilier 13", inputForm2.arrMobilier[12].valeur, inputForm2.arrMobilier[12].passif},
                    new object[] {14, "*.n", "", inputForm2.arrMobilier[13].origine, inputForm2.arrMobilier[13].attribution, inputForm2.arrMobilier[13].reserve, "Mobilier 14", inputForm2.arrMobilier[13].valeur, inputForm2.arrMobilier[13].passif},
                    new object[] {15, "*.o", "", inputForm2.arrMobilier[14].origine, inputForm2.arrMobilier[14].attribution, inputForm2.arrMobilier[14].reserve, "Mobilier 15", inputForm2.arrMobilier[14].valeur, inputForm2.arrMobilier[14].passif},
                    new object[] {16, "*.p", "", inputForm2.arrMobilier[15].origine, inputForm2.arrMobilier[15].attribution, inputForm2.arrMobilier[15].reserve, "Mobilier 16", inputForm2.arrMobilier[15].valeur, inputForm2.arrMobilier[15].passif},
                    new object[] {17, "*.q", "", inputForm2.arrMobilier[16].origine, inputForm2.arrMobilier[16].attribution, inputForm2.arrMobilier[16].reserve, "Mobilier 17", inputForm2.arrMobilier[16].valeur, inputForm2.arrMobilier[16].passif},
                    new object[] {18, "*.r", "", inputForm2.arrMobilier[17].origine, inputForm2.arrMobilier[17].attribution, inputForm2.arrMobilier[17].reserve, "Mobilier 18", inputForm2.arrMobilier[17].valeur, inputForm2.arrMobilier[17].passif},
                    new object[] {19, "*.s", "", inputForm2.arrMobilier[18].origine, inputForm2.arrMobilier[18].attribution, inputForm2.arrMobilier[18].reserve, "Mobilier 19", inputForm2.arrMobilier[18].valeur, inputForm2.arrMobilier[18].passif},
                    new object[] {20, "*.t", "", inputForm2.arrMobilier[19].origine, inputForm2.arrMobilier[19].attribution, inputForm2.arrMobilier[19].reserve, "Mobilier 20", inputForm2.arrMobilier[19].valeur, inputForm2.arrMobilier[19].passif},
                    new object[] {"SOMME D'ARGENT", "", "", "", "", "", "", "", ""},
                    new object[] {1, "*.a", "", inputForm2.arrSommeargent[0].origine, inputForm2.arrSommeargent[0].attribution, "", "Somme argent 1", inputForm2.arrSommeargent[0].valeur, ""},
                    new object[] {2, "*.b", "", inputForm2.arrSommeargent[1].origine, inputForm2.arrSommeargent[1].attribution, "", "Somme argent 2", inputForm2.arrSommeargent[1].valeur, ""},
                    new object[] {3, "*.c", "", inputForm2.arrSommeargent[2].origine, inputForm2.arrSommeargent[2].attribution, "", "Somme argent 3", inputForm2.arrSommeargent[2].valeur, ""},
                    new object[] {4, "*.d", "", inputForm2.arrSommeargent[3].origine, inputForm2.arrSommeargent[3].attribution, "", "Somme argent 4", inputForm2.arrSommeargent[3].valeur, ""},
                    new object[] {5, "*.e", "", inputForm2.arrSommeargent[4].origine, inputForm2.arrSommeargent[4].attribution, "", "Somme argent 5", inputForm2.arrSommeargent[4].valeur, ""},
                    new object[] {6, "*.f", "", inputForm2.arrSommeargent[5].origine, inputForm2.arrSommeargent[5].attribution, "", "Somme argent 6", inputForm2.arrSommeargent[5].valeur, ""},
                    new object[] {7, "*.g", "", inputForm2.arrSommeargent[6].origine, inputForm2.arrSommeargent[6].attribution, "", "Somme argent 7", inputForm2.arrSommeargent[6].valeur, ""},
                    new object[] {8, "*.h", "", inputForm2.arrSommeargent[7].origine, inputForm2.arrSommeargent[7].attribution, "", "Somme argent 8", inputForm2.arrSommeargent[7].valeur, ""},
                    new object[] {9, "*.i", "", inputForm2.arrSommeargent[8].origine, inputForm2.arrSommeargent[8].attribution, "", "Somme argent 9", inputForm2.arrSommeargent[8].valeur, ""},
                    new object[] {10, "*.j", "", inputForm2.arrSommeargent[9].origine, inputForm2.arrSommeargent[9].attribution, "", "Somme argent 10", inputForm2.arrSommeargent[9].valeur, ""},
                    new object[] {"MOBILIER EXONERE", "", "", "", "", "", "", "", ""},
                    new object[] {1, "*.a", inputForm2.arrMobilierexonere[0].typedebien, inputForm2.arrMobilierexonere[0].origine, inputForm2.arrMobilierexonere[0].attribution, inputForm2.arrMobilierexonere[0].reserve, "", inputForm2.arrMobilierexonere[0].valeur, inputForm2.arrMobilierexonere[0].passif},
                    new object[] {2, "*.b", inputForm2.arrMobilierexonere[1].typedebien, inputForm2.arrMobilierexonere[1].origine, inputForm2.arrMobilierexonere[1].attribution, inputForm2.arrMobilierexonere[1].reserve, "", inputForm2.arrMobilierexonere[1].valeur, inputForm2.arrMobilierexonere[1].passif}
                });

            var inputForm3 = js.Deserialize<RappelDeDonationsAnterieuresModel[]>(strInputForm3);
            range = new RangeCoordinates
            {
                Row = 109,
                Column = 3,
                Height = 200,
                Width = 9
            };
            _excelService.SetRange(_sessionId, "ENTREE_S", range,
                new object[]
                {
                    new object[] {inputForm3[0].Pere[0].txtZone01,inputForm3[0].Pere[0].txtZone02,inputForm3[0].Pere[0].txtZone03,inputForm3[0].Pere[0].txtZone04,inputForm3[0].Pere[0].txtZone05,inputForm3[0].Pere[0].txtZone06,inputForm3[0].Pere[0].txtZone07,inputForm3[0].Pere[0].txtZone00_,inputForm3[0].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[1].txtZone01,inputForm3[0].Pere[1].txtZone02,inputForm3[0].Pere[1].txtZone03,inputForm3[0].Pere[1].txtZone04,inputForm3[0].Pere[1].txtZone05,inputForm3[0].Pere[1].txtZone06,inputForm3[0].Pere[1].txtZone07,inputForm3[0].Pere[1].txtZone00_,inputForm3[0].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[2].txtZone01,inputForm3[0].Pere[2].txtZone02,inputForm3[0].Pere[2].txtZone03,inputForm3[0].Pere[2].txtZone04,inputForm3[0].Pere[2].txtZone05,inputForm3[0].Pere[2].txtZone06,inputForm3[0].Pere[2].txtZone07,inputForm3[0].Pere[2].txtZone00_,inputForm3[0].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[3].txtZone01,inputForm3[0].Pere[3].txtZone02,inputForm3[0].Pere[3].txtZone03,inputForm3[0].Pere[3].txtZone04,inputForm3[0].Pere[3].txtZone05,inputForm3[0].Pere[3].txtZone06,inputForm3[0].Pere[3].txtZone07,inputForm3[0].Pere[3].txtZone00_,inputForm3[0].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[4].txtZone01,inputForm3[0].Pere[4].txtZone02,inputForm3[0].Pere[4].txtZone03,inputForm3[0].Pere[4].txtZone04,inputForm3[0].Pere[4].txtZone05,inputForm3[0].Pere[4].txtZone06,inputForm3[0].Pere[4].txtZone07,inputForm3[0].Pere[4].txtZone00_,inputForm3[0].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[5].txtZone01,inputForm3[0].Pere[5].txtZone02,inputForm3[0].Pere[5].txtZone03,inputForm3[0].Pere[5].txtZone04,inputForm3[0].Pere[5].txtZone05,inputForm3[0].Pere[5].txtZone06,inputForm3[0].Pere[5].txtZone07,inputForm3[0].Pere[5].txtZone00_,inputForm3[0].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[6].txtZone01,inputForm3[0].Pere[6].txtZone02,inputForm3[0].Pere[6].txtZone03,inputForm3[0].Pere[6].txtZone04,inputForm3[0].Pere[6].txtZone05,inputForm3[0].Pere[6].txtZone06,inputForm3[0].Pere[6].txtZone07,inputForm3[0].Pere[6].txtZone00_,inputForm3[0].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[7].txtZone01,inputForm3[0].Pere[7].txtZone02,inputForm3[0].Pere[7].txtZone03,inputForm3[0].Pere[7].txtZone04,inputForm3[0].Pere[7].txtZone05,inputForm3[0].Pere[7].txtZone06,inputForm3[0].Pere[7].txtZone07,inputForm3[0].Pere[7].txtZone00_,inputForm3[0].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[8].txtZone01,inputForm3[0].Pere[8].txtZone02,inputForm3[0].Pere[8].txtZone03,inputForm3[0].Pere[8].txtZone04,inputForm3[0].Pere[8].txtZone05,inputForm3[0].Pere[8].txtZone06,inputForm3[0].Pere[8].txtZone07,inputForm3[0].Pere[8].txtZone00_,inputForm3[0].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Pere[9].txtZone01,inputForm3[0].Pere[9].txtZone02,inputForm3[0].Pere[9].txtZone03,inputForm3[0].Pere[9].txtZone04,inputForm3[0].Pere[9].txtZone05,inputForm3[0].Pere[9].txtZone06,inputForm3[0].Pere[9].txtZone07,inputForm3[0].Pere[9].txtZone00_,inputForm3[0].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[1].Pere[0].txtZone01,inputForm3[1].Pere[0].txtZone02,inputForm3[1].Pere[0].txtZone03,inputForm3[1].Pere[0].txtZone04,inputForm3[1].Pere[0].txtZone05,inputForm3[1].Pere[0].txtZone06,inputForm3[1].Pere[0].txtZone07,inputForm3[1].Pere[0].txtZone00_,inputForm3[1].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[1].txtZone01,inputForm3[1].Pere[1].txtZone02,inputForm3[1].Pere[1].txtZone03,inputForm3[1].Pere[1].txtZone04,inputForm3[1].Pere[1].txtZone05,inputForm3[1].Pere[1].txtZone06,inputForm3[1].Pere[1].txtZone07,inputForm3[1].Pere[1].txtZone00_,inputForm3[1].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[2].txtZone01,inputForm3[1].Pere[2].txtZone02,inputForm3[1].Pere[2].txtZone03,inputForm3[1].Pere[2].txtZone04,inputForm3[1].Pere[2].txtZone05,inputForm3[1].Pere[2].txtZone06,inputForm3[1].Pere[2].txtZone07,inputForm3[1].Pere[2].txtZone00_,inputForm3[1].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[3].txtZone01,inputForm3[1].Pere[3].txtZone02,inputForm3[1].Pere[3].txtZone03,inputForm3[1].Pere[3].txtZone04,inputForm3[1].Pere[3].txtZone05,inputForm3[1].Pere[3].txtZone06,inputForm3[1].Pere[3].txtZone07,inputForm3[1].Pere[3].txtZone00_,inputForm3[1].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[4].txtZone01,inputForm3[1].Pere[4].txtZone02,inputForm3[1].Pere[4].txtZone03,inputForm3[1].Pere[4].txtZone04,inputForm3[1].Pere[4].txtZone05,inputForm3[1].Pere[4].txtZone06,inputForm3[1].Pere[4].txtZone07,inputForm3[1].Pere[4].txtZone00_,inputForm3[1].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[5].txtZone01,inputForm3[1].Pere[5].txtZone02,inputForm3[1].Pere[5].txtZone03,inputForm3[1].Pere[5].txtZone04,inputForm3[1].Pere[5].txtZone05,inputForm3[1].Pere[5].txtZone06,inputForm3[1].Pere[5].txtZone07,inputForm3[1].Pere[5].txtZone00_,inputForm3[1].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[6].txtZone01,inputForm3[1].Pere[6].txtZone02,inputForm3[1].Pere[6].txtZone03,inputForm3[1].Pere[6].txtZone04,inputForm3[1].Pere[6].txtZone05,inputForm3[1].Pere[6].txtZone06,inputForm3[1].Pere[6].txtZone07,inputForm3[1].Pere[6].txtZone00_,inputForm3[1].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[7].txtZone01,inputForm3[1].Pere[7].txtZone02,inputForm3[1].Pere[7].txtZone03,inputForm3[1].Pere[7].txtZone04,inputForm3[1].Pere[7].txtZone05,inputForm3[1].Pere[7].txtZone06,inputForm3[1].Pere[7].txtZone07,inputForm3[1].Pere[7].txtZone00_,inputForm3[1].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[8].txtZone01,inputForm3[1].Pere[8].txtZone02,inputForm3[1].Pere[8].txtZone03,inputForm3[1].Pere[8].txtZone04,inputForm3[1].Pere[8].txtZone05,inputForm3[1].Pere[8].txtZone06,inputForm3[1].Pere[8].txtZone07,inputForm3[1].Pere[8].txtZone00_,inputForm3[1].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Pere[9].txtZone01,inputForm3[1].Pere[9].txtZone02,inputForm3[1].Pere[9].txtZone03,inputForm3[1].Pere[9].txtZone04,inputForm3[1].Pere[9].txtZone05,inputForm3[1].Pere[9].txtZone06,inputForm3[1].Pere[9].txtZone07,inputForm3[1].Pere[9].txtZone00_,inputForm3[1].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[2].Pere[0].txtZone01,inputForm3[2].Pere[0].txtZone02,inputForm3[2].Pere[0].txtZone03,inputForm3[2].Pere[0].txtZone04,inputForm3[2].Pere[0].txtZone05,inputForm3[2].Pere[0].txtZone06,inputForm3[2].Pere[0].txtZone07,inputForm3[2].Pere[0].txtZone00_,inputForm3[2].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[1].txtZone01,inputForm3[2].Pere[1].txtZone02,inputForm3[2].Pere[1].txtZone03,inputForm3[2].Pere[1].txtZone04,inputForm3[2].Pere[1].txtZone05,inputForm3[2].Pere[1].txtZone06,inputForm3[2].Pere[1].txtZone07,inputForm3[2].Pere[1].txtZone00_,inputForm3[2].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[2].txtZone01,inputForm3[2].Pere[2].txtZone02,inputForm3[2].Pere[2].txtZone03,inputForm3[2].Pere[2].txtZone04,inputForm3[2].Pere[2].txtZone05,inputForm3[2].Pere[2].txtZone06,inputForm3[2].Pere[2].txtZone07,inputForm3[2].Pere[2].txtZone00_,inputForm3[2].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[3].txtZone01,inputForm3[2].Pere[3].txtZone02,inputForm3[2].Pere[3].txtZone03,inputForm3[2].Pere[3].txtZone04,inputForm3[2].Pere[3].txtZone05,inputForm3[2].Pere[3].txtZone06,inputForm3[2].Pere[3].txtZone07,inputForm3[2].Pere[3].txtZone00_,inputForm3[2].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[4].txtZone01,inputForm3[2].Pere[4].txtZone02,inputForm3[2].Pere[4].txtZone03,inputForm3[2].Pere[4].txtZone04,inputForm3[2].Pere[4].txtZone05,inputForm3[2].Pere[4].txtZone06,inputForm3[2].Pere[4].txtZone07,inputForm3[2].Pere[4].txtZone00_,inputForm3[2].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[5].txtZone01,inputForm3[2].Pere[5].txtZone02,inputForm3[2].Pere[5].txtZone03,inputForm3[2].Pere[5].txtZone04,inputForm3[2].Pere[5].txtZone05,inputForm3[2].Pere[5].txtZone06,inputForm3[2].Pere[5].txtZone07,inputForm3[2].Pere[5].txtZone00_,inputForm3[2].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[6].txtZone01,inputForm3[2].Pere[6].txtZone02,inputForm3[2].Pere[6].txtZone03,inputForm3[2].Pere[6].txtZone04,inputForm3[2].Pere[6].txtZone05,inputForm3[2].Pere[6].txtZone06,inputForm3[2].Pere[6].txtZone07,inputForm3[2].Pere[6].txtZone00_,inputForm3[2].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[7].txtZone01,inputForm3[2].Pere[7].txtZone02,inputForm3[2].Pere[7].txtZone03,inputForm3[2].Pere[7].txtZone04,inputForm3[2].Pere[7].txtZone05,inputForm3[2].Pere[7].txtZone06,inputForm3[2].Pere[7].txtZone07,inputForm3[2].Pere[7].txtZone00_,inputForm3[2].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[8].txtZone01,inputForm3[2].Pere[8].txtZone02,inputForm3[2].Pere[8].txtZone03,inputForm3[2].Pere[8].txtZone04,inputForm3[2].Pere[8].txtZone05,inputForm3[2].Pere[8].txtZone06,inputForm3[2].Pere[8].txtZone07,inputForm3[2].Pere[8].txtZone00_,inputForm3[2].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Pere[9].txtZone01,inputForm3[2].Pere[9].txtZone02,inputForm3[2].Pere[9].txtZone03,inputForm3[2].Pere[9].txtZone04,inputForm3[2].Pere[9].txtZone05,inputForm3[2].Pere[9].txtZone06,inputForm3[2].Pere[9].txtZone07,inputForm3[2].Pere[9].txtZone00_,inputForm3[2].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[3].Pere[0].txtZone01,inputForm3[3].Pere[0].txtZone02,inputForm3[3].Pere[0].txtZone03,inputForm3[3].Pere[0].txtZone04,inputForm3[3].Pere[0].txtZone05,inputForm3[3].Pere[0].txtZone06,inputForm3[3].Pere[0].txtZone07,inputForm3[3].Pere[0].txtZone00_,inputForm3[3].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[1].txtZone01,inputForm3[3].Pere[1].txtZone02,inputForm3[3].Pere[1].txtZone03,inputForm3[3].Pere[1].txtZone04,inputForm3[3].Pere[1].txtZone05,inputForm3[3].Pere[1].txtZone06,inputForm3[3].Pere[1].txtZone07,inputForm3[3].Pere[1].txtZone00_,inputForm3[3].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[2].txtZone01,inputForm3[3].Pere[2].txtZone02,inputForm3[3].Pere[2].txtZone03,inputForm3[3].Pere[2].txtZone04,inputForm3[3].Pere[2].txtZone05,inputForm3[3].Pere[2].txtZone06,inputForm3[3].Pere[2].txtZone07,inputForm3[3].Pere[2].txtZone00_,inputForm3[3].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[3].txtZone01,inputForm3[3].Pere[3].txtZone02,inputForm3[3].Pere[3].txtZone03,inputForm3[3].Pere[3].txtZone04,inputForm3[3].Pere[3].txtZone05,inputForm3[3].Pere[3].txtZone06,inputForm3[3].Pere[3].txtZone07,inputForm3[3].Pere[3].txtZone00_,inputForm3[3].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[4].txtZone01,inputForm3[3].Pere[4].txtZone02,inputForm3[3].Pere[4].txtZone03,inputForm3[3].Pere[4].txtZone04,inputForm3[3].Pere[4].txtZone05,inputForm3[3].Pere[4].txtZone06,inputForm3[3].Pere[4].txtZone07,inputForm3[3].Pere[4].txtZone00_,inputForm3[3].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[5].txtZone01,inputForm3[3].Pere[5].txtZone02,inputForm3[3].Pere[5].txtZone03,inputForm3[3].Pere[5].txtZone04,inputForm3[3].Pere[5].txtZone05,inputForm3[3].Pere[5].txtZone06,inputForm3[3].Pere[5].txtZone07,inputForm3[3].Pere[5].txtZone00_,inputForm3[3].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[6].txtZone01,inputForm3[3].Pere[6].txtZone02,inputForm3[3].Pere[6].txtZone03,inputForm3[3].Pere[6].txtZone04,inputForm3[3].Pere[6].txtZone05,inputForm3[3].Pere[6].txtZone06,inputForm3[3].Pere[6].txtZone07,inputForm3[3].Pere[6].txtZone00_,inputForm3[3].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[7].txtZone01,inputForm3[3].Pere[7].txtZone02,inputForm3[3].Pere[7].txtZone03,inputForm3[3].Pere[7].txtZone04,inputForm3[3].Pere[7].txtZone05,inputForm3[3].Pere[7].txtZone06,inputForm3[3].Pere[7].txtZone07,inputForm3[3].Pere[7].txtZone00_,inputForm3[3].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[8].txtZone01,inputForm3[3].Pere[8].txtZone02,inputForm3[3].Pere[8].txtZone03,inputForm3[3].Pere[8].txtZone04,inputForm3[3].Pere[8].txtZone05,inputForm3[3].Pere[8].txtZone06,inputForm3[3].Pere[8].txtZone07,inputForm3[3].Pere[8].txtZone00_,inputForm3[3].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Pere[9].txtZone01,inputForm3[3].Pere[9].txtZone02,inputForm3[3].Pere[9].txtZone03,inputForm3[3].Pere[9].txtZone04,inputForm3[3].Pere[9].txtZone05,inputForm3[3].Pere[9].txtZone06,inputForm3[3].Pere[9].txtZone07,inputForm3[3].Pere[9].txtZone00_,inputForm3[3].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[4].Pere[0].txtZone01,inputForm3[4].Pere[0].txtZone02,inputForm3[4].Pere[0].txtZone03,inputForm3[4].Pere[0].txtZone04,inputForm3[4].Pere[0].txtZone05,inputForm3[4].Pere[0].txtZone06,inputForm3[4].Pere[0].txtZone07,inputForm3[4].Pere[0].txtZone00_,inputForm3[5].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[1].txtZone01,inputForm3[4].Pere[1].txtZone02,inputForm3[4].Pere[1].txtZone03,inputForm3[4].Pere[1].txtZone04,inputForm3[4].Pere[1].txtZone05,inputForm3[4].Pere[1].txtZone06,inputForm3[4].Pere[1].txtZone07,inputForm3[4].Pere[1].txtZone00_,inputForm3[5].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[2].txtZone01,inputForm3[4].Pere[2].txtZone02,inputForm3[4].Pere[2].txtZone03,inputForm3[4].Pere[2].txtZone04,inputForm3[4].Pere[2].txtZone05,inputForm3[4].Pere[2].txtZone06,inputForm3[4].Pere[2].txtZone07,inputForm3[4].Pere[2].txtZone00_,inputForm3[5].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[3].txtZone01,inputForm3[4].Pere[3].txtZone02,inputForm3[4].Pere[3].txtZone03,inputForm3[4].Pere[3].txtZone04,inputForm3[4].Pere[3].txtZone05,inputForm3[4].Pere[3].txtZone06,inputForm3[4].Pere[3].txtZone07,inputForm3[4].Pere[3].txtZone00_,inputForm3[5].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[4].txtZone01,inputForm3[4].Pere[4].txtZone02,inputForm3[4].Pere[4].txtZone03,inputForm3[4].Pere[4].txtZone04,inputForm3[4].Pere[4].txtZone05,inputForm3[4].Pere[4].txtZone06,inputForm3[4].Pere[4].txtZone07,inputForm3[4].Pere[4].txtZone00_,inputForm3[5].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[5].txtZone01,inputForm3[4].Pere[5].txtZone02,inputForm3[4].Pere[5].txtZone03,inputForm3[4].Pere[5].txtZone04,inputForm3[4].Pere[5].txtZone05,inputForm3[4].Pere[5].txtZone06,inputForm3[4].Pere[5].txtZone07,inputForm3[4].Pere[5].txtZone00_,inputForm3[5].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[6].txtZone01,inputForm3[4].Pere[6].txtZone02,inputForm3[4].Pere[6].txtZone03,inputForm3[4].Pere[6].txtZone04,inputForm3[4].Pere[6].txtZone05,inputForm3[4].Pere[6].txtZone06,inputForm3[4].Pere[6].txtZone07,inputForm3[4].Pere[6].txtZone00_,inputForm3[5].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[7].txtZone01,inputForm3[4].Pere[7].txtZone02,inputForm3[4].Pere[7].txtZone03,inputForm3[4].Pere[7].txtZone04,inputForm3[4].Pere[7].txtZone05,inputForm3[4].Pere[7].txtZone06,inputForm3[4].Pere[7].txtZone07,inputForm3[4].Pere[7].txtZone00_,inputForm3[5].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[8].txtZone01,inputForm3[4].Pere[8].txtZone02,inputForm3[4].Pere[8].txtZone03,inputForm3[4].Pere[8].txtZone04,inputForm3[4].Pere[8].txtZone05,inputForm3[4].Pere[8].txtZone06,inputForm3[4].Pere[8].txtZone07,inputForm3[4].Pere[8].txtZone00_,inputForm3[5].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Pere[9].txtZone01,inputForm3[4].Pere[9].txtZone02,inputForm3[4].Pere[9].txtZone03,inputForm3[4].Pere[9].txtZone04,inputForm3[4].Pere[9].txtZone05,inputForm3[4].Pere[9].txtZone06,inputForm3[4].Pere[9].txtZone07,inputForm3[4].Pere[9].txtZone00_,inputForm3[5].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[5].Pere[0].txtZone01,inputForm3[5].Pere[0].txtZone02,inputForm3[5].Pere[0].txtZone03,inputForm3[5].Pere[0].txtZone04,inputForm3[5].Pere[0].txtZone05,inputForm3[5].Pere[0].txtZone06,inputForm3[5].Pere[0].txtZone07,inputForm3[5].Pere[0].txtZone00_,inputForm3[5].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[1].txtZone01,inputForm3[5].Pere[1].txtZone02,inputForm3[5].Pere[1].txtZone03,inputForm3[5].Pere[1].txtZone04,inputForm3[5].Pere[1].txtZone05,inputForm3[5].Pere[1].txtZone06,inputForm3[5].Pere[1].txtZone07,inputForm3[5].Pere[1].txtZone00_,inputForm3[5].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[2].txtZone01,inputForm3[5].Pere[2].txtZone02,inputForm3[5].Pere[2].txtZone03,inputForm3[5].Pere[2].txtZone04,inputForm3[5].Pere[2].txtZone05,inputForm3[5].Pere[2].txtZone06,inputForm3[5].Pere[2].txtZone07,inputForm3[5].Pere[2].txtZone00_,inputForm3[5].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[3].txtZone01,inputForm3[5].Pere[3].txtZone02,inputForm3[5].Pere[3].txtZone03,inputForm3[5].Pere[3].txtZone04,inputForm3[5].Pere[3].txtZone05,inputForm3[5].Pere[3].txtZone06,inputForm3[5].Pere[3].txtZone07,inputForm3[5].Pere[3].txtZone00_,inputForm3[5].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[4].txtZone01,inputForm3[5].Pere[4].txtZone02,inputForm3[5].Pere[4].txtZone03,inputForm3[5].Pere[4].txtZone04,inputForm3[5].Pere[4].txtZone05,inputForm3[5].Pere[4].txtZone06,inputForm3[5].Pere[4].txtZone07,inputForm3[5].Pere[4].txtZone00_,inputForm3[5].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[5].txtZone01,inputForm3[5].Pere[5].txtZone02,inputForm3[5].Pere[5].txtZone03,inputForm3[5].Pere[5].txtZone04,inputForm3[5].Pere[5].txtZone05,inputForm3[5].Pere[5].txtZone06,inputForm3[5].Pere[5].txtZone07,inputForm3[5].Pere[5].txtZone00_,inputForm3[5].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[6].txtZone01,inputForm3[5].Pere[6].txtZone02,inputForm3[5].Pere[6].txtZone03,inputForm3[5].Pere[6].txtZone04,inputForm3[5].Pere[6].txtZone05,inputForm3[5].Pere[6].txtZone06,inputForm3[5].Pere[6].txtZone07,inputForm3[5].Pere[6].txtZone00_,inputForm3[5].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[7].txtZone01,inputForm3[5].Pere[7].txtZone02,inputForm3[5].Pere[7].txtZone03,inputForm3[5].Pere[7].txtZone04,inputForm3[5].Pere[7].txtZone05,inputForm3[5].Pere[7].txtZone06,inputForm3[5].Pere[7].txtZone07,inputForm3[5].Pere[7].txtZone00_,inputForm3[5].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[8].txtZone01,inputForm3[5].Pere[8].txtZone02,inputForm3[5].Pere[8].txtZone03,inputForm3[5].Pere[8].txtZone04,inputForm3[5].Pere[8].txtZone05,inputForm3[5].Pere[8].txtZone06,inputForm3[5].Pere[8].txtZone07,inputForm3[5].Pere[8].txtZone00_,inputForm3[5].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Pere[9].txtZone01,inputForm3[5].Pere[9].txtZone02,inputForm3[5].Pere[9].txtZone03,inputForm3[5].Pere[9].txtZone04,inputForm3[5].Pere[9].txtZone05,inputForm3[5].Pere[9].txtZone06,inputForm3[5].Pere[9].txtZone07,inputForm3[5].Pere[9].txtZone00_,inputForm3[5].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[6].Pere[0].txtZone01,inputForm3[6].Pere[0].txtZone02,inputForm3[6].Pere[0].txtZone03,inputForm3[6].Pere[0].txtZone04,inputForm3[6].Pere[0].txtZone05,inputForm3[6].Pere[0].txtZone06,inputForm3[6].Pere[0].txtZone07,inputForm3[6].Pere[0].txtZone00_,inputForm3[6].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[1].txtZone01,inputForm3[6].Pere[1].txtZone02,inputForm3[6].Pere[1].txtZone03,inputForm3[6].Pere[1].txtZone04,inputForm3[6].Pere[1].txtZone05,inputForm3[6].Pere[1].txtZone06,inputForm3[6].Pere[1].txtZone07,inputForm3[6].Pere[1].txtZone00_,inputForm3[6].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[2].txtZone01,inputForm3[6].Pere[2].txtZone02,inputForm3[6].Pere[2].txtZone03,inputForm3[6].Pere[2].txtZone04,inputForm3[6].Pere[2].txtZone05,inputForm3[6].Pere[2].txtZone06,inputForm3[6].Pere[2].txtZone07,inputForm3[6].Pere[2].txtZone00_,inputForm3[6].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[3].txtZone01,inputForm3[6].Pere[3].txtZone02,inputForm3[6].Pere[3].txtZone03,inputForm3[6].Pere[3].txtZone04,inputForm3[6].Pere[3].txtZone05,inputForm3[6].Pere[3].txtZone06,inputForm3[6].Pere[3].txtZone07,inputForm3[6].Pere[3].txtZone00_,inputForm3[6].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[4].txtZone01,inputForm3[6].Pere[4].txtZone02,inputForm3[6].Pere[4].txtZone03,inputForm3[6].Pere[4].txtZone04,inputForm3[6].Pere[4].txtZone05,inputForm3[6].Pere[4].txtZone06,inputForm3[6].Pere[4].txtZone07,inputForm3[6].Pere[4].txtZone00_,inputForm3[6].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[5].txtZone01,inputForm3[6].Pere[5].txtZone02,inputForm3[6].Pere[5].txtZone03,inputForm3[6].Pere[5].txtZone04,inputForm3[6].Pere[5].txtZone05,inputForm3[6].Pere[5].txtZone06,inputForm3[6].Pere[5].txtZone07,inputForm3[6].Pere[5].txtZone00_,inputForm3[6].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[6].txtZone01,inputForm3[6].Pere[6].txtZone02,inputForm3[6].Pere[6].txtZone03,inputForm3[6].Pere[6].txtZone04,inputForm3[6].Pere[6].txtZone05,inputForm3[6].Pere[6].txtZone06,inputForm3[6].Pere[6].txtZone07,inputForm3[6].Pere[6].txtZone00_,inputForm3[6].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[7].txtZone01,inputForm3[6].Pere[7].txtZone02,inputForm3[6].Pere[7].txtZone03,inputForm3[6].Pere[7].txtZone04,inputForm3[6].Pere[7].txtZone05,inputForm3[6].Pere[7].txtZone06,inputForm3[6].Pere[7].txtZone07,inputForm3[6].Pere[7].txtZone00_,inputForm3[6].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[8].txtZone01,inputForm3[6].Pere[8].txtZone02,inputForm3[6].Pere[8].txtZone03,inputForm3[6].Pere[8].txtZone04,inputForm3[6].Pere[8].txtZone05,inputForm3[6].Pere[8].txtZone06,inputForm3[6].Pere[8].txtZone07,inputForm3[6].Pere[8].txtZone00_,inputForm3[6].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Pere[9].txtZone01,inputForm3[6].Pere[9].txtZone02,inputForm3[6].Pere[9].txtZone03,inputForm3[6].Pere[9].txtZone04,inputForm3[6].Pere[9].txtZone05,inputForm3[6].Pere[9].txtZone06,inputForm3[6].Pere[9].txtZone07,inputForm3[6].Pere[9].txtZone00_,inputForm3[6].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[7].Pere[0].txtZone01,inputForm3[7].Pere[0].txtZone02,inputForm3[7].Pere[0].txtZone03,inputForm3[7].Pere[0].txtZone04,inputForm3[7].Pere[0].txtZone05,inputForm3[7].Pere[0].txtZone06,inputForm3[7].Pere[0].txtZone07,inputForm3[7].Pere[0].txtZone00_,inputForm3[7].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[1].txtZone01,inputForm3[7].Pere[1].txtZone02,inputForm3[7].Pere[1].txtZone03,inputForm3[7].Pere[1].txtZone04,inputForm3[7].Pere[1].txtZone05,inputForm3[7].Pere[1].txtZone06,inputForm3[7].Pere[1].txtZone07,inputForm3[7].Pere[1].txtZone00_,inputForm3[7].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[2].txtZone01,inputForm3[7].Pere[2].txtZone02,inputForm3[7].Pere[2].txtZone03,inputForm3[7].Pere[2].txtZone04,inputForm3[7].Pere[2].txtZone05,inputForm3[7].Pere[2].txtZone06,inputForm3[7].Pere[2].txtZone07,inputForm3[7].Pere[2].txtZone00_,inputForm3[7].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[3].txtZone01,inputForm3[7].Pere[3].txtZone02,inputForm3[7].Pere[3].txtZone03,inputForm3[7].Pere[3].txtZone04,inputForm3[7].Pere[3].txtZone05,inputForm3[7].Pere[3].txtZone06,inputForm3[7].Pere[3].txtZone07,inputForm3[7].Pere[3].txtZone00_,inputForm3[7].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[4].txtZone01,inputForm3[7].Pere[4].txtZone02,inputForm3[7].Pere[4].txtZone03,inputForm3[7].Pere[4].txtZone04,inputForm3[7].Pere[4].txtZone05,inputForm3[7].Pere[4].txtZone06,inputForm3[7].Pere[4].txtZone07,inputForm3[7].Pere[4].txtZone00_,inputForm3[7].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[5].txtZone01,inputForm3[7].Pere[5].txtZone02,inputForm3[7].Pere[5].txtZone03,inputForm3[7].Pere[5].txtZone04,inputForm3[7].Pere[5].txtZone05,inputForm3[7].Pere[5].txtZone06,inputForm3[7].Pere[5].txtZone07,inputForm3[7].Pere[5].txtZone00_,inputForm3[7].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[6].txtZone01,inputForm3[7].Pere[6].txtZone02,inputForm3[7].Pere[6].txtZone03,inputForm3[7].Pere[6].txtZone04,inputForm3[7].Pere[6].txtZone05,inputForm3[7].Pere[6].txtZone06,inputForm3[7].Pere[6].txtZone07,inputForm3[7].Pere[6].txtZone00_,inputForm3[7].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[7].txtZone01,inputForm3[7].Pere[7].txtZone02,inputForm3[7].Pere[7].txtZone03,inputForm3[7].Pere[7].txtZone04,inputForm3[7].Pere[7].txtZone05,inputForm3[7].Pere[7].txtZone06,inputForm3[7].Pere[7].txtZone07,inputForm3[7].Pere[7].txtZone00_,inputForm3[7].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[8].txtZone01,inputForm3[7].Pere[8].txtZone02,inputForm3[7].Pere[8].txtZone03,inputForm3[7].Pere[8].txtZone04,inputForm3[7].Pere[8].txtZone05,inputForm3[7].Pere[8].txtZone06,inputForm3[7].Pere[8].txtZone07,inputForm3[7].Pere[8].txtZone00_,inputForm3[7].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Pere[9].txtZone01,inputForm3[7].Pere[9].txtZone02,inputForm3[7].Pere[9].txtZone03,inputForm3[7].Pere[9].txtZone04,inputForm3[7].Pere[9].txtZone05,inputForm3[7].Pere[9].txtZone06,inputForm3[7].Pere[9].txtZone07,inputForm3[7].Pere[9].txtZone00_,inputForm3[7].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[8].Pere[0].txtZone01,inputForm3[8].Pere[0].txtZone02,inputForm3[8].Pere[0].txtZone03,inputForm3[8].Pere[0].txtZone04,inputForm3[8].Pere[0].txtZone05,inputForm3[8].Pere[0].txtZone06,inputForm3[8].Pere[0].txtZone07,inputForm3[8].Pere[0].txtZone00_,inputForm3[8].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[1].txtZone01,inputForm3[8].Pere[1].txtZone02,inputForm3[8].Pere[1].txtZone03,inputForm3[8].Pere[1].txtZone04,inputForm3[8].Pere[1].txtZone05,inputForm3[8].Pere[1].txtZone06,inputForm3[8].Pere[1].txtZone07,inputForm3[8].Pere[1].txtZone00_,inputForm3[8].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[2].txtZone01,inputForm3[8].Pere[2].txtZone02,inputForm3[8].Pere[2].txtZone03,inputForm3[8].Pere[2].txtZone04,inputForm3[8].Pere[2].txtZone05,inputForm3[8].Pere[2].txtZone06,inputForm3[8].Pere[2].txtZone07,inputForm3[8].Pere[2].txtZone00_,inputForm3[8].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[3].txtZone01,inputForm3[8].Pere[3].txtZone02,inputForm3[8].Pere[3].txtZone03,inputForm3[8].Pere[3].txtZone04,inputForm3[8].Pere[3].txtZone05,inputForm3[8].Pere[3].txtZone06,inputForm3[8].Pere[3].txtZone07,inputForm3[8].Pere[3].txtZone00_,inputForm3[8].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[4].txtZone01,inputForm3[8].Pere[4].txtZone02,inputForm3[8].Pere[4].txtZone03,inputForm3[8].Pere[4].txtZone04,inputForm3[8].Pere[4].txtZone05,inputForm3[8].Pere[4].txtZone06,inputForm3[8].Pere[4].txtZone07,inputForm3[8].Pere[4].txtZone00_,inputForm3[8].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[5].txtZone01,inputForm3[8].Pere[5].txtZone02,inputForm3[8].Pere[5].txtZone03,inputForm3[8].Pere[5].txtZone04,inputForm3[8].Pere[5].txtZone05,inputForm3[8].Pere[5].txtZone06,inputForm3[8].Pere[5].txtZone07,inputForm3[8].Pere[5].txtZone00_,inputForm3[8].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[6].txtZone01,inputForm3[8].Pere[6].txtZone02,inputForm3[8].Pere[6].txtZone03,inputForm3[8].Pere[6].txtZone04,inputForm3[8].Pere[6].txtZone05,inputForm3[8].Pere[6].txtZone06,inputForm3[8].Pere[6].txtZone07,inputForm3[8].Pere[6].txtZone00_,inputForm3[8].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[7].txtZone01,inputForm3[8].Pere[7].txtZone02,inputForm3[8].Pere[7].txtZone03,inputForm3[8].Pere[7].txtZone04,inputForm3[8].Pere[7].txtZone05,inputForm3[8].Pere[7].txtZone06,inputForm3[8].Pere[7].txtZone07,inputForm3[8].Pere[7].txtZone00_,inputForm3[8].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[8].txtZone01,inputForm3[8].Pere[8].txtZone02,inputForm3[8].Pere[8].txtZone03,inputForm3[8].Pere[8].txtZone04,inputForm3[8].Pere[8].txtZone05,inputForm3[8].Pere[8].txtZone06,inputForm3[8].Pere[8].txtZone07,inputForm3[8].Pere[8].txtZone00_,inputForm3[8].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Pere[9].txtZone01,inputForm3[8].Pere[9].txtZone02,inputForm3[8].Pere[9].txtZone03,inputForm3[8].Pere[9].txtZone04,inputForm3[8].Pere[9].txtZone05,inputForm3[8].Pere[9].txtZone06,inputForm3[8].Pere[9].txtZone07,inputForm3[8].Pere[9].txtZone00_,inputForm3[8].Pere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[9].Pere[0].txtZone01,inputForm3[9].Pere[0].txtZone02,inputForm3[9].Pere[0].txtZone03,inputForm3[9].Pere[0].txtZone04,inputForm3[9].Pere[0].txtZone05,inputForm3[9].Pere[0].txtZone06,inputForm3[9].Pere[0].txtZone07,inputForm3[9].Pere[0].txtZone00_,inputForm3[9].Pere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[1].txtZone01,inputForm3[9].Pere[1].txtZone02,inputForm3[9].Pere[1].txtZone03,inputForm3[9].Pere[1].txtZone04,inputForm3[9].Pere[1].txtZone05,inputForm3[9].Pere[1].txtZone06,inputForm3[9].Pere[1].txtZone07,inputForm3[9].Pere[1].txtZone00_,inputForm3[9].Pere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[2].txtZone01,inputForm3[9].Pere[2].txtZone02,inputForm3[9].Pere[2].txtZone03,inputForm3[9].Pere[2].txtZone04,inputForm3[9].Pere[2].txtZone05,inputForm3[9].Pere[2].txtZone06,inputForm3[9].Pere[2].txtZone07,inputForm3[9].Pere[2].txtZone00_,inputForm3[9].Pere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[3].txtZone01,inputForm3[9].Pere[3].txtZone02,inputForm3[9].Pere[3].txtZone03,inputForm3[9].Pere[3].txtZone04,inputForm3[9].Pere[3].txtZone05,inputForm3[9].Pere[3].txtZone06,inputForm3[9].Pere[3].txtZone07,inputForm3[9].Pere[3].txtZone00_,inputForm3[9].Pere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[4].txtZone01,inputForm3[9].Pere[4].txtZone02,inputForm3[9].Pere[4].txtZone03,inputForm3[9].Pere[4].txtZone04,inputForm3[9].Pere[4].txtZone05,inputForm3[9].Pere[4].txtZone06,inputForm3[9].Pere[4].txtZone07,inputForm3[9].Pere[4].txtZone00_,inputForm3[9].Pere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[5].txtZone01,inputForm3[9].Pere[5].txtZone02,inputForm3[9].Pere[5].txtZone03,inputForm3[9].Pere[5].txtZone04,inputForm3[9].Pere[5].txtZone05,inputForm3[9].Pere[5].txtZone06,inputForm3[9].Pere[5].txtZone07,inputForm3[9].Pere[5].txtZone00_,inputForm3[9].Pere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[6].txtZone01,inputForm3[9].Pere[6].txtZone02,inputForm3[9].Pere[6].txtZone03,inputForm3[9].Pere[6].txtZone04,inputForm3[9].Pere[6].txtZone05,inputForm3[9].Pere[6].txtZone06,inputForm3[9].Pere[6].txtZone07,inputForm3[9].Pere[6].txtZone00_,inputForm3[9].Pere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[7].txtZone01,inputForm3[9].Pere[7].txtZone02,inputForm3[9].Pere[7].txtZone03,inputForm3[9].Pere[7].txtZone04,inputForm3[9].Pere[7].txtZone05,inputForm3[9].Pere[7].txtZone06,inputForm3[9].Pere[7].txtZone07,inputForm3[9].Pere[7].txtZone00_,inputForm3[9].Pere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[8].txtZone01,inputForm3[9].Pere[8].txtZone02,inputForm3[9].Pere[8].txtZone03,inputForm3[9].Pere[8].txtZone04,inputForm3[9].Pere[8].txtZone05,inputForm3[9].Pere[8].txtZone06,inputForm3[9].Pere[8].txtZone07,inputForm3[9].Pere[8].txtZone00_,inputForm3[9].Pere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Pere[9].txtZone01,inputForm3[9].Pere[9].txtZone02,inputForm3[9].Pere[9].txtZone03,inputForm3[9].Pere[9].txtZone04,inputForm3[9].Pere[9].txtZone05,inputForm3[9].Pere[9].txtZone06,inputForm3[9].Pere[9].txtZone07,inputForm3[9].Pere[9].txtZone00_,inputForm3[9].Pere[9].chk01.ToString().ToUpper()},
                    //
                    new object[] {inputForm3[0].Mere[0].txtZone01,inputForm3[0].Mere[0].txtZone02,inputForm3[0].Mere[0].txtZone03,inputForm3[0].Mere[0].txtZone04,inputForm3[0].Mere[0].txtZone05,inputForm3[0].Mere[0].txtZone06,inputForm3[0].Mere[0].txtZone07,inputForm3[0].Mere[0].txtZone00_,inputForm3[0].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[1].txtZone01,inputForm3[0].Mere[1].txtZone02,inputForm3[0].Mere[1].txtZone03,inputForm3[0].Mere[1].txtZone04,inputForm3[0].Mere[1].txtZone05,inputForm3[0].Mere[1].txtZone06,inputForm3[0].Mere[1].txtZone07,inputForm3[0].Mere[1].txtZone00_,inputForm3[0].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[2].txtZone01,inputForm3[0].Mere[2].txtZone02,inputForm3[0].Mere[2].txtZone03,inputForm3[0].Mere[2].txtZone04,inputForm3[0].Mere[2].txtZone05,inputForm3[0].Mere[2].txtZone06,inputForm3[0].Mere[2].txtZone07,inputForm3[0].Mere[2].txtZone00_,inputForm3[0].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[3].txtZone01,inputForm3[0].Mere[3].txtZone02,inputForm3[0].Mere[3].txtZone03,inputForm3[0].Mere[3].txtZone04,inputForm3[0].Mere[3].txtZone05,inputForm3[0].Mere[3].txtZone06,inputForm3[0].Mere[3].txtZone07,inputForm3[0].Mere[3].txtZone00_,inputForm3[0].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[4].txtZone01,inputForm3[0].Mere[4].txtZone02,inputForm3[0].Mere[4].txtZone03,inputForm3[0].Mere[4].txtZone04,inputForm3[0].Mere[4].txtZone05,inputForm3[0].Mere[4].txtZone06,inputForm3[0].Mere[4].txtZone07,inputForm3[0].Mere[4].txtZone00_,inputForm3[0].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[5].txtZone01,inputForm3[0].Mere[5].txtZone02,inputForm3[0].Mere[5].txtZone03,inputForm3[0].Mere[5].txtZone04,inputForm3[0].Mere[5].txtZone05,inputForm3[0].Mere[5].txtZone06,inputForm3[0].Mere[5].txtZone07,inputForm3[0].Mere[5].txtZone00_,inputForm3[0].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[6].txtZone01,inputForm3[0].Mere[6].txtZone02,inputForm3[0].Mere[6].txtZone03,inputForm3[0].Mere[6].txtZone04,inputForm3[0].Mere[6].txtZone05,inputForm3[0].Mere[6].txtZone06,inputForm3[0].Mere[6].txtZone07,inputForm3[0].Mere[6].txtZone00_,inputForm3[0].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[7].txtZone01,inputForm3[0].Mere[7].txtZone02,inputForm3[0].Mere[7].txtZone03,inputForm3[0].Mere[7].txtZone04,inputForm3[0].Mere[7].txtZone05,inputForm3[0].Mere[7].txtZone06,inputForm3[0].Mere[7].txtZone07,inputForm3[0].Mere[7].txtZone00_,inputForm3[0].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[8].txtZone01,inputForm3[0].Mere[8].txtZone02,inputForm3[0].Mere[8].txtZone03,inputForm3[0].Mere[8].txtZone04,inputForm3[0].Mere[8].txtZone05,inputForm3[0].Mere[8].txtZone06,inputForm3[0].Mere[8].txtZone07,inputForm3[0].Mere[8].txtZone00_,inputForm3[0].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[0].Mere[9].txtZone01,inputForm3[0].Mere[9].txtZone02,inputForm3[0].Mere[9].txtZone03,inputForm3[0].Mere[9].txtZone04,inputForm3[0].Mere[9].txtZone05,inputForm3[0].Mere[9].txtZone06,inputForm3[0].Mere[9].txtZone07,inputForm3[0].Mere[9].txtZone00_,inputForm3[0].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[1].Mere[0].txtZone01,inputForm3[1].Mere[0].txtZone02,inputForm3[1].Mere[0].txtZone03,inputForm3[1].Mere[0].txtZone04,inputForm3[1].Mere[0].txtZone05,inputForm3[1].Mere[0].txtZone06,inputForm3[1].Mere[0].txtZone07,inputForm3[1].Mere[0].txtZone00_,inputForm3[1].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[1].txtZone01,inputForm3[1].Mere[1].txtZone02,inputForm3[1].Mere[1].txtZone03,inputForm3[1].Mere[1].txtZone04,inputForm3[1].Mere[1].txtZone05,inputForm3[1].Mere[1].txtZone06,inputForm3[1].Mere[1].txtZone07,inputForm3[1].Mere[1].txtZone00_,inputForm3[1].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[2].txtZone01,inputForm3[1].Mere[2].txtZone02,inputForm3[1].Mere[2].txtZone03,inputForm3[1].Mere[2].txtZone04,inputForm3[1].Mere[2].txtZone05,inputForm3[1].Mere[2].txtZone06,inputForm3[1].Mere[2].txtZone07,inputForm3[1].Mere[2].txtZone00_,inputForm3[1].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[3].txtZone01,inputForm3[1].Mere[3].txtZone02,inputForm3[1].Mere[3].txtZone03,inputForm3[1].Mere[3].txtZone04,inputForm3[1].Mere[3].txtZone05,inputForm3[1].Mere[3].txtZone06,inputForm3[1].Mere[3].txtZone07,inputForm3[1].Mere[3].txtZone00_,inputForm3[1].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[4].txtZone01,inputForm3[1].Mere[4].txtZone02,inputForm3[1].Mere[4].txtZone03,inputForm3[1].Mere[4].txtZone04,inputForm3[1].Mere[4].txtZone05,inputForm3[1].Mere[4].txtZone06,inputForm3[1].Mere[4].txtZone07,inputForm3[1].Mere[4].txtZone00_,inputForm3[1].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[5].txtZone01,inputForm3[1].Mere[5].txtZone02,inputForm3[1].Mere[5].txtZone03,inputForm3[1].Mere[5].txtZone04,inputForm3[1].Mere[5].txtZone05,inputForm3[1].Mere[5].txtZone06,inputForm3[1].Mere[5].txtZone07,inputForm3[1].Mere[5].txtZone00_,inputForm3[1].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[6].txtZone01,inputForm3[1].Mere[6].txtZone02,inputForm3[1].Mere[6].txtZone03,inputForm3[1].Mere[6].txtZone04,inputForm3[1].Mere[6].txtZone05,inputForm3[1].Mere[6].txtZone06,inputForm3[1].Mere[6].txtZone07,inputForm3[1].Mere[6].txtZone00_,inputForm3[1].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[7].txtZone01,inputForm3[1].Mere[7].txtZone02,inputForm3[1].Mere[7].txtZone03,inputForm3[1].Mere[7].txtZone04,inputForm3[1].Mere[7].txtZone05,inputForm3[1].Mere[7].txtZone06,inputForm3[1].Mere[7].txtZone07,inputForm3[1].Mere[7].txtZone00_,inputForm3[1].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[8].txtZone01,inputForm3[1].Mere[8].txtZone02,inputForm3[1].Mere[8].txtZone03,inputForm3[1].Mere[8].txtZone04,inputForm3[1].Mere[8].txtZone05,inputForm3[1].Mere[8].txtZone06,inputForm3[1].Mere[8].txtZone07,inputForm3[1].Mere[8].txtZone00_,inputForm3[1].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[1].Mere[9].txtZone01,inputForm3[1].Mere[9].txtZone02,inputForm3[1].Mere[9].txtZone03,inputForm3[1].Mere[9].txtZone04,inputForm3[1].Mere[9].txtZone05,inputForm3[1].Mere[9].txtZone06,inputForm3[1].Mere[9].txtZone07,inputForm3[1].Mere[9].txtZone00_,inputForm3[1].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[2].Mere[0].txtZone01,inputForm3[2].Mere[0].txtZone02,inputForm3[2].Mere[0].txtZone03,inputForm3[2].Mere[0].txtZone04,inputForm3[2].Mere[0].txtZone05,inputForm3[2].Mere[0].txtZone06,inputForm3[2].Mere[0].txtZone07,inputForm3[2].Mere[0].txtZone00_,inputForm3[2].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[1].txtZone01,inputForm3[2].Mere[1].txtZone02,inputForm3[2].Mere[1].txtZone03,inputForm3[2].Mere[1].txtZone04,inputForm3[2].Mere[1].txtZone05,inputForm3[2].Mere[1].txtZone06,inputForm3[2].Mere[1].txtZone07,inputForm3[2].Mere[1].txtZone00_,inputForm3[2].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[2].txtZone01,inputForm3[2].Mere[2].txtZone02,inputForm3[2].Mere[2].txtZone03,inputForm3[2].Mere[2].txtZone04,inputForm3[2].Mere[2].txtZone05,inputForm3[2].Mere[2].txtZone06,inputForm3[2].Mere[2].txtZone07,inputForm3[2].Mere[2].txtZone00_,inputForm3[2].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[3].txtZone01,inputForm3[2].Mere[3].txtZone02,inputForm3[2].Mere[3].txtZone03,inputForm3[2].Mere[3].txtZone04,inputForm3[2].Mere[3].txtZone05,inputForm3[2].Mere[3].txtZone06,inputForm3[2].Mere[3].txtZone07,inputForm3[2].Mere[3].txtZone00_,inputForm3[2].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[4].txtZone01,inputForm3[2].Mere[4].txtZone02,inputForm3[2].Mere[4].txtZone03,inputForm3[2].Mere[4].txtZone04,inputForm3[2].Mere[4].txtZone05,inputForm3[2].Mere[4].txtZone06,inputForm3[2].Mere[4].txtZone07,inputForm3[2].Mere[4].txtZone00_,inputForm3[2].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[5].txtZone01,inputForm3[2].Mere[5].txtZone02,inputForm3[2].Mere[5].txtZone03,inputForm3[2].Mere[5].txtZone04,inputForm3[2].Mere[5].txtZone05,inputForm3[2].Mere[5].txtZone06,inputForm3[2].Mere[5].txtZone07,inputForm3[2].Mere[5].txtZone00_,inputForm3[2].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[6].txtZone01,inputForm3[2].Mere[6].txtZone02,inputForm3[2].Mere[6].txtZone03,inputForm3[2].Mere[6].txtZone04,inputForm3[2].Mere[6].txtZone05,inputForm3[2].Mere[6].txtZone06,inputForm3[2].Mere[6].txtZone07,inputForm3[2].Mere[6].txtZone00_,inputForm3[2].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[7].txtZone01,inputForm3[2].Mere[7].txtZone02,inputForm3[2].Mere[7].txtZone03,inputForm3[2].Mere[7].txtZone04,inputForm3[2].Mere[7].txtZone05,inputForm3[2].Mere[7].txtZone06,inputForm3[2].Mere[7].txtZone07,inputForm3[2].Mere[7].txtZone00_,inputForm3[2].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[8].txtZone01,inputForm3[2].Mere[8].txtZone02,inputForm3[2].Mere[8].txtZone03,inputForm3[2].Mere[8].txtZone04,inputForm3[2].Mere[8].txtZone05,inputForm3[2].Mere[8].txtZone06,inputForm3[2].Mere[8].txtZone07,inputForm3[2].Mere[8].txtZone00_,inputForm3[2].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[2].Mere[9].txtZone01,inputForm3[2].Mere[9].txtZone02,inputForm3[2].Mere[9].txtZone03,inputForm3[2].Mere[9].txtZone04,inputForm3[2].Mere[9].txtZone05,inputForm3[2].Mere[9].txtZone06,inputForm3[2].Mere[9].txtZone07,inputForm3[2].Mere[9].txtZone00_,inputForm3[2].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[3].Mere[0].txtZone01,inputForm3[3].Mere[0].txtZone02,inputForm3[3].Mere[0].txtZone03,inputForm3[3].Mere[0].txtZone04,inputForm3[3].Mere[0].txtZone05,inputForm3[3].Mere[0].txtZone06,inputForm3[3].Mere[0].txtZone07,inputForm3[3].Mere[0].txtZone00_,inputForm3[3].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[1].txtZone01,inputForm3[3].Mere[1].txtZone02,inputForm3[3].Mere[1].txtZone03,inputForm3[3].Mere[1].txtZone04,inputForm3[3].Mere[1].txtZone05,inputForm3[3].Mere[1].txtZone06,inputForm3[3].Mere[1].txtZone07,inputForm3[3].Mere[1].txtZone00_,inputForm3[3].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[2].txtZone01,inputForm3[3].Mere[2].txtZone02,inputForm3[3].Mere[2].txtZone03,inputForm3[3].Mere[2].txtZone04,inputForm3[3].Mere[2].txtZone05,inputForm3[3].Mere[2].txtZone06,inputForm3[3].Mere[2].txtZone07,inputForm3[3].Mere[2].txtZone00_,inputForm3[3].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[3].txtZone01,inputForm3[3].Mere[3].txtZone02,inputForm3[3].Mere[3].txtZone03,inputForm3[3].Mere[3].txtZone04,inputForm3[3].Mere[3].txtZone05,inputForm3[3].Mere[3].txtZone06,inputForm3[3].Mere[3].txtZone07,inputForm3[3].Mere[3].txtZone00_,inputForm3[3].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[4].txtZone01,inputForm3[3].Mere[4].txtZone02,inputForm3[3].Mere[4].txtZone03,inputForm3[3].Mere[4].txtZone04,inputForm3[3].Mere[4].txtZone05,inputForm3[3].Mere[4].txtZone06,inputForm3[3].Mere[4].txtZone07,inputForm3[3].Mere[4].txtZone00_,inputForm3[3].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[5].txtZone01,inputForm3[3].Mere[5].txtZone02,inputForm3[3].Mere[5].txtZone03,inputForm3[3].Mere[5].txtZone04,inputForm3[3].Mere[5].txtZone05,inputForm3[3].Mere[5].txtZone06,inputForm3[3].Mere[5].txtZone07,inputForm3[3].Mere[5].txtZone00_,inputForm3[3].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[6].txtZone01,inputForm3[3].Mere[6].txtZone02,inputForm3[3].Mere[6].txtZone03,inputForm3[3].Mere[6].txtZone04,inputForm3[3].Mere[6].txtZone05,inputForm3[3].Mere[6].txtZone06,inputForm3[3].Mere[6].txtZone07,inputForm3[3].Mere[6].txtZone00_,inputForm3[3].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[7].txtZone01,inputForm3[3].Mere[7].txtZone02,inputForm3[3].Mere[7].txtZone03,inputForm3[3].Mere[7].txtZone04,inputForm3[3].Mere[7].txtZone05,inputForm3[3].Mere[7].txtZone06,inputForm3[3].Mere[7].txtZone07,inputForm3[3].Mere[7].txtZone00_,inputForm3[3].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[8].txtZone01,inputForm3[3].Mere[8].txtZone02,inputForm3[3].Mere[8].txtZone03,inputForm3[3].Mere[8].txtZone04,inputForm3[3].Mere[8].txtZone05,inputForm3[3].Mere[8].txtZone06,inputForm3[3].Mere[8].txtZone07,inputForm3[3].Mere[8].txtZone00_,inputForm3[3].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[3].Mere[9].txtZone01,inputForm3[3].Mere[9].txtZone02,inputForm3[3].Mere[9].txtZone03,inputForm3[3].Mere[9].txtZone04,inputForm3[3].Mere[9].txtZone05,inputForm3[3].Mere[9].txtZone06,inputForm3[3].Mere[9].txtZone07,inputForm3[3].Mere[9].txtZone00_,inputForm3[3].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[4].Mere[0].txtZone01,inputForm3[4].Mere[0].txtZone02,inputForm3[4].Mere[0].txtZone03,inputForm3[4].Mere[0].txtZone04,inputForm3[4].Mere[0].txtZone05,inputForm3[4].Mere[0].txtZone06,inputForm3[4].Mere[0].txtZone07,inputForm3[4].Mere[0].txtZone00_,inputForm3[4].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[1].txtZone01,inputForm3[4].Mere[1].txtZone02,inputForm3[4].Mere[1].txtZone03,inputForm3[4].Mere[1].txtZone04,inputForm3[4].Mere[1].txtZone05,inputForm3[4].Mere[1].txtZone06,inputForm3[4].Mere[1].txtZone07,inputForm3[4].Mere[1].txtZone00_,inputForm3[4].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[2].txtZone01,inputForm3[4].Mere[2].txtZone02,inputForm3[4].Mere[2].txtZone03,inputForm3[4].Mere[2].txtZone04,inputForm3[4].Mere[2].txtZone05,inputForm3[4].Mere[2].txtZone06,inputForm3[4].Mere[2].txtZone07,inputForm3[4].Mere[2].txtZone00_,inputForm3[4].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[3].txtZone01,inputForm3[4].Mere[3].txtZone02,inputForm3[4].Mere[3].txtZone03,inputForm3[4].Mere[3].txtZone04,inputForm3[4].Mere[3].txtZone05,inputForm3[4].Mere[3].txtZone06,inputForm3[4].Mere[3].txtZone07,inputForm3[4].Mere[3].txtZone00_,inputForm3[4].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[4].txtZone01,inputForm3[4].Mere[4].txtZone02,inputForm3[4].Mere[4].txtZone03,inputForm3[4].Mere[4].txtZone04,inputForm3[4].Mere[4].txtZone05,inputForm3[4].Mere[4].txtZone06,inputForm3[4].Mere[4].txtZone07,inputForm3[4].Mere[4].txtZone00_,inputForm3[4].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[5].txtZone01,inputForm3[4].Mere[5].txtZone02,inputForm3[4].Mere[5].txtZone03,inputForm3[4].Mere[5].txtZone04,inputForm3[4].Mere[5].txtZone05,inputForm3[4].Mere[5].txtZone06,inputForm3[4].Mere[5].txtZone07,inputForm3[4].Mere[5].txtZone00_,inputForm3[4].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[6].txtZone01,inputForm3[4].Mere[6].txtZone02,inputForm3[4].Mere[6].txtZone03,inputForm3[4].Mere[6].txtZone04,inputForm3[4].Mere[6].txtZone05,inputForm3[4].Mere[6].txtZone06,inputForm3[4].Mere[6].txtZone07,inputForm3[4].Mere[6].txtZone00_,inputForm3[4].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[7].txtZone01,inputForm3[4].Mere[7].txtZone02,inputForm3[4].Mere[7].txtZone03,inputForm3[4].Mere[7].txtZone04,inputForm3[4].Mere[7].txtZone05,inputForm3[4].Mere[7].txtZone06,inputForm3[4].Mere[7].txtZone07,inputForm3[4].Mere[7].txtZone00_,inputForm3[4].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[8].txtZone01,inputForm3[4].Mere[8].txtZone02,inputForm3[4].Mere[8].txtZone03,inputForm3[4].Mere[8].txtZone04,inputForm3[4].Mere[8].txtZone05,inputForm3[4].Mere[8].txtZone06,inputForm3[4].Mere[8].txtZone07,inputForm3[4].Mere[8].txtZone00_,inputForm3[4].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[4].Mere[9].txtZone01,inputForm3[4].Mere[9].txtZone02,inputForm3[4].Mere[9].txtZone03,inputForm3[4].Mere[9].txtZone04,inputForm3[4].Mere[9].txtZone05,inputForm3[4].Mere[9].txtZone06,inputForm3[4].Mere[9].txtZone07,inputForm3[4].Mere[9].txtZone00_,inputForm3[4].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[5].Mere[0].txtZone01,inputForm3[5].Mere[0].txtZone02,inputForm3[5].Mere[0].txtZone03,inputForm3[5].Mere[0].txtZone04,inputForm3[5].Mere[0].txtZone05,inputForm3[5].Mere[0].txtZone06,inputForm3[5].Mere[0].txtZone07,inputForm3[5].Mere[0].txtZone00_,inputForm3[5].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[1].txtZone01,inputForm3[5].Mere[1].txtZone02,inputForm3[5].Mere[1].txtZone03,inputForm3[5].Mere[1].txtZone04,inputForm3[5].Mere[1].txtZone05,inputForm3[5].Mere[1].txtZone06,inputForm3[5].Mere[1].txtZone07,inputForm3[5].Mere[1].txtZone00_,inputForm3[5].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[2].txtZone01,inputForm3[5].Mere[2].txtZone02,inputForm3[5].Mere[2].txtZone03,inputForm3[5].Mere[2].txtZone04,inputForm3[5].Mere[2].txtZone05,inputForm3[5].Mere[2].txtZone06,inputForm3[5].Mere[2].txtZone07,inputForm3[5].Mere[2].txtZone00_,inputForm3[5].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[3].txtZone01,inputForm3[5].Mere[3].txtZone02,inputForm3[5].Mere[3].txtZone03,inputForm3[5].Mere[3].txtZone04,inputForm3[5].Mere[3].txtZone05,inputForm3[5].Mere[3].txtZone06,inputForm3[5].Mere[3].txtZone07,inputForm3[5].Mere[3].txtZone00_,inputForm3[5].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[4].txtZone01,inputForm3[5].Mere[4].txtZone02,inputForm3[5].Mere[4].txtZone03,inputForm3[5].Mere[4].txtZone04,inputForm3[5].Mere[4].txtZone05,inputForm3[5].Mere[4].txtZone06,inputForm3[5].Mere[4].txtZone07,inputForm3[5].Mere[4].txtZone00_,inputForm3[5].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[5].txtZone01,inputForm3[5].Mere[5].txtZone02,inputForm3[5].Mere[5].txtZone03,inputForm3[5].Mere[5].txtZone04,inputForm3[5].Mere[5].txtZone05,inputForm3[5].Mere[5].txtZone06,inputForm3[5].Mere[5].txtZone07,inputForm3[5].Mere[5].txtZone00_,inputForm3[5].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[6].txtZone01,inputForm3[5].Mere[6].txtZone02,inputForm3[5].Mere[6].txtZone03,inputForm3[5].Mere[6].txtZone04,inputForm3[5].Mere[6].txtZone05,inputForm3[5].Mere[6].txtZone06,inputForm3[5].Mere[6].txtZone07,inputForm3[5].Mere[6].txtZone00_,inputForm3[5].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[7].txtZone01,inputForm3[5].Mere[7].txtZone02,inputForm3[5].Mere[7].txtZone03,inputForm3[5].Mere[7].txtZone04,inputForm3[5].Mere[7].txtZone05,inputForm3[5].Mere[7].txtZone06,inputForm3[5].Mere[7].txtZone07,inputForm3[5].Mere[7].txtZone00_,inputForm3[5].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[8].txtZone01,inputForm3[5].Mere[8].txtZone02,inputForm3[5].Mere[8].txtZone03,inputForm3[5].Mere[8].txtZone04,inputForm3[5].Mere[8].txtZone05,inputForm3[5].Mere[8].txtZone06,inputForm3[5].Mere[8].txtZone07,inputForm3[5].Mere[8].txtZone00_,inputForm3[5].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[5].Mere[9].txtZone01,inputForm3[5].Mere[9].txtZone02,inputForm3[5].Mere[9].txtZone03,inputForm3[5].Mere[9].txtZone04,inputForm3[5].Mere[9].txtZone05,inputForm3[5].Mere[9].txtZone06,inputForm3[5].Mere[9].txtZone07,inputForm3[5].Mere[9].txtZone00_,inputForm3[5].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[6].Mere[0].txtZone01,inputForm3[6].Mere[0].txtZone02,inputForm3[6].Mere[0].txtZone03,inputForm3[6].Mere[0].txtZone04,inputForm3[6].Mere[0].txtZone05,inputForm3[6].Mere[0].txtZone06,inputForm3[6].Mere[0].txtZone07,inputForm3[6].Mere[0].txtZone00_,inputForm3[6].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[1].txtZone01,inputForm3[6].Mere[1].txtZone02,inputForm3[6].Mere[1].txtZone03,inputForm3[6].Mere[1].txtZone04,inputForm3[6].Mere[1].txtZone05,inputForm3[6].Mere[1].txtZone06,inputForm3[6].Mere[1].txtZone07,inputForm3[6].Mere[1].txtZone00_,inputForm3[6].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[2].txtZone01,inputForm3[6].Mere[2].txtZone02,inputForm3[6].Mere[2].txtZone03,inputForm3[6].Mere[2].txtZone04,inputForm3[6].Mere[2].txtZone05,inputForm3[6].Mere[2].txtZone06,inputForm3[6].Mere[2].txtZone07,inputForm3[6].Mere[2].txtZone00_,inputForm3[6].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[3].txtZone01,inputForm3[6].Mere[3].txtZone02,inputForm3[6].Mere[3].txtZone03,inputForm3[6].Mere[3].txtZone04,inputForm3[6].Mere[3].txtZone05,inputForm3[6].Mere[3].txtZone06,inputForm3[6].Mere[3].txtZone07,inputForm3[6].Mere[3].txtZone00_,inputForm3[6].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[4].txtZone01,inputForm3[6].Mere[4].txtZone02,inputForm3[6].Mere[4].txtZone03,inputForm3[6].Mere[4].txtZone04,inputForm3[6].Mere[4].txtZone05,inputForm3[6].Mere[4].txtZone06,inputForm3[6].Mere[4].txtZone07,inputForm3[6].Mere[4].txtZone00_,inputForm3[6].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[5].txtZone01,inputForm3[6].Mere[5].txtZone02,inputForm3[6].Mere[5].txtZone03,inputForm3[6].Mere[5].txtZone04,inputForm3[6].Mere[5].txtZone05,inputForm3[6].Mere[5].txtZone06,inputForm3[6].Mere[5].txtZone07,inputForm3[6].Mere[5].txtZone00_,inputForm3[6].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[6].txtZone01,inputForm3[6].Mere[6].txtZone02,inputForm3[6].Mere[6].txtZone03,inputForm3[6].Mere[6].txtZone04,inputForm3[6].Mere[6].txtZone05,inputForm3[6].Mere[6].txtZone06,inputForm3[6].Mere[6].txtZone07,inputForm3[6].Mere[6].txtZone00_,inputForm3[6].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[7].txtZone01,inputForm3[6].Mere[7].txtZone02,inputForm3[6].Mere[7].txtZone03,inputForm3[6].Mere[7].txtZone04,inputForm3[6].Mere[7].txtZone05,inputForm3[6].Mere[7].txtZone06,inputForm3[6].Mere[7].txtZone07,inputForm3[6].Mere[7].txtZone00_,inputForm3[6].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[8].txtZone01,inputForm3[6].Mere[8].txtZone02,inputForm3[6].Mere[8].txtZone03,inputForm3[6].Mere[8].txtZone04,inputForm3[6].Mere[8].txtZone05,inputForm3[6].Mere[8].txtZone06,inputForm3[6].Mere[8].txtZone07,inputForm3[6].Mere[8].txtZone00_,inputForm3[6].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[6].Mere[9].txtZone01,inputForm3[6].Mere[9].txtZone02,inputForm3[6].Mere[9].txtZone03,inputForm3[6].Mere[9].txtZone04,inputForm3[6].Mere[9].txtZone05,inputForm3[6].Mere[9].txtZone06,inputForm3[6].Mere[9].txtZone07,inputForm3[6].Mere[9].txtZone00_,inputForm3[6].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[7].Mere[0].txtZone01,inputForm3[7].Mere[0].txtZone02,inputForm3[7].Mere[0].txtZone03,inputForm3[7].Mere[0].txtZone04,inputForm3[7].Mere[0].txtZone05,inputForm3[7].Mere[0].txtZone06,inputForm3[7].Mere[0].txtZone07,inputForm3[7].Mere[0].txtZone00_,inputForm3[7].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[1].txtZone01,inputForm3[7].Mere[1].txtZone02,inputForm3[7].Mere[1].txtZone03,inputForm3[7].Mere[1].txtZone04,inputForm3[7].Mere[1].txtZone05,inputForm3[7].Mere[1].txtZone06,inputForm3[7].Mere[1].txtZone07,inputForm3[7].Mere[1].txtZone00_,inputForm3[7].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[2].txtZone01,inputForm3[7].Mere[2].txtZone02,inputForm3[7].Mere[2].txtZone03,inputForm3[7].Mere[2].txtZone04,inputForm3[7].Mere[2].txtZone05,inputForm3[7].Mere[2].txtZone06,inputForm3[7].Mere[2].txtZone07,inputForm3[7].Mere[2].txtZone00_,inputForm3[7].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[3].txtZone01,inputForm3[7].Mere[3].txtZone02,inputForm3[7].Mere[3].txtZone03,inputForm3[7].Mere[3].txtZone04,inputForm3[7].Mere[3].txtZone05,inputForm3[7].Mere[3].txtZone06,inputForm3[7].Mere[3].txtZone07,inputForm3[7].Mere[3].txtZone00_,inputForm3[7].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[4].txtZone01,inputForm3[7].Mere[4].txtZone02,inputForm3[7].Mere[4].txtZone03,inputForm3[7].Mere[4].txtZone04,inputForm3[7].Mere[4].txtZone05,inputForm3[7].Mere[4].txtZone06,inputForm3[7].Mere[4].txtZone07,inputForm3[7].Mere[4].txtZone00_,inputForm3[7].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[5].txtZone01,inputForm3[7].Mere[5].txtZone02,inputForm3[7].Mere[5].txtZone03,inputForm3[7].Mere[5].txtZone04,inputForm3[7].Mere[5].txtZone05,inputForm3[7].Mere[5].txtZone06,inputForm3[7].Mere[5].txtZone07,inputForm3[7].Mere[5].txtZone00_,inputForm3[7].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[6].txtZone01,inputForm3[7].Mere[6].txtZone02,inputForm3[7].Mere[6].txtZone03,inputForm3[7].Mere[6].txtZone04,inputForm3[7].Mere[6].txtZone05,inputForm3[7].Mere[6].txtZone06,inputForm3[7].Mere[6].txtZone07,inputForm3[7].Mere[6].txtZone00_,inputForm3[7].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[7].txtZone01,inputForm3[7].Mere[7].txtZone02,inputForm3[7].Mere[7].txtZone03,inputForm3[7].Mere[7].txtZone04,inputForm3[7].Mere[7].txtZone05,inputForm3[7].Mere[7].txtZone06,inputForm3[7].Mere[7].txtZone07,inputForm3[7].Mere[7].txtZone00_,inputForm3[7].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[8].txtZone01,inputForm3[7].Mere[8].txtZone02,inputForm3[7].Mere[8].txtZone03,inputForm3[7].Mere[8].txtZone04,inputForm3[7].Mere[8].txtZone05,inputForm3[7].Mere[8].txtZone06,inputForm3[7].Mere[8].txtZone07,inputForm3[7].Mere[8].txtZone00_,inputForm3[7].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[7].Mere[9].txtZone01,inputForm3[7].Mere[9].txtZone02,inputForm3[7].Mere[9].txtZone03,inputForm3[7].Mere[9].txtZone04,inputForm3[7].Mere[9].txtZone05,inputForm3[7].Mere[9].txtZone06,inputForm3[7].Mere[9].txtZone07,inputForm3[7].Mere[9].txtZone00_,inputForm3[7].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[8].Mere[0].txtZone01,inputForm3[8].Mere[0].txtZone02,inputForm3[8].Mere[0].txtZone03,inputForm3[8].Mere[0].txtZone04,inputForm3[8].Mere[0].txtZone05,inputForm3[8].Mere[0].txtZone06,inputForm3[8].Mere[0].txtZone07,inputForm3[8].Mere[0].txtZone00_,inputForm3[8].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[1].txtZone01,inputForm3[8].Mere[1].txtZone02,inputForm3[8].Mere[1].txtZone03,inputForm3[8].Mere[1].txtZone04,inputForm3[8].Mere[1].txtZone05,inputForm3[8].Mere[1].txtZone06,inputForm3[8].Mere[1].txtZone07,inputForm3[8].Mere[1].txtZone00_,inputForm3[8].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[2].txtZone01,inputForm3[8].Mere[2].txtZone02,inputForm3[8].Mere[2].txtZone03,inputForm3[8].Mere[2].txtZone04,inputForm3[8].Mere[2].txtZone05,inputForm3[8].Mere[2].txtZone06,inputForm3[8].Mere[2].txtZone07,inputForm3[8].Mere[2].txtZone00_,inputForm3[8].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[3].txtZone01,inputForm3[8].Mere[3].txtZone02,inputForm3[8].Mere[3].txtZone03,inputForm3[8].Mere[3].txtZone04,inputForm3[8].Mere[3].txtZone05,inputForm3[8].Mere[3].txtZone06,inputForm3[8].Mere[3].txtZone07,inputForm3[8].Mere[3].txtZone00_,inputForm3[8].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[4].txtZone01,inputForm3[8].Mere[4].txtZone02,inputForm3[8].Mere[4].txtZone03,inputForm3[8].Mere[4].txtZone04,inputForm3[8].Mere[4].txtZone05,inputForm3[8].Mere[4].txtZone06,inputForm3[8].Mere[4].txtZone07,inputForm3[8].Mere[4].txtZone00_,inputForm3[8].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[5].txtZone01,inputForm3[8].Mere[5].txtZone02,inputForm3[8].Mere[5].txtZone03,inputForm3[8].Mere[5].txtZone04,inputForm3[8].Mere[5].txtZone05,inputForm3[8].Mere[5].txtZone06,inputForm3[8].Mere[5].txtZone07,inputForm3[8].Mere[5].txtZone00_,inputForm3[8].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[6].txtZone01,inputForm3[8].Mere[6].txtZone02,inputForm3[8].Mere[6].txtZone03,inputForm3[8].Mere[6].txtZone04,inputForm3[8].Mere[6].txtZone05,inputForm3[8].Mere[6].txtZone06,inputForm3[8].Mere[6].txtZone07,inputForm3[8].Mere[6].txtZone00_,inputForm3[8].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[7].txtZone01,inputForm3[8].Mere[7].txtZone02,inputForm3[8].Mere[7].txtZone03,inputForm3[8].Mere[7].txtZone04,inputForm3[8].Mere[7].txtZone05,inputForm3[8].Mere[7].txtZone06,inputForm3[8].Mere[7].txtZone07,inputForm3[8].Mere[7].txtZone00_,inputForm3[8].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[8].txtZone01,inputForm3[8].Mere[8].txtZone02,inputForm3[8].Mere[8].txtZone03,inputForm3[8].Mere[8].txtZone04,inputForm3[8].Mere[8].txtZone05,inputForm3[8].Mere[8].txtZone06,inputForm3[8].Mere[8].txtZone07,inputForm3[8].Mere[8].txtZone00_,inputForm3[8].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[8].Mere[9].txtZone01,inputForm3[8].Mere[9].txtZone02,inputForm3[8].Mere[9].txtZone03,inputForm3[8].Mere[9].txtZone04,inputForm3[8].Mere[9].txtZone05,inputForm3[8].Mere[9].txtZone06,inputForm3[8].Mere[9].txtZone07,inputForm3[8].Mere[9].txtZone00_,inputForm3[8].Mere[9].chk01.ToString().ToUpper()},

                    new object[] {inputForm3[9].Mere[0].txtZone01,inputForm3[9].Mere[0].txtZone02,inputForm3[9].Mere[0].txtZone03,inputForm3[9].Mere[0].txtZone04,inputForm3[9].Mere[0].txtZone05,inputForm3[9].Mere[0].txtZone06,inputForm3[9].Mere[0].txtZone07,inputForm3[9].Mere[0].txtZone00_,inputForm3[9].Mere[0].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[1].txtZone01,inputForm3[9].Mere[1].txtZone02,inputForm3[9].Mere[1].txtZone03,inputForm3[9].Mere[1].txtZone04,inputForm3[9].Mere[1].txtZone05,inputForm3[9].Mere[1].txtZone06,inputForm3[9].Mere[1].txtZone07,inputForm3[9].Mere[1].txtZone00_,inputForm3[9].Mere[1].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[2].txtZone01,inputForm3[9].Mere[2].txtZone02,inputForm3[9].Mere[2].txtZone03,inputForm3[9].Mere[2].txtZone04,inputForm3[9].Mere[2].txtZone05,inputForm3[9].Mere[2].txtZone06,inputForm3[9].Mere[2].txtZone07,inputForm3[9].Mere[2].txtZone00_,inputForm3[9].Mere[2].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[3].txtZone01,inputForm3[9].Mere[3].txtZone02,inputForm3[9].Mere[3].txtZone03,inputForm3[9].Mere[3].txtZone04,inputForm3[9].Mere[3].txtZone05,inputForm3[9].Mere[3].txtZone06,inputForm3[9].Mere[3].txtZone07,inputForm3[9].Mere[3].txtZone00_,inputForm3[9].Mere[3].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[4].txtZone01,inputForm3[9].Mere[4].txtZone02,inputForm3[9].Mere[4].txtZone03,inputForm3[9].Mere[4].txtZone04,inputForm3[9].Mere[4].txtZone05,inputForm3[9].Mere[4].txtZone06,inputForm3[9].Mere[4].txtZone07,inputForm3[9].Mere[4].txtZone00_,inputForm3[9].Mere[4].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[5].txtZone01,inputForm3[9].Mere[5].txtZone02,inputForm3[9].Mere[5].txtZone03,inputForm3[9].Mere[5].txtZone04,inputForm3[9].Mere[5].txtZone05,inputForm3[9].Mere[5].txtZone06,inputForm3[9].Mere[5].txtZone07,inputForm3[9].Mere[5].txtZone00_,inputForm3[9].Mere[5].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[6].txtZone01,inputForm3[9].Mere[6].txtZone02,inputForm3[9].Mere[6].txtZone03,inputForm3[9].Mere[6].txtZone04,inputForm3[9].Mere[6].txtZone05,inputForm3[9].Mere[6].txtZone06,inputForm3[9].Mere[6].txtZone07,inputForm3[9].Mere[6].txtZone00_,inputForm3[9].Mere[6].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[7].txtZone01,inputForm3[9].Mere[7].txtZone02,inputForm3[9].Mere[7].txtZone03,inputForm3[9].Mere[7].txtZone04,inputForm3[9].Mere[7].txtZone05,inputForm3[9].Mere[7].txtZone06,inputForm3[9].Mere[7].txtZone07,inputForm3[9].Mere[7].txtZone00_,inputForm3[9].Mere[7].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[8].txtZone01,inputForm3[9].Mere[8].txtZone02,inputForm3[9].Mere[8].txtZone03,inputForm3[9].Mere[8].txtZone04,inputForm3[9].Mere[8].txtZone05,inputForm3[9].Mere[8].txtZone06,inputForm3[9].Mere[8].txtZone07,inputForm3[9].Mere[8].txtZone00_,inputForm3[9].Mere[8].chk01.ToString().ToUpper()},
                    new object[] {inputForm3[9].Mere[9].txtZone01,inputForm3[9].Mere[9].txtZone02,inputForm3[9].Mere[9].txtZone03,inputForm3[9].Mere[9].txtZone04,inputForm3[9].Mere[9].txtZone05,inputForm3[9].Mere[9].txtZone06,inputForm3[9].Mere[9].txtZone07,inputForm3[9].Mere[9].txtZone00_,inputForm3[9].Mere[9].chk01.ToString().ToUpper()}
                });
        }

        private static string HtmlInputForm(string strInputForm1, string strInputForm2, string strInputForm3, string dossier, string date, string redacteur)
        {
            var i = 0;
            var js = new JavaScriptSerializer();
            #region input form 1
            var form1 = js.Deserialize<DeterminationDesDonatairesModel>(strInputForm1);
            var inputForm = "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
            inputForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Rappel fiscal et civil des donations antérieures</font></b></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Références du dossier</font></b></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            inputForm += "<tr>";
            inputForm += "<td align='right' colspan='2'>Dossier :</td>";
            inputForm += "<td colspan='2'>" + dossier + "</td>";
            inputForm += "</tr>";
            inputForm += "<tr>";
            inputForm += "<td align='right' colspan='2'>Date de signature :</td>";
            inputForm += "<td colspan='2'>" + date + "</td>";
            inputForm += "</tr>";
            inputForm += "<tr>";
            inputForm += "<td align='right' colspan='2'>Rédacteur :</td>";
            inputForm += "<td colspan='2'>" + redacteur + "</td>";
            inputForm += "</tr>";
            inputForm += "<tr><td colspan='4'></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Caractéristiques de la Donation</font></b></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Date de la donation</font></b></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            inputForm += "<tr>";
            inputForm += "<td align='right' colspan='2'>Date de la donation :</td>";
            inputForm += "<td align='right'>" + form1.txtZone00 + "</td><td></td>";
            inputForm += "</tr>";
            inputForm += "<tr><td colspan='4'></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Donateur(s)</font></b></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            inputForm += "<tr>";
            inputForm += "<td align='right' colspan='2'>Nombre de donateurs :</td>";
            inputForm += "<td colspan='2'>" + form1.ddl1Text + "</td>";
            inputForm += "</tr>";
            if (form1.ddl1 == "1" || form1.ddl1 == "3")
            {
                inputForm += "<tr>" +
                    "<td align='right' colspan='2'>Date de naissance du Donateur :</td>" +
                    "<td align='right'>" + form1.txtZone01a + "</td><td></td>" +
                    "</tr>";
            }
            if (form1.ddl1 == "2" || form1.ddl1 == "3")
            {
                inputForm += "<tr>" +
                    "<td align='right' colspan='2'>Date de naissance de la Donatrice :</td>" +
                    "<td align='right'>" + form1.txtZone01b + "</td><td></td>" +
                    "</tr>";
            }
            inputForm += "<tr><td colspan='4'></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Donataires de 1er degré</font></b></td></tr>";
            inputForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            inputForm += "<tr>";
            inputForm += "<td align='right' colspan='2'>Nombre de donataires :</td>";
            inputForm += "<td align='right'>" + form1.ddl3 + "</td><td></td>";
            inputForm += "</tr>";
            var ddl3 = form1.ddl3.TransformToInt();
            for (i = 0; i < ddl3; i++)
            {
                inputForm += "<tr>";
                inputForm += i == 0 ? "<td colspan='4'>1er Donataire (Nom/Prénom) :</td>" : "<td colspan='4'>" + form1.arrDonatairesDeDegre[i].index + "ème Donataire (Nom/Prénom) :</td>";
                inputForm += "</tr>";
                inputForm += "<tr>";
                inputForm += "<td colspan='2'>" + form1.arrDonatairesDeDegre[i].txtZone01 + "</td>";
                inputForm += "<td align='right'>Majeur / émancipé : " + (form1.arrDonatairesDeDegre[i].cb2 ? "Oui" : "Non") + "</td>";
                inputForm += "<td align='right'>Abattement infirmité : " + (form1.arrDonatairesDeDegre[i].cb1 ? "Oui" : "Non") + "</td>";
                inputForm += "</tr>";
                if (form1.ddl1 == "1" || form1.ddl1 == "3")
                {
                    inputForm += "<tr>" +
                        "<td align='right' colspan='2'>Nombre de donation(s) reçue(s) du Père :</td>" +
                        "<td align='right'>" + form1.arrDonatairesDeDegre[i].ddl4 + "</td><td></td>" +
                        "</tr>";
                }
                if (form1.ddl1 == "2" || form1.ddl1 == "3")
                {
                    inputForm += "<tr>" +
                        "<td align='right' colspan='2'>Nombre de donation(s) reçue(s) de la Mère :</td>" +
                        "<td align='right'>" + form1.arrDonatairesDeDegre[i].ddl5 + "</td><td></td>" +
                        "</tr>";
                }
            }
            inputForm += "<tr><td colspan='4'></td></tr>";
            if (ddl3 > 1)
            {
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Nature de la donation</font></b></td></tr>";
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                inputForm += "<tr>";
                inputForm += "<td align='right' colspan='2'>Type de donation :</td>";
                inputForm += "<td>" + form1.ddl6 + "</td><td></td>";
                inputForm += "</tr>";
                inputForm += "<tr>";
                inputForm += "<td align='right' colspan='2'>Quotités données :</td>";
                inputForm += "<td>" + form1.ddl7 + "</td><td></td>";
                inputForm += "</tr>";
                if (form1.ddl7 == "Inégalitaires")
                {
                    inputForm += "<tr>";
                    inputForm += "<td></td><td align='right'>Quotité par défaut</td><td align='right'>Quotité choisie par le Donateur</td><td align='right'>Quotité choisie par la Donatrice</td>";
                    inputForm += "</tr>";
                    if (form1.ddl1 == "1")
                    {
                        for (i = 0; i < ddl3; i++)
                        {
                            inputForm += "<tr>" +
                                "<td>" + (i == 0 ? "1er donataire" : form1.arrNatureDeLaDonation[i].index + "ème donataire") + "</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone01 + " %</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone02 + " %</td>" +
                                "<td align='right'></td>" +
                                "</tr>";
                        }
                        inputForm += "<tr>" +
                            "<td align='right' colspan='2'>Quotité restant à attribuer :</td>" +
                            "<td align='right'>" + form1.txtZone02Total + " %</td>" +
                            "<td align='right'></td>" +
                            "</tr>";
                    }
                    else if (form1.ddl1 == "2")
                    {
                        for (i = 0; i < ddl3; i++)
                        {
                            inputForm += "<tr>" +
                                "<td>" + (i == 0 ? "1er donataire" : form1.arrNatureDeLaDonation[i].index + "ème donataire") + "</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone01 + " %</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone03 + " %</td>" +
                                "<td align='right'></td>" +
                                "</tr>";
                        }
                        inputForm += "<tr>" +
                            "<td align='right' colspan='2'>Quotité restant à attribuer :</td>" +
                            "<td align='right'>" + form1.txtZone03Total + " %</td>" +
                            "<td align='right'></td>" +
                            "</tr>";
                    }
                    else
                    {
                        for (i = 0; i < ddl3; i++)
                        {
                            inputForm += "<tr>" +
                                "<td>" + (i == 0 ? "1er donataire" : form1.arrNatureDeLaDonation[i].index + "ème donataire") + "</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone01 + " %</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone02 + " %</td>" +
                                "<td align='right'>" + form1.arrNatureDeLaDonation[i].txtZone03 + " %</td>" +
                                "</tr>";
                        }
                        inputForm += "<tr>" +
                            "<td align='right' colspan='2'>Quotité restant à attribuer :</td>" +
                            "<td align='right'>" + form1.txtZone02Total + " %</td>" +
                            "<td align='right'>" + form1.txtZone03Total + " %</td>" +
                            "</tr>";
                    }
                    inputForm += "<tr><td colspan='4'>" + form1.msgTxtZone02AndTxtZone03 + "</td></tr>";
                }
            }
            inputForm += "<tr><td colspan='4'></td></tr>";
            inputForm += "</table>";
            #endregion input form 1
            #region input form 2
            var form2 = js.Deserialize<DeterminationDesBiensModel>(strInputForm2);
            inputForm += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
            inputForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Détermination des biens</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            var count = form2.immobilierCount.TransformToInt();
            if (count > 0)
            {
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Bien immobilier</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                for (i = 0; i < count; i++)
                {
                    inputForm += "<tr><td colspan='4'>Bien Immobilier - Article " + (i + 1) + "</td></tr>";
                    inputForm += "<tr>" +
                        "<td>Origine : " + form2.arrImmobilier[i].origine + "</td>" +
                        "<td align='right'>Réserve d'usufruit : " + (form2.arrImmobilier[i].reserve ? "Oui" : "Non") + "</td>" +
                        "<td align='right'>Valeur PP :</td>" +
                        "<td align='right'>" + form2.arrImmobilier[i].valeur + " €</td>" +
                        "</tr>";
                    inputForm += "<tr>";
                    if (ddl3 > 1 && form1.ddl6 == "Donation partage")
                    {
                        inputForm += "<td align='right'>Attribution :</td>" +
                            "<td>" + form2.arrImmobilier[i].attribution + "</td>";
                    }
                    else
                    {
                        inputForm += "<td align='right' colspan='2'></td>";
                    }
                    inputForm += "<td align='right'>Passif :</td>" +
                        "<td align='right'>" + form2.arrImmobilier[i].passif + " €</td>" +
                        "</tr>";
                }
                inputForm += "<tr><td colspan='4'></td></tr>";
            }
            count = form2.immobilierexonereCount.TransformToInt();
            if (count > 0)
            {
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Bien immobilier exonéré</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                for (i = 0; i < count; i++)
                {
                    inputForm += "<tr><td colspan='4'>Bien Immobilier exonéré - Article " + (i + 1) + "</td></tr>";
                    inputForm += "<tr>" +
                        "<td colspan='4'>Type de bien : " + form2.arrImmobilierexonere[i].typedebienText + "</td>" +
                        "</tr>";
                    inputForm += "<tr>" +
                        "<td>Origine : " + form2.arrImmobilierexonere[i].origine + "</td>" +
                        "<td align='right'>Réserve d'usufruit : " + (form2.arrImmobilierexonere[i].reserve ? "Oui" : "Non") + "</td>" +
                        "<td align='right'>Valeur PP :</td>" +
                        "<td align='right'>" + form2.arrImmobilierexonere[i].valeur + " €</td>" +
                        "</tr>";
                    inputForm += "<tr>";
                    if (ddl3 > 1 && form1.ddl6 == "Donation partage")
                    {
                        inputForm += "<td align='right'>Attribution :</td>" +
                            "<td>" + form2.arrImmobilierexonere[i].attribution + "</td>";
                    }
                    else
                    {
                        inputForm += "<td align='right' colspan='2'></td>";
                    }
                    inputForm += "<td align='right'>Passif :</td>" +
                        "<td align='right'>" + form2.arrImmobilierexonere[i].passif + " €</td>" +
                        "</tr>";
                }
                inputForm += "<tr><td colspan='4'></td></tr>";
            }
            count = form2.mobilierCount.TransformToInt();
            if (count > 0)
            {
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Bien mobilier</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                for (i = 0; i < count; i++)
                {
                    inputForm += "<tr><td colspan='4'>Bien Mobilier - Article " + (i + 1) + "</td></tr>";
                    inputForm += "<tr>" +
                        "<td>Origine : " + form2.arrMobilier[i].origine + "</td>" +
                        "<td align='right'>Réserve d'usufruit : " + (form2.arrMobilier[i].reserve ? "Oui" : "Non") + "</td>" +
                        "<td align='right'>Valeur PP :</td>" +
                        "<td align='right'>" + form2.arrMobilier[i].valeur + " €</td>" +
                        "</tr>";
                    inputForm += "<tr>";
                    if (ddl3 > 1 && form1.ddl6 == "Donation partage")
                    {
                        inputForm += "<td align='right'>Attribution :</td>" +
                            "<td>" + form2.arrMobilier[i].attribution + "</td>";
                    }
                    else
                    {
                        inputForm += "<td align='right' colspan='2'></td>";
                    }
                    inputForm += "<td align='right'>Passif :</td>" +
                        "<td align='right'>" + form2.arrMobilier[i].passif + " €</td>" +
                        "</tr>";
                }
                inputForm += "<tr><td colspan='4'></td></tr>";
            }
            count = form2.sommeargentCount.TransformToInt();
            if (count > 0)
            {
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Sommes d'argent éventuellement exonérées (Art. 790 du CGI)</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                for (i = 0; i < count; i++)
                {
                    inputForm += "<tr><td colspan='4'>Somme d'argent - Article " + (i + 1) + "</td></tr>";
                    inputForm += "<tr>" +
                        "<td colspan='2'>Origine : " + form2.arrSommeargent[i].origine + "</td>" +
                        "<td align='right'>Valeur PP :</td>" +
                        "<td align='right'>" + form2.arrSommeargent[i].valeur + " €</td>" +
                        "</tr>";
                    if (ddl3 > 1 && form1.ddl6 == "Donation partage")
                    {
                        inputForm += "<tr>" +
                        "<td align='right'>Attribution :</td>" +
                        "<td>" + form2.arrSommeargent[i].attribution + "</td>" +
                        "<td colspan='2'></td>" +
                        "</tr>";
                    }
                }
                inputForm += "<tr><td colspan='4'></td></tr>";
            }
            count = form2.mobilierexonereCount.TransformToInt();
            if (count > 0)
            {
                inputForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Bien mobilier exonéré</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                for (i = 0; i < count; i++)
                {
                    inputForm += "<tr><td colspan='4'>Bien Mobilier exonéré - Article " + (i + 1) + "</td></tr>";
                    inputForm += "<tr>" +
                        "<td colspan='4'>Type de bien : " + form2.arrMobilierexonere[i].typedebienText + "</td>" +
                        "</tr>";
                    inputForm += "<tr>" +
                        "<td>Origine : " + form2.arrMobilierexonere[i].origine + "</td>" +
                        "<td align='right'>Réserve d'usufruit : " + (form2.arrMobilierexonere[i].reserve ? "Oui" : "Non") + "</td>" +
                        "<td align='right'>Valeur PP :</td>" +
                        "<td align='right'>" + form2.arrMobilierexonere[i].valeur + " €</td>" +
                        "</tr>";
                    inputForm += "<tr>";
                    if (ddl3 > 1 && form1.ddl6 == "Donation partage")
                    {
                        inputForm += "<td align='right'>Attribution :</td>" +
                            "<td>" + form2.arrMobilierexonere[i].attribution + "</td>";
                    }
                    else
                    {
                        inputForm += "<td align='right' colspan='2'></td>";
                    }
                    inputForm += "<td align='right'>Passif :</td>" +
                        "<td align='right'>" + form2.arrMobilierexonere[i].passif + " €</td>" +
                        "</tr>";
                }
                inputForm += "<tr><td colspan='4'></td></tr>";
            }
            inputForm += "</table>";
            #endregion input form 2
            return inputForm;
        }

        #endregion private/public method

        #region Calculation
        protected void btnSynthese_Click(object sender, EventArgs e)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //_excelService = ExcelServiceHelper.ExcelServiceProvider();
            //_sessionId = ExcelServiceHelper.GetSessionId(_excelService, "http://80.14.175.214/Documents%20partages/DONATION01.xlsm", out _status);
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetRangeValueToExcelService();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //var height = 61;
            //switch (int.Parse(ddl3.SelectedValue))
            //{
            //    case 1:
            //        height = 171;
            //        break;
            //    case 2:
            //        height = 281;
            //        break;
            //    case 3:
            //        height = 391;
            //        break;
            //    case 4:
            //        height = 501;
            //        break;
            //    case 5:
            //        height = 611;
            //        break;
            //    case 6:
            //        height = 721;
            //        break;
            //    case 7:
            //        height = 831;
            //        break;
            //    case 8:
            //        height = 941;
            //        break;
            //    case 9:
            //        height = 1051;
            //        break;
            //    case 10:
            //        height = 1161;
            //        break;
            //    default:
            //        break;
            //}
            //var dt = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 9, 2, height, 6, out _status);
            //var dt2 = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 1170, 2, 54, 6, out _status);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetValues(dt, dt2);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Assign_values + elapsedMs + Resource.Milliseconds;

            //Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
        }

        protected void btnSyntheseTaxePere_Click(object sender, EventArgs e)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //_excelService = ExcelServiceHelper.ExcelServiceProvider();
            //_sessionId = ExcelServiceHelper.GetSessionId(_excelService, "http://80.14.175.214/Documents%20partages/DONATION01.xlsm", out _status);
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetRangeValueToExcelService();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //var height = 0;
            //switch (int.Parse(ddl3.SelectedValue))
            //{
            //    case 1:
            //        height = 65;
            //        break;
            //    case 2:
            //        height = 130;
            //        break;
            //    case 3:
            //        height = 195;
            //        break;
            //    case 4:
            //        height = 260;
            //        break;
            //    case 5:
            //        height = 325;
            //        break;
            //    case 6:
            //        height = 390;
            //        break;
            //    case 7:
            //        height = 455;
            //        break;
            //    case 8:
            //        height = 520;
            //        break;
            //    case 9:
            //        height = 585;
            //        break;
            //    case 10:
            //        height = 650;
            //        break;
            //    default:
            //        break;
            //}
            //var dt = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S1", 9, 5, height, 5, out _status); //GetRangeValueFromExcelServiceFromSheet_SORTIE_S1();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetValues_SyntheseTaxePere(dt);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSyntheseTaxePere'));", true);
        }

        protected void btnSyntheseTaxeMere_Click(object sender, EventArgs e)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //_excelService = ExcelServiceHelper.ExcelServiceProvider();
            //_sessionId = ExcelServiceHelper.GetSessionId(_excelService, "http://80.14.175.214/Documents%20partages/DONATION01.xlsm", out _status);
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetRangeValueToExcelService();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //var height = 0;
            //switch (int.Parse(ddl3.SelectedValue))
            //{
            //    case 1:
            //        height = 65;
            //        break;
            //    case 2:
            //        height = 130;
            //        break;
            //    case 3:
            //        height = 195;
            //        break;
            //    case 4:
            //        height = 260;
            //        break;
            //    case 5:
            //        height = 325;
            //        break;
            //    case 6:
            //        height = 390;
            //        break;
            //    case 7:
            //        height = 455;
            //        break;
            //    case 8:
            //        height = 520;
            //        break;
            //    case 9:
            //        height = 585;
            //        break;
            //    case 10:
            //        height = 650;
            //        break;
            //    default:
            //        break;
            //}
            //var dt = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S2", 9, 5, height, 5, out _status); //GetRangeValueFromExcelServiceFromSheet_SORTIE_S2();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetValues_SyntheseTaxeMere(dt);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSyntheseTaxeMere'));", true);
        }

        protected void btnTaxeDonation_Click(object sender, EventArgs e)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //_excelService = ExcelServiceHelper.ExcelServiceProvider();
            //_sessionId = ExcelServiceHelper.GetSessionId(_excelService, "http://80.14.175.214/Documents%20partages/DONATION01.xlsm", out _status);
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = Resource.Connect + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetRangeValueToExcelService();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Set_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //var dt = ExcelServiceHelper.GetRange(_excelService, _sessionId, "SORTIE_S", 1224, 2, 69, 6, out _status); // GetRangeValueFromExcelService();
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //watch = System.Diagnostics.Stopwatch.StartNew();
            //SetValues_TaxeDonation(dt);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //lblShowProcessTime.InnerText = lblShowProcessTime.InnerText + "; " + Resource.Get_range + elapsedMs + Resource.Milliseconds;

            //Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnTaxeDonation'));", true);
        }

        #endregion Calculation

        #region WebMethod

        [WebMethod]
        public static object Synthese(string strInputForm1, string strInputForm2, string strInputForm3, int height)
        {
            SetRangeValueToExcelService(strInputForm1, strInputForm2, strInputForm3, out _excelService, out _sessionId);
            var result1 = ExcelServiceHelper.GetRangeOrigin(_excelService, _sessionId, "SORTIE_S", 9, 2, height, 6, out _status);
            var result2 = ExcelServiceHelper.GetRangeOrigin(_excelService, _sessionId, "SORTIE_S", 1170, 2, 54, 6, out _status);
            return new { result1, result2 };
        }

        [WebMethod]
        public static object[] SyntheseTaxePere(string strInputForm1, string strInputForm2, string strInputForm3, int height)
        {
            SetRangeValueToExcelService(strInputForm1, strInputForm2, strInputForm3, out _excelService, out _sessionId);
            var result = ExcelServiceHelper.GetRangeOrigin(_excelService, _sessionId, "SORTIE_S1", 9, 5, height, 5, out _status);
            return result;
        }

        [WebMethod]
        public static object[] SyntheseTaxeMere(string strInputForm1, string strInputForm2, string strInputForm3, int height)
        {
            SetRangeValueToExcelService(strInputForm1, strInputForm2, strInputForm3, out _excelService, out _sessionId);
            var result = ExcelServiceHelper.GetRangeOrigin(_excelService, _sessionId, "SORTIE_S2", 9, 5, height, 5, out _status);
            return result;
        }

        [WebMethod]
        public static object[] TaxeDonation(string strInputForm1, string strInputForm2, string strInputForm3)
        {
            SetRangeValueToExcelService(strInputForm1, strInputForm2, strInputForm3, out _excelService, out _sessionId);
            var result = ExcelServiceHelper.GetRangeOrigin(_excelService, _sessionId, "SORTIE_S", 1224, 2, 69, 6, out _status);
            return result;
        }

        [WebMethod]
        public static object[] SynthaseDonation(string strInputForm1, string strInputForm2, string strInputForm3, int row, int column)
        {
            SetRangeValueToExcelService(strInputForm1, strInputForm2, strInputForm3, out _excelService, out _sessionId);
            return ExcelServiceHelper.GetRangeOrigin(_excelService, _sessionId, "SORTIE_DON", row, column, 7, 1, out _status);
        }

        [WebMethod]
        public static string HtmlReportSynthese(string strInputForm1, string strInputForm2, string strInputForm3, string dossier, string date, string redacteur, string result1, string result2)
        {
            var inputForm = HtmlInputForm(strInputForm1, strInputForm2, strInputForm3, dossier, date, redacteur);
            #region result
            var index = 0; var i = 0; var k = 0;
            var js = new JavaScriptSerializer();
            var inputForm1 = js.Deserialize<DeterminationDesDonatairesModel>(strInputForm1);
            var inputForm2 = js.Deserialize<DeterminationDesBiensModel>(strInputForm2);
            var jsonResult1 = js.Deserialize<object[]>(result1);
            var jsonResult2 = js.Deserialize<object[]>(result2);
            var resultForm = "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
            //MASSE DONNEE EN NUE PROPRIETE
            var data = (object[])jsonResult1[0];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>MASSE DONNEE EN NUE PROPRIETE</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                resultForm += "<tr>";
                resultForm += "<td align='center' colspan='4'>Cette masse a été calculée en fonction de l'age du Donateur sur les bases de l'article 669 du CGI.</td>";
                resultForm += "</tr>";
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Age</td>";
                resultForm += "<td align='right'>Taux</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult1[1];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Donateur :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[2];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Donatrice :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Situation active nette</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Donateur</td>";
                resultForm += "<td align='right'>Donatrice</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult1[3];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens immobiliers taxables :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[4];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens immobiliers exonérés :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[5];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens mobiliers taxables :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[6];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens mobiliers exonérés :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[7];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Rapport dons en avance sur part :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[8];
                resultForm += "<tr>";
                resultForm += "<td align='right'>Total de l'actif net :</td>";
                resultForm += "<td align='right'>" + data[1] + "</td>";
                resultForm += "<td align='right'>" + data[2] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            //MASSE DONNEE EN PLEINE PROPRIETE
            data = (object[])jsonResult1[9];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>MASSE DONNEE EN NUE PROPRIETE</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Situation active nette</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Donateur</td>";
                resultForm += "<td align='right'>Donatrice</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult1[10];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens immobiliers taxables :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[11];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens immobiliers exonérés :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[12];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens mobiliers taxables :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[13];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens mobiliers exonérés :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[14];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Somme d'argent :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[15];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Rapport dons en avance sur part :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult1[16];
                resultForm += "<tr>";
                resultForm += "<td align='right'>Total de l'actif net :</td>";
                resultForm += "<td align='right'>" + data[1] + "</td>";
                resultForm += "<td align='right'>" + data[2] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            //INCORPORATION DES DONS ANTERIEURS
            data = (object[])jsonResult1[17];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>INCORPORATION DES DONS ANTERIEURS</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                data = (object[])jsonResult1[18];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Du chef du Donateur</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens</td>";
                    resultForm += "<td align='right'>Valeur</td>";
                    resultForm += "<td align='right'>%</td>";
                    resultForm += "<td align='right'>Incorporation</td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult1[19];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens en Nue Propriété :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult1[20];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens en Pleine propriété :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult1[21];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Total :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "</tr>";
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult1[22];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Du chef de la Donatrice</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens</td>";
                    resultForm += "<td align='right'>Valeur</td>";
                    resultForm += "<td align='right'>%</td>";
                    resultForm += "<td align='right'>Incorporation</td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult1[23];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens en Nue Propriété :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult1[24];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Biens en Pleine propriété :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult1[25];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Total :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "</tr>";
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
            }
            //DROITS DANS LA MASSE GLOBALE
            resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>DROITS DANS LA MASSE GLOBALE</font></b></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            data = (object[])jsonResult1[28];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Concernant le donateur</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Droits dans la masse globale</td>";
                resultForm += "<td align='right'>Quote part</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                for (i = 1; i <= inputForm1.ddl3.TransformToInt(); i++)
                {
                    index = 28 + i;
                    data = (object[])jsonResult1[index];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Donataire " + i + " :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            data = (object[])jsonResult1[39];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Concernant le donatrice</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Droits dans la masse globale</td>";
                resultForm += "<td align='right'>Quote part</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                for (i = 1; i <= inputForm1.ddl3.TransformToInt(); i++)
                {
                    index = 39 + i;
                    data = (object[])jsonResult1[index];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Donataire " + i + " :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            data = (object[])jsonResult1[50];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Récapitulatif général</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                for (i = 1; i <= inputForm1.ddl3.TransformToInt(); i++)
                {
                    index = 50 + i;
                    data = (object[])jsonResult1[index];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>Donataire " + i + " :</td>";
                    resultForm += "<td align='right'>" + data[1] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            //Donataire - Allotissement
            for (i = 1; i <= inputForm1.ddl3.TransformToInt(); i++)
            {
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>" + (i == 1 ? "1er Donataire - Allotissement" : i + "ème Donataire - Allotissement") + "<font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                data = (object[])jsonResult1[(110 * (i - 1)) + 63];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Allotissement en provenance du Donateur</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult1[(110 * (i - 1)) + 64];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens immobiliers</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.immobilierCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 65 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 85];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens mobiliers</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.mobilierCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 86 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 106];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens immobiliers exonérés</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.immobilierexonereCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 107 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 109];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens mobiliers exonérés</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.mobilierexonereCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 110 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 112];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Sommes d'argent</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 113];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens reçus antérieurement</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 114];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Soulte à verser</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 115];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Soulte à recevoir</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 116];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Total reçu :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
                data = (object[])jsonResult1[(110 * (i - 1)) + 117];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Allotissement en provenance de la Donatrice</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult1[(110 * (i - 1)) + 118];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens immobiliers</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.immobilierCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 119 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 139];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens mobiliers</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.mobilierCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 140 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 160];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens immobiliers exonérés</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.immobilierexonereCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 161 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 163];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens mobiliers exonérés</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>Art.</td>";
                        resultForm += "<td align='right'>Quote part</td>";
                        resultForm += "<td align='right'>Valeur donnée</td>";
                        resultForm += "<td align='right'>Libellé</td>";
                        resultForm += "</tr>";
                        for (k = 0; k < inputForm2.mobilierexonereCount.TransformToInt(); k++)
                        {
                            data = (object[])jsonResult1[(110 * (i - 1)) + 164 + k];
                            resultForm += "<tr>";
                            resultForm += "<td align='right'>" + data[1] + "</td>";
                            resultForm += "<td align='right'>" + data[2] + "</td>";
                            resultForm += "<td align='right'>" + data[3] + "</td>";
                            resultForm += "<td align='right'>" + data[4] + "</td>";
                            resultForm += "</tr>";
                        }
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 166];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Sommes d'argent</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 167];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Biens reçus antérieurement</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 168];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Soulte à verser</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 169];
                    if (data[5].ToString() != "0")
                    {
                        resultForm += "<tr><td align='center' colspan='4'>Soulte à recevoir</td></tr>";
                        resultForm += "<tr>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "<td align='right'>Montant :</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult1[(110 * (i - 1)) + 170];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Total reçu :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            //CUMUL DES ATTRIBUTIONS ET DES SOULTES
            data = (object[])jsonResult2[0];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>CUMUL DES ATTRIBUTIONS ET DES SOULTES<font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                data = (object[])jsonResult2[1];
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Biens donnés par le Père :</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult2[2];
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Biens donnés par la Mère :</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult2[3];
                resultForm += "<tr>";
                resultForm += "<td align='right'></td>";
                resultForm += "<td align='right'>Total des biens donnés :</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                resultForm += "<tr><td colspan='4'></td></tr>";
                for (i = 1; i <= inputForm1.ddl3.TransformToInt(); i++)
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>" + (i + 1 == 1 ? "1er Donataire" : (i + 1) + "ème Donataire") + "</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult2[(5 * i) + 4 + i];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Reçu du Père :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult2[(5 * i) + 5 + i];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Reçu de la Mère :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult2[(5 * i) + 6 + i];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Total reçu :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult2[(5 * i) + 7 + i];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Soulte à recevoir :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    data = (object[])jsonResult2[(5 * i) + 8 + i];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "<td align='right'>Total de ses droits :</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
            }
            resultForm += "</table>";

            #endregion
            var filename = PdfHelper.GeneratePdf("DON-01", "", 1, inputForm, resultForm, out string pdf);
            return filename;
        }

        [WebMethod]
        public static string HtmlReportSyntheseTaxePere(string strInputForm1, string strInputForm2, string strInputForm3, string dossier, string date, string redacteur, string result)
        {
            var inputForm = HtmlInputForm(strInputForm1, strInputForm2, strInputForm3, dossier, date, redacteur);
            #region result
            var index = 0; var i = 0;
            var js = new JavaScriptSerializer();
            var inputForm1 = js.Deserialize<DeterminationDesDonatairesModel>(strInputForm1);
            var jsonResult = js.Deserialize<object[]>(result);
            var data = (object[])jsonResult[0];
            var resultForm = "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
            for (i = 0; i < inputForm1.ddl3.TransformToInt(); i++)
            {
                index = 65 * i;
                data = (object[])jsonResult[index];
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>" + data[0].ToString().Replace(":", "") + "</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Part du donataire dans la donation</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                data = (object[])jsonResult[index + 2];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult[index + 3];
                resultForm += "<tr>";
                resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult[index + 4];
                resultForm += "<tr>";
                resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                resultForm += "<tr><td colspan='4'></td></tr>";
                data = (object[])jsonResult[index + 5];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Détermination de l`assiette taxable - Nue propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 6];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 7];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 8];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 9];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 10];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 11];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 12];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 13];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 14];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 15];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 16];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Calcul des droits - Nue propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 17];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 18];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 19];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 20];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 21];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 22];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 23];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 24];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 25];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 26];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 27];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 28];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 29];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 30];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 31];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>" + data[0] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 32];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Détermination de l`assiette taxable - Pleine propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 33];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 34];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 35];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 36];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 37];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 38];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 39];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 40];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 41];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 42];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 43];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 44];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Calcul des droits - Pleine propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 45];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 46];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 47];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 48];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 49];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 50];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 51];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 52];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 53];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 54];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 55];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 56];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 57];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 58];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 59];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>" + data[0] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 60];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Droits à payer</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 61];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 62];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 63];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 64];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                }
            }
            resultForm += "</table>";

            #endregion
            var filename = PdfHelper.GeneratePdf("DON-01", "", 1, inputForm, resultForm, out string pdf);
            return filename;
        }

        [WebMethod]
        public static string HtmlReportSyntheseTaxeMere(string strInputForm1, string strInputForm2, string strInputForm3, string dossier, string date, string redacteur, string result)
        {
            var inputForm = HtmlInputForm(strInputForm1, strInputForm2, strInputForm3, dossier, date, redacteur);
            #region result
            var index = 0; var i = 0;
            var js = new JavaScriptSerializer();
            var inputForm1 = js.Deserialize<DeterminationDesDonatairesModel>(strInputForm1);
            var jsonResult = js.Deserialize<object[]>(result);
            var data = (object[])jsonResult[0];
            var resultForm = "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
            for (i = 0; i < inputForm1.ddl3.TransformToInt(); i++)
            {
                index = 65 * i;
                data = (object[])jsonResult[index];
                resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>" + data[0].ToString().Replace(":", "") + "</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Part du donataire dans la donation</font></b></td></tr>";
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                data = (object[])jsonResult[index + 2];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr>";
                    resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                }
                data = (object[])jsonResult[index + 3];
                resultForm += "<tr>";
                resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                data = (object[])jsonResult[index + 4];
                resultForm += "<tr>";
                resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                resultForm += "<td align='right'>" + data[3] + "</td>";
                resultForm += "<td align='right'></td>";
                resultForm += "</tr>";
                resultForm += "<tr><td colspan='4'></td></tr>";
                data = (object[])jsonResult[index + 5];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Détermination de l`assiette taxable - Nue propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 6];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 7];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 8];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 9];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 10];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 11];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 12];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 13];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 14];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 15];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 16];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Calcul des droits - Nue propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 17];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 18];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 19];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 20];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 21];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 22];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 23];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 24];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 25];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 26];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 27];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 28];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 29];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 30];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 31];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>" + data[0] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 32];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Détermination de l`assiette taxable - Pleine propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 33];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 34];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 35];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 36];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 37];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 38];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 39];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 40];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 41];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 42];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 43];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 44];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Calcul des droits - Pleine propriété</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 45];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 46];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 47];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 48];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 49];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 50];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 51];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 52];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 53];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 54];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 55];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 56];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 57];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='right'>sur     " + data[2] + "</td>";
                        resultForm += "<td align='right'>=     " + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 58];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right'>" + data[1] + "</td>";
                        resultForm += "<td align='center' colspan='3'>" + data[2] + "</td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 59];
                    resultForm += "<tr>";
                    resultForm += "<td align='right'>" + data[0] + "</td>";
                    resultForm += "<td align='right'>" + data[2] + "</td>";
                    resultForm += "<td align='right'>" + data[3] + "</td>";
                    resultForm += "<td align='right'></td>";
                    resultForm += "</tr>";
                    resultForm += "<tr><td colspan='4'></td></tr>";
                }
                data = (object[])jsonResult[index + 60];
                if (data[4].ToString() != "0")
                {
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Droits à payer</font></b></td></tr>";
                    resultForm += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                    data = (object[])jsonResult[index + 61];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 62];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 63];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                    data = (object[])jsonResult[index + 64];
                    if (data[4].ToString() != "0")
                    {
                        resultForm += "<tr>";
                        resultForm += "<td align='right' colspan='2'>" + data[0] + "</td>";
                        resultForm += "<td align='right'>" + data[3] + "</td>";
                        resultForm += "<td align='right'></td>";
                        resultForm += "</tr>";
                    }
                }
            }
            resultForm += "</table>";

            #endregion
            var filename = PdfHelper.GeneratePdf("DON-01", "", 1, inputForm, resultForm, out string pdf);
            return filename;
        }

        [WebMethod]
        public static string HtmlReportSyntheseTaxeDonation(string strInputForm1, string strInputForm2, string strInputForm3, string dossier, string date, string redacteur, string result)
        {
            var inputForm = HtmlInputForm(strInputForm1, strInputForm2, strInputForm3, dossier, date, redacteur);
            #region result
            var js = new JavaScriptSerializer();
            var jsonResult = js.Deserialize<object[]>(result);
            var resultForm = "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
            resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>TOTAL DES DROITS ET FRAIS</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            var data = (object[])jsonResult[1];
            resultForm += "<tr>"+
                "<td align='right' colspan='2'>Trésor :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[2];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Débours :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[3];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Emoluments du notaire HT :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[4];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Convention d'honoraires :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[5];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Total des frais :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[6];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Montant des droits :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[7];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Montant des droits et frais :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            resultForm += "<tr><td colspan='4'></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>DETAILS DES FRAIS</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            data = (object[])jsonResult[9];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments du Notaire (Donateur) - C.com. Article A 444-67</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                data = (object[])jsonResult[10];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[11];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[12];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[13];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[14];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>Total :</td>" +
                        "<td align='right'>" + data[3] + "</td><td colspan='2'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[15];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>TOTAL Hors T.V.A :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[16];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Emolument minimum :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[17];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Remise sur prestations non visées à l'article 444-174 du Code de Commerce :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[18];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Remise sur prestations visées à l'article 444-174 du Code de Commerce (Entreprise) :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[19];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Total après remise HT :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            data = (object[])jsonResult[20];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments du Notaire (Donatrice) - C.com. Article A 444-67</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                data = (object[])jsonResult[21];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[22];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[23];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[24];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>sur     " + data[3] + "</td>" +
                        "<td align='right'>=     " + data[4] + "</td>" +
                        "<td align='right'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[25];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>Total :</td>" +
                        "<td align='right'>" + data[3] + "</td><td colspan='2'></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[26];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>TOTAL Hors T.V.A :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[27];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Emolument minimum :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[28];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Remise sur prestations non visées à l'article 444-174 du Code de Commerce :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[29];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Remise sur prestations visées à l'article 444-174 du Code de Commerce (Entreprise) :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[30];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='2'>Total après remise HT :</td>" +
                        "<td align='right'>" + data[4] + "</td><td></td>" +
                        "</tr>";
                }
                resultForm += "<tr><td colspan='4'></td></tr>";
            }
            resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments du Notaire (Donatrice) - C.com. Article A 444-67</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            data = (object[])jsonResult[32];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Emoluments du Notaire - Donateur :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[33];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Emoluments du Notaire - Donatrice :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[34];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Emoluments de formalités :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[35];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Total Emoluments du notaire HT :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[36];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>TVA :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[37];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Total TTC :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            resultForm += "<tr><td colspan='4'></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Convention d`honoraires</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            data = (object[])jsonResult[39];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Convention d`honoraires HT :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[40];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>TVA :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            resultForm += "<tr><td colspan='4'></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Débours</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            data = (object[])jsonResult[42];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Débours :</td>" +
                "<td align='right'>" + data[4] + "</td><td></td>" +
                "</tr>";
            resultForm += "<tr><td colspan='4'></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>TRESOR PUBLIC</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Publicité aux Hypothéques</font></b></td></tr>" +
                "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            resultForm += "<tr><td colspan='4'>Taxe Publicité Foncière :</td></tr>";
            data = (object[])jsonResult[46];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Total des biens immobiliers non exonérés (tenant compte d'un usufruit éventuel) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[47];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Total des biens immobiliers exonérés (tenant compte d'un usufruit éventuel) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[48];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 1) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[49];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 2) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[50];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Base fiscale :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[51];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right'>Taxe Publicité Foncière :</td>" +
                    "<td align='right'>" + data[1] + "</td>" +
                    "<td align='right'>" + data[3] + "</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            resultForm += "<tr><td colspan='4'>CSI (art. 879 du CGI) :</td></tr>";
            data = (object[])jsonResult[54];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Total des biens immobiliers non exonérés (tenant compte d'un usufruit éventuel) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[55];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Total des biens immobiliers exonérés (tenant compte d'un usufruit éventuel) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[56];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Total des biens immobiliers incorporés (donations antérieures) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[57];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 1) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[58];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 2) :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[59];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Réversibilité de l'usufruit :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[60];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right' colspan='3'>Base fiscale :</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[61];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr>" +
                    "<td align='right'>CSI (art. 879 du CGI) :</td>" +
                    "<td align='right'>" + data[1] + "</td>" +
                    "<td align='right'>" + data[3] + "</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            data = (object[])jsonResult[63];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4'>Droit de partage (Art.746 du CGI) :</td></tr>";
                resultForm += "<tr>" +
                    "<td align='right'>CSI (art. 879 du CGI) :</td>" +
                    "<td align='right'>" + data[1] + "</td>" +
                    "<td align='right'>" + data[3] + "</td>" +
                    "<td align='right'>" + data[4] + "</td>" +
                    "</tr>";
            }
            resultForm += "<tr><td colspan='4'></td></tr>";
            data = (object[])jsonResult[65];
            if (data[5].ToString() != "0")
            {
                resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Enregistrement</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
                resultForm += "<tr><td colspan='4'>Droit de partage (Art.746 du CGI) :</td></tr>";
                data = (object[])jsonResult[66];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right'>CSI (art. 879 du CGI) :</td>" +
                        "<td align='right'>" + data[1] + "</td>" +
                        "<td align='right'>" + data[3] + "</td>" +
                        "<td align='right'>" + data[4] + "</td>" +
                        "</tr>";
                }
                data = (object[])jsonResult[68];
                if (data[5].ToString() != "0")
                {
                    resultForm += "<tr>" +
                        "<td align='right' colspan='3'>Droit fixe (clause de réversion) :</td>" +
                        "<td align='right'>" + data[4] + "</td>" +
                        "</tr>";
                }
            }
            resultForm += "</table>";
            #endregion
            var filename = PdfHelper.GeneratePdf("DON-01", "", 1, inputForm, resultForm, out string pdf);
            return filename;
        }

        [WebMethod]
        public static string HtmlReportSyntheseDonation(string strInputForm1, string strInputForm2, string strInputForm3, string dossier, string date, string redacteur, string result, string headerPereMere, string subHeaderPereMere)
        {
            var inputForm = HtmlInputForm(strInputForm1, strInputForm2, strInputForm3, dossier, date, redacteur);
            #region result
            var js = new JavaScriptSerializer();
            var jsonResult = js.Deserialize<object[]>(result);
            var resultForm = "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>"+
                "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>" + headerPereMere + "</font></b></td></tr>"+
                "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
            resultForm += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>" + subHeaderPereMere + "</font></b></td></tr>" +
                    "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            var data = (object[])jsonResult[0];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Rappel de la valeur des biens donnés :</td>" +
                "<td align='right'>" + data[0] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[1];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Montant de la base fiscale :</td>" +
                "<td align='right'>" + data[0] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[2];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Abattement personnel en vigueur au jour de cette donation :</td>" +
                "<td align='right'>" + data[0] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[3];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Abattement utilisé lors de cette donation :</td>" +
                "<td align='right'>" + data[0] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[4];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Sommes taxées :</td>" +
                "<td align='right'>" + data[0] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[5];
            resultForm += "<tr>" +
                "<td align='right' colspan='2'>Droits taxés avant réduction :</td>" +
                "<td align='right'>" + data[0] + "</td><td></td>" +
                "</tr>";
            data = (object[])jsonResult[6];
            if(data[0].ToString() == "0")
            {
                resultForm += "<tr><td align='center' colspan='4'>Attention :</td></tr>";
                resultForm += "<tr><td align='center' colspan='4'>Les calculs des sommes taxées et des droits ont fait l`objet du lissage prévu à l`Art. 784 du CGI.</td></tr>";
            }

            resultForm += "</table>";
            #endregion result
            var filename = PdfHelper.GeneratePdf("DON-01", "", 1, inputForm, resultForm, out string pdf);
            return filename;
        }

        #endregion WebMethod

        protected void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                var simulationId = Request.QueryString["Voir"].TransformToInt();
                var obj = SimulationActeDataAccess.GetSimulationActe(simulationId, txtLibelle.Text.Trim());
                if (obj != null)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Une simulation portant le même nom est déjà enregistrée. Veuillez modifier le nom de la simulation.', 'Échec de enregistrement', {timeOut: 5000});", true);
                    Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
                    return;
                }
                var data = @"{" +
                           "'txtDossier':'" + txtDossier.Text +
                           "','txtDateDeSignature':'" + txtDateSignature.Text +
                           "','txtRedacteur':'" + txtRedacteur.Text +
                           "','inputForm1':'" + hdJsonDeterminationDesDonataires.Value + 
                           "','inputForm2':'" + hdJsonDeterminationDesBiens.Value + 
                           "','inputForm3':'" + hdJsonRappelDeDonationsAnterieures.Value + 
                           "'}";
                obj = SimulationActeDataAccess.GetSimulationActe(simulationId);
                if (obj == null)
                {
                    SimulationActeDataAccess.Save(data, txtLibelle.Text, "DON-01", false, Session["CLIENT_ID"].TransformToInt());
                }
                else
                {
                    SimulationActeDataAccess.Update(simulationId, data, Session["CLIENT_ID"].TransformToInt());
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Cette simulation a été enregistrée avec succès.', 'Notification', {timeOut: 5000});", true);
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
                    Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
                    return;
                }
                var data = @"{" +
                           "'txtDossier':'" + txtDossier.Text +
                           "','txtDateDeSignature':'" + txtDateSignature.Text +
                           "','txtRedacteur':'" + txtRedacteur.Text +
                           "','inputForm1':'" + hdJsonDeterminationDesDonataires.Value +
                           "','inputForm2':'" + hdJsonDeterminationDesBiens.Value +
                           "','inputForm3':'" + hdJsonRappelDeDonationsAnterieures.Value +
                           "'}";
                SimulationActeDataAccess.Save(data, txtLibelleSous.Text, "DON-01", false, Session["CLIENT_ID"].TransformToInt());
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.success('Cette simulation a été enregistrée avec succès.', 'Notification', {timeOut: 5000});", true);
                Page.ClientScript.RegisterStartupScript(GetType(), "input_form_click", "ShowHide_input_form($('#btnSynthese'));", true);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "toastr_message", @"toastr.error('Le serveur de calculs est temporairement indisponible. Veuillez réessayer plus tard.', 'Échec de enregistrement', {timeOut: 5000});", true);
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {

        }
    }

    class DonatairesDeDegreModel
    {
        public int index { get; set; }
        public string txtZone01 { get; set; }
        public bool cb1 { get; set; }
        public bool cb2 { get; set; }
        public string ddl4 { get; set; }
        public string ddl5 { get; set; }
    }

    class NatureDeLaDonationModel
    {
        public int index { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
    }

    class DeterminationDesDonatairesModel
    {
        public string txtZone00 { get; set; }
        public string ddl1 { get; set; }
        public string ddl1Text { get; set; }
        public string txtZone01a { get; set; }
        public string txtZone01b { get; set; }
        public string ddl3 { get; set; }
        public DonatairesDeDegreModel[] arrDonatairesDeDegre { get; set; }
        public string ddl6 { get; set; }
        public string ddl7 { get; set; }
        public NatureDeLaDonationModel[] arrNatureDeLaDonation { get; set; }
        public string txtZone02Total { get; set; }
        public string txtZone03Total { get; set; }
        public string msgTxtZone02AndTxtZone03 { get; set; }
    }

    class DeterminationDesBiensModel
    {
        public string immobilierCount { get; set; }
        public ImmobilierModel[] arrImmobilier { get; set; }
        public string immobilierexonereCount { get; set; }
        public ImmobilierExonereModel[] arrImmobilierexonere { get; set; }
        public string mobilierCount { get; set; }
        public MobilierModel[] arrMobilier { get; set; }
        public string mobilierexonereCount { get; set; }
        public MobilierExonereModel[] arrMobilierexonere { get; set; }
        public string sommeargentCount { get; set; }
        public SommeargentModel[] arrSommeargent { get; set; }
    }

    class RappelDeDonationsAnterieuresModel
    {
        public int Id { get; set; }
        public PereModel[] Pere { get; set; }
        public MereModel[] Mere { get; set; }
    }

    class PereModel
    {
        public int Id { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtZone05 { get; set; }
        public string txtZone06 { get; set; }
        public string txtZone07 { get; set; }
        public string txtZone00_ { get; set; }
        public bool chk01 { get; set; }
    }

    class MereModel
    {
        public int Id { get; set; }
        public string txtZone01 { get; set; }
        public string txtZone02 { get; set; }
        public string txtZone03 { get; set; }
        public string txtZone04 { get; set; }
        public string txtZone05 { get; set; }
        public string txtZone06 { get; set; }
        public string txtZone07 { get; set; }
        public string txtZone00_ { get; set; }
        public bool chk01 { get; set; }
    }

    class ImmobilierModel
    {
        public string origine { get; set; }
        public string attribution { get; set; }
        public bool reserve { get; set; }
        public string valeur { get; set; }
        public string passif { get; set; }
    }

    class ImmobilierExonereModel
    {
        public string typedebien { get; set; }
        public string typedebienText { get; set; }
        public string origine { get; set; }
        public string attribution { get; set; }
        public bool reserve { get; set; }
        public string valeur { get; set; }
        public string passif { get; set; }
    }

    class MobilierModel
    {
        public string origine { get; set; }
        public string attribution { get; set; }
        public bool reserve { get; set; }
        public string valeur { get; set; }
        public string passif { get; set; }
    }

    class MobilierExonereModel
    {
        public string typedebien { get; set; }
        public string typedebienText { get; set; }
        public string origine { get; set; }
        public string attribution { get; set; }
        public bool reserve { get; set; }
        public string valeur { get; set; }
        public string passif { get; set; }
    }

    class SommeargentModel
    {
        public string origine { get; set; }
        public string attribution { get; set; }
        public string valeur { get; set; }
    }

    class SetRangeModel
    {
        public string col1 { get; set;}
        public string col2 { get; set; }
        public string col3 { get; set; }
        public string col4 { get; set; }
        public string col5 { get; set; }
        public string col6 { get; set; }
        public string col7 { get; set; }
        public string col8 { get; set; }
        public string col9 { get; set; }
    }
}