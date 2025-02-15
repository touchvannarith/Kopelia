using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotaliaOnline.Models
{
    public class Device
    {
        public int No { get; set; }
        public int Id { get; set; }
        public string DeviceNumber { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public int ClientId { get; set; }
    }
}