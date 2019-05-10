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
    public int EntityId { get; private set; }

    public AutoResetEvent HandleCommandResetEvent { get; private set; } = new AutoResetEvent(false);

    public CommandManager(IEnumerable<ICommandHandler> commandHandlers)
    {
      _commandHandlers = commandHandlers;
    }
    public void HandleCommand(ICommand command)
    {
      EntityId = command.CommandHeader.EntityId;
      HandleCommandTask = Task.Factory.StartNew(() =>
      {
        try
        {
          foreach (var handler in _commandHandlers.Where(h => h.Selector(command) != null))
          {
            System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Handle command:{handler.GetType()} - {command.CommandHeader.EntityId}");
            handler.Do(command);
          }
        }
        finally
        {
          HandleCommandResetEvent.Set();
        }
      });
    }

    public void Dispose()
    {
      HandleCommandResetEvent?.Dispose();
    }
  }
}
