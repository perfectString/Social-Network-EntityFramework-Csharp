using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static SocialNetwork.Common.EntityValidations.PostValidation;

namespace SocialNetwork.DataProcessor.ImportDTOs
{
    public class ImportPostDto
    {
        /*
         *     "Content": "Just finished a fantastic hiking trip in the mountains! Nature is truly refreshing.",
    "CreatedAt": "2025-02-24T08:30:00",
    "CreatorId": 1

         */

        [Required]
        [JsonProperty("Content")]
        [MaxLength(PostContentMaxLen)]
        [MinLength(PostContentMinLen)]
        public string Content { get; set; } = null!;

        [Required]
        [JsonProperty("CreatedAt")]
        public string CreatedAt { get; set; } = null!;

        [Required]
        [JsonProperty("CreatorId")]
        public int CreatorId { get; set; }

    }
}
