using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Books.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Book : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Client> Clients { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var book = (Book)validationContext.ObjectInstance;
            var repository = validationContext.GetService<IBookRepository>();
            if (repository != null && repository.BookNameExists(book))
            {
                yield return new ValidationResult("A book with this name already exists");
            }
        }
    }
}
