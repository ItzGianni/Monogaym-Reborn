using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogaym_Reborn {

    enum UIComponentType {
        ConsoleIcon, ImageIcon, GameIcon,
        ConsoleWindow, ImageWindow, GameWindow,
        InputText, Button
    }

    abstract internal class UIComponent : IDrawable, IUpdateable {
        public string name;
        public Rectangle mainRect;

        protected int x, y, width, height;
        protected UIComponentType type;
        protected SpriteFont font;
        protected bool wasMouseRightButtonReleased;
        protected bool wasMouseLeftButtonReleased;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        protected MouseState mouseState;
        protected Point mousePosition;

        public bool Enabled { get; set; } = true;
        public int UpdateOrder { get; set; } = 1;
        public string Name { get => name; }
        public UIComponentType Type => type;
        public abstract int DrawOrder { get; set; }

        public UIComponent(string name, int x, int y) {
            this.name = name;
            this.x = x;
            this.y = y;
        }

        abstract public void LoadContent();

        virtual public void Update(GameTime gameTime) {
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);
        }

        abstract public void Draw(SpriteBatch _spriteBatch);

        virtual public void Destroy() {
            Console.WriteLine("UI component detroyed");
            font = null;
            UIManager.RemoveUIComponent(this);
            UpdateManager.RemoveItem(this);
        }
    }
}
