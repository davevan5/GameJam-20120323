using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Growth.Rendering;
using Growth.GameObjects;
using Growth.Input;
using Growth.Cameras;

namespace Growth
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GrowthGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Renderer renderer;
        MouseWorldInput mouseInput;
        CameraStack cameraStack;
        StarFieldRenderer starFieldRenderer;
        Ship ship;
        Crosshair crosshair;

        public GrowthGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();
        }

        private Texture2D[] LoadStarTextures()
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("Sprites\\star01"),
                Content.Load<Texture2D>("Sprites\\star02"),
                Content.Load<Texture2D>("Sprites\\star03"),
                Content.Load<Texture2D>("Sprites\\star04"),
                Content.Load<Texture2D>("Sprites\\star05")
            };
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cameraStack = new CameraStack(new NullCamera(GraphicsDevice));            

            starFieldRenderer = new StarFieldRenderer(GraphicsDevice, cameraStack, LoadStarTextures());
            renderer = new Renderer(GraphicsDevice, cameraStack);

            mouseInput = new MouseWorldInput(GraphicsDevice, cameraStack);

            Sprite shipSprite = new Sprite(Content.Load<Texture2D>("Sprites\\Ship"), new Vector2(16f, 16f));
            ship = new Ship(shipSprite, mouseInput);

            Sprite crossSprite = new Sprite(Content.Load<Texture2D>("Sprites\\Cross"), new Vector2(16f, 16f));
            crosshair = new Crosshair(crossSprite, mouseInput);
            
            renderer.AddSprite(ship.Sprite);
            renderer.AddSprite(crosshair.Sprite);
            cameraStack.PushCamera(new FollowCamera(GraphicsDevice) { Ship = ship });            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            cameraStack.Update(gameTime.ElapsedGameTime.TotalSeconds);            
            ship.Update(gameTime.ElapsedGameTime.TotalSeconds);
            crosshair.Update(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            starFieldRenderer.Render();
            renderer.Render();

            base.Draw(gameTime);
        }
    }
}
