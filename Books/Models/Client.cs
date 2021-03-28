using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Books.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Client : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var client = (Client)validationContext.ObjectInstance;
            var repository = validationContext.GetService<IClientRepository>();
            if (repository != null && repository.ClientNameExists(client))
            {
                yield return new ValidationResult("A Client with this name already exists");
            }
        }
    }
}