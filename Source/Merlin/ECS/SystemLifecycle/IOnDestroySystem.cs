
namespace Merlin.ECS.SystemLifecycle
{
    public interface IOnDestroySystem : ISystem
    {
        void OnDestroy();
    }
}
