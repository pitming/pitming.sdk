//using Microsoft.AspNetCore.Mvc;

using cqrsLib.Event;

namespace ConsoleApp1
{
  public class EntityEvents
  {
    public EntityCreated EntityCreated;
    public EntityCreationFailed EntityCreationFailed;
    public EntityUpdated EntityUpdated;
    public EntityUpdateFailed EntityUpdateFailed;
  }
}
