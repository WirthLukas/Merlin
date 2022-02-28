using Microsoft.Xna.Framework;

namespace Merlin.M2D.EC;

public class Position2D
{
    public float X { get; set; }
    public float Y { get; set; }

    public Position2D(float x = 0, float y = 0)
    {
        X = x;
        Y = y;
    }

    public Position2D(Vector2 position)
    {
        (X, Y) = position;
    }

    public Vector2 ToVector2() => new Vector2(X, Y);

    public static implicit operator Vector2(Position2D position) => position.ToVector2();
    public static explicit operator Position2D(Vector2 vector) => new Position2D(vector); 
}
