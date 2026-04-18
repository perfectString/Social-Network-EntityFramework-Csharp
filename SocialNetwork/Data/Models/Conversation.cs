using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialNetwork.Common.EntityValidations.ConversationValidations;

namespace SocialNetwork.Data.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(TitleMaxLen)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime StartedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        = new HashSet<Message>();

        public virtual ICollection<UserConversation> UsersConversations { get; set; }
        = new HashSet<UserConversation>();

    }
}
