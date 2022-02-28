using Merlin.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Rendering
{
    public class Sprite : IComponent
    {
        public Texture2D Texture { get; set; }
        public float Scale { get; set; }

        public IEntity? Entity { get; set; }

        public virtual Rectangle Bounds
        {
            get
            {
                var textureBounds = Texture.Bounds;
                textureBounds.Width = (int) (textureBounds.Width * Scale);
                textureBounds.Height = (int)(textureBounds.Height * Scale);
                return textureBounds;
            }
        }

        public virtual Rectangle? SourceRectangle => null;

        public Sprite(Texture2D texture, float scale = 1)
        {
            Texture = texture;
            Scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            var destination = Bounds;
            destination.X = (int)position.X;
            destination.Y = (int) position.Y;

            spriteBatch.Draw(
                texture: Texture,
                destinationRectangle: destination,
                sourceRectangle: SourceRectangle,
                color: Color.White
            );
        }
    }
}
