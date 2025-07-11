using ContactManagerApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Register ContactService
builder.Services.AddSingleton<ContactService>();

builder.Services.AddControllers();

// ✅ CORS policy: Allow all localhost ports
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost") // allow any localhost origin
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAngularApp");

// app.UseHttpsRedirection(); // optional
app.UseAuthorization();

app.MapControllers();
app.Run();
