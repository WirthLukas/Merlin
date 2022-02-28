using Merlin.EC;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Merlin.M2D.EC
{
    public class Sprite : BaseComponent, Merlin.EC.IDrawable
    {
        private readonly SpriteBatch _spriteBatch;

        public Texture2D Texture { get; set; }
        public float Scale { get; set; }

        public virtual Rectangle Bounds
        {
            get
            {
                var textureBounds = Texture.Bounds;
                textureBounds.Width = (int)(textureBounds.Width * Scale);
                textureBounds.Height = (int)(textureBounds.Height * Scale);
                return textureBounds;
            }
        }

        public virtual Rectangle? SourceRectangle => null;

        public Sprite(Texture2D texture, SpriteBatch spriteBatch, float scale = 1)
        {
            Texture = texture;
            _spriteBatch = spriteBatch;
            Scale = scale;
        }

        public void Draw()
        {
            var position = Entity.GetComponent<Position2D>() ?? throw new AccessViolationException();
            var destination = Bounds;
            destination.X = (int)position.X;
            destination.Y = (int)position.Y;

            _spriteBatch.Draw(
                texture: Texture,
                destinationRectangle: destination,
                sourceRectangle: SourceRectangle,
                color: Color.White
            );
        }
    }
}
