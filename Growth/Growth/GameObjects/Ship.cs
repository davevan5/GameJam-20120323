using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Growth.GameObjects
{
    public class Ship : Entity
    {
        private const float AccelerationSpeed = 80f;
        private const float DragFactor = 0.95f;

        public Vector2 Velocity;
        public Vector2 Acceleration;
        public int Shield;
        public int Health;

        public void Update(double dt)
        {
            //
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 movement = new Vector2();

            if (keyboardState.IsKeyDown(Keys.W))
                movement -= Vector2.UnitY;
            else if (keyboardState.IsKeyDown(Keys.S))
                movement += Vector2.UnitY;

            if (keyboardState.IsKeyDown(Keys.A))
                movement -= Vector2.UnitX;
            else if (keyboardState.IsKeyDown(Keys.D))
                movement += Vector2.UnitX;

            if (movement != Vector2.Zero)
                movement.Normalize();

            Acceleration = movement * AccelerationSpeed;

            
            Velocity += (Acceleration * (float)dt);
            Velocity *= DragFactor;
            Position += Velocity * (float)dt;
            Acceleration = Vector2.Zero;
        }
    }
}