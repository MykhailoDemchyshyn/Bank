using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class IbanAttribute: ValidationAttribute
    {
        string str = "bad IBAN";
        public string GetErrorMessage() => str;

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
         
            
            if (value.ToString().Length == 29)
            {
                if (value.ToString()[0] == 'U' && value.ToString()[1] == 'A')
                {
                    var cv = value.ToString().ToCharArray();
                    for(int i=2;i<29;i++)
                    {
                        if(!Char.IsDigit(cv[i]))
                        {
                            str = "from 3 to 29 symbol is letter";
                            return new ValidationResult(GetErrorMessage());
                        }
                    }
                }
                else
                {
                    str = "IBAN must start from UA";
                    return new ValidationResult(GetErrorMessage());
                }
            }
            else
            {
                str = "IBAN must has 29 symbols";
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
        
        
    }
}