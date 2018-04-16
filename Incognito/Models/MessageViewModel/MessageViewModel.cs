using System.ComponentModel.DataAnnotations;

namespace Incognito.Models.MessageViewModel
{
    public class MessageVM
    {
        [Required]
        public string ReceiverId { get; set; }

        [Required]
        [StringLength(2048, ErrorMessage = "Word limit is 2048 characters")]
        public string Text { get; set; }
    }
}
