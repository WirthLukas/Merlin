using System;
using System.Collections.Generic;

namespace Merlin.M2D.ECS.Components.Sprites
{
    public class DefaultAnimationHandler : IAnimationHandler
    {
        protected Dictionary<string, Animation> Animations { get; set; }
        public Animation CurrentAnimation { get; protected set; }
        
        #region <<Methods>>

        public void AddAnimation(Animation animation)
        {
            //if (animation == null)
            //    throw new ArgumentNullException(nameof(animation));

            Animations.Add(animation.Name, animation);
        }

        public void AddAnimations(params Animation[] animations)
        {
            foreach (var animation in animations)
            {
                Animations.Add(animation.Name, animation);
            }
        }

        public virtual void ChangeAnimationTo(string name)
        {
            if (!Animations.ContainsKey(name))
                throw new ArgumentException("Can not find any animation with that name");

            Animation a = Animations[name];
            CurrentAnimation = a;           
        }

        #endregion
        
        public DefaultAnimationHandler(Animation startAnimation, params Animation[] furtherAnimations)
        {
            if (startAnimation == null)
                throw new ArgumentNullException(nameof(startAnimation), "Can not handle Animation null");

            Animations = new Dictionary<string, Animation>
            {
                { startAnimation.Name, startAnimation }
            };
            CurrentAnimation = startAnimation;

            AddAnimations(furtherAnimations);            
        }
    }
    
    public interface IAnimationHandler
    {
        Animation CurrentAnimation { get; }
    }
}