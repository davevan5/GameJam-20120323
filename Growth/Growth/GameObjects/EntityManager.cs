using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Growth.GameObjects
{
    public class EntityManager
    {
        private List<Entity> entities;

        public EntityManager()
        {
            entities = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void Update(double dt)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(dt);
            }
        }
    }
}
