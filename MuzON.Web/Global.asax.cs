using AutoMapper;
using MuzON.BLL.MappingProfiles;
using MuzON.Web.MappingProfiles;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MuzON.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
        }
    }

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AllowNullCollections = true;
                x.AddProfile<BLLMappingProfile>();
                x.AddProfile<WebMappingProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
