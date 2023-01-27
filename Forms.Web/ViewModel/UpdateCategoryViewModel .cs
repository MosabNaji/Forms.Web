using System.ComponentModel.DataAnnotations;

namespace Forms.Web.ViewModel
{
    public class CreateCategoryViewModel
    {
        [Required] // important for validation
        public string Name { get; set; }
    }
}
