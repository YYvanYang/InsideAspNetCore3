using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseUrls("http://0.0.0.0::8080")
                    .ConfigureServices(svcs => svcs.AddCors())
                    .Configure(app => app
                        .UsePathBase("/contacts")
                        .UseCors(cors => cors.WithOrigins(
                           "http://www.foo.com:3721",
                           "http://www.bar.com:3721"))
                        .Run(ProcessAsync)))
                .Build()
                .Run();


            static Task ProcessAsync(HttpContext httpContext)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                var contacts = new Contact[]
                {
                new Contact("张三", "123", "zhangsan@gmail.com"),
                new Contact("李四","456", "lisi@gmail.com"),
                new Contact("王五", "789", "wangwu@gmail.com")
                };
                return response.WriteAsync(JsonConvert.SerializeObject(contacts));
            }
        }
    }
}
