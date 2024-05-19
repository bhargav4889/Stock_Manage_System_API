using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Email_Services;
using Stock_Manage_System_API.Login_Service;
using Stock_Manage_System_API.Reminder_Service;
using Stock_Manage_System_API.ResetPassword_Service;
using Stock_Manage_System_API.SMS_Services;
using Stock_Manage_System_API;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register EmailSender
builder.Services.AddTransient<IEmailSender>(i => new EmailSender(
    builder.Configuration["EmailSettings:Host"],
    int.Parse(builder.Configuration["EmailSettings:Port"]),
    bool.Parse(builder.Configuration["EmailSettings:EnableSSL"]),
    builder.Configuration["EmailSettings:UserName"],
    builder.Configuration["EmailSettings:Password"]
));

// Register SMS Sender
builder.Services.AddTransient<ISmsSender>(s => new SmsSender(
    builder.Configuration["TwilioSettings:AccountSid"],
    builder.Configuration["TwilioSettings:AuthToken"],
    builder.Configuration["TwilioSettings:PhoneNumber"]
));

// Register JWT Service
builder.Services.AddScoped<JWT_Service>();

// Register ReminderService as Scoped
builder.Services.AddScoped<ReminderService>();

// Register DailyEmailService
builder.Services.AddScoped<DailyEmailService>();

// Register Background Services
builder.Services.AddHostedService<DailyEmailBackgroundService>();
builder.Services.AddHostedService<ReminderBackgroundProcess>();

// Dependency Injection for DAL and BAL
builder.Services.AddScoped<IAuthDAL, Auth_DALBase>();
builder.Services.AddScoped<IAuthBAL, Auth_BALBase>();

// Register Password Reset Service
builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();

// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("SpecificPolicy", builder =>
    {
        builder.WithOrigins("https://shree-ganesh-agro-management.somee.com")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

// Exception Handling Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(c => c.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        var response = new { Error = exception?.Message };
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(response);
    }));
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("SpecificPolicy");

// Function to configure static files for a given directory and request path
void ConfigureStaticFiles(string directoryName, string requestPath)
{
    var currentDirectory = Directory.GetCurrentDirectory();
    var directoryPath = Path.Combine(currentDirectory, directoryName);

    if (!Directory.Exists(directoryPath))
    {
        Console.WriteLine($"Warning: The directory '{directoryPath}' does not exist. Creating it now...");
        Directory.CreateDirectory(directoryPath);
        Console.WriteLine($"Info: Created directory '{directoryPath}'.");
    }

    try
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(directoryPath),
            RequestPath = requestPath
        });
        Console.WriteLine($"Info: Configured static files for '{directoryName}' at '{requestPath}'.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: Failed to configure static files for '{directoryName}' at '{requestPath}'. Exception: {ex.Message}");
    }
}

// Configure static files for Images and Fonts
ConfigureStaticFiles("Images", "/Images");
ConfigureStaticFiles("Fonts", "/Fonts");

app.Run();
