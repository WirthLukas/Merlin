using System;
using System.Linq;
using Merlin.ECS;
using Merlin.M2D.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceAdventure.Systems
{
    public class PlayerMovingSystem : UpdateSystem
    {
        public override void Update(GameTime gameTime)
        {
            var movableEntities = World.ActiveEntities
                .Where(e => e.HasComponent<Moving2D>() && e.HasComponent<Transform2D>());
            
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
                var move = e.GetComponent<Moving2D>() ??
                           throw new AccessViolationException($"Entity has no {nameof(Moving2D)} Component");

                var transform = e.GetComponent<Transform2D>() ??
                           throw new AccessViolationException($"Entity has no {nameof(Transform2D)} Component");

                move.Direction = inputVector;
                move.OnUpdate(gameTime);

                transform.Position += move.Velocity;
            }         
        }
    }
}