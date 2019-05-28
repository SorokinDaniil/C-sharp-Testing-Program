using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TestingProgram
{
    public class SizeFieldValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            return ((value.ToString().Length) < 10 || (value.ToString().Length) > 32) ? new ValidationResult(false, "Длинна должна быть не менее 10 символов")
                : ValidationResult.ValidResult;

            //return string.IsNullOrWhiteSpace((value ?? "").ToString())
            //    ? new ValidationResult(false, "Field is required.")
            //    : ValidationResult.ValidResult;
        }
    }
}