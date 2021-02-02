using Merlin.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Screens;

namespace TestGame
{
    public class Game1 : Merlin.Core
    {
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;

        public Game1() : base()
        {
            IsMouseVisible = true;
            _screenManager = new(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            //Components.Add(_screenManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _screenManager.AddScreen(new MenuScreen(this.GraphicsDevice, _spriteBatch))
                          .AddScreen(new GameScreen(this.GraphicsDevice, _spriteBatch));

            // TODO: use this.Content to load your game content here
            _screenManager.LoadScreen("Game");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.M) && _screenManager.ActiveScreen.Name is not "Menu")
            {
                _screenManager.LoadScreen("Menu");
            }
            else if (state.IsKeyDown(Keys.G) && _screenManager.ActiveScreen.Name is not "Game")
            {
                _screenManager.LoadScreen("Game");
            }

            // TODO: Add your update logic here
            _screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _screenManager.Draw(gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
