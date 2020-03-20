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

        public Entity[] Entities => _entities.Values.ToArray();
        public Entity[] ActiveEntities => _entities.Values
            .Where(e => !e.Destroyed)
            .ToArray();

        public SpriteBatch SpriteBatch { get; set; }

        internal World()
        { }

        #region <<Methods>>

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

        public void Update(GameTime gameTime)
        {
            foreach (var s in _updateSystems)
                s.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var s in _drawSystems)
                s.Draw(gameTime);
        }

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

        private void OnDrawOrderChanged(object sender, EventArgs e)
        {
            if (sender is IDrawSystem drawSystem && _drawSystems.Contains(drawSystem))
            {
                _drawSystems.Sort();
            }
        }

        private void OnUpdateOrderChanged(object sender, EventArgs e)
        {
            if (sender is IUpdateSystem updateSystem && _updateSystems.Contains(updateSystem))
            {
                _updateSystems.Sort();
            }
        }

        public Entity GetEntityById(ulong id)
        {
            return _entities[id];
        }

        public Entity GetEntityByName(string name)
        {
            return _entities.Values
                .SingleOrDefault(e => e.Name == name);
        }

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
