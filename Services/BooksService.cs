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
        Task<Guid> CreateBook(Book book);
        Task<IEnumerable<Book>> Search(BookSearchDTO bookSearchDTO);
    }

    public class BooksService : IBooksService
    {
        private readonly IAmazonDynamoDB _dynamoClient;

        public BooksService(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        public async Task<Guid> CreateBook(Book book)
        {
            var newBookID = Guid.NewGuid();
            var newBook = new Dictionary<string, AttributeValue>
            {
                {"BookID", new AttributeValue {S = newBookID.ToString()}},
                {"UserID", new AttributeValue {S = book.UserID}},
                {"Title", new AttributeValue {S = book.Title}},
                {"Isbn", new AttributeValue {S = book.Isbn}},
                {"NumberOfPages", new AttributeValue {N = book.NumberOfPages.ToString()}},
                {"Authors", new AttributeValue {S = string.Join(",", book.Authors)}}
            };

            var putItem = new PutItemRequest
            {
                TableName = "Bookworm",
                Item = newBook
            };

            await _dynamoClient.PutItemAsync(putItem);
            
            return newBookID;
        }

        public async Task<IEnumerable<Book>> Search(BookSearchDTO bookSearchDTO)
        {
            return null;
        }
    }
}
