using Merlin.ECS;
using Merlin.ECS.Attributes;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Components.Positioning
{
    [CoreComponent]
    [RequiredComponent(typeof(Position2D))]
    public class Moving2D : Component
    {
        private Vector2 _directionVector;

        public Vector2 DirectionVector
        {
            get => _directionVector;
            set
            {
                if (value.Length() > 1)
                    value.Normalize();

                _directionVector = value;
            }
        }

        public float Speed { get; set; }

        public Vector2 VelocityVector => DirectionVector * Speed;

        public Moving2D(float speed = 1)
            : this (speed, Vector2.UnitX)
        { }

        public Moving2D(float speed, Vector2 directionVector)
        {
            Speed = speed;
            DirectionVector = directionVector;
        }
    }
}
