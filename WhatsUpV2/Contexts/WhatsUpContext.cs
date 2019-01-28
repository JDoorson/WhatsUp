using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhatsUpV2.EFModels;

namespace WhatsUpV2.Contexts
{
    public class WhatsUpContext : DbContext
    {
        public WhatsUpContext() : base("name=SQLServer")
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}