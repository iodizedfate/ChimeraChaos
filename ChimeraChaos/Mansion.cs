using System;
using System.Collections.Generic;
using ChimeraChaos.MacOS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//TODO whole list

//3 make classes for monster parts
//3.5 change monster class to hold parts
//4 build DNA splicer
//5 build doors for floors and have them lead to other rooms



namespace ChimeraChaos
{
    public class Mansion : Sprite
    {
        private List<Room> floors;
        Texture2D ground;
        Texture2D attic;

        //controls
        MouseState lastMouseState;
        MouseState currentMouseState;
        int canvasHeight;

        public Mansion(Texture2D texture, int canvasHeight, Texture2D ground, Texture2D attic):base(texture)
        {
            floors = new List<Room>();
            base.spriteRectangle = new Rectangle(90, 420, 620, 210);
            currentMouseState = Mouse.GetState();
            this.canvasHeight = canvasHeight;
            this.ground = ground;
            this.attic = attic;
        }

        public void AddRoom(Room room)
        {
            int height = floors.Count * 210 + 10;
            spriteRectangle = new Rectangle(spriteRectangle.X, spriteRectangle.Y, spriteRectangle.Width, (floors.Count + 1) * 210 + 10);
            Point point = new Point(10, height);
            room.SetMansion(this,point);
            floors.Add(room);
        }

        private void Move()
        {
            lastMouseState = currentMouseState;

            currentMouseState = Mouse.GetState();

            if(lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                spriteRectangle.Y = (currentMouseState.Y - lastMouseState.Y) + spriteRectangle.Y;
            }
            if(currentMouseState.ScrollWheelValue != 0)
            {
                spriteRectangle.Y += (currentMouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue)/12;
            }
            if (spriteRectangle.Y < canvasHeight - spriteRectangle.Height - 20)
            {
                spriteRectangle.Y = canvasHeight - spriteRectangle.Height - 20;
            }
            if (spriteRectangle.Y > canvasHeight - 130)
            {
                spriteRectangle.Y = canvasHeight - 130;
            }
        }

        public new void Update()
        {
            Move();
            foreach(Room floor in floors)
            {
                floor.Update();
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ground, new Rectangle(0, spriteRectangle.Y + spriteRectangle.Height, 800, 50), Color.White);
            base.Draw(spriteBatch);
            foreach (Room floor in floors)
            {
                floor.Draw(spriteBatch);
            }
            spriteBatch.Draw(attic, new Rectangle(75, spriteRectangle.Y - 200, 650, 200), Color.White);
        }

        internal Rectangle GetBoundries()
        {
            return spriteRectangle;
        }
    }
}
