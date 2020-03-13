using System;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.ECS
{
    public abstract class BaseDrawSystem : BaseSystem, IDrawSystem
    {
        #region <<Fields>>
        private bool _visible;
        private int _drawOrder;
        #endregion

        #region <<Properties>>
        public bool Visible
        {
            get => _visible;
            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    VisibleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public int DrawOrder
        {
            get => _drawOrder;
            set
            {
                _drawOrder = value;
                DrawOrderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected SpriteBatch SpriteBatch { get; set; }
        #endregion

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        protected BaseDrawSystem(bool visible = true, int drawOrder = 0)
        {
            _visible = visible;
            _drawOrder = drawOrder;
        }

        public override void Initialize(World world)
        {
            base.Initialize(world);
            SpriteBatch = world.SpriteBatch;
        }

        public abstract void Draw(GameTime gameTime);
    }
}
