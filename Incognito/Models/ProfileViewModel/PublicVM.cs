using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incognito.Models.MessageViewModel;

namespace Incognito.Models.ProfileViewModel
{
    public class PublicVM
    {
        public MessageVM PublicMessage { get; set; }

        //public UserProfile PublicProfile { get; set; }
        public ProfileCardService ProfileCardService { get; set; }
    }
}
