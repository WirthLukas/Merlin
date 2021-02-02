using Merlin.ECS;

namespace Merlin.Tests.ECS
{
    internal struct Position : IComponent
    {
        public IEntity Entity { get; set; }
    }
    internal struct Movement : IComponent
    {
        public IEntity Entity { get; set; }
    }
    internal struct Sprite : IComponent
    {
        public IEntity Entity { get; set; }
    }
}