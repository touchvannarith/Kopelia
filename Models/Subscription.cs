using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int IdSegment { get; set; }
        public int IdOffer { get; set; }
        public int IdCustomer { get; set; }
        public int IdCustomerBuyer { get; set; }
        public int PricingSubtotal { get; set; }
        public int PricingTotal { get; set; }
        public string ReferenceSegment { get; set; }
        public string ReferenceOffer { get; set; }
        public string ReferenceCustomer { get; set; }
        public string ReferenceCustomerBuyer { get; set; }
        public string StateSubscription { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DatePeriodStart { get; set; }
        public DateTime DatePeriodEnd { get; set; }
        public DateTime DateTerm { get; set; }
        public DateTime DateResetConsumption { get; set; }
        public string StateSubscriptionAfterTerm { get; set; }
        public bool IsTrial { get; set; }
        public int CountDaysTrial { get; set; }
        public bool IsEngaged { get; set; }
        public bool IsCustomerBillable { get; set; }
        public bool IsPaymentCappingReached { get; set; }
        public DateTime DateNextBilling { get; set; }
        public string TitleLocalized { get; set; }
        public int AmountUpFront { get; set; }
        public int AmountTrial { get; set; }
        public int DurationTrial { get; set; }
        public string UnitTrial { get; set; }
        public int AmountRecurrence { get; set; }
        public int DurationRecurrence { get; set; }
        public string UnitRecurrence { get; set; }
        public int CountRecurrences { get; set; }
        public int CountMinRecurrences { get; set; }
        public int AmountTermination { get; set; }
        public List<Feature> Features { get; set; }
        public DateTime DateUpdate { get; set; }
        public List<Link> Links { get; set; }
    }
}