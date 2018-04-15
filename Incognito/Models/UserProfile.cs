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

        [MaxLength(250)]
        public string CompanyName { get; set; }

        [MaxLength(250)]
        public string JobTitle { get; set; }

        [MaxLength(500)]
        public string AfterWords { get; set; }

        public string GetFullName()
        {
            return $"{User.FirstName} {User.LastName}"; 
        }

        public string GetProfilePic()
        {
            var firstName = User.FirstName;
            var lastName = User.LastName;
            var profilePic = "";
            if (lastName == null)
            {
                profilePic = $"{firstName[0]}{firstName[1]}";
            }
            else
            {
                profilePic = $"{firstName[0]}{lastName[0]}";
            }
            return profilePic.ToUpper();
        }
    }
}
