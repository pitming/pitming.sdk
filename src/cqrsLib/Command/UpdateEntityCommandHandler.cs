﻿using System;
using System.Threading;

namespace cqrsLib.Command
{
  public class UpdateEntityCommandHandler : ICommandHandler
  {
    private readonly EntityRepository _entityRepository;

    public UpdateEntityCommandHandler(EntityRepository entityRepository)
    {
      _entityRepository = entityRepository;
    }
    public Func<ICommand, object> Selector => c =>
    {
      return c is EntityCommand command ? command.UpdateEntity : null;
    };

    public Action<ICommand> Do =>
    c =>
    {
      var updateEntity = Selector(c) as UpdateEntity;
      Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Handle command:{this.GetType()} - {updateEntity.Id}/{updateEntity.Child}");
      var entity = _entityRepository.Get(updateEntity.Id);
      entity.Children.Add(updateEntity.Child);
      _entityRepository.Upsert(entity);
    };
  }
}
