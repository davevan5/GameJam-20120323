using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth
{
    public abstract class Entity
    {
        public Texture2D Texture;
        public Vector2 Position;
        public float Rotation;
    }
}