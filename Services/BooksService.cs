using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Bookworm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookworm.Services
{
    public interface IBooksService
    {
        Task<Guid> SaveBook(Book book);
    }

    public class BooksService : IBooksService
    {
        private readonly IAmazonDynamoDB _dynamoClient;

        public BooksService(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        public async Task<Guid> SaveBook(Book book)
        {
            var id = Guid.NewGuid();

            var item = new Dictionary<string, AttributeValue>
            {
                {"BookID", new AttributeValue {S = id.ToString()}},
                {"Title", new AttributeValue {S = book.Title}},
                {"Authors", new AttributeValue {S = string.Join(",", book.Authors)}}
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
