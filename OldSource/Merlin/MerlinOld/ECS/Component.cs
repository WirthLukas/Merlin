using System;
using Merlin.ECS.Contracts;
using Microsoft.Xna.Framework;

namespace Merlin.ECS
{
    /// <summary>
    /// A Component can be added to an entity. This component inflates
    /// the behaviour of the entity, if the corresponding system is applied to
    /// the world object of the entity
    /// 
    /// Lifecycle Methods:
    /// * On... Methods are for the behaviour of subclasses
    /// * events are for other classes, who needs to be informed of changes
    /// </summary>
    public abstract class Component : IComponent
    {
        #region <<Fields>>

        private int _updateOrder;
        private bool _isEnabled;
        private Entity? _entity;

        #endregion

        #region <<Properties>>

        /// <summary>
        /// Is this Component enabled?
        /// Which means: should this Component influence the behavior of the related entity
        /// </summary>
        public bool Enabled
        {
            get => _isEnabled;
            set
            {
                // Don't call Event Methods if nothing is changing
                if (_isEnabled == value)
                    return;

                _isEnabled = value;

                if (_isEnabled)
                    OnEnabled();
                else
                    OnDisabled();

                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Returns the Update Order.
        /// </summary>
        public int UpdateOrder
        {
            get => _updateOrder;
            set
            {
                if (_updateOrder != value)
                {
                    _updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// The entity where this Component is related to
        /// </summary>
        public Entity? Entity
        {
            get => _entity;
            protected set
            {
                if (value == null)
                    RemoveFromEntity();
                else
                    AddToEntity(value);
            }
        }

        #endregion

        /// <summary>
        /// For other classes who needs to be informed if enabled value is changed
        /// </summary>
        public event EventHandler<EventArgs>? EnabledChanged;
        
        /// <summary>
        /// For other classes who needs to be informed if the update order has changed
        /// </summary>
        public event EventHandler<EventArgs>? UpdateOrderChanged;

        #region <<Entity based Methods>>

        /// <summary>
        /// Adds this Component to the given entity and sets this 
        /// to enabled
        /// </summary>
        /// <param name="entity"></param>
        protected internal virtual void AddToEntity(Entity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity), "Cannot add to null");
            OnAddedToEntity();
            Enabled = true;
        }

        /// <summary>
        /// Removes this component from the related entity
        /// </summary>
        protected internal virtual void RemoveFromEntity()
        {
            if (_entity != null)
            {
                _entity = null;
                Enabled = false;
                OnRemovedFromEntity();
            }
        }

        #endregion

        #region <<Lifecycle Methods>>

        /// <summary>
        /// Initializes Members of this component
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Behavior after this component is added to an entity
        /// </summary>
        protected virtual void OnAddedToEntity() { }

        /// <summary>
        /// Behavior after this component is removed from the entity
        /// </summary>
        protected virtual void OnRemovedFromEntity() { }

        /// <summary>
        /// Behavior after this component is changed to Enabled
        /// </summary>
        protected virtual void OnEnabled() { }

        /// <summary>
        /// Behavior after this component is changed to Disabled
        /// </summary>
        protected virtual void OnDisabled() { }

        /// <summary>
        /// If needed, you can update you component based on
        /// the gameTime
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) { }

        #endregion

        #region <<Comparable Implementation>>

        /// <inheritdoc />
        public int CompareTo(IComponent? other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other), "Not comparable with null");

            return _updateOrder.CompareTo(other.UpdateOrder);
        }

        /// <inheritdoc />
        public int CompareTo(object? obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Not comparable with null");

            if (!(obj is IComponent other))
                throw new InvalidOperationException("Can only compare with other Components");

            return CompareTo(other);
        }

        #endregion

        #region <<Fluent Methods>>

        /// <summary>
        /// Sets the update order of this component
        /// </summary>
        /// <param name="updateOrder">new update order</param>
        /// <returns>this component</returns>
        public Component WithUpdateOrder(int updateOrder)
        {
            _updateOrder = updateOrder;
            return this;
        }

        /// <summary>
        /// Sets the enabled value of this component
        /// </summary>
        /// <param name="enabled">is this component enabled</param>
        /// <returns>this component</returns>
        public Component IsEnabled(bool enabled)
        {
            Enabled = enabled;
            return this;
        }

        #endregion
    }
}
