using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth.GameObjects
{
    public abstract class Entity
    {
        public Sprite Sprite;
        public Vector2 Position;
        public float Rotation;

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