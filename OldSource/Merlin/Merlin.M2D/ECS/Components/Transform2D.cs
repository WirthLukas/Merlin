using Merlin.ECS;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Components
{
    public class Transform2D : Component
    {
        public Vector2 Position { get; set; }

        public Transform2D(Vector2 position)
        {
            Position = position;
        }
    }
}
