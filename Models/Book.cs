using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Bookworm.Models
{
    public class Book
    {
        //[Required]
        //[StringLength(100, MinimumLength = 3)]
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("authors")]
        public IEnumerable<string> Authors { get; set; }
        public string Isbn { get; set; }
        [JsonProperty("numberOfPages")]
        public int NumberOfPages { get; set; }
        [JsonProperty("userID")]
        public string UserID { get; set; }
    }
}
