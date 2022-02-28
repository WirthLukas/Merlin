using System;
using System.Diagnostics.CodeAnalysis;
using Merlin.ECS.Contracts;
using Merlin.ECS.InternalUtils;
using Merlin.ECS.Lifecycle;

namespace Merlin.ECS
{
    public class Entity : IEntity
    {
        private static ulong _nextId = 0;
        private bool _destroyed = false;
        private readonly ComponentManager _componentManager;

        public ulong Id { get; }
        public string Name { get; set; }

        public bool Destroyed
        {
            get => _destroyed;
            private set
            {
                _destroyed = value;

                if (_destroyed)
                    DestroyedChanged?.Invoke(this, this);
            }
        }

        public event EventHandler<IEntity>? DestroyedChanged;

        public Entity(string name)
        {
            Id = _nextId++;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _componentManager = new ComponentManager();
        }

        public void Destroy() => Destroyed = true;

        public T AddComponent<T>(T component) where T : Component
        {
            if (component is null) throw new ArgumentNullException(nameof(component));

            // Only needed if you allow Component annotations (which reduced performance)
            // ComponentAnnotationChecker.CheckRequiredComponents(component, Component.RequiredComponentsOf(component));

            _componentManager.AddComponent(component);

            if (component is IOnAttachment a)
            {
                a.OnAttach(this);
            }

            return component;
        }

        public T? GetComponent<T>(bool inherited = false) where T : Component
            => _componentManager.GetComponent<T>(inherited);

        public bool HasComponent<T>() where T : Component
            => _componentManager.HasComponent<T>();

        public bool HasComponentOfType(Type componentType)
            => _componentManager.HasComponentOfType(componentType);

        public Component RemoveComponentOfType(Type type)
        {
            // Only needed if you allow Component annotations (which reduced performance)
            // if (ComponentAnnotationChecker.IsCoreComponent(type)) 
            //     throw new ArgumentException($"Cannot remove Core Component {type.Name}");

            Component c = _componentManager.RemoveComponentOfType(type);
           
            if (c is IOnRemovement r)
            {
                r.OnRemove(this);
            }

            return c;
        }

        public Component RemoveComponent<T>() where T : Component
            => RemoveComponentOfType(typeof(T));

        public int CompareTo([AllowNull] IEntity other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other), "Not comparable with null");

            return this.Id.CompareTo(other.Id);
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Not comparable with null");

            if (!(obj is IEntity other))
                throw new InvalidOperationException("Can only compare with other Components");

            return this.CompareTo(other);
        }

        public void AddComponents(params Component[] components)
        {
            foreach (var component in components)
                AddComponent(component);
        }

        public Entity WithComponents(params Component[] components)
        {
            AddComponents(components);
            return this;
        }
    }
}
