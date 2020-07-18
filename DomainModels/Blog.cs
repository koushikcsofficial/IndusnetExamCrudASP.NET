using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Blog_Headline { get; set; }

        [Column(TypeName = "text")]
        public string Blog_Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Blog_CreatedFrom { get; set; }

        public DateTime Blog_CreatedAt { get; set; }

        public virtual Category Category { get; set; }
    }
}
