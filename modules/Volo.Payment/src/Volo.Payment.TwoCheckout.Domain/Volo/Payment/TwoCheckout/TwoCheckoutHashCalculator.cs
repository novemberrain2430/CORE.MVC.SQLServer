using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Volo.Payment.TwoCheckout
{
    public class TwoCheckoutHashCalculator : ITransientDependency
    {
        private readonly TwoCheckoutOptions _options;

        public TwoCheckoutHashCalculator(IOptions<TwoCheckoutOptions> options)
        {
            _options = options.Value;
        }

        public string GetMd5Hash(string hashString)
        {
            return HmacMd5HashHelper.GetMd5Hash(hashString, _options.Signature);
        }

        public string GetMd5HashForQueryStringParameters(string queryStringParams)
        {
            return GetMd5Hash(queryStringParams.Length + queryStringParams);
        }
    }
}
