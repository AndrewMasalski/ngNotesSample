
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NotesServiceApp.Models
{
    public class Note : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }

        public bool HasValidationErrors()
        {
            return !Validate(new ValidationContext(this)).Any();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Id == 0)
                yield return new ValidationResult("Id cannot be 0");
            if (string.IsNullOrEmpty(Title)) 
                yield return new ValidationResult("Title cannot be null or empty string");
        }
    }
}