using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incognito.Models
{
    public class ReportMessage
    {
        public int Id { get; set; }

        public Message Message { get; set; }

        public int MessageId { get; set; }

        public string Reason { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public DateTime ReportTime { get; set; }
    }
}
