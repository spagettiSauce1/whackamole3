using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhackaMole
{
    class mole
    {
        public Texture2D moleTex;
        public Vector2 pos1= Vector2.Zero;
        public Vector2 speed = new Vector2(0,2);
        public Vector2 direction = new Vector2(0, 1);
        public Rectangle moleBox;
        Vector2 startPos;
        Vector2 maxPos;

        public mole(Texture2D moleTex, Vector2 pos1, int x, int y, Rectangle moleBox)
        {
            this.moleTex = moleTex;
            this.pos1= new Vector2(x, y);
            this.speed = new Vector2(5, 2);
            this.direction = new Vector2(0, -1);
            maxPos.Y = startPos.Y - moleTex.Height;
            startPos = new Vector2(x, y);
            this.moleBox = moleBox; 

        }
        public void Update()
        {
            pos1 = pos1 + direction * speed;
            
            if (pos1.Y > startPos.Y || pos1.Y < maxPos.Y / 2)
            {
                direction = direction * -1;
            } 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(moleTex, pos1,Color.White);
        }
    }
}
    class hole
    {
        Texture2D holeTex;
        Vector2 pos;

        public hole(Texture2D holeTex, int x, int y)
        {
            this.holeTex = holeTex;
            this.pos = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(holeTex, pos, Color.White);
        }

    }
    class grass
    {
        Texture2D grassTex;
        Vector2 pos;

        public grass(Texture2D grassTex, int x, int y)
        {
            this.grassTex = grassTex;
            this.pos = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(grassTex, pos, Color.White);
        }
    }
}
