using Merlin.ECS.Contracts;

namespace Merlin.ECS.Lifecycle
{
    public interface IOnAttachment
    {
        void OnAttach(IEntity entity);
    }

    public interface IOnRemovement
    {
        void OnRemove(IEntity entity);
    }
}
