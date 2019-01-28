using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUpV2.EFModels
{
    public class Message
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public DateTime SentAt { get; set; }

        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }
}