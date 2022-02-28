using System;
using Merlin.ECS;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Components.Positioning
{
    public class NewMoving2D : Component
    {
        #region <<Fields>>

        private Vector2 _velocity;
        private bool _moving = true;

        #endregion
        
        #region <<Properties>>

        public int MaxSpeed { get; }
        public float Acceleration { get; }
        public float Friction { get; }

        public Vector2 Velocity => _velocity;
        
        public Vector2 Direction { get; set; }

        public bool Moving => _moving;

        #endregion

        public NewMoving2D(int maxSpeed, float acceleration = 0.3f, float friction = 0.1f)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Friction = friction;
        }

        #region <<Methods>>

        public override void Update(GameTime gameTime)
        {
            if (_moving)
            {
                _velocity = Direction * Acceleration * gameTime.ElapsedGameTime.Milliseconds;

                _velocity.X = _velocity.X < 0 ? Math.Max(_velocity.X, -MaxSpeed) : Math.Min(_velocity.X, MaxSpeed);
                _velocity.Y = _velocity.Y < 0 ? Math.Max(_velocity.Y, -MaxSpeed) : Math.Min(_velocity.Y, MaxSpeed);
            }
            else
            {
                _velocity *= Friction * gameTime.ElapsedGameTime.Milliseconds;

                if (Math.Abs(_velocity.X) < 0.001)
                    _velocity.X = 0;

                if (Math.Abs(_velocity.Y) < 0.001)
                    _velocity.Y = 0;
            }
        }

        public void StartMoving() => _moving = true;

        public void StopMoving() => _moving = false;

        #endregion

        public NewMoving2D WithDirection(Vector2 direction)
        {
            Direction = direction;
            return this;
        } 
    }
}