using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects.Spawners
{
    public abstract class Spawner
    {
        protected static Random rand = new Random();

        protected EntityConstructor EntityConstructor;        
        protected double TimeSinceRespawn;

        public double RespawnTime;
        public int MaxSpawnCount;        
        public Vector2 Position;

        public Spawner(EntityConstructor entityConstructor)
        {
            EntityConstructor = entityConstructor;
        }
    }
}
