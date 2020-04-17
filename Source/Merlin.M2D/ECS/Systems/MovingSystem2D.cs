using System;
using System.Linq;
using Merlin.ECS;
using Merlin.M2D.ECS.Components.Positioning;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Systems
{
    public class MovingSystem2D : UpdateSystem
    {
        public override void Update(GameTime gameTime)
        {
            var movableEntities = World.ActiveEntities
                .Where(e => e.HasComponent<Position2D>() && e.HasComponent<NewMoving2D>());

            foreach (var e in movableEntities)
            {
                var moving = e.GetComponent<NewMoving2D>();
                moving.Update(gameTime);

                var position = e.GetComponent<Position2D>();
                position.Position += moving.Velocity;
                
                // Console.WriteLine($"Position: {position.Position}");
            }
        }
    }
}
