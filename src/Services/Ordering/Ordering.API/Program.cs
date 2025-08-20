using BuildingBlocks.Behaviors;
using FluentValidation.AspNetCore;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for di

//infrastructure - EF Core
//Application - MediatR
//Api - Carter, HealthChecks,,,

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

//configure the http request pipeline

app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}


app.Run();
