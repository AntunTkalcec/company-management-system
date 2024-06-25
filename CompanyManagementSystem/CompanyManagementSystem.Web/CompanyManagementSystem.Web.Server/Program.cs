using CompanyManagementSystem.Core.Interfaces.Seed;
using CompanyManagementSystem.Infrastructure.Authentication;
using CompanyManagementSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLogging(_ => _.AddConsole());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        IConfigurationSection tokenData = builder.Configuration.GetSection("TokenData");
        options.TokenValidationParameters = TokenValidationConfiguration.GetTokenValidationParameters(tokenData["Issuer"],
            tokenData["Audience"], tokenData["SecretKey"]);
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CompanyManagementSystem.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext(builder.Configuration.GetConnectionString("LocalConnection"));
builder.Services.RegisterServices();

builder.Services.Configure<TokenDataConfiguration>(builder.Configuration.GetSection("TokenData"));

WebApplication app = builder.Build();

app.UseExceptionHandler(_ => { });

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization));

app.UseRouting();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

IServiceScopeFactory scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (IServiceScope scope = scopeFactory.CreateScope())
{
    IDbInitializer? dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
    dbInitializer?.Initialize();
    dbInitializer?.SeedData();
}

app.Run();
