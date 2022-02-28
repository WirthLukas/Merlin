using System;
using System.Collections.Generic;
using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    /// <summary>
    /// Builder class for creating a world object
    /// </summary>
    public class WorldBuilder
    {
        private readonly List<ISystem> _systems = new List<ISystem>();

        /// <summary>
        /// Adds a system to the world context
        /// </summary>
        /// <param name="system"></param>
        /// <returns>this Worldbuilder</returns>
        public WorldBuilder AddSystem(ISystem system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            _systems.Add(system);
            return this;
        }

        /// <summary>
        /// Creates the world object, with the applied configuration
        /// </summary>
        /// <returns>the world object</returns>
        public World Build()
        {
            var world = new World();

            foreach (var system in _systems)
                world.AddSystem(system);

            return world;
        }
    }
}
