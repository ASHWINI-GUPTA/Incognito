using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incognito.Models.ProfileViewModel
{
    public class ProfileVM
    {
        public UserProfileVM UserProfileVM { get; set; }

        public ProfileCardService ProfileCardService { get; set; }
    }
}
