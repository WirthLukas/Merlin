using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.EC;

public interface IInitializable
{
    void Initialize(Entity entity);
}

public interface IUpdatable
{
    void Update();
}

public interface IDrawable
{
    void Draw();
}
