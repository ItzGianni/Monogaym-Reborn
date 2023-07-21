using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogaym_Reborn {
    abstract internal class UIComponent : IDrawable, IUpdateable {
        protected string name;
        protected int x, y, width, height;

        protected Rectangle mainRect;

        protected SpriteFont font;

        protected bool wasMouseRightButtonReleased;
        protected bool wasMouseLeftButtonReleased;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public bool Enabled { get; set; } = true;
        public int UpdateOrder { get; set; } = 1;

        protected MouseState mouseState;
        protected Point mousePosition;

        public UIComponent(string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100) {
            this.name = name;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            mainRect = new Rectangle(x, y, width, height);

            DrawManager.AddItem(this);
            UpdateManager.AddItem(this);
        }

        abstract public void LoadContent();

        virtual public void Update(GameTime gameTime) {
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);
        }

        abstract public void Draw(SpriteBatch _spriteBatch);

        virtual protected void Destroy() {
            Console.WriteLine("UI component detroyed");
            font = null;
            UpdateManager.RemoveItem(this);
            DrawManager.RemoveItem(this);
        }
    }
}
