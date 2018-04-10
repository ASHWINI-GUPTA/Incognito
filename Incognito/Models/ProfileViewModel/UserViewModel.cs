using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incognito.Models.ProfileViewModel
{
    public class UserViewModel
    {
        public IEnumerable<Message> Messages { get; set; }

        public Profile Profile { get; set; }
    }
}
