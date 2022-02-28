using Microsoft.Xna.Framework;
using System;

namespace Merlin
{
    public static class Time
    {
        public static TimeSpan DeltaTime => GameTime.ElapsedGameTime;
        public static GameTime GameTime { get; internal set;} = new GameTime();
    }
}
