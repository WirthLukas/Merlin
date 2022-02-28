using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.Attributes;
using Merlin.ECS.Contracts;

namespace Merlin.ECS.InternalUtils
{
    // These are methods for checking the component annotations
    // Are not executed, because annotations have lower performance
    public static class ComponentAnnotationChecker
    {
        /// <summary>
        /// Returns the types of the components, which are required for the given component
        /// </summary>
        /// <param name="c">the component, which should be analyzed</param>
        /// <returns>The types of the required components</returns>
        internal static Type[] RequiredComponentsOf(IComponent c)
        {
            var attributes = Attribute.GetCustomAttributes(c.GetType());

            var requiredComponents = attributes
                .OfType<RequiredComponentAttribute>()
                .Select(attr => attr.ComponentType)
                .ToArray();

            return requiredComponents;
        }

        /// <summary>
        /// Checks if the given type has the <see cref="CoreComponentAttribute"/>
        /// Core Components can not be removed from the entity
        /// </summary>
        /// <param name="c">the type of a component</param>
        /// <returns>is the given component a core component</returns>
        internal static bool IsCoreComponent(Type c)
        {
            var attributes = Attribute.GetCustomAttributes(c);

            return attributes
                .OfType<CoreComponentAttribute>()
                .Any();
        }
        
        internal static void CheckRequiredComponents(IEntity entity, IComponent adding, IEnumerable<Type> components)
        {
            foreach (var componentType in components)
            {
                if (!entity.HasComponentOfType(componentType))
                    throw new ArgumentException($"Component of type {componentType.Name} has to be added for Component {adding.GetType().Name}");
            }
        }
    }
}