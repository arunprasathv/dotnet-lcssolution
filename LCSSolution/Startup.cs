using System.Web.Http;
using Owin;
using Swashbuckle.Application;
using Swashbuckle.SwaggerUi;
using Swashbuckle.Swagger;
using System.IO;

namespace LCSExercise
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            // Adding to the pipeline with our own middleware
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "LCS Self Host";

                // Call next middleware
                await next.Invoke();
            });
            
            // Custom Middleare
            app.Use(typeof(CustomMiddleware));
          
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Solution To Find Longest Common Substring");
                c.IncludeXmlComments(GetXmlCommentsPath());
            })
            .EnableSwaggerUi();
            

            config.Routes.MapHttpRoute( 
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // Web Api
            app.UseWebApi(config);
        }
        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\LCSSolution.XML", System.AppDomain.CurrentDomain.BaseDirectory);
            //return Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "bin", "LCSSolution.XML");
        }

    }
}
