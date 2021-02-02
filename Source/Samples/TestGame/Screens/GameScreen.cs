using Merlin;
using Merlin.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TestGame.Screens
{
    public class GameScreen : Screen
    {
        private readonly SpriteBatch _spriteBatch;

        public GameScreen(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : base("Game", graphicsDevice, Color.Black)
        {
            _spriteBatch = spriteBatch;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            base.Draw();
            _spriteBatch.FillRectangle(10, 10, 100, 100, Color.Red);
            _spriteBatch.FillRectangle(100, 50, 100, 100, Color.Green);
        }
    }
}
