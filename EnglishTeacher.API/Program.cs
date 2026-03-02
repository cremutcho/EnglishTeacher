using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Application.Mappings;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Application.Services.Implementations;
using EnglishTeacher.API.Middlewares;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ======================================
// 🔹 Controllers
// ======================================
builder.Services.AddControllers();

// ======================================
// 🔹 Banco de Dados (EF Core)
// ======================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ======================================
// 🔐 Identity Configuration
// ======================================
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ======================================
// 🔐 JWT Configuration
// ======================================
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new Exception("JWT Key não configurada no appsettings.json");

if (string.IsNullOrWhiteSpace(jwtIssuer))
    throw new Exception("JWT Issuer não configurado no appsettings.json");

if (string.IsNullOrWhiteSpace(jwtAudience))
    throw new Exception("JWT Audience não configurado no appsettings.json");

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidateAudience = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// ======================================
// 🔹 Swagger + JWT
// ======================================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v3", new OpenApiInfo
    {
        Title = "EnglishTeacher API",
        Version = "v3",
        Description = "API para gerenciamento de alunos e professores (V3)"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// ======================================
// 🔹 AutoMapper (VERSÃO COMPATÍVEL)
// ======================================
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<StudentProfile>();
});

// ======================================
// 🔹 Services (Application Layer)
// ======================================
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// ======================================
// 🔹 Build
// ======================================
var app = builder.Build();

// ======================================
// 🔹 Pipeline HTTP
// ======================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v3/swagger.json", "EnglishTeacher API v3");
        c.RoutePrefix = string.Empty;
    });
}

app.MapGet("/", () => "EnglishTeacher API rodando 🚀");

app.UseHttpsRedirection();

// ✅ Middleware global de exceção
app.UseMiddleware<ExceptionMiddleware>();

// 🔐 Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();