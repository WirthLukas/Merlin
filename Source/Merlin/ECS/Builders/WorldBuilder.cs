using System;
using System.Collections.Generic;
using Merlin.ECS.Contracts;

namespace Merlin.ECS.Builders
{
    public class WorldBuilder
    {
        private readonly List<ISystem> _systems = new List<ISystem>();

        public WorldBuilder AddSystem(ISystem system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            _systems.Add(system);
            return this;
        }

        public World Build()
        {
            var world = new World();

            foreach (var system in _systems)
                world.AddSystem(system);

            return world;
        }
    }
}
