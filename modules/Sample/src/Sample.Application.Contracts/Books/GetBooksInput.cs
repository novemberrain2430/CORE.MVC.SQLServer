using Volo.Abp.Application.Dtos;
using System;

namespace Sample.Books
{
    public class GetBooksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public GetBooksInput()
        {

        }
    }
}