using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignlR_Web_ApI.DTOs;
using SignlR_Web_ApI.Services.Contracts;

namespace SignlR_Web_ApI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService authService;
		public AuthController(IAuthService authService) {
			this.authService = authService;
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterDto registerDto) {
			if (!ModelState.IsValid)
				return BadRequest(new { Message = "Model state not valid" });
			try {
				var result = await authService.RegisterAsync(registerDto);
				if (!result.IsAuthenticated)
					return BadRequest(new { message = result.Message });

				SaveRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
				return Ok(result);
			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "An error occurred during registration.", Details = ex.Message });
			}
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto loginDto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try {
				var result = await authService.LoginAsync(loginDto);
				if (!result.IsAuthenticated)
					return BadRequest(new { message = result.Message });
				if(!string.IsNullOrEmpty(result.RefreshToken)){
					SaveRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
				}
				return Ok(result);
			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "An error occurred during Login.", Details = ex.Message });
			}
		}
		[HttpGet]
		public async Task<IActionResult> RefreshToken(){
			var refreshToken = Request.Cookies["refreshToken"];
			var result = await authService.RefreshTokenAsync(refreshToken);
			if (!result.IsAuthenticated){
				return Unauthorized(result);
			}
			SaveRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> RevokedToken() {
			var resfreshToken = Request.Cookies["refreshToken"];
			if(string.IsNullOrEmpty(resfreshToken))
				return BadRequest(new { Message = "No Spacific Refresh Token"});

			var result = await authService.RefreshTokenInvokeAsync(resfreshToken);
			if (!result)
				return BadRequest(new { Message = "InValid Refresh Token" });

			return Ok(new { Message = "Operation Successed" });
		}
		private void SaveRefreshTokenInCookie(string refreshToken, DateTime expiredOn) {
			CookieOptions cookieOptions = new CookieOptions() {
				Expires = expiredOn.ToLocalTime(),
				HttpOnly = true
			};
			Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
		}
	}
}
