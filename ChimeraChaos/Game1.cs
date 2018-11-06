using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChimeraChaos.MacOS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 mousePosition;
        Texture2D mouseTexture;
        readonly int canvisHeight = 650;
        readonly int canvisWidth = 800;
        Mansion mansion;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = canvisHeight;
            graphics.PreferredBackBufferWidth = canvisWidth;
            this.IsFixedTimeStep = true;
            this.graphics.SynchronizeWithVerticalRetrace = true;
            this.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 33);

        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            this.Window.Title = "Active Application";
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            this.Window.Title = "InActive Application";
            base.OnDeactivated(sender, args);
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //
            int numberOfRooms = 10;
            int monsterPerRoom = 10;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //setup textures
            Texture2D ballTexture = Content.Load<Texture2D>("ball");
            Texture2D roomTexture = Content.Load<Texture2D>("room1");
            Texture2D mansionTexture = Content.Load<Texture2D>("mansion1");
            mouseTexture = Content.Load<Texture2D>("mouse");
            Texture2D ground = Content.Load<Texture2D>("ground");
            Texture2D attic = Content.Load<Texture2D>("attic");
            Texture2D bearhead = Content.Load<Texture2D>("bearhead");
            Texture2D fishbear = Content.Load<Texture2D>("fishbear");
            //build mansion
            Random rnd = new Random();

            mansion = new Mansion(mansionTexture, canvisHeight, ground, attic);
            for (int i = 1; i <= numberOfRooms; i++)
            {
                Room room = new Room(roomTexture);
                for (int j = 1; j <= monsterPerRoom; j++)
                {
                    Texture2D monText;
                    if (j % 2 == 0)
                    {
                        monText = bearhead;
                    }
                    else
                    {
                        monText = fishbear;
                    }
                    //int size = rnd.Next(1,50);
                    int size = 100;
                    Monster monster = new Monster(monText, new Rectangle(rnd.Next(1,600), rnd.Next(1, 200), size, size));
                    room.EnterRoom(monster);
                }
                mansion.AddRoom(room);
            }

        }

        protected override void UnloadContent()
        {
            //texture.Dispose(); <-- Only directly loaded textures
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)//sets game to only run when window active
            {
                // For Mobile devices, this logic will close the Game when the Back button is pressed
                // Exit() is obsolete on iOS
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                mansion.Update();
                mousePosition = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
                // TODO: Add your update logic here
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            mansion.Draw(spriteBatch);
            spriteBatch.Draw(mouseTexture, mousePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}