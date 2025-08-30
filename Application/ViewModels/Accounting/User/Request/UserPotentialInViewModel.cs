using Common.Enum.User;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting.User.Request
{

    public class UserPotentialInViewModel
    {
        public UserTypeEnum UserType { get; set; }
        public string NationalCode { get; set; }
        public string NationalId { get; set; }
        public string Cellphone { get; set; }
    }

    public class UserPotentialPhoneConfirmViewModel
    {
        public string Cellphone { get; set; }
        public string VerificationCode { get; set; }
    }

    public class UserPhoneConfirmViewModel
    {
        public string UserName { get; set; }
        public string Cellphone { get; set; }
        public string VerificationCode { get; set; }
    }
    public class UserEmailConfirmViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }

    public class ResetPasswordByPhonenumberViewModel
    {
        public string Cellphone { get; set; }
        public string VerificationCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ResetPasswordByEmailViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string VerificationCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePassword
    {
        public string UserId { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }



}
