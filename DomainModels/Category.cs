using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Category
    {
        public Category()
        {
            Blogs = new HashSet<Blog>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Category_Name { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}