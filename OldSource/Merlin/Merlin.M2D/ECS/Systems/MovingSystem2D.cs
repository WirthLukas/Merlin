using System;
using System.Linq;

using Merlin.ECS;
using Merlin.M2D.ECS.Components;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Systems
{
    // Expample System
    public class MovingSystem2D : UpdateSystem
    {
        public override void Update(GameTime gameTime)
        {
            var movableEntities = World.ActiveEntities
                .Where(e => e.HasComponent<Transform2D>() && e.HasComponent<Moving2D>());

            foreach (var e in movableEntities)
            {
                var moving = e.GetComponent<Moving2D>();
                moving.OnUpdate(gameTime);

                var position = e.GetComponent<Transform2D>();
                position.Position += moving.Velocity;
            }
        }
    }
}
