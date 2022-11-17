using CORE.MVC.SQLServer.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CORE.MVC.SQLServer.Weekends
{
    public class CreateUpdateWeekendDto
    {
        [Required]
        public int WeekendID { get; set; }

        [Required]
        public bool IsYes { set; get; } = false;
    }
}
