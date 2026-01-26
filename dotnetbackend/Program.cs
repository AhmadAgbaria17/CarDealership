using dotnetbackend.Data;
using dotnetbackend.IRepository;
using dotnetbackend.IServices;
using dotnetbackend.models;
using dotnetbackend.Repository;
using dotnetbackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




builder.Services.AddIdentity<Person, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 12;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
    }
).AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme =
  options.DefaultChallengeScheme =
  options.DefaultForbidScheme =
  options.DefaultScheme =
  options.DefaultSignInScheme =
  options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidAudience = builder.Configuration["JWT:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(
      System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
      )
  };
});


builder.Services.AddScoped<ICarDealerShipsRepository, CarDealerShipsRepository>();
builder.Services.AddScoped<ICarDealerShipService, CarDealerShipService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddScoped<ILikedCarRepository, LikedCarRepository>();


var app = builder.Build();

// Ensure database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        
        // Wait for database to be ready (retry logic)
        var maxRetries = 10;
        var retryCount = 0;
        var migrationApplied = false;
        
        while (retryCount < maxRetries && !migrationApplied)
        {
            try
            {
                logger.LogInformation($"Attempting to connect to database (attempt {retryCount + 1}/{maxRetries})...");
                context.Database.Migrate(); // This will create the database if it doesn't exist and apply migrations
                logger.LogInformation("Database migrations applied successfully.");
                migrationApplied = true;
            }
            catch (Exception dbEx)
            {
                retryCount++;
                logger.LogWarning($"Database migration attempt {retryCount}/{maxRetries} failed: {dbEx.Message}");
                if (retryCount >= maxRetries)
                {
                    logger.LogError(dbEx, "Failed to migrate database after multiple retries.");
                    // Don't throw - let the app start, but log the error
                    break;
                }
                Thread.Sleep(3000); // Wait 3 seconds before retry
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialization.");
        // Don't throw - let the app start even if DB migration fails
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors(options =>
{
  options.AllowAnyMethod()
  .AllowAnyHeader()
  .AllowCredentials()
  .SetIsOriginAllowed(origin => true); // Allow any origin
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();

