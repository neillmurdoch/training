using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
    public class Restaurant // Could you IValidatableObject which will allow custom code to validate your object.
    {
        public int Id { get; set; }
        
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; }

        // Custom code if IValidatable was implemented.
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}

