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
    public class Enemy
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public int health, speed, bulletDelay, currentDifficultyLevel;
        public bool isVisible;
        public List<magic> bulletList;

        //Constructor
        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newbulletTexture)
        {
            bulletList = new List<magic>();
            texture = newTexture;
            bulletTexture = newbulletTexture;
            health = 5;
            position = newPosition;
            currentDifficultyLevel = 1;
            bulletDelay = 40;
            speed = 5;
            isVisible = true;
        }
        public void Update(GameTime gameTime)
        {
            //Update collision rectangle
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            //Update enery movement
            position.Y += speed;

            //move eney back to top if he flies off bottom

            if (position.Y >= 500)
                position.Y = -75;

            EnemyShoot();
            UpdateBullet();

        }

        //Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            //Draw eney bullets
            foreach (magic b in bulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        public void UpdateBullet()
        {
            // for each magic in the magicList, update the movement and if the magic its the top of the screen, remove from the list
            foreach (magic m in bulletList)
            {
                m.boundingBox = new Rectangle((int)m.position.X, (int)m.position.Y, m.texture.Width, m.texture.Height);
                //set movement for magic, if the magic hits the top of screen make visible false
                m.position.Y = m.position.Y - m.speed;

                if (m.position.Y >= 500)
                    m.isVisible = false;
            }
            //Iterate through magiclist and see if any of the magic are not visible, if they arent then make them
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void EnemyShoot()
        {
            if (bulletDelay >= 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                magic newBullet = new magic(bulletTexture);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;

                if (bulletList.Count() < 20)
                    bulletList.Add(newBullet);
            }
            if (bulletDelay == 0)
                bulletDelay = 40;
        }


    }
}
