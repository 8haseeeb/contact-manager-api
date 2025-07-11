using ContactManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Register ContactService
builder.Services.AddSingleton<ContactService>();

// Add controllers
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4300", "https://your-frontend-url.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();

// ✅ This part is CRITICAL for Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

app.Run();
