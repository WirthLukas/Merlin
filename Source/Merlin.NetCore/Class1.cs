using System;
using Microsoft.Xna.Framework;

#nullable enable

namespace Merlin.NetCore
{
    public class Class1 : Game
    {
        protected GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        public Class1(string contentDirectory = "Content")
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Class1 test = new Class1(null);
        }
    }
}
