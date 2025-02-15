using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int IdSegment { get; set; }
        public string ReferenceCustomer { get; set; }
        public string ReferenceSegment { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string ReferenceAffiliation { get; set; }
        public List<Link> Links { get; set; }
    }
}