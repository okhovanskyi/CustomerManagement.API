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

var builder = WebApplication.CreateBuilder(args);

// Persistence Layer
builder.Services.AddSingleton<AccountCollection>();
builder.Services.AddSingleton<TransactionCollection>();
builder.Services.AddSingleton(new UserCollection(10));

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

public partial class Program { }
