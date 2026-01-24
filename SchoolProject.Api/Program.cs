using Microsoft.EntityFrameworkCore;
using SchoolProject.Core;
using SchoolProject.infrustructure;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.Repositories;
using SchoolProject.Service;

var builder = WebApplication.CreateBuilder(args);
//Add service Containers

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//Connection To SQl Server
builder.Services.AddDbContext<AppBDContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Dependiency Injections
builder.Services.AddInfrustructureDependencies()
    .AddServiceDependencies()
    .AddCoreDependencies();



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
