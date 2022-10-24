using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WhackaMole;
using System;

namespace Whack_a_Mole
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Grass variabler
        Texture2D grassTex;

        //Hole variabler
        Texture2D holeTex;

        //Mole variabler
        Texture2D moleTex;
        public Vector2 pos1;
        public Rectangle moleBox;
        public int rndX;
        public int rndY;
        public int moleActive;

        SpriteFont spritefont;
        Vector2 textPos;

        MouseState oldMouseState;
        MouseState mouseState;

        //2D arreyer
        hole[,] holes;
        mole[,] moles;
        grass[,] grassOnHole;


        double timeSinceLastFrame = 0;
        double timeBetweenFrames = 3;

        double timeSinceLastTimerFrame = 0;
        double timeBetweenTimerFrames = 1;

        public int moleWidth;
        public int moleHeight;

        public int score;
        public int timer = 15;

        public Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            random = new Random();

            holeTex = Content.Load<Texture2D>("hole (1)");
            grassTex = Content.Load<Texture2D>("hole_foreground");
            moleTex = Content.Load<Texture2D>("mole");
            spritefont = Content.Load<SpriteFont>("scoreText");

            holes = new hole[3, 3];
            moles = new mole[3, 3];
            grassOnHole = new grass[3, 3];


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = j * 300 + 100;
                    int y = i * 250 + 250;
                    holes[i, j] = new hole(holeTex, x, y);
                    grassOnHole[i, j] = new grass(grassTex, x, y);
                    moles[i, j] = new mole(moleTex, x, y);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Point MousePos = new Point(mouseState.X, mouseState.Y);
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if(timeSinceLastFrame >= timeBetweenFrames)
            {
                timeSinceLastFrame -= timeBetweenFrames;
                rndX = random.Next(0, 3);
                rndY = random.Next(0, 3);
                
            }
            timeSinceLastTimerFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if(timeSinceLastTimerFrame >= timeBetweenTimerFrames)
            {
                timeSinceLastTimerFrame -= timeBetweenTimerFrames;
                timer--;
            }
            

            moles[rndX, rndY].activate();


            foreach (mole m in moles)
            {      
                 m.Update();

                 if (mouseState.LeftButton == ButtonState.Pressed && m.moleBox.Contains(MousePos) && oldMouseState.LeftButton == ButtonState.Released)
                 {
                    m.hitMole();
                    score =+ 100;
                 }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);
            spriteBatch.Begin();

            string overlayText = null;
            string overlayText2 = null;

            foreach(mole m in moles)
            {
                overlayText = "Score: ";
                overlayText2 = "Time: ";
            }

            for (int i = 0; i < holes.GetLength(0); i++)
            {
                for (int j = 0; j < holes.GetLength(1); j++)
                {
                    holes[i, j].Draw(spriteBatch);
                    moles[i, j].Draw(spriteBatch);
                    grassOnHole[i, j].Draw(spriteBatch);
                }
            }

            spriteBatch.DrawString(spritefont,overlayText + score , Vector2.Zero, Color.Yellow);
            spriteBatch.DrawString(spritefont, overlayText2 + timer , new Vector2(0, 25), Color.Yellow);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
