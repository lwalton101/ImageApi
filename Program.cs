using ImageApi;
using ImageApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
FileManager.WwwrootPath = builder.Environment.WebRootPath;
FileManager.InitFileManager();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();