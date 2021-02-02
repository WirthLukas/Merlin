using Merlin.ECS;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Positioning
{
    public class Moving2D : IComponent
    {
        public int MaxSpeed { get; }
        public float Acceleration { get; }
        public float Friction { get; }

        public Vector2 Velocity { get; internal set; }
        public bool IsMoving { get; internal set; }
        public Vector2 Direction { get; set; }

        public IEntity? Entity { get; set; }

        public Moving2D(int maxSpeed, float acceleration = 0.3f, float friction = 0.1f)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Friction = friction;
        }

        public Moving2D WithDirection(Vector2 direction)
        {
            Direction = direction;
            return this;
        }
    }
}
