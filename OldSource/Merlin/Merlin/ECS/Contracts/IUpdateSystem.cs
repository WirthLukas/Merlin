using Microsoft.Xna.Framework;

namespace Merlin.ECS.Contracts
{
    /// <summary>
    /// Marks a system as a updateable system.
    /// The <see cref="World"/> class calls the update method of its
    /// applied update systems
    /// </summary>
    public interface IUpdateSystem : ISystem, IUpdateable
    {
    }
}
