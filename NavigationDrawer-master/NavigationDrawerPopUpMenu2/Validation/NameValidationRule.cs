using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TestingProgram
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regex = new Regex(@"^[а-яА-Я ]+$");

            return !(regex.IsMatch(value.ToString())) ? new ValidationResult(false, "Name may only contain russian alphabet characters")
                : ValidationResult.ValidResult;

            //return string.IsNullOrWhiteSpace((value ?? "").ToString())
            //    ? new ValidationResult(false, "Field is required.")
            //    : ValidationResult.ValidResult;
        }
    }
}