using System.ComponentModel.DataAnnotations;

namespace Stanford_University.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = " Please Enter the Email_ID")]
        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter the Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}
