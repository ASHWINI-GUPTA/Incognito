using System.ComponentModel.DataAnnotations;

namespace Incognito.Models.AdminViewModel
{
    public class AddRoleVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
