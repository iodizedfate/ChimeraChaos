using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ChimeraChaos.MacOS;

namespace ChimeraChaos
{
    public class Sprite
    {
        private Texture2D _texture;
        public Rectangle spriteRectangle;
        public Vector2 spriteVelocity;
        public Vector2 spriteSpeed;


        public Sprite(Texture2D texture)
        {
            _texture = texture;
            spriteRectangle = new Rectangle(0, 0, 100, 100);
            spriteVelocity = new Vector2(0, 0);
            spriteSpeed = new Vector2(0, 0);
        }

        public Sprite(Texture2D texture, Rectangle rectangle)
        {
            _texture = texture;
            spriteRectangle = rectangle;
            spriteVelocity = new Vector2(0, 0);
            spriteSpeed = new Vector2(0, 0);
        }

        public Texture2D getTexture()
        {
            return _texture;
        }

        private void Move()
        {
            spriteSpeed += spriteVelocity;
            spriteRectangle.X += (int) spriteSpeed.X;
            spriteRectangle.Y += (int) spriteSpeed.Y;
        }

        public void Update()
        {
            Move();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, spriteRectangle, Color.White);
        }
    }
}
