using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Volo.Forms.Responses
{
    [Serializable]
    public class UserResponseAlreadyExistException : BusinessException
    {
        public UserResponseAlreadyExistException()
        {
            Code = FormsErrorCodes.UserResponseAlreadyExist;
        }
        
        public UserResponseAlreadyExistException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {

        }
    }
}