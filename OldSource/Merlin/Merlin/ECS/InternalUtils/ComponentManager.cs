﻿using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.Contracts;

namespace Merlin.ECS.InternalUtils
{
    // TODO add method comments
    internal class ComponentManager
    {
        private readonly IDictionary<Type, Component> _components = new Dictionary<Type, Component>();

        public Component[] Components => _components.Values.ToArray();

        public Component[] ActiveComponents
            => _components.Values
                .Where(c => c.IsEnabled)
                .ToArray();

        public TComponent? GetComponent<TComponent>(bool withInherited = false) where TComponent : class, IComponent
        {
            if (withInherited)
            {
                return _components.Values.OfType<TComponent>().FirstOrDefault();
            }

            if (!HasComponent<TComponent>())
                return null;

            return _components[typeof(TComponent)] as TComponent;
        }

        public bool HasComponent<TComponent>() where TComponent : IComponent
            => HasComponentOfType(typeof(TComponent));

        public bool HasComponentOfType(Type type)
        {
            // if (!type.IsSubclassOf(typeof(Component)))
            //     throw new ArgumentException("Type must inherit from IComponent!");

            if (!typeof(Component).IsAssignableFrom(type))
                throw new ArgumentException("Type must inherit from IComponent!");
            
            return _components.ContainsKey(type);
        }

        public TComponent AddComponent<TComponent>(TComponent component) where TComponent : Component
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            _components.Add(component.GetType(), component);
            return component;
        }

        public Component RemoveComponentOfType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!typeof(Component).IsAssignableFrom(type))
                throw new ArgumentException("Type must inherit from IComponent!");
            
            if (!HasComponentOfType(type))
                throw new ArgumentException($"No Component of type {type.Name} found!");

            Component c = _components[type];
            _components.Remove(type);
            return c;
        }

        public void ClearComponents() => _components.Clear();
    }
}
