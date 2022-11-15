using System.ComponentModel.DataAnnotations;

namespace Volo.CmsKit.Admin.Contact
{
    public class UpdateCmsKitContactSettingDto
    {
        [Required]
        [EmailAddress]
        public string ReceiverEmailAddress { get; set; }
    }
}
