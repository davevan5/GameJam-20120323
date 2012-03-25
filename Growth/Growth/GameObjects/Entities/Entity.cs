using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects.Entities
{
    public abstract class Entity
    {
        public Sprite Sprite;        
        public Vector2 Velocity;
        public Vector2 Acceleration;

        // Collision
        public float DragFactor;
        public float CollisionRadius;
        public bool CanCollide;
        public bool IsPhysical;
        public bool IsStatic;
        
        public Entity(Sprite sprite)
        {
            this.Sprite = sprite;
            this.DragFactor = 1f;
        }

        public bool IsAlive { get; private set; }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {            	
                position = value;
                Sprite.Position = value;
            }
        }

        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                value = MathHelper.WrapAngle(value);
                this.rotation = value;
                Sprite.Rotation = value;
            }
        }        

        public abstract void Update(double dt);

        public event EventHandler Destroyed;

        protected virtual void OnDestroyed()
        {
            if (Destroyed != null)
            {
                IsAlive = false;
                Destroyed(this, EventArgs.Empty);
            }
        }

        public virtual void CollisionWith(Entity collider)
        {            
        }
    }
}