//using Merlin.ECS;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Merlin.Tests.ECS
//{
//    [TestClass]
//    public class EntityFilterTests
//    {
//        [TestMethod]
//        public void Test_FilterOnEntity()
//        {
//            var filter = Entity.That
//                .MustHave("Position", "Movement")
//                .MustNotHave("Sprite");

//            var entity1 = new Entity();
//            entity1.AddComponent(new Position());
//            entity1.AddComponent(new Movement());

//            var entity2 = new Entity();
//            entity2.AddComponent(new Position());

//            var entity3 = new Entity();
//            entity3.AddComponent(new Position());
//            entity3.AddComponent(new Movement());
//            entity3.AddComponent(new Sprite());

//            Assert.IsTrue(filter.Check(entity1));
//            Assert.IsFalse(filter.Check(entity2));
//            Assert.IsFalse(filter.Check(entity3));
//        }
//    }
//}
