using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Merlin.M2D.ECS.Components.Sprites
{
    // From https://docs.microsoft.com/en-us/xamarin/graphics-games/monogame/introduction/part2#creating-the-animation-class
    public class DynamicAnimation : SimpleSprite
    {
        #region <<Fields>>

        private readonly List<DynamicAnimationFrame> _frames = new List<DynamicAnimationFrame>();
        private int _currentFrameIndex;
        private TimeSpan _timeIntoAnimation;
        private bool _lastFrame;

        #endregion

        #region <<Properties>>

        public TimeSpan TotalDuration
        {
            get
            {
                double totalSeconds = _frames
                    .Select(f => f.Duration.TotalSeconds)
                    .Sum();

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public bool StopAtLastFrame { get; }

        public override Rectangle Bounds
        {
            get
            {
                Rectangle source = CurrentFrame.SourceRectangle;
                return new Rectangle(
                        x: 0, y: 0,
                        width: (int)(source.Width * Scale),
                        height: (int)(source.Height * Scale)
                    );
            }
        }

        public override Rectangle? SourceRectangle => CurrentFrame.SourceRectangle;

        private DynamicAnimationFrame CurrentFrame
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _frames[_currentFrameIndex];
        }

        #endregion

        public DynamicAnimation(Texture2D texture, float scale = 1, bool stopAtLastFrame = false)
            : base(texture, scale)
        {
            StopAtLastFrame = stopAtLastFrame;
        }

        #region <<Methods>>

        public void AddFrame(Rectangle sourceRectangle, TimeSpan duration)
        {
            var newFrame = new DynamicAnimationFrame
            {
                SourceRectangle = sourceRectangle,
                Duration = duration
            };

            _frames.Add(newFrame);
        }

        public override void Update(GameTime gameTime)
        {
            if(_lastFrame)  // Only set if StopAtLastFrame is activated
                return;

            _timeIntoAnimation += gameTime.ElapsedGameTime;

            if (_timeIntoAnimation > CurrentFrame.Duration)
            {
                _currentFrameIndex++;
                _timeIntoAnimation = TimeSpan.FromSeconds(0);
            }

            if (_currentFrameIndex == _frames.Count)
            {
                if (StopAtLastFrame)
                {
                    _lastFrame = true;
                    // Currently this index is out of the list
                    // after -- it points to the last element of the frames list
                    _currentFrameIndex--;
                }
                else
                {
                    _currentFrameIndex = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle destinationRectangle = Bounds;
            destinationRectangle.X = (int) position.X;
            destinationRectangle.Y = (int) position.Y;

            spriteBatch.Draw(
                    texture: Texture,
                    destinationRectangle: destinationRectangle,
                    sourceRectangle: CurrentFrame.SourceRectangle,
                    color: Color.White
                );
        }

        #endregion

        #region <<Inner Types>>

        public struct DynamicAnimationFrame
        {
            public Rectangle SourceRectangle;
            public TimeSpan Duration;
        }

        #endregion
    }
}
