using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects.Entities
{
    public class NpcEnemy : Entity
    {
        private static Random rand = new Random();
        public const int MaxHealth = 400;
        private const float AccelerationSpeed = 50f;
        private const int ScoreReward = 200;

        private readonly PlayerStats stats;

        private readonly EntityConstructor entityConstructor;
        
        public Entity Target;
        
        public int Health;
        private const int explosiveFactor = 10;
        public int DropCount;

        public NpcEnemy(Sprite sprite, EntityConstructor entityConstructor, PlayerStats stats)
            : base(sprite)
        {
            this.stats = stats;
            DragFactor = 0.95f;
            Health = MaxHealth;
            MaxSpeed = 30f;
            this.entityConstructor = entityConstructor;
        }

        public override void Update(double dt)
        {
            if (Health < 1)
                OnDestroyed();

            if (Target == null)
                return;

            Vector2 direction = Target.Position - this.Position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            Acceleration = direction * AccelerationSpeed;
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public override void CollisionWith(Entity collider)
        {
            if (collider is Projectile)
            {
                Health -= ((Projectile)collider).Damage;                
            }

            base.CollisionWith(collider);
        }

        protected override void OnDestroyed()
        {
            stats.Score += ScoreReward;

            int maxDropDistance = (int)(CollisionRadius * 2);
            for (int i = 0; i < DropCount; i++)
            {
                Ore newOre = (Ore)entityConstructor.MakeEntity(typeof(Ore));
                newOre.Position = Position;
                newOre.Velocity = new Vector2(((float)rand.NextDouble() * 2 - 1) * explosiveFactor, ((float)rand.NextDouble() * 2 - 1) * explosiveFactor);
            }

            base.OnDestroyed();
        }
    }
}
