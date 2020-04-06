using System;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;

namespace Merlin.ECS
{
    /// <summary>
    /// Simple Implementation of an UpdateSystem.
    /// If it gets added to an <see cref="World"/> object, the update method
    /// gets called each frame
    /// </summary>
    public abstract class UpdateSystem : BaseSystem, IUpdateSystem
    {
        #region <<Fields>>

        private bool _enabled;
        private int _updateOrder;

        #endregion

        #region <<Properties>>

        /// <summary>
        /// Determines if the update method should be called (true) by the related
        /// <see cref="World"/> class or not (false)
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value == _enabled) return;

                _enabled = value;
                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The update priority of this system
        /// The lower the value the higher the priority
        /// </summary>
        public int UpdateOrder
        {
            get => _updateOrder;
            set
            {
                _updateOrder = value;
                UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region <<Special Definitions>>

        /// <summary>
        /// Gets called when the enabled value has changed
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;

        /// <summary>
        /// Gets called when the update order value has changed
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        #endregion

        protected UpdateSystem(bool enabled = true, int updateOrder = 0)
        {
            _enabled = enabled;
            _updateOrder = updateOrder;
        }

        /// <summary>
        /// Updates the data of the entities, which have the components this system needs
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
    }
}
