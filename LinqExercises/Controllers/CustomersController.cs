using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            // "Write a query to return all customers in the given city");
            var londonCustomers = from customers in _db.Customers
                                  where customers.City == "London"
                                  select customers;

            return Ok(londonCustomers);
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            // "Write a query to return all customers from Mexico, Sweden and Germany.");
            var mexSwedGerm = from customers in _db.Customers
                              where customers.Country == "Mexico" || customers.Country == "Sweden" || customers.Country == "Germany"
                              select customers;

            return Ok(mexSwedGerm);
        }


        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            /// "Write a query to return all customers with orders that shipped using the given shipperName.");
            var customerList = from shipment in _db.Orders      
                               // group shipment by shipment.Customer into newGroup
                               where shipment.Shipper.CompanyName == shipperName
                               select shipment.Customer;

            Console.WriteLine(customerList.Count());
            return Ok(customerList);
            
        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            // "Write a query to return all customers with no orders in the Orders table.");

            // var resultSet = _db.Customers.Where(c => c.Orders.Count() == 0);

            var customers =
                from c in _db.Customers
                where !c.Orders.Any()
                select c;

            return Ok(customers);
        }

        
    }
}
