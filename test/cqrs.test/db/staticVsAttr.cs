using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace cqrs.test
{
  public abstract class StoreItem<T>
  {
    public StoreItem()
    {
      CheckVersion();
    }

    private bool CheckVersion()
    {
      var storeItemAttribute = GetType().GetCustomAttributes(typeof(StoreItemAttribute), false).FirstOrDefault() as StoreItemAttribute;
      storeItemAttribute?.CheckVersion(GetType(), typeof(T));
      return true;
    }

    public string Id { get; set; }
    public T MyProperty { get; set; }
  }

  [AttributeUsage(AttributeTargets.Class)]
  public class StoreItemAttribute : Attribute
  {
    private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, int>> _checkByTypes = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, int>>();
    public StoreItemAttribute(string version)
    {
      Version = version;
    }

    public void CheckVersion(Type storeType, Type itemType)
    {
      if (_checkByTypes.AddOrUpdate(storeType, new ConcurrentDictionary<Type, int>(), (_, val) => val + 1) == 1)
      {
        Console.WriteLine("Doing the check");
        var versionField = itemType.GetField("Version");
        if (versionField != null)
        {
          var itemVersionValue = versionField.GetValue(null) as string;
          if (Version != itemVersionValue)
          {
            throw new StoreItemVersionException(Version, itemVersionValue);
          }
        }
      }
    }

    public string Version { get; }
  }

  public class StoreItemVersionException : Exception
  {
    public StoreItemVersionException(string storeVersion, string itemVersion) :
      base($"Store version:{storeVersion} != Item version:{itemVersion}")
    { }
  }

  public class MessageWithVersion
  {
    public static string Version = "0.0.1";

    public int Value1 { get; set; }
    public string Value2 { get; set; }
  }

  [StoreItem("0.0.1")]
  public class GoodMessageStoreItem : StoreItem<MessageWithVersion>
  { }

  [StoreItem("1.0.0")]
  public class BadMessageStoreItem : StoreItem<MessageWithVersion>
  { }

  public class RienAVoirMessageStoreItem : StoreItem<string>
  { }


  [TestClass]
  public class StaticVsAttr
  {
    [TestMethod]
    public void AssertBadVersionFailed()
    {
      try
      {
        var messageStoreItem = new BadMessageStoreItem();
        Assert.Fail("Should have throw a specific exception");
      }
      catch (StoreItemVersionException) { }
    }

    [TestMethod]
    public void AssertGoodVersionSucceed()
    {
      for (var i = 0; i < 1000; i++)
      {
        var messageStoreItem = new GoodMessageStoreItem();
        Assert.IsNotNull(messageStoreItem);
      }
    }

    [TestMethod]
    public void AssertNoVersionSucceed()
    {
      var messageStoreItem = new RienAVoirMessageStoreItem();
      Assert.IsNotNull(messageStoreItem);
    }
  }
}
