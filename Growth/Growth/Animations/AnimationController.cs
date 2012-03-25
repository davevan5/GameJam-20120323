using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Growth.Animations
{
    public class AnimationController
    {        
        private readonly IAnimatable animatable;
        private Dictionary<string, Animation> animations;
        private int animationDirection;
        private double elapsedTime;
        private double timePerFrame;

        public AnimationController(IAnimatable animatable)
        {
            if (animatable == null)
                throw new ArgumentNullException("animatable", "animatable is null.");

            this.animatable = animatable;
            animations = new Dictionary<string, Animation>();
        }

        public Animation CurrentAnimation { get; private set; }

        private int currentFrame;
        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            private set
            {
                currentFrame = value;
                animatable.CurrentFrame = value;
            }
        }

        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);
        }

        public void RemoveAnimation(string name)
        {
            animations.Remove(name);
        }

        public void PlayAnimation(string name)
        {
            Animation animation = null;

            if (!animations.TryGetValue(name, out animation))
                throw new ArgumentException("Invalid animation name");

            CurrentAnimation = animation;
            CurrentFrame = animation.StartFrame;
            animationDirection = Math.Sign(animation.EndFrame - animation.StartFrame);
            elapsedTime = 0f;
            timePerFrame = 1f / animation.Fps;
        }

        public event EventHandler AnimationCompleted;

        private void OnAnimationCompleted()
        {
            if (AnimationCompleted != null)
                AnimationCompleted(this, EventArgs.Empty);
        }

        public void Update(double dt)
        {
            if (CurrentAnimation == null)
                return;

            Animation current = CurrentAnimation;
                        
            elapsedTime += dt;

            while (elapsedTime > timePerFrame)
            {
                elapsedTime -= timePerFrame;
                CurrentFrame += animationDirection;

                if (CurrentFrame == current.EndFrame)
                {
                    if (!current.Loop)
                    {
                        CurrentAnimation = null;
                        OnAnimationCompleted();                        
                        break;
                    }                                        
                }

                if (CurrentFrame == current.EndFrame + animationDirection && current.Loop)
                {
                    CurrentFrame = current.StartFrame;
                }
            }            
        }
    }
}
