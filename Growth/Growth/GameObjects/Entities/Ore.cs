using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Growth.GameObjects.Entities
{
    public class Ore : Entity
    {
        //private int rotateDirection;

        public Ore(Sprite sprite)
            : base(sprite) 
        {

        }

        public override void Update(double dt)
        {
            Sprite.Rotation = this.Rotation += 0.3f * (float)dt;
        }

        public override void CollisionWith(Entity collider)
        {
            if (collider is Ship)
                OnDestroyed();

            base.CollisionWith(collider);
        }
    }
}
