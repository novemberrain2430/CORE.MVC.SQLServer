using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Payment.Requests;

namespace Volo.Payment.Iyzico.Pages.Payment.Iyzico
{
    public class PrePaymentModel : AbpPageModel
    {
        [BindProperty] public Guid PaymentRequestId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PrePaymentCheckoutButtonStyle { get; set; }

        protected PaymentRequestWithDetailsDto PaymentRequest { get; set; }

        protected IyzicoOptions IyzicoOptions { get; }

        protected PaymentOptions PaymentGatewayOptions { get; }

        protected IPaymentRequestAppService PaymentRequestAppService { get; }

        [BindProperty] public IyzicoCustomer Customer { get; set; }

        private readonly IPurchaseParameterListGenerator _purchaseParameterListGenerator;
        private readonly IOptions<PaymentWebOptions> _paymentGatewayOptions;

        public PrePaymentModel(
            IOptions<PaymentOptions> paymentGatewayOptions,
            IOptions<IyzicoOptions> iyzicoOptions,
            IPaymentRequestAppService paymentRequestAppService,
            IPurchaseParameterListGenerator purchaseParameterListGenerator,
            IOptions<PaymentWebOptions> paymentGatewayOptions1)
        {
            PaymentRequestAppService = paymentRequestAppService;
            _purchaseParameterListGenerator = purchaseParameterListGenerator;
            _paymentGatewayOptions = paymentGatewayOptions1;
            IyzicoOptions = iyzicoOptions.Value;
            PaymentGatewayOptions = paymentGatewayOptions.Value;

            Customer = new IyzicoCustomer();
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

            PrePaymentCheckoutButtonStyle = IyzicoOptions.PrePaymentCheckoutButtonStyle;
            PaymentRequest = await PaymentRequestAppService.GetAsync(PaymentRequestId);

            var currency = _purchaseParameterListGenerator.GetExtraParameterConfiguration(PaymentRequest).Currency;

            await PaymentRequestAppService.SetCurrencyAsync(new SetPaymentRequestCurrencyDto
            {
                Id = PaymentRequestId,
                Currency = currency
            });
        }


        public virtual async Task<IActionResult> OnPostContinueToCheckout()
        {
            PaymentRequest = await PaymentRequestAppService.GetAsync(PaymentRequestId);
            var config = _purchaseParameterListGenerator.GetExtraParameterConfiguration(PaymentRequest);
            var totalPrice = PaymentRequest.Products.Sum(p => p.TotalPrice).ToString("0.00");
            var callbackUrl = _paymentGatewayOptions.Value.Gateways[IyzicoConsts.GatewayName].PostPaymentUrl +
                              "?paymentRequestId=" + PaymentRequest.Id;

            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale = config.Locale,
                ConversationId = PaymentRequest.Id.ToString(),
                Price = totalPrice,
                PaidPrice = totalPrice,
                Currency = config.Currency,
                BasketId = Guid.NewGuid().ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = callbackUrl,
                EnabledInstallments = new List<int> {1},
                Buyer = new Buyer
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Customer.Name,
                    Surname = Customer.Surname,
                    Email = Customer.Email,
                    IdentityNumber = Customer.IdentityNumber,
                    RegistrationAddress = Customer.Address.Description,
                    Ip = Customer.IpAddress,
                    City = Customer.Address.City,
                    Country = Customer.Address.Country,
                    ZipCode = Customer.Address.ZipCode
                }
            };

            var address = new Address
            {
                ContactName = Customer.Name,
                City = Customer.Address.City,
                Country = Customer.Address.Country,
                Description = Customer.Address.Description,
                ZipCode = Customer.Address.ZipCode
            };

            request.ShippingAddress = address;
            request.BillingAddress = address;
            request.BasketItems = new List<BasketItem>();

            foreach (var product in PaymentRequest.Products)
            {
                for (int i = 0; i < product.Count; i++)
                {
                    request.BasketItems.Add(new BasketItem
                    {
                        Id = product.Code,
                        Name = product.Name,
                        Category1 = "Software",
                        ItemType = BasketItemType.VIRTUAL.ToString(),
                        Price = product.UnitPrice.ToString("0.00")
                    });
                }
            }

            var checkoutFormInitialize = CheckoutFormInitialize.Create(request, new Iyzipay.Options
            {
                ApiKey = IyzicoOptions.ApiKey,
                SecretKey = IyzicoOptions.SecretKey,
                BaseUrl = IyzicoOptions.BaseUrl
            });

            if (!checkoutFormInitialize.ErrorMessage.IsNullOrEmpty())
            {
                throw new UserFriendlyException(checkoutFormInitialize.ErrorMessage);
            }

            return Redirect(checkoutFormInitialize.PaymentPageUrl);
        }
    }
}
