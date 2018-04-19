using System.ComponentModel.DataAnnotations;

namespace Incognito.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [MaxLength(250)]
        public string Twitter { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [MaxLength(250)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [MaxLength(500)]
        [Display(Name = "After Words")]
        public string AfterWords { get; set; }
    }
}
