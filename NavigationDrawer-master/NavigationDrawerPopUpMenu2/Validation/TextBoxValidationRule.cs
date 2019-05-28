using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TestingProgram
{
    public class TextBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]+$");
            
            return !(regex.IsMatch(value.ToString())) ? new ValidationResult(false, "Поле должно содержать латинские буквы или цифры")
                : ValidationResult.ValidResult;

            //return string.IsNullOrWhiteSpace((value ?? "").ToString())
            //    ? new ValidationResult(false, "Field is required.")
            //    : ValidationResult.ValidResult;
            //;:_\-!#\$%&'\*\+/=\?\^`\{\}\|\(\).,\[\]
        }
    }
}