using API.ViewModel.Responses;
using Application.Interfaces;
using AutoMapper;
using API.ViewModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        public NotificationController(
            INotificationService notificationService,
            ILogger<NotificationController> logger,
            IMapper mapper)
        {
            _notificationService = notificationService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Set source user from JWT claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    request.SourceUserId = userId;
                }

                // ✅ CORRECTO: ViewModel → Model (API → Application)
                var notificationModel = _mapper.Map<Application.Models.NotificationRequest>(request);
                var result = await _notificationService.SendNotificationAsync(notificationModel);

                if (!result.Success)
                    return BadRequest(new { message = result.Message });

                // ✅ CORRECTO: Model → ViewModel (Application → API)
                var response = _mapper.Map<NotificationResponse>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        // ... otros métodos ...
    }
}