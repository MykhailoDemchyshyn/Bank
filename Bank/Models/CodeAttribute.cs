using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class CodeAttribute: ValidationAttribute
    {
        string str = "bad code";
        int number;
        public string GetErrorMessage() => str;
        
        public CodeAttribute(int numb)
        {
            number = numb;
        }

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            if (value.ToString().Length == number)
            {
                var cv = value.ToString().ToCharArray();
                for (int i = 0; i < number; i++)
                {
                    if (!Char.IsDigit(cv[i]))
                    {
                        str = "one or more symbols is letter";
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
            else
            {
                str = $"letngth nust has {number} sumbols";
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}