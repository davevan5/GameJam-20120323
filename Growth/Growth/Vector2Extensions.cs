using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth
{   
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            return new Vector2(
                (float)(v.X * Math.Cos(angle) - v.Y * Math.Sin(angle)),
                (float)(v.X * Math.Sin(angle) + v.Y * Math.Cos(angle)));
        }
    }
}
