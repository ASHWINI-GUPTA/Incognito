using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incognito.Models.AdminViewModel
{
    public class EditRoleVM
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        [MaxLength(100, ErrorMessage = "Name should be max 100 char long")]
        public string Name { get; set; }

        public List<string> Users { get; set; }
    }
}
