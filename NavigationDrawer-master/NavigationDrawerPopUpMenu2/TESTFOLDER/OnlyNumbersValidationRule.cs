using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TestingProgram.TESTFOLDER
{
    #region ValidationRule

    public class OnlyNumbersValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = new ValidationResult(true, null);

            string NumberPattern = @"^[0-9-]+$";
            Regex rgx = new Regex(NumberPattern);

            if (rgx.IsMatch(value.ToString()) == false)
            {
                result = new ValidationResult(false, "Must be only numbers");
            }

            return result;
        }
    }

    #endregion
}
