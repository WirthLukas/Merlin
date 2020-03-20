using System.Linq;
using Merlin.ECS;
using Merlin.M2D.ECS.Components.Positioning;
using Microsoft.Xna.Framework;

namespace Merlin.M2D.ECS.Systems
{
    public class MovingSystem2D : BaseUpdateSystem
    {
        public override void Update(GameTime gameTime)
        {
            var movableEntities = World.ActiveEntities
                .Where(e => e.HasComponent<Position2D>() && e.HasComponent<Moving2D>());

            foreach (var e in movableEntities)
            {
                e.GetComponent<Position2D>().Position += e.GetComponent<Moving2D>().VelocityVector;
            }
        }
    }
}
