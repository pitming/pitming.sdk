using cqrsLib.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UnitTestProject1
{

  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void AssertOneMessageSucceed()
    {
      TestSettings.EntityRepository.Clear();
      TestSettings.CommandBus.Initialize();

      var commandManager = TestSettings.CommandManagerCoordinator;
      commandManager.Start();

      var entityId = 18;
      SendCreateEntity(entityId);

      commandManager.Stop();

      Assert.AreEqual(entityId, TestSettings.EntityRepository.Get(entityId)?.Id);
    }

    [TestMethod]
    public void AssertLotsOfCreatedWithDifferentIdsMessageSucceed()
    {
      ThreadPool.GetMaxThreads(out var workerThreads, out var completionThreads);
      Console.WriteLine($"w:{workerThreads}-c:{completionThreads}");
      ThreadPool.GetAvailableThreads(out var workerThreads2, out var completionThreads2);
      Console.WriteLine($"w:{workerThreads2}-c:{completionThreads2}");

      TestSettings.EntityRepository.Clear();
      TestSettings.CommandBus.Initialize();

      var commandManager = TestSettings.CommandManagerCoordinator;
      commandManager.Start();

      var nbCreate = 100;

      var ids = new List<int>();
      for (var i = 0; i < nbCreate; i++)
        ids.Add(i);

      foreach (var id in ids)
        SendCreateEntity(id);

      commandManager.Stop();

      for (var i = 0; i < nbCreate; i++)
      {
        Assert.AreEqual(i, TestSettings.EntityRepository.Get(i)?.Id);
      }
    }

    [TestMethod]
    public void Assert1CreateAndLotsUpdatedOnSameIdSucceed()
    {
      TestSettings.EntityRepository.Clear();
      TestSettings.CommandBus.Initialize();

      var commandManager = TestSettings.CommandManagerCoordinator;
      commandManager.Start();

      var entityId = 18;
      const int nbUpdate = 10;

      SendCreateEntity(entityId);

      for (var i = 0; i < nbUpdate; i++)
        SendUpdateEntity(entityId, i);

      commandManager.Stop();

      var storedEntity = TestSettings.EntityRepository.Get(entityId);
      Assert.IsNotNull(storedEntity);

      Assert.AreEqual(nbUpdate, storedEntity.Children.Count, string.Join(',', storedEntity.Children));

      for (var i = 0; i < nbUpdate; i++)
        Assert.AreEqual(i, storedEntity.Children[i]);
    }

    private CreateEntity SendCreateEntity(int entityId)
    {
      var createEntity = new CreateEntity { Id = entityId };

      TestSettings.CommandBus.SendMessage(new EntityCommand
      {
        CommandHeader = new CommandHeader { EntityId = entityId },
        CreateEntity = createEntity
      });

      return createEntity;
    }

    private UpdateEntity SendUpdateEntity(int entityId, int child)
    {
      var updateEntity = new UpdateEntity { Id = entityId, Child = child };

      TestSettings.CommandBus.SendMessage(new EntityCommand
      {
        CommandHeader = new CommandHeader { EntityId = entityId },
        UpdateEntity = updateEntity
      });

      return updateEntity;
    }
  }
}
