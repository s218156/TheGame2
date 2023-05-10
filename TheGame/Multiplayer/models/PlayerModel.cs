using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Multiplayer.models
{
    internal class PlayerModel
    {
        public int id { get; set; }
        public PlayerRectangle rectangle { get; set; }
        public int textureID { get; set; }
    }
}
