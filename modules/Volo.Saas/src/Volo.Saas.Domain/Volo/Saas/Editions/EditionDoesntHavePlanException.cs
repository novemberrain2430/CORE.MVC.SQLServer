using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Volo.Saas.Editions
{
    [Serializable]
    public class EditionDoesntHavePlanException : BusinessException
    {
        public EditionDoesntHavePlanException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public EditionDoesntHavePlanException(Guid editionId) : base(code: SaasErrorCodes.Edition.EditionDoesntHavePlan)
        {
            EditionId = editionId;
            
            WithData(nameof(EditionId), EditionId);
        }

        public virtual Guid EditionId { get; protected set; }
    }
}
