using System;
//using Microsoft.AspNetCore.Mvc;

namespace cqrsLib.Event
{
  public class EntityCreated
  {
    public Guid EntitiKey { get; set; }
  }
}
