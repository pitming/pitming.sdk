﻿using System;

namespace cqrsLib.Command
{
  public class CreateEntityCommandHandler : ICommandHandler
  {
    private readonly EntityRepository _entityRepository;

    public CreateEntityCommandHandler(EntityRepository entityRepository)
    {
      _entityRepository = entityRepository;
    }
    public Func<ICommand, object> Selector => c =>
    {
      return c is EntityCommand command ? command.CreateEntity : null;
    };
    public Action<ICommand> Do =>
      c =>
    {
      var createEntity = Selector(c) as CreateEntity;
      _entityRepository.Upsert(new Entity { Id = createEntity.Id });
    };
  }
}