using System;

namespace cqrsLib.Command
{
  public interface ICommandHandler
  {
    Func<ICommand, object> Selector { get; }
    Action<ICommand> Do { get; }
  }
}
