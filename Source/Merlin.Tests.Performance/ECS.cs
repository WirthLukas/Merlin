using BenchmarkDotNet.Attributes;
using Merlin.ECS;
using Merlin.ECS.SystemLifecycle;
using System.Collections.Generic;
using System.Linq;

namespace Merlin.Tests.Performance
{
    [AllStatisticsColumn]
    [MemoryDiagnoser]
    public class ECS
    {
        private const int N = 1_000_000;
        private readonly IEcsContext _context;
        private readonly IDrawSystem _drawSystem1;
        private readonly IDrawSystem _drawSystem2;

        public ECS()
        {
            _context = new EcsContext()
                .AddSystem(_drawSystem1 = new DrawSystemClass())
                .AddSystem(_drawSystem2 = new DrawSystemStruct());
        }

        [Benchmark]
        public void TestingClassComponents()
        {
            var entity = new Entity()
                .AddComponent<PositionClass>().Entity
                .AddComponent(new MovementClass()).Entity
                .AddComponent(new SpriteClass()).Entity;

            _context.AddEntity(entity);
            _context.Update();
            _context.Draw();
            _context.DestroyEntity(entity);
        }

        [Benchmark]
        public void TestingStructComponents()
        {
            var entity = new Entity()
                .AddComponent(new PositionStruct()).Entity
                .AddComponent(new MovementStruct()).Entity
                .AddComponent(new SpriteStruct()).Entity;

            _context.AddEntity(entity);
            _context.Update();
            _context.Draw();
            _context.DestroyEntity(entity);
        }
    }

    internal abstract class BaseDrawSystem : IDrawSystem
    {
        public abstract IEntityFilter Filter { get; }

        public bool DrawCalledCorrectly { get; set; }
        public bool UpdateCalledCorrectly { get; set; }

        public void Update(List<IEntity> entities)
        {
            UpdateCalledCorrectly = entities.Count is 1;
        }

        public void Draw(List<IEntity> entities)
        {
            DrawCalledCorrectly = entities.Count is 1;
        }
    }

    internal class DrawSystemClass : BaseDrawSystem
    {
        public override IEntityFilter Filter => Entity.That.MustHave(nameof(PositionClass), nameof(SpriteClass));
    }

    internal class DrawSystemStruct : BaseDrawSystem
    {
        public override IEntityFilter Filter => Entity.That.MustHave(nameof(PositionStruct), nameof(SpriteStruct));
    }
}
