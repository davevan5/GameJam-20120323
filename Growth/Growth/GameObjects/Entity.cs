using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects
{
    public abstract class Entity
    {
        public Sprite Sprite;
        
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
                this.rotation = value;
                Sprite.Rotation = value;
            }
        }

        public Entity(Sprite sprite)
        {
            this.Sprite = sprite;
        }

        public abstract void Update(double dt);

        public event EventHandler Destroyed;

        protected virtual void OnDestroyed()
        {
            if (Destroyed != null)
            {
                Destroyed(this, EventArgs.Empty);
            }
        }
    }
}