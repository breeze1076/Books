using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Books.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Author : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var author = (Author)validationContext.ObjectInstance;
            var repository = validationContext.GetService<IAuthorRepository>();
            if (repository != null && repository.AuthorNameExists(author))
            {
                yield return new ValidationResult("An author with this name already exists");
            }
        }
    }
}
