using System.Globalization;
using System.Windows.Controls;

namespace TestingProgram
{
    public class LoginExistsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Обязательное поле для заполнение")
                : ValidationResult.ValidResult;
        }
    }
}