using System;
using System.ComponentModel.DataAnnotations;

namespace MuzON.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email cannot be null!")]
        [EmailAddress(ErrorMessage = "Please, enter correct email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Passwords not match!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email cannot be null!")]
        [EmailAddress(ErrorMessage = "Please, enter correct email address!")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email cannot be null")]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation doesn't match")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Old password cannot be null")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password cannot be null")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Password and confirmation doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}