using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
  public class TypeRepository : ITypeRepository
  {
    private static readonly Dictionary<string, Dictionary<string, string>> _dic;

    static TypeRepository()
    {
      _dic = new Dictionary<string, Dictionary<string, string>>
      {
        { "Nearline", new Dictionary<string, string> { { "Samba", null } } },
        { "Avid", new Dictionary<string, string> { { "Nexis", "Avid" } } }
      };
    }

    public string[] GetStorageTypes()
    {
      return _dic.Keys.ToArray();
    }

    public string[] GetAccessType(string destinationType)
    {
      var accessDic = _dic.GetValueOrDefault(destinationType);
      if (accessDic == null)
        return new string[0];
      return accessDic.Keys.ToArray();
    }

    public string GetReferencementType(string destinationType, string accessType)
    {
      var accessDic = _dic.GetValueOrDefault(destinationType);
      return accessDic?.GetValueOrDefault(accessType);
    }
  }
}
