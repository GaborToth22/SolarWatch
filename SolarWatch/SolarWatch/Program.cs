using SolarWatch.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGeoCoder, GeoCoder>();
builder.Services.AddSingleton<ISunDataProvider,SunDataProvider>();
builder.Services.AddSingleton<IJsonProcessorGeo, JsonProcessorGeo>();
builder.Services.AddSingleton<IJsonProcessorSun, JsonProcessorSun>();
    
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