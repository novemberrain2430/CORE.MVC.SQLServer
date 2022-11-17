using System;
using System.Collections.Generic;
using System.Text;

namespace CORE.MVC.SQLServer.InOuts
{
    public class CreateUpdateInOutDto
    {
        public InOutType InOutType { get; set; }

        public DateTime IntOutDate { get; set; } = DateTime.Now;
        public AuthenType AuthenType { get; set; } = AuthenType.QrCode;
    }
}
