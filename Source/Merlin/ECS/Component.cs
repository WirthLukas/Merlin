using System;
using System.Linq;
using Merlin.ECS.Attributes;
using Microsoft.Xna.Framework;

namespace Merlin.ECS
{
    public abstract class Component : IUpdateable, IComparable<Component>, IComparable
    {
        #region <<Fields>>

        private int _updateOrder;
        private bool _isEnabled;
        private Entity _entity;

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
        public Entity Entity
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

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        #region <<Entity based Methods>>

        /// <summary>
        /// Adds this Component to the given entity and sets this 
        /// to enabled
        /// </summary>
        /// <param name="entity"></param>
        internal virtual void AddToEntity(Entity entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity), "Cannot add to null");
            OnAddedToEntity();
            Enabled = true;
        }

        /// <summary>
        /// Removes this component from the related entity
        /// </summary>
        internal virtual void RemoveFromEntity()
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
        /// TODO: Description for Init
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

        public int CompareTo(Component other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other), "Not comparable with null");

            return _updateOrder.CompareTo(other.UpdateOrder);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Not comparable with null");

            if (!(obj is Component other))
                throw new InvalidOperationException("Can only compare with other Components");

            return CompareTo(other);
        }

        #endregion

        #region <<Fluent Methods>>

        public Component WithUpdateOrder(int updateOrder)
        {
            _updateOrder = updateOrder;
            return this;
        }

        public Component IsEnabled(bool enabled)
        {
            Enabled = enabled;
            return this;
        }

        #endregion

        internal static Type[] RequiredComponentsOf(Component c)
        {
            var attributes = Attribute.GetCustomAttributes(c.GetType());

            var requiredComponents = attributes
                .OfType<RequiredComponentAttribute>()
                .Select(attr => attr.ComponentType)
                .ToArray();

            return requiredComponents;
        }

        internal static bool IsCoreComponent(Type c)
        {
            var attributes = Attribute.GetCustomAttributes(c);

            return attributes
                .OfType<CoreComponentAttribute>()
                .Any();
        }
    }
}
