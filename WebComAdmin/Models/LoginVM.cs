using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace DaXia.WebComAdmin
{
    public class LoginVM
    {
        [Required(ErrorMessage="请输入用户名")]
        [Display(Name= "用户名")]
        [DataType(DataType.Text)]
        string UserName { get; set; }

        [Required(ErrorMessage="请输入密码")]
        [Display(Name="密码")]
        [DataType(DataType.Text)]
        string Password { get; set; }
    }
}