using Microsoft.Xna.Framework;

namespace Merlin.ECS.Lifecycle
{
    public interface IOnUpdate
    {
        void OnUpdate(GameTime gameTime);
    }
}
