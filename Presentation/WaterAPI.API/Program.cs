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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using Serilog.Events;
using System.Security.Claims;
using Serilog.Context;
using WaterAPI.API.Configurations.ColumnWriter;
using Microsoft.AspNetCore.HttpLogging;
using WaterAPI.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Client dan gelen request neticesinde oluþturulan httpContext nesnesine katmalardaki classlar üzerinden(buisness logic)
//eriþebilmemizi saðlayan bir servistir --> builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
//Persistance ve Infrastructure katmanlarýndaki ServiceRegistration.cs dosyalarýndaki servisler eklendi

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddStorage(storageType.Local);//Local,Azure Storage Eklendi
builder.Services.AddStorage<LocalStorage>();


//Loglama mekanizmasý Eklendi
Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
    needAutoCreateTable: true   ,
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message", new RenderedMessageColumnWriter()  },
        {"message_template", new MessageTemplateColumnWriter()  },
        { "level", new LevelColumnWriter() },
        {"time_stamp", new TimestampColumnWriter() },
        {"exception", new ExceptionColumnWriter() },
        {"log_event",new LogEventSerializedColumnWriter() },
        {"user_name", new UserNameColumnWriter()  }
         })
    .Enrich.FromLogContext()
    .MinimumLevel.Information()    //Sadece bilgi içerikli loglarý tutar
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,//Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanacaðýný belirlediðimiz deðerdir.->www.bilmemne.com
            ValidateIssuer = true,  //Oluþturulacak token deðerini kimin daðýttýðýný ifade eden alandýr. www.myapi.com
            ValidateLifetime = true,//Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
            ValidateIssuerSigningKey = true,//Oluþturulacak token key inin bizim uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires !=null ? expires>DateTime.UtcNow : false ,
            NameClaimType = ClaimTypes.Name,//JWT üzerinde Name claim ine karþýlýk gelen deðeri User.Identity.Name property sinden elde edebiliriz
        };


    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();

app.UseSerilogRequestLogging();//çaðýrýldýktan sonraki middleware ler loglanýr.
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context,next) =>
{
    
   var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name",username);
    await next();
});

app.MapControllers();


app.Run();
