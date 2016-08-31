using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class SuppliersController : ApiController
    {
        private NORTHWNDEntities _db;

        public SuppliersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/suppliers/salesAndMarketing
        [HttpGet, Route("api/suppliers/salesAndMarketing"), ResponseType(typeof(IQueryable<Supplier>))]
        public IHttpActionResult GetSalesAndMarketing()
        {
            // "Write a query to return all Suppliers that are marketing managers or 
            // sales representatives that have a fax number");
            var supplierList = from s in _db.Suppliers
                               where s.Fax != null && (s.ContactTitle == "Marketing Manager" || s.ContactTitle == "Sales Representative")
                               select s;

            return Ok(supplierList);
        }

        //GET: api/suppliers/search
        [HttpGet, Route("api/suppliers/search"), ResponseType(typeof(IQueryable<Supplier>))]
        public IHttpActionResult SearchSuppliers(string term)
        {
            // "Write a query to return all Suppliers containing the 'term' variable in their address. 
            // The list should ordered alphabetically by company name.");
            var list = from a in _db.Suppliers
                       orderby a.CompanyName
                       where a.Address.Contains(term)
                       select a;

            return Ok(list);



        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
