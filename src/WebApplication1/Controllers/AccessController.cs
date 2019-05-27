using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
  public class AccessController : Controller
  {
    private readonly IStorageRepository _storageRepository;

    public AccessController(IStorageRepository storageRepository)
    {
      _storageRepository = storageRepository;
    }
    public async Task<IActionResult> Get(string id)
    {
      var storage = await _storageRepository.GetAccessAsync(id);
      if (storage == null)
        return NotFound();
      return Ok(storage);
    }
  }

  public static class TypeDictionnary
  {
    private static readonly Dictionary<string, Tuple<string, string>[]> _dic;

    static TypeDictionnary()
    {
      _dic = new Dictionary<string, Tuple<string, string>[]>
      {
        { "Nearline", new[] { Tuple.Create("Samba",(string)null) } },
        { "Avid", new[] { Tuple.Create("Nexis", "Avid") } }
      };
    }

    public static string[] GetDestinationTypes()
    {
      return _dic.Keys.ToArray();
    }

    public static string[] GetStorageType(string destinationType)
    {
      var tuple = _dic.GetValueOrDefault(destinationType);
      if (tuple == null)
        return new string[0];
      return tuple.Select(t => t.Item1).ToArray();
    }
  }

  public class DestinationTypeController : Controller
  {
    public IActionResult Get()
    {

      return base.Ok(TypeDictionnary.GetDestinationTypes());
    }
  }

  public class AccessTypeController : Controller
  {
    public IActionResult Get(string destinationType)
    {
      var accessType = TypeDictionnary.GetStorageType(destinationType);
      if (accessType == null)
        return NotFound();
      return Ok(accessType);
    }
  }
}
