using System;
//using Microsoft.AspNetCore.Mvc;

namespace cqrsLib.Event
{
  public interface IEventHandler<TEvent>
  {
    Func<TEvent, object> Selector { get; set; }
    Func<TEvent, string> Do { get; set; }
  }
}
