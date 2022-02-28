using System.Collections.Generic;
using Merlin.ECS;
using Merlin.ECS.SystemLifecycle;
using Merlin.M2D.ECS.Positioning;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Rendering
{
    public class SpriteRenderSystem : IDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;

        public IEntityFilter Filter { get; } = Entity.That.MustHave(nameof(Sprite), nameof(Position2D));

        public SpriteRenderSystem(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Update(List<IEntity> entities) { }

        public void Draw(List<IEntity> entities)
        {
            foreach (var e in entities)
            {
                e.GetComponent<Sprite>().Draw(_spriteBatch, e.GetComponent<Position2D>().ToVector2());
            }
        }
    }
}
