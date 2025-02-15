using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class OnlineClient
    {
        public int CLIENTID { get; set; }
        public string LOGIN { get; set; }
        public string MOT_DE_PASSE { get; set; }
        public string CONFIRMATION_MOT_DE_PASSE { get; set; }
        public string TÉLÉPHONE { get; set; }
        public string ADRESSE_EMAIL { get; set; }
    }
}