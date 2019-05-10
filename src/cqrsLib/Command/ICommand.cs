using cqrsLib.Command;

namespace cqrsLib.Command
{
  public interface ICommand
  {
    CommandHeader CommandHeader { get; }
  }
}