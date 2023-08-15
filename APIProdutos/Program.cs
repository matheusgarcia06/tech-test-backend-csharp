using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TechTestBackendCSharp.Data;
using TechTestBackendCSharp.Models;
using TechTestBackendCSharp.Services.ProdutoService;
using TechTestBackendCSharp.Services;
using MongoDB.Driver;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext do SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do repositório SQL
builder.Services.AddScoped<IProdutoService, ProdutoSqlServerService>();

// Configuração do repositório Arquivo Texto
//var filePath = builder.Configuration.GetValue<string>("FileRepository:FilePath");
//builder.Services.AddScoped<IProdutoService>(provider => new ProdutoFileService(filePath));

// Configuração do repositório MongoDB
//var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDB:ConnectionString");
//var mongoClient = new MongoClient(mongoConnectionString);

//builder.Services.AddScoped<IProdutoService, ProdutoSqlMongoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
