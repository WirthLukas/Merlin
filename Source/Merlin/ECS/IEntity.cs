
namespace Merlin.ECS
{
    public interface IEntity
    {
        uint Id { get; }

        IEntity AddComponent<T>(ref T component);
        IEntity RemoveComponent<T>(out T component);
        ref T GetComponent<T>();
        bool TryGetComponent<T>(out T? component);
        bool HasComponent<T>();

        void Destroy();
    }

    //public interface IEntity : ITypeComponentManager, INameComponentManager
    //{
    //    uint Id { get; }

    //    void Destroy();
    //}

    //public interface ITypeComponentManager
    //{
    //    T AddComponent<T>() where T : IComponent, new();
    //    T AddComponent<T>(T component) where T : IComponent;
    //    T RemoveComponent<T>() where T : IComponent;
    //    T GetComponent<T>() where T : IComponent;
    //    T? GetComponentWithInherited<T>() where T : class, IComponent;
    //    bool HasComponent<T>() where T : IComponent;
    //}

    //public interface INameComponentManager
    //{
    //    T AddComponent<T>(string name, T component) where T : IComponent;
    //    T RemoveComponent<T>(string name) where T : IComponent;
    //    T GetComponent<T>(string name) where T : IComponent;
    //    bool HasComponent(string name);
    //}
}
