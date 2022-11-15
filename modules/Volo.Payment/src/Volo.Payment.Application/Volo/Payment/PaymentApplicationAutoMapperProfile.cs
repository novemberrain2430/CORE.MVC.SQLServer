using AutoMapper;
using System.Linq;
using Volo.Abp.AutoMapper;
using Volo.Payment.Plans;
using Volo.Payment.Requests;
using Volo.Payment.Subscription;

namespace Volo.Payment
{
    public class PaymentApplicationAutoMapperProfile : Profile
    {
        public PaymentApplicationAutoMapperProfile()
        {
            CreateMap<PaymentRequest, PaymentRequestDto>();

            CreateMap<PaymentRequestProduct, PaymentRequestProductDto>();

            CreateMap<PaymentRequest, PaymentRequestWithDetailsDto>();

            CreateMap<PaymentRequest, SubscriptionCreatedEto>()
                .ForMember(
                    p => p.PaymentRequestId,
                    opts => opts.MapFrom(x => x.Id))
                .Ignore(p => p.Properties)
                .Ignore(p => p.PeriodEndDate);
            

            CreateMap<PaymentRequestProduct, PaymentRequestProductCompletedEto>()
                .ForMember(p => p.Properties, opts => opts.MapFrom(p => p.ExtraProperties.ToDictionary(k => k.Key, v => v.Value.ToString())));

            CreateMap<PaymentRequestDto, SubscriptionCreatedEto>()
                .ForMember(
                    p => p.PaymentRequestId,
                    opts => opts.MapFrom(p => p.Id))
                .Ignore(p => p.Properties)
                .Ignore(p => p.PeriodEndDate);

            CreateMap<PaymentRequestDto, SubscriptionCanceledEto>()
                .ForMember(
                    p => p.PaymentRequestId,
                    opts => opts.MapFrom(p => p.Id))
                .Ignore(p => p.Properties)
                .Ignore(p => p.PeriodEndDate);

            CreateMap<PaymentRequestDto, SubscriptionUpdatedEto>()
                .ForMember(
                    p => p.PaymentRequestId,
                    opts => opts.MapFrom(p => p.Id))
                .Ignore(p => p.Properties)
                .Ignore(p => p.PeriodEndDate);


            CreateMap<GatewayPlan, GatewayPlanDto>();

            CreateMap<Plan, PlanDto>();
        }
    }
}
