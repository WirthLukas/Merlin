using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Merlin.Screens
{
    public abstract class Screen : IDisposable
    {
        public string Name { get; set; }
        public Color ClearColor { get; set; }
        public ScreenManager? ScreenManager { get; internal set; }
        protected GraphicsDevice GraphicsDevice { get; set; }

        public Screen(string name, GraphicsDevice graphicsDevice, Color? clearColor = null)
        {
            Name = name;
            GraphicsDevice = graphicsDevice;
            ClearColor = clearColor ?? Color.CornflowerBlue;
        }

        public virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Draw() => GraphicsDevice.Clear(ClearColor);

        public abstract void Update();

        #region <<Dispose Pattern>>

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }

        #endregion
    }
}
