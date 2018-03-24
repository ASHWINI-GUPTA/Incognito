using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incognito.Models
{
    public class Message
    {
        public Message()
        {
            //MessageCategories = new List<MessageCategory>();
        }

        public int Id { get; set; }

        /// <summary>
        /// The Id of Currently LoggedIn User... If LoggedIn Else Null
        /// </summary>
        public string UserId { get; set; }

        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Storing IP Address of the User for Security Concerns.
        /// </summary>
        //public string IpAddress { get; set; }

        public DateTime SentTime { get; set; }

        //public List<MessageCategory> MessageCategories { get; set; }
    }
}
