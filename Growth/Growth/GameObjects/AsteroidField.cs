using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects
{
    public class AsteroidField
    {
        EntityConstructor entityContructor;
        private List<Asteroid> asteroids;
        private const double Respawn = 10;
        private double timeSinceRespawn;

        public int MaxCount;
        public Vector2 Position;
        private Random rand = new Random();

        public AsteroidField(EntityConstructor entityContructor) 
        {
            this.entityContructor = entityContructor;
            this.asteroids = new List<Asteroid>();
        }

        public void Update(double dt)
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Update(dt);
            }

            timeSinceRespawn += dt;
            if (timeSinceRespawn >= Respawn)
                RespawnAsteroid();
        }

        public void RespawnAsteroid()
        {
            if (asteroids.Count < MaxCount)
            {
                Asteroid newAsteroid = (Asteroid)entityContructor.MakeEntity(typeof(Asteroid));
                newAsteroid.Position = this.Position + new Vector2(rand.Next(3, 20), rand.Next(3, 20));
                asteroids.Add(newAsteroid);

                timeSinceRespawn = 0;
            }
        }
    }
}
