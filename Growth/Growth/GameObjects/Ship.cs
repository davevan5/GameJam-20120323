using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Growth.Input;

namespace Growth.GameObjects
{
    public class Ship : Entity
    {
        private EntityConstructor entityConstructor;
        private MouseWorldInput mouseInput;
        private double timeSinceLastFire;

        private const float AccelerationSpeed = 80f;        
        private const int RateOfFire = 7;

        public int Shield;
        public int Health;
        

        public Ship(Sprite sprite, EntityConstructor entityConstructor, MouseWorldInput mouseInput)
            : base(sprite)
        {
            DragFactor = 0.95f;

            this.mouseInput = mouseInput;
            this.entityConstructor = entityConstructor;
        }

        public override void Update(double dt)
        {
            Move(dt);
            SetFacing(GetMouseDirection());            

            if (Mouse.GetState().LeftButton == ButtonState.Pressed
                && timeSinceLastFire > (1f / RateOfFire))
            {
                FireWeapons(dt);
            }

            timeSinceLastFire += dt;
        }

        private void FireWeapons(double dt)
        {            
            Projectile projectile = (Projectile)entityConstructor.MakeEntity(typeof(Projectile));
            projectile.Sprite.Position = projectile.Position = this.Position;
            projectile.SetDirection(GetMouseDirection());

            timeSinceLastFire = 0;
        }

        private void Move(double dt)
        {
            Acceleration = CalculateMovement() * AccelerationSpeed;                        
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

        private void SetFacing(Vector2 direction)
        {
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        private Vector2 GetMouseDirection()
        {
            return Vector2.Normalize(mouseInput.GetMouseWorldPosition() - Position);
        }
    }
}