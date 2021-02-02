using Merlin.ECS;

namespace Merlin.Tests.Performance
{
    internal struct PositionStruct : IComponent
    {
        public IEntity Entity { get; set; }
    }

    internal struct MovementStruct : IComponent
    {
        public IEntity Entity { get; set; }
    }

    internal struct SpriteStruct : IComponent
    {
        public IEntity Entity { get; set; }
    }

    internal class PositionClass : IComponent
    {
        public IEntity Entity { get; set; }
    }

    internal class MovementClass : IComponent
    {
        public IEntity Entity { get; set; }
    }

    internal class SpriteClass : IComponent
    {
        public IEntity Entity { get; set; }
    }
}
