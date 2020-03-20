using System;

namespace SpaceAdventure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var game = new SpaceAdventureGame())
                game.Run();
        }
    }
}
