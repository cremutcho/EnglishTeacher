using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Application.Mappings;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Application.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
// 🔹 Swagger + JWT
// ======================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EnglishTeacher API",
        Version = "v1",
        Description = "API para gerenciamento de alunos e professores"
    });

    // 🔐 JWT no Swagger
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
// 🔹 AutoMapper
// ======================================
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<StudentProfile>();
});

// ======================================
// 🔹 Services (Application Layer)
// ======================================
builder.Services.AddScoped<IStudentService, StudentService>();

// ======================================
// 🔹 Banco de Dados (EF Core)
// ======================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

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

// ======================================
// 🔐 Authentication
// ======================================
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
        // ✅ CORREÇÃO DEFINITIVA DO ERRO
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishTeacher API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.MapGet("/", () => "EnglishTeacher API rodando 🚀");

app.UseHttpsRedirection();

// 🔐 ORDEM CORRETA
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();