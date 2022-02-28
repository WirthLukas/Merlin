using Merlin.ECS;
using Merlin.ECS.Lifecycle;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Components
{
    public class Moving2D : Component, IOnUpdate
    {
        private Vector2 _velocity = Vector2.Zero;

        public int MaxSpeed { get; }
        public float Acceleration { get; }
        public float Friction { get; }

        public Vector2 Direction { get; set; } = Vector2.Zero;
        public Vector2 Velocity => _velocity;

        public Moving2D(int maxSpeed, float acceleration, float friction)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Friction = friction;
        }

        public void OnUpdate(GameTime gameTime)
        {
            if (Direction != Vector2.Zero)            // Is Moving?
            {
                _velocity = Vector2.Lerp(
                        value1: _velocity,
                        value2: Direction * MaxSpeed,
                        amount: Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds
                    );
            }
            else
            {
                _velocity = Vector2.Lerp(
                        value1: _velocity,
                        value2: Vector2.Zero,
                        amount: Friction * (float)gameTime.ElapsedGameTime.TotalSeconds
                    );
            }
        }

        public Moving2D WithDirection(Vector2 direction)
        {
            Direction = direction;
            return this;
        }
    }
}
