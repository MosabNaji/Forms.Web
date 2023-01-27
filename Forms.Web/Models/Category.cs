using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forms.Web.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
