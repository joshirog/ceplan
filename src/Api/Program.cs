using Api.Commons.Extensions.Apps;
using Api.Commons.Extensions.Builders;
using Api.Commons.Extensions.Services;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogBuilder();
builder.AddConfigurationBuilder();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddApi();

var app = builder.Build();

app.AddConfigure();

app.Run();