using System;
using System.Collections.Generic;
using System.Text;

namespace Merlin.ECS
{
    public interface IComponent
    {
        IEntity? Entity { get; set; }
    }
}
