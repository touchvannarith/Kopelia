using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public int IdSegment { get; set; }
        public string ReferenceSegment { get; set; }
        public bool IsVisible { get; set; }
        public string Name { get; set; }
        public string ReferenceOffer { get; set; }
        public string TitleLocalized { get; set; }
        public string PricingLocalized { get; set; }
        public string Currency { get; set; }
        public int Pricing { get; set; }
        public int PricingSubtotal { get; set; }
        public int PricingTotal { get; set; }
        public int AmountRecurrence { get; set; }
        public int DurationRecurrence { get; set; }
        public string UnitRecurrence { get; set; }
        public List<Feature> Features { get; set; }
        public List<Link> Links { get; set; }

        public string TextButton { get; set; }
    }
}