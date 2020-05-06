using System;
using System.Linq;
using Merlin.ECS;
using Merlin.M2D.ECS.Components.Positioning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceAdventure.Systems
{
    public class InputSystem : UpdateSystem
    {
        public override void Update(GameTime gameTime)
        {
            var movableEntities = World.ActiveEntities
                .Where(e => e.HasComponent<NewMoving2D>());
            
            var kbState = Keyboard.GetState();
            var inputVector = new Vector2();

            if (kbState.IsKeyDown(Keys.D))
                inputVector.X = 1;
            else if (kbState.IsKeyDown(Keys.A))
                inputVector.X = -1;
            
            if (kbState.IsKeyDown(Keys.W))
                inputVector.Y = -1;
            else if (kbState.IsKeyDown(Keys.S))
                inputVector.Y = 1;
            
            foreach (var e in movableEntities)
            {
                var move = e.GetComponent<NewMoving2D>() ??
                           throw new AccessViolationException($"Entity has no {nameof(NewMoving2D)} Component");

                if (move.Moving && inputVector == Vector2.One)
                    move.StopMoving();

                if (!move.Moving && inputVector != Vector2.Zero)
                    move.StartMoving();

                move.Direction = inputVector;
                move.Update(gameTime);
            }
            
        }
    }
}