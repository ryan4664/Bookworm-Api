using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookworm.Models
{
    public class BookSearchDTO
    {
        public string isbn { get; set; }
        public string title { get; set; }
        public string authorLastName { get; set; }
    }
}
