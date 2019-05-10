using cqrsLib;
using System;
using System.Collections.Concurrent;

namespace cqrsLib
{
  public class EntityRepository
  {
    private readonly ConcurrentDictionary<int, Entity> _dico = new ConcurrentDictionary<int, Entity>();

    public Entity Get(int id)
    {
      if (_dico.TryGetValue(id, out var entity))
        return entity;
      return null;
    }
    public void Delete(int id) => _dico.TryRemove(id, out var _);

    public void Upsert(Entity entity) => _dico.AddOrUpdate(entity.Id, entity, (_, __) => entity);

    public void Clear() => _dico.Clear();
  }
}
