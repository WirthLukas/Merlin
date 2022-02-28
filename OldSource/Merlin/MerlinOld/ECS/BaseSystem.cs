using System;
using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    public class BaseSystem : ISystem
    {
        private World? _world;

        // TODO: just returning the nullable world? would increase the performance
        /// <summary>
        /// The World where this system is related to
        /// </summary>
        public World World => _world ?? throw new AccessViolationException("World is not initialized");

        /// <inheritdoc />
        public virtual void Initialize(World world) => _world = world;
    }
}
