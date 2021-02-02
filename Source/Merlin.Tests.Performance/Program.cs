using BenchmarkDotNet.Running;
using System;

namespace Merlin.Tests.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ECS>();
            //Console.WriteLine(summary.Table);
            Console.ReadLine();
        }
    }
}
