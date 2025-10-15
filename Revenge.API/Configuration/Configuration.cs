using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Data.Repositories;
using Revenge.Infrestructure.Repositories;

namespace ago_oct_pf_ecommerce_backend.Configuration
{
    public static class Configuration
    {
        public static void ConfigureConnection(this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionString = configuration.GetValue<string>("DbConnection");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<RevengeDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingcartRepository, ShoppingcartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();

        }
    }
}
