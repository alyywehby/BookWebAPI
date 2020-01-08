using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace webapi.Models {
    public class Review {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength=10, ErrorMessage="Headline must be between 10 and 200 chars")]
        public string Headline { get; set; }
        [Required]
        [StringLength(2000, MinimumLength=10, ErrorMessage="Text must be between 10 and 2000 chars")]
        public string Text { get; set; }
        [Required]
        [Range(1,5,ErrorMessage="Rating must between 1 and 5 stars")]
        public int Rating { get; set; }
        public virtual Book Book { get; set; }
        public virtual Reviewer Reviewer { get; set; }
    }
}