using Customer360.Models;
using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;

namespace Customer360.Data
{
    public class CouchbaseRepository<T> : IRepository<T>
        where T : EntityBase<T>
    {
        private readonly IBucket _bucket = ClusterHelper.GetBucket("Customer360");

        private string CreateKey(string id)
        {
            return string.Format("{0}::{1}", typeof(T).Name.ToLower(), id);
        }

        public T Get(string id)
        {
            var key = CreateKey(id);
            IOperationResult<T> result = _bucket.Get<T>(key);
            if (!result.Success)
            {
                // deal with error
                return null;
            }

            return result.Value;
        }

        public T Create(string id, T item)
        {
            item.created = DateTime.Now;
            item.updated = DateTime.Now;
            // item.id = id;
            item.id = AppendKeyIncrement(id);

            var key = CreateKey(item.id);

            IOperationResult<T> result = _bucket.Insert(key, item);
            if (!result.Success)
            {
                throw result.Exception;
            }
            return result.Value;
        }

        private string AppendKeyIncrement(string key)
        {
            const string counterKey = "customer::counter";
            IOperationResult<ulong> result = _bucket.Increment(counterKey, 1, 1);

            return string.Format(key + "-{0}", result.Value);
        }

        public T Update(string id, T item)
        {
            if (item.id == null)
            {
                item.created = DateTime.Now;
                item.id = id;
            }
            item.updated = DateTime.Now;

            var key = CreateKey(id);

            IOperationResult<T> result = _bucket.Replace(key, item);
            
            if (!result.Success)
            {
                throw result.Exception;
            }

            return result.Value;
        }

        public void Delete(string id)
        {
            var key = CreateKey(id);

            IOperationResult result = _bucket.Remove(key);
            if (!result.Success)
            {
                throw result.Exception;
            }

        }

        public IEnumerable<T> GetAll()
        {
            var query = new QueryRequest(
                "SELECT Customer360.* " + 
                "FROM Customer360 " + 
                "WHERE type = $1 " +
                "LIMIT 10;")
                .AddPositionalParameter(typeof(T).Name.ToLower());

            IQueryResult<T> result = _bucket.Query<T>(query);
            if (!result.Success)
            {
                throw result.Exception;
            }

            return result.Rows;
        }

        public IEnumerable<T> GetByCity(string city)
        {

            IQueryRequest query = new QueryRequest("SELECT Customer360.* FROM Customer360 WHERE type = $1 AND billingAddress.city = $2;")
                .AddPositionalParameter(typeof(T).Name.ToLower())
                .AddPositionalParameter(city);

            IQueryResult<T> result = _bucket.Query<T>(query);
            if (!result.Success)
            {
                throw result.Exception;
            }

            return result.Rows;

        }

    }
}