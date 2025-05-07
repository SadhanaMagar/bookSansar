using bookSansar;
using bookSansar.Repositories;
using bookSansar.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure the port explicitly
    builder.WebHost.UseUrls("http://localhost:5000");

    // Add console logging
    builder.Logging.AddConsole();
    builder.Logging.SetMinimumLevel(LogLevel.Information);

    // Basic setup
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "BookSansar API",
            Version = "v1",
            Description = "API for BookSansar application"
        });
    });

    // Add endpoint routing
    builder.Services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

    // Database
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Services
    builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
    builder.Services.AddScoped<IReviewService, ReviewService>();

    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Middleware
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");

    app.MapControllers();

    // Start the application
    var swaggerUrl = "http://localhost:5000/swagger";
    Console.WriteLine("Starting application...");
    Console.WriteLine($"Swagger UI: {swaggerUrl}");
    Console.WriteLine($"API: http://localhost:5000/api");

    // Open browser automatically
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = swaggerUrl,
            UseShellExecute = true
        });
        Console.WriteLine("Opening Swagger UI in your default browser...");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Could not open browser automatically: {ex.Message}");
        Console.WriteLine($"Please manually open: {swaggerUrl}");
    }

    Console.WriteLine("Press Ctrl+C to stop");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting application: {ex.Message}");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    Environment.Exit(1);
}
