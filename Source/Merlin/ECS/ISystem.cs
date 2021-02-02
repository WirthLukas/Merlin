using System.Collections.Generic;

namespace Merlin.ECS
{
    public interface ISystem
    {
        IEntityFilter Filter { get; }

        void Update(List<IEntity> entities);
    }
}
