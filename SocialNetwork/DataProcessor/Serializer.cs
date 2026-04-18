using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialNetwork.Data;
using SocialNetwork.Data.Models.Enum;
using SocialNetwork.DataProcessor.ExportDTOs;
using SocialNetwork.Utilities;

namespace SocialNetwork.DataProcessor
{
    public class Serializer
    {
        public static string ExportUsersWithFriendShipsCountAndTheirPosts(SocialNetworkDbContext dbContext)
        {
            var rootname = "Users";
            ExportUserWFriendshipsDto[] userPostsExport = dbContext
                .Users
                .Include(p => p.Posts)
                .AsNoTracking()
                .OrderBy(u => u.Username)
                .Select(u => new ExportUserWFriendshipsDto
                {
                    FriendshipsCount = dbContext.Friendships.Count(x => x.UserOneId == u.Id
                    || x.UserTwoId == u.Id),
                    Username = u.Username,

                    Post = u.Posts
                    .OrderBy(p => p.Id)
                    .Select(p => new ExportPostsDto
                    {
                        Content = p.Content,
                        CreatedAt = p.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss")
                    }).ToArray()
                }).ToArray();

            return XmlSerializerHelper.Serialize(userPostsExport, rootname);
        }

        public static string ExportConversationsWithMessagesChronologically(SocialNetworkDbContext dbContext)
        {
            var ConvoWMsgChron = dbContext
                .Conversations
                .Include(m => m.Messages)
                .ThenInclude(m=> m.Sender)
                .AsNoTracking()
                .OrderBy(c => c.StartedAt)
                .ToArray()
                .Select(c => new
                {
                    Id = c.Id,
                    Title = c.Title,
                    StartedAt = c.StartedAt.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Messages = c.Messages
                    .OrderBy(m => m.SentAt)
                    .Select(m => new
                    {
                        Content = m.Content,
                        SentAt = m.SentAt.ToString("yyyy-MM-ddTHH:mm:ss"),
                        Status = (MessageStatus)m.Status,
                        SenderUsername = m.Sender.Username
                    }).ToArray()

                }).ToArray();

            return JsonConvert.SerializeObject(ConvoWMsgChron, Formatting.Indented);
        }
    }
}
