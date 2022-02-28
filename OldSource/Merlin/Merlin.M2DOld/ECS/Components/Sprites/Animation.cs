using System;

namespace Merlin.M2D.ECS.Components.Sprites
{
    public class Animation
    {
        public readonly string Name;
        public readonly int AnimationRow;
        public readonly int FrameCount;
        public readonly int FrameSizeX;
        public readonly int FrameSizeY;
        public float SpeedFactor;
        public bool StopAtLastFrame;

        public Animation(string name = "",  int animationRow = 0, int frameCount = 1, int frameSizeX = 32,
            int frameSizeY = 32, float speedFactor = 1, bool stopAtLastFrame = false)
        {
            Name = string.IsNullOrEmpty(name) ? Utils.RandomString(20) : name;
            FrameCount = frameCount;
            AnimationRow = animationRow;
            FrameSizeX = frameSizeX;
            FrameSizeY = frameSizeY;
            SpeedFactor = speedFactor;
            StopAtLastFrame = stopAtLastFrame;
        }

        // public Animation(string name, int animationRow = 0, int frameCount = 1, int frameSize = 32,
        //     float speedFactor = 1, bool stopAtLastFrame = false)
        //     : this(name, animationRow, frameCount, frameSize, frameSize, speedFactor, stopAtLastFrame)
        // { }
    }
}