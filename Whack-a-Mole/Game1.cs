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

        Texture2D grassTex;

        Texture2D holeTex;

        Texture2D moleTex;
        public Vector2 pos1;
        public Rectangle moleBox;
        public int rndX;
        public int rndY;
        public int moleActive;

        Texture2D spritesheet;
        Rectangle srcRec;

        SpriteFont spritefont;
        
        MouseState oldMouseState;
        MouseState mouseState;

        int gS;

        enum GameState
        {
            Start = 0,
            Play = 1,
            GameOver = 2,
        }

        GameState currentState = GameState.Start;

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
        public int timer = 30;

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
            spritesheet = Content.Load<Texture2D>("spritesheet_stone");

            srcRec = new Rectangle(0, 288, 32, 32);

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
            switch (currentState)
            {
                case GameState.Start:

                    gS = 1;
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        currentState = GameState.Play;
                    }
                    
                    break;

                case GameState.Play:
                    
                    gS = 2; 
                    
                    Point MousePos = new Point(mouseState.X, mouseState.Y);
                    oldMouseState = mouseState;
                    mouseState = Mouse.GetState();

                    timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastFrame >= timeBetweenFrames)
                    {
                        timeSinceLastFrame -= timeBetweenFrames;
                        rndX = random.Next(0, 3);
                        rndY = random.Next(0, 3);

                    }
                    
                    timeSinceLastTimerFrame += gameTime.ElapsedGameTime.TotalSeconds;                    
                    if (timeSinceLastTimerFrame >= timeBetweenTimerFrames)
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
                            score = score + 100;
                        }
                    }

                    if(timer == 0)
                    {
                        currentState = GameState.GameOver;
                    }
                    break;
                case GameState.GameOver:

                    gS = 3;
                    
                    break;

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);
            spriteBatch.Begin();

            if (gS == 1)
            {
                string startText = null;
                startText = "Press Space to continue";

                spriteBatch.DrawString(spritefont, startText, new Vector2(380,400), Color.Yellow);
                spriteBatch.Draw(spritesheet,new Vector2(300,300),srcRec,Color.White);
            }

            if(gS == 2)
            {
                string overlayText = null;
                string overlayText2 = null;
                overlayText = "Score: ";
                overlayText2 = "Time: ";

                for (int i = 0; i < holes.GetLength(0); i++)
                {
                    for (int j = 0; j < holes.GetLength(1); j++)
                    {
                        holes[i, j].Draw(spriteBatch);
                        moles[i, j].Draw(spriteBatch);
                        grassOnHole[i, j].Draw(spriteBatch);
                    }
                }

                spriteBatch.DrawString(spritefont, overlayText + score, Vector2.Zero, Color.Yellow);
                spriteBatch.DrawString(spritefont, overlayText2 + timer, new Vector2(0, 25), Color.Yellow);

            }

            if(gS == 3)
            {
                string gameOverText = null;
                gameOverText = "Game Over";

                spriteBatch.DrawString(spritefont, gameOverText, new Vector2(400,420),Color.Yellow);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
