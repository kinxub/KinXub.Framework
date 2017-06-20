using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KinXub.Framework
{
    public class LoginViewModel
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "The Account field is required")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }

        //[DisplayName("驗證碼")]
        //[Required(ErrorMessage = "The Verifycode field is required")]
        //public string Verifycode { get; set; }

        //[DisplayName("記住我")]
        //public bool Rememberme { get; set; }
    }
}
