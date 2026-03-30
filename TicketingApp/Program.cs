using Application.Features.AuthFeatures;
using Application.Features.CategoryFeatures;
using Application.Features.UserFeatures;
using Application.Features.VenueFeatures;
using Application.Features.EventFeatures;
using Application.Interfaces;
using Application.Interfaces.IVenueRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance;
using Persistance.Repositories;
using Persistance.Repositories.VenueRepositories;
using System.Text;
using Application.Interfaces.IEventRespository;
using Persistance.Repositories.EventRepositories;

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
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentification:SecretKeyFor"]!))
};
});
builder.Services.AddScoped<UserAuthFeatures>();
builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
builder.Services.AddScoped<UserFeatures>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CategoryFeatures>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositories>();
builder.Services.AddScoped<VenueFeatures>();
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<EventFeatures>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
