using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Growth.Animations;
using Growth.GameObjects.Entities;

namespace Growth.GameObjects.Templates
{        
    public class BoosterTemplate : ITemplate
    {
        private static Animation[] animations = new Animation[]
        {
            new Animation("Moving", 10, 0, 3, true),
            new Animation("Idle", 10, 4, 7, true),
            new Animation("PowerUp", 10, 8, 11, false),
            new Animation("PowerDown", 10, 12, 15, false),
        };


        private readonly ContentManager content;

        public BoosterTemplate(ContentManager content)
        {
            this.content = content;
        }

        public Entity Make()
        {
            FrameSprite sprite = new FrameSprite(
                content.Load<Texture2D>("Sprites\\blasterSheet"),
                new Rectangle(0, 0, 10, 10),
                new Vector2(10, 5));

            AnimationController animationController = new AnimationController(sprite);
            
            for (int i = 0; i < animations.Length; i++)
            {
                animationController.AddAnimation(animations[i]);
            }

            return new ShipBooster(sprite, animationController, new Vector2(9 - 16, 0) / 32f);
        }
    }
}

