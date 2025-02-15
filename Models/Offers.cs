using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class Offers
    {
        public int Page { get; set; }
        public int SizePage { get; set; }
        public int Count { get; set; }
        public int TotalItems { get; set; }
        public DateTime DateGenerated { get; set; }
        public List<Offer> Items { get; set; }
    }
}