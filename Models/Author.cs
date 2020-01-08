using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webapi.Models {
    public class Author {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength (100, ErrorMessage = "First name cannot be more than 100 chars")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength (200, ErrorMessage = "Last name cannot be more than 200 chars")]
        public string LastName { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}