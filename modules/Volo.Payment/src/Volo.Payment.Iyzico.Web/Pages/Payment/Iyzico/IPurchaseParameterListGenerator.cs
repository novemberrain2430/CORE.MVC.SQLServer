using Volo.Payment.Requests;

namespace Volo.Payment.Iyzico.Pages.Payment.Iyzico
{
    public interface IPurchaseParameterListGenerator
    {
        IyzicoPaymentRequestExtraParameterConfiguration GetExtraParameterConfiguration(PaymentRequestWithDetailsDto paymentRequest);
    }
}
