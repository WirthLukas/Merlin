using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Merlin.Screens
{
    /// <summary>
    /// The Manager of all game screens. Only one Instance is possible!
    /// Access to this instance is provided by the Instance Property
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        private Transition? _activeTransition;
        private readonly Dictionary<string, Screen> _screens = new();

        public Screen? ActiveScreen { get; private set; } = null;
        public Screen? PreviousScreen { get; private set; } = null;
        public int ScreenCount => _screens.Count;

        public new Core Game => (Core)base.Game;

        public ScreenManager(Core game) : base(game) { }

        /// <summary>
        /// Adds the given Screen at the end of the screen buffer
        /// </summary>
        /// <param name="screen">the screen that should be added</param>
        /// <exception cref="ArgumentNullException">If screen is null</exception>
        /// <exception cref="ArgumentException">If a screen with that name is already in the managers list</exception>
        public ScreenManager AddScreen(Screen screen)
        {
            if (screen is null) throw new ArgumentNullException(nameof(screen));

            if (_screens.ContainsKey(screen.Name))
                throw new ArgumentException("There is already a screen with that name");

            _screens.Add(screen.Name, screen);
            return this;
        }

        /// <summary>
        /// Sets the screen with the given name to active and will execute the
        /// given transition each update call, until the transition is finished
        /// </summary>
        /// <param name="name">name of the screen</param>
        /// <param name="transition">transition that should be executed</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void LoadScreen(string name, Transition transition)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Could not search for a screen with name null");

            Screen? screen = GetScreenByName(name);

            if (screen == null)
                throw new ArgumentException("There is no screen with that name");

            LoadScreen(screen, transition);
        }

        /// <summary>
        /// Unloads the previous screen and sets the given screen to active
        /// and loads its contents
        /// </summary>
        /// <param name="name">name of the new active screen</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void LoadScreen(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "could not search for a screen with name null");

            Screen? screen = GetScreenByName(name);

            if (screen == null)
                throw new ArgumentException("There is no screen with that name");

            LoadScreen(screen);
        }

        /// <summary>
        /// Unloads the previous screen and sets the given screen to active
        /// and loads its contents
        /// </summary>
        /// <param name="name">name of the new active screen</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void LoadScreen(Screen screen, Transition transition)
        {
            if (screen is null) throw new ArgumentNullException(nameof(screen));
            if (transition is null) throw new ArgumentNullException(nameof(transition));

            if (_activeTransition is not null)
                return;

            _activeTransition = transition;
            _activeTransition.StateChanged += (sender, args) => LoadScreen(screen);
            _activeTransition.Completed += (sender, args) =>
            {
                _activeTransition.Dispose();
                _activeTransition = null;
            };
        }


        /// <summary>
        /// Unloads the previous screen and sets the given screen to active
        /// and loads its contents
        /// </summary>
        /// <param name="screen">The new Active screen</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void LoadScreen(Screen screen)
        {
            if (screen is null) throw new ArgumentNullException(nameof(screen));

            ActiveScreen?.UnloadContent();
            ActiveScreen?.Dispose();

            screen.Initialize();
            screen.LoadContent();

            ActiveScreen = screen;
        }

        /// <summary>
        /// Unloads the active Screen and loads the previous screen
        /// </summary>
        public void ToPreviosScreen()
        {
            if (PreviousScreen is not null)
            {
                LoadScreen(PreviousScreen.Name);
            }
        }

        /// <summary>
        ///  Unloads the active Screen and loads the previous screen 
        ///  with the given transition
        /// </summary>
        /// <param name="transition">to executing transition</param>
        public void ToPreviosScreen(Transition transition)
        {
            if (PreviousScreen is not null)
            {
                LoadScreen(PreviousScreen.Name, transition);
            }
        }

        /// <summary>
        /// returns the screen with the given name or null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Screen? GetScreenByName(string name) =>
            _screens.ContainsKey(name) ? _screens[name] : null;

        /// <summary>
        /// Updates the currently active screen and the active transition
        /// if it is not already completed 
        /// </summary>
        /// <param name="gameTime">the passed gameTime</param>
        public override void Update(GameTime gameTime)
        {
            ActiveScreen?.Update();
            _activeTransition?.Update(gameTime);
        }

        /// <summary>
        /// Draws the currently acitve screen and the active transition
        /// if it is not already completed 
        /// </summary>
        /// <param name="gameTime">the passed gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            ActiveScreen?.Draw();
            _activeTransition?.Draw(gameTime);
        }
    }
}
