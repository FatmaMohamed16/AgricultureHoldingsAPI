using System.ComponentModel.DataAnnotations;

namespace AmlakState.DTO
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
           
            else if (value is string fileName)
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
                    if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"امتداد الملف غير مسموح به. الامتدادات المسموح بها هي: {string.Join(", ", _extensions)}";
        }
    }
}
