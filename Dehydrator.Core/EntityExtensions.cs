﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dehydrator
{
    /// <summary>
    /// Provides extension methods for <see cref="IEntity"/>s.
    /// </summary>
    [PublicAPI]
    public static class EntityExtensions
    {
        /// <summary>
        /// Copies all public properties <paramref name="from"/> an <see cref="IEntity"/> to a new clone of it.
        /// Skips <see cref="IEntity.Id"/>.
        /// </summary>
        [NotNull]
        public static TEntity Clone<TEntity>([NotNull] this TEntity from)
            where TEntity : class, IEntity, new()
        {
            var to = new TEntity();
            from.TransferState(to);
            return to;
        }

        /// <summary>
        /// Copies all public properties <paramref name="from"/> one <see cref="IEntity"/> <paramref name="to"/> another.
        /// Skips <see cref="IEntity.Id"/>. Recursivley copies <see cref="IEntity"/> collections.
        /// </summary>
        public static void TransferState<TEntity>([NotNull] this TEntity from, [NotNull] TEntity to)
            where TEntity : class, IEntity, new()
        {
            var entityType = typeof(TEntity);

            foreach (var prop in entityType.GetWritableProperties())
            {
                if (prop.Name == nameof(IEntity.Id)) continue;

                var fromValue = prop.GetValue(@from, null);
                if (prop.IsCollection())
                {
                    if (fromValue == null) continue;
                    var referenceType = prop.GetGenericArg();

                    object targetList = prop.GetValue(to, null);
                    Type collectionType;
                    if (targetList == null)
                    {
                        collectionType = typeof(List<>).MakeGenericType(referenceType);
                        targetList = Activator.CreateInstance(collectionType);
                        prop.SetValue(obj: to, value: targetList, index: null);
                    }
                    else
                    {
                        collectionType = targetList.GetType();
                        collectionType.InvokeClear(target: targetList);
                    }

                    foreach (object reference in (IEnumerable)fromValue)
                    {
                        collectionType.InvokeAdd(target: targetList,
                            value: reference);
                    }
                }
                else prop.SetValue(obj: to, value: fromValue, index: null);
            }
        }
    }
}