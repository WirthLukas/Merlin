
namespace Merlin.ECS
{
    public interface IEcsContext
    {
        IEcsContext AddSystem(ISystem system);
        IEcsContext RemoveSystem(ISystem system);

        void InitializeSystems();
        void Update();
        void Draw();
        void DestroySystems();

        void AddEntity(IEntity entity);
        void DestroyEntity(IEntity entity);
    }
}
