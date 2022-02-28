using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Components.Sprites
{
    public class RotatedSprite<T> : SimpleSprite where T : SimpleSprite
    {
        private readonly T _sprite;
        private readonly float _rotationSpeed;
        private float _rotation;

        public override Rectangle Bounds => _sprite.Bounds;
        public override Rectangle? SourceRectangle => _sprite.SourceRectangle;

        public RotatedSprite(T sprite, float rotationSpeed = 0.002f)
            : base(sprite.Texture, sprite.Scale)
        {
            _sprite = sprite;
            _rotationSpeed = rotationSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
            _rotation += (float)(_rotationSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        /// <summary>
        /// Draws the sprite with the given rotation
        ///
        /// Issue!! With scale the rotation doesn't work correctly
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle destination = _sprite.Bounds;
            destination.X = (int) position.X;
            destination.Y = (int) position.Y;

            spriteBatch.DrawRectangle(destination, Color.CornflowerBlue, 2);

            var origin = destination.Size.ToVector2() / 2;
            //var origin = new Vector2(destination.Width / 2f, destination.Height / 2f);   // destination is also the bound
            destination.X = (int)(position.X + origin.X);
            destination.Y = (int)(position.Y + origin.Y);

            // TODO: with scale the rotation doesn't work!
            spriteBatch.Draw(
                texture: _sprite.Texture,
                destinationRectangle: destination,
                sourceRectangle: _sprite.SourceRectangle,
                color: Color.White,
                rotation: _rotation,
                origin: origin, 
                effects: SpriteEffects.None,
                layerDepth: 0
            );

#if DEBUG
            spriteBatch.DrawRectangle(destination, Color.Red, 2);
            spriteBatch.DrawCircle(position + origin, 3, Color.Yellow, thickness: 2);
#endif
        }
    }
}
