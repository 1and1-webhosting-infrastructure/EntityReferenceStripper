﻿namespace Dehydrator
{
    /// <summary>
    /// A base implementation of <see cref="INamedEntity"/>. You can derive from this or implement <see cref="INamedEntity"/> directly.
    /// </summary>
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name)
                ? $"{GetType().Name} {Id}"
                : $"{GetType().Name} {Name} ({Id})";
        }
    }
}