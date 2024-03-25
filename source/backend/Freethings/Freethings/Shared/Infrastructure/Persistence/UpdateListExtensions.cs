using Microsoft.EntityFrameworkCore;

namespace Freethings.Shared.Infrastructure.Persistence;

public static class UpdateListExtensions
{
    public static void Update<TEntity, TAggregate>(
        this List<TEntity> entityList,
        List<TAggregate> aggregateList,
        Func<TEntity, TAggregate, bool> matches,
        Func<TAggregate, TEntity> onAdd,
        DbContext context = null)
    {
        List<TAggregate> toUpdate = aggregateList
            .Where(aggregateItem => entityList.Any(entityItem => matches(entityItem, aggregateItem)))
            .ToList();
        List<TAggregate> toAdd = aggregateList
            .Except(toUpdate)
            .ToList();
        List<TEntity> toRemove = entityList
            .Where(entityItem => aggregateList.All(agrItem => !matches(entityItem, agrItem)))
            .ToList();

        foreach (TAggregate updated in toUpdate)
        {
            TEntity current = entityList.Single(entItem => matches(entItem, updated));
            //onUpdate(current, updated);
        }

        foreach (TAggregate added in toAdd)
        {
            TEntity trackAdded = onAdd(added);
            entityList.Add(trackAdded);
            context?.Add(trackAdded);
        }

        foreach (TEntity removed in toRemove)
        {
            entityList.Remove(removed);
            context?.Remove(removed);
        }
    }
}