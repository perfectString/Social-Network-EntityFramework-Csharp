using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static SocialNetwork.Common.EntityValidations.MessageValidation;

namespace SocialNetwork.DataProcessor.ImportDTOs
{
    [XmlType("Message")]
    public class ImportMessagesDto
    {
        [XmlAttribute("SentAt")]
        [Required]
        public string SentAt { get; set; } = null!;

        [XmlElement]
        [Required]
        [MinLength(MessageContentMinLen)]
        [MaxLength(MessageContentMaxLen)]
        public string Content { get; set; } = null!;

        [XmlElement]
        [Required]
        public string Status { get; set; } = null!;

        [XmlElement]
        [Required]
        public int ConversationId { get; set; }

        [XmlElement]
        [Required]
        public int SenderId { get; set; }


    }
}
