using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MAYShop.Models
{
    public class ChangePasswordModelView
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu hiện tại")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới")]
        [StringLength(100,ErrorMessage ="Mật khẩu tối đa {0} kí tự và tối thiểu {2} kí tự.",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác thực mật khẩu mới")]
        [Compare("NewPasswrod",ErrorMessage = "Mật khẩu mới với mật khẩu xác thực không đúng")]
        public string ConfirmPassword { get; set; }

    }
}