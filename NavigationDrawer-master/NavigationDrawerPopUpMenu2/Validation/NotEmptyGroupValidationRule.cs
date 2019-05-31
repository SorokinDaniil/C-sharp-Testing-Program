using System.Globalization;
using System.Windows.Controls;

namespace TestingProgram
{
    public class NotEmptyGroupValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Выберите группу")
                : ValidationResult.ValidResult;
        }
    }
}