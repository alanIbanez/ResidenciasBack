using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, authService, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IAuthService authService, string token)
        {
            try
            {
                var user = await authService.ValidateTokenAsync(token);

                if (user != null)
                {
                    // Attach user to context on successful validation
                    context.Items["User"] = user;
                }
            }
            catch
            {
                // Token validation failed - do nothing
            }
        }
    }
}