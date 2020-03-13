using System;
using System.Collections.Generic;

namespace Merlin.ECS.Contracts
{
    public interface IEntity : IComponentManager, IDestroyable
    {
        long Id { get; }
        string Name { get; }
    }

    public class EntityNameComparer : IComparer<IEntity>
    {
        public int Compare(IEntity x, IEntity y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }

    public class EntityIdComparer : IComparer<IEntity>
    {
        public int Compare(IEntity x, IEntity y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            return x.Id.CompareTo(y.Id);
        }
    }

    public class EntityDestroyedComparer : IComparer<IEntity>
    {
        public int Compare(IEntity x, IEntity y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            return x.Destroyed.CompareTo(y.Destroyed);
        }
    }
}
