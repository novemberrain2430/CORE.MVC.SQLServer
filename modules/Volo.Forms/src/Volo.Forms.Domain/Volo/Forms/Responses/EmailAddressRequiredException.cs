using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Volo.Forms.Responses
{
    [Serializable]
    public class EmailAddressRequiredException : BusinessException
    {
        public EmailAddressRequiredException()
        {
            Code = FormsErrorCodes.EmailAddressRequired;
        }
        
        public EmailAddressRequiredException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {

        }
    }
}