﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUpV2.EFModels
{
    public class Chat
    {
        public int Id { get; set; }
        public string UserA { get; set; }
        public string UserB { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        ///     Retrieve the most recent message
        /// </summary>
        /// <returns></returns>
        public string GetMostRecentMessage()
        {
            var message = Messages.OrderByDescending(m => m.SentAt).FirstOrDefault();
            return message?.Text ?? "";
        }
    }
}