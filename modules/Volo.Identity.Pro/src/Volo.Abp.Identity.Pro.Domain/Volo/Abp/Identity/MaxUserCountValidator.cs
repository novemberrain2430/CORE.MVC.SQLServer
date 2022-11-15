using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Features;
using Volo.Abp.Identity.Features;

namespace Volo.Abp.Identity
{
    public class MaxUserCountValidator : IUserValidator<IdentityUser>
    {
        protected IFeatureChecker FeatureChecker { get; }
        protected IIdentityUserRepository UserRepository { get; }

        public MaxUserCountValidator(IFeatureChecker featureChecker, IIdentityUserRepository userRepository)
        {
            FeatureChecker = featureChecker;
            UserRepository = userRepository;
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            await CheckMaxUserCountAsync();

            return IdentityResult.Success;
        }

        protected virtual async Task CheckMaxUserCountAsync()
        {
            var maxUserCount = await FeatureChecker.GetAsync<int>(IdentityProFeature.MaxUserCount);
            if (maxUserCount <= 0)
            {
                return;
            }

            var currentUserCount = await UserRepository.GetCountAsync();
            if (currentUserCount >= maxUserCount)
            {
                throw new BusinessException(IdentityProErrorCodes.MaximumUserCount)
                    .WithData("MaxUserCount", maxUserCount);
            }
        }
    }
}
