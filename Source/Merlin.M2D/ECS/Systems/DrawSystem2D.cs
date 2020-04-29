using System;
using Merlin.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Systems
{
    public abstract class DrawSystem2D : DrawSystem
    {
        protected readonly SpriteBatch SpriteBatch;

        protected DrawSystem2D(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));
        }
    }
}
