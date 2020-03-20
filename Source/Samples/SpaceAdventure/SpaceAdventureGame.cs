using System;
using System.Collections.Generic;
using System.Text;
using Merlin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceAdventure.Screens;

namespace SpaceAdventure
{
    public class SpaceAdventureGame : Merlin.Core
    {
		internal SpriteBatch SpriteBatch { get; set; }

        public SpaceAdventureGame()
		    : base(width: 900, height: 600)
        {
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            ScreenManager.AddScreen(new GameScreen(this, "game", Color.Black));
            ScreenManager.LoadScreen("game");
        }

        protected override void LoadContent()
		{
            SpriteBatch = new SpriteBatch(GraphicsDevice);


            base.LoadContent();
		}

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }
    }
}
