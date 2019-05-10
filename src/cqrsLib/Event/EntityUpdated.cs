using System;
//using Microsoft.AspNetCore.Mvc;

namespace cqrsLib.Event
{
  public class EntityUpdated
  {
    public Guid EntitiKey { get; set; }
    public int valueToAdd { get; set; }
  }
}
