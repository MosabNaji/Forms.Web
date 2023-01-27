using System.ComponentModel.DataAnnotations;

namespace Forms.Web.ViewModel
{
    public class UpdateCategoryViewModel
    {

        public int Id { get; set; }
        [Required] // important for validation
        public string Name { get; set; }
    }
}
