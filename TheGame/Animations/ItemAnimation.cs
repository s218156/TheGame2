﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Animations
{
    class ItemAnimation : AnimatedTexture
    {
        public ItemAnimation(Texture2D texture, Rectangle rectangle, int rows, int columns): base(texture,rectangle, rows, columns)
        {

        }

		public override void SetDirection(bool direction)
		{
			throw new NotImplementedException();
		}

		public override void SetMovement(bool isOnMove)
		{
			throw new NotImplementedException();
		}

		public void UpdateRectangle(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }
    }
}
