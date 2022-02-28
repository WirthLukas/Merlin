using System.Collections.Generic;

namespace Merlin.ECS
{
    public interface IEcsContext
    {
        IEnumerable<IEntity> Entities { get; }

        IEcsContext AddSystem(ISystem system);
        IEcsContext RemoveSystem(ISystem system);

        void InitializeSystems();
        void Update();
        void Draw();
        void DestroySystems();

        IEntity AddEntity(IEntity entity);
        void DestroyEntity(IEntity entity);
    }
}
