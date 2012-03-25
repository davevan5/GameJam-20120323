using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.Rendering;
using Growth.Physics;
using Growth.GameObjects.Entities;

namespace Growth.GameObjects
{
    public class EntityManager
    {
        private readonly List<Entity> entities;
        private readonly PhysicsSimulator physics;
        private readonly Renderer renderer;

        public EntityManager(PhysicsSimulator physics, Renderer renderer)
        {
            this.physics = physics;
            entities = new List<Entity>();
            this.renderer = renderer;
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
            entity.Destroyed += OnEntityDestroyed;
            renderer.AddSprite(entity.Sprite);
            physics.AddEntity(entity);
        }

        private void OnEntityDestroyed(object sender, EventArgs e)
        {
            Entity thisEntity = (Entity)sender;
            renderer.RemoveSprite(thisEntity.Sprite);
            entities.Remove(thisEntity);
            physics.RemoveEntity(thisEntity);
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
