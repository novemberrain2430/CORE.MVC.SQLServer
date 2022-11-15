using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Volo.Forms.Responses
{
    [Serializable]
    public class ResponseNotEditableException : BusinessException
    {
        public ResponseNotEditableException()
        {
            Code = FormsErrorCodes.ResponseNotEditable;
        }
        
        public ResponseNotEditableException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {

        }
    }
}