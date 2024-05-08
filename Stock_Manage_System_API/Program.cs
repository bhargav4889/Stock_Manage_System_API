using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stock_Manage_System_API.Email_Services;
using Stock_Manage_System_API.SMS_Services;
using Stock_Manage_System_API.Reminder_Service;
using Microsoft.AspNetCore.Diagnostics;

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

// Register ReminderService as Scoped
builder.Services.AddScoped<ReminderService>();

// Register Background Services
builder.Services.AddHostedService<ReminderBackgroundProcess>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Static Files Configuration
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Images")),
        
    RequestPath = "/Images"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Fonts")),

    RequestPath = "/Fonts"
});

app.Run();
