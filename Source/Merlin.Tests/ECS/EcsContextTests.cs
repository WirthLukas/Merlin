//using Merlin.ECS;
//using Merlin.ECS.SystemLifecycle;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Linq;

//namespace Merlin.Tests.ECS
//{
//    [TestClass]
//    public class EcsContextTests
//    {
//        [TestMethod]
//        public void Test()
//        {
//            var drawSystem = new DrawSystem();

//            var context = new EcsContext();
//            context.AddSystem(drawSystem);

//            //var entity = new Entity();
//            //entity.AddComponent(new Position());
//            //entity.AddComponent(new Movement());
//            //entity.AddComponent(new Sprite());

//            //var entity = new Entity()
//            //    .AddComponent(new Position()).Entity
//            //    .AddComponent(new Movement()).Entity
//            //    .AddComponent(new Sprite()).Entity;

//            context.AddEntity(entity);
//            context.Update();
//            context.Draw();

//            Assert.IsTrue(drawSystem.UpdateCalledCorrectly);
//            Assert.IsTrue(drawSystem.DrawCalledCorrectly);
//        }

//        [TestMethod]
//        public void RemoveTest()
//        {
//            var drawSystem = new DrawSystem();
//            var context = new EcsContext();
//            context.AddSystem(drawSystem);

//            var entity = new Entity()
//                .AddComponent(new Position()).Entity
//                .AddComponent(new Movement()).Entity
//                .AddComponent(new Sprite()).Entity;

//            context.AddEntity(entity);
//            context.RemoveSystem(drawSystem);

//            context.Update();
//            context.Draw();

//            Assert.IsFalse(drawSystem.UpdateCalledCorrectly);
//            Assert.IsFalse(drawSystem.DrawCalledCorrectly);
//        }

//        [TestMethod]
//        public void EntityRemoveTest()
//        {
//            var drawSystem = new DrawSystem();
//            var context = new EcsContext();
//            context.AddSystem(drawSystem);

//            var entity = new Entity()
//                .AddComponent(new Position()).Entity
//                .AddComponent(new Movement()).Entity
//                .AddComponent(new Sprite()).Entity;

//            context.AddEntity(entity);

//            context.Update();
//            context.Draw();

//            Assert.IsTrue(drawSystem.UpdateCalledCorrectly);
//            Assert.IsTrue(drawSystem.DrawCalledCorrectly);

//            context.DestroyEntity(entity);

//            context.Update();
//            context.Draw();

//            Assert.IsFalse(drawSystem.UpdateCalledCorrectly);
//            Assert.IsFalse(drawSystem.DrawCalledCorrectly);
//        }

//        internal class DrawSystem : IDrawSystem
//        {
//            public IEntityFilter Filter => Entity.That.MustHave(nameof(Position), nameof(Sprite));

//            public bool DrawCalledCorrectly { get; set; }
//            public bool UpdateCalledCorrectly { get; set; }

//            public void Update(List<IEntity> entities)
//            {
//                UpdateCalledCorrectly = entities.Count is 1;
//            }

//            public void Draw(List<IEntity> entities)
//            {
//                DrawCalledCorrectly = entities.Count is 1;
//            }
//        }
//    }
//}
