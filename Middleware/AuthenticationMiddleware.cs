namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        private const string ValidToken =
            "TechHive-2026-SecretToken";

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(
                    "Authorization",
                    out var token))
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsJsonAsync(
                    new { error = "Authorization token missing." });

                return;
            }

            var receivedToken =
                token.ToString().Replace("Bearer ", "");

            if (receivedToken != ValidToken)
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsJsonAsync(
                    new { error = "Invalid token." });

                return;
            }

            await _next(context);
        }
    }
}