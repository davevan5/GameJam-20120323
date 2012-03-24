using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.Rendering;

namespace Growth.GameObjects
{
    public class EntityManager
    {
        private List<Entity> entities;
        private Renderer renderer;

        public EntityManager(Renderer renderer)
        {
            entities = new List<Entity>();
            this.renderer = renderer;
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
            entity.Destroyed += OnEntityDestroyed;
            renderer.AddSprite(entity.Sprite);
        }

        private void OnEntityDestroyed(object sender, EventArgs e)
        {
            Entity thisEntity = (Entity)sender;
            renderer.RemoveSprite(thisEntity.Sprite);
            entities.Remove(thisEntity);

            thisEntity.Destroyed -= OnEntityDestroyed;
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
