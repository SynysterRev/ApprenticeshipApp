using JuniorOnly.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.DTO.Register
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailAlreadyRegister", controller: "Account", ErrorMessage = "Email is already used")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public UserRole Role {  get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
