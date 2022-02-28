using Microsoft.Xna.Framework;
using System;

namespace Merlin.Screens
{
    public enum TransitionState : byte { In, Out }

    public abstract class Transition : IDisposable
    {
        private readonly float _halfDuration;
        private float _currentSeconds;

        public float Duration { get; }
        public TransitionState State { get; private set; } = TransitionState.Out;
        public float Value => MathHelper.Clamp(_currentSeconds / _halfDuration, 0f, 1f);

        public event EventHandler? StateChanged;
        public event EventHandler? Completed;

        protected Transition(float duration)
        {
            Duration = duration;
            _halfDuration = duration / 2f;
        }

        public void Update(GameTime gameTime)
        {
            float elapsedSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;

            switch (State)
            {
                case TransitionState.Out:
                    _currentSeconds += elapsedSeconds;

                    if (_currentSeconds >= _halfDuration)
                    {
                        State = TransitionState.In;
                        StateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case TransitionState.In:
                    _currentSeconds -= elapsedSeconds;

                    if (_currentSeconds <= 0.0f)
                    {
                        Completed?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract void Draw(GameTime gameTime);

        public abstract void Dispose();
    }
}
