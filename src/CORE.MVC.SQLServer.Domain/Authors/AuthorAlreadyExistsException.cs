using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace CORE.MVC.SQLServer.Authors
{
    public class AuthorAlreadyExistsException : BusinessException
    {
        public AuthorAlreadyExistsException(string name)
            : base(SQLServerDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
