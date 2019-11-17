using Google.Protobuf;
using Grpc.Core;
using System.Threading.Tasks;
using static Service1;

namespace grpcLib
{
  public class GenericGrpcService : Service1Base
  {
    public override Task<Service1Response> Send(Service1Command request, ServerCallContext context)
    {
      return Task.FromResult(new Service1Response { Message = "Answer from service 1" });
    }
  }

  public class GrpcService : IGenericSendHandler<Service2Command, Service2Response>
  {
    public Task<Service2Response> Send(Service2Command command, ServerCallContext context)
    {
      return Task.FromResult(new Service2Response { Message = "Answer from service 2" });
    }
  }

  public interface IGenericSendHandler<TCommand, TResponse>
    where TCommand : class, IMessage<TCommand>, new()
    where TResponse : class, IMessage<TResponse>, new()
  {
    Task<TResponse> Send(TCommand command, ServerCallContext context);
  }
}
