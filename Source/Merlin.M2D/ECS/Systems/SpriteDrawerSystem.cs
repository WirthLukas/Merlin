using System;
using System.Collections.Generic;
using System.Linq;
using Merlin.ECS;
using Merlin.ECS.Contracts;
using Merlin.M2D.ECS.Components.Positioning;
using Merlin.M2D.ECS.Components.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Systems
{
    public class SpriteDrawerSystem : UpdateSystem, IDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private List<Entity> _entities = new List<Entity>();
        private bool _visible = true;
        private int _drawOrder = 1;

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
                if (value != _drawOrder)
                {
                    _drawOrder = value;
                    DrawOrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;


        public SpriteDrawerSystem(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (var e in _entities)
                e.GetComponent<SimpleSprite>(true).Draw(_spriteBatch, e.GetComponent<Position2D>().Position);
        }

        public override void Update(GameTime gameTime)
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
