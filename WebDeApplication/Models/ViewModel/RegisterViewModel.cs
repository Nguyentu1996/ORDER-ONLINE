using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeApplication.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Đây là trường bắt buộc"), EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        //[Display(Name = "Mật Khẩu")]
        public string Password { get; set; }


        //[Required, DataType(DataType.Password)]
        ////[Display(Name = "Xác Nhận Mật Khẩu")]
        ////[Compare("MatKhau", ErrorMessage = "Mật khẩu không khớp!")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        public string UserName { get; set; }

    }
}
