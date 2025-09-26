using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.Abstractions.Storage;
using WaterAPI.Application.Abstractions.Storage.Local;
using WaterAPI.Application.Abstractions.Token;
using WaterAPI.Infrastructure.Enums;
using WaterAPI.Infrastructure.Services;
using WaterAPI.Infrastructure.Services.Storage;
using WaterAPI.Infrastructure.Services.Storage.Local;
using WaterAPI.Infrastructure.Services.Token;

namespace WaterAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
            {

            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IPaymentService,IyzicoPaymentService>();//Iyzico ödeme servisi
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage,T>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType )
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:

                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }

        }
    }
}
