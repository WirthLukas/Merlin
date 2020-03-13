using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    public abstract class BaseSystem : ISystem
    {
        protected World World { get; set; }

        public virtual void Initialize(World world)
        {
            World = world;
        }
    }
}
