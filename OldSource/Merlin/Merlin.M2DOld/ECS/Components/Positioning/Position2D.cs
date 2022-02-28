using Merlin.ECS;
using Merlin.ECS.Attributes;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Components.Positioning
{
    [CoreComponent]
    public class Position2D : Component
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPositionChanged();
            }
        }

        protected virtual void OnPositionChanged()
        { }

        public Position2D(float x = 0, float y = 0)
            : this(new Vector2(x, y))
        {

        }

        public Position2D(Vector2 position)
        {
            Position = position;
        }
    }
}
