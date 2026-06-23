using UserManagementAPI.Services;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FIX: Now C# can find UserService
builder.Services.AddSingleton<UserService>();

var app = builder.Build();

app.MapGet("/", () => "User Management API is running!");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
        c.RoutePrefix = string.Empty; // makes Swagger UI open at root "/"
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();