using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Account.Public.Web.Pages.Account
{
    public class LinkLoggedModel : AccountPageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid? LinkUserId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid? LinkTenantId { get; set; }

        public string TenantAndUserName { get; set; }

        public bool IsSpaReturnUrl { get; set; }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            if (LinkUserId == null)
            {
                return await RedirectToLoginPageAsync();
            }

            if (!await IdentityLinkUserAppService.IsLinkedAsync(new IsLinkedInput
            {
                UserId = LinkUserId.Value,
                TenantId = LinkTenantId
            }))
            {
                return await RedirectToLoginPageAsync();
            }

            using (CurrentTenant.Change(LinkTenantId))
            {
                TenantConfiguration tenant = null;
                if (LinkTenantId.HasValue)
                {
                    var tenantStore = HttpContext.RequestServices.GetRequiredService<ITenantStore>();
                    tenant = await tenantStore.FindAsync(LinkTenantId.Value);
                }
                var user = await UserManager.FindByIdAsync(LinkUserId.Value.ToString());
                if (user == null)
                {
                    return await RedirectToLoginPageAsync();
                }
                TenantAndUserName = tenant != null ? $"{tenant.Name}\\{user.UserName}" : user.UserName;
            }

            //TODO: Change handler=linkLogin to a special URL.
            IsSpaReturnUrl = !ReturnUrl.IsNullOrWhiteSpace() &&
                             ReturnUrl.Contains("handler=linkLogin", StringComparison.OrdinalIgnoreCase);

            return Page();
        }

        public virtual string GetReturnUrl(string returnUrl, string returnUrlHash)
        {
            return base.GetRedirectUrl(returnUrl, returnUrlHash);
        }

        public virtual string GetSpaReturnUrl(string returnUrl)
        {
            try
            {
                returnUrl = new Uri(returnUrl).GetLeftPart(UriPartial.Path);
            }
            catch (Exception e)
            {
                Logger.LogException(e);
            }

            return returnUrl;
        }

        public virtual string GetSpaLinkLoginReturnUrl(string returnUrl)
        {
            try
            {
                returnUrl = $"{new Uri(returnUrl).GetLeftPart(UriPartial.Path).RemovePostFix("/")}?handler=linkLogin&linkUserId={CurrentUser.Id.Value:D}";
                if (CurrentTenant.Id.HasValue)
                {
                    returnUrl += $"&linkTenantId={CurrentTenant.Id.Value:D}";
                }
            }
            catch (Exception e)
            {
                Logger.LogException(e);
            }

            return returnUrl;
        }

        public virtual Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }

        protected virtual Task<IActionResult> RedirectToLoginPageAsync()
        {
            return Task.FromResult<IActionResult>(RedirectToPage("./Login", new
            {
                ReturnUrl = ReturnUrl,
                ReturnUrlHash = ReturnUrlHash
            }));
        }
    }
}
