using System;
using Microsoft.Xna.Framework;

namespace Merlin
{
    public abstract class Core : Game
    {
        protected GraphicsDeviceManager GraphicsDeviceManager { get; set; }

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
        }
    }
}
