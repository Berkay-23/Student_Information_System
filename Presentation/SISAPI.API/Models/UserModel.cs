﻿using System.ComponentModel.DataAnnotations;

namespace SISAPI.API.Models
{
    public class UserModel
    {
        [Required (ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }

        public string UserType { get; set; }
    }
}
