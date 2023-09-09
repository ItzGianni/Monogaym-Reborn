using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogaym_Reborn {
    internal class Button : UIComponent {

        protected string command;
        protected Texture2D tex;
        protected Color color;
        protected Window parentWin;

        public Button(string command, Window parentWin, string name = "Button", int x = 0, int y = 0, int width = 0, int height = 0, Color color = default) : base(name, x, y) {
            this.command = command;
            this.parentWin = parentWin;
            this.width = width == 0 ? UIManager.DefaultButtonSize.X : width;
            this.height = height == 0 ? UIManager.DefaultButtonSize.Y : height;
            this.color = color == default ? Color.White : color;
            tex = Utilities.CreateBlankTexture(width, height, color);

            DrawOrder = parentWin.DrawOrder + 1;
        }

        public override int DrawOrder { get; set; }

        public override void LoadContent() {
            font = Resources.GetFont("font");
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (mainRect.Contains(mousePosition)) {
                string s = string.Empty;
                Window w = null;
                Utilities.ProcessCommand(command, ref s, ref w);
            }
        }
        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(tex, mainRect, Color.White);
            Vector2 textPos = new Vector2();
            _spriteBatch.DrawString(font, name, textPos, Color.White);
        }
    }
}
