using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

// ReSharper disable once CheckNamespace
namespace Merlin.ECS
{
    public class Entity
    {
        private static uint _id = 0;
        public static IEntityFilter That => new EntityFilter();

        public uint Id { get; }

        private readonly Dictionary<string, IComponent> _components = new ();

        public Entity()
        {
            Id = _id++;
        }

        public T AddComponent<T>() where T : IComponent, new()
        {
            var comp = new T();
            return AddComponent(comp.GetType().Name, comp);
        }

        public T AddComponent<T>(T comp) where T : IComponent => AddComponent(comp.GetType().Name, comp);

        public T AddComponent<T>(string name, T comp) where T : IComponent
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (comp == null) throw new ArgumentNullException(nameof(comp));

            _components.Add(name, comp);
            //comp.Entity = this;
            return comp;
        }

        public T RemoveComponent<T>() where T : IComponent => RemoveComponent<T>(typeof(T).Name);

        public T RemoveComponent<T>(string name) where T : IComponent
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var c = _components[name];
            _components.Remove(name);
            c.Entity = null;
            return (T)c;
        }

        public T GetComponent<T>() where T : IComponent => (T) _components[typeof(T).Name];

        public T GetComponent<T>(string name) where T : IComponent
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return (T) _components[name];
        }

        public T GetComponentWithInherited<T>() where T : class, IComponent
            => _components.Values
                .OfType<T>()
                .First();

        public bool HasComponent<T>() where T : IComponent => _components.ContainsKey(typeof(T).Name);
        public bool HasComponent(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return _components.ContainsKey(name);
        }

        public void Destroy()
        {
            foreach (var (name, comp) in _components)
            {
                comp.Entity = null;
                _components.Remove(name);
            }
        }
    }
}
