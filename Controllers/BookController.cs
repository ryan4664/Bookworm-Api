using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bookworm.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAmazonDynamoDB _dynamoClient;

        public BooksController(ILogger<WeatherForecastController> logger, IAmazonDynamoDB dynamoClient)
        {
            _logger = logger;
            _dynamoClient = dynamoClient;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Book> GetAsync()
        {
            var httpResponse = await client.GetAsync("https://api.altmetric.com/v1/isbn/978-3-319-25557-6");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsAsync<Book>();
            return result;
        }

        [HttpPost]
        //[Authorize]
        public async Task<Guid> CreateBook(Book book)
        {
            var httpResponse = await client.GetAsync("https://api.altmetric.com/v1/isbn/978-3-319-25557-6");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsAsync<Book>();
            var id = Guid.NewGuid();

            var item = new Dictionary<string, AttributeValue>
            {
                {"BookID", new AttributeValue {S = id.ToString()}},
                {"Title", new AttributeValue {S = result.Title}},
                {"Authors", new AttributeValue {S = string.Join(",", result.Authors)}}
            };

            var putItem = new PutItemRequest
            {
                TableName = "Bookworm",
                Item = item
            };

            await _dynamoClient.PutItemAsync(putItem);

            return id;
        }

    }
}
