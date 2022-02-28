using BenchmarkDotNet.Running;
using Merlin.ECS;
using Merlin.ECS.Experimental;
using System;
using System.Collections.Generic;

namespace Merlin.Tests.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<ECS>();
            //Console.WriteLine(summary.Table);

            //var pool = new ComponentPool<Position>();
            //pool.Add(new Position(10, 10), entityId: 1);
            //pool.Add(new Position(20, 20), entityId: 2);
            //pool.Add(new Position(30, 30), entityId: 3);
            //pool.Add(new Position(40, 40), entityId: 4);

            //Print(pool.Components);

            //pool.RemoveFor(4, out var pos);
            //Print(pool.Components);

            //pool.RemoveFor(1, out pos);
            //Print(pool.Components);

            var pool = new ComponentPool<Position>();
            pool.Add(new Position(10, 10), 1);
            ref var x = ref pool.GetFor(1);
            pool.RemoveFor(1, out IComponent component);

            Console.ReadLine();
        }

        static void Print<T>(IEnumerable<T> collection)
        {
            foreach(var item in collection)
            {
                Console.Write($"{item}, ");
            }

            Console.WriteLine();
        }
    }

    public record struct Position(int X, int Y) : IComponent
    {
        public IEntity Entity { get; set; } = null;
    }
}
