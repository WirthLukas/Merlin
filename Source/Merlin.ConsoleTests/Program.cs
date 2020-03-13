using System;
using Merlin.ECS;
using Merlin.M2D.ECS.Components;

namespace Merlin.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

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
            }
        }
    }
}
