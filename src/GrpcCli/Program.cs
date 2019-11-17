using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcCli
{
  class Program
  {
    static async Task Main(string[] args)
    {
      // The port number(5001) must match the port of the gRPC server.
      var channel = GrpcChannel.ForAddress("https://localhost:20987");
      var client = new Service1.Service1Client(channel);
      var client2 = new Service2.Service2Client(channel);
      for (var i = 0; i < 10; i++)
      {
        try
        {
          //var reply = await client.SendAsync(new Service1Command { Name = "GrpcCli" });
          //Console.WriteLine("cli1: " + reply.Message);
          var reply2 = await client2.SendAsync(new Service2Command { Name = "GrpcCli" });
          Console.WriteLine("cli2: " + reply2.Message);
          await Task.Delay(1000);
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}
