//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotaliaOnline
{
    using System;
    using System.Collections.Generic;
    
    public partial class online_Client
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActivated { get; set; }
        public string AllCookie { get; set; }
        public string CurrentCookie { get; set; }
        public string ImageLogo { get; set; }
        public Nullable<int> LimitDevice { get; set; }
        public string ReferenceCustomer { get; set; }
        public Nullable<int> SubscriptionId { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Package { get; set; }
        public string DeviceIdentity { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> LoggedInCount { get; set; }
    }
}
