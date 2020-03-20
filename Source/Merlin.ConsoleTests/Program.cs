using System;
using Merlin;
using Merlin.ECS;
using Merlin.ECS.Attributes;
using Merlin.ECS.Builders;
using Merlin.ECS.Contracts;
using Merlin.M2D.ECS.Components;
using Merlin.M2D.ECS.Components.Positioning;
using Merlin.M2D.ECS.Components.Sprites;

namespace Merlin.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            /*DateTime start = DateTime.Now;

            try
            {
                Entity e = new Entity("test")
                    .WithComponents(
                        new Position2D(),
                        new Moving2D()
                    );

                Console.WriteLine("e correct");

                Entity b = new Entity("b")
                    .WithComponents(
                        new Position2D(),
                        new Moving2D()
                    );

                Console.WriteLine("b correct");

                b.RemoveComponent<Position2D>();

                Console.WriteLine("Removing correct");
            }
            catch (Exception ex)
            {
                var dC = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(" > Error: " + ex.Message);

                Console.ForegroundColor = dC;
            }
            finally
            {
                DateTime stop = DateTime.Now;
                Console.WriteLine($"Dauer: {(stop - start).Milliseconds}");
            }*/

            Entity<> b = new Entity()
                .AddComponent(new Position2D())
                    .WithUpdateOrder(0)
                    .Entity
                .AddComponent(new Moving2D())
                    .Entity;

            Entity e = new Entity()
                .WithComponents(
                    new Position2D()
                        .WithUpdateOrder(0),
                    new Moving2D()
                );

            DateTime start = DateTime.Now;

            for (int i = 0; i < 10000000; i++)
            {
                e.GetComponent<Moving2D>();
            }

            DateTime stop = DateTime.Now;
            Console.WriteLine($"Dauer: {(stop - start).Milliseconds}");
        }
    }
}
