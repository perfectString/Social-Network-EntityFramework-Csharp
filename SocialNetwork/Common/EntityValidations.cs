using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Common
{
    public static class EntityValidations
    {
        public static class UserValidation
        {
            public const int UsernameMaxLen = 20;
            public const int UsernameMinLen = 4;

            public const int EmailMaxLen = 60;
            public const int EmailMinLen = 8;

            public const int PasswordMinLen = 6;
        }

        public static class ConversationValidations
        {
            public const int TitleMaxLen = 30;
            public const int TitleMinLen = 2;
        }

        public static class PostValidation
        {
            public const int PostContentMaxLen = 300;
            public const int PostContentMinLen = 5;
        }

        public static class MessageValidation
        {
            public const int MessageContentMaxLen = 200;
            public const int MessageContentMinLen = 1;
        }
    }
}
