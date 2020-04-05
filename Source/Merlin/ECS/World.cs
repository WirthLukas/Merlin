using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.ECS
{
    public class World : IDisposable
    {
        private readonly List<IUpdateSystem> _updateSystems = new List<IUpdateSystem>();
        private readonly List<IDrawSystem> _drawSystems = new List<IDrawSystem>();
        private readonly SortedList<ulong, Entity> _entities = new SortedList<ulong, Entity>();

        /// <summary>
        /// All entities, which are in this world context
        /// </summary>
        public Entity[] Entities => _entities.Values.ToArray();

        /// <summary>
        /// Returns only the entities, which are not destroyed
        /// </summary>
        public Entity[] ActiveEntities => _entities.Values
            .Where(e => !e.Destroyed)
            .ToArray();

        // TODO: should be removed, cause 3D games don't need a spritebatch
        public SpriteBatch SpriteBatch { get; set; }

        internal World()
        { }

        #region <<Methods>>

        /// <summary>
        /// Adds a system to this world context
        /// </summary>
        /// <param name="system"></param>
        public void AddSystem(ISystem system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Add(updateSystem);
                updateSystem.UpdateOrderChanged += OnUpdateOrderChanged;
            }

            if (system is IDrawSystem drawSystem)
            {
                _drawSystems.Add(drawSystem);
                drawSystem.DrawOrderChanged += OnDrawOrderChanged;
            }
                

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

            ISystem result = null;

            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Remove(updateSystem);
                updateSystem.UpdateOrderChanged -= OnUpdateOrderChanged;
                result = updateSystem;
            }

            if (system is IDrawSystem drawSystem)
            {
                _drawSystems.Remove(drawSystem);
                drawSystem.DrawOrderChanged -= OnDrawOrderChanged;
                result = drawSystem;
            }

            return result as T;
        }

        /// <summary>
        /// Updates all applied update systems <see cref="BaseUpdateSystem"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (var s in _updateSystems)
                s.Update(gameTime);
        }

        /// <summary>
        /// Calls all the applied DrawSystems <see cref="BaseDrawSystem"/>
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
        public T AddEntity<T>(T entity) where T : Entity
        {
            _entities.Add(entity.Id, entity);
            return entity;
        }

        /// <summary>
        /// Destroyes and removes all assigned entities with the given name.
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
        /// Destroyes the assigned entity with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DestroyEntity(ulong id) => DestroyEntity(_entities[id]);

        /// <summary>
        /// Destroyes the given entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DestroyEntity(Entity entity)
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
        private void OnDrawOrderChanged(object sender, EventArgs e)
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
        private void OnUpdateOrderChanged(object sender, EventArgs e)
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
        public Entity GetEntityById(ulong id) => _entities[id];

        /// <summary>
        /// Returns a entity based on the given id or
        /// null if there is no entity with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity GetEntityByIdOrNull(ulong id)
            => !_entities.ContainsKey(id) ? null : _entities[id];

        /// <summary>
        /// Returns a entity of this world based on the given name
        /// or null if there is no entity with that name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entity GetEntityByName(string name) =>
            _entities
                .Values
                .SingleOrDefault(e => e.Name == name);

        #endregion

        #region <<Indexer>>

        /// <summary>
        /// calls <see cref="GetEntityById"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity this[ulong id] => GetEntityById(id);

        /// <summary>
        /// calls <see cref="GetEntityByName"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entity this[string name] => GetEntityByName(name);

        #endregion

        #region <<Dispose Pattern>>

        ~World()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                SpriteBatch?.Dispose();
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
