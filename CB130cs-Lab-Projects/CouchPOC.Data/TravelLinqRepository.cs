using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Linq;
using Microsoft.Extensions.Configuration;

namespace CouchPOC.Data
{
    public class TravelLinqRepository<T> : ITravelRepository<T> where T : class
    {
        private readonly ITravelBucketProvider _provider;
        private readonly ICluster _cluster;

        public TravelLinqRepository(ITravelBucketProvider provider, IConfiguration config)
        {
            _provider = provider;
            var couchConfig = config.GetSection("Couchbase");


            Task.Run(async () =>
            {
                var cluster = await Cluster.ConnectAsync(couchConfig["ConnectionString"], couchConfig["Username"],
                    couchConfig["Password"]);

                
            });
            
        }
        public Task<T> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public T Create(string id, T item)
        {
            throw new NotImplementedException();
        }

        public T Update(string id, T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var bucket = await _provider.GetBucketAsync();

            var context = new BucketContext(bucket);

            var query = (from a in context.Query<T>()
                    select a).
                Take(10);
            return query.ToList();
        }
    }
}