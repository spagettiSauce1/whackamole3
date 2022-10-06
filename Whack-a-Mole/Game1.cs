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
        public Vector2 speed;
        public Vector2 direction;
        public Rectangle moleBox;

        //2D arreyer
        hole[,] holes;
        mole[,] moles;
        grass[,] grassOnHole;

        public int moleWidth;
        public int moleHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 980;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            holeTex = Content.Load<Texture2D>("hole (1)");
            grassTex = Content.Load<Texture2D>("hole_foreground");
            moleTex = Content.Load<Texture2D>("mole");



            holes = new hole[3, 3];
            moles = new mole[3, 3];
            grassOnHole = new grass[3, 3];


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = j * 300;
                    int y = i * 250;
                    holes[i, j] = new hole(holeTex, x, y);
                    grassOnHole[i, j] = new grass(grassTex, x, y);
                    moles[i, j] = new mole(moleTex, pos1, x, y, moleBox);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            foreach (mole m in moles)
            {
                m.Update();

                MouseState mouseState = Mouse.GetState();
                Point MousePos = new Point(mouseState.X, mouseState.Y);
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if(moleBox.Contains(MousePos))
                    {
                        
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);
            spriteBatch.Begin();

            for (int i = 0; i < holes.GetLength(0); i++)
            {
                for (int j = 0; j < holes.GetLength(1); j++)
                {
                    holes[i, j].Draw(spriteBatch);
                    moles[i, j].Draw(spriteBatch);
                    grassOnHole[i, j].Draw(spriteBatch);
                }
            }

            Rectangle moleBox = new Rectangle((int)pos1.X, (int)pos1.Y, moleTex.Width, moleTex.Height);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
