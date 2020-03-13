using Merlin.ECS;
using Merlin.ECS.Attributes;

namespace Merlin.M2D.ECS.Components
{
    [RequiredComponent(typeof(Position2D))]
    public class Moving2D : Component
    {
    }
}
