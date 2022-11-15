﻿using System;
using System.Collections.Generic;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;
using Volo.Payment.Requests;

namespace Volo.Payment.Subscription
{
    [Serializable]
    [EventName("Volo.Payment.SubscriptionCreated")]
    public class SubscriptionCreatedEto : EtoBase, IHasExtraProperties
    {
        public Guid PaymentRequestId { get; set; }

        public PaymentRequestState State { get; private set; }

        public string Currency { get; set; }

        public string Gateway { get; set; }

        public string ExternalSubscriptionId { get; set; }

        public DateTime PeriodEndDate { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
