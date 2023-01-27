using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Forms.Web.ViewModel
{
    public class CreateProductViewModel
    {
        [Required]
        [Display(Name="Product Name")]
        //[DataType(DataType.)]
        public string Name { get; set; }
        [Display(Name = "Product Price")]
        public float Price { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile ImageURL { get; set; }
    }
}
