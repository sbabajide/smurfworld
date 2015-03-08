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
    public class mushroom
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotationAngle;
        public int speed;
        public bool isVisible;
        Random random = new Random();
        public float randx, randy;

        //COnstructor
        public mushroom(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;
            randx = random.Next(0, 750);
            randy = random.Next(-450, -50);
        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {


        }

        //update
        public void Update(GameTime gameTime)
        {
            //Set Bounding box for collision
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            //Update Movement
            position.Y = position.Y + speed;
            if (position.Y >= 500)
                position.Y = -50;

            //Rotate Mushroom
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}

