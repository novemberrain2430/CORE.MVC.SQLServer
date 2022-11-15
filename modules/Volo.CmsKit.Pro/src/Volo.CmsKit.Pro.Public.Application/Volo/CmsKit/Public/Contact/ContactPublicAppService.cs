using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.CmsKit.Contact;

namespace Volo.CmsKit.Public.Contact
{
    public class ContactPublicAppService : ApplicationService, IContactPublicAppService
    {
        protected ContactEmailSender ContactEmailSender { get; }

        public ContactPublicAppService(ContactEmailSender contactEmailSender)
        {
            ContactEmailSender = contactEmailSender;
        }
        
        public virtual async Task SendMessageAsync(ContactCreateInput input)
        {
            await ContactEmailSender.SendAsync(input.Name, input.Subject, input.Email, input.Message);
        }
    }
}