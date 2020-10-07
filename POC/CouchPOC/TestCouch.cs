using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Extensions.DependencyInjection;
using CouchPOC.Data;

namespace CouchPOC
{
    public class TestCouch 
    {
        private readonly ITravelRepository<Airport> _repository;
        private readonly ICouchbaseLifetimeService _couchbase;
        public TestCouch(ITravelRepository<Airport> repository, ICouchbaseLifetimeService couchbase)
        {
            _repository = repository;
            _couchbase = couchbase;
        }

        public async Task Run()
        {
                var orlandoAirport = await _repository.GetAsync("airport_3878");
                var londonAirport =  await _repository.GetAsync("airport_507");
                Console.WriteLine("{0} - {1}", orlandoAirport.FAA, londonAirport.FAA);
                var all = await _repository.GetAllAsync();
        }


        //public void Dispose()
        //{
        //    //_couchbase.Close();
        //}
    }
}
