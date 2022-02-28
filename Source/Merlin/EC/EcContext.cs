using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.EC;

/// <summary>
/// 
/// </summary>
public class EcContext : DrawableGameComponent
{
    private readonly List<Entity> _entities = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="game"></param>
    public EcContext(Game game, int drawOrder = 0, bool visible = true) : base(game)
    {
        DrawOrder = drawOrder;
        Visible = visible;
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();
        _entities.Add(entity);
        return entity;
    }

    /// <inheritdoc />
    public override void Update(GameTime gameTime)
    {
        foreach (var entity in _entities)
        {
            entity.Update();
        }
    }

    /// <inheritdoc />
    public override void Draw(GameTime gameTime)
    {
        foreach (var entity in _entities)
        {
            entity.Draw();
        }
    }
}
