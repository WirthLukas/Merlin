using System;

namespace Merlin.ECS.Attributes
{
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
