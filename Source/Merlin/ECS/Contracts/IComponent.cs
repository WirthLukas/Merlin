using System;
using Microsoft.Xna.Framework;

namespace Merlin.ECS.Contracts
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity">Your implementation of the IEntity interface</typeparam>
    /// <typeparam name="TImpl">The implementing class of this interface</typeparam>
    public interface IComponent: IUpdateable, IComparable<IComponent>, IComparable
    {
        
    }
}