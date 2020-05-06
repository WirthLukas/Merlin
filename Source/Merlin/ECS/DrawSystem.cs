using System;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;

namespace Merlin.ECS
{
    /// <summary>
    /// Is a simple implementation of the <see cref="IDrawSystem"/> interface
    /// </summary>
    public abstract class DrawSystem : BaseSystem, IDrawSystem
    {
        #region <<Fields>>

        private bool _visible;
        private int _drawOrder;

        #endregion

        #region <<Properties>>

        /// <summary>
        /// Determines if the draw method of this system
        /// should be called (true) or not (false)
        /// </summary>
        public bool Visible
        {
            get => _visible;
            set
            {
                if (value == _visible) return;

                _visible = value;
                VisibleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Determines the draw priority of this system.
        /// The lower the value the higher the priority
        /// </summary>
        public int DrawOrder
        {
            get => _drawOrder;
            set
            {
                _drawOrder = value;
                DrawOrderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region <<Special Definitions>>

        /// <summary>
        /// Gets called when the draw order value has changed
        /// </summary>
        public event EventHandler<EventArgs>? DrawOrderChanged;

        /// <summary>
        /// Gets called when the visible value has changed
        /// </summary>
        public event EventHandler<EventArgs>? VisibleChanged;

        #endregion

        /// <summary>
        /// Creates a new DrawSystem
        /// </summary>
        /// <param name="visible">should this system be drawn</param>
        /// <param name="drawOrder">the draw priority of the system</param>
        protected DrawSystem(bool visible = true, int drawOrder = 0)
        {
            _visible = visible;
            _drawOrder = drawOrder;
        }

        #region <<Methods>>

        /// <summary>
        /// Draws entities based on their components
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}
