namespace Merlin.ECS.Contracts
{
    /// <summary>
    /// Mark a class as a System.
    /// Systems inflate the behaviour of an entity
    /// if the entity has the components, which are needed by the system
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Initializes the members of the system.
        /// Gets called by the <see cref="World"/> class, when
        /// this system is added
        /// </summary>
        /// <param name="world">the world, where this system is added</param>
        void Initialize(World world);
    }
}
