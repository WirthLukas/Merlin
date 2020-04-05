using System;
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
        private World? _world;

        public GameScreen(SpaceAdventureGame game, string name)
            : this(game, name, Color.CornflowerBlue)
        {
        }

        public GameScreen(SpaceAdventureGame game, string name, Color clearColor)
            : base(game, name, clearColor)
        {
            _spriteBatch = game.SpriteBatch ?? throw new AccessViolationException("Games SpriteBatch not initialized");
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

            if (Utils.IsNull(_world))
                throw new AccessViolationException("World is not initialized!");

            _world.AddEntity(new Entity("player-1"))
                .WithComponents(
                    new Position2D(100, 100),
                    new RotatedSprite<SimpleSprite>(new SimpleSprite(_content.Load<Texture2D>("Characters/player"), 1.5f))
                );

            //var anim = new DynamicAnimation(_content.Load<Texture2D>("Attacks/darkness"));

            //for (int i = 0; i < 3; i++)
            //{
            //    anim.AddFrame(new Rectangle(i * 192, 0, 192, 192), TimeSpan.FromSeconds(0.25));
            //}

            //_world.AddEntity(new Entity("attack"))
            //    .WithComponents(
            //        new Position2D(200, 200),
            //        new RotatedSprite<SimpleAnimatedSprite>(new SimpleAnimatedSprite(
            //            _content.Load<Texture2D>("Attacks/darkness"),
            //            3, 5, TimeSpan.FromMilliseconds(200), scale: 0.5f,
            //            stopAtLastFrame: false
            //        ))
            //    //new RotatedSprite<DynamicAnimation>(anim)
            //    );

            _world.AddEntity(new Entity("meteor"))
                .WithComponents(
                    new Position2D(600, 20),
                    new RotatedSprite<SimpleSprite>(
                        new SimpleSprite(_content.Load<Texture2D>("Characters/meteor_big1")),
                        -0.002f
                        )
                    //new Moving2D(3, new Vector2(-0.45f, 1))
                );

            _world.AddEntity(new Entity("meteor"))
                .WithComponents(
                    new Position2D(400, 20),
                    new RotatedSprite<SimpleSprite>(
                        new SimpleSprite(_content.Load<Texture2D>("Characters/meteor_big1")),
                        -0.002f
                    )
                    //new Moving2D(2, new Vector2(-1, 1))
                );
        }

        public override void Update(GameTime gameTime)
        {
            _world?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();

            _world?.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
