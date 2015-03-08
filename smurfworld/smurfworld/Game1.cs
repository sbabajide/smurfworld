/*
 * SmurfWorld Game
 * Description: Program checks to see if there is a collision between the player and an enemy. If there is then the game exits.
 * Author: Babajide S. Adegbenro
 * Date: March 02, 2015
 * 
 * */

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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        AudioEngine audioEngine;
        SoundBank soundBank;
        WaveBank waveBank;

        Random random = new Random();
        public int enemyBulletDamage;
        //mushroom List
        List<mushroom> mushroomList = new List<mushroom>();
        List<Enemy> enemyList = new List<Enemy>();
        Player smurf = new Player();
        Background bg = new Background();
        SoundMgr sm = new SoundMgr();



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            this.Window.Title = "SmurfWorld";
            Content.RootDirectory = "Content";
            enemyBulletDamage = 10;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            smurf.LoadContent(Content);
            bg.LoadContent(Content);
            sm.LoadContent(Content);
            MediaPlayer.Play(sm.bgSong);
            MediaPlayer.IsRepeating = true;

            audioEngine = new AudioEngine("Content\\audio\\soundProj.xgs");
            waveBank = new WaveBank(audioEngine, "Content\\audio\\snore.xwb");
            soundBank = new SoundBank(audioEngine, "Content\\audio\\snore.xsb");




        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {

            //updating enemies
            foreach (Enemy e in enemyList)
            {
                if (e.boundingBox.Intersects(smurf.boundingBox))
                {

                    e.isVisible = false;
                    soundBank.GetCue("smurfsound").Play();
                    //Quit();

                }
                //check enemy bullet collision with player
                for (int i = 0; i < e.bulletList.Count; i++)
                {
                    if (e.boundingBox.Intersects(e.bulletList[i].boundingBox))
                    {
                        e.bulletList[i].isVisible = false;
                    }
                }
                e.Update(gameTime);
            }

            foreach (mushroom a in mushroomList)
            {
                if (a.boundingBox.Intersects(smurf.boundingBox))
                {
                    a.isVisible = false;
                    sm.collectmushroom.Play();
                }

                for (int i = 0; i < smurf.magiclist.Count; i++)
                {
                    if (a.boundingBox.Intersects(smurf.magiclist[i].boundingBox))
                    {
                        a.isVisible = false;
                        smurf.magiclist.ElementAt(i).isVisible = false;
                    }
                }
                a.Update(gameTime);
            }
            smurf.Update(gameTime);
            bg.Update(gameTime);
            LoadMushroom();
            LoadEnemies();

            audioEngine.Update();


            //  audioEngine.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            bg.Draw(spriteBatch);
            smurf.Draw(spriteBatch);
            foreach (mushroom a in mushroomList)
            {
                a.Draw(spriteBatch);
            }
            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /*public void PlayCue(string cueName)
        {
            soundBank.PlayCue(cueName);
        }*/

        public void Quit()
        {
            this.Exit();
        }
        //load mushrooms
        public void LoadMushroom()
        {
            int randy = random.Next(-400, -50);
            int randx = random.Next(0, 750);

            if (mushroomList.Count() < 5)
            {
                mushroomList.Add(new mushroom(Content.Load<Texture2D>("images/mushroom"), new Vector2(randx, randy)));
            }

            for (int i = 0; i < mushroomList.Count; i++)
                if (!mushroomList[i].isVisible)
                {
                    mushroomList.RemoveAt(i);
                    i--;
                }
        }
        public void LoadEnemies()
        {
            int randy = random.Next(-400, -50);
            int randx = random.Next(0, 750);

            if (enemyList.Count() < 3)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("images/enemy"), new Vector2(randx, randy), Content.Load<Texture2D>("images/medium")));
            }

            for (int i = 0; i < enemyList.Count; i++)
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
        }
    }
}
