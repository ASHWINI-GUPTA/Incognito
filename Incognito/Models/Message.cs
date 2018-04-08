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

        //public ApplicationUser Recevier { get; set; }

        [Required]
        public string RecevierId { get; set; }

        /// <summary>
        /// The Id of Currently LoggedIn User... If LoggedIn Else Null
        /// </summary>
        public string SenderId { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Text { get; set; }

        /// <summary>
        /// Storing IP Address of the User for Security Concerns.
        /// </summary>
        //public string IpAddress { get; set; }

        public DateTime SentTime { get; set; }

        //public List<MessageCategory> MessageCategories { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsArchived { get; set; }

        public bool IsReported { get; set; }

        [MaxLength(1000)]
        public string ReportReason { get; set; }
    }
}
