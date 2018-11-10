using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimeraChaos
{
    public class Torso:Sprite
    {
        public Torso(Texture2D texture):base(texture)
        {
        }

        public Torso(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {
        }

        public new void Update()
        {
            base.Update();
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
