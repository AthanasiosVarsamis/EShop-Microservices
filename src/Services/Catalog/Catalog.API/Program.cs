var builder = WebApplication.CreateBuilder(args);

//Add services to the container

var app = builder.Build();

//Configure the Htpp requesr pipeline.

app.Run();
