using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Books
{
    public class BookUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
    }
}