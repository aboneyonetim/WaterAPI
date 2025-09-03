using WaterAPI.Application.Repositories;
using WaterAPI.Persistence;//sonradan eklendi
using WaterAPI.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Iservice collection eklemek gerekebilir. ServiceRegistration.cs dosyasýndaki constuctorý kullanmak için

builder.Services.AddPersistenceServices();//sonradan eklendi
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

app.Run();
