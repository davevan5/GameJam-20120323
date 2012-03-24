using Growth.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Growth.Input;
using Microsoft.Xna.Framework;

namespace Growth.GameObjects.Templates
{
    public class ShipTemplate : ITemplate
    {
        private Sprite shipSprite;
        private MouseWorldInput mouseInput;
        private EntityConstructor entityConstructor;

        public ShipTemplate(EntityConstructor entityConstructor, ContentManager content, MouseWorldInput mouseInput)
        {
            this.shipSprite = new Sprite(content.Load<Texture2D>("Sprites\\Ship"), new Vector2(16f, 16f));
            this.mouseInput = mouseInput;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            return new Ship(shipSprite, entityConstructor, mouseInput);
        }
    }
}