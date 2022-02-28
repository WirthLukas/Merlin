using System;
using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    public abstract class Component : IComponent
    {
        private bool _isEnabled = false;
        private uint _updateOrder = 0;
        private IEntity? _entity = null;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;

                    if (_isEnabled)
                        OnEnabled();
                    else
                        OnDisabled();

                    EnabledChanged?.Invoke(this, _isEnabled);
                }
            }
        }

        public uint UpdateOrder
        {
            get => _updateOrder;
            set
            {
                if (_updateOrder != value)
                {
                    _updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, _updateOrder);
                }
            }
        }

        public IEntity? Entity
        {
            get => _entity;
            internal set
            {
                _entity = value;
                IsEnabled = _entity != null;
            }
        }

        public event EventHandler<bool>? EnabledChanged;
        public event EventHandler<uint>? UpdateOrderChanged;

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
    }
}
