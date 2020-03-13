using System;
using System.Linq;
using Merlin.ECS.Attributes;
using Merlin.ECS.Contracts;

namespace Merlin.ECS
{
    public abstract class Component /*: IComparable<Component>, IComparable*/
    {
        private bool _isEnabled;
        private IEntity _entity;
        // private int _updateOrder;

        #region <<Properties>>

        public IEntity Entity
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

        public bool Enabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                
                if (_isEnabled)
                    OnEnabled();
                else
                    OnDisabled();
            }
        }

        //public int UpdateOrder
        //{
        //    get => _updateOrder;
        //    set
        //    {
        //        _updateOrder = value;
        //        OnUpdateOrderChanged(_updateOrder);
        //    }
        //}

        #endregion

        #region <<Entity based Methods>>

        /// <summary>
        /// Adds this Component to the given entity and sets this 
        /// to enabled
        /// </summary>
        /// <param name="entity"></param>
        internal virtual void AddToEntity(IEntity entity)
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
        /// TODO 02 Find good description for Initialize
        /// </summary>
        public virtual void Initialize()
        { }

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

        /*
        /// <summary>
        /// Behavior after the update order is changed.
        /// This Method Is for Class intern or subclasses' handling.
        /// The Event is for other classes
        /// </summary>
        /// <param name="newUpdateOrder"></param>
        protected virtual void OnUpdateOrderChanged(int newUpdateOrder) { }
        */

        #endregion

        #region <<Interface Implementations>>

        //public int CompareTo(Component other)
        //{
        //    if (other == null)
        //        throw new ArgumentNullException(nameof(other), "Not comparable with null");

        //    return _updateOrder.CompareTo(other.UpdateOrder);
        //}

        //public int CompareTo(object obj)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException(nameof(obj), "Not comparable with null");

        //    if (!(obj is Component other))
        //        throw new InvalidOperationException("Can only compare with other Components");

        //    return CompareTo(other);
        //}

        #endregion

        #region <<Fluent Methods>>

        //public Component WithUpdateOrder(int updateOrder)
        //{
        //    _updateOrder = updateOrder;
        //    return this;
        //}

        public Component IsEnabled(bool isEnabled)
        {
            if (_isEnabled != isEnabled)
                Enabled = isEnabled;

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
