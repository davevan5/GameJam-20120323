using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Growth.GameObjects.Templates;
using Microsoft.Xna.Framework.Content;
using Growth.Input;
using Growth.Rendering;

namespace Growth.GameObjects
{
    public class EntityConstructor
    {
        private Dictionary<Type, ITemplate> templates;
        private Renderer renderer;
        private EntityManager entityManager;

        public EntityConstructor(Renderer renderer, EntityManager entityManager, ContentManager content, MouseWorldInput mouseInput)
        {
            this.renderer = renderer;
            this.entityManager = entityManager;

            templates = new Dictionary<Type, ITemplate>()
            {
                { typeof(Ship), new ShipTemplate(this, content, mouseInput) },
                { typeof(Projectile), new ProjectileTemplate(this, content, mouseInput) }
            };
        }

        public Entity MakeEntity(Type type)
        {
            ITemplate template = null;

            if (!templates.TryGetValue(type, out template))
            {
                throw new ArgumentException("Unsupported entity type");
            }

            Entity newEntity = template.Make();
            entityManager.AddEntity(newEntity);
            renderer.AddSprite(newEntity.Sprite);

            return newEntity;
        }
    }
}
