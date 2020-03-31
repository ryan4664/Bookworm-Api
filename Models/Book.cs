using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookworm.Models
{
    public class Book
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }

    }
}
