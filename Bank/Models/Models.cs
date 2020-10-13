using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "other password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateBorn { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

    }

    public class EditPinModel
    {
        [Required]
        [Code(16)]
        public string CardNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Code(4)]
        public string OldPIN { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Code(4)]
        public string NewPIN { get; set; }

    }

    public class ForwardMoneyWithIbanModel
    {
        [Required]
        [Iban]
        public string IBAN { get; set; }
        [Required]
        public string Money { get; set; }
    }
    public class EditRoleModel
    {
        public int PersonId { get; set; }
        public string NewRole { get; set; }

    }

}