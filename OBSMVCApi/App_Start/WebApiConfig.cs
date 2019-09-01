using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using System.Web.Http.Dependencies;
using OBSMVCApi.DAL;
using Unity.Lifetime;
using OBSMVCApi.Models;
using OBSMVCApi.DependencyResolver;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace OBSMVCApi
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {

            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");  //origins,headers,methods   
            //config.EnableCors(cors);
            // hello world
            // some changes
            var container = new UnityContainer();
            container.RegisterType<IRepository<Author>, AuthorRepository>();
            container.RegisterType<IRepository<Category>, CategoryRepository>();
            container.RegisterType<IRepository<Publisher>, PublisherRepository>();
            container.RegisterType<IRepository<Book>, BookRepository>();
            container.RegisterType<IRepository<Stock>, StockRepository>();
            container.RegisterType<IRepository<WishList>, WishlistRepository>();
            container.RegisterType<IRepository<Feedback>, FeedbackRepository>();
            container.RegisterType<IRepository<BookReview>,BookReviewRepository>();
            container.RegisterType<IRepository<Purchase>, PurchaseRepository>();
            container.RegisterType<IRepository<PurchaseLine>, PurchaseLineRepository>();
            container.RegisterType<IRepository<Order>, OrderRepository>();
            container.RegisterType<IRepository<OrderLine>, OrderlineRepository>();
            container.RegisterType<IRepository<Cart>, CartRepository>();


            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
