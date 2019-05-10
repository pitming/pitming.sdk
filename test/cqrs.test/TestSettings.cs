using cqrsLib;
using cqrsLib.Command;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UnitTestProject1
{
  public static class TestSettings
  {
    private static IServiceProvider _serviceProvider;

    static TestSettings()
    {
      var collection = new ServiceCollection();
      collection
        .AddTransient<CommandManagerCoordinator>()
        .AddTransient<ICommandHandler, CreateEntityCommandHandler>()
        .AddTransient<ICommandHandler, UpdateEntityCommandHandler>()
        .AddSingleton<Bus<ICommand>>()
        .AddSingleton<EntityRepository>();

      _serviceProvider = collection.BuildServiceProvider();
    }

    public static CommandManagerCoordinator CommandManagerCoordinator => _serviceProvider.GetRequiredService<CommandManagerCoordinator>();
    public static Bus<ICommand> CommandBus => _serviceProvider.GetRequiredService<Bus<ICommand>>();
    public static EntityRepository EntityRepository => _serviceProvider.GetRequiredService<EntityRepository>();
  }
}
