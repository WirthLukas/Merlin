using Merlin.ECS;
using Merlin.ECS.Builders;
using Merlin.M2D.ECS.Components.Positioning;
using Merlin.M2D.ECS.Components.Sprites;
using Merlin.M2D.ECS.Systems;
using Merlin.Screens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceAdventure.Screens
{
    public class GameScreen : Screen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ContentManager _content;
        private World _world;

        public GameScreen(SpaceAdventureGame game, string name)
            : this(game, name, Color.CornflowerBlue)
        {
        }

        public GameScreen(SpaceAdventureGame game, string name, Color clearColor)
            : base(game, name, clearColor)
        {
            this._spriteBatch = game.SpriteBatch;
            _content = game.Content;
        }

        public override void Initialize()
        {
            base.Initialize();

            _world = new WorldBuilder()
                .AddSystem(new MovingSystem2D())
                .AddSystem(new SpriteDrawerSystem(_spriteBatch))
                .Build();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _world.AddEntity(new Entity("player-1"))
                .WithComponents(
                    new Position2D(100, 100),
                    new SimpleAnimatedSprite(_content.Load<Texture2D>("Characters/player"), 2)
                );
        }

        public override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _world.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
