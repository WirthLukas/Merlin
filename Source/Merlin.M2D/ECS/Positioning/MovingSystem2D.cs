﻿using Merlin.ECS;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Merlin.M2D.ECS.Positioning
{
    public class MovingSystem2D : ISystem
    {
        public IEntityFilter Filter { get; } = Entity.That.MustHave(nameof(Moving2D), nameof(Position2D));

        public void Update(List<IEntity> entities)
        {
            foreach (var e in entities)
            {
                var moving = e.GetComponent<Moving2D>();
                var position = e.GetComponent<Position2D>();

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    moving.IsMoving = true;
                    moving.Direction = new Vector2(1, 0);
                }
                else
                {
                    moving.IsMoving = false;
                    moving.Direction = Vector2.Zero;
                }

                if (moving.IsMoving)
                {
                    //var velocity = moving.Direction * moving.Acceleration; // Todo add GameTime
                    //velocity.X = velocity.X < 0 ? Math.Max(velocity.X, -moving.MaxSpeed) : Math.Min(velocity.X, moving.MaxSpeed);
                    //velocity.Y = velocity.Y < 0 ? Math.Max(velocity.Y, -moving.MaxSpeed) : Math.Min(velocity.Y, moving.MaxSpeed);
                    //moving.Velocity = velocity;

                    moving.Velocity = Vector2.Lerp(
                        moving.Velocity,
                        moving.Direction * moving.MaxSpeed,
                        amount: moving.Acceleration * (float)Time.DeltaTime.TotalSeconds
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

                    moving.Velocity = Vector2.Lerp(
                        moving.Velocity,
                        Vector2.Zero,
                        amount: moving.Friction * (float)Time.DeltaTime.TotalSeconds
                    );
                }

                position.X += moving.Velocity.X;
                position.Y += moving.Velocity.Y;
            }
        }
    }
}
