using AutoMapper;
using Core.Repositories;
using Data.Infrastructure;
using Data.Mappings;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Data
{
    public static class DataAdapter
    {
        public static void RegisterPersistence(this IServiceCollection serviceCollection, Container container, string connectionString)
        {
            serviceCollection.AddDbContext<OrderManagerContext>(options =>
                options.UseSqlServer(connectionString));

            container.Register<IOrderRepository, OrderRepository>();
        }
    }
}