using System;
//using Microsoft.AspNetCore.Mvc;

namespace cqrsLib.Command
{
  public class UpdateEntity
  {
    public int Id { get; set; }
    public int Child { get; set; }
  }
}
