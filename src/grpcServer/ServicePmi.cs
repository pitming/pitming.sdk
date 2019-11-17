using Google.Protobuf;
using grpc = global::Grpc.Core;

namespace grpcLib
{

  public static partial class ServicePmi<TCommand, TResponse>
    where TCommand : class, IMessage<TCommand>, new()
    where TResponse : class, IMessage<TResponse>, new()
  {
    static readonly string __ServiceName = "ServicePmi";

    static readonly grpc::Marshaller<TCommand> __Marshaller_Service1Command = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg),
      x => new MessageParser<TCommand>(() => new TCommand()).ParseFrom(x));
    static readonly grpc::Marshaller<TResponse> __Marshaller_Service1Response = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg),
       x => new MessageParser<TResponse>(() => new TResponse()).ParseFrom(x));

    static readonly grpc::Method<TCommand, TResponse> __Method_Send = new grpc::Method<TCommand, TResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Send",
        __Marshaller_Service1Command,
        __Marshaller_Service1Response);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GreetReflection.Descriptor.Services[0]; }
    }

    ///// <summary>Base class for server-side implementations of Service1</summary>
    //[grpc::BindServiceMethod(typeof(ServicePmi), "BindService")]
    //public abstract partial class ServicePmiBase
    //{
    //  public virtual global::System.Threading.Tasks.Task<TResponse> Send(TCommand request, grpc::ServerCallContext context)
    //  {
    //    throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    //  }

    //}

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(IGenericSendHandler<TCommand, TResponse> serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Send, serviceImpl.Send).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, IGenericSendHandler<TCommand, TResponse> serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Send, serviceImpl == null ? null : new grpc::UnaryServerMethod<TCommand, TResponse>(serviceImpl.Send));
    }

  }
}
