using System.ComponentModel.DataAnnotations;

namespace Volo.Forms.Forms
{
    public class UpdateFormDto
    {
        [Required]
        [StringLength(FormConsts.MaxTitleLength)]
        public string Title { get; set; }
        
        [StringLength(FormConsts.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}