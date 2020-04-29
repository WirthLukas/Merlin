using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS.Contracts;
using Merlin.M2D.ECS.Components.Positioning;
using Merlin.M2D.ECS.Components.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Systems
{ 
    public class SpriteDrawerSystem : DrawSystem2D, IUpdateSystem
    {
        private bool _enabled = true;
        private int _updateOrder = 1;
        private List<IEntity> _entities = new List<IEntity>();
        
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
                if (value != _updateOrder)
                {
                    _updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        
        public SpriteDrawerSystem(SpriteBatch spriteBatch)
            : base(spriteBatch)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var e in _entities)
                e.GetComponent<SimpleSprite>(true).Draw(this.SpriteBatch, e.GetComponent<Position2D>().Position);
        }

        public void Update(GameTime gameTime)
        {
            _entities = World.ActiveEntities
                .Where(e => e.HasComponent<Position2D>())
                .Where(e => e.GetComponent<SimpleSprite>(true) != null)
                .ToList();

            foreach (var e in _entities)
                e.GetComponent<SimpleSprite>(true).Update(gameTime);
        }
    }
}
