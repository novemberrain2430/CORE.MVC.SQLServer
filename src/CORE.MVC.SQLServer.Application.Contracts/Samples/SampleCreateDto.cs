using System;
using System.ComponentModel.DataAnnotations;

namespace CORE.MVC.SQLServer.Samples
{
    public class SampleCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? Date1 { get; set; }
        public int Year { get; set; }
        [StringLength(SampleConsts.CodeMaxLength)]
        public string Code { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsConfirm { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}