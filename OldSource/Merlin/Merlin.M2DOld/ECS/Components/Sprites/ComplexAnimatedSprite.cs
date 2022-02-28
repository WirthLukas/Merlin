using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Components.Sprites
{
    public class ComplexAnimatedSprite : SimpleSprite
    {
        public override Rectangle Bounds { get; }

        public ComplexAnimatedSprite(Texture2D texture, float scale = 1)
            : base(texture, scale)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debug.WriteLine("do nothing ...");
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            base.Draw(spriteBatch, position);
            Debug.WriteLine("do nothing...");
        }
    }
}