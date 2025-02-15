using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string ReferenceFeature { get; set; }
        public bool IsVisible { get; set; }
        public string Properties { get; set; }
        public string TitleLocalized { get; set; }
        public string PricingLocalized { get; set; }
        public string TypeFeature { get; set; }
        public int QuantityIncluded { get; set; }
        public int QuantityCurrent { get; set; }
        public bool IsIncluded { get; set; }
        public bool IsEnabled { get; set; }
    }
}