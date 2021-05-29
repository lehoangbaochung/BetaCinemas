using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    public partial class Member : IdentityUser
    {
        public Member()
        {
            Contacts = new HashSet<Contact>();
            Tickets = new HashSet<Ticket>();
        }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Pass { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Địa chỉ")]
        public string HomeAddress { get; set; }

        [Display(Name = "Số CMND/CCCD/Hộ chiếu")]
        public string CardNumber { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        [Display(Name = "Số vé đã đặt")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
