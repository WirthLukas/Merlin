using System;

namespace Merlin.ECS.Attributes
{
    /// <summary>
    /// Determines the Component as CoreComponent
    ///
    /// This means that the component can not be removed from the
    /// entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CoreComponentAttribute : Attribute
    {
    }
}
