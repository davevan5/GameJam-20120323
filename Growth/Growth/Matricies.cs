// -----------------------------------------------------------------------
// <copyright file="Matricies.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Growth
{        
    public static class Matricies
    {
        const float smallestSqaureUnitSize = 25;

        public static void GetWorldToViewMatrix(Viewport viewport, out Matrix matrix)
        {
            // with sprite batch, the view is not in world coordinates, it's in pixel coordinates
            // so we need to figure out how to get there
            // so we need to figure out how much units the screen represents
            // so lets take the smallest square the screen can make and assign it world unit size
            float smallestDimension = Math.Min(viewport.Width, viewport.Height);
            float screenPixelsPerUnit = smallestDimension / smallestSqaureUnitSize;

            Matrix result = Matrix.Identity;
            result *= Matrix.CreateScale(screenPixelsPerUnit);
            result *= Matrix.CreateTranslation(new Vector3(viewport.Width / 2f, viewport.Height / 2f, 0));
            matrix = result;
        }
    }
}
