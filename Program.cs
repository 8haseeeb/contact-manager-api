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

// Use CORS
app.UseCors("AllowAngularApp");

// Optional: Skip HTTPS redirection for Railway
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ✅ Railway fix: Bind to dynamic PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

app.Run();
