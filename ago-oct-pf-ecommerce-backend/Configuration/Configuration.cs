namespace ago_oct_pf_ecommerce_backend.Configuration
{
    public static class Configuration
    {
        public static void ConfigureConnection(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DbSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("DbSettings:DatabaseName");

            //DB context
            /*services.AddDbContext<SinewaveDbContext>(options =>
            options.UseSqlServer(connectionString));*/





        }
    }
}
