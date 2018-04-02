using System.ComponentModel.DataAnnotations;

namespace Incognito.Models.MessageViewModel
{
    public class MessageViewModel
    {
        [Required]
        public string ReceiverId { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Text { get; set; }
    }
}
