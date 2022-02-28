using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Merlin.EC;
using System;

namespace Merlin.M2D.EC;

public class Moving2D : BaseComponent, IUpdatable
{
    public int MaxSpeed { get; }
    public float Acceleration { get; }
    public float Friction { get; }

    public Vector2 Velocity { get; internal set; }
    public bool IsMoving { get; internal set; }
    public Vector2 Direction { get; set; }

    public Moving2D(int maxSpeed, float acceleration = 0.3f, float friction = 0.1f)
    {
        MaxSpeed = maxSpeed;
        Acceleration = acceleration;
        Friction = friction;
    }

    public Moving2D WithDirection(Vector2 direction)
    {
        Direction = direction;
        return this;
    }

    public void Update()
    {
        var position = Entity.GetComponent<Position2D>() ?? throw new AccessViolationException();

        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            IsMoving = true;
            Direction = new Vector2(1, 0);
        }
        else
        {
            IsMoving = false;
            Direction = Vector2.Zero;
        }

        if (IsMoving)
        {
            //var velocity = moving.Direction * moving.Acceleration; // Todo add GameTime
            //velocity.X = velocity.X < 0 ? Math.Max(velocity.X, -moving.MaxSpeed) : Math.Min(velocity.X, moving.MaxSpeed);
            //velocity.Y = velocity.Y < 0 ? Math.Max(velocity.Y, -moving.MaxSpeed) : Math.Min(velocity.Y, moving.MaxSpeed);
            //moving.Velocity = velocity;

            Velocity = Vector2.Lerp(
                Velocity,
                Direction * MaxSpeed,
                amount: Acceleration * (float)Time.DeltaTime.TotalSeconds
            );
        }
        else
        {
            //var velocity = moving.Velocity * moving.Friction; // TODO: add GameTime

            //if (Math.Abs(velocity.X) < 0.001)
            //    velocity.X = 0;

            //if (Math.Abs(velocity.Y) < 0.001)
            //    velocity.Y = 0;

            //moving.Velocity = velocity;

            Velocity = Vector2.Lerp(
                Velocity,
                Vector2.Zero,
                amount: Friction * (float)Time.DeltaTime.TotalSeconds
            );
        }

        position.X += Velocity.X;
        position.Y += Velocity.Y;
    }
}
