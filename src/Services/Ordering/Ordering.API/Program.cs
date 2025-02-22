 var builder = WebApplication.CreateBuilder(args);

// Add services to the container for di

var app = builder.Build();


//configure the http request pipeline

app.Run();
