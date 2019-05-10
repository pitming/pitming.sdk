using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace cqrsLib
{
  public class Bus<TMessage>
  {
    private BlockingCollection<TMessage> _bus = new BlockingCollection<TMessage>();

    public void SendMessage(TMessage message)
    {
      _bus.Add(message);
    }

    public TMessage GetNextMessage()
    {
      try
      {
        return _bus.Take();
      }
      catch { }
      return default;
    }

    public void Close()
    {
      _bus.CompleteAdding();
    }

    public void Initialize()
    {
      _bus?.Dispose();
      _bus = new BlockingCollection<TMessage>();
    }

    public bool IsClosed => _bus.IsCompleted;

    public bool Empty => _bus.Count == 0;
  }
}
