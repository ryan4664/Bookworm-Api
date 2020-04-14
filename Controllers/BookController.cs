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
using Bookworm.Services;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
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

        [HttpGet("user/{userID}")]
        public async Task<IEnumerable<Book>> GetBooksByUserIDAsync(string userID)
        {
            return await _booksService.GetBooksByUserIDAsync(userID);
        }

        [HttpPost]
        [Authorize]
        public async Task<Guid> CreateBook(Book book)
        {
            return await _booksService.CreateBook(book);
        }

    }
}
