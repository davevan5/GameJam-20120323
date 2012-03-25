using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Growth.GameObjects.Entities
{
    public class Ore : Entity
    {
        private const int ScoreReward = 10;
        private readonly PlayerStats stats;

        public Ore(Sprite sprite, PlayerStats stats)
            : base(sprite) 
        {
            this.stats = stats;
            DragFactor = 0.95f;
        }

        public override void Update(double dt)
        {
            Sprite.Rotation = this.Rotation += 0.3f * (float)dt;
        }

        public override void CollisionWith(Entity collider)
        {
            if (collider is Ship)
            {
                stats.Score += ScoreReward;
                OnDestroyed();
            }

            base.CollisionWith(collider);
        }
    }
}
