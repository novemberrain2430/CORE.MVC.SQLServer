using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace CORE.MVC.SQLServer.Weekends
{
    public class WeekendDto : Entity<long>
    {
        public int WeekendID { set; get; }
        public bool IsYes { set; get; }
    }
}
