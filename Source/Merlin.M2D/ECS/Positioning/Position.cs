using Merlin.ECS;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Positioning
{
    public class Position2D : IComponent
    {
        public float X { get; set; }
        public float Y { get; set; }
        public IEntity? Entity { get; set; }

        public Position2D(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }

        public Position2D(Vector2 position)
        {
            (X, Y) = position;
        }

        public Vector2 ToVector2() => new Vector2(X, Y);
    }
}
