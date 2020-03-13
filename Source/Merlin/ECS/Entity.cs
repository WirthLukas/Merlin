using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    public class Entity : IEntity, IComparable<Entity>, IComparable
    {
        #region <<Fields>>

        private static long _nextId;
        private bool _destroyed;
        private readonly Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

        #endregion

        #region <<Properties>>

        public long Id { get; }
        public string Name { get; set; }

        public Component[] Components => _components
            .Select(item => item.Value)
            .ToArray();

        public Component[] ActiveComponents => _components
            .Select(item => item.Value)
            .Where(c => c.Enabled)
            .ToArray();

        public bool Destroyed
        {
            get => _destroyed;
            internal set
            {
                _destroyed = value;

                if (_destroyed)
                    IsDestroyedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        public event EventHandler<EventArgs> IsDestroyedChanged;

        public Entity(string name)
        {
            Id = _nextId++;
            Name = name;
        }

        #region <<Methods>>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void AddComponent(Component component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            CheckRequiredComponents(component, Component.RequiredComponentsOf(component));

            _components.Add(component.GetType(), component);
            component.AddToEntity(this);
            component.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="components"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void AddComponents(params Component[] components)
        {
            foreach (var c in components)
                AddComponent(c);
        }

        public virtual Component RemoveComponentOfType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!HasComponentOfType(type))
                throw new ArgumentException($"No Component of type {type.Name} found!");

            if (Component.IsCoreComponent(type))
                throw new ArgumentException($"Cannot remove Core Component {type.Name}");

            Component c = _components[type];
            c.RemoveFromEntity();
            _components.Remove(type);
            return c;
        }

        public Component RemoveComponent<T>() where T : Component
            => RemoveComponentOfType(typeof(T));

        public void ClearComponents()
        {
            foreach (var c in _components.Select(item => item.Value))
            {
                c.RemoveFromEntity();
            }

            _components.Clear();
        }

        public virtual T GetComponent<T>() where T : Component
        {
            if (!HasComponent<T>())
                throw new ArgumentException($"No Component of type {typeof(T).Name} found!");

            return _components[typeof(T)] as T;
        }

        public bool HasComponent<T>() where T : Component
            => HasComponentOfType(typeof(T));

        public bool HasComponentOfType(Type type)
        {
            if (!type.IsSubclassOf(typeof(Component)))
                throw new ArgumentException("Type must inherit Component!");

            return _components.ContainsKey(type);
        }

        #endregion

        #region <<Fluent Methods>>

        public Entity WithComponents(params Component[] components)
        {
            AddComponents(components);
            return this;
        }

        #endregion

        #region <<Interface Methods>>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int CompareTo(Entity other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Not comparable with null");

            return this.Id.CompareTo(other.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Not comparable with null");

            if (!(obj is Component other))
                throw new InvalidOperationException("Can only compare with other Components");

            return this.CompareTo(other);
        }

        #endregion

        protected void CheckRequiredComponents(Component adding, Type[] components)
        {
            foreach (var componentType in components)
            {
                if (!HasComponentOfType(componentType))
                    throw new ArgumentException($"Component of type {componentType.Name} has to be added for Component {adding.GetType().Name}");
            }
        }
    }
}
