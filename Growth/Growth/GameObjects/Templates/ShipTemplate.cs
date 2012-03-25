using Growth.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Growth.Input;
using Microsoft.Xna.Framework;
using Growth.GameObjects.Entities;

namespace Growth.GameObjects.Templates
{
    public class ShipTemplate : ITemplate
    {
        private ContentManager content;
        private MouseWorldInput mouseInput;
        private EntityConstructor entityConstructor;

        public ShipTemplate(EntityConstructor entityConstructor, ContentManager content, MouseWorldInput mouseInput)
        {
            this.content = content;
            this.mouseInput = mouseInput;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            Sprite shipSprite = new Sprite(content.Load<Texture2D>("Sprites\\Ship"), new Vector2(16f, 16f));
            return new Ship(shipSprite, entityConstructor, mouseInput)
            {
                CanCollide = true,
                IsPhysical = true,
                CollisionRadius = 0.6f
            };
        }
    }
}