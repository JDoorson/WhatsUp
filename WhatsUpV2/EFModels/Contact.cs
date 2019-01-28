using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUpV2.EFModels
{
    public class Contact
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public virtual Account Owner { get; set; }
    }
}