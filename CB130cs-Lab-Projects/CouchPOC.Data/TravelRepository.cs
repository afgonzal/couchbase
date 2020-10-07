using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Microsoft.Extensions.Configuration;

namespace CouchPOC.Data
{
    public interface ITravelBucketProvider : INamedBucketProvider
    {

    }

    public interface ITravelRepository<T> : IRepository<T> where T : class
    {

    }
    public class TravelRepository<T> : ITravelRepository<T> where T: class
    {
        private readonly ITravelBucketProvider _provider;
        private readonly IConfigurationSection _couchConfig;
        public TravelRepository(ITravelBucketProvider provider, IConfiguration config)
        {
            _couchConfig = config.GetSection("Couchbase");
            _provider = provider;
        }

       

        public async Task<T> GetAsync(string id)
        {
            var bucket = await _provider.GetBucketAsync();
                var collection = bucket.DefaultCollection();
                var result = await collection.GetAsync(id);
                var content = result.ContentAs<T>();
                return content;
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
            var collection = bucket.DefaultCollection();

            

            var cluster = await Cluster.ConnectAsync(_couchConfig["ConnectionString"], _couchConfig["Username"],
                _couchConfig["Password"]);


            var query = $"SELECT data.* from `{_couchConfig["TravelBucket"]}` data where type = \"{typeof(T).Name.ToLower()}\" ";
            var result = await cluster.QueryAsync<T>(query);

            var rows = await result.Rows.ToListAsync();

            //IQueryResult<T> result = bucket.Query<T>(query);

            //var result = await collection.geta
            //var content = result.ContentAs<T>();
            //return content;

            //var cluster = new Cluster(new ClientConfiguration());

            //var j = _provider.BucketName;


            return rows;
        }

       
    }
}
