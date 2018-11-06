using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimeraChaos
{
    public class Room : Sprite
    {
        private List<Monster> monsters;
        private Mansion mansion;
        private Rectangle _mansionBoundries;
        private Rectangle _mansionLocation;
        private Point _offset;

        public Room(Texture2D texture) : base(texture, new Rectangle(0, 0, 600, 200))
        {
            monsters = new List<Monster>();
        }

        public Room(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
            monsters = new List<Monster>();
        }

        public void EnterRoom(Monster sprite)
        {
            sprite.SetRoom(this);
            monsters.Add(sprite);
        }

        public void SetMansion(Mansion mansion,Point point)
        {
            this.mansion = mansion;
            _offset = point;
            SetLocation();
        }

        public Rectangle GetBoundries()
        {
            return spriteRectangle;
        }

        private void SetLocation()
        {
            _mansionBoundries = mansion.GetBoundries();
            spriteRectangle.Location = _mansionBoundries.Location + _offset;
        }

        public new void Update()
        {
            SetLocation();
            base.Update();
            foreach(Monster monster in monsters)
            {
                monster.Update();
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Monster monster in monsters)
            {
                monster.Draw(spriteBatch);
            }
        }

    }
}
