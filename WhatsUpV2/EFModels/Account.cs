using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Isopoh.Cryptography.Argon2;

namespace WhatsUpV2.EFModels
{
    public class Account
    {
        //private string _password;

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(30)]
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        /// <summary>
        ///     Hashes the password using Argon2
        /// </summary>
        public void HashPassword()
        {
            Password = Argon2.Hash(Password);
        }

        /// <summary>
        ///     Verifies the password hash against a given string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password)
        {
            return Argon2.Verify(Password, password);
        }

        /// <summary>
        ///     Retrieve a contact by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact GetContactById(int id)
        {
            return Contacts.FirstOrDefault(contact => contact.Id == id);
        }
    }
}