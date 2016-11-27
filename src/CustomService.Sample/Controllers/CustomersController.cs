using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using System.Web.OData.Routing;

using CustomService.Model;

using Sitecore.Services.Core.Data;
using Sitecore.Services.Infrastructure.OData;

namespace CustomService.Controllers
{
    public class CustomersController 
        : ServiceBaseODataController<Customer>
    {
        public CustomersController(IReadOnlyEntityRepository<Customer> repository) 
            : base(repository)
        {
        }

        #region Your stuff.. including Functions and Actions

        [HttpGet]
        [ODataRoute("TopBuyer()")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult TopBuyer() 
        {
            var customers =
                    Repository
                        .GetData()
                        .ContinueWith<IOrderedQueryable<Customer>>(Customer.GetTopBuyer)
                        .Result
                        .ToArray();

            if (!customers.Any()) 
            {
                return NotFound();
            }

            return Ok(customers.First());
        }

        #endregion
    }
}