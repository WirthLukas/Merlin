using System;
using System.Runtime.CompilerServices;
using Merlin.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Systems
{
    public abstract class DrawSystem2D : DrawSystem
    {
        protected SpriteBatch SpriteBatch
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set;
        }

        protected DrawSystem2D(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch ?? throw new ArgumentNullException(nameof(spriteBatch));
        }
    }
}
