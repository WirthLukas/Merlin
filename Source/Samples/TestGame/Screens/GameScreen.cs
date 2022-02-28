using Merlin;
using Merlin.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Merlin.EC;
using Merlin.M2D.EC;

namespace TestGame.Screens
{
    public class GameScreen : Screen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly EcContext _context;
        private readonly ContentManager _content;

        private Entity? _ship;

        public GameScreen(Game game, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
            : base("Game", graphicsDevice, Color.Black)
        {
            _spriteBatch = spriteBatch;
            _context = new EcContext(game);
            _content = game.Content;

            //game.Components.Add(_context);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _ship = _context.CreateEntity();
            _ship.AddComponent(new Position2D(100, 100));
            _ship.AddComponent(new Moving2D(5, 1, 2)).Entity
                .AddComponent(new Sprite(_content.Load<Texture2D>("characters/player"), _spriteBatch));

            //_ship = new Entity()
            //    .AddComponent(new Position2D(100, 100)).Entity?
            //    .AddComponent(new Moving2D(5, 1, 2)).Entity?
            //    .AddComponent(new Sprite(_content.Load<Texture2D>("characters/player")))
            //    .Entity;

            //_context.AddEntity(_ship!);
        }

        public override void Draw()
        {
            base.Draw();
            _context.Draw(Time.GameTime);
            _spriteBatch.FillRectangle(10, 10, 100, 100, Color.Red);
            _spriteBatch.FillRectangle(100, 50, 100, 100, Color.Green);
        }

        public override void Update()
        {
            _context.Update(Time.GameTime);
        }
    }
}
