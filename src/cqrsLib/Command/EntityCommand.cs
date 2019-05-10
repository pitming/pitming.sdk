//using Microsoft.AspNetCore.Mvc;

namespace cqrsLib.Command
{
  public class EntityCommand : ICommand
  {
    public CommandHeader CommandHeader { get; set; }
    public CreateEntity CreateEntity { get; set; }
    public UpdateEntity UpdateEntity { get; set; }
  }
}
