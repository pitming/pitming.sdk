using System;
using System.Collections.Generic;

namespace cqrsLib
{
  public class Entity
  {
    public int Id { get; set; }
    public List<int> Children { get; } = new List<int>();
  }
}
