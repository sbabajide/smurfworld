using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace smurfworld
{
    public class SoundMgr
    {
        public SoundEffect collectmushroom;
        public SoundEffect cat;
        public Song bgSong;

        //Constructor
        public SoundMgr()
        {
            collectmushroom = null;
            cat = null;
            bgSong = null;
        }

        public void LoadContent(ContentManager Content)
        {
            collectmushroom = Content.Load<SoundEffect>("audio/boing");
            bgSong = Content.Load<Song>("audio/bgSound");
        }
    }
}
