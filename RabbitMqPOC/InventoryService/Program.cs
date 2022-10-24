using System.Net;
using InventoryService.Data;
using InventoryService.Grpc;
using InventoryService.Services;
using MessageBus;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

MessageBusDependencies.RegisterMessageBus(builder.Services);
var folder = Environment.SpecialFolder.LocalApplicationData;
     var path = Environment.GetFolderPath(folder);
     var dbPath = System.IO.Path.Join(path, "inventory.db");

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IInventoryRepo, InventoryRepo>();
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<IInventoryService, InventoryService.Services.InventoryService>();
builder.Services.AddScoped<IStockService, StockService>();


builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.MapGrpcService<InventoryGrpcService>();

app.Run();