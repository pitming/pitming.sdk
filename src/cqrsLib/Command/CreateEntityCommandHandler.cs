using System;
using System.Threading;
using System.Threading.Tasks;

namespace cqrsLib.Command
{
  public class CreateEntityCommandHandler : ICommandHandler
  {
    private readonly EntityRepository _entityRepository;

    public CreateEntityCommandHandler(EntityRepository entityRepository)
    {
      _entityRepository = entityRepository;
    }
    public Func<ICommand, object> Selector => c =>
    {
      return c is EntityCommand command ? command.CreateEntity : null;
    };
    public Action<ICommand> Do =>
      c =>
    {
      var createEntity = Selector(c) as CreateEntity;
      Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Handle command:{this.GetType()} - {createEntity.Id}");
      _entityRepository.Upsert(new Entity { Id = createEntity.Id });
      Task.Delay(TimeSpan.FromMilliseconds(10)).Wait();
    };
  }
}
