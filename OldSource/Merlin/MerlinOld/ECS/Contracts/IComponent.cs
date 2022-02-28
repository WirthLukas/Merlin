using System;
using Microsoft.Xna.Framework;

namespace Merlin.ECS.Contracts
{
    public interface IComponent: IUpdateable, IComparable<IComponent>, IComparable
    {
        
    }
}