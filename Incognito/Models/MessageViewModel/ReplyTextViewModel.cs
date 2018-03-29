using System;
using System.ComponentModel.DataAnnotations;

namespace Incognito.Models.MessageViewModel
{
    public class ReplyTextViewModel
    {
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
    }
}
