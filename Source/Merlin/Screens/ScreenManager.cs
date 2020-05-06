using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;

namespace Merlin.Screens
{
    public class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private readonly Dictionary<string, Screen> _screens = new Dictionary<string, Screen>();
        private Screen? _previousScreen;
        private Screen? _activeScreen;

        public Screen? ActiveScreen => _activeScreen;
        public int ScreenCount => _screens.Count;

        // TODO: Build a Wrapper class for XNA GameComponents to Merlin?
        public new Core Game => (Core) base.Game;

        #region <<Methods>>

        /// <summary>
        /// Adds the given Screen at the end of the screen buffer
        /// </summary>
        /// <param name="screen">the screen that should be added</param>
        /// <exception cref="ArgumentNullException">If screen is null</exception>
        /// <exception cref="ArgumentException">If a screen with that name is already in the managers list</exception>
        public void AddScreen([NotNull]Screen screen)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));

            if (_screens.ContainsKey(screen.Name))
                throw new ArgumentException("There is already a screen with that name");

            _screens.Add(screen.Name, screen);
        }

        /// <summary>
        /// Unloads the previous screen and sets the given screen to active
        /// and loads its contents
        /// </summary>
        /// <param name="name">name of the new active screen</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void LoadScreen([NotNull]string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "could not search for a screen with name null");

            Screen? screen = GetScreenByName(name);

            if (screen == null)
                throw new ArgumentException("There is no screen with that name");

            LoadScreen(screen);
        }

        /// <summary>
        /// Unloads the previous screen and sets the given screen to active
        /// and loads its contents
        /// </summary>
        /// <param name="screen">The new Active screen</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void LoadScreen(Screen screen)
        {
            if (screen == null)
                throw new ArgumentNullException(nameof(screen), "could not load null");

            if (_activeScreen != null)
            {
                _activeScreen.UnloadContent();
                _activeScreen.Dispose();

                _previousScreen = _activeScreen;
            }

            screen.Initialize();
            screen.LoadContent();

            _activeScreen = screen;
        }

        /// <summary>
        /// Unloads the active Screen and loads the previous screen
        /// </summary>
        public void ToPreviousScreen()
        {
            if (_previousScreen != null)
            {
                LoadScreen(_previousScreen);
            }
        }

        /// <summary>
        /// returns the screen with the given name or null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Screen? GetScreenByName(string name) =>
            _screens.ContainsKey(name) ? _screens[name] : null;

        #endregion

        #region <<Lifecycle Methods>>

        /// <summary>
        /// Updates the currently active screen and the active transition
        /// if it is not already completed 
        /// </summary>
        /// <param name="gameTime">the passed gameTime</param>
        public override void Update(GameTime gameTime)
        {
            ActiveScreen?.Update(gameTime);
            // ActiveTransition?.Update(gameTime);
        }

        /// <summary>
        /// Draws the currently active screen and the active transition
        /// if it is not already completed 
        /// </summary>
        /// <param name="gameTime">the passed gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            ActiveScreen?.Draw(gameTime);
            // ActiveTransition?.Draw(gameTime);
        }

        #endregion

        public ScreenManager(Game game)
            : base(game)
        {
        }
    }
}
