using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Components.Sprites
{
    public class SimpleAnimatedSprite : SimpleSprite
    {
        public SimpleAnimatedSprite(Texture2D texture, float scale = 1)
            : base(texture, scale)
        {
        }
    }
}
