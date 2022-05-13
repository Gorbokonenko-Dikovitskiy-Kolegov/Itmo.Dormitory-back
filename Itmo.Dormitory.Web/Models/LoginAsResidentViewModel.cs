using System.ComponentModel.DataAnnotations;

namespace Itmo.Dormitory.Web.Models
{
    public class LoginAsResidentViewModel
    {
        [Required]
        [Display(Name = "Isu number")]
        public string Isu { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
