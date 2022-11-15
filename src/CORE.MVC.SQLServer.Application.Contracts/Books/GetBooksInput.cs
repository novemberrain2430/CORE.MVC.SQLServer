using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace CORE.MVC.SQLServer.Books
{
    public class GetBooksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public Guid? UserId { get; set; }

        public GetBooksInput()
        {

        }
    }
}
