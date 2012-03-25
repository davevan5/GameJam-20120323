using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Growth.Animations;
using Growth.GameObjects.Entities;

namespace Growth.GameObjects
{   
    public class ShipBooster : Entity
    {
        private enum BoosterState
        {
            Idle,
            PowerUp,
            PowerDown,
            Moving
        }

        private BoosterState state;
        private readonly AnimationController animations;
        private readonly Vector2 offset;
        public Ship Player;
        
        public ShipBooster(Sprite sprite, AnimationController animations, Vector2 offset)
            : base(sprite)
        {
            this.animations = animations;
            this.offset = offset;
            animations.AnimationCompleted += OnAnimationCompleted;
            animations.PlayAnimation("Idle");
        }

        private void OnAnimationCompleted(object sender, EventArgs e)
        {
            switch (state)
            {
                case BoosterState.PowerUp:
                    animations.PlayAnimation("Moving");
                    break;

                case BoosterState.PowerDown:
                    animations.PlayAnimation("Idle");
                    break;
            }
        }

        public override void Update(double dt)
        {
            if (Player == null)
                return;

            if ((state == BoosterState.Idle || state == BoosterState.PowerDown)
              && Player.Acceleration != Vector2.Zero)
            {
                state = BoosterState.PowerUp;
                animations.PlayAnimation("PowerUp");
            }

            if ((state == BoosterState.Moving || state == BoosterState.PowerUp)
              && Player.Acceleration == Vector2.Zero)
            {
                state = BoosterState.PowerDown;
                animations.PlayAnimation("PowerDown");
            }
            
            Vector2 rotatedOffset = offset.Rotate(Player.Rotation);

            Position = Player.Position + rotatedOffset;
            Rotation = Player.Rotation;

            animations.Update(dt);
        }
    }
}
