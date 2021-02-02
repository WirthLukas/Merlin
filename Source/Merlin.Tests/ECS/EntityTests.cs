using Merlin.ECS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.Tests.ECS
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void Test()
        {
            var entity = new Entity();
            entity.AddComponent(new Position());

            Assert.IsTrue(entity.HasComponent<Position>());
            Assert.IsNotNull(entity.GetComponent<Position>());
        }
    }
}
