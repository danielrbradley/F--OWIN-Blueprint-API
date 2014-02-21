using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinTest
{
    using Microsoft.Owin;
    using Microsoft.Owin.Hosting;

    using Owin;

    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use(
                async (context, next) =>
                    {
                        await next();
                        await context.Response.WriteAsync("Foo bar\r\n");
                    });

            appBuilder.Run(
                context =>
                    {
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync("Hello world\r\n");
                    });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:5000/"))
            {
                Console.Read();
            }
        }
    }
}
