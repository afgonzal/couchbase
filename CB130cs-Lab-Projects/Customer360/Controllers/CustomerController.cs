using Customer360.Data;
using Customer360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Customer360.Controllers
{
    public class CustomerController : ApiController
    {

        private readonly IRepository<Customer> _customerRepository = new CouchbaseRepository<Customer>();

        // GET: api/Customer
        public HttpResponseMessage Get()
        {
            IEnumerable<Customer> customers = _customerRepository.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        // GET: api/Customer/city/{city}
        [HttpGet]
        [Route("api/Customer/city/{city}")]
        public HttpResponseMessage GetByCity(string city)
        {
            IEnumerable<Customer> customers = _customerRepository.GetByCity(city);
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        // GET: api/Customer/{username}
        public HttpResponseMessage Get(string id)
        {
            Customer customer = _customerRepository.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        // POST: api/Customer/{username}
        public HttpResponseMessage Post([FromBody] Customer customer, string id)
        {
            Customer result = _customerRepository.Create(id, customer);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, customer);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Customer/{customer-id}
        public HttpResponseMessage Put([FromBody] Customer customer, string id)
        {
            Customer result = _customerRepository.Update(id, customer);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, customer);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Customer/{customer-id}
        public HttpResponseMessage Delete(string id)
        {
            _customerRepository.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
