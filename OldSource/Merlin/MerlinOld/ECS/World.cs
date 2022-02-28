using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;

namespace Merlin.ECS
{
    public class World : IDisposable
    {
        private readonly List<IUpdateSystem> _updateSystems = new List<IUpdateSystem>();
        private readonly List<IDrawSystem> _drawSystems = new List<IDrawSystem>();
        private readonly SortedList<ulong, IEntity> _entities = new SortedList<ulong, IEntity>();

        /// <summary>
        /// All entities, which are in this world context
        /// </summary>
        public IEntity[] Entities => _entities.Values.ToArray();

        /// <summary>
        /// Returns only the entities, which are not destroyed
        /// </summary>
        public IEntity[] ActiveEntities => _entities.Values
            .Where(e => !e.Destroyed)
            .ToArray();

        public World() { }

        #region <<Methods>>

        /// <summary>
        /// Adds a system to this world context
        /// </summary>
        /// <param name="system"></param>
        public void AddSystem(ISystem system)
        {
            if (system is null) throw new ArgumentNullException(nameof(system));

            bool added = false;

            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Add(updateSystem);
                updateSystem.UpdateOrderChanged += OnUpdateOrderChanged;
                added = true;
            }

            if (system is IDrawSystem drawSystem)
            {
                _drawSystems.Add(drawSystem);
                drawSystem.DrawOrderChanged += OnDrawOrderChanged;
                added = true;
            }

            if (!added)
                throw new ArgumentException(
                    "Not support System, must be either a IUpdate- or a IDrawSystem",
                    nameof(system)
                );

            system.Initialize(this);
        }

        /// <summary>
        /// Removes a system of this world context
        /// </summary>
        /// <typeparam name="T">Type of the system</typeparam>
        /// <param name="system"></param>
        /// <returns>the given system</returns>
        public T RemoveSystem<T>(T system) where T : class, ISystem 
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            bool removed = false;

            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Remove(updateSystem);
                updateSystem.UpdateOrderChanged -= OnUpdateOrderChanged;
                removed = true;
            }

            if (system is IDrawSystem drawSystem)
            {
                _drawSystems.Remove(drawSystem);
                drawSystem.DrawOrderChanged -= OnDrawOrderChanged;
                removed = true;
            }
            
            if (!removed)
                throw new ArgumentException(
                    "Not support System, must be either a IUpdate- or a IDrawSystem",
                    nameof(system)
                );

            return system;
        }

        /// <summary>
        /// Updates all applied update systems <see cref="UpdateSystem"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (var s in _updateSystems)
                s.Update(gameTime);
        }

        /// <summary>
        /// Calls all the applied DrawSystems <see cref="DrawSystem"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            foreach (var s in _drawSystems)
                s.Draw(gameTime);
        }

        /// <summary>
        /// Adds an entity to the world context
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="entity"></param>
        /// <returns>the given entity</returns>
        public T AddEntity<T>(T entity) where T : IEntity
        {
            _entities.Add(entity.Id, entity);
            return entity;
        }

        /// <summary>
        /// Destroys and removes all assigned entities with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DestroyEntity(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            foreach (var entity in _entities.Values
                .Where(e => e.Name == name))
            {
                DestroyEntity(entity);
            }
        }

        /// <summary>
        /// Destroys the assigned entity with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DestroyEntity(ulong id) => DestroyEntity(_entities[id]);

        /// <summary>
        /// Destroys the given entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DestroyEntity(IEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (!_entities.ContainsKey(entity.Id))
                throw new ArgumentException($"No entity with id {entity.Id} and name {entity.Name} found!");

            entity.Destroy();
            _entities.Remove(entity.Id);
        }

        /// <summary>
        /// Sorts all draw systems after the order of one has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDrawOrderChanged(object? sender, EventArgs e)
        {
            if (sender is IDrawSystem drawSystem && _drawSystems.Contains(drawSystem))
            {
                _drawSystems.Sort();
            }
        }

        /// <summary>
        /// Sorts all update systems after the order of one has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUpdateOrderChanged(object? sender, EventArgs e)
        {
            if (sender is IUpdateSystem updateSystem && _updateSystems.Contains(updateSystem))
            {
                _updateSystems.Sort();
            }
        }

        /// <summary>
        /// Returns a entity based on the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEntity GetEntityById(ulong id) => _entities[id];

        /// <summary>
        /// Returns a entity based on the given id or
        /// null if there is no entity with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEntity? GetEntityByIdOrNull(ulong id)
            => !_entities.ContainsKey(id) ? null : _entities[id];

        /// <summary>
        /// Returns a entity of this world based on the given name
        /// or null if there is no entity with that name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEntity? GetEntityByName(string name) =>
            _entities
                .Values
                .SingleOrDefault(e => e.Name == name);

        #endregion

        #region <<Special Definitions>>

        /// <summary>
        /// calls <see cref="GetEntityById"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEntity this[ulong id] => GetEntityById(id);

        /// <summary>
        /// calls <see cref="GetEntityByName"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEntity? this[string name] => GetEntityByName(name);

        #endregion

        #region <<Dispose Pattern>>

        // TODO: is Dispose pattern needed?

        ~World()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
