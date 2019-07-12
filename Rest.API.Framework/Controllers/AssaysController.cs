using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Rest.API.Framework.DataSource;
using System.Linq;
using System.Web.Http;

namespace Rest.API.Framework.Controllers
{
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
    public class AssaysController : ODataController
    {
        public IHttpActionResult Get()
        {
            return Ok(RestAPIDataSource.Instance.Assays.AsQueryable());
        }
    }
}