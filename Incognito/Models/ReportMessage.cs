using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Incognito.Models
{
    public class ReportMessage
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Reason { get; set; }

        public DateTime ReportTime { get; set; }
        
        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }

        [ForeignKey("MessageId")]
        public Message Message { get; set; }

    }
}
