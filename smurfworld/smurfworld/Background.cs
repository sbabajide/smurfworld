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

namespace smurfworld
{
    public class Background
    {
        public Texture2D texture;
        public Vector2 bgPos1, bgPos2;
        public int speed;

        //constructor
        public Background()
        {
            texture = null;
            bgPos1 = new Vector2(0, 0);
            bgPos2 = new Vector2(-800, 0);
            speed = 1;
        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("images/backgrd");
        }


        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
        }

        //Update
        public void Update(GameTime gameTime)
        {
            bgPos1.X = bgPos1.X + speed;
            bgPos2.X = bgPos2.X + speed;

            //Scrolling background (repeating)
            if (bgPos1.X >= 800)
            {
                bgPos1.X = 0;
                bgPos2.X = -800;
            }
        }
    }
}
