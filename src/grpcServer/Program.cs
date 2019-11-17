using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace grpcLib
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
           .UseStartup<Startup>()
           .ConfigureKestrel(options =>
             options.Listen(IPAddress.Any, 20987, listenOptions =>
              {
                listenOptions.Protocols = HttpProtocols.Http2;
                listenOptions.UseHttps();
              })
           );
    }

  }
}
