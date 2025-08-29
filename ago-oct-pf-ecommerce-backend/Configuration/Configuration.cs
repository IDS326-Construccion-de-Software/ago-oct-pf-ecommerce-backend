namespace ago_oct_pf_ecommerce_backend.Configuration
{
    public static class Configuration
    {
        public static void ConfigureConnection(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DbSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("DbSettings:DatabaseName");

            try
            {
                services.AddDbContext<SinewaveDbContext>(options =>
                options.UseSqlServer(connectionString));

                services.AddScoped<IClosingRepository, ClosingRepository>();
                services.AddScoped<IConfigurationRepository, ConfigurationRepository>();


            }
            catch (Exception)
            {
                /* MONGODB SETTINGS HERE
                try
                {
                    MongoClientConnection clientConnection = new MongoClientConnection(connectionString, databaseName);
                    services.AddSingleton(clientConnection);

                    services.AddScoped<IApplicationRepository, Mongo.Repositories.ApplicationRepository>();
                    services.AddScoped<IActionRepository, Mongo.Repositories.ActionRepository>();
                    services.AddScoped<IRealmRepository, Mongo.Repositories.RealmRepository>();
                    services.AddScoped<IRealmUserRepository, Mongo.Repositories.RealmUserRepository>();
                    services.AddScoped<ISubRealmRepository, Mongo.Repositories.SubRealmRepository>();
                }
                catch (Exception)
                {
                    throw;
                } */
            }


        }
    }
}
