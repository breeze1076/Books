using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
