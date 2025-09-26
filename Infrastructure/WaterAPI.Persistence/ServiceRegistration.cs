using WaterAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WaterAPI.Persistence.Repositories;
using WaterAPI.Application.Repositories;
using WaterAPI.Persistence.Repositories.File;
using WaterAPI.Domain.Entities.Identity;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Persistence.Services;
using WaterAPI.Application.Abstractions.Services.Authentication;
using Microsoft.EntityFrameworkCore.Internal;

namespace WaterAPI.Persistence
{
   public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<WaterAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<WaterAPIDbContext>();

            services.AddScoped < ICustomerReadRepository, CustomerReadRepository > ();
            services.AddScoped < ICustomerWriteRepository, CustomerWriteRepository > ();
            services.AddScoped < IOrderReadRepository, OrderReadRepository > ();
            services.AddScoped < IOrderWriteRepository, OrderWriteRepository > ();
            services.AddScoped < IProductReadRepository, ProductReadRepository > ();
            services.AddScoped < IProductWriteRepository, ProductWriteRepository > ();
            services.AddScoped < IFileReadRepository, FileReadRepository>();
            services.AddScoped < IFileWriteRepository, FileWriteRepository>();
            services.AddScoped < IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped < IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped < IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped < IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped < ICardRegisterReadRepository,CardRegisterReadRepository>();
            services.AddScoped < ICardRegisterWriteRepository,CardRegisterWriteRepository>();

            services.AddScoped < ICardPayloadReadRepository,  CardPayloadReadRepository>();
            services.AddScoped < ICardPayloadWriteRepository, CardPayloadWriteRepository>();



            services.AddScoped < IUserService, UserService>();
            services.AddScoped < IAuthService, AuthService>();
            services.AddScoped < ICardRegisterService, CardRegisterService>();
            services.AddScoped < IExternalAuthentication, AuthService>();
            services.AddScoped < IInternalAuthentication, AuthService>();


        }
    }
}
