using System;

namespace Volo.Saas.Host.Dtos
{
    public class SubscriptionInfoUpdateDto
    {
        public Guid EditionId { get; set; }
        public Guid PaymentRequestId { get; set; }
        public DateTime EndDate { get; set; }
    }
}
