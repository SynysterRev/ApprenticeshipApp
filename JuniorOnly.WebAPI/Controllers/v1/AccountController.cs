using Asp.Versioning;
using JuniorOnly.Application.DTO.Account;
using JuniorOnly.Application.DTO.Register;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Controllers;
using JuniorOnly.Domain.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuniorOnly.WebAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class AccountController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationDto>> RegisterUser(RegisterDto registerDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, registerDto.Role.ToString());
            if (!addRoleResult.Succeeded)
            {
                var errors = addRoleResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            var response = await _jwtService.CreateJwtToken(user);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDto>> LoginUser(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
                
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var response = await _jwtService.CreateJwtToken(user);
            return Ok(response);
        }

        [HttpGet("logout")]
        public async Task<ActionResult<ApplicationUser>> LogoutUser()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

            [HttpGet]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email) != null);
        }
    }
}
