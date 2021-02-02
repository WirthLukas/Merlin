using Merlin;
using Merlin.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TestGame.Screens
{
    public class MenuScreen : Screen
    {
        private readonly SpriteBatch _spriteBatch;

        public MenuScreen(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : base("Menu", graphicsDevice, Color.Transparent)
        {
            _spriteBatch = spriteBatch;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            base.Draw();
            _spriteBatch.FillRectangle(300, 300, 400, 400, Color.Blue);
        }
    }
}
