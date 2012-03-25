using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.GameObjects;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Growth.Physics
{        
    public class PhysicsSimulator
    {
        const float Epsilon = 0.0005f;

        struct CollisionPair
        {
            public Entity A;
            public Entity B;
        }

        List<Entity> entities;
        List<CollisionPair> collisionPairs;

        public PhysicsSimulator()
        {
            entities = new List<Entity>(100);
            collisionPairs = new List<CollisionPair>(100 * 100);
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        private void BroadPhase()
        {
            // to begin with we will work with circle collisions
            // need to find collision pairs.
            for (int i = 0; i < entities.Count; i++)
            {
                if (!entities[i].CanCollide)
                    continue;

                for (int j = i + 1; j < entities.Count; j++)
                {
                    if (!entities[j].CanCollide)
                        continue;

                    // Don't check against self
                    if (entities[i] == entities[j])
                        continue;

                    collisionPairs.Add(new CollisionPair() { A = entities[i], B = entities[j] });
                }
            }
        }

        public void Update(double dt)
        {
            collisionPairs.Clear();

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Velocity += entities[i].Acceleration * (float)dt;
                entities[i].Velocity *= entities[i].DragFactor;

                entities[i].Velocity = new Vector2(
                    Math.Abs(entities[i].Velocity.X) < Epsilon ? 0 : entities[i].Velocity.X,
                    Math.Abs(entities[i].Velocity.Y) < Epsilon ? 0 : entities[i].Velocity.Y);
            }

            BroadPhase();
            NarrowPhase();

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Position += entities[i].Velocity * (float)dt;
                entities[i].Acceleration = Vector2.Zero;
            }
        }

        private void NarrowPhase()
        {
            for (int i = 0; i < collisionPairs.Count; i++)
            {
                CollisionPair pair = collisionPairs[i];

                var distance = pair.A.Position - pair.B.Position;
                if (distance == Vector2.Zero)
                {
                    // ignore it, don't know which way to push
                    continue;
                }

                bool isPhysical = pair.A.IsPhysical && pair.B.IsPhysical;

                if (distance.Length() < pair.A.CollisionRadius + pair.B.CollisionRadius)
                {
                    Vector2 collisionNormal = Vector2.Normalize(distance);
                    pair.A.CollisionWith(pair.B);
                    pair.B.CollisionWith(pair.A);

                    if (isPhysical)
                    {
                        if (!pair.A.IsStatic)
                            pair.A.Velocity = Vector2.Reflect(pair.A.Velocity, collisionNormal);

                        if (!pair.B.IsStatic)
                            pair.B.Velocity = Vector2.Reflect(pair.B.Velocity, -collisionNormal);
                    }
                }
            }
        }
    }
}
