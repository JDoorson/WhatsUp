using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Isopoh.Cryptography.Argon2;

namespace WhatsUpV2.EFModels
{
    public class Account
    {
        private string _password;

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(30)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password
        {
            get => _password;
            set => _password = Argon2.Hash(value);
        }

        public virtual ICollection<Contact> Contacts { get; set; }

        public bool VerifyPassword(string password)
        {
            return Argon2.Verify(_password, password);
        }

        public Contact GetContactById(int id)
        {
            return Contacts.FirstOrDefault(contact => contact.Id == id);
        }
    }
}