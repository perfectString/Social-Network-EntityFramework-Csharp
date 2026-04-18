using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Data.Models.Enum;
using static SocialNetwork.Common.EntityValidations.MessageValidation;

namespace SocialNetwork.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MessageContentMaxLen)]
        public string Content { get; set; } = null!;

        [Required]
        [Column(TypeName = "DATETIME2")]
        public DateTime SentAt  { get; set; }

        [Required]
        public MessageStatus Status { get; set; }

        [ForeignKey(nameof(Conversation))]
        public int ConversationId { get; set; }
       public virtual Conversation Conversation  { get; set; } = null!;

        [ForeignKey(nameof(Sender))]
        public int SenderId { get; set; }

        public virtual User Sender { get; set; } = null!;
    }
}
