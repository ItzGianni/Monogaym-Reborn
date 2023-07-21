using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogaym_Reborn {
    internal class Piper : IDrawable, IUpdateable {

        protected Texture2D tex;
        protected Vector2 position;
        protected Vector2 velocity;
        protected float maxSpeed;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public bool Enabled => true;
        public int UpdateOrder => 1;

        public Piper(Vector2 position = default) {
            this.position = position;
            maxSpeed = 5f;
            DrawManager.AddItem(this);
            UpdateManager.AddItem(this);
        }

        public void LoadContent() {
            tex = Resources.GetTexture("piper96");
        }

        public void Input() {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && position.Y > 0) {
                velocity.Y = -maxSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && position.Y + tex.Height < Game1._graphics.PreferredBackBufferHeight) {
                velocity.Y = maxSpeed;
            }
            else {
                velocity.Y = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && position.X > 0) {
                velocity.X = -maxSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && position.X + tex.Width < Game1._graphics.PreferredBackBufferWidth) {
                velocity.X = maxSpeed;
            }
            else {
                velocity.X = 0;
            }
        }

        public void Update(GameTime gameTime) {
            position += velocity;
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(tex, position, Color.White);
        }
    }
}
