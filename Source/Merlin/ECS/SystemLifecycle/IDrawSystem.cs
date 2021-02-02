
using System.Collections.Generic;

namespace Merlin.ECS.SystemLifecycle
{
    public interface IDrawSystem : ISystem
    {
        void Draw(List<IEntity> entities);
    }
}
