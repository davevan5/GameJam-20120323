using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.GameObjects.Entities;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects.Spawners
{
    public class NpcSpawner : Spawner
    {
        private List<NpcEnemy> npcs;
        public Entity Target;

        public NpcSpawner(EntityConstructor entityConstructor, Entity target)
            : base(entityConstructor)
        {
            this.npcs = new List<NpcEnemy>();
            Target = target;
        }

        private void OnNpcDestroyed(object sender, EventArgs e)
        {
            NpcEnemy thisNpc = (NpcEnemy)sender;
            npcs.Remove(thisNpc);

            (thisNpc).Destroyed -= OnNpcDestroyed;
        }

        public void Update(double dt)
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                npcs[i].Update(dt);
            }

            TimeSinceRespawn += dt;
            if (TimeSinceRespawn >= RespawnTime)
                Spawn();
        }

        public void Spawn()
        {
            if (npcs.Count < MaxSpawnCount)
            {
                NpcEnemy npc = (NpcEnemy)EntityConstructor.MakeEntity(typeof(NpcEnemy));
                npc.Position = this.Position + new Vector2(rand.Next(3, 20), rand.Next(3, 20));
                npc.Destroyed += OnNpcDestroyed;
                npc.Target = Target;
                npcs.Add(npc);

                TimeSinceRespawn = 0;
            }
        }
    }
}
