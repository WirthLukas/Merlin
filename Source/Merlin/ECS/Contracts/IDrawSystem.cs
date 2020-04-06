using Microsoft.Xna.Framework;

namespace Merlin.ECS.Contracts
{
    /// <summary>
    /// Marks a system as a drawable system.
    /// The <see cref="World"/> class calls the draw method of its
    /// applied draw systems
    /// </summary>
    public interface IDrawSystem : IDrawable, ISystem
    {
    }
}
