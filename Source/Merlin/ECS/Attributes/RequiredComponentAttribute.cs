using System;
using Merlin.ECS.Contracts;

namespace Merlin.ECS.Attributes
{
    /// <summary>
    /// Determines which component type have to be added to the entity
    /// before the related component gets added.
    /// Can be applied multiple times
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredComponentAttribute : Attribute
    {
        public Type ComponentType { get; }

        public RequiredComponentAttribute(Type componentType)
        {
            if (!componentType.IsSubclassOf(typeof(IComponent)))
                throw new ArgumentException("Type must inherit from IComponent!");
            
            ComponentType = componentType;
        }
    }
}
