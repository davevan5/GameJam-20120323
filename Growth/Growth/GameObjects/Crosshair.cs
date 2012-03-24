using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Growth.Input;

namespace Growth.GameObjects
{
    public class Crosshair : Entity
    {
        private MouseWorldInput mouseInput;

        public Crosshair(Sprite sprite, MouseWorldInput mouseInput)
            : base(sprite)
        {
            this.mouseInput = mouseInput;
        }

        public override void Update(double dt)
        {
            Position = mouseInput.GetMouseWorldPosition();

            Sprite.Position = Position;
            Sprite.Rotation = Rotation;
        }
    }
}