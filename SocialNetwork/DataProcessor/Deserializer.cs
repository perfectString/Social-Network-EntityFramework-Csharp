using Newtonsoft.Json;
using SocialNetwork.Data;
using SocialNetwork.Data.Models;
using SocialNetwork.Data.Models.Enum;
using SocialNetwork.DataProcessor.ImportDTOs;
using SocialNetwork.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace SocialNetwork.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format.";
        private const string DuplicatedDataMessage = "Duplicated data.";
        private const string SuccessfullyImportedMessageEntity = "Successfully imported message (Sent at: {0}, Status: {1})";
        private const string SuccessfullyImportedPostEntity = "Successfully imported post (Creator {0}, Created at: {1})";

        public static string ImportMessages(SocialNetworkDbContext dbContext, string xmlString)
        {
            var rootName = "Messages";
            var result = new StringBuilder();

            ICollection<Message> messagesToImport = new List<Message>();

            

            IEnumerable<ImportMessagesDto> messagesDtos =
                XmlSerializerHelper.Deserialize<ImportMessagesDto[]>
                (xmlString, rootName);

            if (messagesDtos != null)
            {
                foreach (var messageDto in messagesDtos)
                {
                    if (!IsValid(messageDto))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isStatusValid = MessageStatus.TryParse(messageDto.Status, out MessageStatus messageStatusVal);
                    bool isDateValid = DateTime.TryParseExact(messageDto.SentAt, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture
                        , DateTimeStyles.None, out  DateTime dateTimeVal);

                    bool isSenderValid = dbContext.Users
                        .Any(u=> u.Id == messageDto.SenderId);

                    bool isConvoValid = dbContext
                        .Conversations
                        .Any(c=> c.Id == messageDto.ConversationId);

                    if (!isStatusValid || !isDateValid || !isSenderValid || !isConvoValid)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }


                    var isDuplicatedInBase = dbContext
                        .Messages
                        .Any(m => m.ConversationId == messageDto.ConversationId &&
                        m.Content == messageDto.Content &&
                        m.SentAt == dateTimeVal &&
                        m.Status == messageStatusVal &&
                        m.SenderId == messageDto.SenderId);

                    var isDuplicated = messagesToImport
                        .Any(m => m.ConversationId == messageDto.ConversationId &&
                        m.Content == messageDto.Content &&
                        m.SentAt == dateTimeVal &&
                        m.Status == messageStatusVal &&
                        m.SenderId == messageDto.SenderId);


                    if (isDuplicated || isDuplicatedInBase)
                    {
                        result.AppendLine(DuplicatedDataMessage);
                        continue;
                    }

                    var newMessage = new Message()
                    {
                        SentAt = dateTimeVal,
                        Content = messageDto.Content,
                        Status = messageStatusVal,
                        ConversationId = messageDto.ConversationId,
                        SenderId = messageDto.SenderId
                    };
                    messagesToImport.Add(newMessage);
                    result.AppendLine(String.Format(SuccessfullyImportedMessageEntity,
                        dateTimeVal.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture), messageStatusVal));

                }
                dbContext.AddRange(messagesToImport);
                dbContext.SaveChanges();
            }


            return result.ToString().TrimEnd();

        }

        public static string ImportPosts(SocialNetworkDbContext dbContext, string jsonString)
        {
            var result = new StringBuilder();

            ICollection<Post> postsToImport = new List<Post>();

            IEnumerable<ImportPostDto>? postDtos =
                JsonConvert.DeserializeObject<ImportPostDto[]>(jsonString);

            if (postDtos != null)
            {
                foreach (var postDto in postDtos)
                {
                    if (!IsValid(postDto))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isDateValid = DateTime.TryParseExact(postDto.CreatedAt, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture
                        , DateTimeStyles.None, out DateTime dateTimeVal);

                    bool isCreatorValid = dbContext.Users
                        .Any(u => u.Id == postDto.CreatorId);

                    if (!isDateValid || !isCreatorValid)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool doesPostExistInBase = dbContext.Posts
                        .Any(p=> 
                        p.CreatorId == postDto.CreatorId &&
                        p.Content == postDto.Content &&
                        p.CreatedAt == dateTimeVal);

                    bool doesPostExist = postsToImport
                       .Any(p =>
                       p.CreatorId == postDto.CreatorId &&
                       p.Content == postDto.Content &&
                       p.CreatedAt == dateTimeVal);

                    if (doesPostExist || doesPostExistInBase)
                    {
                        result.AppendLine(DuplicatedDataMessage);
                        continue;
                    }

                    var newPost = new Post()
                    {
                        Content = postDto.Content,
                        CreatedAt = dateTimeVal,
                        CreatorId = postDto.CreatorId
                    };
                    var username = dbContext.Users
                        .Where(u=> u.Id == newPost.CreatorId)
                        .Select(u => u.Username)
                        .FirstOrDefault();

                    postsToImport.Add(newPost);

                    result.AppendLine(String.Format(SuccessfullyImportedPostEntity, username,
                    dateTimeVal.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)));
                }
                dbContext.AddRange(postsToImport);
                dbContext.SaveChanges();
            }

            return result.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            ValidationContext validationContext = new ValidationContext(dto);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (ValidationResult validationResult in validationResults)
            {
                if (validationResult.ErrorMessage != null)
                {
                    string currentMessage = validationResult.ErrorMessage;
                }
            }

            return isValid;
        }
    }
}
