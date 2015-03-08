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
    public class Player
    {

        public Texture2D texture, magicTexture;
        public Vector2 position;
        public int speed;
        public float magicDelay;



        //Collision variables
        public Rectangle boundingBox;
        public bool isColliding;


        public List<magic> magiclist;
        //Constructor
        public Player()
        {
            magiclist = new List<magic>();
            texture = null;
            position = new Vector2(100, 340);
            speed = 5;
            isColliding = false;
            magicDelay = 1;
        }


        //Load Content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("images/smurfright");
            magicTexture = Content.Load<Texture2D>("images/medium");

        }


        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            foreach (magic m in magiclist)
                m.Draw(spriteBatch);
        }

        //Update
        public void Update(GameTime gameTime)
        {

            KeyboardState keyState = Keyboard.GetState();

            //bounding for player
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 128, 48);
            //Shoot magic
            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
            }
            UpdateMagic();

            //smurf control
            //if (keyState.IsKeyDown(Keys.Up))
            //  position.Y = position.Y - speed;

            if (keyState.IsKeyDown(Keys.Left))
                position.X = position.X - speed;

            //if (keyState.IsKeyDown(Keys.Down))
            //position.Y = position.Y + speed;

            if (keyState.IsKeyDown(Keys.Right))
                position.X = position.X + speed;

            //keep smurf in screen bounds

            if (position.X <= 0) position.X = 0;
            if (position.X >= 800 - texture.Width) position.X = 800 - texture.Width;

            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= 500 - texture.Height) position.Y = 500 - texture.Height;


        }
        //Use to set starting of our magic
        public void Shoot()
        {
            //Shoot only if the magic delay resets
            if (magicDelay >= 0)
                magicDelay--;

            //if the magicDelay is at 0, then create a new magicdust in the player position and make visible on the screen and add the magic to the list
            if (magicDelay <= 0)
            {
                magic newMagic = new magic(magicTexture);
                newMagic.position = new Vector2(position.X + 32 - newMagic.texture.Width / 2, position.Y + 30);

                newMagic.isVisible = true;

                if (magiclist.Count() < 20)
                    magiclist.Add(newMagic);
            }

            //reset magic delay
            if (magicDelay == 0)
                magicDelay = 1;
        }

        // update Magic
        public void UpdateMagic()
        {
            // for each magic in the magicList, update the movement and if the magic its the top of the screen, remove from the list
            foreach (magic m in magiclist)
            {
                m.boundingBox = new Rectangle((int)m.position.X, (int)m.position.Y, m.texture.Width, m.texture.Height);
                //set movement for magic, if the magic hits the top of screen make visible false
                m.position.Y = m.position.Y - m.speed;

                if (m.position.Y <= 0)
                    m.isVisible = false;
            }
            //Iterate through magiclist and see if any of the magic are not visible, if they arent then make them
            for (int i = 0; i < magiclist.Count; i++)
            {
                if (magiclist[i].isVisible)
                {
                    magiclist.RemoveAt(i);
                    i--;
                }
            }
        }



    }
}
