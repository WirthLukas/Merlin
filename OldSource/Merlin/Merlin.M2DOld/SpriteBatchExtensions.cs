using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Merlin.M2D
{
    public static class SpriteBatchExtensions
    {
        #region <<Get Default Texture>>
        private static Texture2D _whitePixelTexture;

        private static Texture2D GetTexture(SpriteBatch spriteBatch)
        {
            if (_whitePixelTexture == null)
            {
                _whitePixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _whitePixelTexture.SetData<Color>(new[] { Color.White });

                spriteBatch.Disposing += (sender, args) =>
                {
                    _whitePixelTexture?.Dispose();
                    _whitePixelTexture = null;
                };
            }

            return _whitePixelTexture;
        }
        #endregion

        #region <<Rectangle>>
        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">the destination drawing surface</param>
        /// <param name="rectangle">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        /// <param name="layerDepth">The depth of the layer of this shape</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, float thickness = 1f, int layerDepth = 0)
        {
            Texture2D texture = GetTexture(spriteBatch);
            Vector2 topLeft = new Vector2(rectangle.X, rectangle.Y);
            Vector2 topRight = new Vector2(rectangle.X + rectangle.Width - thickness, rectangle.Y);
            Vector2 bottomLeft = new Vector2(rectangle.X, rectangle.Y + rectangle.Height - thickness);
            Vector2 horizontalScale = new Vector2(rectangle.Width, thickness);
            Vector2 verticalScale = new Vector2(thickness, rectangle.Height);

            spriteBatch.Draw(texture: texture, position: topLeft, sourceRectangle: null, color: color,
                    rotation: 0f, origin: Vector2.Zero, scale: horizontalScale, effects: SpriteEffects.None,
                    layerDepth: layerDepth
                );
            spriteBatch.Draw(texture: texture, position: topLeft, sourceRectangle: null, color: color,
                    rotation: 0f, origin: Vector2.Zero, scale: verticalScale, effects: SpriteEffects.None,
                    layerDepth: layerDepth
                );
            spriteBatch.Draw(texture: texture, position: topRight, sourceRectangle: null, color: color,
                    rotation: 0f, origin: Vector2.Zero, scale: verticalScale, effects: SpriteEffects.None,
                    layerDepth: layerDepth
                );
            spriteBatch.Draw(texture: texture, position: bottomLeft, sourceRectangle: null, color: color,
                    rotation: 0f, origin: Vector2.Zero, scale: horizontalScale, effects: SpriteEffects.None,
                    layerDepth: layerDepth
                );
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">the destination drawing surface</param>
        /// <param name="x">x coordinate of the rectangle</param>
        /// <param name="y">y coordinate of the rectangle</param>
        /// <param name="width">the width of the rectangle</param>
        /// <param name="height">the height of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        /// <param name="layerDepth">The depth of the layer of this shape</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, float x, float y, float width, float height,
            Color color, float thickness = 1f, int layerDepth = 0)
        {
            Rectangle rect = new Rectangle(
                    (int)x,
                    (int)y,
                    (int)width,
                    (int)height
                );

            DrawRectangle(spriteBatch, rect, color, thickness, layerDepth);
        }

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">the destination drawing surface</param>
        /// <param name="position">Position, where the rectangle should be drawn</param>
        /// <param name="width">the width of the rectangle</param>
        /// <param name="height">the height of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        /// <param name="layerDepth">The depth of the layer of this shape</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 position, float width, float height,
            Color color, float thickness = 1f, int layerDepth = 0)
        {
            Rectangle rect = new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    (int)width,
                    (int)height
                );

            DrawRectangle(spriteBatch, rect, color, thickness, layerDepth);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="position">Position, where the rectangle should be drawn</param>
        /// <param name="width">width of the rectangle</param>
        /// <param name="height">height of the rectangle</param>
        /// <param name="color"></param>
        /// <param name="layerDepth"></param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 position, float width,
            float height, Color color, int layerDepth = 0)
        {
            // the 1px big default Texture of this class will be scaled with that width and height 
            Vector2 scale = new Vector2(width, height);

            spriteBatch.Draw(texture: GetTexture(spriteBatch), position: position, sourceRectangle: null,
                color: color, rotation: 0f, origin: Vector2.Zero, scale: scale, effects: SpriteEffects.None,
                layerDepth: layerDepth);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rectangle">The rectangle that should be drawn</param>
        /// <param name="color"></param>
        /// <param name="layerDepth"></param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, int layerDepth = 0)
        {
            FillRectangle(spriteBatch, new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Height,
                color, layerDepth);
        }

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">x coordinate of the rectangle</param>
        /// <param name="y">y coordinate of the rectangle</param>
        /// <param name="width">width of the rectangle</param>
        /// <param name="height">height of the rectangle</param>
        /// <param name="color"></param>
        /// <param name="layerDepth"></param>
        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float width,
            float height, Color color, int layerDepth = 0)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), width, height, color, layerDepth);
        }
        #endregion

        #region <<Line>>

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x1">The X coord of the first point</param>
        /// <param name="y1">The Y coord of the first point</param>
        /// <param name="x2">The X coord of the second point</param>
        /// <param name="y2">The Y coord of the second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        /// <param name="layerDepth">The depth of the layer of this shape</param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness = 1f, int layerDepth = 0)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness, layerDepth);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        /// <param name="layerDepth">The depth of the layer of this shape</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f, int layerDepth = 0)
        {
            // calculate the distance between the two vectors
            var distance = Vector2.Distance(point1, point2);

            // calculate the angle between the two vectors
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness, layerDepth);
        }

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        /// <param name="layerDepth">The depth of the layer of this shape</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle,
            Color color, float thickness = 1f, int layerDepth = 0)
        {
            var origin = new Vector2(0, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(texture: GetTexture(spriteBatch), position: point, sourceRectangle: null,
                color: color, rotation: angle, origin: origin, scale: scale, effects: SpriteEffects.None,
                layerDepth: layerDepth);
        }

        #endregion

        #region <<Polygon>>

        public static void DrawPolygon(this SpriteBatch spriteBatch, Vector2[] vertex, Color color,
            float thickness = 1f, int layerDepth = 0)
        {
            if (vertex.Length <= 1)
                return;

            int count = vertex.Length;

            // drawing from first point to last point (point by point)
            for (int i = 0; i < count - 1; i++)
            {
                DrawLine(spriteBatch, vertex[i], vertex[i + 1], color, thickness, layerDepth);
            }

            // drawing form last point to first point (closing the shape)
            DrawLine(spriteBatch, vertex[count - 1], vertex[0], color, thickness, layerDepth);
        }
        #endregion

        #region <<Circle>>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, Color color,
            int segments = 16, float thickness = 1f, int layerDepth = 1)
        {

            Vector2[] vertex = new Vector2[segments];

            double increment = Math.PI * 2.0 / segments;
            double theta = 0.0;

            for (int i = 0; i < segments; i++)
            {
                vertex[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }

            DrawPolygon(spriteBatch, vertex, color, thickness, layerDepth);
        }

        #endregion
    }
}
