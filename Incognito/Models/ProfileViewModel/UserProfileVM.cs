using System.ComponentModel.DataAnnotations;

namespace Incognito.Models.ProfileViewModel
{
    public class UserProfileVM
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(250)]
        [Display(Name = "Twitter Handle")]
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
