using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsUpV2.EFModels
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string DisplayName { get; set; }

        public int OwnerId { get; set; }
        public virtual Account Owner { get; set; }
    }
}