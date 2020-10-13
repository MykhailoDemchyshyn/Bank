using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class User
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime DateBorn { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IBAN { get; set; }
        public string CardNumber { get; set; }
        public int Money  { get; set; }
        public string PIN { get; set; }


        public int? BankId { get; set; }
        public Bank Bank { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}