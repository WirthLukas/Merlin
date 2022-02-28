using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.Screens
{
    public abstract class Screen : IDisposable
    {
        public string Name { get; }
        public Color ClearColor { get; set; }
        protected GraphicsDevice GraphicsDevice { get; set; }

        protected Screen(Game game, string name)
            : this (game, name, Color.CornflowerBlue)
        { }

        protected Screen(Game game, string name, Color clearColor)
        {
            Name = name;
            GraphicsDevice = game.GraphicsDevice;
            ClearColor = clearColor;
        }

        public virtual void LoadContent() { }
        
        public virtual void Initialize() { }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearColor);
        }

        public virtual void UnloadContent() { }

        #region <<Dispose Pattern>>

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        #endregion

    }
}
