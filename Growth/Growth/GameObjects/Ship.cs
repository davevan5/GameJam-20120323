using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Growth.Input;

namespace Growth.GameObjects
{
    public class Ship : Entity
    {
        private const float AccelerationSpeed = 80f;
        private const float DragFactor = 0.95f;

        public Vector2 Origin = new Vector2(16f, 16f);
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public int Shield;
        public int Health;

        public void Update(double dt, MouseWorldInput mouseInput)
        {
            Move(dt);
            SetFacing(mouseInput.GetMouseWorldPosition());            
        }

        private void Move(double dt)
        {
            Acceleration = CalculateMovement() * AccelerationSpeed;
            Velocity = (Velocity + (Acceleration * (float)dt)) * DragFactor;

            Position += Velocity * (float)dt;
            Acceleration = Vector2.Zero;
        }

        private Vector2 CalculateMovement()
        {
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

            return movement;
        }

        private void SetFacing(Vector2 mousePos)
        {
            Vector2 distance = Vector2.Zero;
            distance = mousePos - Position;
            Rotation = (float)Math.Atan2(distance.Y, distance.X);
        }
    }
}