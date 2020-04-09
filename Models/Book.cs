using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bookworm.Models
{
    public class Book
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public IEnumerable<string> Authors { get; set; }

    }
}
