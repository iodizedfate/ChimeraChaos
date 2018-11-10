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
        Head head;
        Torso torso;
        Legs legs;
        bool upright;

        public Room room = null;

        private void _setSpeed()
        {
            Random rnd = new Random();
            Thread.Sleep(1);
            base.spriteSpeed.X = rnd.Next(-5, 5);
            Thread.Sleep(1);
            base.spriteSpeed.Y = rnd.Next(-5, 5);
        }

        private Rectangle _getMonsterRectangle()
        {
            int Width = 100;
            int Height = 100;
            //adjusts the spriteRectangle for Monster class
            if (upright)
            {
                Width = head.spriteRectangle.Width;
                Height = head.spriteRectangle.Y + head.spriteRectangle.Height + torso.spriteRectangle.Y + torso.spriteRectangle.Height + legs.spriteRectangle.Y + legs.spriteRectangle.Height;
            }
            else
            {
                Height = head.spriteRectangle.Height;
                Width = head.spriteRectangle.X + head.spriteRectangle.Width + torso.spriteRectangle.X + torso.spriteRectangle.Width + legs.spriteRectangle.X + legs.spriteRectangle.Width;
            }
            return new Rectangle(0, 0, Width, Height);
        }

        public Monster(Texture2D texture): base(texture, new Rectangle(0, 0, 100, 100))
        {
            _roomLocation = new Rectangle(0, 0, 100, 100);
            _setSpeed();
            base.spriteRectangle = _getMonsterRectangle();

        }

        public Monster(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            _roomLocation = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            _setSpeed();
            base.spriteRectangle = _getMonsterRectangle();
        }

        public Monster(Head head, Torso torso, Legs legs, bool upright) : base(null)
        {
            _roomLocation = new Rectangle(0, 0, 50, 50);
            this.head = head;
            this.torso = torso;
            this.legs = legs;
            this.upright = upright;
            _setSpeed();
            base.spriteRectangle = _getMonsterRectangle();
        }

        public Monster(Texture2D texture, Head head, Torso torso, Legs legs):base (texture)
        {
            _roomLocation = new Rectangle(0, 0, 100, 100);
            this.head = head;
            this.torso = torso;
            this.legs = legs;
            _setSpeed();
            base.spriteRectangle = _getMonsterRectangle();
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

            //TODO fix logic to prevent item from leaving room
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
            Point torsoShift = new Point(0,0);
            Point legsShift = new Point(0, 0);
            if (head != null)
            {
                if (upright)
                {
                    torsoShift = new Point(0, head.spriteRectangle.Height);
                    legsShift = new Point(0, head.spriteRectangle.Height + torso.spriteRectangle.Y + torso.spriteRectangle.Height);
                }
                else
                {
                    torsoShift = new Point(head.spriteRectangle.Width, 0);
                    legsShift = new Point(head.spriteRectangle.Width + torso.spriteRectangle.X + torso.spriteRectangle.Width, 0);
                }
            }
            spriteBatch.Draw(head.getTexture(), new Rectangle(_roomLocation.Location + head.spriteRectangle.Location, head.spriteRectangle.Size), Color.White);
            spriteBatch.Draw(torso.getTexture(), new Rectangle(torsoShift + _roomLocation.Location + torso.spriteRectangle.Location, torso.spriteRectangle.Size), Color.White);
            spriteBatch.Draw(legs.getTexture(), new Rectangle(legsShift + _roomLocation.Location + legs.spriteRectangle.Location, legs.spriteRectangle.Size), Color.White);
            //base.Draw(spriteBatch);
        }

    }
}