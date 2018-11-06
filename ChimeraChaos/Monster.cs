using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimeraChaos
{
    public class Monster : Sprite
    {
        private Rectangle _roomBoundries;
        private Rectangle _roomLocation;


        public Room room = null;

        public Monster(Texture2D texture) : base(texture, new Rectangle(0, 0, 100, 100))
        {
            _roomLocation = new Rectangle(0, 0, 100, 100);
            base.spriteSpeed.X = 10;
            base.spriteSpeed.Y = 4;
        }

        public Monster(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            Random rnd = new Random();
            _roomLocation = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            base.spriteSpeed.X = rnd.Next(-5,5);
            Thread.Sleep(1);
            base.spriteSpeed.Y = rnd.Next(-5,5);
            Thread.Sleep(1);
        }

        public void SetRoom(Room room)
        {
            this.room = room;
            _roomBoundries = room.GetBoundries();
            _roomLocation.Location = _roomBoundries.Location + base.spriteRectangle.Location;
        }

        public void UpdateLocation()
        {
            _roomBoundries = room.GetBoundries();
            _roomLocation.Location = _roomBoundries.Location + base.spriteRectangle.Location;
        }

        private void Collision()
        {

            //TODO Add logic to prevent item from leaving room
            if (_roomLocation.X + base.spriteRectangle.Width > _roomBoundries.X + _roomBoundries.Width)
            {
                base.spriteRectangle.X = _roomBoundries.Width - base.spriteRectangle.Width;
                _roomLocation.Location = _roomBoundries.Location + base.spriteRectangle.Location;
                base.spriteSpeed.X *= -1;
            }
            if (base.spriteRectangle.X < 0)
            {
                base.spriteRectangle.X = 0;
                _roomLocation.Location = _roomBoundries.Location + base.spriteRectangle.Location;
                base.spriteSpeed.X *= -1;
            }
            if (_roomLocation.Y + base.spriteRectangle.Height > _roomBoundries.Y + _roomBoundries.Height)
            {
                base.spriteRectangle.Y = _roomBoundries.Height - base.spriteRectangle.Height;
                _roomLocation.Location = _roomBoundries.Location + base.spriteRectangle.Location;
                base.spriteSpeed.Y *= -1;
            }
            if (base.spriteRectangle.Y < 0)
            {
                base.spriteRectangle.Y = 0;
                _roomLocation.Location = _roomBoundries.Location + base.spriteRectangle.Location;
                base.spriteSpeed.Y *= -1;
            }
            // need to find rooms location size and determine where in the canvis the object is.
        }

        public new void Update()
        {
            UpdateLocation();
            base.Update();
            Collision();
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            //TODO: fix this!
            spriteBatch.Draw(base.getTexture(), _roomLocation, Color.White);
            //base.Draw(spriteBatch);
        }

    }
}
