using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace webapi.Models {
    public class Reviewer {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage="FirstName cannot be more than 100 chars")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage="LastName cannot be more than 200 chars")]
        public string LastName { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}