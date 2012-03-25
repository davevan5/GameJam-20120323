using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Growth.Animations;

public class FrameSprite : Sprite, IAnimatable
{
    private Rectangle frameSize;
    private int frameCount;
    private int framesHigh;
    private int framesWide;

    public FrameSprite(Texture2D texture, Rectangle frameSize, Vector2 origin)
        : base(texture, origin)
    {
        this.frameSize = frameSize;
        this.framesWide = texture.Width / frameSize.Width;
        this.framesHigh = texture.Height / frameSize.Height;
        this.frameCount = framesWide * framesHigh;
        CurrentFrame = 0;
        SetSourceRectangle();
    }

    private int currentFrame;
    public int CurrentFrame
    {
        get
        {
            return currentFrame;
        }
        set
        {
            currentFrame = value;

            if (currentFrame < 0)
                currentFrame = 0;

            if (currentFrame >= frameCount)
                currentFrame = frameCount - 1;

            SetSourceRectangle();
        }
    }

    private void SetSourceRectangle()
    {
        int frameX = currentFrame % framesWide;
        int frameY = currentFrame / framesWide;

        SourceRectangle = new Rectangle(frameX * frameSize.Width, frameY * frameSize.Height, frameSize.Width, frameSize.Height);
    }
}