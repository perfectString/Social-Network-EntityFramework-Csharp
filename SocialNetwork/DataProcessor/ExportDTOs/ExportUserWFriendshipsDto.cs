using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SocialNetwork.DataProcessor.ExportDTOs
{
    [XmlType("User")]
    public class ExportUserWFriendshipsDto
    {
        [XmlAttribute("Friendships")]
        public int FriendshipsCount { get; set; }

        [XmlElement("Username")]
        public string Username { get; set; } = null!;

        [XmlArray("Posts")]
        public ExportPostsDto[] Post { get; set; } = null!;
    }

    [XmlType("Post")]
    public class ExportPostsDto
    {
        [XmlElement]
        public string Content { get; set; } = null!;

        [XmlElement]
        public string CreatedAt { get; set; } = null!;
    }
}
