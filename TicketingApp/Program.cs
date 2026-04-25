using Application.Features.AuthFeatures;
using Application.Features.CategoryFeatures;
using Application.Features.EventFeatures;
using Application.Features.TicketFeatures;
using Application.Features.UserFeatures;
using Application.Features.VenueFeatures;
using Application.Interfaces;
using Application.Interfaces.IEventRespository;
using Application.Interfaces.ITicketRepository;
using Application.Interfaces.IVenueRepository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance;
using Persistance.Repositories;
using Persistance.Repositories.EventRepositories;
using Persistance.Repositories.TicketRepository;
using Persistance.Repositories.VenueRepositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this api"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            }, new List<string>()
        }
    });
});

builder.Services.AddDbContext<TicketingAppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentification:Issuer"],
        ValidAudience = builder.Configuration["Authentification:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentification:SecretKeyFor"]!)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendUI", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Your Angular/React URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<UserAuthFeatures>();
builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
builder.Services.AddScoped<UserFeatures>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<CategoryFeatures>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositories>();
builder.Services.AddScoped<VenueFeatures>();
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<EventFeatures>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<TicketFeatures>();
builder.Services.AddScoped<ITicketRepository, TicketRepostiroy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendUI");

app.UseAuthorization();

app.MapControllers();

app.Run();
