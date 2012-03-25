using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Growth.Animations;

public class Sprite
{
    public Vector2 Origin;
    public Vector2 Position;
    public float Rotation;
    public Texture2D Texture;
    public Rectangle SourceRectangle;
    public Color Tint;

    public Sprite(Texture2D texture)
        : this(texture, Vector2.Zero)
    {               
    }

    public Sprite(Texture2D texture, Vector2 origin)
    {
        Texture = texture;
        Origin = origin;
        Tint = Color.White;
        SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
    }
}
