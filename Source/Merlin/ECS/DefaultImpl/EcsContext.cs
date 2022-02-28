using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.SystemLifecycle;

// ReSharper disable once CheckNamespace
namespace Merlin.ECS
{
    // TODO: Dispose Pattern?
    public class EcsContext : IEcsContext
    {
        private readonly List<IOnInitSystem> _onInitSystems = new();
        private readonly List<IOnDestroySystem> _onDestroySystems = new();

        private readonly Dictionary<ISystem, List<IEntity>> _systems = new();
        private readonly Dictionary<IDrawSystem, List<IEntity>> _drawSystems = new();
        private readonly Dictionary<uint, IEntity> _entities = new();

        public IEnumerable<IEntity> Entities => _entities.Values;

        public IEcsContext AddSystem(ISystem system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            if (system is IOnInitSystem init) _onInitSystems.Add(init);
            if (system is IOnDestroySystem destroy) _onDestroySystems.Add(destroy);
            if (system is IDrawSystem draw) _drawSystems.Add(draw, new List<IEntity>());

            _systems.Add(system, new List<IEntity>());
            return this;
        }

        public IEcsContext RemoveSystem(ISystem system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            if (system is IOnInitSystem init) _onInitSystems.Remove(init);
            if (system is IOnDestroySystem destroy) _onDestroySystems.Remove(destroy);
            if (system is IDrawSystem draw) _drawSystems.Remove(draw);

            _systems.Remove(system);
            return this;
        }

        public void InitializeSystems() => _onInitSystems.ForEach(s => s.OnInit());

        public void Update()
        {
            foreach (var (updateSystem, entities) in _systems)
            {
                updateSystem.Update(entities);
            }
        }

        public void Draw()
        {
            foreach (var (drawSystem, entities) in _drawSystems)
            {
                drawSystem.Draw(entities);
            }
        }

        public void DestroySystems() => _onDestroySystems.ForEach(s => s.OnDestroy());

        public IEntity AddEntity(IEntity entity)
        {
            _entities.Add(entity.Id, entity);

            foreach (var (updateSystem, entities) in _systems)
            {
                if (updateSystem.Filter.Check(entity))
                {
                    entities.Add(entity);
                }
            }

            foreach (var (drawSystem, entities) in _drawSystems)
            {
                if (drawSystem.Filter.Check(entity))
                {
                    entities.Add(entity);
                }
            }

            return entity;
        }

        public void DestroyEntity(IEntity entity)
        {
            entity.Destroy();
            _entities.Remove(entity.Id);

            foreach (var (_, entities) in _systems)
            {
                entities.Remove(entity);
            }

            foreach (var (_, entities) in _drawSystems)
            {
                entities.Remove(entity);
            }
        }

        #region <<Queries>>

        // Todo Entity types??

        public IEntity? GetSingleton(Func<IEntity, bool> predicate) => _entities.Values.SingleOrDefault(predicate);

        public IEntity? GetRandom(Func<IEntity, bool> predicate)
        {
            return null;
        }

        public IEnumerable<IEntity> GetEntitiesByComponent()
        {
            return null;
        }

        #endregion
    }
}
