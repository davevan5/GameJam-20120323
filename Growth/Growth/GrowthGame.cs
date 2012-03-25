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
using Growth.GameObjects.Templates;
using Growth.Physics;

namespace Growth
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GrowthGame : Microsoft.Xna.Framework.Game
    {
        Song song;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        MouseWorldInput mouseInput;
        CameraStack cameraStack;
        StarFieldRenderer starFieldRenderer;
        TargetPointer targetPointer;


        Renderer renderer;
        EntityManager entityManager;
        EntityConstructor entityContructor;
        PhysicsSimulator physics;

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
            physics = new PhysicsSimulator();
            entityManager = new EntityManager(physics, renderer);
            entityContructor = new EntityConstructor(entityManager, Content, mouseInput);            

            Sprite pointerSprite = new Sprite(Content.Load<Texture2D>("Sprites\\Cross"), new Vector2(16f, 16f));
            MousePointer mousePointer = new MousePointer(pointerSprite, mouseInput);
            entityManager.AddEntity(mousePointer);
            renderer.AddSprite(mousePointer.Sprite);

            

            Planet earth = (Planet)entityContructor.MakeEntity(typeof(Planet));
            Ship playerShip =  (Ship)entityContructor.MakeEntity(typeof(Ship));
            playerShip.Position = new Vector2(20, 20);
            cameraStack.PushCamera(new FollowCamera(GraphicsDevice) { Ship = playerShip });

            Sprite arrowSprite = new Sprite(Content.Load<Texture2D>("Sprites\\Arrow"), new Vector2(16f, 16f));
            targetPointer = new TargetPointer(earth, playerShip, arrowSprite);
            renderer.AddSprite(arrowSprite);

            song = Content.Load<Song>("Music\\bgmus01");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            
        }

        protected override void UnloadContent()
        {
            starFieldRenderer.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            
            entityManager.Update(gameTime.ElapsedGameTime.TotalSeconds);
            physics.Update(gameTime.ElapsedGameTime.TotalSeconds);
            targetPointer.Update();
            cameraStack.Update(gameTime.ElapsedGameTime.TotalSeconds);

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
