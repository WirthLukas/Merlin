using System;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;

namespace Merlin.ECS
{
    public abstract class BaseUpdateSystem : BaseSystem, IUpdateSystem
    {
        #region <<Fields>>

        private bool _enabled;
        private int _updateOrder;

        #endregion

        #region <<Properties>>

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    EnabledChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

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


        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        protected BaseUpdateSystem(bool enabled = true, int updateOrder = 0)
        {
            _enabled = enabled;
            _updateOrder = updateOrder;
        }

        public abstract void Update(GameTime gameTime);
    }
}
