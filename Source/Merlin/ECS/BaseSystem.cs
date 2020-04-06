using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    public abstract class BaseSystem : ISystem
    {
        /// <summary>
        /// The World where this system is related to
        /// </summary>
        protected World World { get; set; }

        /// <inheritdoc />
        public virtual void Initialize(World world) => World = world;
    }
}
