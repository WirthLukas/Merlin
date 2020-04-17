using System;
using Microsoft.Xna.Framework;


namespace Merlin.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var v = new Vector2(2, 2);
            var d = new Vector2(0, 2);
            
            Console.WriteLine($"v: {v}\nd: {d}\nr: {v*d}");
        }
    }
}
