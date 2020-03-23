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

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<WeatherForecastController> _logger;

        public BooksController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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
        [Authorize]
        public async Task<int> CreateBook(Book book)
        {
            var httpResponse = await client.GetAsync("https://api.altmetric.com/v1/isbn/978-3-319-25557-6");
            return 1;
        }
    }
}
