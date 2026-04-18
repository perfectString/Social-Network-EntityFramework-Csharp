using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialNetwork.Common.EntityValidations.PostValidation;

namespace SocialNetwork.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PostContentMaxLen)]
        public string Content { get; set; } = null!;
        public DateTime CreatedAt  { get; set; }

        [ForeignKey(nameof(Creator))]
        public int CreatorId  { get; set; }

        public virtual User Creator { get; set; } = null!;
    }
}
