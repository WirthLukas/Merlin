using System;

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
            ComponentType = componentType;
        }
    }
}
