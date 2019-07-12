using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Rest.API.Framework.Models;
using System.Web.Http;

namespace Rest.API.Framework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.EnsureInitialized();
        }
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "RestAPI";
            builder.ContainerName = "DefaultContainer";
            builder.EntitySet<Whiskey>("Whiskeys");
            builder.EntitySet<Assay>("Assays");
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
