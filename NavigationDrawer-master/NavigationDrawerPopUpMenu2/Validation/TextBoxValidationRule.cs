using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TestingProgram
{
    public class TextBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regex = new Regex(@"^[a-zA-Z0-9;:_\-!#\$%&'\*\+/=\?\^`\{\}\|\(\).,\[\]]+$");

            return !(regex.IsMatch(value.ToString())) ? new ValidationResult(false, "俄罗斯人物")
                : ValidationResult.ValidResult;

            //return string.IsNullOrWhiteSpace((value ?? "").ToString())
            //    ? new ValidationResult(false, "Field is required.")
            //    : ValidationResult.ValidResult;
        }
    }
}