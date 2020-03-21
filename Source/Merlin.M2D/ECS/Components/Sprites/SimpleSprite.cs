using Merlin.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Components.Sprites
{
    public class SimpleSprite : Component
    {
        public Texture2D Texture { get; }
        public float Scale { get; set; }

        public virtual Rectangle Bounds
        {
            get
            {
                Rectangle textureBounds = Texture.Bounds;

                textureBounds.Width = (int) (textureBounds.Width * Scale);
                textureBounds.Height = (int) (textureBounds.Height * Scale);

                return textureBounds;

                /*return new Rectangle(
                    textureBounds.X, textureBounds.Y,
                    (int)(textureBounds.Width * Scale),
                    (int)(textureBounds.Height * Scale)
                );*/
            }
        }

        public SimpleSprite(Texture2D texture, float scale = 1)
        {
            Texture = texture;
            Scale = scale;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle destination = Bounds;
            destination.X = (int)position.X;
            destination.Y = (int)position.Y;

            spriteBatch.Draw(
                texture: Texture,
                destinationRectangle: destination,
                sourceRectangle: null,
                color: Color.White
            );
        }
    }
}
