using api.Data;
using api.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<IUnitsService, UnitsService>();

// Add controllers
builder.Services.AddControllers();

// Configure PostgreSQL database context
var connectionString = Env.GetString("DATABASE_CONNECTION_STRING")
                       ?? throw new InvalidOperationException(
                           "DATABASE_CONNECTION_STRING is not set in environment variables");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configure CORS policy
var allowedHosts = Env.GetString("ALLOWED_HOSTS")?.Split(',')
                   ?? throw new InvalidOperationException("ALLOWED_HOSTS is not set in environment variables");
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedHosts)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}

// Use CORS
app.UseCors();

// Use controllers
app.MapControllers();

app.Run();