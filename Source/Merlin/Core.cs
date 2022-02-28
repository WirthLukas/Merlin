using System;
using Microsoft.Xna.Framework;

namespace Merlin
{
    public class Core : Game
    {
        protected GraphicsDeviceManager GraphicsDeviceManager;
        // protected readonly ScreenManager ScreenManager;

        protected Core(int width = 1280, int height = 720, bool isFullScreen = false,
            string contentDirectory = "Content")
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height,
                IsFullScreen = isFullScreen
            };

            Content.RootDirectory = contentDirectory ?? throw new ArgumentNullException(nameof(contentDirectory));

            // ScreenManager = new ScreenManager(this);
            // Components.Add(ScreenManager);
        }

        // TODO: Add AfterLoadContent Method?

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Time.GameTime = gameTime;
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Time.GameTime = gameTime;
        }
    }
}
