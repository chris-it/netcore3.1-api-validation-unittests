using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bunnings.Domain.Entities
{
    /// <summary>
    /// This implements a couple of validation methods
    /// 1. Property validation using data annotations. This is for indivisual properties
    /// 2. Class level validations usiing validatable object. This can validate the class as a whole or any other complex validations.
    /// </summary>
    public class Roster : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate >= EndDate)
            {
                yield return new ValidationResult("End date should be greater than start date", new string[] { "endDate" });
            }
        }
    }
}
