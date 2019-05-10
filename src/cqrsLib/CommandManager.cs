using cqrsLib.Command;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace cqrsLib
{
  public class CommandManager
  {
    private readonly IEnumerable<ICommandHandler> _commandHandlers;

    public CommandManager(IEnumerable<ICommandHandler> commandHandlers)
    {
      _commandHandlers = commandHandlers;
    }
    public void HandleCommand(ICommand command)
    {
      HandleCommandTask = Task.Factory.StartNew(() =>
      {
        foreach (var handler in _commandHandlers.Where(h => h.Selector(command) != null))
        {
          System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Handle command:{handler.GetType()} - {command.CommandHeader.EntityId}");
          handler.Do(command);
        }
      });
    }

    public Task HandleCommandTask { get; private set; }
  }
}
