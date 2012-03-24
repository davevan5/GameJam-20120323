using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Growth.Cameras
{    
    public class CameraStack
    {
        private readonly Camera defaultCamera;
        private readonly Stack<Camera> cameras;

        public CameraStack(Camera defaultCamera)
        {
            if (defaultCamera == null)
                throw new ArgumentNullException("defaultCamera", "defaultCamera is null.");

            this.defaultCamera = defaultCamera;
            this.cameras = new Stack<Camera>();
        }

        public Camera Current
        {
            get
            {
                if (cameras.Count == 0)
                    return defaultCamera;

                return cameras.Peek();
            }
        }

        public void PushCamera(Camera camera)
        {
            cameras.Push(camera);
        }

        public void PopCamera()
        {
            cameras.Pop();
        }

        public void Update(double dt)
        {
            defaultCamera.Update(dt);

            foreach (var camera in cameras)
            {
                camera.Update(dt);
            }
        }
    }
}
