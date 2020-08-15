using System.ComponentModel.DataAnnotations;

namespace Project1.Website.Models
{
    public class LoginModel
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}