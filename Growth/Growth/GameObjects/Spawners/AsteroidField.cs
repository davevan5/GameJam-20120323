using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Growth.GameObjects.Entities;
using Growth.Rendering;

namespace Growth.GameObjects.Spawners
{
    public class AsteroidField : Spawner
    {
        private List<Asteroid> asteroids;

        public AsteroidField(EntityConstructor entityConstructor)
            : base(entityConstructor)
        {
            this.asteroids = new List<Asteroid>();
        }

        public void Update(double dt)
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Update(dt);
            }

            TimeSinceRespawn += dt;
            if (TimeSinceRespawn >= RespawnTime)
                Spawn();
        }

        private void OnAsteroidDestroyed(object sender, EventArgs e)
        {
            Asteroid thisAsteroid = (Asteroid)sender;
            asteroids.Remove(thisAsteroid);

            (thisAsteroid).Destroyed -= OnAsteroidDestroyed;
        }

        public void Spawn()
        {
            if (asteroids.Count < MaxSpawnCount)
            {
                Asteroid newAsteroid = (Asteroid)EntityConstructor.MakeEntity(typeof(Asteroid));
                newAsteroid.Position = this.Position + new Vector2(rand.Next(3, 20), rand.Next(3, 20));
                newAsteroid.Destroyed += OnAsteroidDestroyed;
                asteroids.Add(newAsteroid);

                TimeSinceRespawn = 0;
            }
        }
    }
}
