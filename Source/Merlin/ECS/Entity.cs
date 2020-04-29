using System;
using Merlin.ECS.Contracts;
using Merlin.ECS.InternalUtils;

namespace Merlin.ECS
{
    public class Entity : IEntity
    {
        private static ulong _nextId;
        private readonly ComponentManager _componentManager;
        private bool _destroyed;

        #region <<Properties>>

        /// <summary>
        /// Id of the Entity
        /// </summary>
        public ulong Id { get; }

        /// <summary>
        /// Name of the Entity
        /// </summary>
        public string Name { get; set; }

        public Component[] Components => _componentManager.Components;
        public Component[] ActiveComponents => _componentManager.ActiveComponents;

        /// <summary>
        /// Returns if the Entity is destroyed.
        /// Systems will not update this entity 
        /// if it is destroyed
        /// </summary>
        public bool Destroyed
        {
            get => _destroyed;
            internal set
            {
                _destroyed = value;

                if (_destroyed)
                    DestroyedChanged?.Invoke(this);
            }
        }

        #endregion

        public event DestroyedHandler DestroyedChanged;

        public Entity(string name)
        {
            Id = _nextId++;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _componentManager = new ComponentManager();
        }

        public Entity()
            : this(Utils.RandomString(10))
        { }

        #region <<ComponentManager Methods>>

        public T GetComponent<T>(bool withInherited = false) where T : class, IComponent
            => _componentManager.GetComponent<T>(withInherited);

        public bool HasComponent<T>() where T : class, IComponent
            => _componentManager.HasComponent<T>();

        public bool HasComponentOfType(Type type)
            => _componentManager.HasComponentOfType(type);

        public T AddComponent<T>(T component) where T : Component
        {
            if (component is null) throw new ArgumentNullException(nameof(component));

            // Only needed if you allow Component annotations (which reduced performance)
            // ComponentAnnotationChecker.CheckRequiredComponents(component, Component.RequiredComponentsOf(component));

            _componentManager.AddComponent(component);
            component.AddToEntity(this);
            component.Initialize();
            return component;
        }

        public T AddComponent<T>() where T : Component, new()
            => AddComponent(new T());

        public void AddComponents(params Component[] components)
        {
            foreach (var component in components)
                AddComponent(component);
        }

        public Component RemoveComponentOfType(Type type)
        {
            // Only needed if you allow Component annotations (which reduced performance)
            // if (ComponentAnnotationChecker.IsCoreComponent(type)) 
            //     throw new ArgumentException($"Cannot remove Core Component {type.Name}");

            Component c = _componentManager.RemoveComponentOfType(type);
            c.RemoveFromEntity();
            return c;
        }

        public Component RemoveComponent<T>() where T : Component
            => RemoveComponentOfType(typeof(T));

        public void ClearComponents()
        {
            foreach (var c in _componentManager.Components)
            {
                c.RemoveFromEntity();
            }

            _componentManager.ClearComponents();
        }

        #endregion

        #region <<Methods>>

        public void Destroy() => Destroyed = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int CompareTo(IEntity other)
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

        #region <<Fluent Methods>>

        public Entity WithComponents(params Component[] components)
        {
            AddComponents(components);
            return this;
        }

        #endregion
    }
}
