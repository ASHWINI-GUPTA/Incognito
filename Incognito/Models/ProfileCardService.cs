using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incognito.Models
{
    public class ProfileCardService
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Twitter { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetProfilePic()
        {
            var firstName = FirstName;
            var lastName = LastName;
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
