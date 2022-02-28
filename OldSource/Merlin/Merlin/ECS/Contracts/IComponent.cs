using System;

namespace Merlin.ECS.Contracts
{
    public interface IComponent
    {
        bool IsEnabled { get; }
        uint UpdateOrder { get; }
        IEntity? Entity { get; }

        event EventHandler<bool>? EnabledChanged;
        event EventHandler<uint>? UpdateOrderChanged;
    }
}
