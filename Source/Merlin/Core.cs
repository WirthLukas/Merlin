using System;
using Merlin.Screens;
using Microsoft.Xna.Framework;

namespace Merlin
{
    public class Core : Game
    {
        protected GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        protected ScreenManager ScreenManager { get; set; }

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

            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);
        }

        // TODO: Add AfterLoadContent Method?

        #region <<Lifecycle Methods>>

        

        #endregion
    }
}
