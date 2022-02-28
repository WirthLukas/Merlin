using Microsoft.Xna.Framework;

namespace Merlin.Extensions
{
    public static class Vector2Extensions
    {
        private const double Epsilon = 1e-06;

        // From https://github.com/godotengine/godot/blob/master/modules/mono/glue/GodotSharp/GodotSharp/Core/Vector2.cs
        public static Vector2 MoveTowards(this Vector2 @this, Vector2 to, float delta)
        {
            Vector2 difference = to - @this;
            var length = difference.Length();
            return length <= delta || length < Epsilon ? to : @this + difference / length * delta;
        }
    }
}
