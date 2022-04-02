using System.ComponentModel.DataAnnotations;

namespace SISAPI.API.Models
{
    public class UserModel
    {
        [Required (ErrorMessage = "Mail alanı boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }

        public string UserType { get; set; }
    }
}
