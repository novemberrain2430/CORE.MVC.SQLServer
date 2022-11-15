using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Payment.Requests;

namespace Volo.Payment.Payu.Pages.Payment.Payu
{
    public class PrePaymentModel : AbpPageModel
    {
        [BindProperty] public Guid PaymentRequestId { get; set; }

        public List<PurchaseParameter> PurchaseParameters { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PrePaymentCheckoutButtonStyle { get; set; }

        protected PaymentRequestWithDetailsDto PaymentRequest { get; set; }
        protected PayuOptions PayuOptions { get; }
        protected PaymentOptions PaymentGatewayOptions { get; }
        protected IPaymentRequestAppService PaymentRequestAppService { get; }

        private readonly IPurchaseParameterListGenerator _purchaseParameterListGenerator;

        public PrePaymentModel(
            IOptions<PaymentOptions> paymentGatewayOptions,
            IOptions<PayuOptions> payuOptions,
            IPaymentRequestAppService paymentRequestAppService,
            IPurchaseParameterListGenerator purchaseParameterListGenerator)
        {
            PaymentRequestAppService = paymentRequestAppService;
            _purchaseParameterListGenerator = purchaseParameterListGenerator;
            PayuOptions = payuOptions.Value;
            PaymentGatewayOptions = paymentGatewayOptions.Value;
        }

        public virtual ActionResult OnGet()
        {
            return BadRequest();
        }

        public virtual async Task OnPostAsync()
        {
            if (CurrentUser.Id != null)
            {
                Name = CurrentUser.Name;
                Surname = CurrentUser.SurName;
                Email = CurrentUser.Email;
            }

            PrePaymentCheckoutButtonStyle = PayuOptions.PrePaymentCheckoutButtonStyle;
            PaymentRequest = await PaymentRequestAppService.GetAsync(PaymentRequestId);
            PurchaseParameters = _purchaseParameterListGenerator.Generate(PaymentRequest);

            var currency = _purchaseParameterListGenerator.GetExtraParameterConfiguration(PaymentRequest).Currency;

            await PaymentRequestAppService.SetCurrencyAsync(new SetPaymentRequestCurrencyDto
            {
                Id = PaymentRequestId,
                Currency = currency
            });
        }
    }
}
