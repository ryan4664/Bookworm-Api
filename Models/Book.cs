using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Amazon.DynamoDBv2.DataModel;

namespace Bookworm.Models
{
    [DynamoDBTable("Bookworm")]
    public class Book
    {
        public string BookID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("authors")]
        public string Authors { get; set; }
        
        public string Isbn { get; set; }
        
        [JsonProperty("numberOfPages")]
        public int NumberOfPages { get; set; }
        
        [JsonProperty("userID")]
        public string UserID { get; set; }
    }
}
