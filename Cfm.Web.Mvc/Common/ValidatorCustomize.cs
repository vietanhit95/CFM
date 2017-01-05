using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Cfm.Web.Mvc.Common
{
    public class CustomMoneyValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string sMoney = value.ToString().Replace(",","").Replace(".","");
                long lMoney = 0;
                try
                {
                    lMoney = long.Parse(sMoney);
                    if(lMoney >= 0)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Số tiền không hợp lệ!");
                    }
                }
                catch
                {
                    return new ValidationResult("Số tiền không hợp lệ!");
                }
            }
            else
            {
                return new ValidationResult("Số tiền không được để trống!");
            }
        } 
    }
}