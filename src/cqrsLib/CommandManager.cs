using cqrsLib.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace cqrsLib
{
  public class CommandManager : IDisposable
  {
    private readonly IEnumerable<ICommandHandler> _commandHandlers;

    public Task HandleCommandTask { get; private set; }
    public int? EntityId { get; private set; } //be sure entityId are nullable to avoid special init variable (like 0 for int)

    public ManualResetEvent HandleCommandResetEvent { get; private set; } = new ManualResetEvent(false);

    public CommandManager(IEnumerable<ICommandHandler> commandHandlers)
    {
      _commandHandlers = commandHandlers;
    }
    public void HandleCommand(ICommand command)
    {
      Console.WriteLine($"Locking for id:{command.CommandHeader.EntityId}");
      HandleCommandResetEvent.Reset();
      Console.WriteLine($"Locked for id:{command.CommandHeader.EntityId}");
      EntityId = command.CommandHeader.EntityId;
      HandleCommandTask = Task.Factory.StartNew(() =>
      {
        try
        {
          foreach (var handler in _commandHandlers.Where(h => h.Selector(command) != null))
          {
            handler.Do(command);
          }
        }
        finally
        {
          Console.WriteLine($"Releasing for id:{command.CommandHeader.EntityId}");
          HandleCommandResetEvent.Set();
          Console.WriteLine($"Released for id:{command.CommandHeader.EntityId}");
        }
      });
    }

    public void Dispose()
    {
      HandleCommandResetEvent?.Dispose();
    }
  }
}
