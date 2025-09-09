using API.ViewModel.Response;
using Application.Interfaces;
using Application.Models.Auth;
using AutoMapper;
using API.ViewModel.Requests;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, IMapper mapper)
        {
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // ✅ CORRECTO: ViewModel → Model (API → Application)
                var loginModel = _mapper.Map<LoginModel>(request);
                var result = await _authService.AuthenticateAsync(loginModel);

                if (!result.Success)
                    return Unauthorized(new { message = result.Message });

                // ✅ CORRECTO: Model → ViewModel (Application → API)
                var response = _mapper.Map<AuthResponse>(result);

                if (result.User != null)
                {
                    response.User = _mapper.Map<UserResponse>(result.User);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        // ... otros métodos similares ...
    }
}