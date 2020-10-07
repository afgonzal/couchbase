using System;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CouchPOC.Data
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            var couchbaseConfig = config.GetSection("Couchbase");
            services.AddCouchbase(couchbaseConfig);
            services.AddCouchbaseBucket<ITravelBucketProvider>(couchbaseConfig["TravelBucket"]);

            

            services.AddTransient<ITravelRepository<Airport>,TravelRepository<Airport>>();
;
            //services.AddDbContext<EconomoContext>(options => options.UseSqlServer(config.GetConnectionString("Economo")), ServiceLifetime.Transient);

            //services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
