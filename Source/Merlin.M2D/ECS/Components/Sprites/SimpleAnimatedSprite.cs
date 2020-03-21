using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D.ECS.Components.Sprites
{
    // Based on http://rbwhitaker.wikidot.com/monogame-texture-atlases-2
    /// <summary>
    /// 
    /// </summary>
    public class SimpleAnimatedSprite : SimpleSprite
    {
        #region <<Fields>>

        private readonly int _totalFrames;
        private int _currentFrame;
        private TimeSpan _animationTimer;
        private bool _lastFrame;

        #endregion

        #region <<Properties>>

        public int FrameWidth { get; }
        public int FrameHeight { get; }
        public int Rows { get; }
        public int Columns { get; }
        public TimeSpan FrameDuration { get; set; }
        public bool StopAtLastFrame { get; set; }

        public override Rectangle Bounds
        {
            get
            {
                Rectangle bounds = Texture.Bounds;
                // divide width and height of texture into width and height of one frame
                // and expand them by scale
                // TODO: Use FrameWidth ?
                bounds.Width = (int)((float)Texture.Width / Columns * Scale);
                bounds.Height = (int)((float)Texture.Height / Rows * Scale);
                return bounds;
            }
        }

        #endregion


        public SimpleAnimatedSprite(Texture2D texture, int rows, int cols,
            bool stopAtLastFrame = false, float scale = 1)
            : this(texture, rows, cols, TimeSpan.FromSeconds(1), stopAtLastFrame, scale)
        {
        }

        public SimpleAnimatedSprite(Texture2D texture, int rows, int cols,
            TimeSpan frameDuration, bool stopAtLastFrame = false, float scale = 1)
            : base(texture, scale)
        {
            Rows = rows;
            Columns = cols;
            FrameDuration = frameDuration;
            StopAtLastFrame = stopAtLastFrame;

            FrameWidth = Texture.Width / Columns;    // width of every single frame 
            FrameHeight = Texture.Height / Rows;     // height of every single frame

            _totalFrames = Rows * Columns;
            _animationTimer = TimeSpan.FromSeconds(0);
        }

        #region <<Methods>>

        public override void Update(GameTime gameTime)
        {
            if (_lastFrame && StopAtLastFrame)
                return;
            
            // Increase Timer
            _animationTimer += gameTime.ElapsedGameTime;

            if (_animationTimer >= FrameDuration)
            {
                //Reset Timer
                _animationTimer = TimeSpan.FromSeconds(0);

                _currentFrame++;

                if (_currentFrame == _totalFrames)
                {
                    if (!StopAtLastFrame)
                        _currentFrame = 0;
                    else
                        _lastFrame = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            //int width = Texture.Width / Columns;    // width of every single frame 
            //int height = Texture.Height / Rows;     // height of every single frame
            int row = (int)((float)_currentFrame / Columns);
            int col = _currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(
                x: FrameWidth * col,
                y: FrameHeight * row,
                width: FrameWidth,
                height: FrameHeight
            );

            Rectangle destinationRectangle = new Rectangle(
                x: (int)position.X,
                y: (int)position.Y,
                width: (int)(FrameWidth * Scale),
                height: (int)(FrameHeight * Scale)
            );

            spriteBatch.Draw(
                texture: Texture,
                destinationRectangle: destinationRectangle,
                sourceRectangle: sourceRectangle,
                color: Color.White
            );
        }

        #endregion
    }
}
