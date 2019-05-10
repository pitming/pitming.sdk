using cqrsLib.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace cqrsLib
{
  public class CommandManagerCoordinator : IDisposable
  {
    private readonly Bus<ICommand> _commandBus;
    private readonly List<CommandManager> _commandManagers = new List<CommandManager>();
    private readonly AutoResetEvent _commandBusEmptyResetEvent = new AutoResetEvent(false);
    private const int NB_WORKER = 1;

    public CommandManagerCoordinator(Bus<ICommand> commandBus, IEnumerable<ICommandHandler> commandHandlers)
    {
      _commandBus = commandBus;

      for (var i = 0; i < NB_WORKER; i++)
      {
        _commandManagers.Add(new CommandManager(commandHandlers));
      }
    }

    public void Start()
    {
      Task.Factory.StartNew(() =>
      {
        try
        {
          while (!_commandBus.IsClosed)
          {
            foreach (var commandManager in _commandManagers)
            {
              try
              {
                var command = _commandBus.GetNextMessage();
                if (command != null)
                {
                  var concurrentCommandManager = _commandManagers.FirstOrDefault(cm => cm.EntityId == command.CommandHeader.EntityId);
                  if (concurrentCommandManager != null)
                  {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Id {command.CommandHeader.EntityId} in use. Waiting");
                    concurrentCommandManager.HandleCommandResetEvent?.WaitOne();
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Id {command.CommandHeader.EntityId} no more in use. Resume");
                  }
                  commandManager.HandleCommand(command);
                }
              }
              catch { }
            }

            Task.WaitAll(_commandManagers.Select(cm => cm.HandleCommandTask).Where(t => t != null).ToArray());
          }
        }
        catch { }

        _commandBusEmptyResetEvent.Set();
      });
    }

    public void Stop()
    {
      _commandBus.Close();
      _commandBusEmptyResetEvent.WaitOne();
    }

    public void Dispose()
    {
      foreach (var cm in _commandManagers)
        cm?.Dispose();

      _commandBusEmptyResetEvent?.Dispose();
    }
  }
}
