using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace CORE.MVC.SQLServer.Holidays
{
    public class HolidayDto : Entity<long>
    {
        public string Name { set; get; }
        public int Month { set; get; }
        public int Year { set; get; }
        public DateTime? Day { set; get; }
        public int HeSo { set; get; }
        public string Note { set; get; }
    }
}
