using Merlin.EC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.Tests.EC;

[TestClass]
public class EntityTests
{
    [TestMethod]
    public void Test_AddComponentToEntity()
    {
        var entity = new Entity()
            .AddComponent<Position>()
            .Entity;

        entity.AddComponent<Mover>();
        entity.AddComponent<Sprite>();

        Assert.IsNotNull(entity.GetComponent<Position>());
        Assert.IsNotNull(entity.GetComponent<Mover>());
        Assert.IsNotNull(entity.GetComponent<Sprite>());

        Assert.IsNotNull(entity.GetComponent<Position>()?.Entity);

        entity.Update();
        Assert.AreEqual(10, entity.GetComponent<Mover>()?.TestValue);
        Assert.AreEqual(0, entity.GetComponent<Sprite>()?.TestValue);

        entity.Draw();
        Assert.AreEqual(10, entity.GetComponent<Mover>()?.TestValue);
        Assert.AreEqual(10, entity.GetComponent<Sprite>()?.TestValue);
    }

    [TestMethod]
    public void Test_RemoveComponentFromEntity()
    {
        var entity = new Entity()
            .AddComponent<Position>()
            .Entity;

        entity.AddComponent<Mover>();
        entity.AddComponent<Sprite>();

        entity.Update();
        entity.Draw();

        var comp = entity.RemoveComponent<Position>();
        Assert.IsNotNull(comp);
        Assert.IsNull(entity.GetComponent<Position>());

        var comp2 = entity.RemoveComponent<Mover>();
        Assert.IsNotNull(comp2);
        Assert.IsNull(entity.GetComponent<Mover>());
        comp2.TestValue = 0;
        entity.Update();
        Assert.AreEqual(0, comp2.TestValue);
        
        var comp3 = entity.RemoveComponent<Sprite>();
        Assert.IsNotNull(comp3);
        Assert.IsNull(entity.GetComponent<Sprite>());
        comp3.TestValue = 0;
        entity.Draw();
        Assert.AreEqual(0, comp3.TestValue);
    }
}

public class Position : IInitializable
{
    public int X { get; set; }
    public int Y { get; set; }

    public Entity Entity { get; set; }

    public void Initialize(Entity entity)
    {
        Entity = entity;
        (X, Y) = (10, 20);
    }
}

public class Mover : IUpdatable
{
    public int TestValue = 0;

    public void Update()
    {
        TestValue = 10;
    }
}

public class Sprite : IDrawable
{
    public int TestValue = 0;

    public void Draw()
    {
        TestValue = 10;
    }
}
