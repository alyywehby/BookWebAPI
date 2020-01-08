using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace webapi.Models {
    public class Country {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage="Name cannot be more than 50 chars")]
        public string Name { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}