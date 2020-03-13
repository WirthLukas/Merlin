using System;

namespace Merlin.ECS.Contracts
{
    public interface IDestroyable
    {
        bool Destroyed { get; }

        event EventHandler<EventArgs> IsDestroyedChanged;
    }
}
