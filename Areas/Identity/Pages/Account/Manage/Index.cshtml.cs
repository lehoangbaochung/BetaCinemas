using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BetaCinemas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BetaCinemas.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;

        public IndexModel(
            UserManager<Member> userManager,
            SignInManager<Member> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Họ tên")]
            public string FullName { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Ngày sinh")]
            public DateTime Birthday { get; set; }

            [Display(Name = "Giới tính")]
            public bool Gender { get; set; }

            [Phone]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Địa chỉ")]
            public string HomeAddress { get; set; }

            [Display(Name = "Số CMND/CCCD/Hộ chiếu")]
            public string CardNumber { get; set; }
        }

        private async Task LoadAsync(Member member)
        {
            var userName = await _userManager.GetUserNameAsync(member);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(member);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FullName = member.FullName,
                Birthday = member.Birthday,
                Gender = member.Gender,
                HomeAddress = member.HomeAddress,
                CardNumber = member.CardNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Không thể tải dữ liệu thành viên với ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Không thể tải dữ liệu thành viên với ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Đã xảy ra sự cố khi thiết lập số điện thoại này";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Tài khoản thành viên của bạn đã được cập nhật";
            return RedirectToPage();
        }
    }
}
