using System.ComponentModel.DataAnnotations;

namespace Incognito.Models
{
    public class MessageCategory
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}