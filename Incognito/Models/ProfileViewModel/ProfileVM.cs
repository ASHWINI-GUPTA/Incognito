using Incognito.Models.MessageViewModel;
using System.Collections.Generic;

namespace Incognito.Models.ProfileViewModel
{
    public class ProfileVM
    {
        public UserProfileVM UserProfileVM { get; set; }

        public ProfileCardService ProfileCardService { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public MessageVM PublicMessage { get; set; }
    }
}
