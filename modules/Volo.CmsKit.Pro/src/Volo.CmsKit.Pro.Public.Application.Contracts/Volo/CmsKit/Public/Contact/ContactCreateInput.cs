using System.ComponentModel.DataAnnotations;

namespace Volo.CmsKit.Public.Contact
{
    public class ContactCreateInput
    {
        [Required] 
        public string Name { get; set; }
        
        [Required] 
        public string Subject { get; set; }
        
        [Required] 
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required] 
        public string Message { get; set; }
        
        [Required] 
        public string RecaptchaToken { get; set; }
    }
}