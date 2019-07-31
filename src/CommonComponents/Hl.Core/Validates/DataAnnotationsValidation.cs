using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl.Core.Validates
{
    public class DataAnnotationsValidation : BaseValidation
    {
        public DataAnnotationsValidation(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var validationContext = new ValidationContext(instance);
            var validationResults = new List<ValidationResult>();
            IsValid = Validator.TryValidateObject(instance, validationContext, validationResults, true);
            ErrorMessages = validationResults.Select(p => p.ErrorMessage).ToList();
            PrimaryErrorMessage = ErrorMessages.FirstOrDefault();
        }
    }
}
