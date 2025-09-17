using WaterAPI.Application.Repositories;
using WaterAPI.Persistence;//sonradan eklendi
using WaterAPI.Persistence.Repositories;
using FluentValidation.AspNetCore;
using WaterAPI.Application.Validators.Products;
using FluentValidation;
using WaterAPI.Infrastructure.Filters;
using WaterAPI.Infrastructure;
using WaterAPI.Infrastructure.Services.Storage.Local;
using WaterAPI.Application.Abstractions.Storage;
using WaterAPI.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Persistance ve Infrastructure katmanlarýndaki ServiceRegistration.cs dosyalarýndaki servisler eklendi

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage(storageType.Local);//Local,Azure
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddControllers()  
//.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
//.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter=true);
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

//builder.Services.AddFluentValidationAutoValidation()
//       .AddValidatorsFromAssemblyContaining<CreateProductValidator>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Admin")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,//Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanacaðýný belirlediðimiz deðerdir.->www.bilmemne.com
            ValidateIssuer = true,  //Oluþturulacak token deðerini kimin daðýttýðýný ifade eden alandýr. www.myapi.com
            ValidateLifetime = true,//Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
            ValidateIssuerSigningKey = true,//Oluþturulacak token key inin bizim uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],   
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };


    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
