using System;
using System.ComponentModel.DataAnnotations;

namespace DriverTracker.Models
{
    public class LessThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;

        public LessThanOrEqualToAttribute(string dependentProperty)
        {
            _dependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object model = validationContext.ObjectInstance;

            return Convert.ToDecimal(value) > Convert.ToDecimal(model.GetType().GetProperty(_dependentProperty).GetValue(model))
                ? new ValidationResult(this.ErrorMessage)
                : ValidationResult.Success;
        }
    }
}
