using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class ModelValidation
    {
        public void Validate()
        {
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (isValid == false)
            {
                StringBuilder sbrErrors = new StringBuilder();
                foreach (var validationResult in results)
                {
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }

                throw new ValidationException(sbrErrors.ToString());
            }
        }
    }
}
