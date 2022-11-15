using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Payment.Requests
{
    public class PaymentRequest : CreationAuditedAggregateRoot<Guid>
    {
        public virtual ICollection<PaymentRequestProduct> Products { get; protected set; }

        public PaymentRequestState State { get; private set; }

        /// <summary>
        /// Currency code ex: USD, EUR
        /// </summary>
        [CanBeNull]
        public string Currency { get; set; }

        [CanBeNull]
        public string Gateway { get; set; }

        [CanBeNull]
        public string FailReason { get; private set; }

        public DateTime? EmailSendDate { get; set; }

        public string ExternalSubscriptionId { get; protected set; }

        private PaymentRequest()
        {

        }

        public PaymentRequest(Guid id)
        {
            Id = id;
            Products = new List<PaymentRequestProduct>();
        }

        public void SetExternalSubscriptionId([NotNull] string externalSubscriptionId)
        {
            if (!ExternalSubscriptionId.IsNullOrEmpty())
            {
                throw new BusinessException(PaymentDomainErrorCodes.Requests.CantUpdateExternalSubscriptionId);
            }

            ExternalSubscriptionId = Check.NotNullOrEmpty(externalSubscriptionId, nameof(externalSubscriptionId));
        }

        public PaymentRequestProduct AddProduct(
            [NotNull] string code,
            [NotNull] string name,
            PaymentType paymentType = PaymentType.OneTime,
            float unitPrice = 0,
            int count = 1,
            Guid? planId = null,
            float? totalPrice = null,
            Dictionary<string, IPaymentRequestProductExtraParameterConfiguration> extraProperties = null)
        {
            var product = new PaymentRequestProduct(
                Id,
                code,
                name,
                paymentType,
                unitPrice,
                count: count,
                planId: planId,
                totalPrice: totalPrice
            );

            if (extraProperties != null)
            {
                foreach (var extraProperty in extraProperties)
                {
                    product.SetProperty(extraProperty.Key, extraProperty.Value);
                }
            }

            Products.Add(product);

            return product;
        }

        public void Complete()
        {
            if (State != PaymentRequestState.Waiting)
            {
                throw new ApplicationException(
                    $"Can not complete a payment in '{State}' state!"
                );
            }

            State = PaymentRequestState.Completed;
        }

        public void Failed([CanBeNull] string reason = null)
        {
            if (State != PaymentRequestState.Waiting)
            {
                throw new ApplicationException(
                    $"Can not fail a payment in '{State}' state!"
                );
            }

            State = PaymentRequestState.Failed;
            FailReason = reason;
        }
    }
}
