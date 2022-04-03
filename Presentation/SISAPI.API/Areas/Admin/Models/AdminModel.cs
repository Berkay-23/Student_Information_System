using System.ComponentModel.DataAnnotations;

namespace SISAPI.API.Areas.Admin.Models
{
    public class AdminModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
    }
}
