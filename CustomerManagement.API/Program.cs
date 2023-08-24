using CustomerManagement.API.Command;
using CustomerManagement.API.Command.Commands;
using CustomerManagement.API.Command.Handlers;
using CustomerManagement.API.Command.Interfaces;
using CustomerManagement.API.Persistence;
using CustomerManagement.API.Query.Handlers;
using CustomerManagement.API.Query.Interfaces;
using CustomerManagement.API.Query.Queries;
using CustomerManagement.API.Query.QueryResults;
using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Repositories;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Services;
using CustomerManagement.API.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Persistence Layer
builder.Services.AddSingleton<AccountCollection>();
builder.Services.AddSingleton<TransactionCollection>();
builder.Services.AddSingleton<UserCollection>();

// Repository Layer
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Service Layer
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUserService, UserService>();

// Commands
builder.Services.AddScoped<ICommandHandler<OpenNewAccountForUserCommand, CommandResult>, UsersFinancialDataCommadHandler>();

// Queries
builder.Services.AddScoped<IQueryHandler<GetUserFinancialDataQuery, GetUserFinancialDataQueryResult>, UserFinancialDataQueryHandler>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.ValidateIssuer = false;
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;
        options.TokenValidationParameters.IssuerSigningKey = JwtTokenUtility.KeyPair;        
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Adds Authorization header using the Bearer scheme.",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
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
                    },
                    Scheme = "oauth2",
                    In = ParameterLocation.Header
                },
                new string[] { }
            }
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
