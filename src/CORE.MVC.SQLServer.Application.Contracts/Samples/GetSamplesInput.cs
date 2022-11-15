using Volo.Abp.Application.Dtos;
using System;

namespace CORE.MVC.SQLServer.Samples
{
    public class GetSamplesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public DateTime? Date1Min { get; set; }
        public DateTime? Date1Max { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public bool? IsConfirm { get; set; }
        public Guid? UserId { get; set; }

        public GetSamplesInput()
        {

        }
    }
}